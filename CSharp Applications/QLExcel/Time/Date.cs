/****************************** Project Header ******************************\
Project:	      QuantLib Extension (QLExtension)
Author:			  Letian.zj@gmail.com
URL:			  https://github.com/letianzj/QLExtension
Copyright 2014 Letian_zj
This file is part of  QuantLib Extension (QLExtension) Project.
QuantLib Extension (QLExtension) is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, 
either version 3 of the License, or (at your option) any later version.
QuantTrading is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. 
See the GNU General Public License for more details.
You should have received a copy of the GNU General Public License along with QLExtension. 
If not, see http://www.gnu.org/licenses/.
\***************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExcelDna.Integration;
using ExcelDna.Integration.Rtd;
using Xl = Microsoft.Office.Interop.Excel;
using QLEX;

namespace QLExcel
{
    public class qlTime
    {
        [ExcelFunction(Description = "today's date (non volatile function)", Category = "QLExcel - Time")]
        public static Object qlTimeToday(bool withTime)
        {
            if (withTime)
                return DateTime.Now;
            else
                return DateTime.Today;
        }

        //[ExcelFunction(IsVolatile=true)]
        [ExcelFunction(Description = "set the evaluation date of the whole spreadsheet", Category = "QLExcel - Time")]
        public static object qlTimeSetEvaluationDate(
            [ExcelArgument(Description = "Evaluation Date ")]DateTime date)
        {
            if (ExcelUtil.CallFromWizard())
                return false;

            string callerAddress = "";
            callerAddress = ExcelUtil.getActiveCellAddress();

            QLEX.Date todaysDate; 
            try
            {
                if (date == DateTime.MinValue)
                    todaysDate = QLEX.QLConverter.ConvertObject<QLEX.Date>(DateTime.Today);
                else
                    todaysDate = QLEX.QLConverter.ConvertObject<QLEX.Date>(date);

                QLEX.Settings.instance().setEvaluationDate(todaysDate);

                return QLEX.QLConverter.ConvertObject<DateTime>(todaysDate);
            }
            catch (Exception e)
            {
                ExcelUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                return false;
            }
        }

        [ExcelFunction(Description = "Get the evaluation date of the whole spreadsheet", Category = "QLExcel - Time")]
        public static object qlTimeGetEvaluationDate()
        {
            if (ExcelUtil.CallFromWizard())
                return false;

            string callerAddress = "";
            callerAddress = ExcelUtil.getActiveCellAddress();

            try
            {
                Date dt = QLEX.Settings.instance().getEvaluationDate();
                return QLEX.QLConverter.ConvertObject<DateTime>(dt);
            }
            catch (Exception e)
            {
                ExcelUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                return e.Message;
            }
        }

        [ExcelFunction(Description = "calculate year fraction between two dates", Category = "QLExcel - Time")]
        public static object qlTimeYearFraction(
            [ExcelArgument(Description = "Start Date ")]DateTime date1,
            [ExcelArgument(Description = "End Date ")]DateTime date2,
            [ExcelArgument(Description = "Day Counter (default ActualActual) ")]string dc = "ActualActual")
        {
            if (ExcelUtil.CallFromWizard())
                return "";

            string callerAddress = "";
            callerAddress = ExcelUtil.getActiveCellAddress();
            OHRepository.Instance.removeErrorMessage(callerAddress);

            try
            {
                if ((date1 == DateTime.MinValue) || (date2 == DateTime.MinValue))
                    throw new Exception("Date must not be empty. ");

                Date start = QLEX.QLConverter.ConvertObject<Date>(date1);
                Date end = QLEX.QLConverter.ConvertObject<Date>(date2);
                DayCounter daycounter = QLEX.QLConverter.ConvertObject<DayCounter>(dc);
                return daycounter.yearFraction(start, end);
            }
            catch (Exception e)
            {
                ExcelUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                return "";
            }
        }

        [ExcelFunction(Description = "business days between two dates (doesn't include these two days)", Category = "QLExcel - Time")]
        public static object qlTimeBusinessDaysBetween(
            [ExcelArgument(Description = "Start Date ")]DateTime date1,
            [ExcelArgument(Description = "End Date ")]DateTime date2,
            [ExcelArgument(Description = "Calendar (default NYC) ")]string calendar)
        {
            if (ExcelUtil.CallFromWizard())
                return "";

            string callerAddress = "";
            callerAddress = ExcelUtil.getActiveCellAddress();
            OHRepository.Instance.removeErrorMessage(callerAddress);

            try
            {
                if ((date1 == DateTime.MinValue) || (date2 == DateTime.MinValue))
                    throw new Exception("Date must not be empty. ");
                Date start = QLEX.QLConverter.ConvertObject<Date>(date1);
                Date end = QLEX.QLConverter.ConvertObject<Date>(date2);

                if (string.IsNullOrEmpty(calendar)) calendar = "NYC";
                QLEX.Calendar can = QLEX.QLConverter.ConvertObject<QLEX.Calendar>(calendar);
                
                return can.businessDaysBetween(start, end, false, false);
            }
            catch (Exception e)
            {
                ExcelUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                return "";
            }
        }

        [ExcelFunction(Description = "check if the given date is a business day", Category = "QLExcel - Time")]
        public static bool qlTimeIsBusinessDay(
            [ExcelArgument(Description = "Date ")]DateTime date,
            [ExcelArgument(Description = "Calendar (default NYC) ")]string calendar)
        {
            if (ExcelUtil.CallFromWizard())
                return false;

            string callerAddress = "";
            try
            {
                callerAddress = ExcelUtil.getActiveCellAddress();

                OHRepository.Instance.removeErrorMessage(callerAddress);
            }
            catch (Exception)
            {
            }
            try
            {
                if (date == DateTime.MinValue)
                    throw new Exception("Date must not be empty. ");
                QLEX.Date d = QLEX.QLConverter.ConvertObject<QLEX.Date>(date);

                if (string.IsNullOrEmpty(calendar)) calendar = "NYC";
                QLEX.Calendar can = QLEX.QLConverter.ConvertObject<QLEX.Calendar>(calendar);
                
                return can.isBusinessDay(d);
            }
            catch (Exception e)
            {
                ExcelUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                return false;
            }
        }

        [ExcelFunction(Description = "adjust a date to business day", Category = "QLExcel - Time")]
        public static object qlTimeAdjustDate(
            [ExcelArgument(Description = "Date ")]DateTime date,
            [ExcelArgument(Description = "Calendar (default NYC) ")]string calendar,
            [ExcelArgument(Description = "BusinessDayConvention (default ModifiedFollowing) ")]string bdc)
        {
            if (ExcelUtil.CallFromWizard())
                return "";

            string callerAddress = "";
            callerAddress = ExcelUtil.getActiveCellAddress();
            OHRepository.Instance.removeErrorMessage(callerAddress);

            try
            {
                if (date == DateTime.MinValue)
                    throw new Exception("Date must not be empty. ");
                QLEX.Date d = QLEX.QLConverter.ConvertObject<QLEX.Date>(date);

                if (string.IsNullOrEmpty(calendar)) calendar = "NYC";
                QLEX.Calendar can = QLEX.QLConverter.ConvertObject<QLEX.Calendar>(calendar);

                if (string.IsNullOrEmpty(bdc)) bdc = "MF";                
                BusinessDayConvention bdc2 = QLEX.QLConverter.ConvertObject<BusinessDayConvention>(bdc);

                Date newday = can.adjust(d, bdc2);
                return newday.serialNumber();
            }
            catch(TypeInitializationException e)
            {
                ExcelUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                object[,] ret = new object[5,1];
                ret[0,1] = e.ToString();
                ret[1,1] = e.Message;
                ret[2,1] = e.StackTrace;
                ret[3,3] = e.Source;
                ret[4,1] = e.InnerException.Message;
                return ret;
            }
            catch (Exception e)
            {
                ExcelUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                return e.Message;
            }
        }

        [ExcelFunction(Description = "advance forward a date acording to tenor given", Category = "QLExcel - Time")]
        public static object qlTimeAdvanceDate(
            [ExcelArgument(Description = "Date ")]DateTime date,
            [ExcelArgument(Description = "Calendar (default NYC) ")]string calendar,
            [ExcelArgument(Description = "Tenor (e.g. '3D' or '2Y') ")]string tenor,
            [ExcelArgument(Description = "BusinessDayConvention (default ModifiedFollowing) ")]string bdc,
            [ExcelArgument(Description = "is endofmonth ")]bool eom)
        {
            if (ExcelUtil.CallFromWizard())
                return "";

            string callerAddress = "";
            callerAddress = ExcelUtil.getActiveCellAddress();
            OHRepository.Instance.removeErrorMessage(callerAddress);

            try
            {
                if (date == DateTime.MinValue)
                    throw new Exception("Date must not be empty. ");
                QLEX.Date d = QLEX.QLConverter.ConvertObject<QLEX.Date>(date);

                if (string.IsNullOrEmpty(calendar)) calendar = "NYC";
                QLEX.Calendar can = QLEX.QLConverter.ConvertObject<QLEX.Calendar>(calendar);

                if (string.IsNullOrEmpty(tenor))
                    tenor = "1D";
                QLEX.Period period = QLEX.QLConverter.ConvertObject<QLEX.Period>(tenor);

                if (string.IsNullOrEmpty(bdc)) bdc = "MF";
                BusinessDayConvention bdc2 = QLEX.QLConverter.ConvertObject<BusinessDayConvention>(bdc);

                Date newday = can.advance(d, period, bdc2, eom);
                return newday.serialNumber();
            }
            catch (Exception e)
            {
                ExcelUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                return e.Message;
            }
        }

        [ExcelFunction(Description = "return next IMM date ", Category = "QLExcel - Time")]
        public static object qlTimeNextIMMDate(
            [ExcelArgument(Description = "Date ")]DateTime date,
            [ExcelArgument(Description = "is main cycle ? ")]bool maincycle)
        {
            if (ExcelUtil.CallFromWizard())
                return "";

            string callerAddress = "";
            callerAddress = ExcelUtil.getActiveCellAddress();
            OHRepository.Instance.removeErrorMessage(callerAddress);

            try
            {
                if (date == DateTime.MinValue)
                    throw new Exception("Date must not be empty. ");
                QLEX.Date d = QLEX.QLConverter.ConvertObject<QLEX.Date>(date);

                QLEX.Date immdate = QLEX.IMM.nextDate(d, maincycle);

                return immdate.serialNumber();
            }
            catch (Exception e)
            {
                ExcelUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                return e.Message;
            }
        }

        [ExcelFunction(Description = "time schedule betwwen two days according to tenor", Category = "QLExcel - Time")]
        public static object qlTimeSchedule(
            [ExcelArgument(Description = "Start Date ")]DateTime date1,
            [ExcelArgument(Description = "End Date ")]DateTime date2,
            [ExcelArgument(Description = "Tenor (e.g. '3D' or '2Y') ")]string tenor,
            [ExcelArgument(Description = "Calendar (default NYC) ")]string calendar,
            [ExcelArgument(Description = "BusinessDayConvention (default ModifiedFollowing) ")]string bdc,
            [ExcelArgument(Description = "DateGenerationRule (default Backward) ")]string rule,
            [ExcelArgument(Description = "is endofmonth ")]bool eom)
        {
            if (ExcelUtil.CallFromWizard())
                return "";

            string callerAddress = "";
            callerAddress = ExcelUtil.getActiveCellAddress();
            OHRepository.Instance.removeErrorMessage(callerAddress);

            try
            {
                if ((date1 == DateTime.MinValue) || (date2 == DateTime.MinValue))
                    throw new Exception("Date must not be empty. ");
                Date start = QLEX.QLConverter.ConvertObject<Date>(date1);
                Date end = QLEX.QLConverter.ConvertObject<Date>(date2);

                QLEX.Period period = QLEX.QLConverter.ConvertObject<QLEX.Period>(tenor);

                if (string.IsNullOrEmpty(calendar)) calendar = "NYC";
                QLEX.Calendar can = QLEX.QLConverter.ConvertObject<QLEX.Calendar>(calendar);

                if (string.IsNullOrEmpty(bdc)) bdc = "MF";
                BusinessDayConvention bdc2 = QLEX.QLConverter.ConvertObject<BusinessDayConvention>(bdc);

                if (string.IsNullOrEmpty(rule)) rule = "BACKWARD";
                DateGeneration.Rule rule2 = QLEX.QLConverter.ConvertObject<DateGeneration.Rule>(rule);

                Schedule sch = new Schedule(start, end, period, can, bdc2, bdc2, rule2, eom);

                object[,] ret = new object[sch.size(), 1];
                for (uint i = 0; i < sch.size(); i++)
                {
                    ret[i, 0] = sch.date(i).serialNumber();
                }

                return ret;
            }
            catch (Exception e)
            {
                ExcelUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                return "";
            }
        }

        [ExcelFunction(Description = "convert from futures symbols to date ", Category = "QLExcel - Time")]
        public static object qlTimeDateFromFuturesSymbol(
            [ExcelArgument(Description = "futures symbol (e.g. F16 ")]string fsymbol)
        {
            if (ExcelUtil.CallFromWizard())
                return "";

            string callerAddress = "";
            callerAddress = ExcelUtil.getActiveCellAddress();
            OHRepository.Instance.removeErrorMessage(callerAddress);

            try
            {
                DateTime ret = QLEX.Utils.DateFromFuturesSymbol(fsymbol);
                return ret;
            }
            catch (Exception e)
            {
                ExcelUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                return "";
            }
        }
    }
}
