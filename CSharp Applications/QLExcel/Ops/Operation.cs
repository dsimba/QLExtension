﻿/****************************** Project Header ******************************\
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
using System.Threading.Tasks;
using Xl = Microsoft.Office.Interop.Excel;
using ExcelDna.Integration;
using ExcelDna.Integration.CustomUI;
using ExcelDna.Logging;

namespace QLExcel
{
    sealed public class Operation
    {
        public static bool CallFromWizardFlag = true;
        public static bool CallerAddressFlag = false;

        [ExcelFunction(Description = "toggle callFromWizard control", Category = "QLExcel - Operation")]
        public static bool qlOpCheckCallFromWizard(
            [ExcelArgument(Description = "true/false")] bool control)
        {
            CallFromWizardFlag = control;
            return CallFromWizardFlag;
        }

        [ExcelFunction(Description = "toggle callerAddress control", Category = "QLExcel - Operation")]
        public static bool qlOpCallerAddressControl(
            [ExcelArgument(Description = "true/false")] bool control)
        {
            CallerAddressFlag = control;
            return CallerAddressFlag;
        }

        [ExcelFunction(Description = "Display xll path", IsMacroType = true, Category = "QLExcel - Operation")]
        public static string qlOpLibXllPath()
        {
            string callerAddress = "";
            callerAddress = ExcelUtil.getActiveCellAddress();
            OHRepository.Instance.removeErrorMessage(callerAddress);

            string appName = null;
            try
            {
                appName = Version.getXllPath();
            }
            catch (Exception exception_)
            {
                ExcelUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), exception_.Message);
                appName = exception_.Message;
            }
            return appName;
        }

        #region enum
        /*
        [ExcelFunction(Description = "Display enumeration type", Category = "QLExcel - Operation")]
        public static object qlDisplayAllEnum()
        {
            Dictionary<string, List<String>> enumdic = QLExpansion.EnumRepository.Instance.getEnumMap();

            int col = 0;
            string[,] ret = new string[100, enumdic.Count];
            foreach (KeyValuePair<string, List<string>> kvp in enumdic)
            {
                int row = 0;
                ret[row++, col] = kvp.Key;
                foreach (string data in kvp.Value)
                {
                    ret[row++, col] = data;
                }
                while (row < 100)
                {
                    ret[row++, col] = "";
                }
                col++;
            }

            return ret;
        }
        */
        /// <summary>
        /// See conversion for details
        /// </summary>
        /// <param name="enumclassname"></param>
        /// <returns></returns>
        [ExcelFunction(Description = "retrieve enum list", Category = "QLExcel - Operation")]
        public static object qlOpGetEnumerationList(
            [ExcelArgument(Description = "Enum Class Name (Daycounter, Calendar, BusinessDayConvention)")] string enumclassname)
        {
            if (ExcelUtil.CallFromWizard())
                return "";

            string callerAddress = "";
            callerAddress = ExcelUtil.getActiveCellAddress();

            try
            {
                // by default ExcelDna is horizontal; make vertical
                object[,] ret = new object[10, 1];
                for (int i = 0; i < 10; i++)
                {
                    ret[i, 0] = "";
                }
                switch (enumclassname.ToUpper())
                {
                    case "DAYCOUNTER":
                        ret[0, 0] = "ACTUAL360";
                        ret[1, 0] = "ACTUAL365";
                        ret[2, 0] = "ACTUALACTUAL";
                        break;
                    case "CALENDAR":
                        ret[0, 0] = "NYC";
                        ret[1, 0] = "LON";
                        ret[2, 0] = "NYC|LON";
                        break;
                    case "BUSINESSDAYCONVENTION":
                        ret[0, 0] = "F";
                        ret[1, 0] = "MF";
                        ret[2, 0] = "P";
                        ret[3, 0] = "MP";
                        ret[4, 0] = "NONE";
                        break;
                    case "DGRULE":      // Date Generation Rule
                        ret[0, 0] = "Backward";
                        ret[1, 0] = "Forward";
                        ret[2, 0] = "Zero";
                        ret[3, 0] = "ThirdWednesday";
                        ret[4, 0] = "Twentieth";
                        ret[5, 0] = "TwentiethIMM";
                        ret[6, 0] = "CDS";
                        break;
                    default:
                        ret[0, 0] = "unkown enum type";
                        break;
                }

                return ret;
            }
            catch (Exception e)
            {
                ExcelUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                return e.Message;
            }
        }

        [ExcelFunction(Description = "List the IDs of objects in repository", Category = "QLExcel - Operation")]
        public static object qlOpListObjects(
            [ExcelArgument(Description = "pattern ")] string pattern)
        {
            if (ExcelUtil.CallFromWizard())
                return new string[0, 0];

            List<String> objids = OHRepository.Instance.listObjects(pattern);

            object[,] ret = new object[objids.Count, 1];
            int i = 0;
            foreach (string str in objids)
            {
                ret[i++, 0] = str;
            }

            return ret;
        }

        [ExcelFunction(Description = "Get object class name", Category = "QLExcel - Operation")]
        public static string qlOpObjectClassName(
            [ExcelArgument(Description = "id of object ")] string objID)
        {
            if (ExcelUtil.CallFromWizard())
                return "";

            return OHRepository.Instance.getObjectType(objID).Name;
        }

        [ExcelFunction(Description = "Get object caller address", Category = "QLExcel - Operation")]
        public static string qlOpObjectCallerAddress(
            [ExcelArgument(Description = "id of object ")] string objID)
        {
            if (ExcelUtil.CallFromWizard())
                return "";

            return OHRepository.Instance.getCallerAddress(objID);
        }

        [ExcelFunction(Description = "Get object creation time", Category = "QLExcel - Operation")]
        public static DateTime qlOpObjectCreationTime(
            [ExcelArgument(Description = "id of object ")] string objID)
        {
            if (ExcelUtil.CallFromWizard())
                return DateTime.MinValue;

            return OHRepository.Instance.getObjectCreationTime(objID);
        }

        [ExcelFunction(Description = "Get object update time", Category = "QLExcel - Operation")]
        public static DateTime qlOpObjectUpdateTime(
            [ExcelArgument(Description = "id of object ")] string objID)
        {
            if (ExcelUtil.CallFromWizard())
                return DateTime.MinValue;

            return OHRepository.Instance.getObjectUpdateTime(objID);
        }
        #endregion
    }
}
