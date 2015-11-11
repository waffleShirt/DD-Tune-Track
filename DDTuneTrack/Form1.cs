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
    /* Operating mode enum.
     * The program can operate in either New Tune mode,
     * when a new tune's details are being entered, or in
     * Update Tune mode, where a tune has been selected
     * from the DataGridView and may have it's details
     * updated or completely removed from the grid. 
     */ 
    enum Mode
    {
        Mode_NewTune,
        Mode_UpdateTune,
        Mode_Max,
    };

    public partial class DDTuneTrackForm : Form
    {
        private TuneList mTuneList;
        private Mode mMode = Mode.Mode_NewTune;
        private DateTime mToday = DateTime.Now; 
 
        // Strings for dialog boxes
        private string mSubmitDialogTitle = "Submit all tunes?";
        private string mSubmitDialogDetail = "Are you sure you want to save all the current items in the tune list to file?";
        private string mSubmitDialogNoTunesTitle = "No Tunes To Submit"; 
        private string mSubmitDialogNoTunesDetail = "There are no tunes to submit";
        private string mCloseDialogTitle = "Closing";
        private string mCloseDialogDetail = "Are you sure you want to close? Any unsaved tunes will be automatically saved."; 

        // Date time picker min/max dates
        DateTime mSeasonStartDate = new DateTime(2015, 10, 01);
        DateTime mSeasonEndDate = new DateTime(2016, 04, 30); 

        /// <summary>
        /// Main form constructor
        /// </summary>
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

            // Make sure the tune entry date is set correctly
            UpdateEntryDate(); 

            // Ensure the charged button in the charge list tab has the right text
            UpdateChargedButtonText();

            // Set dateTimePicker date formats
            dtpTuneDate.CustomFormat = CultureHelper.GetInstance().GetDefaultDateFormatString();
            dtpChargeListDate.CustomFormat = CultureHelper.GetInstance().GetDefaultDateFormatString(); 

            // Set dateTimePicker min/max dates
            dtpTuneDate.MinDate = mSeasonStartDate;
            dtpTuneDate.MaxDate = mSeasonEndDate;
            dtpChargeListDate.MinDate = mSeasonStartDate;
            dtpChargeListDate.MaxDate = mSeasonEndDate; 
        }

        //=====================================================================
        // Button and Event Handlers
        //=====================================================================

        /// <summary>
        /// Handles the Clear (New Tune Mode) and Remove (Update Tune Mode)
        /// button being pressed. Redirects program flow to another function
        /// to handle the button pressed based on the current operating mode. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Handles the Save (New Tune Mode) and Update (Update Tune Mode)
        /// button being pressed. Redirects program flow to another function
        /// to handle the button pressed based on the current operating mode. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Handles a KeyUp event on the Asset Number Text Box. 
        /// It will only respond to the Enter key being released.
        /// If the Enter Key is released program flow redirects to
        /// save the current tune details to the DataGridView.  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAssetNumber_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                SavePressed(); 
            }
        }

        /// <summary>
        /// Handles a KeyUp event on the Notes Text Box. It will only respond 
        /// to the Enter key being released. If the Enter Key is released 
        /// program flow redirects to save the current tune details to the 
        /// DataGridView.  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNotes_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                SavePressed();
            }
        }

        /// <summary>
        /// Handles the Submit button being pressed. The handler shows
        /// a dialog box asking the user if they wish to save the current
        /// tune list and handles the Yes/No response accordingly. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Handles the Previous Day button being pressed in the
        /// Charge List tab. When pressed it shows the charge list
        /// from the previous day, so long as the minimum date 
        /// value for Charge Lists isn't already being displayed. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrevDay_Click(object sender, EventArgs e)
        {
            if (dtpChargeListDate.Value.Date != dtpChargeListDate.MinDate)
            {
                dtpChargeListDate.Value = dtpChargeListDate.Value.Date.AddDays(-1);

                // In case next day button has been disabled
                btnGoToNextDay.Enabled = true; 
            }

            // If we're now at the minimum date for the picker disable the prevDay button
            if (dtpChargeListDate.Value.Date == dtpChargeListDate.MinDate)
            {
                btnGoToPrevDay.Enabled = false; 
            }

            DisplayChargeListForCurrentDate();
        }

        /// <summary>
        /// Sets the date in the Charge List date picker to the current date. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGoToToday_Click(object sender, EventArgs e)
        {
            dtpChargeListDate.Value = DateTime.Now;
            DisplayChargeListForCurrentDate();

            btnGoToPrevDay.Enabled = true;
            btnGoToNextDay.Enabled = true; 
        }

        /// Handles the Next Day button being pressed in the
        /// Charge List tab. When pressed it shows the charge list
        /// from the next day, so long as the maximum date 
        /// value for Charge Lists isn't already being displayed. 
        private void btnNextDay_Click(object sender, EventArgs e)
        {
            if (dtpChargeListDate.Value.Date != dtpChargeListDate.MaxDate)
            {
                dtpChargeListDate.Value = dtpChargeListDate.Value.Date.AddDays(1);
                
                // In case prev day button had been disabled
                btnGoToPrevDay.Enabled = true; 
            }

                // If we're now at the maximum date for the picker disable the prevDay button
            if (dtpChargeListDate.Value.Date == dtpChargeListDate.MaxDate)
            {
                btnGoToNextDay.Enabled = false; 
            }

            DisplayChargeListForCurrentDate(); 
        }

        /// <summary>
        /// Handles the Charge List Date pickers date being changed. 
        /// The previous and next day buttons are enabled and disabled
        /// depending on the date selected. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Handles the Mark as Charged (for an uncharged list) or the
        /// Mark as Uncharged (for a charged list) button being pressed. 
        /// The text on the button is toggled depending on the updated
        /// charge status for a charge list after the button is pressed. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnToggleCharged_Click(object sender, EventArgs e)
        {
            ChargeListManager.GetInstance().ToggleChargedStatusForDate(dtpChargeListDate.Value);
            UpdateChargedLabelText();
            UpdateChargedButtonText(); 
        }

        /// <summary>
        /// Handles a row in the Tune List DataGridView being selected. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            OnTuneListRowSelectionChanged();
            HideValidationLabels();
        }

        /// <summary>
        /// Handles the FormClosing event raised when the user tries to close
        /// the program. A DialogBox is shown asking the user if they are sure
        /// they want to quit. Selecting Yes will save all the unsaved data. 
        /// Selecting no will cancel the FormClosing event and nothing further
        /// happens. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Handles a MouseUp event on the Tune List DataGridView. Only handles
        /// the left mouse button going up. If the button goes up and the mouse
        /// was not over one of the current rows in the DataGridView then all
        /// selected rows in the DataGridView are unselected. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv_MouseUp(object sender, MouseEventArgs e)
        {
            // Test if user clicked away from all the rows within the DataGridView. 
            // If they did, then unselect anys elected row. 
            if (e.Button == MouseButtons.Left)
            {
                if (dgv.HitTest(e.X, e.Y).Type == DataGridViewHitTestType.None)
                {
                    ClearDGVSelection();
                }
            }
        }

        /// <summary>
        /// Handles a MouseUp event on the main form. Only handles the left 
        /// mouse button going up If the mouse button goes up any selected rows
        /// in the TuneList DataGridView are unselected. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DDTuneTrackForm_MouseUp(object sender, MouseEventArgs e)
        {
            // Test if user clicked on the form while an item was selected in the DGV. 
            // If one was, clear the selection
            if (e.Button == MouseButtons.Left)
            {
                if (mMode == Mode.Mode_UpdateTune)
                {
                    ClearDGVSelection();
                }
            }
        }

        /// <summary>
        /// Handles a MouseUp event on the Tune Entry tab. Only handles the 
        /// left mouse button going up If the mouse button goes up any selected 
        /// rows in the TuneList DataGridView are unselected. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabPage1_MouseUp(object sender, MouseEventArgs e)
        {
            // Test if user clicked on the tabPage while an item was selected in the DGV. 
            // If one was, clear the selection
            if (e.Button == MouseButtons.Left)
            {
                if (mMode == Mode.Mode_UpdateTune)
                {
                    ClearDGVSelection();
                }
            }
        }

        /// <summary>
        /// Handles the periodic tick of the program timer. When it ticks the
        /// current date and the last saved date are checked. If the new date
        /// is greater than the current date we know it has passed midnight. If
        /// any unsaved data is in the Tune List DataGridView the data is saved
        /// and the dates in the date pickers updated to the new day. This 
        /// means the program is ready for a new day and no data is lost. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("Timer Ticked");
            if (DateTime.Now.Date.CompareTo(mToday.Date) > 0)
            {
                Console.WriteLine("Date Changed!");
                mToday = DateTime.Now;

                // Save out the current tune list
                // Note that as it has passed midnight we want the charge list to be YESTERDAYS date
                SubmitTuneList(DateTime.Now.AddDays(-1));

                // Update date objects
                UpdateEntryDate();
                dtpChargeListDate.Value = DateTime.Now;
                dtpTuneDate.Value = DateTime.Now;
            }
        }

        //=====================================================================
        // Regular Methods
        //=====================================================================

        /// <summary>
        /// Clears all of the data currently in the input controls and resets
        /// any with default data back to the default state. Validation labels
        /// are also hidden. 
        /// </summary>
        private void ClearPressed()
        {
            ClearInputData();
            HideValidationLabels();
        }

        /// <summary>
        /// Removes en existing tune selected in the DataGridView, clears all
        /// input data and resets the program back to New Tune mode. 
        /// </summary>
        private void RemovePressed()
        {
            mTuneList.RemoveExistingTune(); 

            // Clear all input
            ClearInputData(); 

            // Reset mode to Tune Mode
            EnableNewTuneMode(); 
        }

        /// <summary>
        /// Makes a call to validate input data and if all input data is valid 
        /// saves the tune data to the DataGridView. 
        /// </summary>
        private void SavePressed()
        {
            if (ValidateInput())
            {
                // Save Row To Data Grid View
                mTuneList.AddNewTune(txtAssetNumber.Text, cmbTuneType.SelectedItem.ToString(), dtpTuneDate.Value.ToString(CultureHelper.GetInstance().GetDefaultDateFormatString()), txtEntryDate.Text, cmbStaff.SelectedItem.ToString(), txtNotes.Text);
                
                ClearInputOnSaveOrUpdate();

                // Set focus back to asset number
                txtAssetNumber.Focus();
            }
        }

        /// <summary>
        /// Displays the Charge List for the current date in the Charge List 
        /// date picker. 
        /// </summary>
        private void DisplayChargeListForCurrentDate()
        {
            // Display the charge list data for the current date in the text box
            txtChargeList.Text = ChargeListManager.GetInstance().GetChargeListTextForDate(dtpChargeListDate.Value);
            UpdateChargedLabelText();
            UpdateChargedButtonText();
        }

        /// <summary>
        /// Updates the current tune selected in the DataGridView using the 
        /// current input data. 
        /// </summary>
        private void UpdatePressed()
        {
            if (ValidateInput())
            {
                // Update existing row
                mTuneList.UpdateExistingTune(txtAssetNumber.Text, cmbTuneType.SelectedItem.ToString(), dtpTuneDate.Value.ToString(CultureHelper.GetInstance().GetDefaultDateFormatString()), txtEntryDate.Text, cmbStaff.SelectedItem.ToString(), txtNotes.Text);

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
            txtNotes.Text = string.Empty; 
        }

        /// <summary>
        /// Updates the text in the Tune Entry Date text box to the current
        /// date. 
        /// </summary>
        private void UpdateEntryDate()
        {
            txtEntryDate.Text = DateTime.Now.ToString(CultureHelper.GetInstance().GetDefaultDateFormatString()); 
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
            txtNotes.Text = string.Empty; 
        }

        /// <summary>
        /// Hides all validation labels. 
        /// </summary>
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

        /// <summary>
        /// Runs the logic for the selected row in the Tune List being 
        /// changed. When the row is changed Update Mode is enabled and the 
        /// data from the selected row is loaded into the data input 
        /// controls. 
        /// </summary>
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
                dtpTuneDate.Value = DateTime.ParseExact(row.Cells["colTuneDate"].Value.ToString(), CultureHelper.GetInstance().GetDefaultDateFormatString(), null);
                cmbStaff.SelectedIndex = cmbStaff.FindStringExact(row.Cells["colStaff"].Value.ToString());
                txtNotes.Text = row.Cells["colNotes"].Value.ToString(); 
            }
        }

        /// <summary>
        /// Submits the current tune list to be written to the storgage Excel
        /// spreadsheet and the corresponding Charge List to be generated. 
        /// </summary>
        private void SubmitTuneList()
        {
            // Write Tunes to File
            mTuneList.WriteTuneListToFile();

            // Add the new charge list
            ChargeListManager.GetInstance().AddNewChargeList(ChargeListManager.GetInstance().BuildChargeList(mTuneList));

            // Charge list is built. Destroy the DataGridView and clear all data
            ClearInputData();

            mTuneList.ClearAllData();

            dtpChargeListDate.Value = DateTime.Now; 
        }

        /// <summary>
        /// Submits the current tune list to be written to the storgage Excel
        /// spreadsheet and the corresponding Charge List to be generated. The
        /// date of the tune list can be set. This is primarily used when the
        /// tune list is automatically saved after midnight and needs the date
        /// set to the previous days date. 
        /// </summary>
        /// <param name="date">Charge List date</param>
        private void SubmitTuneList(DateTime date)
        {
            // Write Tunes to File
            mTuneList.WriteTuneListToFile(); 
            
            // Add the new charge list
            ChargeListManager.GetInstance().AddNewChargeList(ChargeListManager.GetInstance().BuildChargeList(mTuneList, date));

            // Charge list is built. Destroy the DataGridView and clear all data
            ClearInputData();

            mTuneList.ClearAllData();

            DisplayChargeListForCurrentDate();

            dtpChargeListDate.Value = DateTime.Now; 
        }

        /// <summary>
        /// Loads the staff list from file and updates the values in the staff
        /// ComboBox. 
        /// </summary>
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

        /// <summary>
        /// Sets the text of the Charged label in the Charge List tab. The 
        /// label text is set based on the charged state of the current charge
        /// list. 
        /// </summary>
        private void UpdateChargedLabelText()
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

        /// <summary>
        /// Sets the text of the Charged button in the Charge List tab. The 
        /// button text is set based on the charged state of the current charge
        /// list. 
        /// </summary>
        private void UpdateChargedButtonText()
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

        /// <summary>
        /// Unselects all selected rows in the TuneList DataGridView. 
        /// </summary>
        private void ClearDGVSelection()
        {
            dgv.ClearSelection();
            ClearInputData();
            EnableNewTuneMode();
        }
    }
}
