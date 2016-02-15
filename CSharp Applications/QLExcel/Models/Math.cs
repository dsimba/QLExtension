using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExcelDna.Integration;
using ExcelDna.Integration.Rtd;
using Xl = Microsoft.Office.Interop.Excel;
using QLEX;

namespace QLExcel.Models
{
    public class Math
    {
        #region Threefry
        [ExcelFunction(Description = "Generate Threefry PRNG", Category = "QLExcel - Math")]
        public static object qlMathGenThreefryURng(
            [ExcelArgument(Description = "seed of the rng (can't be zero)")] int seed,
            [ExcelArgument(Description = "skip (jump forward) ")]int skip)
        {
            if (ExcelUtil.CallFromWizard())
                return "";

            string callerAddress = "";
            callerAddress = ExcelUtil.getActiveCellAddress();
            Xl.Range range = ExcelUtil.getActiveCellRange();
            int c = range.Count;        // range should be one column

            try
            {
                double[,] ret = new double[c,1];
                double[] ret0 = new double[c];

                QLEX.NQuantLibc.uniformthreefry(seed, skip, ret0, c);

                for (int i = 0; i < c; i++)
                {
                    ret[i, 0] = ret0[i];
                }

                return ret;
            }
            catch (Exception e)
            {
                ExcelUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                return e.Message;
            }
        }

        [ExcelFunction(Description = "Generate Threefry PRNG", Category = "QLExcel - Math")]
        public static object qlMathGenThreefryGaussianRng(
            [ExcelArgument(Description = "seed of the rng (can't be zero)")] int seed,
            [ExcelArgument(Description = "skip (jump forward) ")]int skip)
        {
            if (ExcelUtil.CallFromWizard())
                return "";

            string callerAddress = "";
            callerAddress = ExcelUtil.getActiveCellAddress();
            Xl.Range range = ExcelUtil.getActiveCellRange();
            int c = range.Count;        // range should be one column

            try
            {
                double[,] ret = new double[c, 1];
                double[] ret0 = new double[c];

                QLEX.NQuantLibc.normalthreefry(seed, skip, ret0, c);

                for (int i = 0; i < c; i++)
                {
                    ret[i, 0] = ret0[i];
                }

                return ret;
            }
            catch (Exception e)
            {
                ExcelUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                return e.Message;
            }
        }

        [ExcelFunction(Description = "Generate Threefry PRNG", Category = "QLExcel - Math")]
        public static object qlMathGenThreefryGammaRng(
            [ExcelArgument(Description = "seed of the rng (can't be zero)")] int seed,
            [ExcelArgument(Description = "skip (jump forward) ")]int skip,
            [ExcelArgument(Description = "shape (shape) ")]double shape,
                [ExcelArgument(Description = "scale (scale) ")]double scale)
        {
            if (ExcelUtil.CallFromWizard())
                return "";

            string callerAddress = "";
            callerAddress = ExcelUtil.getActiveCellAddress();
            Xl.Range range = ExcelUtil.getActiveCellRange();
            int c = range.Count;        // range should be one column

            try
            {
                double[,] ret = new double[c, 1];
                double[] ret0 = new double[c];

                QLEX.NQuantLibc.gammathreefry(seed, skip, ret0, c, shape, scale);

                for (int i = 0; i < c; i++)
                {
                    ret[i, 0] = ret0[i];
                }

                return ret;
            }
            catch (Exception e)
            {
                ExcelUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                return e.Message;
            }
        }
        /*
        [ExcelFunction(Description = "Generate Threefry PRNG", Category = "QLExcel - Math")]
        public static object qlMathGenThreefryGaussianRng(
            [ExcelArgument(Description = "seed of the rng (can't be zero)")] int seed,
            [ExcelArgument(Description = "(restart)counterbase ")]int counterbase,
            [ExcelArgument(Description = "skip (jump forward) ")]int skip)
        {
            if (SystemUtil.CallFromWizard())
                return "";

            string callerAddress = "";
            callerAddress = SystemUtil.getActiveCellAddress();
            Xl.Range range = SystemUtil.getActiveCellRange();
            int c = range.Count;        // range should be one column

            if (seed == 0)
            {
                Random r = new Random();
                counterbase = r.Next(10000);
            }

            try
            {
                double[,] ret = new double[c, 1];
                QLEX.BoostThreefryUniformRng rng = new BoostThreefryUniformRng(seed);
                rng.restart(counterbase);
                rng.discard(skip);
                QLEX.InvCumulativeThreefryGaussianRng rngN = new InvCumulativeThreefryGaussianRng(rng);
                
                for (int i = 0; i < c; i++)
                {
                    ret[i, 0] = rngN.next().value();
                }

                return ret;
            }
            catch (Exception e)
            {
                SystemUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                return e.Message;
            }
        }

        [ExcelFunction(Description = "Generate Threefry PRNG", Category = "QLExcel - Math")]
        public static object qlMathGenThreefryStudentRng(
            [ExcelArgument(Description = "seed of the rng (can't be zero)")] int seed,
            [ExcelArgument(Description = "(restart)counterbase ")]int counterbase,
            [ExcelArgument(Description = "skip (jump forward) ")]int skip,
            [ExcelArgument(Description = "Student t Param ")]int N)
        {
            if (SystemUtil.CallFromWizard())
                return "";

            string callerAddress = "";
            callerAddress = SystemUtil.getActiveCellAddress();
            Xl.Range range = SystemUtil.getActiveCellRange();
            int c = range.Count;        // range should be one column

            if (seed == 0)
            {
                Random r = new Random();
                counterbase = r.Next(10000);
            }

            try
            {
                double[,] ret = new double[c, 1];
                QLEX.BoostThreefryUniformRng rng = new BoostThreefryUniformRng(seed);
                rng.restart(counterbase);
                rng.discard(skip);
                InverseCumulativeStudent ic = new InverseCumulativeStudent(N);
                QLEX.InvCumulativeThreefryStudentRng rngT = new InvCumulativeThreefryStudentRng(rng, ic);

                for (int i = 0; i < c; i++)
                {
                    ret[i, 0] = rngT.next().value();
                }

                return ret;
            }
            catch (Exception e)
            {
                SystemUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                return e.Message;
            }
        }

        [ExcelFunction(Description = "Generate Threefry PRNG", Category = "QLExcel - Math")]
        public static object qlMathGenThreefryPoissonRng(
            [ExcelArgument(Description = "seed of the rng (can't be zero)")] int seed,
            [ExcelArgument(Description = "(restart)counterbase ")]int counterbase,
            [ExcelArgument(Description = "skip (jump forward) ")]int skip,
            [ExcelArgument(Description = "Poisson Param ")]double lambda)
        {
            if (SystemUtil.CallFromWizard())
                return "";

            string callerAddress = "";
            callerAddress = SystemUtil.getActiveCellAddress();
            Xl.Range range = SystemUtil.getActiveCellRange();
            int c = range.Count;        // range should be one column

            if (seed == 0)
            {
                Random r = new Random();
                counterbase = r.Next(10000);
            }

            try
            {
                double[,] ret = new double[c, 1];
                QLEX.BoostThreefryUniformRng rng = new BoostThreefryUniformRng(seed);
                rng.restart(counterbase);
                rng.discard(skip);
                InverseCumulativePoisson ic = new InverseCumulativePoisson(lambda);
                QLEX.InvCumulativeThreefryPoissonRng rngP = new InvCumulativeThreefryPoissonRng(rng, ic);

                for (int i = 0; i < c; i++)
                {
                    ret[i, 0] = rngP.next().value();
                }

                return ret;
            }
            catch (Exception e)
            {
                SystemUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                return e.Message;
            }
        }
        #endregion

        #region Philox
        [ExcelFunction(Description = "Generate Philox PRNG", Category = "QLExcel - Math")]
        public static object qlMathGenPhiloxURng(
            [ExcelArgument(Description = "seed of the rng (can't be zero)")] int seed,
            [ExcelArgument(Description = "(restart)counterbase ")]int counterbase,
            [ExcelArgument(Description = "skip (jump forward) ")]int skip)
        {
            if (SystemUtil.CallFromWizard())
                return "";

            string callerAddress = "";
            callerAddress = SystemUtil.getActiveCellAddress();
            Xl.Range range = SystemUtil.getActiveCellRange();
            int c = range.Count;        // range should be one column

            if (seed == 0)
            {
                Random r = new Random();
                counterbase = r.Next(10000);
            }

            try
            {
                double[,] ret = new double[c, 1];
                QLEX.BoostPhiloxUniformRng rng = new BoostPhiloxUniformRng(seed);
                rng.restart(counterbase);
                rng.discard(skip);

                for (int i = 0; i < c; i++)
                {
                    ret[i, 0] = rng.next().value();
                }

                return ret;
            }
            catch (Exception e)
            {
                SystemUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                return e.Message;
            }
        }

        [ExcelFunction(Description = "Generate Philox PRNG", Category = "QLExcel - Math")]
        public static object qlMathGenPhiloxGaussianRng(
            [ExcelArgument(Description = "seed of the rng (can't be zero)")] int seed,
            [ExcelArgument(Description = "(restart)counterbase ")]int counterbase,
            [ExcelArgument(Description = "skip (jump forward) ")]int skip)
        {
            if (SystemUtil.CallFromWizard())
                return "";

            string callerAddress = "";
            callerAddress = SystemUtil.getActiveCellAddress();
            Xl.Range range = SystemUtil.getActiveCellRange();
            int c = range.Count;        // range should be one column

            if (seed == 0)
            {
                Random r = new Random();
                counterbase = r.Next(10000);
            }

            try
            {
                double[,] ret = new double[c, 1];
                QLEX.BoostPhiloxUniformRng rng = new BoostPhiloxUniformRng(seed);
                rng.restart(counterbase);
                rng.discard(skip);
                QLEX.InvCumulativePhiloxGaussianRng rngN = new InvCumulativePhiloxGaussianRng(rng);

                for (int i = 0; i < c; i++)
                {
                    ret[i, 0] = rngN.next().value();
                }

                return ret;
            }
            catch (Exception e)
            {
                SystemUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                return e.Message;
            }
        }

        [ExcelFunction(Description = "Generate Philox PRNG", Category = "QLExcel - Math")]
        public static object qlMathGenPhiloxStudentRng(
            [ExcelArgument(Description = "seed of the rng (can't be zero)")] int seed,
            [ExcelArgument(Description = "(restart)counterbase ")]int counterbase,
            [ExcelArgument(Description = "skip (jump forward) ")]int skip,
            [ExcelArgument(Description = "Student t Param ")]int N)
        {
            if (SystemUtil.CallFromWizard())
                return "";

            string callerAddress = "";
            callerAddress = SystemUtil.getActiveCellAddress();
            Xl.Range range = SystemUtil.getActiveCellRange();
            int c = range.Count;        // range should be one column

            if (seed == 0)
            {
                Random r = new Random();
                counterbase = r.Next(10000);
            }

            try
            {
                double[,] ret = new double[c, 1];
                QLEX.BoostPhiloxUniformRng rng = new BoostPhiloxUniformRng(seed);
                rng.restart(counterbase);
                rng.discard(skip);
                InverseCumulativeStudent ic = new InverseCumulativeStudent(N);
                QLEX.InvCumulativePhiloxStudentRng rngT = new InvCumulativePhiloxStudentRng(rng, ic);

                for (int i = 0; i < c; i++)
                {
                    ret[i, 0] = rngT.next().value();
                }

                return ret;
            }
            catch (Exception e)
            {
                SystemUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                return e.Message;
            }
        }

        [ExcelFunction(Description = "Generate Philox PRNG", Category = "QLExcel - Math")]
        public static object qlMathGenPhiloxPoissonRng(
            [ExcelArgument(Description = "seed of the rng (can't be zero)")] int seed,
            [ExcelArgument(Description = "(restart)counterbase ")]int counterbase,
            [ExcelArgument(Description = "skip (jump forward) ")]int skip,
            [ExcelArgument(Description = "Poisson Param ")]double lambda)
        {
            if (SystemUtil.CallFromWizard())
                return "";

            string callerAddress = "";
            callerAddress = SystemUtil.getActiveCellAddress();
            Xl.Range range = SystemUtil.getActiveCellRange();
            int c = range.Count;        // range should be one column

            if (seed == 0)
            {
                Random r = new Random();
                counterbase = r.Next(10000);
            }

            try
            {
                double[,] ret = new double[c, 1];
                QLEX.BoostPhiloxUniformRng rng = new BoostPhiloxUniformRng(seed);
                rng.restart(counterbase);
                rng.discard(skip);
                InverseCumulativePoisson ic = new InverseCumulativePoisson(lambda);
                QLEX.InvCumulativePhiloxPoissonRng rngP = new InvCumulativePhiloxPoissonRng(rng, ic);

                for (int i = 0; i < c; i++)
                {
                    ret[i, 0] = rngP.next().value();
                }

                return ret;
            }
            catch (Exception e)
            {
                SystemUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                return e.Message;
            }
        }*/
        #endregion

        #region Interpolation
        [ExcelFunction(Description = "One Dimensional interpolation", Category = "QLExcel - Math")]
        public static string qlMathLinearInterpolation(
            [ExcelArgument(Description = "interpolation obj id")] string objId,
            [ExcelArgument(Description = "variable x ")]double[] x,
            [ExcelArgument(Description = "variable y ")]double[] y)
        {
            if (ExcelUtil.CallFromWizard())
                return "";

            string callerAddress = "";
            callerAddress = ExcelUtil.getActiveCellAddress();

            try
            {
                if (x.Length != y.Length)
                {
                    return "size mismatch";
                }

                QlArray xa = new QlArray((uint)x.Length);
                QlArray ya = new QlArray((uint)y.Length);

                for (uint i = 0; i < x.Length; i++)
                {
                    xa.set(i, x[i]);
                    ya.set(i, y[i]);
                }

                LinearInterpolation interp = new LinearInterpolation(xa, ya);

                // Store the futures and return its id
                string id = "Int@" + objId;
                OHRepository.Instance.storeObject(id, interp, callerAddress);
                id += "#" + (String)DateTime.Now.ToString(@"HH:mm:ss");
                return id;
            }
            catch (Exception e)
            {
                ExcelUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                return e.Message;
            }
        }

        [ExcelFunction(Description = "One Dimensional interpolation", Category = "QLExcel - Math")]
        public static string qlMathLogLinearInterpolation(
            [ExcelArgument(Description = "interpolation obj id")] string objId,
            [ExcelArgument(Description = "variable x ")]double[] x,
            [ExcelArgument(Description = "variable y ")]double[] y)
        {
            if (ExcelUtil.CallFromWizard())
                return "";

            string callerAddress = "";
            callerAddress = ExcelUtil.getActiveCellAddress();

            try
            {
                if (x.Length != y.Length)
                {
                    return "size mismatch";
                }

                QlArray xa = new QlArray((uint)x.Length);
                QlArray ya = new QlArray((uint)y.Length);

                for (uint i = 0; i < x.Length; i++)
                {
                    xa.set(i, x[i]);
                    ya.set(i, y[i]);
                }

                LogLinearInterpolation interp = new LogLinearInterpolation(xa, ya);

                // Store the futures and return its id
                string id = "Int@" + objId;
                OHRepository.Instance.storeObject(id, interp, callerAddress);
                id += "#" + (String)DateTime.Now.ToString(@"HH:mm:ss");
                return id;
            }
            catch (Exception e)
            {
                ExcelUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                return e.Message;
            }
        }

        [ExcelFunction(Description = "One Dimensional interpolation", Category = "QLExcel - Math")]
        public static object qlMathGet1DInterpolation(
            [ExcelArgument(Description = "interpolation obj id")] string objId,
            [ExcelArgument(Description = "variable x ")]double x,
            [ExcelArgument(Description = "Linear/LogLinear ")]string type)
        {
            if (ExcelUtil.CallFromWizard())
                return "";

            string callerAddress = "";
            callerAddress = ExcelUtil.getActiveCellAddress();

            try
            {
                if (type.ToUpper() == "LINEAR")
                {
                    LinearInterpolation interp = OHRepository.Instance.getObject<LinearInterpolation>(objId);
                    double ret = interp.call(x, true);

                    return ret;
                }
                else if (type.ToUpper() == "LOGLINEAR")
                {
                    LogLinearInterpolation interp = OHRepository.Instance.getObject<LogLinearInterpolation>(objId);
                    double ret = interp.call(x, true);

                    return ret;
                }
                else
                {
                    return "Unknown interpolation type";
                }
                
            }
            catch (Exception e)
            {
                ExcelUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                return e.Message;
            }
        }

        [ExcelFunction(Description = "Two Dimensional interpolation", Category = "QLExcel - Math")]
        public static string qlMathBiLinearInterpolation(
            [ExcelArgument(Description = "interpolation obj id")] string objId,
            [ExcelArgument(Description = "row variable x ")]double[] x,
            [ExcelArgument(Description = "col variable y ")]double[] y,
            [ExcelArgument(Description = "variable z ")]double[,] z)
        {
            if (ExcelUtil.CallFromWizard())
                return "";

            string callerAddress = "";
            callerAddress = ExcelUtil.getActiveCellAddress();

            try
            {
                if ((x.Length != z.GetLength(0)) || (y.Length != z.GetLength(1)))
                {
                    return "size mismatch";
                }

                QlArray xa = new QlArray((uint)x.Length);
                QlArray ya = new QlArray((uint)y.Length);
                for (uint i = 0; i < x.Length; i++)
                {
                    xa.set(i, x[i]);
                    ya.set(i, y[i]);
                }

                Matrix ma = new Matrix((uint)x.Length, (uint)y.Length);
                for (uint i = 0; i < x.Length; i++)
                {
                    for (uint j = 0; j < y.Length; j++)
                    {
                        ma.set(i, j, z[i, j]);
                    }
                }

                BilinearInterpolation interp = new BilinearInterpolation(xa, ya, ma);

                // Store the futures and return its id
                string id = "Int@" + objId;
                OHRepository.Instance.storeObject(id, interp, callerAddress);
                id += "#" + (String)DateTime.Now.ToString(@"HH:mm:ss");
                return id;
            }
            catch (Exception e)
            {
                ExcelUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                return e.Message;
            }
        }

        [ExcelFunction(Description = "Two Dimensional interpolation", Category = "QLExcel - Math")]
        public static object qlMathGet2DInterpolation(
            [ExcelArgument(Description = "interpolation obj id")] string objId,
            [ExcelArgument(Description = "row variable x ")]double x,
            [ExcelArgument(Description = "col variable y ")]double y)
        {
            if (ExcelUtil.CallFromWizard())
                return "";

            string callerAddress = "";
            callerAddress = ExcelUtil.getActiveCellAddress();

            try
            {
                BilinearInterpolation interp = OHRepository.Instance.getObject<BilinearInterpolation>(objId);

                double ret = interp.call(x, y);
                return ret;
            }
            catch (Exception e)
            {
                ExcelUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                return e.Message;
            }
        }
        #endregion
    }
}
