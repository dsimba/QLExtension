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
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Threading;
using System.Reflection;
using Xl = Microsoft.Office.Interop.Excel;
using System.Data;
using System.Runtime.InteropServices;
using ExcelDna.Integration;
using ExcelDna.Integration.CustomUI;
using ExcelDna.Logging;
using System.Data.SqlClient;


namespace QLExcel
{
    [ComVisible(true)]
    //[ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class RibbonMenu : ExcelRibbon
    {
        public static SynchronizationContext syncContext_;
        #region Event Handler
        public static void Login_Click()
        {
            try
            {
                System.Windows.Forms.MessageBox.Show("You are now logged into QLExtension library");
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Unable to log into QLExtension library");
            }
        }

        public void Error_Click(IRibbonControl control_)
        {
            LogDisplay.Show();
        }

        // [ExcelCommand(MenuName = "Range Tools", MenuText = "Square Selection")]
        public static void ReadData_Click()
        {
            object[,] result = null;
            // Get a reference to the current selection
            ExcelReference selection = (ExcelReference)XlCall.Excel(XlCall.xlfSelection);

            try
            {
            }
            catch
            {
                result = new object[,] { { "Unable to retrieve data." } };
            }

            // Now create the target reference that will refer to Sheet 2, getting a reference that contains the SheetId first
            // ExcelReference sheet2 = (ExcelReference)XlCall.Excel(XlCall.xlSheetId, "Sheet2"); // Throws exception if no Sheet2 exists
            // ... then creating the reference with the right size as new ExcelReference(RowFirst, RowLast, ColFirst, ColLast, SheetId)
            int resultRows = result.GetLength(0);
            int resultCols = result.GetLength(1);
            //ExcelReference target = new ExcelReference(selection.RowFirst, selection.RowFirst + resultRows - 1,
            ExcelReference target = new ExcelReference(0, 0 + resultRows - 1,           // start from top
                selection.ColumnLast + 1, selection.ColumnLast + resultCols, selection.SheetId);
            // Finally setting the result into the target range.
            target.SetValue(result);
        }

        public void Help_Click(IRibbonControl control_)
        {
            //System.Diagnostics.Process.Start(xllDir + @"documents\QLExcel.chm");
            System.Windows.Forms.MessageBox.Show("Please contact letian.zj for help.");
        }

        public void About_Click(IRibbonControl control_)
        {
            // About abt = new About();
            // abt.ShowDialog();
            System.Windows.Forms.MessageBox.Show("QLEX v1.0");
        }

        public void Function_Click(IRibbonControl control_)
        {
            Xl.Application xlApp = (Xl.Application)ExcelDna.Integration.ExcelDnaUtil.Application;
            String fname = control_.Id;
            fname = "=" + fname + "()";

            Xl.Range rg = xlApp.ActiveCell;

            String cellName = ExcelUtil.ExcelColumnIndexToName(rg.Column) + rg.Row;

            Xl._Worksheet sheet = (Xl.Worksheet)xlApp.ActiveSheet;
            Xl.Range range = sheet.get_Range(cellName, System.Type.Missing);
            string previousFormula = range.FormulaR1C1.ToString();
            range.Value2 = fname;
            range.Select();

            syncContext_ = SynchronizationContext.Current;
            if (syncContext_ == null)
            {
                syncContext_ = new System.Windows.Forms.WindowsFormsSynchronizationContext();
            }

            FunctionWizardThread othread = new FunctionWizardThread(range, syncContext_);
            Thread thread = new Thread(new ThreadStart(othread.functionargumentsPopup));
            thread.Start();
        }

        public void excelFile_Click(IRibbonControl control_)
        {
            Xl.Application xlApp = (Xl.Application)ExcelDna.Integration.ExcelDnaUtil.Application;
            String fname = control_.Id;

            string file = "";
            switch (fname)
            {
                case "StockTrading":
                    file = @".\Workbooks\StockTrading.xlsm";
                    break;
                case "VanillaOptionPricer":
                    file = @".\Workbooks\VanillaOptionPricer.xlsx";
                    break;
                case "SABRModel":
                    file = @".\Workbooks\SABRModel.xlsm";
                    break;         
                case "PRNG":
                    file = @".\Workbooks\PRNG.xlsx";
                    break;
                case "BookTrades":
                    file = @".\Workbooks\BookTrades.xlsm";
                    break;
                case "LoadHistCurve":
                    file = @".\Workbooks\HistUSDCurves.xlsm";
                    break;
                case "PublishCurve":
                    file = @".\Workbooks\USDIRCurve.xlsm";
                    break;
                default:
                    break;
            }

            //file = @"C:\Workspace\Output\Debug\OptionPricer.xlsx";
            // rootPath = System.IO.Path.Combine(rootPath, @"..\..\QLExpansion\");
            // rootPath = System.IO.Path.GetFullPath(rootPath); 
            string filepath = System.IO.Path.Combine(QLEX.ConfigManager.Instance.RootDir, file);
            filepath = System.IO.Path.GetFullPath(filepath);
            xlApp.Workbooks.Open(filepath);
        }
        #endregion
    }
}
