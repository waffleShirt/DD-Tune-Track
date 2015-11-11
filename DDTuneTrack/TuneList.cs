using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace DDTuneTrack
{
    /// <summary>
    /// Author: Tom Burridge
    /// Implements the functionality required to initialise the DataGridView
    /// used to display tunes, as well as the Add/Remove/Edit functionality 
    /// for the data rows. 
    /// </summary>
    class TuneList
    {
        private DDTuneTrackForm mAppForm; 
        private DataGridView mTuneListDGV;
        private bool mAllowSelectionChange = false;
        private Point mLocation = new Point(6, 278);
        private System.Drawing.Size mSize = new System.Drawing.Size(734, 197); 
        private string mWorkbookName = "DDRentalTunes2015.xlsx";

        /// <summary>
        /// TuneList Constructor. 
        /// </summary>
        /// <param name="appForm">Main application form</param>
        /// <param name="dgv">DataGridView from main application form</param>
        public TuneList(DDTuneTrackForm appForm, DataGridView dgv)
        {
            mAppForm = appForm; 
            mTuneListDGV = dgv;

            InitializeDataGridView(); 
        }

        /// <summary>
        /// Initialises the formatting and interactivity of the DataGridView
        /// that displays the tunes. 
        /// </summary>
        private void InitializeDataGridView()
        {
            mTuneListDGV.AllowUserToAddRows = false;
            mTuneListDGV.AllowUserToDeleteRows = true;
            mTuneListDGV.AllowUserToResizeRows = false;
            mTuneListDGV.AllowUserToResizeColumns = false;
            mTuneListDGV.EnableHeadersVisualStyles = false;

            mTuneListDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            mTuneListDGV.EditMode = DataGridViewEditMode.EditOnKeystroke;
            mTuneListDGV.ShowEditingIcon = false;

            mTuneListDGV.Name = "dgvPendingTuneList";
            mTuneListDGV.Size = mSize;
            mTuneListDGV.Location = mLocation; 
            mTuneListDGV.TabIndex = 0;

            mTuneListDGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            mTuneListDGV.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            InitialiseColumns();

            mTuneListDGV.ColumnHeadersDefaultCellStyle.Font = new Font("Verdana", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            mTuneListDGV.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            mTuneListDGV.ColumnHeadersDefaultCellStyle.BackColor = Color.Gainsboro;

            mTuneListDGV.RowHeadersDefaultCellStyle.Padding = new Padding(3);//helps to get rid of the selection triangle?
            mTuneListDGV.RowHeadersDefaultCellStyle.Font = new Font("Verdana", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            mTuneListDGV.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            mTuneListDGV.RowHeadersDefaultCellStyle.BackColor = Color.Gainsboro;

            // Set columns to fit width of container
            mTuneListDGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        /// <summary>
        /// Clears all data from the DataGridView. 
        /// </summary>
        public void ClearAllData()
        {
            mTuneListDGV.Rows.Clear();
            mAllowSelectionChange = false; 
        }

        /// <summary>
        /// Initialises all of the columns in the DataGridView with the correct
        /// headings. Also sets all the necessary interctivity attributes such
        /// as ReadOnly state etc. 
        /// </summary>
        private void InitialiseColumns()
        {
            // Asset Number Column
            DataGridViewTextBoxColumn assetColumn = new DataGridViewTextBoxColumn();
            assetColumn.HeaderText = "Asset Number";
            assetColumn.Name = "colAssetNumber";
            assetColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            assetColumn.ReadOnly = true; 
            mTuneListDGV.Columns.Add(assetColumn);

            // Tune Type Column
            DataGridViewTextBoxColumn tuneTypeColumn = new DataGridViewTextBoxColumn();
            tuneTypeColumn.HeaderText = "Tune Type";
            tuneTypeColumn.Name = "colTuneType";
            tuneTypeColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            tuneTypeColumn.ReadOnly = true; 
            mTuneListDGV.Columns.Add(tuneTypeColumn);

            // Tune Date Column
            DataGridViewTextBoxColumn tuneDateColumn = new DataGridViewTextBoxColumn();
            tuneDateColumn.HeaderText = "Tune Date";
            tuneDateColumn.Name = "colTuneDate";
            tuneDateColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            tuneDateColumn.ReadOnly = true; 
            mTuneListDGV.Columns.Add(tuneDateColumn);

            // Entry Date Column
            DataGridViewTextBoxColumn entryDateColumn = new DataGridViewTextBoxColumn();
            entryDateColumn.HeaderText = "Entry Date";
            entryDateColumn.Name = "colEntryDate";
            entryDateColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            entryDateColumn.ReadOnly = true; 
            mTuneListDGV.Columns.Add(entryDateColumn);

            // Staff Column
            DataGridViewTextBoxColumn staffColumn = new DataGridViewTextBoxColumn();
            staffColumn.HeaderText = "Staff";
            staffColumn.Name = "colStaff";
            staffColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            staffColumn.ReadOnly = true; 
            mTuneListDGV.Columns.Add(staffColumn);

            // Notes Column
            DataGridViewTextBoxColumn notesColumn = new DataGridViewTextBoxColumn();
            notesColumn.HeaderText = "Notes/Parts";
            notesColumn.Name = "colNotes";
            notesColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            notesColumn.ReadOnly = true;
            mTuneListDGV.Columns.Add(notesColumn);
        }

        /// <summary>
        /// Adds a new tune to the Tune List. 
        /// </summary>
        /// <param name="assetNumber">Tune asset number</param>
        /// <param name="tuneType">Tune type</param>
        /// <param name="tuneDate">Tune date</param>
        /// <param name="entryDate">Date tune entered into program</param>
        /// <param name="staffMember">Tuning staff</param>
        /// <param name="notes">Extra notes about the tune</param>
        public void AddNewTune(string assetNumber, string tuneType, string tuneDate, string entryDate, string staffMember, string notes)
        {
            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(mTuneListDGV); 
            bool rowSet = row.SetValues(assetNumber, tuneType, tuneDate, entryDate, staffMember, notes);
            row.HeaderCell.Value = (1 + mTuneListDGV.Rows.Count).ToString() ;
            mTuneListDGV.Rows.Add(row);

            // The first entered row will be selected, we dont want this to happen. 
            if (mTuneListDGV.Rows.Count == 1)
            {
                mTuneListDGV.Rows[0].Selected = false;
                
                // Now that the first row is inserted and unselected we can
                // enable calls to OnSelectionChanged to actually do something
                mAllowSelectionChange = true; 
            }
        }

        /// <summary>
        /// Updates an existing tune in the Tune List. 
        /// </summary>
        /// <param name="assetNumber">Tune asset number</param>
        /// <param name="tuneType">Tune type</param>
        /// <param name="tuneDate">Tune date</param>
        /// <param name="entryDate">Date tune entered into program</param>
        /// <param name="staffMember">Tuning staff</param>
        /// <param name="notes">Extra notes about the tune</param>
        public void UpdateExistingTune(string assetNumber, string tuneType, string tuneDate, string entryDate, string staffMember, string notes)
        {
            mTuneListDGV.Rows[mTuneListDGV.SelectedRows[0].Index].SetValues(assetNumber, tuneType, tuneDate, entryDate, staffMember, notes);      
       
            // Deselect row
            mTuneListDGV.Rows[mTuneListDGV.SelectedRows[0].Index].Selected = false; 
        }

        /// <summary>
        /// Removes an existing tune from the Tune List. The tune that will be
        /// removed is the tune that is currently selected in the DataGridView.
        /// </summary>
        public void RemoveExistingTune()
        {
            // Remove row
            mTuneListDGV.Rows.RemoveAt(mTuneListDGV.SelectedRows[0].Index);

            // Deselect row
            if (mTuneListDGV.Rows.Count > 0)
            {
                mTuneListDGV.Rows[mTuneListDGV.SelectedRows[0].Index].Selected = false;
            }
            else
            {
                mAllowSelectionChange = false; 
            }
        }

        /// <summary>
        /// Gets the current row selected in the DataGridView. 
        /// </summary>
        /// <returns>Row currently selected in DataGridView.</returns>
        public DataGridViewRow GetSelectedRow()
        {
            if (mAllowSelectionChange)
            {
                if (mTuneListDGV.SelectedRows.Count > 0)
                {
                    return mTuneListDGV.SelectedRows[0];
                }
                else
                {
                    return null;
                }
            }

            return null; 
        }

        /// <summary>
        /// Gets the number of rows in the Tune List. 
        /// </summary>
        /// <returns>Number of rows in the tune list.</returns>
        public int GetNumRows()
        {
            return mTuneListDGV.Rows.Count;
        }

        /// <summary>
        /// Returns all the current data rows in the Tune List
        /// </summary>
        /// <returns>All data rows in the Tune List</returns>
        public DataGridViewRowCollection GetAllTuneRows()
        {
            return mTuneListDGV.Rows; 
        }

        /// <summary>
        /// Calls the methods in the Excel Writer Helper to write the data in
        /// the Tune List to an Excel Spreadsheet. 
        /// </summary>
        public void WriteTuneListToFile()
        {
            string dir = Directory.GetCurrentDirectory();
            string workbookPath = dir + "\\" + mWorkbookName;
            
            ExcelWriterHelper.GetInstance().OpenExcelSpreadsheet(workbookPath);

            foreach (DataGridViewRow row in mTuneListDGV.Rows)
            {
                ExcelWriterHelper.GetInstance().WriteRow(row); 
            }

            ExcelWriterHelper.GetInstance().CloseSpreadsheet(); 
        }
    }
}
