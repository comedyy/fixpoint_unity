//
// @brief: 定点数Inspector
// @version: 1.0.0
// @author nt
// @date: 2021.06.21
// 
// 
//

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Mathematics.FixedPoint;
using Unity.Mathematics;

namespace Mathematics.FixedPoint.Editor
{
    struct fieldData
    {
        public object parent;
        public FieldInfo field;
        public object value;
        public int idx;
    }
    class BaseDrawer
    {
        private static Type TypeNumber = typeof(fp);
        private static Type TypeNumber2 = typeof(fp2);
        private static Type TypeNumber3 = typeof(fp3);
        private static Type TypeNumber4 = typeof(fp4);
        private static Type TypeNumber2x2 = typeof(float2x2);
        private static Type TypeNumber3x2 = typeof(float3x2);
        private static Type TypeNumber4x2 = typeof(float4x2);
        private static Type TypeNumber2x3 = typeof(float2x3);
        private static Type TypeNumber3x3 = typeof(fp3x3);
        private static Type TypeNumber4x3 = typeof(float4x3);
        private static Type TypeNumber2x4 = typeof(float2x4);
        private static Type TypeNumber3x4 = typeof(float3x4);
        private static Type TypeNumber4x4 = typeof(float4x4);
        private static bool IsTargetType(Type t)
        {
            return t.Equals(TypeNumber) || t.Equals(TypeNumber2) || t.Equals(TypeNumber3) || t.Equals(TypeNumber4) ||
                t.Equals(TypeNumber2x2) || t.Equals(TypeNumber2x3) || t.Equals(TypeNumber2x4) ||
                t.Equals(TypeNumber3x2) || t.Equals(TypeNumber3x3) || t.Equals(TypeNumber3x4) ||
                t.Equals(TypeNumber4x2) || t.Equals(TypeNumber4x3) || t.Equals(TypeNumber4x4);
        }
        public static object GetParentObjectOfProperty(string path, object obj, List<fieldData> lstFieldData)
        {
            List<string> fields = new List<string>(path.Split('.'));
            while (true)
            {
                if (fields.Count == 1 || fields.Count == 0)
                    return obj;
                var t = obj.GetType();
                var f = t.GetField(fields[0], BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                if (null == f)
                {
                    Debug.LogError("BaseDrawer::GetParentObjectOfProperty - f is null, path = " + path);
                    return obj;
                }
                var childValue = f.GetValue(obj);
                //if (IsTargetType(childValue.GetType()))
                lstFieldData.Add(new fieldData
                {
                    parent = obj,
                    field = f,
                    value = childValue,
                    idx = -1
                });
                if (childValue is System.Collections.IList lstValue && fields.Count > 2 && fields[1].Equals("Array") 
                    && fields[2].StartsWith("data["))
                {
                    int tmpidx = int.Parse(fields[2].Substring(5, fields[2].Length - 6));
                    lstFieldData.Add(new fieldData {
                        parent = childValue,
                        field = null,
                        value = lstValue[tmpidx],
                        idx = tmpidx,
                    });

                    obj = lstValue[tmpidx];
                    fields.RemoveRange(0, 3);
                }
                else
                {
                    fields.RemoveAt(0);
                    obj = childValue;
                }
            }
        }
        public static void ResetLabelText(PropertyDrawer drawer, GUIContent label)
        {
            if (null == drawer || null == drawer.fieldInfo || null == label) return;
            if (drawer.fieldInfo.IsDefined(typeof(NumberLabelAttribute)))
            {
                var labels = drawer.fieldInfo.GetCustomAttributes(typeof(NumberLabelAttribute)).ToList();
                if (null != labels && labels.Count > 0)
                    label.text = (labels[0] as NumberLabelAttribute).title;
            }
        }
    }

    // [CustomPropertyDrawer(typeof(number))]
    // public class NumberDrawer : PropertyDrawer
    // {
    //     // Draw the property inside the given rect
    //     public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    //     {
    //         using (new EditorGUI.PropertyScope(position, label, property))
    //         {
    //             BaseDrawer.ResetLabelText(this, label);
    //             List<fieldData> lstFieldData = new List<fieldData>();
    //             var parent = BaseDrawer.GetParentObjectOfProperty(property.propertyPath, property.serializedObject.targetObject, lstFieldData);
    //             var type = parent.GetType();
    //             number value;
    //             number last;
    //             if (type.Equals(typeof(number)))
    //             {
    //                 value = last = (number)parent;
    //                 value = (number)EditorGUI.FloatField(position, label, (float)value);
    //                 parent = value;
    //             }
    //             else
    //             {
    //                 var f = type.GetField(property.name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
    //                 var o = f.GetValue(parent);
    //                 value = last = (number)o;
    //                 value = (number)EditorGUI.FloatField(position, label, (float)value);
    //                 f.SetValue(parent, value);
    //             }
    //             if (!value.Equals(last))
    //             {
    //                 //Debug.LogErrorFormat("path={0}, name={1}, value : {2} => {3}", property.propertyPath, property.name, o.ToString(), value.ToString());
    //                 object lastValue = parent;
    //                 for (int k = lstFieldData.Count - 1; k >= 0; --k)
    //                 {
    //                     fieldData fd = lstFieldData[k];
    //                     if (fd.idx > -1 && fd.parent is System.Collections.IList lstParent)
    //                     {
    //                         lstParent[fd.idx] = lastValue;
    //                     }
    //                     else
    //                         fd.field.SetValue(fd.parent, lastValue);
    //                     lastValue = fd.parent;
    //                 }
    //                 EditorUtility.SetDirty(property.serializedObject.targetObject);
    //             }
    //         }
    //     }
    // }

    [CustomPropertyDrawer(typeof(fp2)), CustomPropertyDrawer(typeof(fp3)), CustomPropertyDrawer(typeof(fp4)), CustomPropertyDrawer(typeof(fpQuaternion))]
    [CustomPropertyDrawer(typeof(DoNotNormalizeAttribute))]
    class PrimitiveVectorDrawer : PropertyDrawer
    {
        private string _PropertyType;

        string GetPropertyType(SerializedProperty property)
        {
            if (_PropertyType == null)
            {
                _PropertyType = property.type;
                var isManagedRef = property.type.StartsWith("managedReference", StringComparison.Ordinal);
                if (isManagedRef)
                {
                    var startIndex = "managedReference<".Length;
                    var length = _PropertyType.Length - startIndex - 1;
                    _PropertyType = _PropertyType.Substring("managedReference<".Length, length);
                }
            }

            return _PropertyType;
        }

        static class Content
        {
            public static readonly string doNotNormalizeCompatibility = L10n.Tr(
                $"{typeof(DoNotNormalizeAttribute).Name} only works with {typeof(fpQuaternion)} and primitive vector types."
            );
            public static readonly string doNotNormalizeTooltip =
                L10n.Tr("This value is not normalized, which may produce unexpected results.");

            public static readonly GUIContent[] labels2 = { new GUIContent("X"), new GUIContent("Y") };
            public static readonly GUIContent[] labels3 = { new GUIContent("X"), new GUIContent("Y"), new GUIContent("Z") };
            public static readonly GUIContent[] labels4 = { new GUIContent("X"), new GUIContent("Y"), new GUIContent("Z"), new GUIContent("W") };
        }

        public override bool CanCacheInspectorGUI(SerializedProperty property)
        {
            return false;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var height = EditorGUIUtility.singleLineHeight;
            if (!EditorGUIUtility.wideMode)
                height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            return height;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var subLabels = Content.labels4;
            var startIter = "x";
            var propertyType = GetPropertyType(property);
            switch (propertyType[propertyType.Length - 1])
            {
                case '2':
                    subLabels = Content.labels2;
                    break;
                case '3':
                    subLabels = Content.labels3;
                    break;
                case '4':
                    subLabels = Content.labels4;
                    break;
                default:
                    {
                        if (property.type == nameof(fpQuaternion))
                            startIter = "x";
                        else if (attribute is DoNotNormalizeAttribute)
                        {
                            EditorGUI.HelpBox(EditorGUI.PrefixLabel(position, label), Content.doNotNormalizeCompatibility, MessageType.None);
                            return;
                        }
                        break;
                    }
            }

            if (attribute is DoNotNormalizeAttribute && string.IsNullOrEmpty(label.tooltip))
                label.tooltip = Content.doNotNormalizeTooltip;

            BaseDrawer.ResetLabelText(this, label);
            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.MultiPropertyField(position, subLabels, property.FindPropertyRelative(startIter), label);
            EditorGUI.EndProperty();
        }
    }

    [CustomPropertyDrawer(typeof(float2x2)), CustomPropertyDrawer(typeof(float2x3)), CustomPropertyDrawer(typeof(float2x4))]
    [CustomPropertyDrawer(typeof(float3x2)), CustomPropertyDrawer(typeof(fp3x3)), CustomPropertyDrawer(typeof(float3x4))]
    [CustomPropertyDrawer(typeof(float4x2)), CustomPropertyDrawer(typeof(float4x3)), CustomPropertyDrawer(typeof(float4x4))]
    class NumberMatrixDrawer : PropertyDrawer
    {
        public override bool CanCacheInspectorGUI(SerializedProperty property)
        {
            return false;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!property.isExpanded)
                return EditorGUIUtility.singleLineHeight;
            var rows = 1 + property.type[property.type.Length - 3] - '0';
            return rows * EditorGUIUtility.singleLineHeight + (rows - 1) * EditorGUIUtility.standardVerticalSpacing;
        }

        static ReadOnlyCollection<string> k_ColPropertyPaths =
            new ReadOnlyCollection<string>(new[] { "c0", "c1", "c2", "c3" });
        static ReadOnlyCollection<string> k_RowPropertyPaths =
            new ReadOnlyCollection<string>(new[] { "x", "y", "z", "w" });

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            BaseDrawer.ResetLabelText(this, label);
            position.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(position, property, label, false);

            if (Event.current.type == EventType.ContextClick && position.Contains(Event.current.mousePosition))
            {
                DoUtilityMenu(property);
                Event.current.Use();
            }

            if (!property.isExpanded)
                return;

            var rows = property.type[property.type.Length - 3] - '0';
            var cols = property.type[property.type.Length - 1] - '0';

            ++EditorGUI.indentLevel;
            position = EditorGUI.IndentedRect(position);
            --EditorGUI.indentLevel;

            var elementType = property.FindPropertyRelative("c0.x").propertyType;
            for (var row = 0; row < rows; ++row)
            {
                position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
                var elementRect = new Rect(position)
                {
                    width = elementType == SerializedPropertyType.Boolean
                        ? EditorGUIUtility.singleLineHeight
                        : (position.width - (cols - 1) * EditorGUIUtility.standardVerticalSpacing) / cols
                };
                for (var col = 0; col < cols; ++col)
                {
                    EditorGUI.PropertyField(
                        elementRect,
                        property.FindPropertyRelative($"{k_ColPropertyPaths[col]}.{k_RowPropertyPaths[row]}"),
                        GUIContent.none
                    );
                    elementRect.x += elementRect.width + EditorGUIUtility.standardVerticalSpacing;
                }
            }
        }

        Dictionary<SerializedPropertyType, Action<SerializedProperty, bool>> k_UtilityValueSetters =
            new Dictionary<SerializedPropertyType, Action<SerializedProperty, bool>>
            {
                { SerializedPropertyType.Boolean, (property, b) => property.boolValue = b },
                { SerializedPropertyType.Float, (property, b) => property.floatValue = b ? 1f : 0f },
                { SerializedPropertyType.Integer, (property, b) => property.intValue = b ? 1 : 0 }
            };

        void DoUtilityMenu(SerializedProperty property)
        {
            var rows = property.type[property.type.Length - 3] - '0';
            var cols = property.type[property.type.Length - 1] - '0';
            var elementType = property.FindPropertyRelative("c0.x").propertyType;
            var setValue = k_UtilityValueSetters[elementType];
            var menu = new GenericMenu();
            property = property.Copy();
            menu.AddItem(
                EditorGUIUtility.TrTextContent("Set to Zero"),
                false,
                () =>
                {
                    property.serializedObject.Update(); ;
                    for (var row = 0; row < rows; ++row)
                        for (var col = 0; col < cols; ++col)
                            setValue(
                                property.FindPropertyRelative($"{k_ColPropertyPaths[col]}.{k_RowPropertyPaths[row]}"),
                                false
                            );
                    property.serializedObject.ApplyModifiedProperties();
                }
            );
            if (rows == cols)
            {
                menu.AddItem(
                    EditorGUIUtility.TrTextContent("Reset to Identity"),
                    false,
                    () =>
                    {
                        property.serializedObject.Update();
                        for (var row = 0; row < rows; ++row)
                            for (var col = 0; col < cols; ++col)
                                setValue(
                                    property.FindPropertyRelative($"{k_ColPropertyPaths[col]}.{k_RowPropertyPaths[row]}"),
                                    row == col
                                );
                        property.serializedObject.ApplyModifiedProperties();
                    }
                );
            }
            menu.ShowAsContext();
        }
    }
}