using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Deterministics.Math;

#if Debug || DEBUG
using System.Diagnostics;
#endif

#if Debug || DEBUG
// Internal representation is identical to IEEE binary32 floating point numbers
[DebuggerDisplay("{ToStringInv()}")]
#endif
[System.Serializable]
[StructLayout(LayoutKind.Explicit)]
[Unity.IL2CPP.CompilerServices.Il2CppEagerStaticClassConstruction]
public struct fp : IEquatable<fp>, IComparable<fp>
{
    private static readonly fp _atan2Number1 = new fp(-883);
    private static readonly fp _atan2Number2 = new fp(3767);
    private static readonly fp _atan2Number3 = new fp(7945);
    private static readonly fp _atan2Number4 = new fp(12821);
    private static readonly fp _atan2Number5 = new fp(21822);
    private static readonly fp _atan2Number6 = new fp(65536);
    private static readonly fp _atan2Number7 = new fp(102943);
    private static readonly fp _atan2Number8 = new fp(205887);
    private static readonly fp _atanApproximatedNumber1 = new fp(16036);
    private static readonly fp _atanApproximatedNumber2 = new fp(4345);

    [FieldOffset(0)]
    public long RawValue;
    #region 静态常量
    public static readonly fp _0_9995 = Create(0, 9995);
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

    readonly static fp one_div_pi2 = FromRaw(OneDivPi2Long);

    //以上写法在导出IL2CPP代码时会导出IL2CPP_RUNTIME_CLASS_INIT运行时代理，导致性能下降；因而改用以下写法
    public unsafe static fp MinNormal
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            long raw = 1L;
            return *(fp*)(&raw);
        }
    }
    public unsafe static fp one
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            long raw = ONE;
            return *(fp*)(&raw);
        }
    }
    public unsafe static fp two
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            long raw = ONE;
            return *(fp*)(&raw);
        }
    }
    public unsafe static fp three
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            long raw = ONE;
            return *(fp*)(&raw);
        }
    }
    public unsafe static fp half
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            long raw = HALF;
            return *(fp*)(&raw);
        }
    }
    public unsafe static fp _0_25
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            long raw = _0_25Const;
            return *(fp*)(&raw);
        }
    }
    public unsafe static fp zero
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            long raw = 0L;
            return *(fp*)(&raw);
        }
    }
    public unsafe static fp PI
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            long raw = RawPiLong;
            return *(fp*)(&raw);
        }
    }
    
    public unsafe static fp PITimes2
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            long raw = RawPiTimes2Long;
            return *(fp*)(&raw);
        }
    }
    public unsafe static fp Rad2Deg
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            long raw = nRad2Deg;
            return *(fp*)(&raw);
        }
    }
    public unsafe static fp Deg2Rad
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            long raw = nDeg2Rad;
            return *(fp*)(&raw);
        }
    }
    public unsafe static fp EXP
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            long raw = lExp;
            return *(fp*)(&raw);
        }
    }
    public unsafe static fp NaN
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            long raw = long.MinValue;
            return *(fp*)(&raw);
        }
    }
    public unsafe static fp PositiveInfinity
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            long raw = long.MaxValue;
            return *(fp*)(&raw);
        }
    }
    public unsafe static fp MaxValue
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            long raw = long.MaxValue - 1L;
            return *(fp*)(&raw);
        }
    }
    public unsafe static fp NegativeInfinity
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            long raw = long.MinValue + 1L;
            return *(fp*)(&raw);
        }
    }
    public unsafe static fp MinValue
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            long raw = long.MinValue + 2L;
            return *(fp*)(&raw);
        }
    }
    public unsafe static fp MaxInteger
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            long raw = nMaxInteger;
            return *(fp*)(&raw);
        }
    }
    #endregion
    public const long PiDiv2Long = (long)(Math.PI * ONE / 2 + 0.5);
    public const long RawPiLong = (long)(Math.PI * ONE + 0.5);
    public const long RawPiTimes2Long = (long)(2 * Math.PI * ONE + 0.5);
    public const long OneDivPi2Long = (long)(ONE / Math.PI / 2f + 0.5);
    const long nRad2Deg = (long)(ONE * 180.0 / Math.PI + 0.5);
    const long nDeg2Rad = (long)(ONE * Math.PI / 180.0 + 0.5);
    const long lExp = (long)(Math.E * ONE + 0.5);
    public const int FRACTIONAL_PLACES = 16;
    public const int FRACTIONAL_PLACES_SQRT = 8;
    const long WaterShedSqrt = 4L << FRACTIONAL_PLACES;
    public const long ONE = 1L << FRACTIONAL_PLACES;
    public const long nMaxInteger = (long.MaxValue - 1L) & IntegerMask;
    public const long SMALL_SQRT = ONE >> 12;
    const long HALF = ONE >> 1;
    const long _0_25Const = ONE >> 2;
    const long FracMask = ONE - 1L;//小数部分
    const long IntegerMask = ~FracMask;//整数部分

    internal fp(long rawValue)
    {
        RawValue = rawValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp FromRaw(long rawValue) => new fp(rawValue);

    #region operator +、-、*、/
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp operator +(fp x, fp y)
    {
        x.RawValue += y.RawValue;
        return x;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp operator -(fp x, fp y)
    {
        x.RawValue -= y.RawValue;
        return x;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp operator *(fp x, fp y)
    {
        x.RawValue = (x.RawValue * y.RawValue) >> FRACTIONAL_PLACES;
        return x;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp operator /(fp x, fp y)
    {
        x.RawValue = (x.RawValue << FRACTIONAL_PLACES) / y.RawValue;
        return x;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp operator +(fp x, int y)
    {
        x.RawValue += (long)y << FRACTIONAL_PLACES;
        return x;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp operator +(int x, fp y)
    {
        y.RawValue += (long)x << FRACTIONAL_PLACES;
        return y;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp operator -(fp x, int y)
    {
        x.RawValue -= (long)y << FRACTIONAL_PLACES;
        return x;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp operator -(int x, fp y)
    {
        y.RawValue = ((long)x << FRACTIONAL_PLACES) - y.RawValue;
        return y;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp operator *(fp x, int y)
    {
        x.RawValue *= y;
        return x;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp operator *(int x, fp y)
    {
        y.RawValue *= x;
        return y;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp operator /(fp x, int y)
    {
        x.RawValue /= y;
        return x;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp operator /(int x, fp y)
    {
        long rawValue = (long)x << (FRACTIONAL_PLACES * 2);
        y.RawValue = rawValue / y.RawValue;
        return y;
    }
    #endregion

        #region operator ++、--
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp operator ++(fp x)
    {
        x.RawValue += ONE;
        return x;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp operator --(fp x)
    {
        x.RawValue -= ONE;
        return x;
    }
    #endregion

    #region operator 求余、取正、取反
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp operator %(fp x, fp y)
    {
        x.RawValue %= y.RawValue;
        return x;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp operator %(fp x, int y)
    {
        x.RawValue %= (long)y << FRACTIONAL_PLACES;
        return x;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp operator %(int x, fp y)
    {
#if NT_STRICT_NUMBER || NT_NUMBER_BY_FLOAT
        return (number)x % y;
#else
        y.RawValue = ((long)x << FRACTIONAL_PLACES) % y.RawValue;
        return y;
#endif
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp operator +(fp x) => x;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp operator -(fp x)
    {
        x.RawValue = -x.RawValue;
        return x;
    }
    #endregion

    #region operator ==、!=、>、<、>=、<=
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(fp x, fp y)
    {
        return x.RawValue == y.RawValue;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(fp x, int y)
    {
        return x.RawValue == ((long)y << FRACTIONAL_PLACES);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(int x, fp y)
    {
        return y.RawValue == ((long)x << FRACTIONAL_PLACES);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(fp x, int y)
    {
        return x.RawValue != ((long)y << FRACTIONAL_PLACES);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(int x, fp y)
    {
        return y.RawValue != ((long)x << FRACTIONAL_PLACES);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(fp x, fp y)
    {
        return x.RawValue != y.RawValue;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(fp x, fp y)
    {
        return x.RawValue > y.RawValue;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(fp x, fp y)
    {
        return x.RawValue < y.RawValue;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(fp x, int y)
    {
        return x.RawValue > ((long)y << FRACTIONAL_PLACES);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(int x, fp y)
    {
        return y.RawValue < ((long)x << FRACTIONAL_PLACES);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(fp x, int y)
    {
        return x.RawValue >= ((long)y << FRACTIONAL_PLACES);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(int x, fp y)
    {
        return ((long)x << FRACTIONAL_PLACES) >= y.RawValue;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(fp x, int y)
    {
        return x.RawValue <= ((long)y << FRACTIONAL_PLACES);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(int x, fp y)
    {
        return ((long)x << FRACTIONAL_PLACES) <= y.RawValue;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(fp x, int y)
    {
        return x.RawValue < ((long)y << FRACTIONAL_PLACES);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(int x, fp y)
    {
        return ((long)x << FRACTIONAL_PLACES) < y.RawValue;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(fp x, fp y)
    {
        return x.RawValue >= y.RawValue;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(fp x, fp y)
    {
        return x.RawValue <= y.RawValue;
    }
    #endregion

    #region operator convert from/to long、float、int
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator float(fp value) => (float)value.RawValue / ONE;
    public static implicit operator fp(int value) => new fp((long)value << FRACTIONAL_PLACES);
    public static implicit operator fp(uint value) => new fp((long)value << FRACTIONAL_PLACES);
    #endregion

    #region IsPositiveInfinity、IsNegativeInfinity、IsInfinity、IsNaN
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsPositiveInfinity(fp x) => x.RawValue == PositiveInfinity.RawValue;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNegativeInfinity(fp x) => x.RawValue == NegativeInfinity.RawValue;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsInfinity(fp x) => x.RawValue == PositiveInfinity.RawValue || x.RawValue == NegativeInfinity.RawValue;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNaN(fp x) => x.RawValue == NaN.RawValue;
    #endregion

    #region Equals、CompareTo、GetHashCode
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object obj) => obj is fp && RawValue == ((fp)obj).RawValue;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(fp other) => RawValue == other.RawValue;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(fp other) => RawValue.CompareTo(other.RawValue);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(object obj) => obj is fp f ? RawValue.CompareTo(((fp)obj).RawValue) : throw new ArgumentException("obj");
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
        return ((float)this).ToString("F5");
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToString(string format, IFormatProvider formatProvider)
    {
        if (RawValue == NaN.RawValue) return "NaN";
        if (RawValue == PositiveInfinity.RawValue) return "Infinity";
        if (RawValue == NegativeInfinity.RawValue) return "-Infinity";
        return ((float)this).ToString(format, formatProvider);
    }
#if Debug || DEBUG
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToStringInv()
    {
        if (RawValue == NaN.RawValue) return "NaN";
        if (RawValue == PositiveInfinity.RawValue) return "Infinity";
        if (RawValue == NegativeInfinity.RawValue) return "-Infinity";
        return ((float)this).ToString(System.Globalization.CultureInfo.InvariantCulture);
    }
#endif
    #endregion

    #region Sign、Abs、Floor、Ceiling、Round、Truncate
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp Floor(fp value)
    {
        value.RawValue &= IntegerMask;
        return value;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp Ceiling(fp value)
    {
        if ((value.RawValue & FracMask) != 0L)
            value.RawValue = (value.RawValue + ONE) & IntegerMask;
        return value;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp Round(fp x)
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
    public static fp Truncate(fp x)
    {
        int sign = x.RawValue < 0 ? -1 : 1;
        x.RawValue = sign * ((sign * x.RawValue) & IntegerMask);
        return x;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Sign(fp value) => value.RawValue.CompareTo(0L);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp Abs(fp value)
    {
        if (value.RawValue < 0L)
            value.RawValue = -value.RawValue;
        return value;
    }
    #endregion

    #region Sqrt、Pow、Exp、Log、Log2、Log10
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp Sqrt(fp x)
    {
        x.RawValue = SqrtRaw(x.RawValue);
        return x;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp Pow(fp nbase, fp x)
    {
        nbase.RawValue = PowRaw(nbase.RawValue, x.RawValue);
        return nbase;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp Exp(fp x)
    {
        x.RawValue = PowRaw(EXP.RawValue, x.RawValue);
        return x;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp Log(fp x, fp nbase)
    {
        x.RawValue = LogRaw(x.RawValue, nbase.RawValue);
        return x;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp Log(fp x)
    {
        x.RawValue = LogRaw(x.RawValue, EXP.RawValue);
        return x;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp Log2(fp x)
    {
        x.RawValue = LogRaw(x.RawValue, 2L << FRACTIONAL_PLACES);
        return x;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp Log10(fp x)
    {
        x.RawValue = LogRaw(x.RawValue, 10L << FRACTIONAL_PLACES);
        return x;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long SqrtRaw(long x)
    {
        return fixlut.sqrt(x);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static long PowRaw(long nbase, int x)
    {
        if (x == 1) return nbase;
        if (x == 0) return ONE;
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
    public static fp StrictPow(fp nbase, fp x)
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
    public static fp StrictExp(fp x) => StrictPow(EXP, x);
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
    #endregion

    #region Sin, Cos, Tan, Asin, Acos, Atan, Atan2
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp Sin(fp num)
    {
        num.RawValue %= RawPiTimes2Long;
        num       *= one_div_pi2;
        var raw = fixlut.sin(num.RawValue);
        fp result;
        result.RawValue = raw;
        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp Cos(fp num)
    {
        num.RawValue %= RawPiTimes2Long;
        num       *= fp.one_div_pi2;
        return new fp(fixlut.cos(num.RawValue));
    }

    public static void SinCos(fp num, out fp sin, out fp cos)
    {
        num.RawValue %= RawPiTimes2Long;
        num       *= one_div_pi2;
        fixlut.sin_cos(num.RawValue, out var sinVal, out var cosVal);
        sin.RawValue = sinVal;
        cos.RawValue = cosVal;
    }

    public static void SinCos(long num, out long sin, out long cos)
    {
        num %= RawPiTimes2Long;
        num = (num * OneDivPi2Long) >> FRACTIONAL_PLACES;
        fixlut.sin_cos(num, out sin, out cos);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp Tan(fp num)
    {
        num.RawValue %= RawPiTimes2Long;
        num       *= one_div_pi2;
        return new fp(fixlut.tan(num.RawValue));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp Asin(fp value)
    {
        return new fp(fixlut.asin(value.RawValue));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp Acos(fp value)
    {
        return new fp(fixlut.acos(value.RawValue));
    }

    /// <param name="num">Tan</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp Atan(fp num) {
        return Atan2(num, fp.one);
    }

    /// <param name="x">Denominator</param>
    /// <param name="y">Numerator</param>
    public static fp Atan2(fp y, fp x) {
        var absX = Abs(x);
        var absY = Abs(y);
        var t3   = absX;
        var t1   = absY;
        var t0   = Max(t3, t1);
        t1 = Min(t3, t1);
        t3 = fp.one / t0;
        t3 = t1 * t3;
        var t4 = t3 * t3;
        t0 = _atan2Number1;
        t0 = t0 * t4 + _atan2Number2;
        t0 = t0 * t4 - _atan2Number3;
        t0 = t0 * t4 + _atan2Number4;
        t0 = t0 * t4 - _atan2Number5;
        t0 = t0 * t4 + _atan2Number6;
        t3 = t0 * t3;
        t3 = absY > absX ? _atan2Number7 - t3 : t3;
        t3 = x < fp.zero ? _atan2Number8 - t3 : t3;
        t3 = y < fp.zero ? -t3 : t3;
        return t3;
    }
    #endregion

    #region Sinh, Cosh, Tanh
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp Sinh(fp x)
    {
        long rawValue = PowRaw(EXP.RawValue, -x.RawValue);
        x.RawValue = PowRaw(EXP.RawValue, x.RawValue);
        x.RawValue = (x.RawValue - rawValue) / 2L;
        return x;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp Cosh(fp x)
    {
        long rawValue = PowRaw(EXP.RawValue, -x.RawValue);
        x.RawValue = PowRaw(EXP.RawValue, x.RawValue);
        x.RawValue = (x.RawValue + rawValue) / 2L;
        return x;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp Tanh(fp x)
    {
        x.RawValue = PowRaw(EXP.RawValue, x.RawValue <<= 1);
        x.RawValue = ((x.RawValue - ONE) << FRACTIONAL_PLACES) / (x.RawValue + ONE);
        return x;
    }
    #endregion

    #region Min, Max
    public static fp Min(fp x, fp y) => x.RawValue < y.RawValue ? x : y;
    public static fp Max(fp x, fp y) => x.RawValue > y.RawValue ? x : y;
    #endregion

    // ------------------- 后面移除掉。
    #region operator &、|、^、~
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp operator &(fp x, fp y)
    {
        x.RawValue &= y.RawValue;
        return x;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp operator |(fp x, fp y)
    {
        x.RawValue |= y.RawValue;
        return x;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp operator ^(fp x, fp y)
    {
        x.RawValue ^= y.RawValue;
        return x;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp operator ~(fp x)
    {
        x.RawValue = ~x.RawValue;
        return x;
    }
    #endregion

    #region operator >>、<<
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp operator >>(fp x, int amount)
    {
        x.RawValue >>= amount;
        return x;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp operator <<(fp x, int amount)
    {
        x.RawValue <<= amount;
        return x;
    }
    #endregion

    public static fp ConvertFrom(float f)
    {
        return new fp(){RawValue = (long)(Math.Round(f * ONE))};
    }

    public static fp Create(long integerPart, int fractionPart)
    {
        if(integerPart != 0 && fractionPart < 0) throw new Exception($"创建错误 {integerPart} {fractionPart}");

        return CreateInternal(integerPart, fractionPart, 10000);
    }

    public static fp CreateByDivisor(long value, int divisor)
    {
        if(divisor < 0) throw new Exception($"创建错误 除数== 0");

        var integerPart = value / divisor;
        var fraction = (int)value % divisor;

        return CreateInternal(integerPart, fraction, divisor);
    }

    public static fp CreateByDivisor(uint value, int divisor)
    {
        return CreateByDivisor((int)value, divisor);
    }

    static fp CreateInternal(long intPart, int fractionPart, int divisor)
    {
        // Assert.IsTrue(fractionPart < divisor);
        var fraction = (fractionPart << FRACTIONAL_PLACES) / divisor;
        var v = (intPart << FRACTIONAL_PLACES) + fraction;
        return new fp(){ RawValue = v };    
    }

    public static fp Parse(string value)
    {
        float x = float.Parse(value);

        var intValue = (long)Math.Round(x * 10000);
        return CreateByDivisor(intValue, 10000);
    }
}