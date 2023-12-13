using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Nt.Deterministics
{
    public enum RotationOrder : byte
    {
        XYZ,
        XZY,
        YXZ,
        YZX,
        ZXY,
        ZYX,
        Default = ZXY
    }
    [System.Serializable]
    public struct quaternion : IEquatable<quaternion>, IFormattable
    {
        /// <summary>
        /// x
        /// </summary>
        public number x;

        /// <summary>
        /// y
        /// </summary>
        public number y;

        /// <summary>
        /// z
        /// </summary>
        public number z;

        /// <summary>
        /// w
        /// </summary>
        public number w;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        /// <param name="z">z</param>
        /// <param name="w">w</param>
        public quaternion(number x, number y, number z, number w)
        {
            this.x.RawValue = x.RawValue;
            this.y.RawValue = y.RawValue;
            this.z.RawValue = z.RawValue;
            this.w.RawValue = w.RawValue;
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="value">float4</param>
        public quaternion(float4 value)
        {
            this.x.RawValue = value.x.RawValue;
            this.y.RawValue = value.y.RawValue;
            this.z.RawValue = value.z.RawValue;
            this.w.RawValue = value.w.RawValue;
        }

        /// <summary>
        /// ctor. from matrix 3x3
        /// </summary>
        /// <param name="m">matrix</param>
        public quaternion(float3x3 m)
        {
            float3 u = m.c0;
            float3 v = m.c1;
            float3 w = m.c2;
            number s = new number(constant.sign64);
            number u_sign = u.x & s;
            number t = u_sign.RawValue == 0L ? (v.y + w.z) : (v.y - w.z);
            float4 u_mask = new float4(u_sign >> 63);
            float4 t_mask = new float4(t >> 63);
            number tr = number.one + number.Abs(u.x);
            float4 sign_flips = new float4(number.zero, s, s, s) ^ (u_mask & new float4(number.zero, s, number.zero, s)) 
                ^ (t_mask & new float4(s, s, s, number.zero));
            bool bSign0 = sign_flips.x.RawValue == 0L;
            bool bSign1 = sign_flips.y.RawValue == 0L;
            bool bSign2 = sign_flips.z.RawValue == 0L;
            bool bSign3 = sign_flips.w.RawValue == 0L;
            float4 value = new float4(tr, u.y, w.x, v.z) + new float4(
                bSign0 ? t : -t,
                bSign1 ? v.x : -v.x,
                bSign2 ? u.z : -u.z,
                bSign3 ? w.y : -w.y);

            value = (value & ~u_mask) | (value.zwxy & u_mask);
            value = (value.wzyx & ~t_mask) | (value & t_mask);
            value = math.normalize(value);
            this.x.RawValue = value.x.RawValue;
            this.y.RawValue = value.y.RawValue;
            this.z.RawValue = value.z.RawValue;
            this.w.RawValue = value.w.RawValue;
        }

        // /// <summary>
        // /// get or set value of x/y/z/w by index
        // /// </summary>
        // /// <param name="index">index</param>
        // /// <returns>value of x/y/z/w</returns>
        // public number this[int index]
        // {
        //     [MethodImpl(MethodImplOptions.AggressiveInlining)]
        //     get
        //     {
        //         switch(index)
        //         {
        //             case 0: return this.x;
        //             case 1: return this.y;
        //             case 2: return this.z;
        //             case 3: return this.w;
        //             default: throw new IndexOutOfRangeException($"Invalid quaternion index! {index}");
        //         }
        //     }
        //     [MethodImpl(MethodImplOptions.AggressiveInlining)]
        //     set
        //     {
        //         switch (index)
        //         {
        //             case 0:
        //                 this.x = value;
        //                 break;
        //             case 1:
        //                 this.y = value;
        //                 break;
        //             case 2:
        //                 this.z = value;
        //                 break;
        //             case 3:
        //                 this.w = value;
        //                 break;
        //             default:
        //                 throw new IndexOutOfRangeException($"Invalid quaternion index! {index}");
        //         }
        //     }
        // }

        /// <summary>
        /// quaternion(0, 0, 0, 1)
        /// </summary>
        public static quaternion identity
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => new quaternion(number.zero, number.zero, number.zero, number.one);
        }

        // /// <summary>
        // /// get or set euler angles (ZXY)
        // /// </summary>
        // public float3 eulerAngles
        // {
        //     [MethodImpl(MethodImplOptions.AggressiveInlining)]
        //     get
        //     {
        //         //��ZXY˳�򣬽���ŷ����
        //         float3 euler;
        //         number t = w * x - y * z;
        //         if (number.Abs(t).RawValue > number.half.RawValue - number.SMALL_SQRT) // ������̬,������Ϊ��90��
        //         {
        //             bool bSign = (t.RawValue & long.MinValue) == 0L;
        //             euler.x = bSign ? number.PIDiv2 : -number.PIDiv2;//pitch
        //             //�˿̣�w * y + x * z = 0 �� number.half - x * x - y * y = 0�����Բ���ֱ�������·�ʽ����euler.y
        //             //euler.y = number.Atan2(w * y + x * z, number.half - x * x - y * y);//yaw
        //             euler.y = (bSign ? -2 : 2) * number.Atan2(z, w);
        //             euler.z = number.zero;//roll
        //         }
        //         else
        //         {
        //             euler.x = number.Asin(2 * t);
        //             euler.y = number.Atan2(w * y + x * z, number.half - x * x - y * y);
        //             euler.z = number.Atan2(w * z + x * y, number.half - x * x - z * z);
        //         }
        //         euler *= number.Rad2Deg;
        //         return euler;
        //     }
        //     [MethodImpl(MethodImplOptions.AggressiveInlining)]
        //     set
        //     {
        //         //��ZXY˳�򣬶�ӦUnity.Mathematics.quaternion.EulerZXY
        //         float3 rad = number.half * value * number.Deg2Rad;
        //         math.sincos(rad, out float3 s, out float3 c);
        //         x = s.x * c.y * c.z + c.x * s.y * s.z;
        //         y = c.x * s.y * c.z - s.x * c.y * s.z;
        //         z = c.x * c.y * s.z - s.x * s.y * c.z;
        //         w = c.x * c.y * c.z + s.x * s.y * s.z;
        //     }
        // }

        /// <summary>
        /// get normalized quaternion
        /// </summary>
        public quaternion normalized
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => quaternion.normalize(this);
        }

        // /// <summary>
        // /// get the angle between two quaternion a and b
        // /// </summary>
        // /// <param name="a"></param>
        // /// <param name="b"></param>
        // /// <returns></returns>
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // public static number Angle(quaternion a, quaternion b)
        // {
        //     number dot = quaternion.Dot(a, b);
        //     bool isEqual = quaternion.IsEqualUsingDot(dot);
        //     return isEqual ? number.zero : (number.Acos(number.Min(number.Abs(dot), number.one)) * 2 * number.Rad2Deg);
        // }

        /// <summary>
        /// construct quaternion from axis and angle
        /// </summary>
        /// <param name="angle">the degree of angle</param>
        /// <param name="axis">the axis of normalized vector3</param>
        /// <returns>quaternion</returns>
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // public static quaternion AngleAxis(number angle, float3 axis)
        // {
        //     math.sincos(number.half * angle * number.Deg2Rad, out number sina, out number cosa);
        //     return new quaternion(math.float4(axis * sina, cosa));
        // }

        /// <summary>
        /// construct quaternion from axis and angle
        /// </summary>
        /// <param name="axis">the aixs</param>
        /// <param name="angle">the radian of angle</param>
        /// <returns>quaternion</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [Obsolete("Use quaternion.AngleAxis instead. This function was deprecated because it uses radians instead of degrees")]
        public static quaternion AxisAngle(float3 axis, number angle)
        {
            math.sincos(number.half * angle, out number sina, out number cosa);
            return new quaternion(math.float4(axis * sina, cosa));
        }

        /// <summary>
        /// dot value of two quaternion a and b
        /// </summary>
        /// <param name="a">quaternion a</param>
        /// <param name="b">quaternion b</param>
        /// <returns>dot value</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Dot(quaternion a, quaternion b) => a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;

#region  Euler
        /// <summary>
        /// construct quaternion from euler angles.(XYZ)
        /// </summary>
        /// <param name="euler">euler angles in radians</param>
        /// <returns>quaternion</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion EulerXYZ(float3 xyz)
        {
            math.sincos(number.half * xyz, out float3 s, out float3 c);
            return new quaternion(s.x * c.y * c.z - c.x * s.y * s.z,
                c.x * s.y * c.z + s.x * c.y * s.z,
                c.x * c.y * s.z - s.x * s.y * c.z,
                c.x * c.y * c.z + s.x * s.y * s.z);
        }

        /// <summary>
        /// construct quaternion from euler angles.(XZY)
        /// </summary>
        /// <param name="euler">euler angles in radians</param>
        /// <returns>quaternion</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion EulerXZY(float3 xyz)
        {
            math.sincos(number.half * xyz, out float3 s, out float3 c);
            return new quaternion(s.x * c.y * c.z + c.x * s.y * s.z,
                c.x * s.y * c.z + s.x * c.y * s.z,
                c.x * c.y * s.z - s.x * s.y * c.z,
                c.x * c.y * c.z - s.x * s.y * s.z);
        }

        /// <summary>
        /// construct quaternion from euler angles.(YXZ)
        /// </summary>
        /// <param name="euler">euler angles in radians</param>
        /// <returns>quaternion</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion EulerYXZ(float3 xyz)
        {
            math.sincos(number.half * xyz, out float3 s, out float3 c);
            return new quaternion(s.x * c.y * c.z - c.x * s.y * s.z,
                c.x * s.y * c.z + s.x * c.y * s.z,
                c.x * c.y * s.z + s.x * s.y * c.z,
                c.x * c.y * c.z - s.x * s.y * s.z);
        }

        /// <summary>
        /// construct quaternion from euler angles.(YZX)
        /// </summary>
        /// <param name="euler">euler angles in radians</param>
        /// <returns>quaternion</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion EulerYZX(float3 xyz)
        {
            math.sincos(number.half * xyz, out float3 s, out float3 c);
            return new quaternion(s.x * c.y * c.z - c.x * s.y * s.z,
                c.x * s.y * c.z - s.x * c.y * s.z,
                c.x * c.y * s.z + s.x * s.y * c.z,
                c.x * c.y * c.z + s.x * s.y * s.z);
        }

        /// <summary>
        /// construct quaternion from euler angles.(ZXY)
        /// </summary>
        /// <param name="euler">euler angles in radians</param>
        /// <returns>quaternion</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion EulerZXY(float3 xyz)
        {
            math.sincos(number.half * xyz, out float3 s, out float3 c);
            return new quaternion(s.x * c.y * c.z + c.x * s.y * s.z,
                c.x * s.y * c.z - s.x * c.y * s.z,
                c.x * c.y * s.z - s.x * s.y * c.z,
                c.x * c.y * c.z + s.x * s.y * s.z);
        }

        /// <summary>
        /// construct quaternion from euler angles.(ZYX)
        /// </summary>
        /// <param name="euler">euler angles in radians</param>
        /// <returns>quaternion</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion EulerZYX(float3 xyz)
        {
            math.sincos(number.half * xyz, out float3 s, out float3 c);
            return new quaternion(s.x * c.y * c.z + c.x * s.y * s.z,
                c.x * s.y * c.z - s.x * c.y * s.z,
                c.x * c.y * s.z + s.x * s.y * c.z,
                c.x * c.y * c.z - s.x * s.y * s.z);
        }

        /// <summary>
        /// construct quaternion from euler angles.(XYZ)
        /// </summary>
        /// <param name="x">the x euler angle in radians</param>
        /// <param name="y">the y euler angle in radians</param>
        /// <param name="z">the z euler angle in radians</param>
        /// <returns>quaternion</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion EulerXYZ(number x, number y, number z)
        {
            math.sincos(number.half * new float3(x, y, z), out float3 s, out float3 c);
            return new quaternion(s.x * c.y * c.z - c.x * s.y * s.z,
                c.x * s.y * c.z + s.x * c.y * s.z,
                c.x * c.y * s.z - s.x * s.y * c.z,
                c.x * c.y * c.z + s.x * s.y * s.z);
        }

        /// <summary>
        /// construct quaternion from euler angles.(XZY)
        /// </summary>
        /// <param name="x">the x euler angle in radians</param>
        /// <param name="y">the y euler angle in radians</param>
        /// <param name="z">the z euler angle in radians</param>
        /// <returns>quaternion</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion EulerXZY(number x, number y, number z)
        {
            math.sincos(number.half * new float3(x, y, z), out float3 s, out float3 c);
            return new quaternion(s.x * c.y * c.z + c.x * s.y * s.z,
                c.x * s.y * c.z + s.x * c.y * s.z,
                c.x * c.y * s.z - s.x * s.y * c.z,
                c.x * c.y * c.z - s.x * s.y * s.z);
        }

        /// <summary>
        /// construct quaternion from euler angles.(YXZ)
        /// </summary>
        /// <param name="x">the x euler angle in radians</param>
        /// <param name="y">the y euler angle in radians</param>
        /// <param name="z">the z euler angle in radians</param>
        /// <returns>quaternion</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion EulerYXZ(number x, number y, number z)
        {
            math.sincos(number.half * new float3(x, y, z), out float3 s, out float3 c);
            return new quaternion(s.x * c.y * c.z - c.x * s.y * s.z,
                c.x * s.y * c.z + s.x * c.y * s.z,
                c.x * c.y * s.z + s.x * s.y * c.z,
                c.x * c.y * c.z - s.x * s.y * s.z);
        }

        /// <summary>
        /// construct quaternion from euler angles.(YZX)
        /// </summary>
        /// <param name="x">the x euler angle in radians</param>
        /// <param name="y">the y euler angle in radians</param>
        /// <param name="z">the z euler angle in radians</param>
        /// <returns>quaternion</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion EulerYZX(number x, number y, number z)
        {
            math.sincos(number.half * new float3(x, y, z), out float3 s, out float3 c);
            return new quaternion(s.x * c.y * c.z - c.x * s.y * s.z,
                c.x * s.y * c.z - s.x * c.y * s.z,
                c.x * c.y * s.z + s.x * s.y * c.z,
                c.x * c.y * c.z + s.x * s.y * s.z);
        }

        /// <summary>
        /// construct quaternion from euler angles.(ZXY)
        /// </summary>
        /// <param name="x">the x euler angle in radians</param>
        /// <param name="y">the y euler angle in radians</param>
        /// <param name="z">the z euler angle in radians</param>
        /// <returns>quaternion</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion EulerZXY(number x, number y, number z)
        {
            math.sincos(number.half * new float3(x, y, z), out float3 s, out float3 c);
            return new quaternion(s.x * c.y * c.z + c.x * s.y * s.z,
                c.x * s.y * c.z - s.x * c.y * s.z,
                c.x * c.y * s.z - s.x * s.y * c.z,
                c.x * c.y * c.z + s.x * s.y * s.z);
        }

        /// <summary>
        /// construct quaternion from euler angles.(ZYX)
        /// </summary>
        /// <param name="x">the x euler angle in radians</param>
        /// <param name="y">the y euler angle in radians</param>
        /// <param name="z">the z euler angle in radians</param>
        /// <returns>quaternion</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion EulerZYX(number x, number y, number z)
        {
            math.sincos(number.half * new float3(x, y, z), out float3 s, out float3 c);
            return new quaternion(s.x * c.y * c.z + c.x * s.y * s.z,
                c.x * s.y * c.z - s.x * c.y * s.z,
                c.x * c.y * s.z + s.x * s.y * c.z,
                c.x * c.y * c.z - s.x * s.y * s.z);
        }

        /// <summary>
        /// construct quaternion from euler angles.
        /// </summary>
        /// <param name="xyz">euler angles in radians</param>
        /// <param name="order">rotate order</param>
        /// <returns>quaternion</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion Euler(float3 xyz, RotationOrder order = RotationOrder.ZXY)
        {
            switch(order)
            {
                case RotationOrder.XYZ : return quaternion.EulerXYZ(xyz);
                case RotationOrder.XZY : return quaternion.EulerXZY(xyz);
                case RotationOrder.YXZ : return quaternion.EulerYXZ(xyz);
                case RotationOrder.YZX : return quaternion.EulerYZX(xyz);
                case RotationOrder.ZXY : return quaternion.EulerZXY(xyz);
                case RotationOrder.ZYX : return quaternion.EulerZYX(xyz);
                default : return quaternion.EulerZXY(xyz);
            }
        }

        /// <summary>
        /// construct quaternion from euler angles.
        /// </summary>
        /// <param name="x">the x euler angle in radians</param>
        /// <param name="y">the y euler angle in radians</param>
        /// <param name="z">the z euler angle in radians</param>
        /// <param name="order">rotate order</param>
        /// <returns>quaternion</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion Euler(number x, number y, number z, RotationOrder order = RotationOrder.ZXY)
        {
            return Euler(new float3(x, y, z), order);
        }

#endregion

#region Rotate
        /// <summary>
        /// construct quaternion by rotating special angle in radians around the X Axis
        /// </summary>
        /// <param name="angle">the special angle in radians</param>
        /// <returns>quaternion</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion RotateX(number angle)
        {
            math.sincos(number.half * angle, out var sina, out var cosa);
            return new quaternion(sina, number.zero, number.zero, cosa);
        }

        /// <summary>
        /// construct quaternion by rotating special angle in radians around the Y Axis
        /// </summary>
        /// <param name="angle">the special angle in radians</param>
        /// <returns>quaternion</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion RotateY(number angle)
        {
            math.sincos(number.half * angle, out var sina, out var cosa);
            return new quaternion(number.zero, sina, number.zero, cosa);
        }

        /// <summary>
        /// construct quaternion by rotating special angle in radians around the Z Axis
        /// </summary>
        /// <param name="angle">the special angle in radians</param>
        /// <returns>quaternion</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion RotateZ(number angle){
            math.sincos(number.half * angle, out var sina, out var cosa);
            return new quaternion(number.zero, number.zero, sina, cosa);
        }
#endregion

        /// <summary>
        /// construct quaternion from euler angles.(ZXY)
        /// </summary>
        /// <param name="euler">radians of euler angles. z as roll, x as pitch, y as yaw</param>
        /// <returns>quaternion</returns>
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // [Obsolete("Use quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees.")]
        // public static quaternion EulerAngles(float3 euler)
        // {
        //     math.sincos(number.half * euler, out float3 s, out float3 c);
        //     return new quaternion(
        //         s.x * c.y * c.z + c.x * s.y * s.z,
        //         c.x * s.y * c.z - s.x * c.y * s.z,
        //         c.x * c.y * s.z - s.x * s.y * c.z,
        //         c.x * c.y * c.z + s.x * s.y * s.z);
        // }

        /// <summary>
        /// construct quaternion from euler angles.(ZXY)
        /// </summary>
        /// <param name="x">radian of pitch</param>
        /// <param name="y">radian of yaw</param>
        /// <param name="z">radian of roll</param>
        /// <returns>quaternion</returns>
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // [Obsolete("Use quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees.")]
        // public static quaternion EulerAngles(number x, number y, number z)
        // {
        //     math.sincos(number.half * new float3(x, y, z), out float3 s, out float3 c);
        //     return new quaternion(
        //         s.x * c.y * c.z + c.x * s.y * s.z,
        //         c.x * s.y * c.z - s.x * c.y * s.z,
        //         c.x * c.y * s.z - s.x * s.y * c.z,
        //         c.x * c.y * c.z + s.x * s.y * s.z);
        // }

        /// <summary>
        /// construct quaternion from euler angles, same as EulerAngles.(ZXY)
        /// </summary>
        /// <param name="x">radian of pitch</param>
        /// <param name="y">radian of yaw</param>
        /// <param name="z">radian of roll</param>
        /// <returns>quaternion</returns>
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // [Obsolete("Use quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees.")]
        // public static quaternion EulerRotation(number x, number y, number z)
        // {
        //     math.sincos(number.half * new float3(x, y, z), out float3 s, out float3 c);
        //     return new quaternion(
        //         s.x * c.y * c.z + c.x * s.y * s.z,
        //         c.x * s.y * c.z - s.x * c.y * s.z,
        //         c.x * c.y * s.z - s.x * s.y * c.z,
        //         c.x * c.y * c.z + s.x * s.y * s.z);
        // }

        /// <summary>
        /// construct quaternion from euler angles, same as EulerAngles.(ZXY)
        /// </summary>
        /// <param name="euler">the radians of euler angles. z as roll, x as pitch, y as yaw</param>
        /// <returns>quaternion</returns>
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // [Obsolete("Use quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees.")]
        // public static quaternion EulerRotation(float3 euler)
        // {
        //     math.sincos(number.half * euler, out float3 s, out float3 c);
        //     return new quaternion(
        //         s.x * c.y * c.z + c.x * s.y * s.z,
        //         c.x * s.y * c.z - s.x * c.y * s.z,
        //         c.x * c.y * s.z - s.x * s.y * c.z,
        //         c.x * c.y * c.z + s.x * s.y * s.z);
        // }

        /// <summary>
        /// construct quaternion by rotating one vector to another vector 
        /// </summary>
        /// <param name="fromDirection">from vector</param>
        /// <param name="toDirection">to vector</param>
        /// <returns>quaternion</returns>
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // public static quaternion FromToRotation(float3 fromDirection, float3 toDirection)
        // {
        //     number angle = math.angle(fromDirection, toDirection);
        //     float3 axis = math.normalize(math.cross(fromDirection, toDirection));
        //     return AxisAngle(axis, angle);
        // }

        /// <summary>
        /// get conjugated quaternion
        /// </summary>
        /// <param name="rotation">the original quaternion</param>
        /// <returns>quaternion</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion Inverse(quaternion rotation) => new quaternion(-rotation.x, -rotation.y, -rotation.z, rotation.w);

        /// <summary>
        /// Linear interpolation of two quaternions in interval [0, 1]
        /// </summary>
        /// <param name="a">quaternion a</param>
        /// <param name="b">quaternion b</param>
        /// <param name="t">coefficient. 0 if t < 0, 1 if t > 1</param>
        /// <returns>quaternion</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion Lerp(quaternion a, quaternion b, number t)
        {
            t = math.clamp(t, number.zero, number.one);
            bool opposite = Dot(a, b).RawValue < 0L;
            number inverse = number.one - t;
            return new quaternion(
                opposite ? (inverse * a.x - t * b.x) : (inverse * a.x + t * b.x),
                opposite ? (inverse * a.y - t * b.y) : (inverse * a.y + t * b.y),
                opposite ? (inverse * a.z - t * b.z) : (inverse * a.z + t * b.z),
                opposite ? (inverse * a.w - t * b.w) : (inverse * a.w + t * b.w)).normalized;
        }

        /// <summary>
        /// Linear interpolation of two quaternions
        /// </summary>
        /// <param name="q1">quaternion a</param>
        /// <param name="q2">quaternion b</param>
        /// <param name="t">coefficient</param>
        /// <returns>quaternion</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion LerpUnclamped(quaternion q1, quaternion q2, number t)
        {
            bool opposite = Dot(q1, q2).RawValue < 0L;
            if(opposite)
            {
                q2 = new quaternion(-q2.x, -q2.y, -q2.z, -q2.w);
            }

            number inverse = number.one - t;
            return new quaternion(
                inverse * q1.x + t * q2.x,
                inverse * q1.y + t * q2.y,
                inverse * q1.z + t * q2.z,
                inverse * q1.w + t * q2.w).normalized;
        }

        /// <summary>
        /// construct quaternion from forward vector. float3.up as default upwards vector.
        /// </summary>
        /// <param name="forward">forward vector</param>
        /// <returns>quaternion</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion LookRotation(float3 forward) => quaternion.LookRotation(forward, float3.up);

        /// <summary>
        /// construct quaternion from forward vector and upwards vector
        /// </summary>
        /// <param name="forward">forward vector</param>
        /// <param name="upwards">upwards vector</param>
        /// <returns>quaternion</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion LookRotation(float3 forward, float3 upwards)
        {
            float3 t = math.normalize(math.cross(upwards, forward));
            return new quaternion(math.float3x3(t, math.cross(forward, t), forward));
        }

        /// <summary>
        /// construct quaternion from forward vector and upwards vector in safe mode
        /// </summary>
        /// <param name="forward">forward vector</param>
        /// <param name="upwards">upwards vector</param>
        /// <returns>quaternion</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion LookRotationSafe(float3 forward, float3 upwards)
        {
            number forwardLengthSq = math.dot(forward, forward);
            number upLengthSq = math.dot(upwards, upwards);
            forward *= math.rsqrt(forwardLengthSq);
            upwards *= math.rsqrt(upLengthSq);
            float3 t = math.cross(upwards, forward);
            number tLengthSq = math.dot(t, t);
            t *= math.rsqrt(tLengthSq);
            number mn = math.min(math.min(forwardLengthSq, upLengthSq), tLengthSq);
            number mx = math.max(math.max(forwardLengthSq, upLengthSq), tLengthSq);
            bool accept = mn > number.MinValue && mx < number.MaxValue && math.isfinite(forwardLengthSq) && math.isfinite(upLengthSq) && math.isfinite(tLengthSq);
            return accept ? new quaternion(math.float3x3(t, upwards, forward)) : quaternion.identity;
        }

        /// <summary>
        /// normalize the quaternion
        /// </summary>
        /// <param name="q">original quaternion</param>
        /// <returns>the normalized quaternion</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion normalize(quaternion q)
        {
            number mag = number.Sqrt(quaternion.Dot(q, q));
            if (mag.RawValue == 0L) return quaternion.identity;
            return new quaternion(q.x / mag, q.y / mag, q.z / mag, q.w / mag);
        }

        /// <summary>
        /// construct quaternion by spherical interpolation of two quaternions and the giving radian of angle. But the interpolation quaternion does not exceed the to quaternion.
        /// </summary>
        /// <param name="from">from quaternion</param>
        /// <param name="to">to quaternion</param>
        /// <param name="maxDegreesDelta">the giving radian of angle between the from quaternion and the interpolation quaternion</param>
        /// <returns>quaternion</returns>
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // public static quaternion RotateTowards(quaternion from, quaternion to, number maxDegreesDelta)
        // {
        //     number angle = quaternion.Angle(from, to);
        //     if (angle.RawValue == 0L) return to;
        //     return quaternion.SlerpUnclamped(from, to, number.Min(number.one, maxDegreesDelta / angle));
        // }

        /// <summary>
        /// spherical interpolation of two quaternions in interval [0, 1]
        /// </summary>
        /// <param name="a">quaternion a</param>
        /// <param name="b">quaternion b</param>
        /// <param name="t">coefficient. 0 if t < 0, 1 if t > 0</param>
        /// <returns>quaternion</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion Slerp(quaternion a, quaternion b, number t)
        {
            //�˴���Ϊa��b��Ϊ��λ����Ԫ�أ����ټ���a��b�Ƿ�Ϊ��λ����Ԫ�أ��ɵ��������м���
            t = math.clamp(t, number.zero, number.one);
            number dot = Dot(a, b);
            if (number.Abs(dot).RawValue > number.ONE - number.SMALL_SQRT) return a;//a��b��Ȼ��෴����ʱ���ز�ֵ
            number rad = number.Acos(number.Abs(dot));
            number invSin = number.one / number.Sin(rad);
            number inverse = number.Sin((number.one - t) * rad) * invSin;
            number opposite = dot.RawValue < 0L ? -number.Sin(t * rad) * invSin : number.Sin(t * rad) * invSin;
            return new quaternion(
                (inverse * a.x) + (opposite * b.x),
                (inverse * a.y) + (opposite * b.y),
                (inverse * a.z) + (opposite * b.z),
                (inverse * a.w) + (opposite * b.w));
        }

        /// <summary>
        /// spherical interpolation of two quaternions
        /// </summary>
        /// <param name="q1">quaternion a</param>
        /// <param name="q2">quaternion b</param>
        /// <param name="t">coefficient</param>
        /// <returns>quaternion</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion SlerpUnclamped(quaternion q1, quaternion q2, number t)
        {
            number dt = Dot(q1, q2);
            if(dt < 0)
            {
                dt = -dt;
                q2 = new quaternion(-q2.x, -q2.y, -q2.z, -q2.w);
            }

            if(dt < number._0_9995)
            {
                var angle = math.acos(dt);
                var s = math.rsqrt(number.one - dt * dt);    // 1.0f / sin(angle)
                var w1 = math.sin(angle * (number.one - t)) * s;
                var w2 = math.sin(angle * t) * s;
                return new quaternion(
                    (w1 * q1.x) + (w2 * q2.x),
                    (w1 * q1.y) + (w2 * q2.y),
                    (w1 * q1.z) + (w2 * q2.z),
                    (w1 * q1.w) + (w2 * q2.w));
            }
            else
            {
                return LerpUnclamped(q1, q2, t);
            }

            // if (number.Abs(dt).RawValue > number.ONE - number.SMALL_SQRT) return a;//a��b��Ȼ��෴����ʱ���ز�ֵ
            // number rad = number.Acos(number.Abs(dt));
            // number invSin = number.one / number.Sin(rad);
            // number inverse = number.Sin((number.one - t) * rad) * invSin;
            // number opposite = dt.RawValue < 0L ? -number.Sin(t * rad) * invSin : number.Sin(t * rad) * invSin;
            // return new quaternion(
            //     (inverse * a.x) + (opposite * b.x),
            //     (inverse * a.y) + (opposite * b.y),
            //     (inverse * a.z) + (opposite * b.z),
            //     (inverse * a.w) + (opposite * b.w));
        }

        // /// <summary>
        // /// get euler angles in radians of the giving quaternion
        // /// </summary>
        // /// <param name="rotation">the giving quaternion</param>
        // /// <returns>euler angles in radians</returns>
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // [Obsolete("Use quaternion.eulerAngles instead. This function was deprecated because it uses radians instead of degrees.")]
        // public static float3 ToEulerAngles(quaternion rotation) => rotation.eulerAngles * number.Deg2Rad;

        /// <summary>
        /// is this quaternion equal to the other one.
        /// </summary>
        /// <param name="other">the other quaternion</param>
        /// <returns>true if equals, otherwise false</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(quaternion other) => x.RawValue == other.x.RawValue && y.RawValue == other.y.RawValue &&
            z.RawValue == other.z.RawValue && w.RawValue == other.w.RawValue;

        /// <summary>
        /// is this quaternion equal to the other object. 
        /// </summary>
        /// <param name="other">the other object</param>
        /// <returns>true if object is quaternion and equals to this, otherwise false</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object other) => other is quaternion qOther && this.Equals(qOther);

        /// <summary>
        /// get the hash code of this quaternion
        /// </summary>
        /// <returns>hash code</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => ((x.RawValue * 0x6E050B01u) + (y.RawValue * 0x750FDBF5u) +
            (z.RawValue * 0x7F3DD499u) + (w.RawValue * 0x52EAAEBBu)).GetHashCode();
            
        /// <summary>
        /// get the normalized quaternion of this one
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Normalize() => this = quaternion.normalize(this);

        // /// <summary>
        // /// set x/y/z/w values
        // /// </summary>
        // /// <param name="newX">new value of X</param>
        // /// <param name="newY">new value of Y</param>
        // /// <param name="newZ">new value of Z</param>
        // /// <param name="newW">new value of W</param>
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // public void Set(number newX, number newY, number newZ, number newW)
        // {
        //     x.RawValue = newX.RawValue;
        //     y.RawValue = newY.RawValue;
        //     z.RawValue = newZ.RawValue;
        //     w.RawValue = newW.RawValue;
        // }

        // /// <summary>
        // /// construct quaternion from axis and angle to this
        // /// </summary>
        // /// <param name="axis">the axis</param>
        // /// <param name="angle">the angle in radians</param>
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // [Obsolete("Use quaternion.AngleAxis instead. This function was deprecated because it uses radians instead of degrees.")]
        // public void SetAxisAngle(float3 axis, number angle) => this = quaternion.AxisAngle(axis, angle);

        /// <summary>
        /// construct quaternion from euler angles to this.(ZXY)
        /// </summary>
        /// <param name="euler">euler angles in radians. z as roll, x as pitch, y as yaw</param>
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // [Obsolete("Use quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees.")]
        // public void SetEulerAngles(float3 euler) => this = quaternion.EulerAngles(euler);

        /// <summary>
        /// construct quaternion from euler angles to this. (ZXY)
        /// </summary>
        /// <param name="x">pitch angle in radians</param>
        /// <param name="y">yaw angle in radians</param>
        /// <param name="z">roll angle in radians</param>
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // [Obsolete("Use quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees.")]
        // public void SetEulerAngles(number x, number y, number z) => this = quaternion.EulerAngles(x, y, z);

        // /// <summary>
        // /// construct quaternion by euler angles to this, same as SetEulerAngles. (ZXY)
        // /// </summary>
        // /// <param name="euler">euler angles in radians. z as roll, x as pitch, y as yaw</param>
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // [Obsolete("Use quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees.")]
        // public void SetEulerRotation(float3 euler) => this = quaternion.EulerRotation(euler);

        // /// <summary>
        // /// construct quaternion from euler angles to this, same as SetEulerAngles. (ZXY)
        // /// </summary>
        // /// <param name="x">pitch angle in radians</param>
        // /// <param name="y">yaw angle in radians</param>
        // /// <param name="z">roll angle in radians</param>
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // [Obsolete("Use quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees.")]
        // public void SetEulerRotation(number x, number y, number z) => this = quaternion.EulerRotation(x, y, z);

        /// <summary>
        /// construct quaternion by rotating one vector to another vector, and set to this.
        /// </summary>
        /// <param name="fromDirection">the from vector</param>
        /// <param name="toDirection">the to vector</param>
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // public void SetFromToRotation(float3 fromDirection, float3 toDirection) => this = quaternion.FromToRotation(fromDirection, toDirection);

        /// <summary>
        /// construct quaternion from forward vector and upwards vector, and set to this.  float3.up as default upwards vector.
        /// </summary>
        /// <param name="view">forward vector</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetLookRotation(float3 view) => this = quaternion.LookRotation(view);

        /// <summary>
        /// construct quaternion from forward vector and upwards vector, and set to this
        /// </summary>
        /// <param name="view">forward vector</param>
        /// <param name="up">upwards vector</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetLookRotation(float3 view, float3 up) => this = quaternion.LookRotation(view, up);

        /// <summary>
        /// get the axis and angle of this quaternion
        /// </summary>
        /// <param name="angle">the angle in degrees</param>
        /// <param name="axis">the axis</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ToAngleAxis(out number angle, out float3 axis)
        {
            axis = float3.right;
            angle = number.zero;
            number dot = x * x + y * y + z * z;
            if (number.Abs(dot).RawValue > number.SMALL_SQRT)
            {
                angle = number.Acos(math.clamp(w, -number.one, number.one));
                axis = new float3(x, y, z) / number.Sin(angle);
                angle *= (number.Rad2Deg * 2);//�Ƕ�
            }
        }

        /// <summary>
        /// get the axis and angle of this quaternion
        /// </summary>
        /// <param name="axis">the axis</param>
        /// <param name="angle">the angle in radians</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [Obsolete("Use quaternion.ToAngleAxis instead. This function was deprecated because it uses radians instead of degrees.")]
        public void ToAxisAngle(out float3 axis, out number angle)
        {
            axis = float3.right;
            angle = number.zero;
            number dot = x * x + y * y + z * z;
            if (number.Abs(dot).RawValue > number.SMALL_SQRT)
            {
                angle = number.Acos(math.clamp(w, -number.one, number.one));
                axis = new float3(x, y, z) / number.Sin(angle);
                angle *= 2;//����
            }
        }

        /// <summary>
        /// get a formatted string of the quaternion.
        /// </summary>
        /// <param name="format">A numeric format string.</param>
        /// <param name="formatProvider">An object that specifies culture-specific formatting.</param>
        /// <returns>the formatted string</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider formatProvider)
        {
            bool flag = string.IsNullOrEmpty(format);
            if (flag) format = "F2";
            return string.Format(formatProvider, "({0}, {1}, {2}, {3})", new object[]
            {
                this.x.ToString(format, formatProvider),
                this.y.ToString(format, formatProvider),
                this.z.ToString(format, formatProvider),
                this.w.ToString(format, formatProvider)
            });
        }

        /// <summary>
        /// get a formatted string of the quaternion.
        /// </summary>
        /// <param name="format">A numeric format string.</param>
        /// <returns>the formatted string</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format) => this.ToString(format, CultureInfo.InvariantCulture.NumberFormat);

        /// <summary>
        /// get a formatted string of the quaternion.
        /// </summary>
        /// <returns>the formatted string</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => this.ToString(null, CultureInfo.InvariantCulture.NumberFormat);

        /// <summary>
        /// operator * of rotating one point by the giving quaternion
        /// </summary>
        /// <param name="rotation">the giving quaternion</param>
        /// <param name="point">the original point</param>
        /// <returns>the point rotated</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 operator *(quaternion rotation, float3 point)
        {
            number x = rotation.x * 2;
            number y = rotation.y * 2;
            number z = rotation.z * 2;
            number xx = rotation.x * x;
            number yy = rotation.y * y;
            number zz = rotation.z * z;
            number xy = rotation.x * y;
            number xz = rotation.x * z;
            number yz = rotation.y * z;
            number wx = rotation.w * x;
            number wy = rotation.w * y;
            number wz = rotation.w * z;
            float3 res;
            res.x = (number.one - (yy + zz)) * point.x + (xy - wz) * point.y + (xz + wy) * point.z;
            res.y = (xy + wz) * point.x + (number.one - (xx + zz)) * point.y + (yz - wx) * point.z;
            res.z = (xz - wy) * point.x + (yz + wx) * point.y + (number.one - (xx + yy)) * point.z;
            return res;
        }

        /// <summary>
        /// opterator * of two quaternions
        /// </summary>
        /// <param name="lhs">left quaternion</param>
        /// <param name="rhs">right quaternion</param>
        /// <returns>the result quaternion</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion operator *(quaternion lhs, quaternion rhs)
            => new quaternion(lhs.w * rhs.x + lhs.x * rhs.w + lhs.y * rhs.z - lhs.z * rhs.y,
                lhs.w * rhs.y + lhs.y * rhs.w + lhs.z * rhs.x - lhs.x * rhs.z,
                lhs.w * rhs.z + lhs.z * rhs.w + lhs.x * rhs.y - lhs.y * rhs.x,
                lhs.w * rhs.w - lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z);

        /// <summary>
        /// is lhs quaternion equal to rhs quaternion. 
        /// </summary>
        /// <param name="lhs">lhs quaternion</param>
        /// <param name="rhs">rhs quaternion</param>
        /// <returns>true if equals, otherwise false</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(quaternion lhs, quaternion rhs) => quaternion.IsEqualUsingDot(quaternion.Dot(lhs, rhs));

        /// <summary>
        /// is lhs quaternion not equal to rhs quaternion. 
        /// </summary>
        /// <param name="lhs">lhs quaternion</param>
        /// <param name="rhs">rhs quaternion</param>
        /// <returns>true if not equals, otherwise false</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(quaternion lhs, quaternion rhs) => !(lhs == rhs);

        /// <summary>
        /// is two quaternions equals to each other by using dot.
        /// </summary>
        /// <param name="dot">the dot value</param>
        /// <returns>true if dot value equals to 1 within the accuracy, otherwise false</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsEqualUsingDot(number dot) => dot.RawValue >= number.ONE - number.SMALL_SQRT;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Unity.Mathematics.quaternion(quaternion v) { return new Unity.Mathematics.quaternion(v.x, v.y, v.z, v.w); }
    }

    public static partial class math
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Approximately(quaternion num2, Unity.Mathematics.quaternion b)
        {
            return Approximately(num2.x, b.value.x) 
                && Approximately(num2.y, b.value.y)
                && Approximately(num2.z, b.value.z)
                && Approximately(num2.w, b.value.w);
        }

        /// <summary>Returns the inverse of a quaternion value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion inverse(quaternion q)
        {
            return quaternion.Inverse(q);
        }

        /// <summary>Returns the dot product of two quaternions.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number dot(quaternion a, quaternion b)
        {
            return quaternion.Dot(a, b);
        }

        /// <summary>Returns a normalized version of a quaternion q by scaling it by 1 / length(q).</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion normalize(quaternion q)
        {
            return quaternion.normalize(q);
        }

        public static quaternion slerp(quaternion q1, quaternion q2, number t)
        {
            return quaternion.SlerpUnclamped(q1, q2, t);
        }

        /// <summary>Returns the result of a normalized linear interpolation between two quaternions q1 and a2 using an interpolation parameter t.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion nlerp(quaternion q1, quaternion q2, number t)
        {
            return quaternion.LerpUnclamped(q1, q2, t);
        }

        /// <summary>Returns the result of transforming the quaternion b by the quaternion a.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion mul(quaternion a, quaternion b)
        {
            return new quaternion(new float4(a.w, a.w, a.w, a.w) * new float4(b.x, b.y, b.z, b.w) 
                    + ( new float4(a.x, a.y, a.z, a.x) * new float4(b.w, b.w, b.w, b.x) + new float4(a.y, a.z, a.x, a.y) * new float4(b.z, b.x, b.y, b.y)) * float4(1, 1, 1, -1)
                    - new float4(a.z, a.x, a.y, a.z) * new float4(b.y, b.z, b.x, b.z));
        }

        /// <summary>Returns the result of transforming a vector by a quaternion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 mul(quaternion q, float3 v)
        {
            float3 t = 2 * math.cross(new float3(q.x, q.y, q.z), v);
            return v + q.w * t + cross(new float3(q.x, q.y, q.z), t);
        }

        /// <summary>Returns the result of rotating a vector by a unit quaternion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 rotate(quaternion q, float3 v)
        {
            float3 t = 2 * cross(new float3(q.x, q.y, q.z), v);
            return v + q.w * t + cross(new float3(q.x, q.y, q.z), t);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 forward(quaternion q) { return mul(q, float3(0, 0, 1)); }  // for compatibility
    }
}
