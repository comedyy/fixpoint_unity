//
// @brief: 定点数
// @version: 1.0.0
// @author nt
// @date: 2021.06.11
// 
// 
//
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
#if Debug || DEBUG
using System.Diagnostics;
#endif

namespace Nt.Deterministics
{

#if Debug || DEBUG
    // Internal representation is identical to IEEE binary32 floating point numbers
    [DebuggerDisplay("{ToStringInv()}")]
#endif
    [System.Serializable]
    [StructLayout(LayoutKind.Explicit)]
    public struct number : IEquatable<number>, IComparable<number>
    {
        [FieldOffset(0)]
        public long RawValue;
        #region 静态常量
        //public static readonly number MinNormal = new number(1L);
        //public static readonly decimal Precision = (decimal)MinNormal;
        //public static readonly number one = new number(ONE);
        //public static readonly number zero = new number();
        //public static readonly number PIDiv2 = new number(PiDiv2);
        //public static readonly number PI = new number(Pi);
        //public static readonly number PI3Div2 = new number(Pi3Div2);
        //public static readonly number PITimes2 = new number(PiTimes2);
        //public static readonly number PIOver180 = new number(72L);
        //public static readonly number Rad2Deg = (number)180 / PI;
        //public static readonly number Deg2Rad = PI / (number)180;
        //public static readonly number EXP = new number(lExp);
        //public static readonly number NaN = new number(long.MinValue);
        //public static readonly number PositiveInfinity = new number(long.MaxValue);
        //public static readonly number MaxValue = new number(long.MaxValue - 1L);
        //public static readonly number NegativeInfinity = new number(long.MinValue + 1L);
        //public static readonly number MinValue = new number(long.MinValue + 2L);
        //public static readonly number MaxInteger = new number(MaxValue.RawValue & IntegerMask);

        //以上写法在导出IL2CPP代码时会导出IL2CPP_RUNTIME_CLASS_INIT运行时代理，导致性能下降；因而改用以下写法
        public unsafe static number MinNormal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                long raw = 1L;
                return *(number*)(&raw);
            }
        }
        public unsafe static decimal Precision
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return (decimal)MinNormal;
            }
        }
        public unsafe static number one
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                long raw = ONE;
                return *(number*)(&raw);
            }
        }
        public unsafe static number half
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                long raw = HALF;
                return *(number*)(&raw);
            }
        }
        public unsafe static number zero
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                long raw = 0L;
                return *(number*)(&raw);
            }
        }
        public unsafe static number PIDiv2
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                long raw = PiDiv2;
                return *(number*)(&raw);
            }
        }
        public unsafe static number PI
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                long raw = Pi;
                return *(number*)(&raw);
            }
        }
        public unsafe static number PI3Div2
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                long raw = Pi3Div2;
                return *(number*)(&raw);
            }
        }
        public unsafe static number PITimes2
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                long raw = PiTimes2;
                return *(number*)(&raw);
            }
        }
        public unsafe static number PIOver180
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                long raw = 72L;
                return *(number*)(&raw);
            }
        }
        public unsafe static number Rad2Deg
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                long raw = nRad2Deg;
                return *(number*)(&raw);
            }
        }
        public unsafe static number Deg2Rad
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                long raw = nDeg2Rad;
                return *(number*)(&raw);
            }
        }
        public unsafe static number EXP
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                long raw = lExp;
                return *(number*)(&raw);
            }
        }
        public unsafe static number NaN
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                long raw = long.MinValue;
                return *(number*)(&raw);
            }
        }
        public unsafe static number PositiveInfinity
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                long raw = long.MaxValue;
                return *(number*)(&raw);
            }
        }
        public unsafe static number MaxValue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                long raw = long.MaxValue - 1L;
                return *(number*)(&raw);
            }
        }
        public unsafe static number NegativeInfinity
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                long raw = long.MinValue + 1L;
                return *(number*)(&raw);
            }
        }
        public unsafe static number MinValue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                long raw = long.MinValue + 2L;
                return *(number*)(&raw);
            }
        }
        public unsafe static number MaxInteger
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                long raw = nMaxInteger;
                return *(number*)(&raw);
            }
        }
        public unsafe static number Small
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                long small = SMALL;
                return *(number*)(&small);
            }
        }
        public unsafe static number SmallSqr
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                long small_sqrt = SMALL_SQRT;
                return *(number*)(&small_sqrt);
            }
        }
        #endregion
        public const long PiDiv2 = (long)(Math.PI * ONE / 2 + 0.5);
        public const long Pi = (long)(Math.PI * ONE + 0.5);
        public const long Pi3Div2 = (long)(3 * Math.PI * ONE / 2 + 0.5);
        public const long PiTimes2 = (long)(2 * Math.PI * ONE + 0.5);
        public const long nRad2Deg = (long)(ONE * 180.0 / Math.PI + 0.5);
        public const long nDeg2Rad = (long)(ONE * Math.PI / 180.0 + 0.5);
        const long lExp = (long)(Math.E * ONE + 0.5);
        public const int FRACTIONAL_PLACES = 16;
        public const int FRACTIONAL_PLACES_SQRT = 8;
        public const long WaterShedSqrt = 4L << FRACTIONAL_PLACES;
        public const int INTEGER_PLACES = sizeof(long) - FRACTIONAL_PLACES;
        public const long ONE = 1L << FRACTIONAL_PLACES;
        public const long nMaxInteger = (long.MaxValue - 1L) & IntegerMask;
        public const long SMALL = ONE >> 6;
        public const long SMALL_SQRT = ONE >> 12;
        const long HALF = ONE >> 1;
        const long FracMask = ONE - 1L;//小数部分
        const long IntegerMask = ~FracMask;//整数部分
        const long HighPlaceMask = long.MinValue >> (FRACTIONAL_PLACES - 1);
        const long LowPlaceMask = ~HighPlaceMask;
        const long WaterShed = 1L << (sizeof(long) - FRACTIONAL_PLACES - 2);//用于判断左移是否越界

        internal number(long rawValue)
        {
            RawValue = rawValue;
        }
        public number(int value)
        {
            RawValue = (long)value << FRACTIONAL_PLACES;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FromRaw(long rawValue) => new number(rawValue);

        #region operator +、-、*、/
#if NT_STRICT_NUMBER
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator +(number x, number y) => StrictAdd(x, y);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator -(number x, number y) => StrictSubstract(x, y);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator *(number x, number y) => StrictMutiply(x, y);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator /(number x, number y) => StrictDivide(x, y);
#elif NT_NUMBER_BY_FLOAT
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator +(number x, number y) => FloatAdd(x, y);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator -(number x, number y) => FloatSubstract(x, y);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator *(number x, number y) => FloatMutiply(x, y);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator /(number x, number y) => FloatDivide(x, y);
#else
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator +(number x, number y)
        {
            x.RawValue += y.RawValue;
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator -(number x, number y)
        {
            x.RawValue -= y.RawValue;
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator *(number x, number y)
        {
            x.RawValue = (x.RawValue * y.RawValue) >> FRACTIONAL_PLACES;
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator /(number x, number y)
        {
            x.RawValue = (x.RawValue << FRACTIONAL_PLACES) / y.RawValue;
            return x;
        }
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FastAdd(number x, number y)
        {
            x.RawValue += y.RawValue;
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number StrictAdd(number x, number y)
        {
            if (x.RawValue == NaN.RawValue || y.RawValue == NaN.RawValue) return NaN;
            bool bx = x.RawValue == PositiveInfinity.RawValue || x.RawValue == NegativeInfinity.RawValue;
            bool by = y.RawValue == PositiveInfinity.RawValue || y.RawValue == NegativeInfinity.RawValue;
            if (bx && by && x.RawValue > 0 && y.RawValue > 0) return PositiveInfinity;
            if (bx && by && x.RawValue < 0 && y.RawValue < 0) return NegativeInfinity;
            if (bx && by) return NaN;
            if (bx && !by) return x;
            if (!bx && by) return y;
            if (x.RawValue > 0 && y.RawValue > 0 && x.RawValue > MaxValue.RawValue - y.RawValue) return PositiveInfinity;//处理向上越界的情况
            if (x.RawValue < 0 && y.RawValue < 0 && x.RawValue < MinValue.RawValue - y.RawValue) return NegativeInfinity;//处理向下越界的情况
            x.RawValue += y.RawValue;
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FloatAdd(number x, number y)
        {
            x.RawValue += y.RawValue;
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator +(number x, int y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return x + (number)y;
#else
            x.RawValue += (long)y << FRACTIONAL_PLACES;
            return x;
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator +(int x, number y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return (number)x + y;
#else
            y.RawValue += (long)x << FRACTIONAL_PLACES;
            return y;
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator +(number x, float y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return x + (number)y;
#else
            x.RawValue += (long)(y * ONE);
            return x;
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator +(float x, number y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return (number)x + y;
#else
            y.RawValue += (long)(x * ONE);
            return y;
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator +(number x, double y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return x + (number)y;
#else
            x.RawValue += (long)(y * ONE);
            return x;
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator +(double x, number y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return (number)x + y;
#else
            y.RawValue += (long)(x * ONE);
            return y;
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FastSubstract(number x, number y)
        {
            x.RawValue -= y.RawValue;
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number StrictSubstract(number x, number y)
        {
            if (x.RawValue == NaN.RawValue || y.RawValue == NaN.RawValue) return NaN;
            bool bx = x.RawValue == PositiveInfinity.RawValue || x.RawValue == NegativeInfinity.RawValue;
            bool by = y.RawValue == PositiveInfinity.RawValue || y.RawValue == NegativeInfinity.RawValue;
            if (bx && by && x.RawValue > 0 && y.RawValue < 0) return PositiveInfinity;
            if (bx && by && x.RawValue < 0 && y.RawValue > 0) return NegativeInfinity;
            if (bx && by) return NaN;
            if (bx && !by) return x;
            if (!bx && by) return -y;
            if (x.RawValue > 0 && y.RawValue < 0 && x.RawValue > MaxValue.RawValue + y.RawValue) return PositiveInfinity;//处理向上越界的情况
            if (x.RawValue < 0 && y.RawValue > 0 && x.RawValue < MinValue.RawValue + y.RawValue) return NegativeInfinity;//处理向下越界的情况
            x.RawValue -= y.RawValue;
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FloatSubstract(number x, number y)
        {
            x.RawValue -= y.RawValue;
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator -(number x, int y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return x - (number)y;
#else
            x.RawValue -= (long)y << FRACTIONAL_PLACES;
            return x;
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator -(int x, number y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return (number)x - y;
#else
            y.RawValue = ((long)x << FRACTIONAL_PLACES) - y.RawValue;
            return y;
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator -(number x, float y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return x - (number)y;
#else
            x.RawValue -= (long)(y * ONE);
            return x;
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator -(float x, number y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return (number)x - y;
#else
            y.RawValue = (long)(x * ONE) - y.RawValue;
            return y;
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator -(number x, double y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return x - (number)y;
#else
            x.RawValue -= (long)(y * ONE);
            return x;
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator -(double x, number y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return (number)x - y;
#else
            y.RawValue = (long)(x * ONE) - y.RawValue;
            return y;
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long StrictRawMutiply(long x, long y)
        {
            var lx = x >> FRACTIONAL_PLACES;
            var fx = x & FracMask;
            var ly = y >> FRACTIONAL_PLACES;
            var fy = y & FracMask;
            long v1 = (lx * ly) << FRACTIONAL_PLACES;
            long v2 = lx * fy;
            long v3 = fx * ly;
            long v4 = (fx * fy) >> FRACTIONAL_PLACES;
            return v1 + v2 + v3 + v4;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FastMutiply(number x, number y)
        {
            x.RawValue = (x.RawValue * y.RawValue) >> FRACTIONAL_PLACES;
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number StrictMutiply(number x, number y)
        {
            if (x.RawValue == NaN.RawValue || y.RawValue == NaN.RawValue) return NaN;
            bool bx = x.RawValue == PositiveInfinity.RawValue || x.RawValue == NegativeInfinity.RawValue;
            bool by = y.RawValue == PositiveInfinity.RawValue || y.RawValue == NegativeInfinity.RawValue;
            int signX = x.RawValue == 0 ? 0 : x.RawValue < 0 ? -1 : 1;
            int signY = y.RawValue == 0 ? 0 : y.RawValue < 0 ? -1 : 1;
            int sign = signX * signY;
            if (bx && by) return sign > 0 ? PositiveInfinity : NegativeInfinity;
            if (bx && !by || !bx && by) return sign == 0 ? NaN : sign > 0 ? PositiveInfinity : NegativeInfinity;
            var absX = x.RawValue < 0 ? -x.RawValue : x.RawValue;
            var absY = y.RawValue < 0 ? -y.RawValue : y.RawValue;
            if (absX > ONE && absY > ONE && absX > StrictRawDivide(MaxValue.RawValue, absY))//越界情况
                return sign > 0 ? PositiveInfinity : NegativeInfinity;
            x.RawValue = StrictRawMutiply(x.RawValue, y.RawValue);
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FloatMutiply(number x, number y)
        {
            x.RawValue = (long)((float)x.RawValue * y.RawValue / ONE);
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator *(number x, int y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return x * (number)y;
#else
            x.RawValue *= y;
            return x;
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator *(int x, number y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return (number)x * y;
#else
            y.RawValue *= x;
            return y;
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator *(number x, float y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return x * (number)y;
#else
            x.RawValue = (long)(x.RawValue * y);
            return x;
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator *(float x, number y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return (number)x * y;
#else
            y.RawValue = (long)(x * y.RawValue);
            return y;
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator *(number x, double y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return x * (number)y;
#else
            x.RawValue = (long)(x.RawValue * y);
            return x;
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator *(double x, number y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return (number)x * y;
#else
            y.RawValue = (long)(x * y.RawValue);
            return y;
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long StrictRawDivide(long x, long y)
        {
            int signX = x == 0 ? 0 : x < 0 ? -1 : 1;
            int signY = y == 0 ? 0 : y < 0 ? -1 : 1;
            int sign = signX * signY;
            var absX = x < 0 ? -x : x;
            var absY = y < 0 ? -y : y;
            long ulRet = (absX / absY) << FRACTIONAL_PLACES;
            long ulMod = absX % absY;
            if (absY < WaterShed)
                ulRet += (ulMod << FRACTIONAL_PLACES) / absY;
            else
                ulRet += ulMod / (absY >> FRACTIONAL_PLACES);
            return sign * ulRet;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FastDivide(number x, number y)
        {
            x.RawValue = (x.RawValue << FRACTIONAL_PLACES) / y.RawValue;
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number StrictDivide(number x, number y)
        {
            if (x.RawValue == NaN.RawValue || y.RawValue == NaN.RawValue) return NaN;
            if (x.RawValue == 0 && y.RawValue == 0) return NaN;
            bool bx = x.RawValue == PositiveInfinity.RawValue || x.RawValue == NegativeInfinity.RawValue;
            bool by = y.RawValue == PositiveInfinity.RawValue || y.RawValue == NegativeInfinity.RawValue;
            if (bx && by) return NaN;
            int signX = x.RawValue == 0 ? 0 : x.RawValue < 0 ? -1 : 1;
            int signY = y.RawValue == 0 ? 0 : y.RawValue < 0 ? -1 : 1;
            int sign = signX * signY;
            if (bx && !by) return sign == 0 ? x : sign > 0 ? PositiveInfinity : NegativeInfinity;
            if (!bx && by) return zero;
            if (y.RawValue == 0) return x > 0 ? PositiveInfinity : NegativeInfinity;
            var absX = x.RawValue < 0 ? -x.RawValue : x.RawValue;
            var absY = y.RawValue < 0 ? -y.RawValue : y.RawValue;
            if (absY < ONE && absX > StrictRawMutiply(MaxValue.RawValue, absY))//越界情况
                return sign > 0 ? PositiveInfinity : NegativeInfinity;
            x.RawValue = StrictRawDivide(x.RawValue, y.RawValue);
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FloatDivide(number x, number y)
        {
            x.RawValue = (long)((float)x.RawValue / y.RawValue * ONE);
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator /(number x, int y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return x / (number)y;
#else
            x.RawValue /= y;
            return x;
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator /(int x, number y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return (number)x / y;
#else
            long rawValue = ((long)x << FRACTIONAL_PLACES) << FRACTIONAL_PLACES;
            y.RawValue = rawValue / y.RawValue;
            return y;
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator /(number x, float y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return x / (number)y;
#else
            x.RawValue = (x.RawValue << FRACTIONAL_PLACES) / (long)(y * ONE);
            return x;
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator /(float x, number y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return (number)x / y;
#else
            long rawValue = (long)(x * ONE);
            y.RawValue = (rawValue << FRACTIONAL_PLACES) / y.RawValue;
            return y;
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator /(number x, double y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return x / (number)y;
#else
            x.RawValue = (x.RawValue << FRACTIONAL_PLACES) / (long)(y * ONE);
            return x;
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator /(double x, number y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return (number)x / y;
#else
            long rawValue = (long)(x * ONE);
            y.RawValue = (rawValue << FRACTIONAL_PLACES) / y.RawValue;
            return y;
#endif
        }
        #endregion

        #region operator ++、--
#if NT_STRICT_NUMBER
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator ++(number x) => StrictSelfInc(x);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator --(number x) => StrictSelfSub(x);
#elif NT_NUMBER_BY_FLOAT
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator ++(number x) => FloatSelfInc(x);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator --(number x) => FloatSelfSub(x);
#else
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator ++(number x)
        {
            x.RawValue += ONE;
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator --(number x)
        {
            x.RawValue -= ONE;
            return x;
        }
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FastSelfInc(number x)
        {
            x.RawValue += ONE;
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number StrictSelfInc(number x)
        {
            if (x.RawValue == NaN.RawValue) return NaN;
            if (x.RawValue == PositiveInfinity.RawValue || x.RawValue == NegativeInfinity.RawValue) return x;
            if (x.RawValue == MaxValue.RawValue || x.RawValue == MinValue.RawValue) return x;
            x.RawValue += ONE;
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FloatSelfInc(number x)
        {
            x.RawValue += ONE;
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FastSelfSub(number x)
        {
            x.RawValue -= ONE;
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number StrictSelfSub(number x)
        {
            if (x.RawValue == NaN.RawValue) return NaN;
            if (x.RawValue == PositiveInfinity.RawValue || x.RawValue == NegativeInfinity.RawValue) return x;
            if (x.RawValue == MaxValue.RawValue || x.RawValue == MinValue.RawValue) return x;
            x.RawValue -= ONE;
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FloatSelfSub(number x)
        {
            x.RawValue -= ONE;
            return x;
        }
        #endregion

        #region operator &、|、^、~
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator &(number x, number y)
        {
            x.RawValue &= y.RawValue;
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator &(number x, long y)
        {
            x.RawValue &= y;
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator &(long x, number y)
        {
            y.RawValue &= x;
            return y;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator |(number x, number y)
        {
            x.RawValue |= y.RawValue;
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator |(number x, long y)
        {
            x.RawValue |= y;
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator |(long x, number y)
        {
            y.RawValue |= x;
            return y;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator ^(number x, number y)
        {
            x.RawValue ^= y.RawValue;
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator ^(number x, long y)
        {
            x.RawValue ^= y;
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator ^(long x, number y)
        {
            y.RawValue ^= x;
            return y;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator ~(number x)
        {
            x.RawValue = ~x.RawValue;
            return x;
        }
        #endregion

        #region operator 求余、取正、取反
#if NT_STRICT_NUMBER
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator %(number x, number y) => StrictMod(x, y);
#elif NT_NUMBER_BY_FLOAT
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator %(number x, number y) => FloatMod(x, y);
#else
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator %(number x, number y)
        {
            x.RawValue %= y.RawValue;
            return x;
        }
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator %(number x, int y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return x % (number)y;
#else
            x.RawValue %= (long)y << FRACTIONAL_PLACES;
            return x;
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator %(int x, number y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return (number)x % y;
#else
            y.RawValue = ((long)x << FRACTIONAL_PLACES) % y.RawValue;
            return y;
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator %(number x, float y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return x % (number)y;
#else
            x.RawValue %= (long)(y * ONE);
            return x;
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator %(float x, number y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return (number)x % y;
#else
            y.RawValue = (long)(x * ONE) % y.RawValue;
            return y;
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator %(number x, double y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return x % (number)y;
#else
            x.RawValue %= (long)(y * ONE);
            return x;
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator %(double x, number y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return (number)x % y;
#else
            y.RawValue = (long)(x * ONE) % y.RawValue;
            return y;
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FastMod(number x, number y)
        {
            x.RawValue %= y.RawValue;
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number StrictMod(number x, number y)
        {
            if (x.RawValue == NaN.RawValue || y.RawValue == NaN.RawValue) return NaN;
            if (x.RawValue == PositiveInfinity.RawValue || x.RawValue == NegativeInfinity.RawValue) return NaN;
            if (y.RawValue == PositiveInfinity.RawValue || y.RawValue == NegativeInfinity.RawValue) return x;
            x.RawValue %= y.RawValue;
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FloatMod(number x, number y)
        {
            x.RawValue %= y.RawValue;
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator +(number x) => x;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator -(number x)
        {
            x.RawValue = -x.RawValue;
            return x;
        }
        #endregion

        #region operator ==、!=、>、<、>=、<=
#if NT_STRICT_NUMBER
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(number x, number y) => StrictEqualTo(x, y);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(number x, number y) => StrictNEqualTo(x, y);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >(number x, number y) => StrictGreatThan(x, y);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=(number x, number y) => StrictGreatEqualThan(x, y);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=(number x, number y) => StrictLessEqualThan(x, y);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <(number x, number y) => StrictLessThan(x, y);
#else
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(number x, number y) => x.RawValue == y.RawValue;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(number x, number y) => x.RawValue != y.RawValue;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >(number x, number y) => x.RawValue > y.RawValue;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=(number x, number y) => x.RawValue >= y.RawValue;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=(number x, number y) => x.RawValue <= y.RawValue;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <(number x, number y) => x.RawValue < y.RawValue;
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool FastEqualTo(number x, number y) => x.RawValue == y.RawValue;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool StrictEqualTo(number x, number y)
        {
            if (x.RawValue == NaN.RawValue || y.RawValue == NaN.RawValue) return false;
            return x.RawValue == y.RawValue;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(number x, int y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return x == (number)y;
#else
            return x.RawValue == ((long)y << FRACTIONAL_PLACES);
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(int x, number y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return y == x;
#else
            return y.RawValue == ((long)x << FRACTIONAL_PLACES);
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(number x, float y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return x == (number)y;
#else
            return x.RawValue == (long)(y * ONE);
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(float x, number y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return y == x;
#else
            return y.RawValue == (long)(x * ONE);
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(number x, double y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return x == (number)y;
#else
            return x.RawValue == (long)(y * ONE);
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(double x, number y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return y == x;
#else
            return y.RawValue == (long)(x * ONE);
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(number x, long y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return x == (number)y;
#else
            return x.RawValue == (y << FRACTIONAL_PLACES);
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(long x, number y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return y == x;
#else
            return y.RawValue == (x << FRACTIONAL_PLACES);
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool FastNEqualTo(number x, number y) => x.RawValue != y.RawValue;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool StrictNEqualTo(number x, number y)
        {
            if (x.RawValue == NaN.RawValue || y.RawValue == NaN.RawValue) return true;
            return x.RawValue != y.RawValue;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(number x, int y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return x != (number)y;
#else
            return x.RawValue != ((long)y << FRACTIONAL_PLACES);
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(int x, number y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return y != x;
#else
            return y.RawValue != ((long)x << FRACTIONAL_PLACES);
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(number x, float y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return x != (number)y;
#else
            return x.RawValue != (long)(y * ONE);
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(float x, number y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return y != x;
#else
            return y.RawValue != (long)(x * ONE);
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(number x, double y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return x != (number)y;
#else
            return x.RawValue != (long)(y * ONE);
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(double x, number y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return y != x;
#else
            return y.RawValue != (long)(x * ONE);
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(number x, long y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return x != (number)y;
#else
            return x.RawValue != (y << FRACTIONAL_PLACES);
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(long x, number y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return y != x;
#else
            return y.RawValue != (x << FRACTIONAL_PLACES);
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool FastGreatThan(number x, number y) => x.RawValue > y.RawValue;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool StrictGreatThan(number x, number y)
        {
            if (x.RawValue == NaN.RawValue || y.RawValue == NaN.RawValue) return false;
            return x.RawValue > y.RawValue;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >(number x, int y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return x > (number)y;
#else
            return x.RawValue > ((long)y << FRACTIONAL_PLACES);
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >(int x, number y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return y < x;
#else
            return y.RawValue < ((long)x << FRACTIONAL_PLACES);
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >(number x, float y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return x > (number)y;
#else
            return x.RawValue > (long)(y * ONE);
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >(float x, number y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return y < x;
#else
            return y.RawValue < (long)(x * ONE);
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >(number x, double y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return x > (number)y;
#else
            return x.RawValue > (long)(y * ONE);
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >(double x, number y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return y < x;
#else
            return y.RawValue < (long)(x * ONE);
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >(number x, long y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return x > (number)y;
#else
            return x.RawValue > (y << FRACTIONAL_PLACES);
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >(long x, number y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return y < x;
#else
            return y.RawValue < (x << FRACTIONAL_PLACES);
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool FastGreatEqualThan(number x, number y) => x.RawValue >= y.RawValue;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool StrictGreatEqualThan(number x, number y)
        {
            if (x.RawValue == NaN.RawValue || y.RawValue == NaN.RawValue) return false;
            return x.RawValue >= y.RawValue;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=(number x, int y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return x >= (number)y;
#else
            return x.RawValue >= ((long)y << FRACTIONAL_PLACES);
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=(int x, number y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return (number)x >= y;
#else
            return ((long)x << FRACTIONAL_PLACES) >= y.RawValue;
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=(number x, float y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return x >= (number)y;
#else
            return x.RawValue >= (long)(y * ONE);
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=(float x, number y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return (number)x >= y;
#else
            return (long)(x * ONE) >= y.RawValue;
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=(number x, double y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return x >= (number)y;
#else
            return x.RawValue >= (long)(y * ONE);
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=(double x, number y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return (number)x >= y;
#else
            return (long)(x * ONE) >= y.RawValue;
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=(number x, long y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return x >= (number)y;
#else
            return x.RawValue >= (y << FRACTIONAL_PLACES);
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=(long x, number y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return (number)x >= y;
#else
            return (x << FRACTIONAL_PLACES) >= y.RawValue;
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool FastLessEqualThan(number x, number y) => x.RawValue <= y.RawValue;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool StrictLessEqualThan(number x, number y)
        {
            if (x.RawValue == NaN.RawValue || y.RawValue == NaN.RawValue) return false;
            return x.RawValue <= y.RawValue;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=(number x, int y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return x <= (number)y;
#else
            return x.RawValue <= ((long)y << FRACTIONAL_PLACES);
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=(int x, number y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return (number)x <= y;
#else
            return ((long)x << FRACTIONAL_PLACES) <= y.RawValue;
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=(number x, float y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return x <= (number)y;
#else
            return x.RawValue <= (long)(y * ONE);
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=(float x, number y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return (number)x <= y;
#else
            return (long)(x * ONE) <= y.RawValue;
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=(number x, double y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return x <= (number)y;
#else
            return x.RawValue <= (long)(y * ONE);
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=(double x, number y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return (number)x <= y;
#else
            return (long)(x * ONE) <= y.RawValue;
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=(number x, long y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return x <= (number)y;
#else
            return x.RawValue <= (y << FRACTIONAL_PLACES);
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=(long x, number y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return (number)x <= y;
#else
            return (x << FRACTIONAL_PLACES) <= y.RawValue;
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool FastLessThan(number x, number y) => x.RawValue < y.RawValue;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool StrictLessThan(number x, number y)
        {
            if (x.RawValue == NaN.RawValue || y.RawValue == NaN.RawValue) return false;
            return x.RawValue < y.RawValue;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <(number x, int y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return x < (number)y;
#else
            return x.RawValue < ((long)y << FRACTIONAL_PLACES);
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <(int x, number y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return y > x;
#else
            return ((long)x << FRACTIONAL_PLACES) < y.RawValue;
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <(number x, float y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return x < (number)y;
#else
            return x.RawValue < (long)(y * ONE);
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <(float x, number y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return y > x;
#else
            return (long)(x * ONE) < y.RawValue;
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <(number x, double y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return x < (number)y;
#else
            return x.RawValue < (long)(y * ONE);
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <(double x, number y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return y > x;
#else
            return (long)(x * ONE) < y.RawValue;
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <(number x, long y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return x < (number)y;
#else
            return x.RawValue < (y << FRACTIONAL_PLACES);
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <(long x, number y)
        {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
            return y > x;
#else
            return (x << FRACTIONAL_PLACES) < y.RawValue;
#endif
        }
        #endregion

        #region operator >>、<<
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator >>(number x, int amount)
        {
            x.RawValue >>= amount;
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number operator <<(number x, int amount)
        {
            x.RawValue <<= amount;
            return x;
        }
        #endregion

        #region operator convert from/to long、float、int、double、decimal
#if NT_STRICT_NUMBER
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator number(long value) => StrictAsNumber(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator long(number value) => StrictAsLong(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator number(int value) => StrictAsNumber(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator int(number value) => StrictAsInt(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator number(float value) => StrictAsNumber(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator float(number value) => StrictAsFloat(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator number(double value) => StrictAsNumber(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator double(number value) => StrictAsDouble(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator number(decimal value) => StrictAsNumber(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator decimal(number value) => StrictAsDecimal(value);
#else
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator number(long value) => new number(value << FRACTIONAL_PLACES);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator long(number value) => value.RawValue >> FRACTIONAL_PLACES;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator number(int value) => new number((long)value << FRACTIONAL_PLACES);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator int(number value) => (int)(value.RawValue >> FRACTIONAL_PLACES);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator number(float value) => new number((long)(value * ONE));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator float(number value) => (float)value.RawValue / ONE;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator number(double value) => new number((long)(value * ONE));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator double(number value) => (double)value.RawValue / ONE;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator number(decimal value) => new number((long)(value * ONE));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator decimal(number value) => (decimal)value.RawValue / ONE;
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FastAsNumber(long value) => new number(value << FRACTIONAL_PLACES);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number StrictAsNumber(long value) => new number(value << FRACTIONAL_PLACES);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long FastAsLong(number value) => value.RawValue >> FRACTIONAL_PLACES;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long StrictAsLong(number value) => value.RawValue >> FRACTIONAL_PLACES;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FastAsNumber(int value) => new number((long)value << FRACTIONAL_PLACES);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number StrictAsNumber(int value) => new number((long)value << FRACTIONAL_PLACES);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int FastAsInt(number value) => (int)(value.RawValue >> FRACTIONAL_PLACES);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int StrictAsInt(number value) => (int)(value.RawValue >> FRACTIONAL_PLACES);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FastAsNumber(float value) => new number((long)(value * ONE));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number StrictAsNumber(float value)
        {
            if (float.IsNaN(value)) return NaN;
            if (float.IsPositiveInfinity(value)) return PositiveInfinity;
            if (float.IsNegativeInfinity(value)) return NegativeInfinity;
            return new number((long)(value * ONE));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float FastAsFloat(number value) => (float)value.RawValue / ONE;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float StrictAsFloat(number value)
        {
            if (value.RawValue == NaN.RawValue) return float.NaN;
            if (value.RawValue == PositiveInfinity.RawValue) return float.PositiveInfinity;
            if (value.RawValue == NegativeInfinity.RawValue) return float.NegativeInfinity;
            return (float)value.RawValue / ONE;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FastAsNumber(double value) => new number((long)(value * ONE));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number StrictAsNumber(double value)
        {
            if (double.IsNaN(value)) return NaN;
            if (double.IsPositiveInfinity(value)) return PositiveInfinity;
            if (double.IsNegativeInfinity(value)) return NegativeInfinity;
            return new number((long)(value * ONE));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double FastAsDouble(number value) => (double)value.RawValue / ONE;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double StrictAsDouble(number value)
        {
            if (value.RawValue == NaN.RawValue) return double.NaN;
            if (value.RawValue == PositiveInfinity.RawValue) return double.PositiveInfinity;
            if (value.RawValue == NegativeInfinity.RawValue) return double.NegativeInfinity;
            return (double)value.RawValue / ONE;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FastAsNumber(decimal value) => new number((long)(value * ONE));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number StrictAsNumber(decimal value) => new number((long)(value * ONE));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static decimal FastAsDecimal(number value) => (decimal)value.RawValue / ONE;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static decimal StrictAsDecimal(number value) => (decimal)value.RawValue / ONE;
        #endregion

        #region IsPositiveInfinity、IsNegativeInfinity、IsInfinity、IsNaN
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPositiveInfinity(number x) => x.RawValue == PositiveInfinity.RawValue;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNegativeInfinity(number x) => x.RawValue == NegativeInfinity.RawValue;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInfinity(number x) => x.RawValue == PositiveInfinity.RawValue || x.RawValue == NegativeInfinity.RawValue;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNaN(number x) => x.RawValue == NaN.RawValue;
        #endregion

        #region Equals、CompareTo、GetHashCode
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj) => obj is number && RawValue == ((number)obj).RawValue;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(number other) => RawValue == other.RawValue;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int CompareTo(number other) => RawValue.CompareTo(other.RawValue);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int CompareTo(object obj) => obj is number f ? RawValue.CompareTo(((number)obj).RawValue) : throw new ArgumentException("obj");
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => RawValue.GetHashCode();
        #endregion

        #region ToString
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            if (RawValue == NaN.RawValue) return "NaN";
            if (RawValue == PositiveInfinity.RawValue) return "Infinity";
            if (RawValue == NegativeInfinity.RawValue) return "-Infinity";
            return ((double)this).ToString();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (RawValue == NaN.RawValue) return "NaN";
            if (RawValue == PositiveInfinity.RawValue) return "Infinity";
            if (RawValue == NegativeInfinity.RawValue) return "-Infinity";
            return ((double)this).ToString(format, formatProvider);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format)
        {
            if (RawValue == NaN.RawValue) return "NaN";
            if (RawValue == PositiveInfinity.RawValue) return "Infinity";
            if (RawValue == NegativeInfinity.RawValue) return "-Infinity";
            return ((double)this).ToString(format);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(IFormatProvider provider)
        {
            if (RawValue == NaN.RawValue) return "NaN";
            if (RawValue == PositiveInfinity.RawValue) return "Infinity";
            if (RawValue == NegativeInfinity.RawValue) return "-Infinity";
            return ((double)this).ToString(provider);
        }
#if Debug || DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToStringInv()
        {
            if (RawValue == NaN.RawValue) return "NaN";
            if (RawValue == PositiveInfinity.RawValue) return "Infinity";
            if (RawValue == NegativeInfinity.RawValue) return "-Infinity";
            return ((double)this).ToString(System.Globalization.CultureInfo.InvariantCulture);
        }
#endif
        #endregion

        #region Sign、Abs、Floor、Ceiling、Round、Truncate
#if NT_STRICT_NUMBER
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Floor(number value) => StrictFloor(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Ceiling(number value) => StrictCeiling(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Round(number x) => StrictRound(x);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Truncate(number x) => StrictTruncate(x);
#elif NT_NUMBER_BY_FLOAT
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Floor(number value) => FloatFloor(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Ceiling(number value) =>FloatCeiling(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Round(number x) => FloatRound(x);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Truncate(number x) => FloatTruncate(x);
#else
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Floor(number value)
        {
            value.RawValue &= IntegerMask;
            return value;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Ceiling(number value)
        {
            if ((value.RawValue & FracMask) != 0L)
                value.RawValue = (value.RawValue + ONE) & IntegerMask;
            return value;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Round(number x)
        {
            int sign = x.RawValue < 0 ? -1 : 1;
            if (sign < 0)
                x.RawValue = -x.RawValue;
            var lInte = x.RawValue >> FRACTIONAL_PLACES;
            var lFrac = x.RawValue & FracMask;
            if (lFrac == HALF)//优先靠近偶数且不越界，如4.5取整为4，3.5取整为4
            {
                var lret = lInte % 2L == 0L ? lInte : lInte + 1L;
                x.RawValue = sign * lret * ONE;
                return x;
            }
            x.RawValue = sign * ((x.RawValue + HALF) & IntegerMask);
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Truncate(number x)
        {
            int sign = x.RawValue < 0 ? -1 : 1;
            x.RawValue = sign * ((sign * x.RawValue) & IntegerMask);
            return x;
        }
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Sign(number value) => value.RawValue.CompareTo(0L);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Abs(number value)
        {
            if (value.RawValue < 0L)
                value.RawValue = -value.RawValue;
            return value;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FastFloor(number value)
        {
            value.RawValue &= IntegerMask;
            return value;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number StrictFloor(number value)
        {
            if (value.RawValue == NaN.RawValue) return value;
            if (value.RawValue == PositiveInfinity.RawValue || value.RawValue == NegativeInfinity.RawValue) return value;
            if (value.RawValue < -MaxInteger.RawValue) return -MaxInteger;
            value.RawValue &= IntegerMask;
            return value;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FloatFloor(number value)
        {
            value.RawValue = ONE * (long)Math.Floor((double)value.RawValue / ONE);
            return value;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FastCeiling(number value)
        {
            if ((value.RawValue & FracMask) != 0L)
                value.RawValue = (value.RawValue + ONE) & IntegerMask;
            return value;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number StrictCeiling(number value)
        {
            if (value.RawValue == NaN.RawValue) return value;
            if (value.RawValue == PositiveInfinity.RawValue || value.RawValue == NegativeInfinity.RawValue) return value;
            if (value.RawValue > MaxInteger.RawValue) return MaxInteger;
            if (value.RawValue < -MaxInteger.RawValue) return -MaxInteger;
            if ((value.RawValue & FracMask) != 0L)
                value.RawValue = (value.RawValue + ONE) & IntegerMask;
            return value;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FloatCeiling(number value)
        {
            value.RawValue = ONE * (long)Math.Ceiling((double)value.RawValue / ONE);
            return value;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FastRound(number x)
        {
            int sign = x.RawValue < 0 ? -1 : 1;
            if (sign < 0)
                x.RawValue = -x.RawValue;
            var lInte = x.RawValue >> FRACTIONAL_PLACES;
            var lFrac = x.RawValue & FracMask;
            if (lFrac == HALF)//优先靠近偶数且不越界，如4.5取整为4，3.5取整为4
            {
                var lret = lInte % 2L == 0L ? lInte : lInte + 1L;
                x.RawValue = sign * lret * ONE;
                return x;
            }
            x.RawValue = sign * ((x.RawValue + HALF) & IntegerMask);
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number StrictRound(number x)
        {
            if (x.RawValue == NaN.RawValue) return x;
            if (x.RawValue == PositiveInfinity.RawValue || x.RawValue == NegativeInfinity.RawValue) return x;
            if (x.RawValue > MaxInteger.RawValue) return MaxInteger;
            if (x.RawValue < -MaxInteger.RawValue) return -MaxInteger;
            int sign = x.RawValue < 0 ? -1 : 1;
            if (sign < 0)
                x.RawValue = -x.RawValue;
            var lInte = x.RawValue >> FRACTIONAL_PLACES;
            var lFrac = x.RawValue & FracMask;
            if (lFrac == HALF)//优先靠近偶数且不越界，如4.5取整为4，3.5取整为4
            {
                var lret = lInte % 2L == 0L ? lInte : lInte + 1L;
                x.RawValue = sign * lret * ONE;
                return x;
            }
            x.RawValue = sign * ((x.RawValue + HALF) & IntegerMask);
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FloatRound(number x)
        {
            x.RawValue = ONE * (long)Math.Round((double)x.RawValue / ONE);
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FastTruncate(number x)
        {
            int sign = x.RawValue < 0 ? -1 : 1;
            x.RawValue = sign * ((sign * x.RawValue) & IntegerMask);
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number StrictTruncate(number x)
        {
            if (x.RawValue == NaN.RawValue) return x;
            if (x.RawValue == PositiveInfinity.RawValue || x.RawValue == NegativeInfinity.RawValue) return x;
            int sign = x.RawValue < 0 ? -1 : 1;
            x.RawValue = sign * ((sign * x.RawValue) & IntegerMask);
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FloatTruncate(number x)
        {
            x.RawValue = ONE * (long)Math.Truncate((double)x.RawValue / ONE);
            return x;
        }
        #endregion

        #region Sqrt、Pow、Exp、Log、Log2、Log10
#if NT_STRICT_NUMBER
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Sqrt(number x) => StrictSqrt(x);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Pow(number nbase, number x) => StrictPow(nbase, x);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Exp(number x) => StrictExp(x);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Log(number x, number nbase) => StrictLog(x, nbase);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Log(number x) => StrictLog(x, EXP);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Log2(number x) => StrictLog2(x);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Log10(number x) => StrictLog10(x);
#elif NT_NUMBER_BY_FLOAT
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Sqrt(number x) => FloatSqrt(x);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Pow(number nbase, number x) => FloatPow(nbase, x);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Exp(number x) => FloatExp(x);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Log(number x, number nbase) => FloatLog(x, nbase);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Log(number x) => FloatLog(x, EXP);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Log2(number x) => FloatLog2(x);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Log10(number x) => FloatLog10(x);
#else
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Sqrt(number x)
        {
            x.RawValue = SqrtRaw(x.RawValue);
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Pow(number nbase, number x)
        {
            nbase.RawValue = PowRaw(nbase.RawValue, x.RawValue);
            return nbase;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Exp(number x)
        {
            x.RawValue = PowRaw(EXP.RawValue, x.RawValue);
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Log(number x, number nbase)
        {
            x.RawValue = LogRaw(x.RawValue, nbase.RawValue);
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Log(number x)
        {
            x.RawValue = LogRaw(x.RawValue, EXP.RawValue);
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Log2(number x)
        {
            x.RawValue = LogRaw(x.RawValue, 2L << FRACTIONAL_PLACES);
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Log10(number x)
        {
            x.RawValue = LogRaw(x.RawValue, 10L << FRACTIONAL_PLACES);
            return x;
        }
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long SqrtRaw(long x)
        {
            if (x < 0L) throw new ArgumentOutOfRangeException("The number x has to be positive");
            if (x == 0L) return 0L;
            bool lessThanOne = x < ONE;
            long sValue = x;
            int count = 0;
            if (lessThanOne) sValue <<= FRACTIONAL_PLACES;
            while (sValue > WaterShedSqrt)
            {
                count++;
                sValue >>= 2;
            }
            long index = (sValue - ONE) / 3L;
#if !NO_NUMBER_SUPPORT_BURST
            sValue = NumberLut.sqrt_aprox_lut.Data[(int)index] << count;
#else
            sValue = NumberLut.sqrt_aprox_lut[index] << count;
#endif
            if (lessThanOne) sValue >>= FRACTIONAL_PLACES_SQRT;
            return sValue;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FastSqrt(number value)
        {
            value.RawValue = SqrtRaw(value.RawValue);
            return value;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number StrictSqrt(number value)
        {
            if (value.RawValue == NaN.RawValue || value.RawValue < 0) return NaN;
            if (value.RawValue == PositiveInfinity.RawValue || value.RawValue == NegativeInfinity.RawValue) return value;
            value.RawValue = SqrtRaw(value.RawValue);
            return value;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FloatSqrt(number x)
        {
            x.RawValue = (long)(ONE * Math.Sqrt((double)x.RawValue / ONE));
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static long PowRaw(long nbase, int x)
        {
            if (x == 1) return nbase;
            if (x == 0) return 1L;
            long tmp = PowRaw(nbase, x / 2);
            tmp = (tmp * tmp) >> FRACTIONAL_PLACES;
            return (x & 1) == 0/*偶数*/ ? tmp : ((nbase * tmp) >> FRACTIONAL_PLACES);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static long PowRaw(long nbase, long x)
        {
            if (x == 0L) return ONE;
            if (x == ONE) return nbase;
            if (nbase == ONE) return ONE;
            if (nbase == 0L) return x < 0 ? long.MaxValue : 0L;
            bool isneg = x < 0;
            if (isneg) x = -x;
            int lInt = (int)(x >> FRACTIONAL_PLACES);
            long lFrac = x & FracMask;
            long ret = PowRaw(nbase, lInt);
            long tmp = SqrtRaw(nbase);
            long mask = ONE;
            while ((mask >>= 1) != 0)
            {
                if ((mask & lFrac) != 0)
                    ret = (ret * tmp) >> FRACTIONAL_PLACES;
                lFrac &= ~mask;
                if (lFrac == 0)
                    break;
                tmp = SqrtRaw(tmp);
            }
            return isneg ? (ONE << FRACTIONAL_PLACES) / ret : ret;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FastPow(number nbase, number x)
        {
            nbase.RawValue = PowRaw(nbase.RawValue, x.RawValue);
            return nbase;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number StrictPow(number nbase, number x)
        {
            if (nbase.RawValue == NaN.RawValue || x.RawValue == NaN.RawValue) return NaN;
            if (nbase.RawValue == ONE) return one;
            if (x.RawValue == 0L) return one;
            if (x.RawValue == ONE) return nbase;
            if (nbase.RawValue == NegativeInfinity.RawValue)
            {
                if (x.RawValue < 0) return zero;
                if ((x.RawValue & FracMask) == 0L && (x.RawValue >> FRACTIONAL_PLACES) % 2 == 1)//奇数
                    return NegativeInfinity;
                return PositiveInfinity;
            }
            if (nbase.RawValue == PositiveInfinity.RawValue) return x.RawValue < 0 ? zero : nbase;
            if (nbase.RawValue == 0L) return x.RawValue < 0 ? PositiveInfinity : zero;
            nbase.RawValue = PowRaw(nbase.RawValue, x.RawValue);
            return nbase;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FloatPow(number nbase, number x)
        {
            nbase.RawValue = (long)(ONE * Math.Pow((double)nbase.RawValue / ONE, (double)x.RawValue / ONE));
            return nbase;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FastExp(number x)
        {
            x.RawValue = PowRaw(EXP.RawValue, x.RawValue);
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number StrictExp(number x) => StrictPow(EXP, x);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FloatExp(number x)
        {
            x.RawValue = (long)(ONE * Math.Exp((double)x.RawValue / ONE));
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static long LogRaw(long x, long nbase)
        {
            if (x <= 0 || nbase < 0) return long.MaxValue;
            if (nbase == 0) return x == 0 ? ONE : long.MaxValue;
            if (nbase == ONE) return long.MaxValue;
            if (x == ONE) return 0L;
            long lInt = 0;
            int sign = 1;
            if (nbase < ONE) { sign = -1; nbase = (ONE << FRACTIONAL_PLACES) / nbase; }
            if (x < ONE) { sign *= -1; x = (ONE << FRACTIONAL_PLACES) / x; }
            while (x > nbase) { lInt++; x = (x << FRACTIONAL_PLACES) / nbase; }
            lInt <<= FRACTIONAL_PLACES;
            long tmp = SqrtRaw(nbase);
            long mask = ONE;
            while ((mask >>= 1) != 0)
            {
                if (x == tmp)
                {
                    lInt |= mask;
                    break;
                }
                if (x > tmp)
                {
                    x = (x << FRACTIONAL_PLACES) / tmp;
                    lInt |= mask;
                }
                tmp = SqrtRaw(tmp);
            }
            return sign * lInt;

        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FastLog(number x, number nbase)
        {
            x.RawValue = LogRaw(x.RawValue, nbase.RawValue);
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number StrictLog(number x, number nbase)
        {
            if (x.RawValue <= 0L || nbase.RawValue < 0L) return NaN;
            if (nbase.RawValue == 0L) return x.RawValue == 0L ? one : NaN;
            if (nbase.RawValue == ONE) return NaN;
            if (nbase.RawValue == PositiveInfinity.RawValue) return x.RawValue == ONE ? zero : NaN;
            if (nbase.RawValue == NegativeInfinity.RawValue) return NaN;
            if (x.RawValue == PositiveInfinity.RawValue) return nbase.RawValue > ONE ? PositiveInfinity : NegativeInfinity;
            if (x.RawValue == ONE) return zero;
            nbase.RawValue = LogRaw(x.RawValue, nbase.RawValue);
            return nbase;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FloatLog(number x, number nbase)
        {
            x.RawValue = (long)(ONE * Math.Log((double)x.RawValue / ONE, (double)nbase.RawValue / ONE));
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FastLog2(number x)
        {
            x.RawValue = LogRaw(x.RawValue, 2L << FRACTIONAL_PLACES);
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number StrictLog2(number x) => StrictLog(x, (number)2);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FloatLog2(number x)
        {
            x.RawValue = (long)(ONE * Math.Log((double)x.RawValue / ONE, 2.0));
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FastLog10(number x)
        {
            x.RawValue = LogRaw(x.RawValue, 10L << FRACTIONAL_PLACES);
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number StrictLog10(number x) => StrictLog(x, (number)10);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FloatLog10(number x)
        {
            x.RawValue = (long)(ONE * Math.Log10((double)x.RawValue / ONE));
            return x;
        }
        #endregion

        #region Sin, Cos, Tan, Asin, Acos, Atan, Atan2
#if NT_STRICT_NUMBER
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Sin(number i) => StrictSin(i);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Cos(number i) => StrictCos(i);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Tan(number i) => StrictTan(i);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Asin(number F) => StrictAsin(F);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Acos(number F) => StrictAcos(F);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Atan(number F) => StrictAtan(F);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Atan2(number F1, number F2) => StrictAtan2(F1, F2);
#elif NT_NUMBER_BY_FLOAT
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Sin(number i) => FloatSin(i);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Cos(number i) => FloatCos(i);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Tan(number i) => FloatTan(i);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Asin(number F) => FloatAsin(F);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Acos(number F) => FloatAcos(F);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Atan(number F) => FloatAtan(F);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Atan2(number F1, number F2) => FloatAtan2(F1, F2);
#else
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Sin(number rad)
        {
            rad.RawValue %= PiTimes2;
            if (rad.RawValue < -Pi)
                rad.RawValue = -Pi - rad.RawValue;
            else if (rad.RawValue > Pi)
                rad.RawValue = Pi - rad.RawValue;
#if !NO_NUMBER_SUPPORT_BURST
            rad.RawValue = NumberLut.sin_lut.Data[(int)(rad.RawValue + Pi)];
#else
            rad.RawValue = NumberLut.sin_lut[rad.RawValue + Pi];
#endif
            return rad;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Cos(number rad)
        {
            rad.RawValue %= PiTimes2;
            if (rad.RawValue < -Pi)
                rad.RawValue = PiTimes2 + rad.RawValue;
            else if (rad.RawValue > Pi)
                rad.RawValue = PiTimes2 - rad.RawValue;
#if !NO_NUMBER_SUPPORT_BURST
            rad.RawValue = NumberLut.cos_lut.Data[(int)(rad.RawValue + Pi)];
#else
            rad.RawValue = NumberLut.cos_lut[rad.RawValue + Pi];
#endif
            return rad;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Tan(number rad)
        {
            if (rad.RawValue < -Pi)
                rad.RawValue %= -Pi;
            if (rad.RawValue > Pi)
                rad.RawValue %= Pi;
#if !NO_NUMBER_SUPPORT_BURST
            rad.RawValue = NumberLut.tan_lut.Data[(int)(rad.RawValue + Pi)];
#else
            rad.RawValue = NumberLut.tan_lut[rad.RawValue + Pi];
#endif
            return rad;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Asin(number value)
        {
            if (value.RawValue < -ONE || value.RawValue > ONE) return NaN;
#if !NO_NUMBER_SUPPORT_BURST
            value.RawValue = NumberLut.asin_lut.Data[(int)(value.RawValue + ONE)];
#else
            value.RawValue = NumberLut.asin_lut[value.RawValue + ONE];
#endif
            return value;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Acos(number value)
        {
            if (value.RawValue < -ONE || value.RawValue > ONE) return NaN;
#if !NO_NUMBER_SUPPORT_BURST
            value.RawValue = NumberLut.acos_lut.Data[(int)(value.RawValue + ONE)];
#else
            value.RawValue = NumberLut.acos_lut[value.RawValue + ONE];
#endif
            return value;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Atan(number value)
        {
            value.RawValue = AtanRaw(value.RawValue);
            return value;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Atan2(number y, number x)
        {
            y.RawValue = Atan2Raw(y.RawValue, x.RawValue);
            return y;
        }
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FastSin(number rad)
        {
            rad.RawValue %= PiTimes2;
            if (rad.RawValue < -Pi)
                rad.RawValue = -Pi - rad.RawValue;
            else if (rad.RawValue > Pi)
                rad.RawValue = Pi - rad.RawValue;
#if !NO_NUMBER_SUPPORT_BURST
            rad.RawValue = NumberLut.sin_lut.Data[(int)(rad.RawValue + Pi)];
#else
            rad.RawValue = NumberLut.sin_lut[rad.RawValue + Pi];
#endif
            return rad;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number StrictSin(number rad)
        {
            if (rad.RawValue == NaN.RawValue) return NaN;
            if (rad.RawValue == PositiveInfinity.RawValue || rad.RawValue == NegativeInfinity.RawValue) return NaN;
            if (rad.RawValue == MaxValue.RawValue || rad.RawValue == MinValue.RawValue) return rad;
            rad.RawValue %= PiTimes2;
            if (rad.RawValue < -Pi)
                rad.RawValue = -Pi - rad.RawValue;
            else if (rad.RawValue > Pi)
                rad.RawValue = Pi - rad.RawValue;
#if !NO_NUMBER_SUPPORT_BURST
            rad.RawValue = NumberLut.sin_lut.Data[(int)(rad.RawValue + Pi)];
#else
            rad.RawValue = NumberLut.sin_lut[rad.RawValue + Pi];
#endif
            return rad;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FloatSin(number rad)
        {
            rad.RawValue = (long)(ONE * Math.Sin((double)rad.RawValue / ONE));
            return rad;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FastCos(number rad)
        {
            rad.RawValue %= PiTimes2;
            if (rad.RawValue < -Pi)
                rad.RawValue = PiTimes2 + rad.RawValue;
            else if (rad.RawValue > Pi)
                rad.RawValue = PiTimes2 - rad.RawValue;
#if !NO_NUMBER_SUPPORT_BURST
            rad.RawValue = NumberLut.cos_lut.Data[(int)(rad.RawValue + Pi)];
#else
            rad.RawValue = NumberLut.cos_lut[rad.RawValue + Pi];
#endif
            return rad;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number StrictCos(number rad)
        {
            if (rad.RawValue == NaN.RawValue) return NaN;
            if (rad.RawValue == PositiveInfinity.RawValue || rad.RawValue == NegativeInfinity.RawValue) return NaN;
            if (rad.RawValue == MaxValue.RawValue || rad.RawValue == MinValue.RawValue) return rad;
            rad.RawValue %= PiTimes2;
            if (rad.RawValue < -Pi)
                rad.RawValue = PiTimes2 + rad.RawValue;
            else if (rad.RawValue > Pi)
                rad.RawValue = PiTimes2 - rad.RawValue;
#if !NO_NUMBER_SUPPORT_BURST
            rad.RawValue = NumberLut.cos_lut.Data[(int)(rad.RawValue + Pi)];
#else
            rad.RawValue = NumberLut.cos_lut[rad.RawValue + Pi];
#endif
            return rad;

        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FloatCos(number rad)
        {
            rad.RawValue = (long)(ONE * Math.Sin(((double)rad.RawValue + PiDiv2) / ONE));
            return rad;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FastTan(number rad)
        {
            if (rad.RawValue < -Pi)
                rad.RawValue %= -Pi;
            if (rad.RawValue > Pi)
                rad.RawValue %= Pi;
#if !NO_NUMBER_SUPPORT_BURST
            rad.RawValue = NumberLut.tan_lut.Data[(int)(rad.RawValue + Pi)];
#else
            rad.RawValue = NumberLut.tan_lut[rad.RawValue + Pi];
#endif
            return rad;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number StrictTan(number rad)
        {
            if (rad.RawValue == NaN.RawValue) return NaN;
            if (rad.RawValue == PositiveInfinity.RawValue || rad.RawValue == NegativeInfinity.RawValue) return NaN;
            if (rad.RawValue == MaxValue.RawValue || rad.RawValue == MinValue.RawValue) return rad;
            if (rad.RawValue < -Pi)
                rad.RawValue %= -Pi;
            if (rad.RawValue > Pi)
                rad.RawValue %= Pi;
#if !NO_NUMBER_SUPPORT_BURST
            rad.RawValue = NumberLut.tan_lut.Data[(int)(rad.RawValue + Pi)];
#else
            rad.RawValue = NumberLut.tan_lut[rad.RawValue + Pi];
#endif
            return rad;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FloatTan(number rad)
        {
            rad.RawValue = (long)(ONE * Math.Tan(((double)rad.RawValue + PiDiv2) / ONE));
            return rad;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FastAsin(number value)
        {
            if (value.RawValue < -ONE || value.RawValue > ONE) return NaN;
#if !NO_NUMBER_SUPPORT_BURST
            value.RawValue = NumberLut.asin_lut.Data[(int)(value.RawValue + ONE)];
#else
            value.RawValue = NumberLut.asin_lut[value.RawValue + ONE];
#endif
            return value;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number StrictAsin(number value)
        {
            if (value.RawValue == NaN.RawValue) return NaN;
            if (value.RawValue < -ONE || value.RawValue > ONE) return NaN;
#if !NO_NUMBER_SUPPORT_BURST
            value.RawValue = NumberLut.asin_lut.Data[(int)(value.RawValue + ONE)];
#else
            value.RawValue = NumberLut.asin_lut[value.RawValue + ONE];
#endif
            return value;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FloatAsin(number value)
        {
            value.RawValue = (long)(ONE * Math.Asin((double)value.RawValue / ONE));
            return value;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FastAcos(number value)
        {
            if (value.RawValue < -ONE || value.RawValue > ONE) return NaN;
#if !NO_NUMBER_SUPPORT_BURST
            value.RawValue = NumberLut.acos_lut.Data[(int)(value.RawValue + ONE)];
#else
            value.RawValue = NumberLut.acos_lut[value.RawValue + ONE];
#endif
            return value;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number StrictAcos(number value)
        {
            if (value.RawValue == NaN.RawValue) return NaN;
            if (value.RawValue < -ONE || value.RawValue > ONE) return NaN;
#if !NO_NUMBER_SUPPORT_BURST
            value.RawValue = NumberLut.acos_lut.Data[(int)(value.RawValue + ONE)];
#else
            value.RawValue = NumberLut.acos_lut[value.RawValue + ONE];
#endif
            return value;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FloatAcos(number value)
        {
            value.RawValue = (long)(ONE * Math.Acos((double)value.RawValue / ONE));
            return value;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long AtanRaw(long value)
        {
            bool isneg = value < 0;
            if (isneg) value = -value;
            if (value <= LutGenerator.ATAN_DENSITY1_COVER_RAW)
            {
#if !NO_NUMBER_SUPPORT_BURST
                value = NumberLut.atan_lut.Data[(int)value];
#else
                value = NumberLut.atan_lut[value];
#endif
                if (isneg) value = -value;
                return value;
            }
            if (value <= LutGenerator.ATAN_DENSITY2_COVER_RAW)
            {
                value = (value - LutGenerator.ATAN_DENSITY1_COVER_RAW) >> 12;
#if !NO_NUMBER_SUPPORT_BURST
                value = NumberLut.atan_lut.Data[(int)(LutGenerator.ATAN_DENSITY2_INDEX_BEGIN + value)];
#else
                value = NumberLut.atan_lut[LutGenerator.ATAN_DENSITY2_INDEX_BEGIN + value];
#endif
                if (isneg) value = -value;
                return value;
            }
            if (value <= LutGenerator.ATAN_DENSITY3_COVER_RAW)
            {
                value = (value - LutGenerator.ATAN_DENSITY2_COVER_RAW) >> 20;
#if !NO_NUMBER_SUPPORT_BURST
                value = NumberLut.atan_lut.Data[(int)(LutGenerator.ATAN_DENSITY3_INDEX_BEGIN + value)];
#else
                value = NumberLut.atan_lut[LutGenerator.ATAN_DENSITY3_INDEX_BEGIN + value];
#endif
                if (isneg) value = -value;
                return value;
            }
#if !NO_NUMBER_SUPPORT_BURST
            value = NumberLut.atan_lut.Data[NumberLut.atan_lut.Data.length - 1];
#else
            value = NumberLut.atan_lut[NumberLut.atan_lut.Length - 1];
#endif
            if (isneg) value = -value;
            return value;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FastAtan(number value)
        {
            value.RawValue = AtanRaw(value.RawValue);
            return value;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number StrictAtan(number value)
        {
            if (value.RawValue == NaN.RawValue) return NaN;
            value.RawValue = AtanRaw(value.RawValue);
            return value;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FloatAtan(number value)
        {
            value.RawValue = (long)(ONE * Math.Atan((double)value.RawValue / ONE));
            return value;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long Atan2Raw(long y, long x)
        {
            bool isnegX = x < 0L, isnegY = y < 0L;
            long xRawValue = isnegX ? -x : x;
            long yRawValue = isnegY ? -y : y;
            bool bHighArea = xRawValue < yRawValue;//true:(Pi/4, Pi/2] or false:[0, Pi/4]
            long idx = bHighArea ? xRawValue : yRawValue;
            xRawValue = bHighArea ? yRawValue : xRawValue;
            yRawValue = idx;
            idx = (yRawValue << number.FRACTIONAL_PLACES) / xRawValue;
#if !NO_NUMBER_SUPPORT_BURST
            long ret = bHighArea ? number.PiDiv2 - NumberLut.atan2_lut.Data[(int)idx] : NumberLut.atan2_lut.Data[(int)idx];
#else
            long ret = bHighArea ? number.PiDiv2 - NumberLut.atan2_lut[idx] : NumberLut.atan2_lut[idx];
#endif
            ret = isnegX ? number.Pi - ret : ret;
            return isnegY ? -ret : ret;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FastAtan2(number y, number x)
        {
            y.RawValue = Atan2Raw(y.RawValue, x.RawValue);
            return y;
            //if (x.RawValue > 0L)
            //{
            //    x.RawValue = (y.RawValue << FRACTIONAL_PLACES) / x.RawValue;
            //    x.RawValue = AtanRaw(x.RawValue);
            //    return x;
            //}
            //if (x.RawValue < 0L)
            //{
            //    x.RawValue = (y.RawValue << FRACTIONAL_PLACES) / x.RawValue;
            //    x.RawValue = AtanRaw(x.RawValue) + (y.RawValue >= 0L ? Pi : -Pi);
            //    return x;
            //}
            //if (y.RawValue == 0L) return zero;
            //return y.RawValue > 0L ? PIDiv2 : -PIDiv2;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number StrictAtan2(number y, number x)
        {
            if (y.RawValue == NaN.RawValue || x.RawValue == NaN.RawValue) return NaN;
            if (x.RawValue > 0L) return StrictAtan(StrictDivide(y, x));
            if (x.RawValue < 0L)
            {
                y.RawValue = StrictAtan(StrictDivide(y, x)).RawValue + (y.RawValue >= 0L ? Pi : -Pi);
                return y;
            }
            if (y.RawValue == 0L) return zero;
            return y.RawValue > 0L ? PIDiv2 : -PIDiv2;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FloatAtan2(number y, number x)
        {
            y.RawValue = (long)(ONE * Math.Atan2((double)y.RawValue / ONE, (double)x.RawValue / ONE));
            return y;
        }
        #endregion

        #region Sinh, Cosh, Tanh
#if NT_STRICT_NUMBER
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Sinh(number x) => StrictSinh(x);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Cosh(number x) => StrictCosh(x);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Tanh(number x) => StrictTanh(x);
#elif NT_NUMBER_BY_FLOAT
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Sinh(number x) => FloatSinh(x);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Cosh(number x) => FloatCosh(x);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Tanh(number x) => FloatTanh(x);
#else
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Sinh(number x)
        {
            long rawValue = PowRaw(EXP.RawValue, -x.RawValue);
            x.RawValue = PowRaw(EXP.RawValue, x.RawValue);
            x.RawValue = (x.RawValue - rawValue) / 2L;
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Cosh(number x)
        {
            long rawValue = PowRaw(EXP.RawValue, -x.RawValue);
            x.RawValue = PowRaw(EXP.RawValue, x.RawValue);
            x.RawValue = (x.RawValue + rawValue) / 2L;
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number Tanh(number x)
        {
            x.RawValue = PowRaw(EXP.RawValue, x.RawValue <<= 1);
            x.RawValue = ((x.RawValue - ONE) << FRACTIONAL_PLACES) / (x.RawValue + ONE);
            return x;
        }
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FastSinh(number x)
        {
            long rawValue = PowRaw(EXP.RawValue, -x.RawValue);
            x.RawValue = PowRaw(EXP.RawValue, x.RawValue);
            x.RawValue = (x.RawValue - rawValue) / 2L;
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number StrictSinh(number x)
        {
            if (x.RawValue == NaN.RawValue) return NaN;
            x = StrictExp(x) - StrictExp(-x);
            x.RawValue /= 2;
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FloatSinh(number x)
        {
            x.RawValue = (long)(ONE * Math.Sinh((double)x.RawValue / ONE));
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FastCosh(number x)
        {
            long rawValue = PowRaw(EXP.RawValue, -x.RawValue);
            x.RawValue = PowRaw(EXP.RawValue, x.RawValue);
            x.RawValue = (x.RawValue + rawValue) / 2L;
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number StrictCosh(number x)
        {
            if (x.RawValue == NaN.RawValue) return NaN;
            x = StrictExp(x) + StrictExp(-x);
            x.RawValue /= 2;
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FloatCosh(number x)
        {
            x.RawValue = (long)(ONE * Math.Cosh((double)x.RawValue / ONE));
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FastTanh(number x)
        {
            x.RawValue = PowRaw(EXP.RawValue, x.RawValue <<= 1);
            x.RawValue = ((x.RawValue - ONE) << FRACTIONAL_PLACES) / (x.RawValue + ONE);
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number StrictTanh(number x)
        {
            if (x.RawValue == NaN.RawValue) return NaN;
            x.RawValue <<= 1;
            x = StrictExp(x);
            x.RawValue = ((x.RawValue - ONE) << FRACTIONAL_PLACES) / (x.RawValue + ONE);
            return x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number FloatTanh(number x)
        {
            x.RawValue = (long)(ONE * Math.Tanh((double)x.RawValue / ONE));
            return x;
        }
        #endregion

        #region Min, Max
        public static number Min(number x, number y) => x.RawValue < y.RawValue ? x : y;
        public static number Max(number x, number y) => x.RawValue > y.RawValue ? x : y;
        #endregion
    }
}