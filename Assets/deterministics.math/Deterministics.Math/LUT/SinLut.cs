namespace Deterministics.Math {
    public static partial class fixlut {
        public static readonly int[] SinLut = {
            0, 804, 1608, 2412, 3216, 4019, 4821, 5623, 6424, 7224,
            8022, 8820, 9616, 10411, 11204, 11996, 12785, 13573, 14359, 15143,
            15924, 16703, 17479, 18253, 19024, 19792, 20557, 21320, 22078, 22834,
            23586, 24335, 25080, 25821, 26558, 27291, 28020, 28745, 29466, 30182,
            30893, 31600, 32303, 33000, 33692, 34380, 35062, 35738, 36410, 37076,
            37736, 38391, 39040, 39683, 40320, 40951, 41576, 42194, 42806, 43412,
            44011, 44604, 45190, 45769, 46341, 46906, 47464, 48015, 48559, 49095,
            49624, 50146, 50660, 51166, 51665, 52156, 52639, 53114, 53581, 54040,
            54491, 54934, 55368, 55794, 56212, 56621, 57022, 57414, 57798, 58172,
            58538, 58896, 59244, 59583, 59914, 60235, 60547, 60851, 61145, 61429,
            61705, 61971, 62228, 62476, 62714, 62943, 63162, 63372, 63572, 63763,
            63944, 64115, 64277, 64429, 64571, 64704, 64827, 64940, 65043, 65137,
            65220, 65294, 65358, 65413, 65457, 65492, 65516, 65531, 65536, 65531,
            65516, 65492, 65457, 65413, 65358, 65294, 65220, 65137, 65043, 64940,
            64827, 64704, 64571, 64429, 64277, 64115, 63944, 63763, 63572, 63372,
            63162, 62943, 62714, 62476, 62228, 61971, 61705, 61429, 61145, 60851,
            60547, 60235, 59914, 59583, 59244, 58896, 58538, 58172, 57798, 57414,
            57022, 56621, 56212, 55794, 55368, 54934, 54491, 54040, 53581, 53114,
            52639, 52156, 51665, 51166, 50660, 50146, 49624, 49095, 48559, 48015,
            47464, 46906, 46341, 45769, 45190, 44604, 44011, 43412, 42806, 42194,
            41576, 40951, 40320, 39683, 39040, 38391, 37736, 37076, 36410, 35738,
            35062, 34380, 33692, 33000, 32303, 31600, 30893, 30182, 29466, 28745,
            28020, 27291, 26558, 25821, 25080, 24335, 23586, 22834, 22078, 21320,
            20557, 19792, 19024, 18253, 17479, 16703, 15924, 15143, 14359, 13573,
            12785, 11996, 11204, 10411, 9616, 8820, 8022, 7224, 6424, 5623,
            4821, 4019, 3216, 2412, 1608, 804, 0, -804, -1608, -2412,
            -3216, -4019, -4821, -5623, -6424, -7224, -8022, -8820, -9616, -10411,
            -11204, -11996, -12785, -13573, -14359, -15143, -15924, -16703, -17479, -18253,
            -19024, -19792, -20557, -21320, -22078, -22834, -23586, -24335, -25080, -25821,
            -26558, -27291, -28020, -28745, -29466, -30182, -30893, -31600, -32303, -33000,
            -33692, -34380, -35062, -35738, -36410, -37076, -37736, -38391, -39040, -39683,
            -40320, -40951, -41576, -42194, -42806, -43412, -44011, -44604, -45190, -45769,
            -46341, -46906, -47464, -48015, -48559, -49095, -49624, -50146, -50660, -51166,
            -51665, -52156, -52639, -53114, -53581, -54040, -54491, -54934, -55368, -55794,
            -56212, -56621, -57022, -57414, -57798, -58172, -58538, -58896, -59244, -59583,
            -59914, -60235, -60547, -60851, -61145, -61429, -61705, -61971, -62228, -62476,
            -62714, -62943, -63162, -63372, -63572, -63763, -63944, -64115, -64277, -64429,
            -64571, -64704, -64827, -64940, -65043, -65137, -65220, -65294, -65358, -65413,
            -65457, -65492, -65516, -65531, -65536, -65531, -65516, -65492, -65457, -65413,
            -65358, -65294, -65220, -65137, -65043, -64940, -64827, -64704, -64571, -64429,
            -64277, -64115, -63944, -63763, -63572, -63372, -63162, -62943, -62714, -62476,
            -62228, -61971, -61705, -61429, -61145, -60851, -60547, -60235, -59914, -59583,
            -59244, -58896, -58538, -58172, -57798, -57414, -57022, -56621, -56212, -55794,
            -55368, -54934, -54491, -54040, -53581, -53114, -52639, -52156, -51665, -51166,
            -50660, -50146, -49624, -49095, -48559, -48015, -47464, -46906, -46341, -45769,
            -45190, -44604, -44011, -43412, -42806, -42194, -41576, -40951, -40320, -39683,
            -39040, -38391, -37736, -37076, -36410, -35738, -35062, -34380, -33692, -33000,
            -32303, -31600, -30893, -30182, -29466, -28745, -28020, -27291, -26558, -25821,
            -25080, -24335, -23586, -22834, -22078, -21320, -20557, -19792, -19024, -18253,
            -17479, -16703, -15924, -15143, -14359, -13573, -12785, -11996, -11204, -10411,
            -9616, -8820, -8022, -7224, -6424, -5623, -4821, -4019, -3216, -2412,
            -1608, -804, 0
        };
    }
}