//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated. To update the generation of this file, modify and re-run Unity.Mathematics.CodeGen.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Runtime.CompilerServices;

namespace Nt.Deterministics
{
    partial class math
    {
        /// <summary>Returns the number value result of a matrix multiplication between a number value and a number value.</summary>
        /// <param name="a">Left hand side argument of the matrix multiply.</param>
        /// <param name="b">Right hand side argument of the matrix multiply.</param>
        /// <returns>The computed matrix multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number mul(number a, number b)
        {
            return a * b;
        }

        /// <summary>Returns the number value result of a matrix multiplication between a float2 row vector and a float2 column vector.</summary>
        /// <param name="a">Left hand side argument of the matrix multiply.</param>
        /// <param name="b">Right hand side argument of the matrix multiply.</param>
        /// <returns>The computed matrix multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number mul(float2 a, float2 b)
        {
            return a.x * b.x + a.y * b.y;
        }

        /// <summary>Returns the float2 row vector result of a matrix multiplication between a float2 row vector and a float2x2 matrix.</summary>
        /// <param name="a">Left hand side argument of the matrix multiply.</param>
        /// <param name="b">Right hand side argument of the matrix multiply.</param>
        /// <returns>The computed matrix multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 mul(float2 a, float2x2 b)
        {
            return float2(
                a.x * b.c0.x + a.y * b.c0.y,
                a.x * b.c1.x + a.y * b.c1.y);
        }

        /// <summary>Returns the float3 row vector result of a matrix multiplication between a float2 row vector and a float2x3 matrix.</summary>
        /// <param name="a">Left hand side argument of the matrix multiply.</param>
        /// <param name="b">Right hand side argument of the matrix multiply.</param>
        /// <returns>The computed matrix multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 mul(float2 a, float2x3 b)
        {
            return float3(
                a.x * b.c0.x + a.y * b.c0.y,
                a.x * b.c1.x + a.y * b.c1.y,
                a.x * b.c2.x + a.y * b.c2.y);
        }

        /// <summary>Returns the float4 row vector result of a matrix multiplication between a float2 row vector and a float2x4 matrix.</summary>
        /// <param name="a">Left hand side argument of the matrix multiply.</param>
        /// <param name="b">Right hand side argument of the matrix multiply.</param>
        /// <returns>The computed matrix multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 mul(float2 a, float2x4 b)
        {
            return float4(
                a.x * b.c0.x + a.y * b.c0.y,
                a.x * b.c1.x + a.y * b.c1.y,
                a.x * b.c2.x + a.y * b.c2.y,
                a.x * b.c3.x + a.y * b.c3.y);
        }

        /// <summary>Returns the number value result of a matrix multiplication between a float3 row vector and a float3 column vector.</summary>
        /// <param name="a">Left hand side argument of the matrix multiply.</param>
        /// <param name="b">Right hand side argument of the matrix multiply.</param>
        /// <returns>The computed matrix multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number mul(float3 a, float3 b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }

        /// <summary>Returns the float2 row vector result of a matrix multiplication between a float3 row vector and a float3x2 matrix.</summary>
        /// <param name="a">Left hand side argument of the matrix multiply.</param>
        /// <param name="b">Right hand side argument of the matrix multiply.</param>
        /// <returns>The computed matrix multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 mul(float3 a, float3x2 b)
        {
            return float2(
                a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z,
                a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z);
        }

        /// <summary>Returns the float3 row vector result of a matrix multiplication between a float3 row vector and a float3x3 matrix.</summary>
        /// <param name="a">Left hand side argument of the matrix multiply.</param>
        /// <param name="b">Right hand side argument of the matrix multiply.</param>
        /// <returns>The computed matrix multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 mul(float3 a, float3x3 b)
        {
            return float3(
                a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z,
                a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z,
                a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z);
        }

        /// <summary>Returns the float4 row vector result of a matrix multiplication between a float3 row vector and a float3x4 matrix.</summary>
        /// <param name="a">Left hand side argument of the matrix multiply.</param>
        /// <param name="b">Right hand side argument of the matrix multiply.</param>
        /// <returns>The computed matrix multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 mul(float3 a, float3x4 b)
        {
            return float4(
                a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z,
                a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z,
                a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z,
                a.x * b.c3.x + a.y * b.c3.y + a.z * b.c3.z);
        }

        /// <summary>Returns the number value result of a matrix multiplication between a float4 row vector and a float4 column vector.</summary>
        /// <param name="a">Left hand side argument of the matrix multiply.</param>
        /// <param name="b">Right hand side argument of the matrix multiply.</param>
        /// <returns>The computed matrix multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static number mul(float4 a, float4 b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
        }

        /// <summary>Returns the float2 row vector result of a matrix multiplication between a float4 row vector and a float4x2 matrix.</summary>
        /// <param name="a">Left hand side argument of the matrix multiply.</param>
        /// <param name="b">Right hand side argument of the matrix multiply.</param>
        /// <returns>The computed matrix multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 mul(float4 a, float4x2 b)
        {
            return float2(
                a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z + a.w * b.c0.w,
                a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z + a.w * b.c1.w);
        }

        /// <summary>Returns the float3 row vector result of a matrix multiplication between a float4 row vector and a float4x3 matrix.</summary>
        /// <param name="a">Left hand side argument of the matrix multiply.</param>
        /// <param name="b">Right hand side argument of the matrix multiply.</param>
        /// <returns>The computed matrix multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 mul(float4 a, float4x3 b)
        {
            return float3(
                a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z + a.w * b.c0.w,
                a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z + a.w * b.c1.w,
                a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z + a.w * b.c2.w);
        }

        /// <summary>Returns the float4 row vector result of a matrix multiplication between a float4 row vector and a float4x4 matrix.</summary>
        /// <param name="a">Left hand side argument of the matrix multiply.</param>
        /// <param name="b">Right hand side argument of the matrix multiply.</param>
        /// <returns>The computed matrix multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 mul(float4 a, float4x4 b)
        {
            return float4(
                a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z + a.w * b.c0.w,
                a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z + a.w * b.c1.w,
                a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z + a.w * b.c2.w,
                a.x * b.c3.x + a.y * b.c3.y + a.z * b.c3.z + a.w * b.c3.w);
        }

        /// <summary>Returns the float2 column vector result of a matrix multiplication between a float2x2 matrix and a float2 column vector.</summary>
        /// <param name="a">Left hand side argument of the matrix multiply.</param>
        /// <param name="b">Right hand side argument of the matrix multiply.</param>
        /// <returns>The computed matrix multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 mul(float2x2 a, float2 b)
        {
            return a.c0 * b.x + a.c1 * b.y;
        }

        /// <summary>Returns the float2x2 matrix result of a matrix multiplication between a float2x2 matrix and a float2x2 matrix.</summary>
        /// <param name="a">Left hand side argument of the matrix multiply.</param>
        /// <param name="b">Right hand side argument of the matrix multiply.</param>
        /// <returns>The computed matrix multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2x2 mul(float2x2 a, float2x2 b)
        {
            return float2x2(
                a.c0 * b.c0.x + a.c1 * b.c0.y,
                a.c0 * b.c1.x + a.c1 * b.c1.y);
        }

        /// <summary>Returns the float2x3 matrix result of a matrix multiplication between a float2x2 matrix and a float2x3 matrix.</summary>
        /// <param name="a">Left hand side argument of the matrix multiply.</param>
        /// <param name="b">Right hand side argument of the matrix multiply.</param>
        /// <returns>The computed matrix multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2x3 mul(float2x2 a, float2x3 b)
        {
            return float2x3(
                a.c0 * b.c0.x + a.c1 * b.c0.y,
                a.c0 * b.c1.x + a.c1 * b.c1.y,
                a.c0 * b.c2.x + a.c1 * b.c2.y);
        }

        /// <summary>Returns the float2x4 matrix result of a matrix multiplication between a float2x2 matrix and a float2x4 matrix.</summary>
        /// <param name="a">Left hand side argument of the matrix multiply.</param>
        /// <param name="b">Right hand side argument of the matrix multiply.</param>
        /// <returns>The computed matrix multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2x4 mul(float2x2 a, float2x4 b)
        {
            return float2x4(
                a.c0 * b.c0.x + a.c1 * b.c0.y,
                a.c0 * b.c1.x + a.c1 * b.c1.y,
                a.c0 * b.c2.x + a.c1 * b.c2.y,
                a.c0 * b.c3.x + a.c1 * b.c3.y);
        }

        /// <summary>Returns the float2 column vector result of a matrix multiplication between a float2x3 matrix and a float3 column vector.</summary>
        /// <param name="a">Left hand side argument of the matrix multiply.</param>
        /// <param name="b">Right hand side argument of the matrix multiply.</param>
        /// <returns>The computed matrix multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 mul(float2x3 a, float3 b)
        {
            return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z;
        }

        /// <summary>Returns the float2x2 matrix result of a matrix multiplication between a float2x3 matrix and a float3x2 matrix.</summary>
        /// <param name="a">Left hand side argument of the matrix multiply.</param>
        /// <param name="b">Right hand side argument of the matrix multiply.</param>
        /// <returns>The computed matrix multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2x2 mul(float2x3 a, float3x2 b)
        {
            return float2x2(
                a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z,
                a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z);
        }

        /// <summary>Returns the float2x3 matrix result of a matrix multiplication between a float2x3 matrix and a float3x3 matrix.</summary>
        /// <param name="a">Left hand side argument of the matrix multiply.</param>
        /// <param name="b">Right hand side argument of the matrix multiply.</param>
        /// <returns>The computed matrix multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2x3 mul(float2x3 a, float3x3 b)
        {
            return float2x3(
                a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z,
                a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z,
                a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z);
        }

        /// <summary>Returns the float2x4 matrix result of a matrix multiplication between a float2x3 matrix and a float3x4 matrix.</summary>
        /// <param name="a">Left hand side argument of the matrix multiply.</param>
        /// <param name="b">Right hand side argument of the matrix multiply.</param>
        /// <returns>The computed matrix multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2x4 mul(float2x3 a, float3x4 b)
        {
            return float2x4(
                a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z,
                a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z,
                a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z,
                a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z);
        }

        /// <summary>Returns the float2 column vector result of a matrix multiplication between a float2x4 matrix and a float4 column vector.</summary>
        /// <param name="a">Left hand side argument of the matrix multiply.</param>
        /// <param name="b">Right hand side argument of the matrix multiply.</param>
        /// <returns>The computed matrix multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 mul(float2x4 a, float4 b)
        {
            return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z + a.c3 * b.w;
        }

        /// <summary>Returns the float2x2 matrix result of a matrix multiplication between a float2x4 matrix and a float4x2 matrix.</summary>
        /// <param name="a">Left hand side argument of the matrix multiply.</param>
        /// <param name="b">Right hand side argument of the matrix multiply.</param>
        /// <returns>The computed matrix multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2x2 mul(float2x4 a, float4x2 b)
        {
            return float2x2(
                a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w,
                a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w);
        }

        /// <summary>Returns the float2x3 matrix result of a matrix multiplication between a float2x4 matrix and a float4x3 matrix.</summary>
        /// <param name="a">Left hand side argument of the matrix multiply.</param>
        /// <param name="b">Right hand side argument of the matrix multiply.</param>
        /// <returns>The computed matrix multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2x3 mul(float2x4 a, float4x3 b)
        {
            return float2x3(
                a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w,
                a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w,
                a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w);
        }

        /// <summary>Returns the float2x4 matrix result of a matrix multiplication between a float2x4 matrix and a float4x4 matrix.</summary>
        /// <param name="a">Left hand side argument of the matrix multiply.</param>
        /// <param name="b">Right hand side argument of the matrix multiply.</param>
        /// <returns>The computed matrix multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2x4 mul(float2x4 a, float4x4 b)
        {
            return float2x4(
                a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w,
                a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w,
                a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w,
                a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z + a.c3 * b.c3.w);
        }

        /// <summary>Returns the float3 column vector result of a matrix multiplication between a float3x2 matrix and a float2 column vector.</summary>
        /// <param name="a">Left hand side argument of the matrix multiply.</param>
        /// <param name="b">Right hand side argument of the matrix multiply.</param>
        /// <returns>The computed matrix multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 mul(float3x2 a, float2 b)
        {
            return a.c0 * b.x + a.c1 * b.y;
        }

        /// <summary>Returns the float3x2 matrix result of a matrix multiplication between a float3x2 matrix and a float2x2 matrix.</summary>
        /// <param name="a">Left hand side argument of the matrix multiply.</param>
        /// <param name="b">Right hand side argument of the matrix multiply.</param>
        /// <returns>The computed matrix multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x2 mul(float3x2 a, float2x2 b)
        {
            return float3x2(
                a.c0 * b.c0.x + a.c1 * b.c0.y,
                a.c0 * b.c1.x + a.c1 * b.c1.y);
        }

        /// <summary>Returns the float3x3 matrix result of a matrix multiplication between a float3x2 matrix and a float2x3 matrix.</summary>
        /// <param name="a">Left hand side argument of the matrix multiply.</param>
        /// <param name="b">Right hand side argument of the matrix multiply.</param>
        /// <returns>The computed matrix multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 mul(float3x2 a, float2x3 b)
        {
            return float3x3(
                a.c0 * b.c0.x + a.c1 * b.c0.y,
                a.c0 * b.c1.x + a.c1 * b.c1.y,
                a.c0 * b.c2.x + a.c1 * b.c2.y);
        }

        /// <summary>Returns the float3x4 matrix result of a matrix multiplication between a float3x2 matrix and a float2x4 matrix.</summary>
        /// <param name="a">Left hand side argument of the matrix multiply.</param>
        /// <param name="b">Right hand side argument of the matrix multiply.</param>
        /// <returns>The computed matrix multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x4 mul(float3x2 a, float2x4 b)
        {
            return float3x4(
                a.c0 * b.c0.x + a.c1 * b.c0.y,
                a.c0 * b.c1.x + a.c1 * b.c1.y,
                a.c0 * b.c2.x + a.c1 * b.c2.y,
                a.c0 * b.c3.x + a.c1 * b.c3.y);
        }

        /// <summary>Returns the float3 column vector result of a matrix multiplication between a float3x3 matrix and a float3 column vector.</summary>
        /// <param name="a">Left hand side argument of the matrix multiply.</param>
        /// <param name="b">Right hand side argument of the matrix multiply.</param>
        /// <returns>The computed matrix multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 mul(float3x3 a, float3 b)
        {
            return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z;
        }

        /// <summary>Returns the float3x2 matrix result of a matrix multiplication between a float3x3 matrix and a float3x2 matrix.</summary>
        /// <param name="a">Left hand side argument of the matrix multiply.</param>
        /// <param name="b">Right hand side argument of the matrix multiply.</param>
        /// <returns>The computed matrix multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x2 mul(float3x3 a, float3x2 b)
        {
            return float3x2(
                a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z,
                a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z);
        }

        /// <summary>Returns the float3x3 matrix result of a matrix multiplication between a float3x3 matrix and a float3x3 matrix.</summary>
        /// <param name="a">Left hand side argument of the matrix multiply.</param>
        /// <param name="b">Right hand side argument of the matrix multiply.</param>
        /// <returns>The computed matrix multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 mul(float3x3 a, float3x3 b)
        {
            return float3x3(
                a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z,
                a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z,
                a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z);
        }

        /// <summary>Returns the float3x4 matrix result of a matrix multiplication between a float3x3 matrix and a float3x4 matrix.</summary>
        /// <param name="a">Left hand side argument of the matrix multiply.</param>
        /// <param name="b">Right hand side argument of the matrix multiply.</param>
        /// <returns>The computed matrix multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x4 mul(float3x3 a, float3x4 b)
        {
            return float3x4(
                a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z,
                a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z,
                a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z,
                a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z);
        }

        /// <summary>Returns the float3 column vector result of a matrix multiplication between a float3x4 matrix and a float4 column vector.</summary>
        /// <param name="a">Left hand side argument of the matrix multiply.</param>
        /// <param name="b">Right hand side argument of the matrix multiply.</param>
        /// <returns>The computed matrix multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 mul(float3x4 a, float4 b)
        {
            return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z + a.c3 * b.w;
        }

        /// <summary>Returns the float3x2 matrix result of a matrix multiplication between a float3x4 matrix and a float4x2 matrix.</summary>
        /// <param name="a">Left hand side argument of the matrix multiply.</param>
        /// <param name="b">Right hand side argument of the matrix multiply.</param>
        /// <returns>The computed matrix multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x2 mul(float3x4 a, float4x2 b)
        {
            return float3x2(
                a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w,
                a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w);
        }

        /// <summary>Returns the float3x3 matrix result of a matrix multiplication between a float3x4 matrix and a float4x3 matrix.</summary>
        /// <param name="a">Left hand side argument of the matrix multiply.</param>
        /// <param name="b">Right hand side argument of the matrix multiply.</param>
        /// <returns>The computed matrix multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 mul(float3x4 a, float4x3 b)
        {
            return float3x3(
                a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w,
                a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w,
                a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w);
        }

        /// <summary>Returns the float3x4 matrix result of a matrix multiplication between a float3x4 matrix and a float4x4 matrix.</summary>
        /// <param name="a">Left hand side argument of the matrix multiply.</param>
        /// <param name="b">Right hand side argument of the matrix multiply.</param>
        /// <returns>The computed matrix multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x4 mul(float3x4 a, float4x4 b)
        {
            return float3x4(
                a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w,
                a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w,
                a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w,
                a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z + a.c3 * b.c3.w);
        }

        /// <summary>Returns the float4 column vector result of a matrix multiplication between a float4x2 matrix and a float2 column vector.</summary>
        /// <param name="a">Left hand side argument of the matrix multiply.</param>
        /// <param name="b">Right hand side argument of the matrix multiply.</param>
        /// <returns>The computed matrix multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 mul(float4x2 a, float2 b)
        {
            return a.c0 * b.x + a.c1 * b.y;
        }

        /// <summary>Returns the float4x2 matrix result of a matrix multiplication between a float4x2 matrix and a float2x2 matrix.</summary>
        /// <param name="a">Left hand side argument of the matrix multiply.</param>
        /// <param name="b">Right hand side argument of the matrix multiply.</param>
        /// <returns>The computed matrix multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x2 mul(float4x2 a, float2x2 b)
        {
            return float4x2(
                a.c0 * b.c0.x + a.c1 * b.c0.y,
                a.c0 * b.c1.x + a.c1 * b.c1.y);
        }

        /// <summary>Returns the float4x3 matrix result of a matrix multiplication between a float4x2 matrix and a float2x3 matrix.</summary>
        /// <param name="a">Left hand side argument of the matrix multiply.</param>
        /// <param name="b">Right hand side argument of the matrix multiply.</param>
        /// <returns>The computed matrix multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x3 mul(float4x2 a, float2x3 b)
        {
            return float4x3(
                a.c0 * b.c0.x + a.c1 * b.c0.y,
                a.c0 * b.c1.x + a.c1 * b.c1.y,
                a.c0 * b.c2.x + a.c1 * b.c2.y);
        }

        /// <summary>Returns the float4x4 matrix result of a matrix multiplication between a float4x2 matrix and a float2x4 matrix.</summary>
        /// <param name="a">Left hand side argument of the matrix multiply.</param>
        /// <param name="b">Right hand side argument of the matrix multiply.</param>
        /// <returns>The computed matrix multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 mul(float4x2 a, float2x4 b)
        {
            return float4x4(
                a.c0 * b.c0.x + a.c1 * b.c0.y,
                a.c0 * b.c1.x + a.c1 * b.c1.y,
                a.c0 * b.c2.x + a.c1 * b.c2.y,
                a.c0 * b.c3.x + a.c1 * b.c3.y);
        }

        /// <summary>Returns the float4 column vector result of a matrix multiplication between a float4x3 matrix and a float3 column vector.</summary>
        /// <param name="a">Left hand side argument of the matrix multiply.</param>
        /// <param name="b">Right hand side argument of the matrix multiply.</param>
        /// <returns>The computed matrix multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 mul(float4x3 a, float3 b)
        {
            return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z;
        }

        /// <summary>Returns the float4x2 matrix result of a matrix multiplication between a float4x3 matrix and a float3x2 matrix.</summary>
        /// <param name="a">Left hand side argument of the matrix multiply.</param>
        /// <param name="b">Right hand side argument of the matrix multiply.</param>
        /// <returns>The computed matrix multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x2 mul(float4x3 a, float3x2 b)
        {
            return float4x2(
                a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z,
                a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z);
        }

        /// <summary>Returns the float4x3 matrix result of a matrix multiplication between a float4x3 matrix and a float3x3 matrix.</summary>
        /// <param name="a">Left hand side argument of the matrix multiply.</param>
        /// <param name="b">Right hand side argument of the matrix multiply.</param>
        /// <returns>The computed matrix multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x3 mul(float4x3 a, float3x3 b)
        {
            return float4x3(
                a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z,
                a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z,
                a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z);
        }

        /// <summary>Returns the float4x4 matrix result of a matrix multiplication between a float4x3 matrix and a float3x4 matrix.</summary>
        /// <param name="a">Left hand side argument of the matrix multiply.</param>
        /// <param name="b">Right hand side argument of the matrix multiply.</param>
        /// <returns>The computed matrix multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 mul(float4x3 a, float3x4 b)
        {
            return float4x4(
                a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z,
                a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z,
                a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z,
                a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z);
        }

        /// <summary>Returns the float4 column vector result of a matrix multiplication between a float4x4 matrix and a float4 column vector.</summary>
        /// <param name="a">Left hand side argument of the matrix multiply.</param>
        /// <param name="b">Right hand side argument of the matrix multiply.</param>
        /// <returns>The computed matrix multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 mul(float4x4 a, float4 b)
        {
            return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z + a.c3 * b.w;
        }

        /// <summary>Returns the float4x2 matrix result of a matrix multiplication between a float4x4 matrix and a float4x2 matrix.</summary>
        /// <param name="a">Left hand side argument of the matrix multiply.</param>
        /// <param name="b">Right hand side argument of the matrix multiply.</param>
        /// <returns>The computed matrix multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x2 mul(float4x4 a, float4x2 b)
        {
            return float4x2(
                a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w,
                a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w);
        }

        /// <summary>Returns the float4x3 matrix result of a matrix multiplication between a float4x4 matrix and a float4x3 matrix.</summary>
        /// <param name="a">Left hand side argument of the matrix multiply.</param>
        /// <param name="b">Right hand side argument of the matrix multiply.</param>
        /// <returns>The computed matrix multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x3 mul(float4x4 a, float4x3 b)
        {
            return float4x3(
                a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w,
                a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w,
                a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w);
        }

        /// <summary>Returns the float4x4 matrix result of a matrix multiplication between a float4x4 matrix and a float4x4 matrix.</summary>
        /// <param name="a">Left hand side argument of the matrix multiply.</param>
        /// <param name="b">Right hand side argument of the matrix multiply.</param>
        /// <returns>The computed matrix multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 mul(float4x4 a, float4x4 b)
        {
            return float4x4(
                a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w,
                a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w,
                a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w,
                a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z + a.c3 * b.c3.w);
        }

    }
}
