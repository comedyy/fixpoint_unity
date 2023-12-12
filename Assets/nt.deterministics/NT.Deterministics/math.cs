using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using ShuffleComponent = Unity.Mathematics.math.ShuffleComponent;

namespace Nt.Deterministics
{
    /// <summary>
    /// A static class to contain various math functions and constants.
    /// </summary>
    public static partial class math
    {
        /// <summary>Returns the bit pattern of a number as a uint.</summary>
        /// <param name="x">The number bits to copy.</param>
        /// <returns>The uint with the same bit pattern as the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint asuint(number x) { return (uint)(int)x; }

        /// <summary>Returns the bit pattern of a float2 as a uint2.</summary>
        /// <param name="x">The float2 bits to copy.</param>
        /// <returns>The uint2 with the same bit pattern as the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint2 asuint(float2 x) { return new uint2(asuint(x.x), asuint(x.y)); }

        /// <summary>Returns the bit pattern of a float3 as a uint3.</summary>
        /// <param name="x">The float3 bits to copy.</param>
        /// <returns>The uint3 with the same bit pattern as the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3 asuint(float3 x) { return new uint3(asuint(x.x), asuint(x.y), asuint(x.z)); }

        /// <summary>Returns the bit pattern of a float4 as a uint4.</summary>
        /// <param name="x">The float4 bits to copy.</param>
        /// <returns>The uint4 with the same bit pattern as the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint4 asuint(float4 x) { return new uint4(asuint(x.x), asuint(x.y), asuint(x.z), asuint(x.w)); }

        /// <summary>Returns the bit pattern of a double as a number.</summary>
        /// <param name="x">The double bits to copy.</param>
        /// <returns>The long with the same bit pattern as the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number asnumber(double x)
        {
            LongDoubleUnion u;
            u.longValue = 0;
            u.doubleValue = x;
            return (number)u.longValue;
        }

        /// <summary>Returns the bit pattern of a double2 as a float2.</summary>
        /// <param name="x">The double bits to copy.</param>
        /// <returns>The long with the same bit pattern as the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 asnumber(double2 x) { return float2(asnumber(x.x), asnumber(x.y)); }

        /// <summary>Returns the bit pattern of a double3 as a float3.</summary>
        /// <param name="x">The double bits to copy.</param>
        /// <returns>The long with the same bit pattern as the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 asnumber(double3 x) { return float3(asnumber(x.x), asnumber(x.y), asnumber(x.z)); }

        /// <summary>Returns the bit pattern of a double4 as a float4.</summary>
        /// <param name="x">The double bits to copy.</param>
        /// <returns>The long with the same bit pattern as the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 asnumber(double4 x) { return float4(asnumber(x.x), asnumber(x.y), asnumber(x.z), asnumber(x.w)); }

        /// <summary>Returns the bit pattern of a number as a double.</summary>
        /// <param name="x">The long bits to copy.</param>
        /// <returns>The double with the same bit pattern as the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double asdouble(number x)
        {
            LongDoubleUnion u;
            u.doubleValue = 0;
            u.longValue = x.RawValue;
            return u.doubleValue;
        }

        /// <summary>Returns the bit pattern of a float2 as a double2.</summary>
        /// <param name="x">The long bits to copy.</param>
        /// <returns>The double with the same bit pattern as the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2 asdouble(float2 x) { return new double2(asdouble(x.x), asdouble(x.y)); }

        /// <summary>Returns the bit pattern of a float3 as a double3.</summary>
        /// <param name="x">The long bits to copy.</param>
        /// <returns>The double with the same bit pattern as the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double3 asdouble(float3 x) { return new double3(asdouble(x.x), asdouble(x.y), asdouble(x.z)); }

        /// <summary>Returns the bit pattern of a float4 as a double4.</summary>
        /// <param name="x">The long bits to copy.</param>
        /// <returns>The double with the same bit pattern as the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double4 asdouble(float4 x) { return new double4(asdouble(x.x), asdouble(x.y), asdouble(x.z), asdouble(x.w)); }


        /// <summary>Returns true if the input number is a finite floating point value, false otherwise.</summary>
        /// <param name="x">The number value to test.</param>
        /// <returns>True if the number is finite, false otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool isfinite(number x) { return abs(x) < number.PositiveInfinity; }

        /// <summary>Returns a bool2 indicating for each component of a float2 whether it is a finite floating point value.</summary>
        /// <param name="x">The float2 value to test.</param>
        /// <returns>A bool2 where it is true in a component if that component is finite, false otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool2 isfinite(float2 x) { return abs(x) < number.PositiveInfinity; }

        /// <summary>Returns a bool3 indicating for each component of a float3 whether it is a finite floating point value.</summary>
        /// <param name="x">The float3 value to test.</param>
        /// <returns>A bool3 where it is true in a component if that component is finite, false otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3 isfinite(float3 x) { return abs(x) < number.PositiveInfinity; }

        /// <summary>Returns a bool4 indicating for each component of a float4 whether it is a finite floating point value.</summary>
        /// <param name="x">The float4 value to test.</param>
        /// <returns>A bool4 where it is true in a component if that component is finite, false otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool4 isfinite(float4 x) { return abs(x) < number.PositiveInfinity; }

        /// <summary>Returns the minimum of two number values.</summary>
        /// <param name="x">The first input value.</param>
        /// <param name="y">The second input value.</param>
        /// <returns>The minimum of the two input values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number min(number x, number y) { return x <= y ? x : y; }

        /// <summary>Returns the componentwise minimum of two float2 vectors.</summary>
        /// <param name="x">The first input value.</param>
        /// <param name="y">The second input value.</param>
        /// <returns>The componentwise minimum of the two input values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 min(float2 x, float2 y) { return new float2(min(x.x, y.x), min(x.y, y.y)); }

        /// <summary>Returns the componentwise minimum of two float3 vectors.</summary>
        /// <param name="x">The first input value.</param>
        /// <param name="y">The second input value.</param>
        /// <returns>The componentwise minimum of the two input values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 min(float3 x, float3 y) { return new float3(min(x.x, y.x), min(x.y, y.y), min(x.z, y.z)); }

        /// <summary>Returns the componentwise minimum of two float4 vectors.</summary>
        /// <param name="x">The first input value.</param>
        /// <param name="y">The second input value.</param>
        /// <returns>The componentwise minimum of the two input values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 min(float4 x, float4 y) { return new float4(min(x.x, y.x), min(x.y, y.y), min(x.z, y.z), min(x.w, y.w)); }

        /// <summary>Returns the maximum of two number values.</summary>
        /// <param name="x">The first input value.</param>
        /// <param name="y">The second input value.</param>
        /// <returns>The maximum of the two input values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number max(number x, number y) { return x >= y ? x : y; }

        /// <summary>Returns the componentwise maximum of two float2 vectors.</summary>
        /// <param name="x">The first input value.</param>
        /// <param name="y">The second input value.</param>
        /// <returns>The componentwise maximum of the two input values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 max(float2 x, float2 y) { return new float2(max(x.x, y.x), max(x.y, y.y)); }

        /// <summary>Returns the componentwise maximum of two float3 vectors.</summary>
        /// <param name="x">The first input value.</param>
        /// <param name="y">The second input value.</param>
        /// <returns>The componentwise maximum of the two input values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 max(float3 x, float3 y) { return new float3(max(x.x, y.x), max(x.y, y.y), max(x.z, y.z)); }

        /// <summary>Returns the componentwise maximum of two float4 vectors.</summary>
        /// <param name="x">The first input value.</param>
        /// <param name="y">The second input value.</param>
        /// <returns>The componentwise maximum of the two input values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 max(float4 x, float4 y) { return new float4(max(x.x, y.x), max(x.y, y.y), max(x.z, y.z), max(x.w, y.w)); }


        /// <summary>Returns the result of linearly interpolating from x to y using the interpolation parameter s.</summary>
        /// <remarks>
        /// If the interpolation parameter is not in the range [0, 1], then this function extrapolates.
        /// </remarks>
        /// <param name="x">The first endpoint, corresponding to the interpolation parameter value of 0.</param>
        /// <param name="y">The second endpoint, corresponding to the interpolation parameter value of 1.</param>
        /// <param name="s">The interpolation parameter. May be a value outside the interval [0, 1].</param>
        /// <returns>The interpolation from x to y.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number lerp(number x, number y, number s) { return x + s * (y - x); }

        /// <summary>Returns the result of a componentwise linear interpolating from x to y using the interpolation parameter s.</summary>
        /// <remarks>
        /// If the interpolation parameter is not in the range [0, 1], then this function extrapolates.
        /// </remarks>
        /// <param name="x">The first endpoint, corresponding to the interpolation parameter value of 0.</param>
        /// <param name="y">The second endpoint, corresponding to the interpolation parameter value of 1.</param>
        /// <param name="s">The interpolation parameter. May be a value outside the interval [0, 1].</param>
        /// <returns>The componentwise interpolation from x to y.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 lerp(float2 x, float2 y, number s) { return x + s * (y - x); }

        /// <summary>Returns the result of a componentwise linear interpolating from x to y using the interpolation parameter s.</summary>
        /// <remarks>
        /// If the interpolation parameter is not in the range [0, 1], then this function extrapolates.
        /// </remarks>
        /// <param name="x">The first endpoint, corresponding to the interpolation parameter value of 0.</param>
        /// <param name="y">The second endpoint, corresponding to the interpolation parameter value of 1.</param>
        /// <param name="s">The interpolation parameter. May be a value outside the interval [0, 1].</param>
        /// <returns>The componentwise interpolation from x to y.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 lerp(float3 x, float3 y, number s) { return x + s * (y - x); }

        /// <summary>Returns the result of a componentwise linear interpolating from x to y using the interpolation parameter s.</summary>
        /// <remarks>
        /// If the interpolation parameter is not in the range [0, 1], then this function extrapolates.
        /// </remarks>
        /// <param name="x">The first endpoint, corresponding to the interpolation parameter value of 0.</param>
        /// <param name="y">The second endpoint, corresponding to the interpolation parameter value of 1.</param>
        /// <param name="s">The interpolation parameter. May be a value outside the interval [0, 1].</param>
        /// <returns>The componentwise interpolation from x to y.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 lerp(float4 x, float4 y, number s) { return x + s * (y - x); }


        /// <summary>Returns the result of a componentwise linear interpolating from x to y using the corresponding components of the interpolation parameter s.</summary>
        /// <remarks>
        /// If the interpolation parameter is not in the range [0, 1], then this function extrapolates.
        /// </remarks>
        /// <param name="x">The first endpoint, corresponding to the interpolation parameter value of 0.</param>
        /// <param name="y">The second endpoint, corresponding to the interpolation parameter value of 1.</param>
        /// <param name="s">The interpolation parameter. May be a value outside the interval [0, 1].</param>
        /// <returns>The componentwise interpolation from x to y.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 lerp(float2 x, float2 y, float2 s) { return x + s * (y - x); }

        /// <summary>Returns the result of a componentwise linear interpolating from x to y using the corresponding components of the interpolation parameter s.</summary>
        /// <remarks>
        /// If the interpolation parameter is not in the range [0, 1], then this function extrapolates.
        /// </remarks>
        /// <param name="x">The first endpoint, corresponding to the interpolation parameter value of 0.</param>
        /// <param name="y">The second endpoint, corresponding to the interpolation parameter value of 1.</param>
        /// <param name="s">The interpolation parameter. May be a value outside the interval [0, 1].</param>
        /// <returns>The componentwise interpolation from x to y.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 lerp(float3 x, float3 y, float3 s) { return x + s * (y - x); }

        /// <summary>Returns the result of a componentwise linear interpolating from x to y using the corresponding components of the interpolation parameter s.</summary>
        /// <remarks>
        /// If the interpolation parameter is not in the range [0, 1], then this function extrapolates.
        /// </remarks>
        /// <param name="x">The first endpoint, corresponding to the interpolation parameter value of 0.</param>
        /// <param name="y">The second endpoint, corresponding to the interpolation parameter value of 1.</param>
        /// <param name="s">The interpolation parameter. May be a value outside the interval [0, 1].</param>
        /// <returns>The componentwise interpolation from x to y.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 lerp(float4 x, float4 y, float4 s) { return x + s * (y - x); }


        /// <summary>Returns the result of normalizing a fix point value x to a range [a, b]. The opposite of lerp. Equivalent to (x - a) / (b - a).</summary>
        /// <param name="a">The first endpoint of the range.</param>
        /// <param name="b">The second endpoint of the range.</param>
        /// <param name="x">The value to normalize to the range.</param>
        /// <returns>The interpolation parameter of x with respect to the input range [a, b].</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number unlerp(number a, number b, number x) { return (x - a) / (b - a); }

        /// <summary>Returns the componentwise result of normalizing a fix point value x to a range [a, b]. The opposite of lerp. Equivalent to (x - a) / (b - a).</summary>
        /// <param name="a">The first endpoint of the range.</param>
        /// <param name="b">The second endpoint of the range.</param>
        /// <param name="x">The value to normalize to the range.</param>
        /// <returns>The componentwise interpolation parameter of x with respect to the input range [a, b].</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 unlerp(float2 a, float2 b, float2 x) { return (x - a) / (b - a); }

        /// <summary>Returns the componentwise result of normalizing a fix point value x to a range [a, b]. The opposite of lerp. Equivalent to (x - a) / (b - a).</summary>
        /// <param name="a">The first endpoint of the range.</param>
        /// <param name="b">The second endpoint of the range.</param>
        /// <param name="x">The value to normalize to the range.</param>
        /// <returns>The componentwise interpolation parameter of x with respect to the input range [a, b].</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 unlerp(float3 a, float3 b, float3 x) { return (x - a) / (b - a); }

        /// <summary>Returns the componentwise result of normalizing a fix point value x to a range [a, b]. The opposite of lerp. Equivalent to (x - a) / (b - a).</summary>
        /// <param name="a">The first endpoint of the range.</param>
        /// <param name="b">The second endpoint of the range.</param>
        /// <param name="x">The value to normalize to the range.</param>
        /// <returns>The componentwise interpolation parameter of x with respect to the input range [a, b].</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 unlerp(float4 a, float4 b, float4 x) { return (x - a) / (b - a); }


        /// <summary>Returns the result of a non-clamping linear remapping of a value x from source range [a, b] to the destination range [c, d].</summary>
        /// <param name="a">The first endpoint of the source range [a,b].</param>
        /// <param name="b">The second endpoint of the source range [a, b].</param>
        /// <param name="c">The first endpoint of the destination range [c, d].</param>
        /// <param name="d">The second endpoint of the destination range [c, d].</param>
        /// <param name="x">The value to remap from the source to destination range.</param>
        /// <returns>The remap of input x from the source range to the destination range.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number remap(number a, number b, number c, number d, number x) { return lerp(c, d, unlerp(a, b, x)); }

        /// <summary>Returns the componentwise result of a non-clamping linear remapping of a value x from source range [a, b] to the destination range [c, d].</summary>
        /// <param name="a">The first endpoint of the source range [a,b].</param>
        /// <param name="b">The second endpoint of the source range [a, b].</param>
        /// <param name="c">The first endpoint of the destination range [c, d].</param>
        /// <param name="d">The second endpoint of the destination range [c, d].</param>
        /// <param name="x">The value to remap from the source to destination range.</param>
        /// <returns>The componentwise remap of input x from the source range to the destination range.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 remap(float2 a, float2 b, float2 c, float2 d, float2 x) { return lerp(c, d, unlerp(a, b, x)); }

        /// <summary>Returns the componentwise result of a non-clamping linear remapping of a value x from source range [a, b] to the destination range [c, d].</summary>
        /// <param name="a">The first endpoint of the source range [a,b].</param>
        /// <param name="b">The second endpoint of the source range [a, b].</param>
        /// <param name="c">The first endpoint of the destination range [c, d].</param>
        /// <param name="d">The second endpoint of the destination range [c, d].</param>
        /// <param name="x">The value to remap from the source to destination range.</param>
        /// <returns>The componentwise remap of input x from the source range to the destination range.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 remap(float3 a, float3 b, float3 c, float3 d, float3 x) { return lerp(c, d, unlerp(a, b, x)); }

        /// <summary>Returns the componentwise result of a non-clamping linear remapping of a value x from source range [a, b] to the destination range [c, d].</summary>
        /// <param name="a">The first endpoint of the source range [a,b].</param>
        /// <param name="b">The second endpoint of the source range [a, b].</param>
        /// <param name="c">The first endpoint of the destination range [c, d].</param>
        /// <param name="d">The second endpoint of the destination range [c, d].</param>
        /// <param name="x">The value to remap from the source to destination range.</param>
        /// <returns>The componentwise remap of input x from the source range to the destination range.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 remap(float4 a, float4 b, float4 c, float4 d, float4 x) { return lerp(c, d, unlerp(a, b, x)); }


        /// <summary>Returns the result of a multiply-add operation (a * b + c) on 3 number values.</summary>
        /// <remarks>
        /// When Burst compiled with fast math enabled on some architectures, this could be converted to a fused multiply add (FMA).
        /// FMA is more accurate due to rounding once at the end of the computation rather than twice that is required when
        /// this computation is not fused.
        /// </remarks>
        /// <param name="a">First value to multiply.</param>
        /// <param name="b">Second value to multiply.</param>
        /// <param name="c">Third value to add to the product of a and b.</param>
        /// <returns>The multiply-add of the inputs.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number mad(number a, number b, number c) { return a * b + c; }

        /// <summary>Returns the result of a componentwise multiply-add operation (a * b + c) on 3 float2 vectors.</summary>
        /// <remarks>
        /// When Burst compiled with fast math enabled on some architectures, this could be converted to a fused multiply add (FMA).
        /// FMA is more accurate due to rounding once at the end of the computation rather than twice that is required when
        /// this computation is not fused.
        /// </remarks>
        /// <param name="a">First value to multiply.</param>
        /// <param name="b">Second value to multiply.</param>
        /// <param name="c">Third value to add to the product of a and b.</param>
        /// <returns>The componentwise multiply-add of the inputs.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 mad(float2 a, float2 b, float2 c) { return a * b + c; }

        /// <summary>Returns the result of a componentwise multiply-add operation (a * b + c) on 3 float3 vectors.</summary>
        /// <remarks>
        /// When Burst compiled with fast math enabled on some architectures, this could be converted to a fused multiply add (FMA).
        /// FMA is more accurate due to rounding once at the end of the computation rather than twice that is required when
        /// this computation is not fused.
        /// </remarks>
        /// <param name="a">First value to multiply.</param>
        /// <param name="b">Second value to multiply.</param>
        /// <param name="c">Third value to add to the product of a and b.</param>
        /// <returns>The componentwise multiply-add of the inputs.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 mad(float3 a, float3 b, float3 c) { return a * b + c; }

        /// <summary>Returns the result of a componentwise multiply-add operation (a * b + c) on 3 float4 vectors.</summary>
        /// <remarks>
        /// When Burst compiled with fast math enabled on some architectures, this could be converted to a fused multiply add (FMA).
        /// FMA is more accurate due to rounding once at the end of the computation rather than twice that is required when
        /// this computation is not fused.
        /// </remarks>
        /// <param name="a">First value to multiply.</param>
        /// <param name="b">Second value to multiply.</param>
        /// <param name="c">Third value to add to the product of a and b.</param>
        /// <returns>The componentwise multiply-add of the inputs.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 mad(float4 a, float4 b, float4 c) { return a * b + c; }


        /// <summary>Returns the result of clamping the value x into the interval [a, b], where x, a and b are number values.</summary>
        /// <param name="x">Input value to be clamped.</param>
        /// <param name="a">Lower bound of the interval.</param>
        /// <param name="b">Upper bound of the interval.</param>
        /// <returns>The clamping of the input x into the interval [a, b].</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number clamp(number x, number a, number b) { return max(a, min(b, x)); }

        /// <summary>Returns the result of a componentwise clamping of the value x into the interval [a, b], where x, a and b are float2 vectors.</summary>
        /// <param name="x">Input value to be clamped.</param>
        /// <param name="a">Lower bound of the interval.</param>
        /// <param name="b">Upper bound of the interval.</param>
        /// <returns>The componentwise clamping of the input x into the interval [a, b].</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 clamp(float2 x, float2 a, float2 b) { return max(a, min(b, x)); }

        /// <summary>Returns the result of a componentwise clamping of the value x into the interval [a, b], where x, a and b are float3 vectors.</summary>
        /// <param name="x">Input value to be clamped.</param>
        /// <param name="a">Lower bound of the interval.</param>
        /// <param name="b">Upper bound of the interval.</param>
        /// <returns>The componentwise clamping of the input x into the interval [a, b].</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 clamp(float3 x, float3 a, float3 b) { return max(a, min(b, x)); }

        /// <summary>Returns the result of a componentwise clamping of the value x into the interval [a, b], where x, a and b are float4 vectors.</summary>
        /// <param name="x">Input value to be clamped.</param>
        /// <param name="a">Lower bound of the interval.</param>
        /// <param name="b">Upper bound of the interval.</param>
        /// <returns>The componentwise clamping of the input x into the interval [a, b].</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 clamp(float4 x, float4 a, float4 b) { return max(a, min(b, x)); }


        /// <summary>Returns the result of clamping the number value x into the interval [0, 1].</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The clamping of the input into the interval [0, 1].</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number saturate(number x) { return clamp(x, number.zero, number.one); }

        /// <summary>Returns the result of a componentwise clamping of the float2 vector x into the interval [0, 1].</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise clamping of the input into the interval [0, 1].</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 saturate(float2 x) { return clamp(x, new float2(0.0f), new float2(1.0f)); }

        /// <summary>Returns the result of a componentwise clamping of the float3 vector x into the interval [0, 1].</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise clamping of the input into the interval [0, 1].</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 saturate(float3 x) { return clamp(x, new float3(0.0f), new float3(1.0f)); }

        /// <summary>Returns the result of a componentwise clamping of the float4 vector x into the interval [0, 1].</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise clamping of the input into the interval [0, 1].</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 saturate(float4 x) { return clamp(x, new float4(0.0f), new float4(1.0f)); }


        /// <summary>Returns the absolute value of a number value.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The absolute value of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number abs(number x) { return number.Abs(x); }

        /// <summary>Returns the componentwise absolute value of a float2 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise absolute value of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 abs(float2 x) { return float2(abs(x.x), abs(x.y)); }

        /// <summary>Returns the componentwise absolute value of a float3 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise absolute value of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 abs(float3 x) { return float3(abs(x.x), abs(x.y), abs(x.z)); }

        /// <summary>Returns the componentwise absolute value of a float4 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise absolute value of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 abs(float4 x) { return float4(abs(x.x), abs(x.y), abs(x.z), abs(x.w)); }


        /// <summary>Returns the dot product of two number values. Equivalent to multiplication.</summary>
        /// <param name="x">The first value.</param>
        /// <param name="y">The second value.</param>
        /// <returns>The dot product of two values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number dot(number x, number y) { return x * y; }

        /// <summary>Returns the dot product of two float2 vectors.</summary>
        /// <param name="x">The first vector.</param>
        /// <param name="y">The second vector.</param>
        /// <returns>The dot product of two vectors.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number dot(float2 x, float2 y) { return x.x * y.x + x.y * y.y; }

        /// <summary>Returns the dot product of two float3 vectors.</summary>
        /// <param name="x">The first vector.</param>
        /// <param name="y">The second vector.</param>
        /// <returns>The dot product of two vectors.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number dot(float3 x, float3 y) { return x.x * y.x + x.y * y.y + x.z * y.z; }

        /// <summary>Returns the dot product of two float4 vectors.</summary>
        /// <param name="x">The first vector.</param>
        /// <param name="y">The second vector.</param>
        /// <returns>The dot product of two vectors.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number dot(float4 x, float4 y) { return x.x * y.x + x.y * y.y + x.z * y.z + x.w * y.w; }


        /// <summary>Returns the tangent of a number value.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The tangent of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number tan(number x) { return number.Tan(x); }

        /// <summary>Returns the componentwise tangent of a float2 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise tangent of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 tan(float2 x) { return float2(tan(x.x), tan(x.y)); }

        /// <summary>Returns the componentwise tangent of a float3 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise tangent of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 tan(float3 x) { return float3(tan(x.x), tan(x.y), tan(x.z)); }

        /// <summary>Returns the componentwise tangent of a float4 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise tangent of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 tan(float4 x) { return float4(tan(x.x), tan(x.y), tan(x.z), tan(x.w)); }


        /// <summary>Returns the hyperbolic tangent of a number value.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The hyperbolic tangent of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number tanh(number x) { return number.Tanh(x); }

        /// <summary>Returns the componentwise hyperbolic tangent of a float2 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise hyperbolic tangent of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 tanh(float2 x) { return float2(tanh(x.x), tanh(x.y)); }

        /// <summary>Returns the componentwise hyperbolic tangent of a float3 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise hyperbolic tangent of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 tanh(float3 x) { return float3(tanh(x.x), tanh(x.y), tanh(x.z)); }

        /// <summary>Returns the componentwise hyperbolic tangent of a float4 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise hyperbolic tangent of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 tanh(float4 x) { return float4(tanh(x.x), tanh(x.y), tanh(x.z), tanh(x.w)); }


        /// <summary>Returns the arctangent of a number value.</summary>
        /// <param name="x">A tangent value, usually the ratio y/x on the unit circle.</param>
        /// <returns>The arctangent of the input, in radians.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number atan(number x) { return number.Atan(x); }

        /// <summary>Returns the componentwise arctangent of a float2 vector.</summary>
        /// <param name="x">A tangent value, usually the ratio y/x on the unit circle.</param>
        /// <returns>The componentwise arctangent of the input, in radians.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 atan(float2 x) { return float2(atan(x.x), atan(x.y)); }

        /// <summary>Returns the componentwise arctangent of a float3 vector.</summary>
        /// <param name="x">A tangent value, usually the ratio y/x on the unit circle.</param>
        /// <returns>The componentwise arctangent of the input, in radians.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 atan(float3 x) { return float3(atan(x.x), atan(x.y), atan(x.z)); }

        /// <summary>Returns the componentwise arctangent of a float4 vector.</summary>
        /// <param name="x">A tangent value, usually the ratio y/x on the unit circle.</param>
        /// <returns>The componentwise arctangent of the input, in radians.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 atan(float4 x) { return float4(atan(x.x), atan(x.y), atan(x.z), atan(x.w)); }


        /// <summary>Returns the 2-argument arctangent of a pair of number values.</summary>
        /// <param name="y">Numerator of the ratio y/x, usually the y component on the unit circle.</param>
        /// <param name="x">Denominator of the ratio y/x, usually the x component on the unit circle.</param>
        /// <returns>The arctangent of the ratio y/x, in radians.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number atan2(number y, number x) { return number.Atan2(y, x); }

        /// <summary>Returns the componentwise 2-argument arctangent of a pair of float2 vectors.</summary>
        /// <param name="y">Numerator of the ratio y/x, usually the y component on the unit circle.</param>
        /// <param name="x">Denominator of the ratio y/x, usually the x component on the unit circle.</param>
        /// <returns>The componentwise arctangent of the ratio y/x, in radians.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 atan2(float2 y, float2 x) { return float2(atan2(y.x, x.x), atan2(y.y, x.y)); }

        /// <summary>Returns the componentwise 2-argument arctangent of a pair of float3 vectors.</summary>
        /// <param name="y">Numerator of the ratio y/x, usually the y component on the unit circle.</param>
        /// <param name="x">Denominator of the ratio y/x, usually the x component on the unit circle.</param>
        /// <returns>The componentwise arctangent of the ratio y/x, in radians.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 atan2(float3 y, float3 x) { return float3(atan2(y.x, x.x), atan2(y.y, x.y), atan2(y.z, x.z)); }

        /// <summary>Returns the componentwise 2-argument arctangent of a pair of float4 vectors.</summary>
        /// <param name="y">Numerator of the ratio y/x, usually the y component on the unit circle.</param>
        /// <param name="x">Denominator of the ratio y/x, usually the x component on the unit circle.</param>
        /// <returns>The componentwise arctangent of the ratio y/x, in radians.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 atan2(float4 y, float4 x) { return float4(atan2(y.x, x.x), atan2(y.y, x.y), atan2(y.z, x.z), atan2(y.w, x.w)); }


        /// <summary>Returns the cosine of a number value.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The cosine cosine of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number cos(number x) { return number.Cos(x); }

        /// <summary>Returns the componentwise cosine of a float2 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise cosine cosine of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 cos(float2 x) { return float2(cos(x.x), cos(x.y)); }

        /// <summary>Returns the componentwise cosine of a float3 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise cosine cosine of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 cos(float3 x) { return float3(cos(x.x), cos(x.y), cos(x.z)); }

        /// <summary>Returns the componentwise cosine of a float4 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise cosine cosine of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 cos(float4 x) { return float4(cos(x.x), cos(x.y), cos(x.z), cos(x.w)); }


        /// <summary>Returns the hyperbolic cosine of a number value.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The hyperbolic cosine of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number cosh(number x) { return number.Cosh(x); }

        /// <summary>Returns the componentwise hyperbolic cosine of a float2 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise hyperbolic cosine of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 cosh(float2 x) { return float2(cosh(x.x), cosh(x.y)); }

        /// <summary>Returns the componentwise hyperbolic cosine of a float3 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise hyperbolic cosine of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 cosh(float3 x) { return float3(cosh(x.x), cosh(x.y), cosh(x.z)); }

        /// <summary>Returns the componentwise hyperbolic cosine of a float4 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise hyperbolic cosine of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 cosh(float4 x) { return float4(cosh(x.x), cosh(x.y), cosh(x.z), cosh(x.w)); }


        /// <summary>Returns the arccosine of a number value.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The arccosine of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number acos(number x) { return number.Acos(x); }

        /// <summary>Returns the componentwise arccosine of a float2 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise arccosine of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 acos(float2 x) { return float2(acos(x.x), acos(x.y)); }

        /// <summary>Returns the componentwise arccosine of a float3 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise arccosine of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 acos(float3 x) { return float3(acos(x.x), acos(x.y), acos(x.z)); }

        /// <summary>Returns the componentwise arccosine of a float4 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise arccosine of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 acos(float4 x) { return float4(acos(x.x), acos(x.y), acos(x.z), acos(x.w)); }


        /// <summary>Returns the sine of a number value.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The sine of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number sin(number x) { return number.Sin(x); }

        /// <summary>Returns the componentwise sine of a float2 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise sine of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 sin(float2 x) { return float2(sin(x.x), sin(x.y)); }

        /// <summary>Returns the componentwise sine of a float3 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise sine of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 sin(float3 x) { return float3(sin(x.x), sin(x.y), sin(x.z)); }

        /// <summary>Returns the componentwise sine of a float4 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise sine of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 sin(float4 x) { return float4(sin(x.x), sin(x.y), sin(x.z), sin(x.w)); }


        /// <summary>Returns the hyperbolic sine of a number value.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The hyperbolic sine of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number sinh(number x) { return number.Sinh(x); }

        /// <summary>Returns the componentwise hyperbolic sine of a float2 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise hyperbolic sine of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 sinh(float2 x) { return new float2(sinh(x.x), sinh(x.y)); }

        /// <summary>Returns the componentwise hyperbolic sine of a float3 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise hyperbolic sine of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 sinh(float3 x) { return new float3(sinh(x.x), sinh(x.y), sinh(x.z)); }

        /// <summary>Returns the componentwise hyperbolic sine of a float4 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise hyperbolic sine of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 sinh(float4 x) { return new float4(sinh(x.x), sinh(x.y), sinh(x.z), sinh(x.w)); }


        /// <summary>Returns the arcsine of a number value.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The arcsine of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number asin(number x) { return number.Asin(x); }

        /// <summary>Returns the componentwise arcsine of a float2 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise arcsine of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 asin(float2 x) { return float2(asin(x.x), asin(x.y)); }

        /// <summary>Returns the componentwise arcsine of a float3 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise arcsine of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 asin(float3 x) { return float3(asin(x.x), asin(x.y), asin(x.z)); }

        /// <summary>Returns the componentwise arcsine of a float4 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise arcsine of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 asin(float4 x) { return float4(asin(x.x), asin(x.y), asin(x.z), asin(x.w)); }


        /// <summary>Returns the result of rounding a number value up to the nearest integral value less or equal to the original value.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The round down to nearest integral value of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number floor(number x) { return number.Floor(x); }

        /// <summary>Returns the result of rounding each component of a float2 vector value down to the nearest value less or equal to the original value.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise round down to nearest integral value of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 floor(float2 x) { return float2(floor(x.x), floor(x.y)); }

        /// <summary>Returns the result of rounding each component of a float3 vector value down to the nearest value less or equal to the original value.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise round down to nearest integral value of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 floor(float3 x) { return float3(floor(x.x), floor(x.y), floor(x.z)); }

        /// <summary>Returns the result of rounding each component of a float4 vector value down to the nearest value less or equal to the original value.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise round down to nearest integral value of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 floor(float4 x) { return float4(floor(x.x), floor(x.y), floor(x.z), floor(x.w)); }


        /// <summary>Returns the result of rounding a number value up to the nearest integral value greater or equal to the original value.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The round up to nearest integral value of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number ceil(number x) { return number.Ceiling(x); }

        /// <summary>Returns the result of rounding each component of a float2 vector value up to the nearest value greater or equal to the original value.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise round up to nearest integral value of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 ceil(float2 x) { return float2(ceil(x.x), ceil(x.y)); }

        /// <summary>Returns the result of rounding each component of a float3 vector value up to the nearest value greater or equal to the original value.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise round up to nearest integral value of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 ceil(float3 x) { return float3(ceil(x.x), ceil(x.y), ceil(x.z)); }

        /// <summary>Returns the result of rounding each component of a float4 vector value up to the nearest value greater or equal to the original value.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise round up to nearest integral value of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 ceil(float4 x) { return float4(ceil(x.x), ceil(x.y), ceil(x.z), ceil(x.w)); }


        /// <summary>Returns the result of rounding a number value to the nearest integral value.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The round to nearest integral value of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number round(number x) { return number.Round(x); }

        /// <summary>Returns the result of rounding each component of a float2 vector value to the nearest integral value.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise round to nearest integral value of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 round(float2 x) { return float2(round(x.x), round(x.y)); }

        /// <summary>Returns the result of rounding each component of a float3 vector value to the nearest integral value.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise round to nearest integral value of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 round(float3 x) { return float3(round(x.x), round(x.y), round(x.z)); }

        /// <summary>Returns the result of rounding each component of a float4 vector value to the nearest integral value.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise round to nearest integral value of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 round(float4 x) { return float4(round(x.x), round(x.y), round(x.z), round(x.w)); }


        /// <summary>Returns the result of truncating a number value to an integral number value.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The truncation of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number trunc(number x) { return number.Truncate(x); }

        /// <summary>Returns the result of a componentwise truncation of a float2 value to an integral float2 value.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise truncation of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 trunc(float2 x) { return float2(trunc(x.x), trunc(x.y)); }

        /// <summary>Returns the result of a componentwise truncation of a float3 value to an integral float3 value.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise truncation of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 trunc(float3 x) { return float3(trunc(x.x), trunc(x.y), trunc(x.z)); }

        /// <summary>Returns the result of a componentwise truncation of a float4 value to an integral float4 value.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise truncation of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 trunc(float4 x) { return float4(trunc(x.x), trunc(x.y), trunc(x.z), trunc(x.w)); }


        /// <summary>Returns the fractional part of a number value.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The fractional part of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number frac(number x) { return x - floor(x); }

        /// <summary>Returns the componentwise fractional parts of a float2 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise fractional part of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 frac(float2 x) { return x - floor(x); }

        /// <summary>Returns the componentwise fractional parts of a float3 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise fractional part of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 frac(float3 x) { return x - floor(x); }

        /// <summary>Returns the componentwise fractional parts of a float4 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise fractional part of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 frac(float4 x) { return x - floor(x); }


        /// <summary>Returns the reciprocal a number value.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The reciprocal of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number rcp(number x) { return number.one / x; }

        /// <summary>Returns the componentwise reciprocal a float2 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise reciprocal of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 rcp(float2 x) { return number.one / x; }

        /// <summary>Returns the componentwise reciprocal a float3 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise reciprocal of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 rcp(float3 x) { return number.one / x; }

        /// <summary>Returns the componentwise reciprocal a float4 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise reciprocal of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 rcp(float4 x) { return number.one / x; }


        /// <summary>Returns the sign of a number value. -1.0f if it is less than zero, 0.0f if it is zero and 1.0f if it greater than zero.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The sign of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number sign(number x) { return (number)number.Sign(x); }

        /// <summary>Returns the componentwise sign of a float2 value. 1.0f for positive components, 0.0f for zero components and -1.0f for negative components.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise sign of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 sign(float2 x) { return new float2(sign(x.x), sign(x.y)); }

        /// <summary>Returns the componentwise sign of a float3 value. 1.0f for positive components, 0.0f for zero components and -1.0f for negative components.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise sign of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 sign(float3 x) { return new float3(sign(x.x), sign(x.y), sign(x.z)); }

        /// <summary>Returns the componentwise sign of a float4 value. 1.0f for positive components, 0.0f for zero components and -1.0f for negative components.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise sign of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 sign(float4 x) { return new float4(sign(x.x), sign(x.y), sign(x.z), sign(x.w)); }


        /// <summary>Returns x raised to the power y.</summary>
        /// <param name="x">The exponent base.</param>
        /// <param name="y">The exponent power.</param>
        /// <returns>The result of raising x to the power y.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number pow(number x, number y) { return number.Pow(x, y); }

        /// <summary>Returns the componentwise result of raising x to the power y.</summary>
        /// <param name="x">The exponent base.</param>
        /// <param name="y">The exponent power.</param>
        /// <returns>The componentwise result of raising x to the power y.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 pow(float2 x, float2 y) { return new float2(pow(x.x, y.x), pow(x.y, y.y)); }

        /// <summary>Returns the componentwise result of raising x to the power y.</summary>
        /// <param name="x">The exponent base.</param>
        /// <param name="y">The exponent power.</param>
        /// <returns>The componentwise result of raising x to the power y.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 pow(float3 x, float3 y) { return new float3(pow(x.x, y.x), pow(x.y, y.y), pow(x.z, y.z)); }

        /// <summary>Returns the componentwise result of raising x to the power y.</summary>
        /// <param name="x">The exponent base.</param>
        /// <param name="y">The exponent power.</param>
        /// <returns>The componentwise result of raising x to the power y.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 pow(float4 x, float4 y) { return new float4(pow(x.x, y.x), pow(x.y, y.y), pow(x.z, y.z), pow(x.w, y.w)); }


        /// <summary>Returns the base-e exponential of x.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The base-e exponential of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number exp(number x) { return number.Exp(x); }

        /// <summary>Returns the componentwise base-e exponential of x.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise base-e exponential of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 exp(float2 x) { return new float2(exp(x.x), exp(x.y)); }

        /// <summary>Returns the componentwise base-e exponential of x.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise base-e exponential of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 exp(float3 x) { return new float3(exp(x.x), exp(x.y), exp(x.z)); }

        /// <summary>Returns the componentwise base-e exponential of x.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise base-e exponential of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 exp(float4 x) { return new float4(exp(x.x), exp(x.y), exp(x.z), exp(x.w)); }


        /// <summary>Returns the base-2 exponential of x.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The base-2 exponential of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number exp2(number x) { return number.Pow((number)2, x); }

        /// <summary>Returns the componentwise base-2 exponential of x.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise base-2 exponential of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 exp2(float2 x) { return new float2(exp2(x.x), exp2(x.y)); }

        /// <summary>Returns the componentwise base-2 exponential of x.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise base-2 exponential of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 exp2(float3 x) { return new float3(exp2(x.x), exp2(x.y), exp2(x.z)); }

        /// <summary>Returns the componentwise base-2 exponential of x.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise base-2 exponential of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 exp2(float4 x) { return new float4(exp2(x.x), exp2(x.y), exp2(x.z), exp2(x.w)); }


        /// <summary>Returns the base-10 exponential of x.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The base-10 exponential of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number exp10(number x) { return number.Pow((number)10, x); }

        /// <summary>Returns the componentwise base-10 exponential of x.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise base-10 exponential of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 exp10(float2 x) { return new float2(exp10(x.x), exp10(x.y)); }

        /// <summary>Returns the componentwise base-10 exponential of x.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise base-10 exponential of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 exp10(float3 x) { return new float3(exp10(x.x), exp10(x.y), exp10(x.z)); }

        /// <summary>Returns the componentwise base-10 exponential of x.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise base-10 exponential of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 exp10(float4 x) { return new float4(exp10(x.x), exp10(x.y), exp10(x.z), exp10(x.w)); }


        /// <summary>Returns the natural logarithm of a number value.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The natural logarithm of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number log(number x) { return number.Log(x); }

        /// <summary>Returns the componentwise natural logarithm of a float2 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise natural logarithm of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 log(float2 x) { return new float2(log(x.x), log(x.y)); }

        /// <summary>Returns the componentwise natural logarithm of a float3 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise natural logarithm of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 log(float3 x) { return new float3(log(x.x), log(x.y), log(x.z)); }

        /// <summary>Returns the componentwise natural logarithm of a float4 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise natural logarithm of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 log(float4 x) { return new float4(log(x.x), log(x.y), log(x.z), log(x.w)); }


        /// <summary>Returns the base-2 logarithm of a number value.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The base-2 logarithm of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number log2(number x) { return number.Log2(x); }

        /// <summary>Returns the componentwise base-2 logarithm of a float2 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise base-2 logarithm of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 log2(float2 x) { return new float2(log2(x.x), log2(x.y)); }

        /// <summary>Returns the componentwise base-2 logarithm of a float3 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise base-2 logarithm of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 log2(float3 x) { return new float3(log2(x.x), log2(x.y), log2(x.z)); }

        /// <summary>Returns the componentwise base-2 logarithm of a float4 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise base-2 logarithm of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 log2(float4 x) { return new float4(log2(x.x), log2(x.y), log2(x.z), log2(x.w)); }

        /// <summary>Returns the base-10 logarithm of a number value.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The base-10 logarithm of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number log10(number x) { return number.Log10(x); }

        /// <summary>Returns the componentwise base-10 logarithm of a float2 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise base-10 logarithm of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 log10(float2 x) { return new float2(log10(x.x), log10(x.y)); }

        /// <summary>Returns the componentwise base-10 logarithm of a float3 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise base-10 logarithm of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 log10(float3 x) { return new float3(log10(x.x), log10(x.y), log10(x.z)); }

        /// <summary>Returns the componentwise base-10 logarithm of a float4 vector.</summary>
        /// <param name="x">Input value.</param>
        /// <returns>The componentwise base-10 logarithm of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 log10(float4 x) { return new float4(log10(x.x), log10(x.y), log10(x.z), log10(x.w)); }


        /// <summary>Returns the fix point remainder of x/y.</summary>
        /// <param name="x">The dividend in x/y.</param>
        /// <param name="y">The divisor in x/y.</param>
        /// <returns>The remainder of x/y.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number fmod(number x, number y) { return x % y; }

        /// <summary>Returns the componentwise fix point remainder of x/y.</summary>
        /// <param name="x">The dividend in x/y.</param>
        /// <param name="y">The divisor in x/y.</param>
        /// <returns>The componentwise remainder of x/y.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 fmod(float2 x, float2 y) { return new float2(x.x % y.x, x.y % y.y); }

        /// <summary>Returns the componentwise fix point remainder of x/y.</summary>
        /// <param name="x">The dividend in x/y.</param>
        /// <param name="y">The divisor in x/y.</param>
        /// <returns>The componentwise remainder of x/y.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 fmod(float3 x, float3 y) { return new float3(x.x % y.x, x.y % y.y, x.z % y.z); }

        /// <summary>Returns the componentwise fix point remainder of x/y.</summary>
        /// <param name="x">The dividend in x/y.</param>
        /// <param name="y">The divisor in x/y.</param>
        /// <returns>The componentwise remainder of x/y.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 fmod(float4 x, float4 y) { return new float4(x.x % y.x, x.y % y.y, x.z % y.z, x.w % y.w); }


        /// <summary>Splits a number value into an integral part i and a fractional part that gets returned. Both parts take the sign of the input.</summary>
        /// <param name="x">Value to split into integral and fractional part.</param>
        /// <param name="i">Output value containing integral part of x.</param>
        /// <returns>The fractional part of x.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number modf(number x, out number i) { i = trunc(x); return x - i; }

        /// <summary>
        /// Performs a componentwise split of a float2 vector into an integral part i and a fractional part that gets returned.
        /// Both parts take the sign of the corresponding input component.
        /// </summary>
        /// <param name="x">Value to split into integral and fractional part.</param>
        /// <param name="i">Output value containing integral part of x.</param>
        /// <returns>The componentwise fractional part of x.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 modf(float2 x, out float2 i) { i = trunc(x); return x - i; }

        /// <summary>
        /// Performs a componentwise split of a float3 vector into an integral part i and a fractional part that gets returned.
        /// Both parts take the sign of the corresponding input component.
        /// </summary>
        /// <param name="x">Value to split into integral and fractional part.</param>
        /// <param name="i">Output value containing integral part of x.</param>
        /// <returns>The componentwise fractional part of x.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 modf(float3 x, out float3 i) { i = trunc(x); return x - i; }

        /// <summary>
        /// Performs a componentwise split of a float4 vector into an integral part i and a fractional part that gets returned.
        /// Both parts take the sign of the corresponding input component.
        /// </summary>
        /// <param name="x">Value to split into integral and fractional part.</param>
        /// <param name="i">Output value containing integral part of x.</param>
        /// <returns>The componentwise fractional part of x.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 modf(float4 x, out float4 i) { i = trunc(x); return x - i; }


        /// <summary>Returns the square root of a number value.</summary>
        /// <param name="x">Value to use when computing square root.</param>
        /// <returns>The square root.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number sqrt(number x) { return number.Sqrt(x); }

        /// <summary>Returns the componentwise square root of a float2 vector.</summary>
        /// <param name="x">Value to use when computing square root.</param>
        /// <returns>The componentwise square root.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 sqrt(float2 x) { return new float2(sqrt(x.x), sqrt(x.y)); }

        /// <summary>Returns the componentwise square root of a float3 vector.</summary>
        /// <param name="x">Value to use when computing square root.</param>
        /// <returns>The componentwise square root.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 sqrt(float3 x) { return new float3(sqrt(x.x), sqrt(x.y), sqrt(x.z)); }

        /// <summary>Returns the componentwise square root of a float4 vector.</summary>
        /// <param name="x">Value to use when computing square root.</param>
        /// <returns>The componentwise square root.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 sqrt(float4 x) { return new float4(sqrt(x.x), sqrt(x.y), sqrt(x.z), sqrt(x.w)); }


        /// <summary>Returns the reciprocal square root of a number value.</summary>
        /// <param name="x">Value to use when computing reciprocal square root.</param>
        /// <returns>The reciprocal square root.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number rsqrt(number x) { return number.one / sqrt(x); }

        /// <summary>Returns the componentwise reciprocal square root of a float2 vector.</summary>
        /// <param name="x">Value to use when computing reciprocal square root.</param>
        /// <returns>The componentwise reciprocal square root.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 rsqrt(float2 x) { return number.one / sqrt(x); }

        /// <summary>Returns the componentwise reciprocal square root of a float3 vector.</summary>
        /// <param name="x">Value to use when computing reciprocal square root.</param>
        /// <returns>The componentwise reciprocal square root.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 rsqrt(float3 x) { return number.one / sqrt(x); }

        /// <summary>Returns the componentwise reciprocal square root of a float4 vector</summary>
        /// <param name="x">Value to use when computing reciprocal square root.</param>
        /// <returns>The componentwise reciprocal square root.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 rsqrt(float4 x) { return number.one / sqrt(x); }


        /// <summary>Returns a normalized version of the float2 vector x by scaling it by 1 / length(x).</summary>
        /// <param name="x">Vector to normalize.</param>
        /// <returns>The normalized vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 normalize(float2 x)// { return rsqrt(dot(x, x)) * x; }
        {
            if (x.Equals(Nt.Deterministics.float2.zero)) return x;
            bool isnegX = x.x.RawValue < 0L, isnegY = x.y.RawValue < 0L;
            long xRawValue = isnegX ? -x.x.RawValue : x.x.RawValue;
            long yRawValue = isnegY ? -x.y.RawValue : x.y.RawValue;
            bool bHighArea = xRawValue < yRawValue;//true:(Pi/4, Pi/2] or false:[0, Pi/4]
            long idx = bHighArea ? xRawValue : yRawValue;
            xRawValue = bHighArea ? yRawValue : xRawValue;
            yRawValue = idx;
            idx = (yRawValue << number.FRACTIONAL_PLACES) / xRawValue;
#if !NO_NUMBER_SUPPORT_BURST
            xRawValue = bHighArea ? NumberLut.normalize2_y_lut.Data[(int)idx] : NumberLut.normalize2_x_lut.Data[(int)idx];
            yRawValue = bHighArea ? NumberLut.normalize2_x_lut.Data[(int)idx] : NumberLut.normalize2_y_lut.Data[(int)idx];
#else
            xRawValue = bHighArea ? NumberLut.normalize2_y_lut[idx] : NumberLut.normalize2_x_lut[idx];
            yRawValue = bHighArea ? NumberLut.normalize2_x_lut[idx] : NumberLut.normalize2_y_lut[idx];
#endif
            x.x.RawValue = isnegX ? -xRawValue : xRawValue;
            x.y.RawValue = isnegY ? -yRawValue : yRawValue;
            //bool isnegX = false, isnegY = false;
            //if (x.x.RawValue < 0L) { isnegX = true; x.x.RawValue = -x.x.RawValue; }
            //if (x.y.RawValue < 0L) { isnegY = true; x.y.RawValue = -x.y.RawValue; }
            //long idx;
            //if (x.y.RawValue > x.x.RawValue)//(Pi/4, Pi/2]
            //{
            //    idx = (x.x.RawValue << number.FRACTIONAL_PLACES) / x.y.RawValue;
            //    x.x.RawValue = isnegX ? -NumberLut.normalize2_y_lut[idx] : NumberLut.normalize2_y_lut[idx];
            //    x.y.RawValue = isnegY ? -NumberLut.normalize2_x_lut[idx] : NumberLut.normalize2_x_lut[idx];
            //}
            //else//[0, Pi/4]
            //{
            //    idx = (x.y.RawValue << number.FRACTIONAL_PLACES) / x.x.RawValue;
            //    x.x.RawValue = isnegX ? -NumberLut.normalize2_x_lut[idx] : NumberLut.normalize2_x_lut[idx];
            //    x.y.RawValue = isnegY ? -NumberLut.normalize2_y_lut[idx] : NumberLut.normalize2_y_lut[idx];
            //}
            return x;
        }

        /// <summary>Returns a normalized version of the float3 vector x by scaling it by 1 / length(x).</summary>
        /// <param name="x">Vector to normalize.</param>
        /// <returns>The normalized vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 normalize(float3 x) { return x.Equals(Nt.Deterministics.float3.zero) ? x : rsqrt(dot(x, x)) * x; }

        /// <summary>Returns a normalized version of the float4 vector x by scaling it by 1 / length(x).</summary>
        /// <param name="x">Vector to normalize.</param>
        /// <returns>The normalized vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 normalize(float4 x) { return x.Equals(Nt.Deterministics.float4.zero) ? x : rsqrt(dot(x, x)) * x; }


        /// <summary>
        /// Returns a safe normalized version of the float2 vector x by scaling it by 1 / length(x).
        /// Returns the given default value when 1 / length(x) does not produce a finite number.
        /// </summary>
        /// <param name="x">Vector to normalize.</param>
        /// <param name="defaultvalue">Vector to return if normalized vector is not finite.</param>
        /// <returns>The normalized vector or the default value if the normalized vector is not finite.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public float2 normalizesafe(float2 x, float2 defaultvalue = new float2())
        {
            //number len = math.dot(x, x);
            //return math.select(defaultvalue, x * math.rsqrt(len), len > number.MinNormal);
            if (x.Equals(Nt.Deterministics.float2.zero)) return x;
            bool isnegX = x.x.RawValue < 0L, isnegY = x.y.RawValue < 0L;
            long xRawValue = isnegX ? -x.x.RawValue : x.x.RawValue;
            long yRawValue = isnegY ? -x.y.RawValue : x.y.RawValue;
            bool bHighArea = xRawValue < yRawValue;//true:(Pi/4, Pi/2] or false:[0, Pi/4]
            long idx = bHighArea ? xRawValue : yRawValue;
            xRawValue = bHighArea ? yRawValue : xRawValue;
            yRawValue = idx;
            idx = (yRawValue << number.FRACTIONAL_PLACES) / xRawValue;
#if !NO_NUMBER_SUPPORT_BURST
            xRawValue = bHighArea ? NumberLut.normalize2_y_lut.Data[(int)idx] : NumberLut.normalize2_x_lut.Data[(int)idx];
            yRawValue = bHighArea ? NumberLut.normalize2_x_lut.Data[(int)idx] : NumberLut.normalize2_y_lut.Data[(int)idx];
#else
            xRawValue = bHighArea ? NumberLut.normalize2_y_lut[idx] : NumberLut.normalize2_x_lut[idx];
            yRawValue = bHighArea ? NumberLut.normalize2_x_lut[idx] : NumberLut.normalize2_y_lut[idx];
#endif
            x.x.RawValue = isnegX ? -xRawValue : xRawValue;
            x.y.RawValue = isnegY ? -yRawValue : yRawValue;
            return x;
        }

        /// <summary>
        /// Returns a safe normalized version of the float3 vector x by scaling it by 1 / length(x).
        /// Returns the given default value when 1 / length(x) does not produce a finite number.
        /// </summary>
        /// <param name="x">Vector to normalize.</param>
        /// <param name="defaultvalue">Vector to return if normalized vector is not finite.</param>
        /// <returns>The normalized vector or the default value if the normalized vector is not finite.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public float3 normalizesafe(float3 x, float3 defaultvalue = new float3())
        {
            number len = math.dot(x, x);
            return len > number.MinNormal ? x * math.rsqrt(len) : defaultvalue;
        }

        /// <summary>
        /// Returns a safe normalized version of the float4 vector x by scaling it by 1 / length(x).
        /// Returns the given default value when 1 / length(x) does not produce a finite number.
        /// </summary>
        /// <param name="x">Vector to normalize.</param>
        /// <param name="defaultvalue">Vector to return if normalized vector is not finite.</param>
        /// <returns>The normalized vector or the default value if the normalized vector is not finite.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public float4 normalizesafe(float4 x, float4 defaultvalue = new float4())
        {
            number len = math.dot(x, x);
            return len > number.MinNormal ? x * math.rsqrt(len) : defaultvalue;
        }


        /// <summary>Returns the length of a number value. Equivalent to the absolute value.</summary>
        /// <param name="x">Value to use when computing length.</param>
        /// <returns>Length of x.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number length(number x) { return abs(x); }

        /// <summary>Returns the length of a float2 vector.</summary>
        /// <param name="x">Vector to use when computing length.</param>
        /// <returns>Length of vector x.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number length(float2 x) { return sqrt(dot(x, x)); }
        //{
        //    long _dot = (x.x.RawValue * x.x.RawValue >> number.FRACTIONAL_PLACES) + (x.y.RawValue * x.y.RawValue >> number.FRACTIONAL_PLACES);
        //    x.x.RawValue = number.SqrtRaw(_dot);
        //    return x.x;
        //}
        //50MOps/s
        //{
        //    bool isnegX = x.x.RawValue < 0L, isnegY = x.y.RawValue < 0L;
        //    long xRawValue = isnegX ? -x.x.RawValue : x.x.RawValue;
        //    long yRawValue = isnegY ? -x.y.RawValue : x.y.RawValue;
        //    bool bHighArea = xRawValue < yRawValue;//true:(Pi/4, Pi/2] or false:[0, Pi/4]
        //    long idx = bHighArea ? xRawValue : yRawValue;
        //    xRawValue = bHighArea ? yRawValue : xRawValue;
        //    yRawValue = idx;
        //    idx = (yRawValue << number.FRACTIONAL_PLACES) / xRawValue;
        //    yRawValue = NumberLut.normalize2_x_lut[idx];
        //    x.x.RawValue = (xRawValue << number.FRACTIONAL_PLACES) / yRawValue;
        //    return x.x;
        //}

        /// <summary>Returns the length of a float3 vector.</summary>
        /// <param name="x">Vector to use when computing length.</param>
        /// <returns>Length of vector x.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number length(float3 x) { return sqrt(dot(x, x)); }

        /// <summary>Returns the length of a float4 vector.</summary>
        /// <param name="x">Vector to use when computing length.</param>
        /// <returns>Length of vector x.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number length(float4 x) { return sqrt(dot(x, x)); }


        /// <summary>Returns the squared length of a number value. Equivalent to squaring the value.</summary>
        /// <param name="x">Value to use when computing squared length.</param>
        /// <returns>Squared length of x.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number lengthsq(number x) { return x * x; }

        /// <summary>Returns the squared length of a float2 vector.</summary>
        /// <param name="x">Vector to use when computing squared length.</param>
        /// <returns>Squared length of vector x.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number lengthsq(float2 x) { return dot(x, x); }

        /// <summary>Returns the squared length of a float3 vector.</summary>
        /// <param name="x">Vector to use when computing squared length.</param>
        /// <returns>Squared length of vector x.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number lengthsq(float3 x) { return dot(x, x); }

        /// <summary>Returns the squared length of a float4 vector.</summary>
        /// <param name="x">Vector to use when computing squared length.</param>
        /// <returns>Squared length of vector x.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number lengthsq(float4 x) { return dot(x, x); }


        /// <summary>Returns the distance between two number values.</summary>
        /// <param name="x">First value to use in distance computation.</param>
        /// <param name="y">Second value to use in distance computation.</param>
        /// <returns>The distance between x and y.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number distance(number x, number y) { return abs(y - x); }

        /// <summary>Returns the distance between two float2 vectors.</summary>
        /// <param name="x">First vector to use in distance computation.</param>
        /// <param name="y">Second vector to use in distance computation.</param>
        /// <returns>The distance between x and y.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number distance(float2 x, float2 y) { return length(y - x); }

        /// <summary>Returns the distance between two float3 vectors.</summary>
        /// <param name="x">First vector to use in distance computation.</param>
        /// <param name="y">Second vector to use in distance computation.</param>
        /// <returns>The distance between x and y.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number distance(float3 x, float3 y) { return length(y - x); }

        /// <summary>Returns the distance between two float4 vectors.</summary>
        /// <param name="x">First vector to use in distance computation.</param>
        /// <param name="y">Second vector to use in distance computation.</param>
        /// <returns>The distance between x and y.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number distance(float4 x, float4 y) { return length(y - x); }


        /// <summary>Returns the squared distance between two number values.</summary>
        /// <param name="x">First value to use in distance computation.</param>
        /// <param name="y">Second value to use in distance computation.</param>
        /// <returns>The squared distance between x and y.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number distancesq(number x, number y) { return (y - x) * (y - x); }

        /// <summary>Returns the squared distance between two float2 vectors.</summary>
        /// <param name="x">First vector to use in distance computation.</param>
        /// <param name="y">Second vector to use in distance computation.</param>
        /// <returns>The squared distance between x and y.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number distancesq(float2 x, float2 y) { return lengthsq(y - x); }

        /// <summary>Returns the squared distance between two float3 vectors.</summary>
        /// <param name="x">First vector to use in distance computation.</param>
        /// <param name="y">Second vector to use in distance computation.</param>
        /// <returns>The squared distance between x and y.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number distancesq(float3 x, float3 y) { return lengthsq(y - x); }

        /// <summary>Returns the squared distance between two float4 vectors.</summary>
        /// <param name="x">First vector to use in distance computation.</param>
        /// <param name="y">Second vector to use in distance computation.</param>
        /// <returns>The squared distance between x and y.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number distancesq(float4 x, float4 y) { return lengthsq(y - x); }


        /// <summary>Returns the cross product of two float3 vectors.</summary>
        /// <param name="x">First vector to use in cross product.</param>
        /// <param name="y">Second vector to use in cross product.</param>
        /// <returns>The cross product of x and y.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 cross(float3 x, float3 y) { return (x * y.yzx - x.yzx * y).yzx; }


        /// <summary>Returns a smooth Hermite interpolation between 0.0f and 1.0f when x is in [a, b].</summary>
        /// <param name="a">The minimum range of the x parameter.</param>
        /// <param name="b">The maximum range of the x parameter.</param>
        /// <param name="x">The value to be interpolated.</param>
        /// <returns>Returns a value camped to the range [0, 1].</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number smoothstep(number a, number b, number x)
        {
            var t = saturate((x - a) / (b - a));
            return t * t * (3.0f - (2.0f * t));
        }

        /// <summary>Returns a componentwise smooth Hermite interpolation between 0.0f and 1.0f when x is in [a, b].</summary>
        /// <param name="a">The minimum range of the x parameter.</param>
        /// <param name="b">The maximum range of the x parameter.</param>
        /// <param name="x">The value to be interpolated.</param>
        /// <returns>Returns component values camped to the range [0, 1].</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 smoothstep(float2 a, float2 b, float2 x)
        {
            var t = saturate((x - a) / (b - a));
            return t * t * ((number)3.0f - ((number)2.0f * t));
        }

        /// <summary>Returns a componentwise smooth Hermite interpolation between 0.0f and 1.0f when x is in [a, b].</summary>
        /// <param name="a">The minimum range of the x parameter.</param>
        /// <param name="b">The maximum range of the x parameter.</param>
        /// <param name="x">The value to be interpolated.</param>
        /// <returns>Returns component values camped to the range [0, 1].</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 smoothstep(float3 a, float3 b, float3 x)
        {
            var t = saturate((x - a) / (b - a));
            return t * t * ((number)3.0f - ((number)2.0f * t));
        }

        /// <summary>Returns a componentwise smooth Hermite interpolation between 0.0f and 1.0f when x is in [a, b].</summary>
        /// <param name="a">The minimum range of the x parameter.</param>
        /// <param name="b">The maximum range of the x parameter.</param>
        /// <param name="x">The value to be interpolated.</param>
        /// <returns>Returns component values camped to the range [0, 1].</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 smoothstep(float4 a, float4 b, float4 x)
        {
            var t = saturate((x - a) / (b - a));
            return t * t * ((number)3.0f - ((number)2.0f * t));
        }


        /// <summary>Returns true if any component of the input float2 vector is non-zero, false otherwise.</summary>
        /// <param name="x">Vector of values to compare.</param>
        /// <returns>True if any the components of x are non-zero, false otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool any(float2 x) { return x.x != 0.0f || x.y != 0.0f; }

        /// <summary>Returns true if any component of the input float3 vector is non-zero, false otherwise.</summary>
        /// <param name="x">Vector of values to compare.</param>
        /// <returns>True if any the components of x are non-zero, false otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool any(float3 x) { return x.x != 0.0f || x.y != 0.0f || x.z != 0.0f; }

        /// <summary>Returns true if any component of the input float4 vector is non-zero, false otherwise.</summary>
        /// <param name="x">Vector of values to compare.</param>
        /// <returns>True if any the components of x are non-zero, false otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool any(float4 x) { return x.x != 0.0f || x.y != 0.0f || x.z != 0.0f || x.w != 0.0f; }


        /// <summary>Returns true if any component of the input float2 vector is NaN, false otherwise.</summary>
        /// <param name="x">Vector of values to compare.</param>
        /// <returns>True if any the components of x are NaN, false otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool anyNaN(float2 x) { return number.IsNaN(x.x) || number.IsNaN(x.y); }

        /// <summary>Returns true if any component of the input float3 vector is NaN, false otherwise.</summary>
        /// <param name="x">Vector of values to compare.</param>
        /// <returns>True if any the components of x are NaN, false otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool anyNaN(float3 x) { return number.IsNaN(x.x) || number.IsNaN(x.y) || number.IsNaN(x.z); }

        /// <summary>Returns true if any component of the input float4 vector is NaN, false otherwise.</summary>
        /// <param name="x">Vector of values to compare.</param>
        /// <returns>True if any the components of x are NaN, false otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool anyNaN(float4 x) { return number.IsNaN(x.x) || number.IsNaN(x.y) || number.IsNaN(x.z) || number.IsNaN(x.w); }


        /// <summary>Returns true if all components of the input float2 vector are non-zero, false otherwise.</summary>
        /// <param name="x">Vector of values to compare.</param>
        /// <returns>True if all the components of x are non-zero, false otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool all(float2 x) { return x.x != 0.0f && x.y != 0.0f; }

        /// <summary>Returns true if all components of the input float3 vector are non-zero, false otherwise.</summary>
        /// <param name="x">Vector of values to compare.</param>
        /// <returns>True if all the components of x are non-zero, false otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool all(float3 x) { return x.x != 0.0f && x.y != 0.0f && x.z != 0.0f; }

        /// <summary>Returns true if all components of the input float4 vector are non-zero, false otherwise.</summary>
        /// <param name="x">Vector of values to compare.</param>
        /// <returns>True if all the components of x are non-zero, false otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool all(float4 x) { return x.x != 0.0f && x.y != 0.0f && x.z != 0.0f && x.w != 0.0f; }


        /// <summary>Returns b if c is true, a otherwise.</summary>
        /// <param name="a">Value to use if c is false.</param>
        /// <param name="b">Value to use if c is true.</param>
        /// <param name="c">Bool value to choose between a and b.</param>
        /// <returns>The selection between a and b according to bool c.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number select(number a, number b, bool c) { return c ? b : a; }

        /// <summary>Returns b if c is true, a otherwise.</summary>
        /// <param name="a">Value to use if c is false.</param>
        /// <param name="b">Value to use if c is true.</param>
        /// <param name="c">Bool value to choose between a and b.</param>
        /// <returns>The selection between a and b according to bool c.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 select(float2 a, float2 b, bool c) { return c ? b : a; }

        /// <summary>Returns b if c is true, a otherwise.</summary>
        /// <param name="a">Value to use if c is false.</param>
        /// <param name="b">Value to use if c is true.</param>
        /// <param name="c">Bool value to choose between a and b.</param>
        /// <returns>The selection between a and b according to bool c.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 select(float3 a, float3 b, bool c) { return c ? b : a; }

        /// <summary>Returns b if c is true, a otherwise.</summary>
        /// <param name="a">Value to use if c is false.</param>
        /// <param name="b">Value to use if c is true.</param>
        /// <param name="c">Bool value to choose between a and b.</param>
        /// <returns>The selection between a and b according to bool c.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 select(float4 a, float4 b, bool c) { return c ? b : a; }


        /// <summary>
        /// Returns a componentwise selection between two double4 vectors a and b based on a bool4 selection mask c.
        /// Per component, the component from b is selected when c is true, otherwise the component from a is selected.
        /// </summary>
        /// <param name="a">Values to use if c is false.</param>
        /// <param name="b">Values to use if c is true.</param>
        /// <param name="c">Selection mask to choose between a and b.</param>
        /// <returns>The componentwise selection between a and b according to selection mask c.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 select(float2 a, float2 b, bool2 c) { return new float2(c.x ? b.x : a.x, c.y ? b.y : a.y); }

        /// <summary>
        /// Returns a componentwise selection between two double4 vectors a and b based on a bool4 selection mask c.
        /// Per component, the component from b is selected when c is true, otherwise the component from a is selected.
        /// </summary>
        /// <param name="a">Values to use if c is false.</param>
        /// <param name="b">Values to use if c is true.</param>
        /// <param name="c">Selection mask to choose between a and b.</param>
        /// <returns>The componentwise selection between a and b according to selection mask c.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 select(float3 a, float3 b, bool3 c) { return new float3(c.x ? b.x : a.x, c.y ? b.y : a.y, c.z ? b.z : a.z); }

        /// <summary>
        /// Returns a componentwise selection between two double4 vectors a and b based on a bool4 selection mask c.
        /// Per component, the component from b is selected when c is true, otherwise the component from a is selected.
        /// </summary>
        /// <param name="a">Values to use if c is false.</param>
        /// <param name="b">Values to use if c is true.</param>
        /// <param name="c">Selection mask to choose between a and b.</param>
        /// <returns>The componentwise selection between a and b according to selection mask c.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 select(float4 a, float4 b, bool4 c) { return new float4(c.x ? b.x : a.x, c.y ? b.y : a.y, c.z ? b.z : a.z, c.w ? b.w : a.w); }


        /// <summary>Returns the result of a step function where the result is 1.0f when x &gt;= y and 0.0f otherwise.</summary>
        /// <param name="y">Value to be used as a threshold for returning 1.</param>
        /// <param name="x">Value to compare against threshold y.</param>
        /// <returns>1 if the comparison x &gt;= y is true, otherwise 0.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number step(number y, number x) { return select(number.zero, number.one, x >= y); }

        /// <summary>Returns the result of a componentwise step function where each component is 1.0f when x &gt;= y and 0.0f otherwise.</summary>
        /// <param name="y">Vector of values to be used as a threshold for returning 1.</param>
        /// <param name="x">Vector of values to compare against threshold y.</param>
        /// <returns>1 if the componentwise comparison x &gt;= y is true, otherwise 0.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 step(float2 y, float2 x) { return select(float2(0.0f), float2(1.0f), x >= y); }

        /// <summary>Returns the result of a componentwise step function where each component is 1.0f when x &gt;= y and 0.0f otherwise.</summary>
        /// <param name="y">Vector of values to be used as a threshold for returning 1.</param>
        /// <param name="x">Vector of values to compare against threshold y.</param>
        /// <returns>1 if the componentwise comparison x &gt;= y is true, otherwise 0.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 step(float3 y, float3 x) { return select(float3(0.0f), float3(1.0f), x >= y); }

        /// <summary>Returns the result of a componentwise step function where each component is 1.0f when x &gt;= y and 0.0f otherwise.</summary>
        /// <param name="y">Vector of values to be used as a threshold for returning 1.</param>
        /// <param name="x">Vector of values to compare against threshold y.</param>
        /// <returns>1 if the componentwise comparison x &gt;= y is true, otherwise 0.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 step(float4 y, float4 x) { return select(float4(0.0f), float4(1.0f), x >= y); }


        /// <summary>Given an incident vector i and a normal vector n, returns the reflection vector r = i - 2.0f * dot(i, n) * n.</summary>
        /// <param name="i">Incident vector.</param>
        /// <param name="n">Normal vector.</param>
        /// <returns>Reflection vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 reflect(float2 i, float2 n) { return i - (number)2f * n * dot(i, n); }

        /// <summary>Given an incident vector i and a normal vector n, returns the reflection vector r = i - 2.0f * dot(i, n) * n.</summary>
        /// <param name="i">Incident vector.</param>
        /// <param name="n">Normal vector.</param>
        /// <returns>Reflection vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 reflect(float3 i, float3 n) { return i - (number)2f * n * dot(i, n); }

        /// <summary>Given an incident vector i and a normal vector n, returns the reflection vector r = i - 2.0f * dot(i, n) * n.</summary>
        /// <param name="i">Incident vector.</param>
        /// <param name="n">Normal vector.</param>
        /// <returns>Reflection vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 reflect(float4 i, float4 n) { return i - (number)2f * n * dot(i, n); }


        /// <summary>Returns the refraction vector given the incident vector i, the normal vector n and the refraction index eta.</summary>
        /// <param name="i">Incident vector.</param>
        /// <param name="n">Normal vector.</param>
        /// <param name="eta">Index of refraction.</param>
        /// <returns>Refraction vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 refract(float2 i, float2 n, number eta)
        {
            number ni = dot(n, i);
            number k = number.one - eta * eta * (number.one - ni * ni);
            return select(number.zero, eta * i - (eta * ni + sqrt(k)) * n, k >= 0);
        }

        /// <summary>Returns the refraction vector given the incident vector i, the normal vector n and the refraction index eta.</summary>
        /// <param name="i">Incident vector.</param>
        /// <param name="n">Normal vector.</param>
        /// <param name="eta">Index of refraction.</param>
        /// <returns>Refraction vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 refract(float3 i, float3 n, number eta)
        {
            number ni = dot(n, i);
            number k = number.one - eta * eta * (number.one - ni * ni);
            return select(number.zero, eta * i - (eta * ni + sqrt(k)) * n, k >= 0);
        }

        /// <summary>Returns the refraction vector given the incident vector i, the normal vector n and the refraction index eta.</summary>
        /// <param name="i">Incident vector.</param>
        /// <param name="n">Normal vector.</param>
        /// <param name="eta">Index of refraction.</param>
        /// <returns>Refraction vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 refract(float4 i, float4 n, number eta)
        {
            number ni = dot(n, i);
            number k = number.one - eta * eta * (number.one - ni * ni);
            return select(number.zero, eta * i - (eta * ni + sqrt(k)) * n, k >= 0);
        }

        /// <summary>
        /// Compute vector projection of a onto b.
        /// </summary>
        /// <remarks>
        /// Some finite vectors a and b could generate a non-finite result. This is most likely when a's components
        /// are very large (close to Single.MaxValue) or when b's components are very small (close to number.MinNormal).
        /// In these cases, you can call <see cref="projectsafe(Nt.Deterministics.float2,Nt.Deterministics.float2,Nt.Deterministics.float2)"/>
        /// which will use a given default value if the result is not finite.
        /// </remarks>
        /// <param name="a">Vector to project.</param>
        /// <param name="b">Non-zero vector to project onto.</param>
        /// <returns>Vector projection of a onto b.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 project(float2 a, float2 b)
        {
            return (dot(a, b) / dot(b, b)) * b;
        }

        /// <summary>
        /// Compute vector projection of a onto b.
        /// </summary>
        /// <remarks>
        /// Some finite vectors a and b could generate a non-finite result. This is most likely when a's components
        /// are very large (close to Single.MaxValue) or when b's components are very small (close to number.MinNormal).
        /// In these cases, you can call <see cref="projectsafe(Nt.Deterministics.float3,Nt.Deterministics.float3,Nt.Deterministics.float3)"/>
        /// which will use a given default value if the result is not finite.
        /// </remarks>
        /// <param name="a">Vector to project.</param>
        /// <param name="b">Non-zero vector to project onto.</param>
        /// <returns>Vector projection of a onto b.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 project(float3 a, float3 b)
        {
            return (dot(a, b) / dot(b, b)) * b;
        }

        /// <summary>
        /// Compute vector projection of a onto b.
        /// </summary>
        /// <remarks>
        /// Some finite vectors a and b could generate a non-finite result. This is most likely when a's components
        /// are very large (close to Single.MaxValue) or when b's components are very small (close to number.MinNormal).
        /// In these cases, you can call <see cref="projectsafe(Nt.Deterministics.float4,Nt.Deterministics.float4,Nt.Deterministics.float4)"/>
        /// which will use a given default value if the result is not finite.
        /// </remarks>
        /// <param name="a">Vector to project.</param>
        /// <param name="b">Non-zero vector to project onto.</param>
        /// <returns>Vector projection of a onto b.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 project(float4 a, float4 b)
        {
            return (dot(a, b) / dot(b, b)) * b;
        }

        /// <summary>
        /// Compute vector projection of a onto b. If result is not finite, then return the default value instead.
        /// </summary>
        /// <remarks>
        /// This function performs extra checks to see if the result of projecting a onto b is finite. If you know that
        /// your inputs will generate a finite result or you don't care if the result is finite, then you can call
        /// <see cref="project(Nt.Deterministics.float2,Nt.Deterministics.float2)"/> instead which is faster than this
        /// function.
        /// </remarks>
        /// <param name="a">Vector to project.</param>
        /// <param name="b">Non-zero vector to project onto.</param>
        /// <param name="defaultValue">Default value to return if projection is not finite.</param>
        /// <returns>Vector projection of a onto b or the default value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 projectsafe(float2 a, float2 b, float2 defaultValue = new float2())
        {
            var proj = project(a, b);

            return select(defaultValue, proj, Unity.Mathematics.math.all(isfinite(proj)));
        }

        /// <summary>
        /// Compute vector projection of a onto b. If result is not finite, then return the default value instead.
        /// </summary>
        /// <remarks>
        /// This function performs extra checks to see if the result of projecting a onto b is finite. If you know that
        /// your inputs will generate a finite result or you don't care if the result is finite, then you can call
        /// <see cref="project(Nt.Deterministics.float3,Nt.Deterministics.float3)"/> instead which is faster than this
        /// function.
        /// </remarks>
        /// <param name="a">Vector to project.</param>
        /// <param name="b">Non-zero vector to project onto.</param>
        /// <param name="defaultValue">Default value to return if projection is not finite.</param>
        /// <returns>Vector projection of a onto b or the default value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 projectsafe(float3 a, float3 b, float3 defaultValue = new float3())
        {
            var proj = project(a, b);

            return select(defaultValue, proj, Unity.Mathematics.math.all(isfinite(proj)));
        }

        /// <summary>
        /// Compute vector projection of a onto b. If result is not finite, then return the default value instead.
        /// </summary>
        /// <remarks>
        /// This function performs extra checks to see if the result of projecting a onto b is finite. If you know that
        /// your inputs will generate a finite result or you don't care if the result is finite, then you can call
        /// <see cref="project(Nt.Deterministics.float4,Nt.Deterministics.float4)"/> instead which is faster than this
        /// function.
        /// </remarks>
        /// <param name="a">Vector to project.</param>
        /// <param name="b">Non-zero vector to project onto.</param>
        /// <param name="defaultValue">Default value to return if projection is not finite.</param>
        /// <returns>Vector projection of a onto b or the default value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 projectsafe(float4 a, float4 b, float4 defaultValue = new float4())
        {
            var proj = project(a, b);

            return select(defaultValue, proj, Unity.Mathematics.math.all(isfinite(proj)));
        }

        /// <summary>Conditionally flips a vector n if two vectors i and ng are pointing in the same direction. Returns n if dot(i, ng) &lt; 0, -n otherwise.</summary>
        /// <param name="n">Vector to conditionally flip.</param>
        /// <param name="i">First vector in direction comparison.</param>
        /// <param name="ng">Second vector in direction comparison.</param>
        /// <returns>-n if i and ng point in the same direction; otherwise return n unchanged.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 faceforward(float2 n, float2 i, float2 ng) { return select(n, -n, dot(ng, i) >= 0.0f); }

        /// <summary>Conditionally flips a vector n if two vectors i and ng are pointing in the same direction. Returns n if dot(i, ng) &lt; 0, -n otherwise.</summary>
        /// <param name="n">Vector to conditionally flip.</param>
        /// <param name="i">First vector in direction comparison.</param>
        /// <param name="ng">Second vector in direction comparison.</param>
        /// <returns>-n if i and ng point in the same direction; otherwise return n unchanged.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 faceforward(float3 n, float3 i, float3 ng) { return select(n, -n, dot(ng, i) >= 0.0f); }

        /// <summary>Conditionally flips a vector n if two vectors i and ng are pointing in the same direction. Returns n if dot(i, ng) &lt; 0, -n otherwise.</summary>
        /// <param name="n">Vector to conditionally flip.</param>
        /// <param name="i">First vector in direction comparison.</param>
        /// <param name="ng">Second vector in direction comparison.</param>
        /// <returns>-n if i and ng point in the same direction; otherwise return n unchanged.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 faceforward(float4 n, float4 i, float4 ng) { return select(n, -n, dot(ng, i) >= 0.0f); }


        /// <summary>Returns the sine and cosine of the input number value x through the out parameters s and c.</summary>
        /// <remarks>When Burst compiled, his method is faster than calling sin() and cos() separately.</remarks>
        /// <param name="x">Input angle in radians.</param>
        /// <param name="s">Output sine of the input.</param>
        /// <param name="c">Output cosine of the input.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void sincos(number x, out number s, out number c) { s = sin(x); c = cos(x); }

        /// <summary>Returns the componentwise sine and cosine of the input float2 vector x through the out parameters s and c.</summary>
        /// <remarks>When Burst compiled, his method is faster than calling sin() and cos() separately.</remarks>
        /// <param name="x">Input vector containing angles in radians.</param>
        /// <param name="s">Output vector containing the componentwise sine of the input.</param>
        /// <param name="c">Output vector containing the componentwise cosine of the input.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void sincos(float2 x, out float2 s, out float2 c) { s = sin(x); c = cos(x); }

        /// <summary>Returns the componentwise sine and cosine of the input float3 vector x through the out parameters s and c.</summary>
        /// <remarks>When Burst compiled, his method is faster than calling sin() and cos() separately.</remarks>
        /// <param name="x">Input vector containing angles in radians.</param>
        /// <param name="s">Output vector containing the componentwise sine of the input.</param>
        /// <param name="c">Output vector containing the componentwise cosine of the input.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void sincos(float3 x, out float3 s, out float3 c) { s = sin(x); c = cos(x); }

        /// <summary>Returns the componentwise sine and cosine of the input float4 vector x through the out parameters s and c.</summary>
        /// <remarks>When Burst compiled, his method is faster than calling sin() and cos() separately.</remarks>
        /// <param name="x">Input vector containing angles in radians.</param>
        /// <param name="s">Output vector containing the componentwise sine of the input.</param>
        /// <param name="c">Output vector containing the componentwise cosine of the input.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void sincos(float4 x, out float4 s, out float4 c) { s = sin(x); c = cos(x); }

        /// <summary>Returns the result of converting a number value from degrees to radians.</summary>
        /// <param name="x">Angle in degrees.</param>
        /// <returns>Angle converted to radians.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number radians(number x) { return x * number.Deg2Rad; }

        /// <summary>Returns the result of a componentwise conversion of a float2 vector from degrees to radians.</summary>
        /// <param name="x">Vector containing angles in degrees.</param>
        /// <returns>Vector containing angles converted to radians.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 radians(float2 x) { return x * number.Deg2Rad; }

        /// <summary>Returns the result of a componentwise conversion of a float3 vector from degrees to radians.</summary>
        /// <param name="x">Vector containing angles in degrees.</param>
        /// <returns>Vector containing angles converted to radians.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 radians(float3 x) { return x * number.Deg2Rad; }

        /// <summary>Returns the result of a componentwise conversion of a float4 vector from degrees to radians.</summary>
        /// <param name="x">Vector containing angles in degrees.</param>
        /// <returns>Vector containing angles converted to radians.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 radians(float4 x) { return x * number.Deg2Rad; }


        /// <summary>Returns the result of converting a double value from radians to degrees.</summary>
        /// <param name="x">Angle in radians.</param>
        /// <returns>Angle converted to degrees.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number degrees(number x) { return x * number.Rad2Deg; }

        /// <summary>Returns the result of a componentwise conversion of a double2 vector from radians to degrees.</summary>
        /// <param name="x">Vector containing angles in radians.</param>
        /// <returns>Vector containing angles converted to degrees.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 degrees(float2 x) { return x * number.Rad2Deg; }

        /// <summary>Returns the result of a componentwise conversion of a double3 vector from radians to degrees.</summary>
        /// <param name="x">Vector containing angles in radians.</param>
        /// <returns>Vector containing angles converted to degrees.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 degrees(float3 x) { return x * number.Rad2Deg; }

        /// <summary>Returns the result of a componentwise conversion of a double4 vector from radians to degrees.</summary>
        /// <param name="x">Vector containing angles in radians.</param>
        /// <returns>Vector containing angles converted to degrees.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 degrees(float4 x) { return x * number.Rad2Deg; }


        /// <summary>Returns the minimum component of a float2 vector.</summary>
        /// <param name="x">The vector to use when computing the minimum component.</param>
        /// <returns>The value of the minimum component of the vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number cmin(float2 x) { return min(x.x, x.y); }

        /// <summary>Returns the minimum component of a float3 vector.</summary>
        /// <param name="x">The vector to use when computing the minimum component.</param>
        /// <returns>The value of the minimum component of the vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number cmin(float3 x) { return min(min(x.x, x.y), x.z); }

        /// <summary>Returns the minimum component of a float4 vector.</summary>
        /// <param name="x">The vector to use when computing the minimum component.</param>
        /// <returns>The value of the minimum component of the vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number cmin(float4 x) { return min(min(x.x, x.y), min(x.z, x.w)); }


        /// <summary>Returns the maximum component of a float2 vector.</summary>
        /// <param name="x">The vector to use when computing the maximum component.</param>
        /// <returns>The value of the maximum component of the vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number cmax(float2 x) { return max(x.x, x.y); }

        /// <summary>Returns the maximum component of a float3 vector.</summary>
        /// <param name="x">The vector to use when computing the maximum component.</param>
        /// <returns>The value of the maximum component of the vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number cmax(float3 x) { return max(max(x.x, x.y), x.z); }

        /// <summary>Returns the maximum component of a float4 vector.</summary>
        /// <param name="x">The vector to use when computing the maximum component.</param>
        /// <returns>The value of the maximum component of the vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number cmax(float4 x) { return max(max(x.x, x.y), max(x.z, x.w)); }


        /// <summary>Returns the horizontal sum of components of a float2 vector.</summary>
        /// <param name="x">The vector to use when computing the horizontal sum.</param>
        /// <returns>The horizontal sum of of components of the vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number csum(float2 x) { return x.x + x.y; }

        /// <summary>Returns the horizontal sum of components of a float3 vector.</summary>
        /// <param name="x">The vector to use when computing the horizontal sum.</param>
        /// <returns>The horizontal sum of of components of the vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number csum(float3 x) { return x.x + x.y + x.z; }

        /// <summary>Returns the horizontal sum of components of a float4 vector.</summary>
        /// <param name="x">The vector to use when computing the horizontal sum.</param>
        /// <returns>The horizontal sum of of components of the vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number csum(float4 x) { return (x.x + x.y) + (x.z + x.w); }

        /// <summary>
        /// Computes the square (x * x) of the input argument x.
        /// </summary>
        /// <param name="x">Value to square.</param>
        /// <returns>Returns the square of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number square(number x)
        {
            return x * x;
        }

        /// <summary>
        /// Computes the component-wise square (x * x) of the input argument x.
        /// </summary>
        /// <param name="x">Value to square.</param>
        /// <returns>Returns the square of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 square(float2 x)
        {
            return x * x;
        }

        /// <summary>
        /// Computes the component-wise square (x * x) of the input argument x.
        /// </summary>
        /// <param name="x">Value to square.</param>
        /// <returns>Returns the square of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 square(float3 x)
        {
            return x * x;
        }

        /// <summary>
        /// Computes the component-wise square (x * x) of the input argument x.
        /// </summary>
        /// <param name="x">Value to square.</param>
        /// <returns>Returns the square of the input.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 square(float4 x)
        {
            return x * x;
        }

        /// <summary>
        /// Packs components with an enabled mask to the left.
        /// </summary>
        /// <remarks>
        /// This function is also known as left packing. The effect of this function is to filter out components that
        /// are not enabled and leave an output buffer tightly packed with only the enabled components. A common use
        /// case is if you perform intersection tests on arrays of data in structure of arrays (SoA) form and need to
        /// produce an output array of the things that intersected.
        /// </remarks>
        /// <param name="output">Pointer to packed output array where enabled components should be stored to.</param>
        /// <param name="index">Index into output array where first enabled component should be stored to.</param>
        /// <param name="val">The value to to compress.</param>
        /// <param name="mask">Mask indicating which components are enabled.</param>
        /// <returns>Index to element after the last one stored.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe int compress(number* output, int index, float4 val, bool4 mask)
        {
            return Unity.Mathematics.math.compress((int*)output, index, *(int4*)&val, mask);
        }

        /// <summary>
        /// Generate an orthonormal basis given a single unit length normal vector.
        /// </summary>
        /// <remarks>
        /// This implementation is from "Building an Orthonormal Basis, Revisited"
        /// https://graphics.pixar.com/library/OrthonormalB/paper.pdf
        /// </remarks>
        /// <param name="normal">Unit length normal vector.</param>
        /// <param name="basis1">Output unit length vector, orthogonal to normal vector.</param>
        /// <param name="basis2">Output unit length vector, orthogonal to normal vector and basis1.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void orthonormal_basis(float3 normal, out float3 basis1, out float3 basis2)
        {
            var sign = normal.z >= 0.0f ? 1.0f : -1.0f;
            var a = -1.0f / (sign + normal.z);
            var b = normal.x * normal.y * a;
            basis1.x = 1.0f + sign * normal.x * normal.x * a;
            basis1.y = sign * b;
            basis1.z = -sign * normal.x;
            basis2.x = b;
            basis2.y = sign + normal.y * normal.y * a;
            basis2.z = -normal.y;
        }

        /// <summary>
        /// angle(Radian, not degree) between x and y
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number angle(float2 x, float2 y)
        {
            long angle1 = number.Atan2Raw(x.y.RawValue, x.x.RawValue);
            long angle2 = number.Atan2Raw(y.y.RawValue, y.x.RawValue);
            x.x.RawValue = angle1 > angle2 ? angle1 - angle2 : angle2 - angle1;
            x.y.RawValue = x.x.RawValue > number.Pi ? number.PiTimes2 - x.x.RawValue : x.x.RawValue;
            return x.y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number angle(float3 x, float3 y)
        {
            if (anyNaN(x) || anyNaN(y)) return number.NaN;
            var num = length(x) * length(y);
            if (num <= number.MinNormal) return number.zero;
            return number.Acos(dot(x, y) / num);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number angle(float3 x, float3 y, float3 axis)
        {
            if (anyNaN(x) || anyNaN(y) || anyNaN(axis)) return number.NaN;
            var axis0 = cross(x, y);
            var dotValue = dot(x, y);
            if (axis0.Equals(Deterministics.float3.zero)) return dotValue > 0 ? number.zero : number.PI;
            var num = length(x) * length(y);
            if (num <= number.MinNormal) return number.zero;
            var ang = number.Acos(dotValue / num);
            return dot(axis0, axis) >= number.zero ? ang : number.PITimes2 - ang;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float4 unpacklo(float4 a, float4 b)
        {
            return shuffle(a, b, ShuffleComponent.LeftX, ShuffleComponent.RightX, ShuffleComponent.LeftY, ShuffleComponent.RightY);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float4 unpackhi(float4 a, float4 b)
        {
            return shuffle(a, b, ShuffleComponent.LeftZ, ShuffleComponent.RightZ, ShuffleComponent.LeftW, ShuffleComponent.RightW);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float4 movelh(float4 a, float4 b)
        {
            return shuffle(a, b, ShuffleComponent.LeftX, ShuffleComponent.LeftY, ShuffleComponent.RightX, ShuffleComponent.RightY);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float4 movehl(float4 a, float4 b)
        {
            return shuffle(b, a, ShuffleComponent.LeftZ, ShuffleComponent.LeftW, ShuffleComponent.RightZ, ShuffleComponent.RightW);
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct LongDoubleUnion
        {
            [FieldOffset(0)]
            public long longValue;
            [FieldOffset(0)]
            public double doubleValue;
        }
    }
}
