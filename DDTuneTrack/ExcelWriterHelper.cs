using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Windows.Forms; 

namespace DDTuneTrack
{
    /// <summary>
    /// Author: Tom Burridge
    /// Provides helper function to open, write to, and close an excel 
    /// spreadsheet. Requires Microsoft Office Excel Interop libraries. Note 
    /// that on end user machines Microsoft Office Excel needs to be installed. 
    /// in some casses the Primary Interop Assemblies Redistributable (Office 
    /// 2010 and earlier) may also need to be installed. For Office 2013 the 
    /// interop assemblies are no longer required and all necessary interop 
    /// libraries are installed by default. The helper functions are NOT 
    /// compatible with Office Starter editions. The helper is a singleton
    /// class. 
    /// </summary>
    class ExcelWriterHelper
    {
        private static ExcelWriterHelper s_instance = null;

        Microsoft.Office.Interop.Excel.Application mXLApp = null;
        Workbook mXLWorkBook = null;
        Worksheet mXLWorksheet = null;

        /// <summary>
        /// Private empty constructor as part of Singleton Pattern. 
        /// </summary>
        private ExcelWriterHelper() { }

        /// <summary>
        /// Returns the singleton instance of the helper. 
        /// </summary>
        /// <returns>Helper class instance</returns>
        public static ExcelWriterHelper GetInstance()
        {
            if (s_instance == null)
            {
                s_instance = new ExcelWriterHelper();
            }

            return s_instance;
        }

        /// <summary>
        /// Opens an Excel spreadsheet. First an application object is created
        /// and opened, and then a workbook and worksheet object are opened.
        /// Note that the currently active sheet in the workbook is the one 
        /// that is help for writing to. 
        /// </summary>
        /// <param name="workbookPath">Path to spreadhseet</param>
        public void OpenExcelSpreadsheet(string workbookPath)
        {
            try
            {
                // Start excel and get application object
                mXLApp = new Microsoft.Office.Interop.Excel.Application();
                mXLApp.Visible = false;

                mXLWorkBook = mXLApp.Workbooks.Open(workbookPath);

                mXLWorksheet = mXLWorkBook.ActiveSheet; 
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message); 
                Console.WriteLine(e.Message); 
            }
        }

        /// <summary>
        /// Writes all the cells from a DataGridView row to the next empty row
        /// in the active worksheet in the spreadsheet. 
        /// </summary>
        /// <param name="row">Data row to write</param>
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

        /// <summary>
        /// Closes the currently open spreadsheet and the associated Excel 
        /// application.
        /// </summary>
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
