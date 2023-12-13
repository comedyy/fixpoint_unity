using System;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Globalization;
using System.Diagnostics.Contracts;

#if !NO_NT_RANDOM

namespace Nt.Deterministics
{
    [System.Runtime.InteropServices.ComVisible(true)]
    [Serializable]
    public class Random
    {
        //
        // Private Constants 
        //
        private const int MBIG = Int32.MaxValue;
        private const int MSEED = 161803398;
        private const int MZ = 0;


        //
        // Member Variables
        //
        private int inext;
        private int inextp;
        private int[] SeedArray = new int[56];

        //
        // Public Constants
        //

        //
        // Native Declarations
        //

        //
        // Constructors
        //

        public Random()
          : this(Environment.TickCount)
        {
        }

        public Random(int Seed)
        {
            int ii;
            int mj, mk;

            //Initialize our Seed array.
            //This algorithm comes from Numerical Recipes in C (2nd Ed.)
            int subtraction = (Seed == Int32.MinValue) ? Int32.MaxValue : Math.Abs(Seed);
            mj = MSEED - subtraction;
            SeedArray[55] = mj;
            mk = 1;
            for (int i = 1; i < 55; i++)
            {  //Apparently the range [1..55] is special (Knuth) and so we're wasting the 0'th position.
                ii = (21 * i) % 55;
                SeedArray[ii] = mk;
                mk = mj - mk;
                if (mk < 0) mk += MBIG;
                mj = SeedArray[ii];
            }
            for (int k = 1; k < 5; k++)
            {
                for (int i = 1; i < 56; i++)
                {
                    SeedArray[i] -= SeedArray[1 + (i + 30) % 55];
                    if (SeedArray[i] < 0) SeedArray[i] += MBIG;
                }
            }
            inext = 0;
            inextp = 21;
            Seed = 1;
        }

        //
        // Package Private Methods
        //

        /*====================================Sample====================================
        **Action: Return a new random number [0..1) and reSeed the Seed array.
        **Returns: A double [0..1)
        **Arguments: None
        **Exceptions: None
        ==============================================================================*/
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected double Sample()
        {
            //Including this division at the end gives us significantly improved
            //random number distribution.
            return (InternalSample() * (1.0 / MBIG));
        }

        private int InternalSample()
        {
            int retVal;
            int locINext = inext;
            int locINextp = inextp;

            if (++locINext >= 56) locINext = 1;
            if (++locINextp >= 56) locINextp = 1;

            retVal = SeedArray[locINext] - SeedArray[locINextp];

            if (retVal == MBIG) retVal--;
            if (retVal < 0) retVal += MBIG;

            SeedArray[locINext] = retVal;

            inext = locINext;
            inextp = locINextp;

            return retVal;
        }

        //
        // Public Instance Methods
        // 


        /*=====================================Next=====================================
        **Returns: An int [0..Int32.MaxValue)
        **Arguments: None
        **Exceptions: None.
        ==============================================================================*/
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Next()
        {
            return InternalSample();
        }

        private double GetSampleForLargeRange()
        {
            // The distribution of double value returned by Sample 
            // is not distributed well enough for a large range.
            // If we use Sample for a range [Int32.MinValue..Int32.MaxValue)
            // We will end up getting even numbers only.

            int result = InternalSample();
            // Note we can't use addition here. The distribution will be bad if we do that.
            bool negative = (InternalSample() % 2 == 0) ? true : false;  // decide the sign based on second sample
            if (negative)
            {
                result = -result;
            }
            double d = result;
            d += (Int32.MaxValue - 1); // get a number in range [0 .. 2 * Int32MaxValue - 1)
            d /= 2 * (uint)Int32.MaxValue - 1;
            return d;
        }


        /*=====================================Next=====================================
        **Returns: An int [minvalue..maxvalue)
        **Arguments: minValue -- the least legal value for the Random number.
        **           maxValue -- One greater than the greatest legal return value.
        **Exceptions: None.
        ==============================================================================*/
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Next(int minValue, int maxValue)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentOutOfRangeException("minValue", "Argument_MinMaxValue");
            }
            Contract.EndContractBlock();

            long range = (long)maxValue - minValue;
            if (range <= (long)Int32.MaxValue)
            {
                return ((int)(Sample() * range) + minValue);
            }
            else
            {
                return (int)((long)(GetSampleForLargeRange() * range) + minValue);
            }
        }


        /*=====================================Next=====================================
        **Returns: An int [0..maxValue)
        **Arguments: maxValue -- One more than the greatest legal return value.
        **Exceptions: None.
        ==============================================================================*/
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Next(int maxValue)
        {
            if (maxValue < 0)
            {
                throw new ArgumentOutOfRangeException("maxValue", "ArgumentOutOfRange_MustBePositive");
            }
            Contract.EndContractBlock();
            return (int)(Sample() * maxValue);
        }


        /*=====================================Next=====================================
        **Returns: A double [0..1)
        **Arguments: None
        **Exceptions: None
        ==============================================================================*/
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double NextDouble()
        {
            return Sample();
        }


        /*==================================NextBytes===================================
        **Action:  Fills the byte array with random bytes [0..0x7f].  The entire array is filled.
        **Returns:Void
        **Arugments:  buffer -- the array to be filled.
        **Exceptions: None
        ==============================================================================*/
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NextBytes(byte[] buffer)
        {
            if (buffer == null) throw new ArgumentNullException("buffer");
            Contract.EndContractBlock();
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = (byte)(InternalSample() % (Byte.MaxValue + 1));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public fp NextNumber()
        {
            var high = (long)Next();
            var low = (long)Next();
            return fp.FromRaw((high << 32) + low);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public fp NextNumber(fp maxValue)
        {
            if (maxValue < 0) throw new ArgumentException("maxValue can't be less than 0!");
            const long lowmask = (1L << 32) - 1;
            const long highmask = ~lowmask;
            long high_max = (maxValue.RawValue & highmask) >> 32;
            long low_max = maxValue.RawValue & lowmask;
            long high = (long)Next(0, (int)high_max);
            long low = (long)Next(int.MinValue, high != high_max ? int.MaxValue : (int)(int.MinValue + low_max)) - (long)int.MinValue;
            return fp.FromRaw((high << 32) + low);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public fp NextNumber(fp minValue, fp maxValue)
        {
            if (minValue > maxValue) throw new ArgumentException("max can't be less than min!");
            if (minValue.RawValue < 0 && maxValue.RawValue > 0)
            {
                ulong range = (ulong)maxValue.RawValue + (ulong)(-minValue.RawValue);
                const ulong lowmask = (1L << 32) - 1;
                const ulong highmask = ~lowmask;
                long high_max = (long)((range & highmask) >> 32);
                long low_max = (long)(range & lowmask);
                long high = (long)Next(int.MinValue, (int)(int.MinValue + high_max)) - (long)int.MinValue;
                long low = (long)Next(int.MinValue, high != high_max ? int.MaxValue : (int)(int.MinValue + low_max)) - (long)int.MinValue;
                return fp.FromRaw((high << 32) + low + minValue.RawValue);
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
        public float2 NextNumber2(fp maxValue) => new float2()
        {
            x = NextNumber(maxValue),
            y = NextNumber(maxValue)
        };
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float2 NextNumber2(fp minValue, fp maxValue) => new float2()
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
        public float3 NextNumber3(fp maxValue) => new float3()
        {
            x = NextNumber(maxValue),
            y = NextNumber(maxValue),
            z = NextNumber(maxValue)
        };
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float3 NextNumber3(fp minValue, fp maxValue) => new float3()
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
        public float4 NextNumber4(fp maxValue) => new float4()
        {
            x = NextNumber(maxValue),
            y = NextNumber(maxValue),
            z = NextNumber(maxValue),
            w = NextNumber(maxValue)
        };
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float4 NextNumber4(fp minValue, fp maxValue) => new float4()
        {
            x = NextNumber(minValue, maxValue),
            y = NextNumber(minValue, maxValue),
            z = NextNumber(minValue, maxValue),
            w = NextNumber(minValue, maxValue)
        };
    }
}
#endif