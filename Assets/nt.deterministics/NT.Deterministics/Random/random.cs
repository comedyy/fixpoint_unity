using System;
using System.Runtime.CompilerServices;

#if NO_NT_RANDOM

namespace Nt.Deterministics
{
    public class Random
    {
        protected int m_Seed;
        protected System.Random m_Random;
        public Random()
            : this(Environment.TickCount) { }
        public Random(int seed)
        {
            m_Seed = seed;
            m_Random = new System.Random(seed);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Next() => m_Random.Next();
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Next(int maxValue) => m_Random.Next(maxValue);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Next(int minValue, int maxValue) => m_Random.Next(minValue, maxValue);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NextBytes(byte[] buffer) => m_Random.NextBytes(buffer);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double NextDouble() => m_Random.NextDouble();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public number NextNumber()
        {
            var high = (long)m_Random.Next();
            var low = (long)m_Random.Next();
            return number.FromRaw((high << 32) + low);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public number NextNumber(number maxValue)
        {
            if (maxValue < 0) throw new ArgumentException("maxValue can't be less than 0!");
            const long lowmask = (1L << 32) - 1;
            const long highmask = ~lowmask;
            long high_max = (maxValue.RawValue & highmask) >> 32;
            long low_max = maxValue.RawValue & lowmask;
            long high = (long)m_Random.Next(0, (int)high_max);
            long low = (long)m_Random.Next(int.MinValue, high != high_max ? int.MaxValue : (int)(int.MinValue + low_max)) - (long)int.MinValue;
            return number.FromRaw((high << 32) + low);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public number NextNumber(number minValue, number maxValue)
        {
            if (minValue < maxValue) throw new ArgumentException("max can't be less than min!");
            if (minValue.RawValue < 0 && maxValue.RawValue > 0)
            {
                ulong range = (ulong)maxValue.RawValue + (ulong)(-minValue.RawValue);
                const ulong lowmask = (1L << 32) - 1;
                const ulong highmask = ~lowmask;
                long high_max = (long)((range & highmask) >> 32);
                long low_max = (long)(range & lowmask);
                long high = (long)m_Random.Next(int.MinValue, (int)(int.MinValue + high_max)) - (long)int.MinValue;
                long low = (long)m_Random.Next(int.MinValue, high != high_max ? int.MaxValue : (int)(int.MinValue + low_max)) - (long)int.MinValue;
                return number.FromRaw((high << 32) + low + minValue.RawValue);
            }
            return minValue + NextNumber(maxValue - minValue);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float2 NextNumber2() => new float2()
        {
            x = NextNumber(),
            y = NextNumber()
        };
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float2 NextNumber2(number maxValue) => new float2()
        {
            x = NextNumber(maxValue),
            y = NextNumber(maxValue)
        };
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float2 NextNumber2(number minValue, number maxValue) => new float2()
        {
            x = NextNumber(minValue, maxValue),
            y = NextNumber(minValue, maxValue)
        };
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float3 NextNumber3() => new float3()
        {
            x = NextNumber(),
            y = NextNumber(),
            z = NextNumber()
        };
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float3 NextNumber3(number maxValue) => new float3()
        {
            x = NextNumber(maxValue),
            y = NextNumber(maxValue),
            z = NextNumber(maxValue)
        };
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float3 NextNumber3(number minValue, number maxValue) => new float3()
        {
            x = NextNumber(minValue, maxValue),
            y = NextNumber(minValue, maxValue),
            z = NextNumber(minValue, maxValue)
        };
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float4 NextNumber4() => new float4()
        {
            x = NextNumber(),
            y = NextNumber(),
            z = NextNumber(),
            w = NextNumber()
        };
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float4 NextNumber4(number maxValue) => new float4()
        {
            x = NextNumber(maxValue),
            y = NextNumber(maxValue),
            z = NextNumber(maxValue),
            w = NextNumber(maxValue)
        };
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float4 NextNumber4(number minValue, number maxValue) => new float4()
        {
            x = NextNumber(minValue, maxValue),
            y = NextNumber(minValue, maxValue),
            z = NextNumber(minValue, maxValue),
            w = NextNumber(minValue, maxValue)
        };
    }
}
#endif
