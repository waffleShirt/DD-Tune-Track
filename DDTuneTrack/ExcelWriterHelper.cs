using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Windows.Forms; 

namespace DDTuneTrack
{
    class ExcelWriterHelper
    {
        private static ExcelWriterHelper s_instance = null;

        Microsoft.Office.Interop.Excel.Application mXLApp = null;
        Workbook mXLWorkBook = null;
        Worksheet mXLWorksheet = null;

        private ExcelWriterHelper() { }

        public static ExcelWriterHelper GetInstance()
        {
            if (s_instance == null)
            {
                s_instance = new ExcelWriterHelper();
            }

            return s_instance;
        }

        public void OpenExcelSpreadsheet()
        {
            try
            {
                // Start excel and get application object
                mXLApp = new Microsoft.Office.Interop.Excel.Application();
                mXLApp.Visible = false;

                string dir = Directory.GetCurrentDirectory(); 

                mXLWorkBook = mXLApp.Workbooks.Open(dir + "\\DDRentalTunes2015.xlsx");

                mXLWorksheet = mXLWorkBook.ActiveSheet; 
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message); 
                Console.WriteLine(e.Message); 
            }
        }

        public void WriteRow(DataGridViewRow row)
        {
            try
            {
                if (mXLWorksheet != null)
                {
                    Range xlRange = mXLWorksheet.Cells[mXLWorksheet.Rows.Count, 1];
                    long lastRow = (long)xlRange.get_End(XlDirection.xlUp).Row;
                    long newRow = lastRow + 1;

                    for (int i = 0; i < row.Cells.Count; ++i)
                    {
                        mXLWorksheet.Cells[newRow, i + 1] = row.Cells[i].Value;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void CloseSpreadsheet()
        {
            try
            {
                if (mXLWorkBook != null)
                {
                    mXLWorkBook.Save();
                    mXLWorkBook.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
