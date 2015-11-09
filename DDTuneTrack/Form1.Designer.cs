namespace DDTuneTrack
{
    partial class DDTuneTrackForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnSuppressDing = new System.Windows.Forms.Button();
            this.lblStaffError = new System.Windows.Forms.Label();
            this.lblTuneDateError = new System.Windows.Forms.Label();
            this.lblTuneTypeError = new System.Windows.Forms.Label();
            this.lblAssetNumberError = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.btnSaveUpdate = new System.Windows.Forms.Button();
            this.btnClearRemove = new System.Windows.Forms.Button();
            this.txtEntryDate = new System.Windows.Forms.TextBox();
            this.dtpTuneDate = new System.Windows.Forms.DateTimePicker();
            this.cmbStaff = new System.Windows.Forms.ComboBox();
            this.cmbTuneType = new System.Windows.Forms.ComboBox();
            this.txtAssetNumber = new System.Windows.Forms.TextBox();
            this.lblStaff = new System.Windows.Forms.Label();
            this.lblEntryDate = new System.Windows.Forms.Label();
            this.lblTuneDate = new System.Windows.Forms.Label();
            this.lblTuneType = new System.Windows.Forms.Label();
            this.lblAssetNumber = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnNextDay = new System.Windows.Forms.Button();
            this.btnPrevDay = new System.Windows.Forms.Button();
            this.dtpChargeListDate = new System.Windows.Forms.DateTimePicker();
            this.lblChargeListDate = new System.Windows.Forms.Label();
            this.lblChargeStatus = new System.Windows.Forms.Label();
            this.btnToggleCharged = new System.Windows.Forms.Button();
            this.txtChargeList = new System.Windows.Forms.TextBox();
            this.lblNotes = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(754, 554);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtNotes);
            this.tabPage1.Controls.Add(this.lblNotes);
            this.tabPage1.Controls.Add(this.btnSuppressDing);
            this.tabPage1.Controls.Add(this.lblStaffError);
            this.tabPage1.Controls.Add(this.lblTuneDateError);
            this.tabPage1.Controls.Add(this.lblTuneTypeError);
            this.tabPage1.Controls.Add(this.lblAssetNumberError);
            this.tabPage1.Controls.Add(this.btnSubmit);
            this.tabPage1.Controls.Add(this.dgv);
            this.tabPage1.Controls.Add(this.btnSaveUpdate);
            this.tabPage1.Controls.Add(this.btnClearRemove);
            this.tabPage1.Controls.Add(this.txtEntryDate);
            this.tabPage1.Controls.Add(this.dtpTuneDate);
            this.tabPage1.Controls.Add(this.cmbStaff);
            this.tabPage1.Controls.Add(this.cmbTuneType);
            this.tabPage1.Controls.Add(this.txtAssetNumber);
            this.tabPage1.Controls.Add(this.lblStaff);
            this.tabPage1.Controls.Add(this.lblEntryDate);
            this.tabPage1.Controls.Add(this.lblTuneDate);
            this.tabPage1.Controls.Add(this.lblTuneType);
            this.tabPage1.Controls.Add(this.lblAssetNumber);
            this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(746, 528);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Ski/Board Tune Entry";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnSuppressDing
            // 
            this.btnSuppressDing.Enabled = false;
            this.btnSuppressDing.Location = new System.Drawing.Point(417, 489);
            this.btnSuppressDing.Name = "btnSuppressDing";
            this.btnSuppressDing.Size = new System.Drawing.Size(75, 23);
            this.btnSuppressDing.TabIndex = 20;
            this.btnSuppressDing.Text = "button1";
            this.btnSuppressDing.UseVisualStyleBackColor = true;
            this.btnSuppressDing.Visible = false;
            // 
            // lblStaffError
            // 
            this.lblStaffError.AutoSize = true;
            this.lblStaffError.ForeColor = System.Drawing.Color.Red;
            this.lblStaffError.Location = new System.Drawing.Point(478, 162);
            this.lblStaffError.Name = "lblStaffError";
            this.lblStaffError.Size = new System.Drawing.Size(217, 25);
            this.lblStaffError.TabIndex = 19;
            this.lblStaffError.Text = "Staff Member Not Set";
            this.lblStaffError.Visible = false;
            // 
            // lblTuneDateError
            // 
            this.lblTuneDateError.AutoSize = true;
            this.lblTuneDateError.ForeColor = System.Drawing.Color.Red;
            this.lblTuneDateError.Location = new System.Drawing.Point(478, 90);
            this.lblTuneDateError.Name = "lblTuneDateError";
            this.lblTuneDateError.Size = new System.Drawing.Size(189, 25);
            this.lblTuneDateError.TabIndex = 17;
            this.lblTuneDateError.Text = "Tune Date Not Set";
            this.lblTuneDateError.Visible = false;
            // 
            // lblTuneTypeError
            // 
            this.lblTuneTypeError.AutoSize = true;
            this.lblTuneTypeError.ForeColor = System.Drawing.Color.Red;
            this.lblTuneTypeError.Location = new System.Drawing.Point(478, 49);
            this.lblTuneTypeError.Name = "lblTuneTypeError";
            this.lblTuneTypeError.Size = new System.Drawing.Size(192, 25);
            this.lblTuneTypeError.TabIndex = 16;
            this.lblTuneTypeError.Text = "Tune Type Not Set";
            this.lblTuneTypeError.Visible = false;
            // 
            // lblAssetNumberError
            // 
            this.lblAssetNumberError.AutoSize = true;
            this.lblAssetNumberError.ForeColor = System.Drawing.Color.Red;
            this.lblAssetNumberError.Location = new System.Drawing.Point(478, 12);
            this.lblAssetNumberError.Name = "lblAssetNumberError";
            this.lblAssetNumberError.Size = new System.Drawing.Size(224, 25);
            this.lblAssetNumberError.TabIndex = 15;
            this.lblAssetNumberError.Text = "Asset Number Not Set";
            this.lblAssetNumberError.Visible = false;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(640, 481);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(100, 38);
            this.btnSubmit.TabIndex = 14;
            this.btnSubmit.Text = "Submit Tune List";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // dgv
            // 
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(6, 278);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.Size = new System.Drawing.Size(734, 197);
            this.dgv.TabIndex = 13;
            this.dgv.SelectionChanged += new System.EventHandler(this.dgv_SelectionChanged);
            // 
            // btnSaveUpdate
            // 
            this.btnSaveUpdate.Location = new System.Drawing.Point(368, 235);
            this.btnSaveUpdate.Name = "btnSaveUpdate";
            this.btnSaveUpdate.Size = new System.Drawing.Size(105, 37);
            this.btnSaveUpdate.TabIndex = 12;
            this.btnSaveUpdate.Text = "Save";
            this.btnSaveUpdate.UseVisualStyleBackColor = true;
            this.btnSaveUpdate.Click += new System.EventHandler(this.btnSaveUpdate_Click);
            // 
            // btnClearRemove
            // 
            this.btnClearRemove.Location = new System.Drawing.Point(257, 235);
            this.btnClearRemove.Name = "btnClearRemove";
            this.btnClearRemove.Size = new System.Drawing.Size(105, 37);
            this.btnClearRemove.TabIndex = 11;
            this.btnClearRemove.Text = "Clear";
            this.btnClearRemove.UseVisualStyleBackColor = true;
            this.btnClearRemove.Click += new System.EventHandler(this.btnClearRemove_Click);
            // 
            // txtEntryDate
            // 
            this.txtEntryDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEntryDate.Location = new System.Drawing.Point(163, 122);
            this.txtEntryDate.Name = "txtEntryDate";
            this.txtEntryDate.ReadOnly = true;
            this.txtEntryDate.Size = new System.Drawing.Size(309, 31);
            this.txtEntryDate.TabIndex = 10;
            // 
            // dtpTuneDate
            // 
            this.dtpTuneDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTuneDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTuneDate.Location = new System.Drawing.Point(163, 85);
            this.dtpTuneDate.MaxDate = new System.DateTime(2016, 4, 30, 0, 0, 0, 0);
            this.dtpTuneDate.MinDate = new System.DateTime(2015, 10, 1, 0, 0, 0, 0);
            this.dtpTuneDate.Name = "dtpTuneDate";
            this.dtpTuneDate.Size = new System.Drawing.Size(309, 31);
            this.dtpTuneDate.TabIndex = 9;
            // 
            // cmbStaff
            // 
            this.cmbStaff.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStaff.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStaff.FormattingEnabled = true;
            this.cmbStaff.Location = new System.Drawing.Point(163, 159);
            this.cmbStaff.Name = "cmbStaff";
            this.cmbStaff.Size = new System.Drawing.Size(309, 33);
            this.cmbStaff.TabIndex = 8;
            // 
            // cmbTuneType
            // 
            this.cmbTuneType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTuneType.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTuneType.FormattingEnabled = true;
            this.cmbTuneType.Location = new System.Drawing.Point(162, 46);
            this.cmbTuneType.Name = "cmbTuneType";
            this.cmbTuneType.Size = new System.Drawing.Size(310, 33);
            this.cmbTuneType.TabIndex = 6;
            // 
            // txtAssetNumber
            // 
            this.txtAssetNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAssetNumber.Location = new System.Drawing.Point(162, 9);
            this.txtAssetNumber.Name = "txtAssetNumber";
            this.txtAssetNumber.Size = new System.Drawing.Size(310, 31);
            this.txtAssetNumber.TabIndex = 5;
            this.txtAssetNumber.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtAssetNumber_KeyUp);
            // 
            // lblStaff
            // 
            this.lblStaff.AutoSize = true;
            this.lblStaff.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStaff.Location = new System.Drawing.Point(3, 167);
            this.lblStaff.Name = "lblStaff";
            this.lblStaff.Size = new System.Drawing.Size(62, 25);
            this.lblStaff.TabIndex = 4;
            this.lblStaff.Text = "Staff:";
            // 
            // lblEntryDate
            // 
            this.lblEntryDate.AutoSize = true;
            this.lblEntryDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntryDate.Location = new System.Drawing.Point(3, 128);
            this.lblEntryDate.Name = "lblEntryDate";
            this.lblEntryDate.Size = new System.Drawing.Size(119, 25);
            this.lblEntryDate.TabIndex = 3;
            this.lblEntryDate.Text = "Entry Date:";
            // 
            // lblTuneDate
            // 
            this.lblTuneDate.AutoSize = true;
            this.lblTuneDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTuneDate.Location = new System.Drawing.Point(3, 91);
            this.lblTuneDate.Name = "lblTuneDate";
            this.lblTuneDate.Size = new System.Drawing.Size(118, 25);
            this.lblTuneDate.TabIndex = 2;
            this.lblTuneDate.Text = "Tune Date:";
            // 
            // lblTuneType
            // 
            this.lblTuneType.AutoSize = true;
            this.lblTuneType.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTuneType.Location = new System.Drawing.Point(3, 54);
            this.lblTuneType.Name = "lblTuneType";
            this.lblTuneType.Size = new System.Drawing.Size(121, 25);
            this.lblTuneType.TabIndex = 1;
            this.lblTuneType.Text = "Tune Type:";
            // 
            // lblAssetNumber
            // 
            this.lblAssetNumber.AutoSize = true;
            this.lblAssetNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAssetNumber.Location = new System.Drawing.Point(3, 15);
            this.lblAssetNumber.Name = "lblAssetNumber";
            this.lblAssetNumber.Size = new System.Drawing.Size(153, 25);
            this.lblAssetNumber.TabIndex = 0;
            this.lblAssetNumber.Text = "Asset Number:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnNextDay);
            this.tabPage2.Controls.Add(this.btnPrevDay);
            this.tabPage2.Controls.Add(this.dtpChargeListDate);
            this.tabPage2.Controls.Add(this.lblChargeListDate);
            this.tabPage2.Controls.Add(this.lblChargeStatus);
            this.tabPage2.Controls.Add(this.btnToggleCharged);
            this.tabPage2.Controls.Add(this.txtChargeList);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(656, 528);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Tune Charge Lists";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnNextDay
            // 
            this.btnNextDay.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNextDay.Location = new System.Drawing.Point(522, 13);
            this.btnNextDay.Name = "btnNextDay";
            this.btnNextDay.Size = new System.Drawing.Size(128, 31);
            this.btnNextDay.TabIndex = 6;
            this.btnNextDay.Text = "Next Day";
            this.btnNextDay.UseVisualStyleBackColor = true;
            this.btnNextDay.Click += new System.EventHandler(this.btnNextDay_Click);
            // 
            // btnPrevDay
            // 
            this.btnPrevDay.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrevDay.Location = new System.Drawing.Point(351, 14);
            this.btnPrevDay.Name = "btnPrevDay";
            this.btnPrevDay.Size = new System.Drawing.Size(165, 31);
            this.btnPrevDay.TabIndex = 5;
            this.btnPrevDay.Text = "Previous Day";
            this.btnPrevDay.UseVisualStyleBackColor = true;
            this.btnPrevDay.Click += new System.EventHandler(this.btnPrevDay_Click);
            // 
            // dtpChargeListDate
            // 
            this.dtpChargeListDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpChargeListDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpChargeListDate.Location = new System.Drawing.Point(75, 14);
            this.dtpChargeListDate.MaxDate = new System.DateTime(2016, 4, 30, 0, 0, 0, 0);
            this.dtpChargeListDate.MinDate = new System.DateTime(2015, 10, 1, 0, 0, 0, 0);
            this.dtpChargeListDate.Name = "dtpChargeListDate";
            this.dtpChargeListDate.Size = new System.Drawing.Size(200, 31);
            this.dtpChargeListDate.TabIndex = 4;
            this.dtpChargeListDate.Value = new System.DateTime(2015, 11, 6, 0, 0, 0, 0);
            this.dtpChargeListDate.ValueChanged += new System.EventHandler(this.dtpChargeListDate_ValueChanged);
            // 
            // lblChargeListDate
            // 
            this.lblChargeListDate.AutoSize = true;
            this.lblChargeListDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChargeListDate.Location = new System.Drawing.Point(8, 19);
            this.lblChargeListDate.Name = "lblChargeListDate";
            this.lblChargeListDate.Size = new System.Drawing.Size(63, 25);
            this.lblChargeListDate.TabIndex = 3;
            this.lblChargeListDate.Text = "Date:";
            // 
            // lblChargeStatus
            // 
            this.lblChargeStatus.AutoSize = true;
            this.lblChargeStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChargeStatus.ForeColor = System.Drawing.Color.Red;
            this.lblChargeStatus.Location = new System.Drawing.Point(3, 485);
            this.lblChargeStatus.Name = "lblChargeStatus";
            this.lblChargeStatus.Size = new System.Drawing.Size(304, 37);
            this.lblChargeStatus.TabIndex = 2;
            this.lblChargeStatus.Text = "X Not Yet Charged";
            // 
            // btnToggleCharged
            // 
            this.btnToggleCharged.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnToggleCharged.Location = new System.Drawing.Point(437, 485);
            this.btnToggleCharged.Name = "btnToggleCharged";
            this.btnToggleCharged.Size = new System.Drawing.Size(213, 37);
            this.btnToggleCharged.TabIndex = 1;
            this.btnToggleCharged.Text = "Mark As Charged";
            this.btnToggleCharged.UseVisualStyleBackColor = true;
            this.btnToggleCharged.Click += new System.EventHandler(this.btnToggleCharged_Click);
            // 
            // txtChargeList
            // 
            this.txtChargeList.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChargeList.Location = new System.Drawing.Point(3, 50);
            this.txtChargeList.Multiline = true;
            this.txtChargeList.Name = "txtChargeList";
            this.txtChargeList.ReadOnly = true;
            this.txtChargeList.Size = new System.Drawing.Size(647, 429);
            this.txtChargeList.TabIndex = 0;
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotes.Location = new System.Drawing.Point(3, 204);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(124, 25);
            this.lblNotes.TabIndex = 21;
            this.lblNotes.Text = "Notes/Parts";
            // 
            // txtNotes
            // 
            this.txtNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNotes.Location = new System.Drawing.Point(163, 198);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(310, 31);
            this.txtNotes.TabIndex = 22;
            // 
            // DDTuneTrackForm
            // 
            this.AcceptButton = this.btnSuppressDing;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 578);
            this.Controls.Add(this.tabControl1);
            this.Name = "DDTuneTrackForm";
            this.Text = "Double Diamond Tune Tracker - Winter 2015/2016";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DDTuneTrackForm_FormClosed);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DateTimePicker dtpTuneDate;
        private System.Windows.Forms.ComboBox cmbStaff;
        private System.Windows.Forms.ComboBox cmbTuneType;
        private System.Windows.Forms.TextBox txtAssetNumber;
        private System.Windows.Forms.Label lblStaff;
        private System.Windows.Forms.Label lblEntryDate;
        private System.Windows.Forms.Label lblTuneDate;
        private System.Windows.Forms.Label lblTuneType;
        private System.Windows.Forms.Label lblAssetNumber;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button btnSaveUpdate;
        private System.Windows.Forms.Button btnClearRemove;
        private System.Windows.Forms.TextBox txtEntryDate;
        private System.Windows.Forms.Label lblChargeStatus;
        private System.Windows.Forms.Button btnToggleCharged;
        private System.Windows.Forms.TextBox txtChargeList;
        private System.Windows.Forms.Button btnNextDay;
        private System.Windows.Forms.Button btnPrevDay;
        private System.Windows.Forms.DateTimePicker dtpChargeListDate;
        private System.Windows.Forms.Label lblChargeListDate;
        private System.Windows.Forms.Label lblStaffError;
        private System.Windows.Forms.Label lblTuneDateError;
        private System.Windows.Forms.Label lblTuneTypeError;
        private System.Windows.Forms.Label lblAssetNumberError;
        private System.Windows.Forms.Button btnSuppressDing;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Label lblNotes;
    }
}

