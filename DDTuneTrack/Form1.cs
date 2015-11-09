using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.IO; 

namespace DDTuneTrack
{
    enum Mode
    {
        Mode_NewTune,
        Mode_UpdateTune,
        Mode_Max,
    };

    public partial class DDTuneTrackForm : Form
    {
        private TuneList mTuneList;
        private ChargeList mChargeList; 
        private Mode mMode = Mode.Mode_NewTune;
 
        // Strings for dialog boxes
        private string mSubmitDialogTitle = "Submit all tunes?";
        private string mSubmitDialogDetail = "Are you sure you want to save all the current items in the tune list to file?";
        private string mSubmitDialogNoTunesTitle = "No Tunes To Submit"; 
        private string mSubmitDialogNoTunesDetail = "There are no tunes to submit";
        private string mCloseDialogTitle = "Closing";
        private string mCloseDialogDetail = "Are you sure you want to close? Any unsaved tunes will be automatically saved."; 

        public DDTuneTrackForm()
        {
            InitializeComponent();

            // Load tune types from disk
            TuneTypes.LoadTuneTypesList(cmbTuneType);
 
            // Load staff list from disk
            LoadStaffList(); 

            mTuneList = new TuneList(this, this.dgv);

            // Load charge lists from file
            ChargeListManager.GetInstance().LoadChargeLists(); 

            // Set the current charge list date to today. If a list already
            // exists for today it will be displayed immediately in the viewer. 
            dtpChargeListDate.Value = DateTime.Now; 

            UpdateEntryDate(); 

            // Ensure the charged button in the charge list tab has the right text
            SetChargedButtonState(); 
        }

        private void btnClearRemove_Click(object sender, EventArgs e)
        {
            if (mMode == Mode.Mode_NewTune)
            {
                ClearPressed(); 
            }
            else if (mMode == Mode.Mode_UpdateTune)
            {
                RemovePressed(); 
            }
        }

        private void btnSaveUpdate_Click(object sender, EventArgs e)
        {
            if (mMode == Mode.Mode_NewTune)
            {
                SavePressed();
            }
            else if (mMode == Mode.Mode_UpdateTune)
            {
                UpdatePressed(); 
            }
        }

        private void txtAssetNumber_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                SavePressed(); 
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (mTuneList.GetNumRows() == 0)
            {
                DialogResult dialogResult = MessageBox.Show(mSubmitDialogNoTunesDetail, mSubmitDialogNoTunesTitle, MessageBoxButtons.OK);
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show(mSubmitDialogDetail, mSubmitDialogTitle, MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    SubmitTuneList();
                }
                else if (dialogResult == DialogResult.No)
                {
                    // Do nothing
                }
            } 
        }

        private void btnPrevDay_Click(object sender, EventArgs e)
        {
            if (dtpChargeListDate.Value != dtpChargeListDate.MinDate)
            {
                dtpChargeListDate.Value = dtpChargeListDate.Value.AddDays(-1);

                // In case next day button has been disabled
                btnGoToNextDay.Enabled = true; 
            }

            // If we're now at the minimum date for the picker disable the prevDay button
            if (dtpChargeListDate.Value == dtpChargeListDate.MinDate)
            {
                btnGoToPrevDay.Enabled = false; 
            }

            DisplayChargeListForCurrentDate();
        }

        private void DisplayChargeListForCurrentDate()
        {
            // Display the charge list data for the current date in the text box
            txtChargeList.Text = ChargeListManager.GetInstance().GetChargeListTextForDate(dtpChargeListDate.Value);
            SetChargedLabelState();
            SetChargedButtonState(); 
        }

        private void btnNextDay_Click(object sender, EventArgs e)
        {
            if (dtpChargeListDate.Value != dtpChargeListDate.MaxDate)
            {
                dtpChargeListDate.Value = dtpChargeListDate.Value.AddDays(1);
                
                // In case prev day button had been disabled
                btnGoToPrevDay.Enabled = true; 
            }

                // If we're now at the maximum date for the picker disable the prevDay button
            if (dtpChargeListDate.Value == dtpChargeListDate.MaxDate)
            {
                btnGoToNextDay.Enabled = false; 
            }

            DisplayChargeListForCurrentDate(); 
        }

        private void dtpChargeListDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpChargeListDate.Value == dtpChargeListDate.MinDate)
            {
                btnGoToPrevDay.Enabled = false;
                btnGoToNextDay.Enabled = true;
            }

            if (dtpChargeListDate.Value == dtpChargeListDate.MaxDate)
            {
                btnGoToNextDay.Enabled = false;
                btnGoToPrevDay.Enabled = true;
            }

            DisplayChargeListForCurrentDate(); 
        }

        private void btnToggleCharged_Click(object sender, EventArgs e)
        {
            ChargeListManager.GetInstance().ToggleChargedStatusForDate(dtpChargeListDate.Value);
            SetChargedLabelState();
            SetChargedButtonState(); 
        }

        private void ClearPressed()
        {
            ClearInputData();
            HideValidationLabels();
        }

        private void RemovePressed()
        {
            mTuneList.RemoveExistingTune(); 

            // Clear all input
            ClearInputData(); 

            // Reset mode to Tune Mode
            EnableNewTuneMode(); 
        }

        private void SavePressed()
        {
            if (ValidateInput())
            {
                // Save Row To Data Grid View
                mTuneList.AddNewTune(txtAssetNumber.Text, cmbTuneType.SelectedItem.ToString(), dtpTuneDate.Value.ToShortDateString(), txtEntryDate.Text, cmbStaff.SelectedItem.ToString(), txtNotes.Text);
                
                ClearInputOnSaveOrUpdate();

                // Set focus back to asset number
                txtAssetNumber.Focus();
            }
        }

        private void UpdatePressed()
        {
            if (ValidateInput())
            {
                // Update existing row
                mTuneList.UpdateExistingTune(txtAssetNumber.Text, cmbTuneType.SelectedItem.ToString(), dtpTuneDate.Value.ToShortDateString(), txtEntryDate.Text, cmbStaff.SelectedItem.ToString(), txtNotes.Text);

                ClearInputOnSaveOrUpdate();     // !!!! May not be the right option, perhaps better to clear all data on update? 

                // Set focus back to asset number
                txtAssetNumber.Focus(); 

                // Reset the mode back to New Tune
                EnableNewTuneMode(); 
            }
        }

        /// <summary>
        /// Clears the input data after a Save or Update operation. 
        /// Only the asset number is cleared, as we expect on successive
        /// inputs to retain the same tune type, tune date and staff member. 
        /// </summary>
        private void ClearInputOnSaveOrUpdate()
        {
            txtAssetNumber.Text = string.Empty; 
        }

        private void UpdateEntryDate()
        {
            txtEntryDate.Text = DateTime.Now.ToShortDateString(); 
        }

        /// <summary>
        /// Clears all the input data in the form. 
        /// Note that all input data is cleared to blank
        /// or default data. 
        /// </summary>
        private void ClearInputData()
        {
            txtAssetNumber.Text = "";
            cmbTuneType.SelectedIndex = -1;
            dtpTuneDate.Value = DateTime.Now;
            UpdateEntryDate();
            cmbStaff.SelectedIndex = -1; 
        }

        private void HideValidationLabels()
        {
            lblAssetNumberError.Visible = false;
            lblTuneTypeError.Visible = false;
            lblStaffError.Visible = false;
        }

        /// <summary>
        /// Checks validity of input data. 
        /// </summary>
        /// <returns>True if input data is valid, otherwise false.</returns>
        private bool ValidateInput()
        {
            int numErrors = 0; 

            if (txtAssetNumber.Text == string.Empty)
            {
                lblAssetNumberError.Visible = true;
                ++numErrors; 
            }
            else
            {
                lblAssetNumberError.Visible = false;
            }

            if (cmbTuneType.SelectedIndex < 0)
            {
                lblTuneTypeError.Visible = true;
                ++numErrors; 
            }
            else
            {
                lblTuneTypeError.Visible = false;
            }

            if (cmbStaff.SelectedIndex < 0)
            {
                lblStaffError.Visible = true;
                ++numErrors;
            }
            else
            {
                lblStaffError.Visible = false;
            }

            if (numErrors > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            OnTuneListRowSelectionChanged();
        }

        /// <summary>
        /// Sets the program to operate in Tune Update Mode. 
        /// In this mode an existing tune selected in the tune list
        /// data grid view can be deleted or updated. 
        /// </summary>
        public void EnableTuneUpdateMode()
        {
            mMode = Mode.Mode_UpdateTune;
            btnClearRemove.Text = "Remove";
            btnSaveUpdate.Text = "Update"; 
        }

        /// <summary>
        /// Sets the program to operate in New Tune Mode. 
        /// In this mode a new tune can be input and added to
        /// the tune list data grid view. 
        /// </summary>
        public void EnableNewTuneMode()
        {
            mMode = Mode.Mode_NewTune;
            btnClearRemove.Text = "Clear";
            btnSaveUpdate.Text = "Save";
        }

        public void OnTuneListRowSelectionChanged()
        {
            // Get the row. If we don't receive a row we can't do anything
            DataGridViewRow row = mTuneList.GetSelectedRow();

            if (row != null)
            {
                // Put program into Tune Update Mode
                EnableTuneUpdateMode();

                // Populate the input data with the values from the row
                txtAssetNumber.Text = row.Cells["colAssetNumber"].Value.ToString();
                cmbTuneType.SelectedIndex = cmbTuneType.FindStringExact(row.Cells["colTuneType"].Value.ToString());
                dtpTuneDate.Value = DateTime.ParseExact(row.Cells["colTuneDate"].Value.ToString(), "d/MM/yyyy", null);
                cmbStaff.SelectedIndex = cmbStaff.FindStringExact(row.Cells["colStaff"].Value.ToString());
            }
        }

        private void SubmitTuneList()
        {
            // Write Tunes to File
            mTuneList.WriteTuneListToFile(); 
            
            // Add the new charge list
            ChargeListManager.GetInstance().AddNewChargeList(mTuneList.BuildChargeList());

            // Charge list is built. Destroy the DataGridView and clear all data
            ClearInputData();

            mTuneList.ClearAllData(); 
        }

        private void LoadStaffList()
        {
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader("staff.txt"))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        cmbStaff.Items.Add(line); 
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        private void SetChargedLabelState()
        {
            bool charged = ChargeListManager.GetInstance().GetChargedListChargedStatusForDate(dtpChargeListDate.Value);

            if (charged)
            {
                lblChargeStatus.Text = "✓ Charged";
                lblChargeStatus.ForeColor = Color.Green;
            }
            else
            {
                lblChargeStatus.Text = "X Not Yet Charged";
                lblChargeStatus.ForeColor = Color.Red;
            }
        }

        private void SetChargedButtonState()
        {
            bool charged = ChargeListManager.GetInstance().GetChargedListChargedStatusForDate(dtpChargeListDate.Value); 

            if (charged)
            {
                btnToggleCharged.Text = "Mark As Not Charged"; 
            }
            else
            {
                btnToggleCharged.Text = "Mark As Charged"; 
            }
        }

        private void DDTuneTrackForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void btnGoToToday_Click(object sender, EventArgs e)
        {
            dtpChargeListDate.Value = DateTime.Now;
            DisplayChargeListForCurrentDate(); 
        }

        private void DDTuneTrackForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.Cancel == true)
            {
                // User didn't want to close, we don't have to do anything more. 
                return; 
            }

            DialogResult dialogResult = MessageBox.Show(mCloseDialogDetail, mCloseDialogTitle, MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                // Submit any remaining tunes and then write out charge list to disk
                SubmitTuneList();
                ChargeListManager.GetInstance().WriteChargeListsToDisk();
            }
            else
            {
                // Do nothing
                e.Cancel = true;
                base.OnFormClosing(e);
            }
        }
    }
}
