﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExcelDna.Integration;
using ExcelDna.Integration.Rtd;
using Xl = Microsoft.Office.Interop.Excel;
using QLEX;

namespace QLExcel.Risk
{
    public class PFE
    {
        [ExcelFunction(Description = "PFE ", Category = "QLExcel - Risk")]
        public static string qlRiskPortfolio(
            [ExcelArgument(Description = "Portfolio Id ")] string ObjectId,
            [ExcelArgument(Description = "Instruments in portfolio ")] object[] instids,
            [ExcelArgument(Description = "trigger ")]object trigger)
        {
            if (ExcelUtil.CallFromWizard())
                return "";

            string callerAddress = "";
            callerAddress = ExcelUtil.getActiveCellAddress();

            try
            {
                List<EnergyCommodityExt> port_ = new List<EnergyCommodityExt>();

                foreach (string sid in instids)
                {
                    EnergyCommodityExt inst = OHRepository.Instance.getObject<EnergyCommodityExt>(sid);
                    port_.Add(inst);
                }

                string id = "Port@" + ObjectId;
                OHRepository.Instance.storeObject(id, port_, callerAddress);
                id += "#" + (String)DateTime.Now.ToString(@"HH:mm:ss");
                return id;
            }
            catch (Exception e)
            {
                ExcelUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                return e.Message;
            }
        }

        [ExcelFunction(Description = "Calibrate PCA within curves ", Category = "QLExcel - Risk")]
        public static object qlRiskCalibrateCurvePCA(
            [ExcelArgument(Description = "as of date ")] DateTime asofdate,
            [ExcelArgument(Description = "curve 1 ")] string curve1,
            [ExcelArgument(Description = "(optional) curve 2 ")] string curve2,
            [ExcelArgument(Description = "trigger ")]object trigger)
        {
            if (ExcelUtil.CallFromWizard())
                return "";

            string callerAddress = "";
            callerAddress = ExcelUtil.getActiveCellAddress();

            try
            {
                List<string> curves = new List<string>();
                curves.Add(curve1.ToUpper());

                if (!ExcelUtil.isNull(curve2) && (!string.IsNullOrEmpty(curve2)))
                {
                    curves.Add(curve2.ToUpper());
                }

                return asofdate;
            }
            catch (Exception e)
            {
                ExcelUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                return e.Message;
            }
        }

        [ExcelFunction(Description = "Calibrate ATMVol for each curves ", Category = "QLExcel - Risk")]
        public static object qlRiskCalibrateCurveATMVol(
            [ExcelArgument(Description = "as of date ")] DateTime asofdate,
            [ExcelArgument(Description = "curve 1 ")] string curve1,
            [ExcelArgument(Description = "(optional) curve 2 ")] string curve2,
            [ExcelArgument(Description = "trigger ")]object trigger)
        {
            if (ExcelUtil.CallFromWizard())
                return "";

            string callerAddress = "";
            callerAddress = ExcelUtil.getActiveCellAddress();

            try
            {
                List<string> curves = new List<string>();
                curves.Add(curve1.ToUpper());

                if (!ExcelUtil.isNull(curve2) && (!string.IsNullOrEmpty(curve2)))
                {
                    curves.Add(curve2.ToUpper());
                }

                return asofdate;
            }
            catch (Exception e)
            {
                ExcelUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                return e.Message;
            }
        }

        [ExcelFunction(Description = "Calibrate PCA corr between two curves ", Category = "QLExcel - Risk")]
        public static object qlRiskCalibrateCurvePCACorr(
            [ExcelArgument(Description = "as of date ")] DateTime asofdate,
            [ExcelArgument(Description = "curve 1 ")] string curve1,
            [ExcelArgument(Description = "curve 2 ")] string curve2,
            [ExcelArgument(Description = "number of factors ")] string nfactors,
            [ExcelArgument(Description = "trigger ")]object trigger)
        {
            if (ExcelUtil.CallFromWizard())
                return "";

            string callerAddress = "";
            callerAddress = ExcelUtil.getActiveCellAddress();

            try
            {
                if (ExcelUtil.isNull(nfactors))
                    nfactors = "3";

                List<string> curves = new List<string>();
                curves.Add(curve1.ToUpper());

                if (!ExcelUtil.isNull(curve2) && (!string.IsNullOrEmpty(curve2)))
                {
                    curves.Add(curve2.ToUpper());
                }
                else
                {
                    return asofdate;
                }

                return asofdate;
            }
            catch (Exception e)
            {
                ExcelUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                return e.Message;
            }
        }

        [ExcelFunction(Description = "Simulate two base curve ", Category = "QLExcel - Risk")]
        public static object qlRiskSimOneBaseCurve(
            [ExcelArgument(Description = "as of date ")] DateTime asofdate,
            [ExcelArgument(Description = "curve name ")] string curve,
            [ExcelArgument(Description = "trigger ")]object trigger)
        {
            if (ExcelUtil.CallFromWizard())
                return "";

            string callerAddress = "";
            callerAddress = ExcelUtil.getActiveCellAddress();

            try
            {
                return asofdate;
            }
            catch (Exception e)
            {
                ExcelUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                return e.Message;
            }
        }

        [ExcelFunction(Description = "Simulate two base curve ", Category = "QLExcel - Risk")]
        public static object qlRiskSimTwoBaseCurves(
            [ExcelArgument(Description = "as of date ")] DateTime asofdate,
            [ExcelArgument(Description = "curve 1 name ")] string curve1,
            [ExcelArgument(Description = "curve 2 name ")] string curve2,
            [ExcelArgument(Description = "trigger ")]object trigger)
        {
            if (ExcelUtil.CallFromWizard())
                return "";

            string callerAddress = "";
            callerAddress = ExcelUtil.getActiveCellAddress();

            try
            {
                return asofdate;
            }
            catch (Exception e)
            {
                ExcelUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                return e.Message;
            }
        }

        [ExcelFunction(Description = "PFE ", Category = "QLExcel - Risk")]
        public static string qlRiskGetTradePFE(
            [ExcelArgument(Description = "as of date ")] DateTime asofdate,
            [ExcelArgument(Description = "trade id ")] string tradeid,
            [ExcelArgument(Description = "trigger ")]object trigger)
        {
            if (ExcelUtil.CallFromWizard())
                return "";

            string callerAddress = "";
            callerAddress = ExcelUtil.getActiveCellAddress();

            try
            {
                return tradeid;
            }
            catch (Exception e)
            {
                ExcelUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                return e.Message;
            }
        }
    }
}
