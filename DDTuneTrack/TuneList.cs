using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Drawing; 

/**************************************************************
 * Class: Tune List
 * Author: Tom Burridge
 * Date Created: 5/11/2015
 * Description: Implements the functionality required to 
 * initialise the DataGridView used to display tunes, as well 
 * the Add/Remove/Edit functionality for the data rows. 
 *************************************************************/
namespace DDTuneTrack
{
    class TuneList
    {
        private DDTuneTrackForm mAppForm; 
        private DataGridView mTuneListDGV;
        private int mNumRows = 0;
        private bool mAllowSelectionChange = false;
        private Point mLocation = new Point(6, 278);
        private System.Drawing.Size mSize = new System.Drawing.Size(734, 197); 

        public TuneList(DDTuneTrackForm appForm, DataGridView dgv)
        {
            mAppForm = appForm; 
            mTuneListDGV = dgv;

            InitializeDataGridView(); 
        }

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

        public void ClearAllData()
        {
            mTuneListDGV.Rows.Clear();
            mNumRows = 0;
            mAllowSelectionChange = false; 
        }

        private DataGridViewRow AddEmptyRow()
        {
            DataGridViewRow row = new DataGridViewRow();
            row.HeaderCell.Value = mNumRows.ToString();
            mTuneListDGV.Rows.Add(row);
            ++mNumRows;

            return row; 
        }

        private void AddAColumn(int i)
        {
            DataGridViewTextBoxColumn Acolumn = new DataGridViewTextBoxColumn();
            //OK I know this only works normally for 26 chars(columns)
            // I leave the rest of the Excel columns up to you to figure out :o)
            char ch = (char)(i + 65);
            Acolumn.HeaderText = ch.ToString();
            Acolumn.Name = "Column" + i.ToString();
            Acolumn.Width = 60;
            Acolumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            //make a Style template to be used in the grid
            DataGridViewCell Acell = new DataGridViewTextBoxCell();
            Acell.Style.BackColor = Color.LightCyan;
            Acell.Style.SelectionBackColor = Color.FromArgb(128, 255, 255);
            Acolumn.CellTemplate = Acell;
            mTuneListDGV.Columns.Add(Acolumn);
        }

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

        public void AddNewTune(string assetNumber, string tuneType, string tuneDate, string entryDate, string staffMember, string notes)
        {
            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(mTuneListDGV); 
            bool rowSet = row.SetValues(assetNumber, tuneType, tuneDate, entryDate, staffMember, notes);
            row.HeaderCell.Value = mNumRows.ToString();
            mTuneListDGV.Rows.Add(row);

            ++mNumRows;

            // The first entered row will be selected, we dont want this to happen. 
            if (mNumRows == 1)
            {
                mTuneListDGV.Rows[0].Selected = false;
                
                // Now that the first row is inserted and unselected we can
                // enable calls to OnSelectionChanged to actually do something
                mAllowSelectionChange = true; 
            }
        }

        public void UpdateExistingTune(string assetNumber, string tuneType, string tuneDate, string entryDate, string staffMember, string notes)
        {
            mTuneListDGV.Rows[mTuneListDGV.SelectedRows[0].Index].SetValues(assetNumber, tuneType, tuneDate, entryDate, staffMember, notes);      
       
            // Deselect row
            mTuneListDGV.Rows[mTuneListDGV.SelectedRows[0].Index].Selected = false; 
        }

        public void RemoveExistingTune()
        {
            // Remove row
            mTuneListDGV.Rows.RemoveAt(mTuneListDGV.SelectedRows[0].Index);

            // Decrease row count
            --mNumRows; 

            // Deselect row
            if (mNumRows > 0)
            {
                mTuneListDGV.Rows[mTuneListDGV.SelectedRows[0].Index].Selected = false;
            }
        }

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

        public int GetNumRows()
        {
            return mNumRows;
        }

        public ChargeList BuildChargeList()
        {
            ChargeList cl = new ChargeList();

            foreach (DataGridViewRow row in mTuneListDGV.Rows)
            {
                cl.AddTune(row.Cells["colTuneType"].Value.ToString(), row.Cells["colAssetNumber"].Value.ToString() + ": " + row.Cells["colNotes"].Value.ToString()); 
            }

            return cl; 
        }

        public void WriteTuneListToFile()
        {
            // Test Excel Functionality
            ExcelWriterHelper.GetInstance().OpenExcelSpreadsheet();

            foreach (DataGridViewRow row in mTuneListDGV.Rows)
            {
                ExcelWriterHelper.GetInstance().WriteRow(row); 
            }

            ExcelWriterHelper.GetInstance().CloseSpreadsheet(); 
        }
    }
}
