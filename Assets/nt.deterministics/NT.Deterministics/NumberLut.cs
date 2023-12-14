using System;
using System.IO;
using System.Runtime.CompilerServices;
#if !NO_NUMBER_SUPPORT_BURST
using Unity.Burst;
using UnityEngine;
#endif

namespace Mathematics.FixedPoint
{
    public delegate byte[] LutProvider(string path);
    public class NumberLut
    {
#if !NO_NUMBER_SUPPORT_BURST
        public unsafe static readonly SharedStatic<LutPointer> sin_lut = SharedStatic<LutPointer>.GetOrCreate<NumberLut, SinLutPointerType>();
        public unsafe static readonly SharedStatic<LutPointer> cos_lut = SharedStatic<LutPointer>.GetOrCreate<NumberLut, CosLutPointerType>();
        public unsafe static readonly SharedStatic<LutPointer> tan_lut = SharedStatic<LutPointer>.GetOrCreate<NumberLut, TanLutPointerType>();
        public unsafe static readonly SharedStatic<LutPointer> asin_lut = SharedStatic<LutPointer>.GetOrCreate<NumberLut, AsinLutPointerType>();
        public unsafe static readonly SharedStatic<LutPointer> acos_lut = SharedStatic<LutPointer>.GetOrCreate<NumberLut, AcosLutPointerType>();
        public unsafe static readonly SharedStatic<LutPointer> atan_lut = SharedStatic<LutPointer>.GetOrCreate<NumberLut, AtanLutPointerType>();
        public unsafe static readonly SharedStatic<LutPointer> atan2_lut = SharedStatic<LutPointer>.GetOrCreate<NumberLut, Atan2LutPointerType>();
        public unsafe static readonly SharedStatic<LutPointer> sqrt_aprox_lut = SharedStatic<LutPointer>.GetOrCreate<NumberLut, SqrtLutPointerType>();
        public unsafe static readonly SharedStatic<LutPointer> normalize2_x_lut 
            = SharedStatic<LutPointer>.GetOrCreate<NumberLut, NormalizeXLutPointerType>();
        public unsafe static readonly SharedStatic<LutPointer> normalize2_y_lut 
            = SharedStatic<LutPointer>.GetOrCreate<NumberLut, NormalizeYLutPointerType>();
        public class SinLutPointerType { }
        public class CosLutPointerType { }
        public class TanLutPointerType { }
        public class AsinLutPointerType { }
        public class AcosLutPointerType { }
        public class AtanLutPointerType { }
        public class Atan2LutPointerType { }
        public class NormalizeXLutPointerType { }
        public class NormalizeYLutPointerType { }
        public class SqrtLutPointerType { }
#else
        public static long[] sin_lut;
        public static long[] cos_lut;
        public static long[] tan_lut;
        public static long[] asin_lut;
        public static long[] acos_lut;
        public static long[] atan_lut;
        public static long[] atan2_lut;
        public static long[] sqrt_aprox_lut;
        public static long[] normalize2_x_lut;
        public static long[] normalize2_y_lut;
#endif

        public static bool IsLoaded
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
#if !NO_NUMBER_SUPPORT_BURST
                return sin_lut.Data.isCreated && cos_lut.Data.isCreated && tan_lut.Data.isCreated
                    && asin_lut.Data.isCreated && acos_lut.Data.isCreated && atan_lut.Data.isCreated && atan2_lut.Data.isCreated
                    && sqrt_aprox_lut.Data.isCreated && normalize2_x_lut.Data.isCreated && normalize2_y_lut.Data.isCreated;
#else
                return sin_lut != null && sin_lut.Length != 0
                    && cos_lut != null && cos_lut.Length != 0
                    && tan_lut != null && tan_lut.Length != 0
                    && asin_lut != null && asin_lut.Length != 0
                    && acos_lut != null && acos_lut.Length != 0
                    && atan_lut != null && atan_lut.Length != 0
                    && atan2_lut != null && atan2_lut.Length != 0
                    && sqrt_aprox_lut != null && sqrt_aprox_lut.Length != 0
                    && normalize2_x_lut != null && normalize2_x_lut.Length != 0
                    && normalize2_y_lut != null && normalize2_y_lut.Length != 0;
#endif
            }
        }

        public static void Init(string directoryPath)
        {
#if !NO_NUMBER_SUPPORT_BURST
            Load(directoryPath, LutGenerator.TABLE_NAME_SIN, ref sin_lut.Data);
            Load(directoryPath, LutGenerator.TABLE_NAME_COS, ref cos_lut.Data);
            Load(directoryPath, LutGenerator.TABLE_NAME_TAN, ref tan_lut.Data);
            Load(directoryPath, LutGenerator.TABLE_NAME_ASIN, ref asin_lut.Data);
            Load(directoryPath, LutGenerator.TABLE_NAME_ACOS, ref acos_lut.Data);
            Load(directoryPath, LutGenerator.TABLE_NAME_ATAN, ref atan_lut.Data);
            Load(directoryPath, LutGenerator.TABLE_NAME_SQRT, ref sqrt_aprox_lut.Data);
            Load(directoryPath, LutGenerator.TABLE_NAME_NORMALIZE2_X, ref normalize2_x_lut.Data);
            Load(directoryPath, LutGenerator.TABLE_NAME_NORMALIZE2_Y, ref normalize2_y_lut.Data);
            Load(directoryPath, LutGenerator.TABLE_NAME_ATAN2, ref atan2_lut.Data);
#else
            Load(directoryPath, LutGenerator.TABLE_NAME_SIN, ref sin_lut);
            Load(directoryPath, LutGenerator.TABLE_NAME_COS, ref cos_lut);
            Load(directoryPath, LutGenerator.TABLE_NAME_TAN, ref tan_lut);
            Load(directoryPath, LutGenerator.TABLE_NAME_ASIN, ref asin_lut);
            Load(directoryPath, LutGenerator.TABLE_NAME_ACOS, ref acos_lut);
            Load(directoryPath, LutGenerator.TABLE_NAME_ATAN, ref atan_lut);
            Load(directoryPath, LutGenerator.TABLE_NAME_SQRT, ref sqrt_aprox_lut);
            Load(directoryPath, LutGenerator.TABLE_NAME_NORMALIZE2_X, ref normalize2_x_lut);
            Load(directoryPath, LutGenerator.TABLE_NAME_NORMALIZE2_Y, ref normalize2_y_lut);
            Load(directoryPath, LutGenerator.TABLE_NAME_ATAN2, ref atan2_lut);
#endif
        }

        public static void Init(LutProvider lutProvider)
        {
#if !NO_NUMBER_SUPPORT_BURST
            Load(lutProvider, LutGenerator.TABLE_NAME_SIN, ref sin_lut.Data);
            Load(lutProvider, LutGenerator.TABLE_NAME_COS, ref cos_lut.Data);
            Load(lutProvider, LutGenerator.TABLE_NAME_TAN, ref tan_lut.Data);
            Load(lutProvider, LutGenerator.TABLE_NAME_ASIN, ref asin_lut.Data);
            Load(lutProvider, LutGenerator.TABLE_NAME_ACOS, ref acos_lut.Data);
            Load(lutProvider, LutGenerator.TABLE_NAME_ATAN, ref atan_lut.Data);
            Load(lutProvider, LutGenerator.TABLE_NAME_ATAN2, ref atan2_lut.Data);
            Load(lutProvider, LutGenerator.TABLE_NAME_SQRT, ref sqrt_aprox_lut.Data);
            Load(lutProvider, LutGenerator.TABLE_NAME_NORMALIZE2_X, ref normalize2_x_lut.Data);
            Load(lutProvider, LutGenerator.TABLE_NAME_NORMALIZE2_Y, ref normalize2_y_lut.Data);
#else
            Load(lutProvider, LutGenerator.TABLE_NAME_SIN, ref sin_lut);
            Load(lutProvider, LutGenerator.TABLE_NAME_COS, ref cos_lut);
            Load(lutProvider, LutGenerator.TABLE_NAME_TAN, ref tan_lut);
            Load(lutProvider, LutGenerator.TABLE_NAME_ASIN, ref asin_lut);
            Load(lutProvider, LutGenerator.TABLE_NAME_ACOS, ref acos_lut);
            Load(lutProvider, LutGenerator.TABLE_NAME_ATAN, ref atan_lut);
            Load(lutProvider, LutGenerator.TABLE_NAME_SQRT, ref sqrt_aprox_lut);
            Load(lutProvider, LutGenerator.TABLE_NAME_NORMALIZE2_X, ref normalize2_x_lut);
            Load(lutProvider, LutGenerator.TABLE_NAME_NORMALIZE2_Y, ref normalize2_y_lut);
            Load(lutProvider, LutGenerator.TABLE_NAME_ATAN2, ref atan2_lut);
#endif
        }

        public static void Dispose()
        {
#if !NO_NUMBER_SUPPORT_BURST
            if (sin_lut.Data.isCreated) sin_lut.Data.Dispose();
            if (cos_lut.Data.isCreated) cos_lut.Data.Dispose();
            if (tan_lut.Data.isCreated) tan_lut.Data.Dispose();
            if (asin_lut.Data.isCreated) asin_lut.Data.Dispose();
            if (acos_lut.Data.isCreated) acos_lut.Data.Dispose();
            if (atan_lut.Data.isCreated) atan_lut.Data.Dispose();
            if (atan2_lut.Data.isCreated) atan2_lut.Data.Dispose();
            if (sqrt_aprox_lut.Data.isCreated) sqrt_aprox_lut.Data.Dispose();
            if (normalize2_x_lut.Data.isCreated) normalize2_x_lut.Data.Dispose();
            if (normalize2_y_lut.Data.isCreated) normalize2_y_lut.Data.Dispose();
#else
            sin_lut = null;
            cos_lut = null;
            tan_lut = null;
            asin_lut = null;
            acos_lut = null;
            atan_lut = null;
            atan2_lut = null;
            sqrt_aprox_lut = null;
            normalize2_x_lut = null;
            normalize2_y_lut = null;
#endif
        }

        public static void GenerateTables(string directoryPath)
        {
            LutGenerator.Generate(directoryPath);
        }

        private static void Load(LutProvider lutProvider, string path, ref long[] lut)
        {
            byte[] data = lutProvider(path);
            lut = new long[data.Length / 8];
            Buffer.BlockCopy(data, 0, lut, 0, data.Length);
        }

        private static void Load(string directoryPath, string filePath, ref long[] lut)
        {
            byte[] data = File.ReadAllBytes(Path.Combine(directoryPath, filePath) + ".bytes");
            lut = new long[data.Length / 8];
            Buffer.BlockCopy(data, 0, lut, 0, data.Length);
        }

        private unsafe static void Load(LutProvider lutProvider, string path, ref LutPointer pointer)
        {
            byte[] data = lutProvider(path);
            pointer.Allocate(data);
        }

        private unsafe static void Load(string directoryPath, string filePath, ref LutPointer pointer)
        {
            byte[] data = File.ReadAllBytes(Path.Combine(directoryPath, filePath) + ".bytes");
            pointer.Allocate(data);
        }
    }

    internal class LutGenerator
    {
        public const long ATAN_DENSITY1_COVER = 6L;
        public const long ATAN_DENSITY2_COVER = 250L;
        public const long ATAN_DENSITY3_COVER = 9994L;
        public const long ATAN_SIZE_DENSITY1 = ATAN_DENSITY1_COVER * fp.ONE;
        public const long ATAN_SIZE_DENSITY2 = 3904L;
        public const long ATAN_SIZE_DENSITY3 = 609L;
        public const long ATAN_DENSITY1_COVER_RAW = ATAN_DENSITY1_COVER * fp.ONE;
        public const long ATAN_DENSITY2_COVER_RAW = ATAN_DENSITY2_COVER * fp.ONE;
        public const long ATAN_DENSITY3_COVER_RAW = ATAN_DENSITY3_COVER * fp.ONE;
        public const long ATAN_DENSITY1_INDEX_BEGIN = 0L;
        public const long ATAN_DENSITY2_INDEX_BEGIN = ATAN_SIZE_DENSITY1;
        public const long ATAN_DENSITY3_INDEX_BEGIN = ATAN_SIZE_DENSITY1 + ATAN_SIZE_DENSITY2;
        public const string TABLE_NAME_SIN = "FPSin";
        public const string TABLE_NAME_COS = "FPCos";
        public const string TABLE_NAME_TAN = "FPTan";
        public const string TABLE_NAME_ASIN = "FPAsin";
        public const string TABLE_NAME_ACOS = "FPAcos";
        public const string TABLE_NAME_ATAN = "FPAtan";
        public const string TABLE_NAME_SQRT = "FPSqrt";
        public const string TABLE_NAME_NORMALIZE2_X = "FP2NormalizeX";
        public const string TABLE_NAME_NORMALIZE2_Y = "FP2NormalizeY";
        public const string TABLE_NAME_ATAN2 = "FP2Atan2";
        private static void Generate(Func<double, double> op, string file, long min, long max)
        {
            file += ".bytes";
            if (File.Exists(file))
                File.Delete(file);
            using (FileStream fs = File.OpenWrite(file))
            {
                long ONE = 1L << fp.FRACTIONAL_PLACES;
                for (long i = min; i <= max; i += 1L)
                {
                    long value = (long)Math.Round(op((double)i / ONE) * ONE);
                    fs.Write(BitConverter.GetBytes(value), 0, 8);
                }
                fs.Flush();
            }
        }

        private static void GenerateSqrt(string file)
        {
            file += ".bytes";
            if (File.Exists(file))
                File.Delete(file);
            using (FileStream fs = File.OpenWrite(file))
            {
                for (long i = 0L; i <= 3L * fp.ONE; i += 3L)
                {
                    long value = (long)(Math.Sqrt((double)fp.FromRaw(fp.ONE + i)) * fp.ONE);
                    fs.Write(BitConverter.GetBytes(value), 0, 8);
                }
                fs.Flush();
            }
        }

        public static void GenerateForPiOver2Space(Func<double, double> op, string file)
        {
            file += ".bytes";
            if (File.Exists(file))
                File.Delete(file);
            using (FileStream fs = File.OpenWrite(file))
            {
                for (long i = -fp.RawPiLong; i <= fp.RawPiLong; i += 1L)
                {
                    long value = (long)(op((double)fp.FromRaw(i)) * fp.ONE);
                    fs.Write(BitConverter.GetBytes(value), 0, 8);
                }
                fs.Flush();
            }
        }

        public static void GenerateAtan(string file)
        {
            file += ".bytes";
            if (File.Exists(file))
                File.Delete(file);
            using (FileStream fs = File.OpenWrite(file))
            {
                //[0, 6]
                for (long i = 0L; i <= ATAN_SIZE_DENSITY1; i += 1L)
                {
                    long value = (long)Math.Round(Math.Atan((double)i / fp.ONE) * fp.ONE);
                    fs.Write(BitConverter.GetBytes(value), 0, 8);
                }
                //(6, 250], step 0.0625
                long baseNumber = ATAN_SIZE_DENSITY1;
                long rawStep = fp.ONE / 16L;
                for (long j = 0L; j < ATAN_SIZE_DENSITY2; j += 1L)
                {
                    baseNumber += rawStep;
                    long value2 = (long)Math.Round(Math.Atan((double)baseNumber / fp.ONE) * fp.ONE);
                    fs.Write(BitConverter.GetBytes(value2), 0, 8);
                }
                //[250, 9994], step 16
                long rawStep2 = 16L * fp.ONE;
                for (long k = 0L; k < ATAN_SIZE_DENSITY3; k += 1L)
                {
                    baseNumber += rawStep2;
                    long value3 = (long)Math.Round(Math.Atan((double)baseNumber / fp.ONE) * fp.ONE);
                    fs.Write(BitConverter.GetBytes(value3), 0, 8);
                }
                fs.Flush();
            }
        }

        public static void GenerateNormalizeNumber2(string fileX, string fileY)
        {
            fileX += ".bytes";
            fileY += ".bytes";
            if (File.Exists(fileX)) File.Delete(fileX);
            if (File.Exists(fileY)) File.Delete(fileY);
            using (FileStream fsX = File.OpenWrite(fileX), fsY = File.OpenWrite(fileY))
            {
                for (int k = 0; k <= fp.ONE; ++k)
                {
                    double _atan = Math.Atan((double)k / fp.ONE);
                    long valueX = (long)(fp.ONE * Math.Cos(_atan));
                    long valueY = (long)(fp.ONE * Math.Sin(_atan));
                    fsX.Write(BitConverter.GetBytes(valueX), 0, 8);
                    fsY.Write(BitConverter.GetBytes(valueY), 0, 8);
                }
                fsX.Flush();
                fsY.Flush();
            }
        }

        /// <summary>
        /// 生成float2与XAixs在[0, Pi/4]中的夹角
        /// </summary>
        /// <param name="file">输出文件</param>
        public static void GenerateAtan2(string file)
        {
            file += ".bytes";
            if (File.Exists(file)) File.Delete(file);
            using (FileStream fs = File.OpenWrite(file))
            {
                for (int k = 0; k <= fp.ONE; ++k)
                {
                    long _atan = (long)(fp.ONE * Math.Atan((double)k / fp.ONE));
                    fs.Write(BitConverter.GetBytes(_atan), 0, 8);
                }
                fs.Flush();
            }
        }

        public static void Generate(string directoryPath)
        {
            LutGenerator.GenerateForPiOver2Space(new Func<double, double>(Math.Sin), Path.Combine(directoryPath, TABLE_NAME_SIN));
            LutGenerator.GenerateForPiOver2Space(new Func<double, double>(Math.Cos), Path.Combine(directoryPath, TABLE_NAME_COS));
            LutGenerator.Generate(new Func<double, double>(Math.Tan), Path.Combine(directoryPath, TABLE_NAME_TAN), -fp.RawPiLong, fp.RawPiLong);
            LutGenerator.Generate(new Func<double, double>(Math.Asin), Path.Combine(directoryPath, TABLE_NAME_ASIN), -fp.ONE, fp.ONE);
            LutGenerator.Generate(new Func<double, double>(Math.Acos), Path.Combine(directoryPath, TABLE_NAME_ACOS), -fp.ONE, fp.ONE);
            LutGenerator.GenerateAtan(Path.Combine(directoryPath, TABLE_NAME_ATAN));
            LutGenerator.GenerateSqrt(Path.Combine(directoryPath, TABLE_NAME_SQRT));
            LutGenerator.GenerateNormalizeNumber2(Path.Combine(directoryPath, TABLE_NAME_NORMALIZE2_X), 
                Path.Combine(directoryPath, TABLE_NAME_NORMALIZE2_Y));
            LutGenerator.GenerateAtan2(Path.Combine(directoryPath, TABLE_NAME_ATAN2));
        }
    }
}
