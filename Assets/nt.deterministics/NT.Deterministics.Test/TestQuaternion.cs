using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Nt.Deterministics;
// #if UNITY_EDITOR
// using Sirenix.OdinInspector;
// #endif

public class TestQuaternion : MonoBehaviour
{
    public Quaternion q;
    public Unity.Mathematics.quaternion uq;
    public quaternion myQ;
    public float4 f4;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

#if UNITY_EDITOR
    // [Button("TestMatrix2Quaternion")]
    // public static void TestMatrix2Quaternion(float step = 5f)
    // {
    //     AutoLoadLut.AutoLoad();
    //     float range = 360f;
    //     Quaternion q1, q2, q3;
    //     Vector3 axis;
    //     UnityEngine.Matrix4x4 m;
    //     for (float a = 0f; a < range; a += step)
    //     {
    //         q1 = Quaternion.AngleAxis(a, Vector3.up);
    //         for (float b = 0f; b < range; b += step)
    //         {
    //             q2 = Quaternion.AngleAxis(b, Vector3.right);
    //             axis = q2 * q1 * Vector3.forward;
    //             for (float angle = 0f; angle < range; angle += step)
    //             {
    //                 q3 = Quaternion.AngleAxis(angle, axis);

    //                 m = Matrix4x4.Rotate(q3);

    //                 //Nt.Deterministics.float3x3 tmpM = new Nt.Deterministics.float3x3(
    //                 //    new float3((number)m.m00, (number)m.m01, (number)m.m02),
    //                 //    new float3((number)m.m10, (number)m.m11, (number)m.m12),
    //                 //    new float3((number)m.m20, (number)m.m21, (number)m.m22));
    //                 Nt.Deterministics.float4x4 tmpM = new Nt.Deterministics.float4x4(
    //                     new float4((number)m.m00, (number)m.m01, (number)m.m02, (number)m.m03),
    //                     new float4((number)m.m10, (number)m.m11, (number)m.m12, (number)m.m13),
    //                     new float4((number)m.m20, (number)m.m21, (number)m.m22, (number)m.m23),
    //                     new float4((number)m.m30, (number)m.m31, (number)m.m32, (number)m.m33));
    //                 Unity.Mathematics.float3x3 tmpM1 = new Unity.Mathematics.float3x3(
    //                     new Unity.Mathematics.float3(m.m00, m.m01, m.m02),
    //                     new Unity.Mathematics.float3(m.m10, m.m11, m.m12),
    //                     new Unity.Mathematics.float3(m.m20, m.m21, m.m22));

    //                 Nt.Deterministics.quaternion myQ = new Nt.Deterministics.quaternion(tmpM);
    //                 Unity.Mathematics.quaternion UQ = new Unity.Mathematics.quaternion(tmpM1);
    //                 float4 value_multa_delta = new float4(
    //                     math.abs(myQ.x - UQ.value.x),
    //                     math.abs(myQ.y - UQ.value.y),
    //                     math.abs(myQ.z - UQ.value.z),
    //                     math.abs(myQ.w - UQ.value.w));
    //                 float4 value_multa_delta1 = new float4(
    //                     math.abs(myQ.x + UQ.value.x),
    //                     math.abs(myQ.y + UQ.value.y),
    //                     math.abs(myQ.z + UQ.value.z),
    //                     math.abs(myQ.w + UQ.value.w));
    //                 var bLess1 = value_multa_delta < new float4((number)0.0001f);//������
    //                 var bLess2 = value_multa_delta1 < new float4((number)0.0001f);//��������෴
    //                 if (bLess1.x && bLess1.y && bLess1.z && bLess1.w || bLess2.x && bLess2.y && bLess2.z && bLess2.w)
    //                 {
    //                     //Debug.Log("��������ȷ");
    //                 }
    //                 else
    //                 {
    //                     float v1 = (float)math.max(math.max(value_multa_delta.x, value_multa_delta.y),
    //                         math.max(value_multa_delta.z, value_multa_delta.w));
    //                     float v2 = (float)math.max(math.max(value_multa_delta1.x, value_multa_delta1.y),
    //                         math.max(value_multa_delta1.z, value_multa_delta1.w));
    //                     float tmp_max = Mathf.Min(v1, v2);
    //                     Debug.LogError($"delta:{tmp_max}, {a}, {b}, {angle}, {value_multa_delta}, {value_multa_delta1}");
    //                 }
    //             }
    //         }
    //     }
    // }
    // [Button("TestQuaternionEulerAngles")]
    public static void TestQuaternionEulerAngles(float step = 5f)
    {
        AutoLoadLut.AutoLoad();
        float range = 360f;
        float max_angle_delta = 0f;
        float max_delta = 0f;
        float max_A1 = 0f, max_A2 = 0f, max_B1 = 0f, max_B2 = 0f, max_Angle1 = 0f, max_Angle2 = 0f;
        for (float a = 0f; a < range; a += step)
        {
            for (float b = 0f; b < range; b += step)
            {
                for (float angle = 0f; angle < range; angle += step)
                {
                    Quaternion q = Quaternion.identity;
                    q.eulerAngles = new Vector3(a, b, angle);
                    quaternion myQ = quaternion.identity;
                    myQ.eulerAngles = new float3((number)a, (number)b, (number)angle);

                    float4 value_delta = new float4(
                        math.abs(myQ.x - q.x),
                        math.abs(myQ.y - q.y),
                        math.abs(myQ.z - q.z),
                        math.abs(myQ.w - q.w));
                    float4 value_delta1 = new float4(
                        math.abs(myQ.x + q.x),
                        math.abs(myQ.y + q.y),
                        math.abs(myQ.z + q.z),
                        math.abs(myQ.w + q.w));
                    var bLess1 = value_delta < new float4((number)0.001f);//������
                    var bLess2 = value_delta1 < new float4((number)0.001f);//����෴
                    float tmp_max = (float)math.max(math.max(value_delta.x, value_delta.y), math.max(value_delta.z, value_delta.w));
                    float tmp_max1 = (float)math.max(math.max(value_delta1.x, value_delta1.y), math.max(value_delta1.z, value_delta1.w));
                    tmp_max = Math.Min(tmp_max, tmp_max1);
                    if (max_delta < tmp_max)
                    {
                        max_delta = tmp_max;
                        max_A1 = a;
                        max_B1 = b;
                        max_Angle1 = angle;
                    }
                    if (bLess1.x && bLess1.y && bLess1.z && bLess1.w || bLess2.x && bLess2.y && bLess2.z && bLess2.w)
                    {
                        //Debug.Log("��������ȷ");
                    }
                    //else
                    //{
                    //    Debug.LogError($"delta:{tmp_max}, {a}, {b}, {angle}, {value_delta}");
                    //}

                    Vector3 eulerAngles = q.eulerAngles;
                    float3 myEulerAngles = myQ.eulerAngles;
                    float3 angle_delta = new float3(
                        math.abs(myEulerAngles.x - eulerAngles.x), 
                        math.abs(myEulerAngles.y - eulerAngles.y), 
                        math.abs(myEulerAngles.z - eulerAngles.z));
                    angle_delta = new float3(
                        angle_delta.x > 270 ? angle_delta.x - 360 : angle_delta.x,
                        angle_delta.y > 270 ? angle_delta.y - 360 : angle_delta.y,
                        angle_delta.z > 270 ? angle_delta.z - 360 : angle_delta.z
                        );//����һ�£��������360������
                    var bLess3 = angle_delta < new float3((number)0.001f);//ŷ�������
                    tmp_max = (float)math.max(math.max(angle_delta.x, angle_delta.y), angle_delta.z);
                    if (max_angle_delta < tmp_max)
                    {
                        max_angle_delta = tmp_max;
                        max_A2 = a;
                        max_B2 = b;
                        max_Angle2 = angle;
                    }
                    if (bLess3.x && bLess3.y && bLess3.z)
                    {
                        //Debug.Log("��������ȷ");
                    }
                    //else
                    //{
                    //    Debug.LogError($"angle_delta:{tmp_max}, {a}, {b}, {angle}, {angle_delta}");
                    //}
                }
            }
        }
        if (max_delta > 0f)
            Debug.LogWarning($"max_delta = {max_delta}, {max_A1}, {max_B1}, {max_Angle1}");
        if (max_angle_delta > 0f)
            Debug.LogWarning($"max_angle_delta = {max_angle_delta}, {max_A2}, {max_B2}, {max_Angle2}");
    }
    // [Button("TestQuaternionLerp���Բ�ֵ")]
    public static void TestQuaternionLerp(float step = 5f)
    {
        AutoLoadLut.AutoLoad();
        float range = 360f;
        int krange = 20;
        Quaternion q1, q2, q3, q4;
        Vector3 axis;
        float max_delta = 0f;
        for (float a = 0f; a < range; a += step)
        {
            q1 = Quaternion.AngleAxis(a, Vector3.up);
            for (float b = 0f; b < range; b += step)
            {
                q2 = Quaternion.AngleAxis(b, Vector3.right);
                axis = q2 * q1 * Vector3.forward;
                for (float angle = 0f; angle < range; angle += step)
                {
                    q3 = Quaternion.AngleAxis(angle, axis);
                    q4 = Quaternion.AngleAxis(angle + 233f, axis);
                    for (int k = -krange; k <= 2 * krange; ++k)
                    {
                        float kstep = 1.0f * k / krange;
                        //var q = Quaternion.Lerp(q3, q4, kstep);
                        //var myQ = quaternion.Lerp(
                        //    new quaternion((number)q3.x, (number)q3.y, (number)q3.z, (number)q3.w),
                        //    new quaternion((number)q4.x, (number)q4.y, (number)q4.z, (number)q4.w),
                        //    (number)kstep
                        //    );
                        var q = Quaternion.LerpUnclamped(q3, q4, kstep);
                        var myQ = quaternion.LerpUnclamped(
                            new quaternion((number)q3.x, (number)q3.y, (number)q3.z, (number)q3.w),
                            new quaternion((number)q4.x, (number)q4.y, (number)q4.z, (number)q4.w),
                            (number)kstep
                            );

                        float4 value_multa_delta = new float4(
                            math.abs(myQ.x - q.x),
                            math.abs(myQ.y - q.y),
                            math.abs(myQ.z - q.z),
                            math.abs(myQ.w - q.w));
                        var bLess1 = value_multa_delta < new float4((number)0.0001f);//������
                        float tmp_max = (float)math.max(math.max(value_multa_delta.x, value_multa_delta.y),
                            math.max(value_multa_delta.z, value_multa_delta.w));
                        if (max_delta < tmp_max) max_delta = tmp_max;
                        if (bLess1.x && bLess1.y && bLess1.z && bLess1.w)
                        {
                            //Debug.Log("��������ȷ");
                        }
                        else
                        {
                            Debug.LogError($"delta:{tmp_max}, {a}, {b}, {angle}, {k}, {value_multa_delta}");
                        }
                    }
                }
            }
        }
        if (max_delta > 0f)
            Debug.LogWarning($"max_delta = {max_delta}");
    }
    // [Button("TestQuaternionSLerp�����ֵ")]
    public static void TestQuaternionSLerp(float step = 5f)
    {
        AutoLoadLut.AutoLoad();
        float range = 360f;
        int krange = 20;
        Quaternion q1, q2, q3, q4;
        Vector3 axis;
        float max_delta = 0f;
        for (float a = 0f; a < range; a += step)
        {
            q1 = Quaternion.AngleAxis(a, Vector3.up);
            for (float b = 0f; b < range; b += step)
            {
                q2 = Quaternion.AngleAxis(b, Vector3.right);
                axis = q2 * q1 * Vector3.forward;
                for (float angle = 0f; angle < range; angle += step)
                {
                    q3 = Quaternion.AngleAxis(angle, axis);
                    q4 = Quaternion.AngleAxis(angle + 233f, axis);
                    for (int k = -krange; k <= 2 * krange; ++k)
                    {
                        float kstep = 1.0f * k / krange;
                        //var q = Quaternion.Slerp(q3, q4, kstep);
                        //var myQ = quaternion.Slerp(
                        //    new quaternion((number)q3.x, (number)q3.y, (number)q3.z, (number)q3.w),
                        //    new quaternion((number)q4.x, (number)q4.y, (number)q4.z, (number)q4.w),
                        //    (number)kstep
                        //    );
                        var q = Quaternion.SlerpUnclamped(q3, q4, kstep);
                        var myQ = quaternion.SlerpUnclamped(
                            new quaternion((number)q3.x, (number)q3.y, (number)q3.z, (number)q3.w),
                            new quaternion((number)q4.x, (number)q4.y, (number)q4.z, (number)q4.w),
                            (number)kstep
                            );

                        float4 value_multa_delta = new float4(
                            math.abs(myQ.x - q.x),
                            math.abs(myQ.y - q.y),
                            math.abs(myQ.z - q.z),
                            math.abs(myQ.w - q.w));
                        var bLess1 = value_multa_delta < new float4((number)0.0001f);//������
                        float tmp_max = (float)math.max(math.max(value_multa_delta.x, value_multa_delta.y),
                            math.max(value_multa_delta.z, value_multa_delta.w));
                        if (max_delta < tmp_max) max_delta = tmp_max;
                        if (bLess1.x && bLess1.y && bLess1.z && bLess1.w)
                        {
                            //Debug.Log("��������ȷ");
                        }
                        else
                        {
                            Debug.LogError($"delta:{tmp_max}, {a}, {b}, {angle}, {k}, {value_multa_delta}");
                        }
                    }
                }
            }
        }
        if (max_delta > 0f)
            Debug.LogWarning($"max_delta = {max_delta}");
    }
    // [Button("TestQuaternionAngle")]
    public static void TestQuaternionAngle(float step = 5f)
    {
        AutoLoadLut.AutoLoad();
        float range = 360f;
        float max_delta = 0f;
        float max_A = 0f, max_B = 0f, max_Angle = 0f;
        for (float a = 0f; a < range; a += step)
        {
            for (float b = 0f; b < range; b += step)
            {
                for (float angle = 0f; angle < range; angle += step)
                {
                    Quaternion q1 = Quaternion.identity;
                    q1.eulerAngles = new Vector3(a, b, angle);
                    Quaternion q2 = Quaternion.identity;
                    q2.eulerAngles = new Vector3(angle, a, b);

                    float angleQ1Q2 = Quaternion.Angle(q1, q2);

                    quaternion myQ1 = new quaternion((number)q1.x, (number)q1.y, (number)q1.z, (number)q1.w);
                    quaternion myQ2 = new quaternion((number)q2.x, (number)q2.y, (number)q2.z, (number)q2.w);

                    number myAngleQ1Q2 = quaternion.Angle(myQ1, myQ2);

                    float delta = (float)number.Abs(myAngleQ1Q2 - angleQ1Q2);
                    if (max_delta < delta)
                    {
                        max_delta = delta;
                        max_A = a;
                        max_B = b;
                        max_Angle = angle;
                    }
                    if (delta < 0.001f)
                    {
                        //Debug.Log("��������ȷ");
                    }
                    //else
                    //{
                    //    Debug.LogError($"delta:{delta}, {a}, {b}, {angle}");
                    //}
                }
            }
        }
        if (max_delta > 0f)
            Debug.LogWarning($"max_delta = {max_delta}, {max_A}, {max_B}, {max_Angle}");
    }
    // [Button("TestQuaternionAngleAxis")]
    // public static void TestQuaternionAngleAxis(float step = 5f)
    // {
    //     AutoLoadLut.AutoLoad();
    //     float range = 360f;
    //     Quaternion q1, q2, q3;
    //     Vector3 axis;
    //     float max_delta = 0f;
    //     float max_A = 0f, max_B = 0f, max_Angle = 0f;
    //     for (float a = 0f; a < range; a += step)
    //     {
    //         q1 = Quaternion.AngleAxis(a, Vector3.up);
    //         for (float b = 0f; b < range; b += step)
    //         {
    //             q2 = Quaternion.AngleAxis(b, Vector3.right);
    //             axis = q2 * q1 * Vector3.forward;
    //             for (float angle = 0f; angle < range; angle += step)
    //             {
    //                 q3 = Quaternion.AngleAxis(angle, axis);
    //                 quaternion myQ = quaternion.AngleAxis((number)angle, (float3)axis);
    //                 //q3 = Quaternion.AxisAngle(axis, angle * Mathf.PI / 180f);
    //                 //quaternion myQ = quaternion.AxisAngle((float3)axis, (number)(angle * Mathf.PI / 180f));

    //                 float4 value_multa_delta = new float4(
    //                     math.abs(myQ.x - q3.x),
    //                     math.abs(myQ.y - q3.y),
    //                     math.abs(myQ.z - q3.z),
    //                     math.abs(myQ.w - q3.w));
    //                 float4 value_multa_delta1 = new float4(
    //                     math.abs(myQ.x + q3.x),
    //                     math.abs(myQ.y + q3.y),
    //                     math.abs(myQ.z + q3.z),
    //                     math.abs(myQ.w + q3.w));
    //                 var bLess1 = value_multa_delta < new float4((number)0.0001f);//������
    //                 var bLess2 = value_multa_delta1 < new float4((number)0.0001f);//��������෴
    //                 float v1 = (float)math.max(math.max(value_multa_delta.x, value_multa_delta.y),
    //                     math.max(value_multa_delta.z, value_multa_delta.w));
    //                 float v2 = (float)math.max(math.max(value_multa_delta1.x, value_multa_delta1.y),
    //                     math.max(value_multa_delta1.z, value_multa_delta1.w));
    //                 float tmp_max = Mathf.Min(v1, v2);
    //                 if (max_delta < tmp_max)
    //                 {
    //                     max_delta = tmp_max;
    //                     max_A = a;
    //                     max_B = b;
    //                     max_Angle = angle;
    //                 }
    //                 if (bLess1.x && bLess1.y && bLess1.z && bLess1.w || bLess2.x && bLess2.y && bLess2.z && bLess2.w)
    //                 {
    //                     //Debug.Log("��������ȷ");
    //                 }
    //                 //else
    //                 //{
    //                 //    Debug.LogError($"delta:{tmp_max}, {a}, {b}, {angle}, {value_multa_delta}, {value_multa_delta1}");
    //                 //}
    //             }
    //         }
    //     }
    //     if (max_delta > 0f)
    //         Debug.LogWarning($"max_delta = {max_delta}, {max_A}, {max_B}, {max_Angle}");
    // }
    // [Button("TestQuaternionToAngleAxis")]
    public void TestQuaternionToAngleAxis(float step = 5f)
    {
        AutoLoadLut.AutoLoad();
        float range = 360f;
        Quaternion q1, q2, q3;
        Vector3 axis;
        float max_delta = 0f;
        float max_A = 0f, max_B = 0f, max_Angle = 0f;
        for (float a = 0f; a < range; a += step)
        {
            q1 = Quaternion.AngleAxis(a, Vector3.up);
            for (float b = 0f; b < range; b += step)
            {
                q2 = Quaternion.AngleAxis(b, Vector3.right);
                axis = q2 * q1 * Vector3.forward;
                for (float angle = 0f; angle < range; angle += step)
                {
                    q3 = Quaternion.AngleAxis(angle, axis);
                    quaternion myQ = new quaternion((number)q3.x, (number)q3.y, (number)q3.z, (number)q3.w);

                    q3.ToAngleAxis(out float qAngle, out Vector3 qAxis);
                    myQ.ToAngleAxis(out number myAngle, out float3 myAxis);
                    //q3.ToAxisAngle(out Vector3 qAxis, out float qAngle);
                    //myQ.ToAxisAngle(out float3 myAxis, out number myAngle);

                    float4 value_multa_delta = new float4(
                        math.abs(myAxis.x - qAxis.x),
                        math.abs(myAxis.y - qAxis.y),
                        math.abs(myAxis.z - qAxis.z),
                        math.abs(myAngle - qAngle));
                    float4 value_multa_delta1 = new float4(
                        math.abs(myAxis.x + qAxis.x),
                        math.abs(myAxis.y + qAxis.y),
                        math.abs(myAxis.z + qAxis.z),
                        math.abs(myAngle + qAngle));
                    var bLess1 = value_multa_delta < new float4((number)0.0001f);//������
                    var bLess2 = value_multa_delta1 < new float4((number)0.0001f);//��������෴
                    float v1 = (float)math.max(math.max(value_multa_delta.x, value_multa_delta.y),
                        math.max(value_multa_delta.z, value_multa_delta.w));
                    float v2 = (float)math.max(math.max(value_multa_delta1.x, value_multa_delta1.y),
                        math.max(value_multa_delta1.z, value_multa_delta1.w));
                    float tmp_max = Mathf.Min(v1, v2);
                    if (max_delta < tmp_max)
                    {
                        max_delta = tmp_max;
                        max_A = a;
                        max_B = b;
                        max_Angle = angle;
                    }
                    if (bLess1.x && bLess1.y && bLess1.z && bLess1.w || bLess2.x && bLess2.y && bLess2.z && bLess2.w)
                    {
                        //Debug.Log("��������ȷ");
                    }
                    //else
                    //{
                    //    Debug.LogError($"delta:{tmp_max}, {a}, {b}, {angle}, {value_multa_delta}, {value_multa_delta1}");
                    //}
                }
            }
        }
        if (max_delta > 0f)
            Debug.LogWarning($"max_delta = {max_delta}, {max_A}, {max_B}, {max_Angle}");
    }
    // [Button("TestQuaternion.RotatePoint")]
    public void TestQuaternionRotatePoint(float step = 5f)
    {
        AutoLoadLut.AutoLoad();
        float range = 360f;
        float max_delta = 0f;
        float max_A = 0f, max_B = 0f, max_Angle = 0f;
        for (float a = 0f; a < range; a += step)
        {
            for (float b = 0f; b < range; b += step)
            {
                for (float angle = 0f; angle < range; angle += step)
                {
                    Quaternion q = Quaternion.identity;
                    q.eulerAngles = new Vector3(a, b, angle);
                    Vector3 point0 = new Vector3(1.23f, 45.1f, 78.0f);
                    Vector3 point1 = q * point0;

                    quaternion myQ = quaternion.identity;
                    myQ.eulerAngles = new float3((number)a, (number)b, (number)angle);
                    float3 myPoint0 = new float3((number)point0.x, (number)point0.y, (number)point0.z);
                    float3 myPoint1 = myQ * myPoint0;

                    float3 value_multa_delta = new float3(
                        math.abs(myPoint1.x - point1.x),
                        math.abs(myPoint1.y - point1.y),
                        math.abs(myPoint1.z - point1.z));
                    var bLess1 = value_multa_delta < new float3((number)0.0001f);//������
                    float tmp_max = (float)math.max(math.max(value_multa_delta.x, value_multa_delta.y), value_multa_delta.z);
                    if (max_delta < tmp_max)
                    {
                        max_delta = tmp_max;
                        max_A = a;
                        max_B = b;
                        max_Angle = angle;
                    }
                    if (bLess1.x && bLess1.y && bLess1.z)
                    {
                        //Debug.Log("��������ȷ");
                    }
                    //else
                    //{
                    //    Debug.LogError($"delta:{tmp_max}, {a}, {b}, {angle}, {value_multa_delta}, {value_multa_delta1}");
                    //}
                }
            }
        }
        if (max_delta > 0f)
            Debug.LogWarning($"max_delta = {max_delta}, {max_A}, {max_B}, {max_Angle}");
    }
    // [Button("TestQuaternion.Cross.Quaternion")]
    public void TestQuaternionCrossQuaternion(float step = 5f)
    {
        AutoLoadLut.AutoLoad();
        float range = 360f;
        float max_delta = 0f;
        float max_A = 0f, max_B = 0f, max_Angle = 0f;
        for (float a = 0f; a < range; a += step)
        {
            for (float b = 0f; b < range; b += step)
            {
                for (float angle = 0f; angle < range; angle += step)
                {
                    Quaternion q0 = Quaternion.identity;
                    q0.eulerAngles = new Vector3(a, 0f, angle);
                    Quaternion q1 = Quaternion.identity;
                    q1.eulerAngles = new Vector3(0f, b, angle / 2);

                    var q = q0 * q1;

                    quaternion myQ0 = quaternion.identity;
                    myQ0.eulerAngles = new float3((number)a, number.zero, (number)angle);
                    quaternion myQ1 = quaternion.identity;
                    myQ1.eulerAngles = new float3(number.zero, (number)b, (number)angle / 2);

                    var myQ = myQ0 * myQ1;

                    float4 value_multa_delta = new float4(
                        math.abs(myQ.x - q.x),
                        math.abs(myQ.y - q.y),
                        math.abs(myQ.z - q.z),
                        math.abs(myQ.w - q.w));
                    var bLess1 = value_multa_delta < new float4((number)0.0001f);//������
                    float tmp_max = (float)math.max(math.max(value_multa_delta.x, value_multa_delta.y), 
                        math.max(value_multa_delta.x, value_multa_delta.w));
                    if (max_delta < tmp_max)
                    {
                        max_delta = tmp_max;
                        max_A = a;
                        max_B = b;
                        max_Angle = angle;
                    }
                    if (bLess1.x && bLess1.y && bLess1.z)
                    {
                        //Debug.Log("��������ȷ");
                    }
                    //else
                    //{
                    //    Debug.LogError($"delta:{tmp_max}, {a}, {b}, {angle}, {value_multa_delta}, {value_multa_delta1}");
                    //}
                }
            }
        }
        if (max_delta > 0f)
            Debug.LogWarning($"max_delta = {max_delta}, {max_A}, {max_B}, {max_Angle}");
    }
#endif
}
