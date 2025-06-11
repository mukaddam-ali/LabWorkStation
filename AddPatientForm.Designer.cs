namespace Lab
{
    partial class AddPatientForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddPatientForm));
            xtraTabControl = new DevExpress.XtraTab.XtraTabControl();
            tabPatientInfo = new DevExpress.XtraTab.XtraTabPage();
            FromAddPatientToMenu = new Button();
            BtnNextToTests = new Button();
            dateEditVisitDate = new DevExpress.XtraEditors.DateEdit();
            textEditFullName = new DevExpress.XtraEditors.TextEdit();
            labelControl2 = new DevExpress.XtraEditors.LabelControl();
            labelControl1 = new DevExpress.XtraEditors.LabelControl();
            tabSelectTests = new DevExpress.XtraTab.XtraTabPage();
            BtnBackToPatientInfo = new Button();
            SearchLabel = new Label();
            txtSearch = new TextBox();
            TestCheckBox = new CheckedListBox();
            btnNextToValues = new DevExpress.XtraEditors.SimpleButton();
            tabEnterValues = new DevExpress.XtraTab.XtraTabPage();
            BtnBackToSelectTest = new Button();
            PatientName3 = new DevExpress.XtraEditors.TextEdit();
            Date3 = new DevExpress.XtraEditors.TextEdit();
            EditPagePatientTests = new DevExpress.XtraEditors.XtraScrollableControl();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            btnSavePatient = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)xtraTabControl).BeginInit();
            xtraTabControl.SuspendLayout();
            tabPatientInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dateEditVisitDate.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dateEditVisitDate.Properties.CalendarTimeProperties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)textEditFullName.Properties).BeginInit();
            tabSelectTests.SuspendLayout();
            tabEnterValues.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PatientName3.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Date3.Properties).BeginInit();
            SuspendLayout();
            // 
            // xtraTabControl
            // 
            xtraTabControl.Appearance.BackColor = Color.White;
            xtraTabControl.Appearance.Options.UseBackColor = true;
            xtraTabControl.Dock = DockStyle.Fill;
            xtraTabControl.Location = new Point(0, 0);
            xtraTabControl.Name = "xtraTabControl";
            xtraTabControl.SelectedTabPage = tabPatientInfo;
            xtraTabControl.Size = new Size(1077, 711);
            xtraTabControl.TabIndex = 0;
            xtraTabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] { tabPatientInfo, tabSelectTests, tabEnterValues });
            // 
            // tabPatientInfo
            // 
            tabPatientInfo.Appearance.PageClient.BackColor = Color.White;
            tabPatientInfo.Appearance.PageClient.Options.UseBackColor = true;
            tabPatientInfo.Controls.Add(FromAddPatientToMenu);
            tabPatientInfo.Controls.Add(BtnNextToTests);
            tabPatientInfo.Controls.Add(dateEditVisitDate);
            tabPatientInfo.Controls.Add(textEditFullName);
            tabPatientInfo.Controls.Add(labelControl2);
            tabPatientInfo.Controls.Add(labelControl1);
            tabPatientInfo.Name = "tabPatientInfo";
            tabPatientInfo.Size = new Size(1075, 676);
            tabPatientInfo.Text = "Patient Info";
            // 
            // FromAddPatientToMenu
            // 
            FromAddPatientToMenu.Font = new Font("Tahoma", 11F);
            FromAddPatientToMenu.Location = new Point(344, 417);
            FromAddPatientToMenu.Name = "FromAddPatientToMenu";
            FromAddPatientToMenu.Size = new Size(128, 43);
            FromAddPatientToMenu.TabIndex = 1;
            FromAddPatientToMenu.Text = "Menu";
            FromAddPatientToMenu.UseVisualStyleBackColor = true;
            FromAddPatientToMenu.Click += FromAddPatientToMenu_Click;
            // 
            // BtnNextToTests
            // 
            BtnNextToTests.Font = new Font("Tahoma", 11F);
            BtnNextToTests.Location = new Point(610, 417);
            BtnNextToTests.Name = "BtnNextToTests";
            BtnNextToTests.Size = new Size(128, 43);
            BtnNextToTests.TabIndex = 1;
            BtnNextToTests.Text = "Next";
            BtnNextToTests.UseVisualStyleBackColor = true;
            BtnNextToTests.Click += BtnNextToTests_Click;
            // 
            // dateEditVisitDate
            // 
            dateEditVisitDate.EditValue = null;
            dateEditVisitDate.Location = new Point(513, 238);
            dateEditVisitDate.Name = "dateEditVisitDate";
            dateEditVisitDate.Properties.Appearance.Font = new Font("Tahoma", 11F);
            dateEditVisitDate.Properties.Appearance.Options.UseFont = true;
            dateEditVisitDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            dateEditVisitDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            dateEditVisitDate.Size = new Size(225, 34);
            dateEditVisitDate.TabIndex = 3;
            // 
            // textEditFullName
            // 
            textEditFullName.Location = new Point(513, 140);
            textEditFullName.Name = "textEditFullName";
            textEditFullName.Properties.Appearance.Font = new Font("Tahoma", 11F);
            textEditFullName.Properties.Appearance.Options.UseFont = true;
            textEditFullName.Size = new Size(225, 34);
            textEditFullName.TabIndex = 2;
            // 
            // labelControl2
            // 
            labelControl2.Appearance.Font = new Font("Tahoma", 11F);
            labelControl2.Appearance.Options.UseFont = true;
            labelControl2.Location = new Point(344, 241);
            labelControl2.Name = "labelControl2";
            labelControl2.Size = new Size(108, 27);
            labelControl2.TabIndex = 1;
            labelControl2.Text = "Visit Date :";
            // 
            // labelControl1
            // 
            labelControl1.Appearance.Font = new Font("Tahoma", 11F);
            labelControl1.Appearance.Options.UseFont = true;
            labelControl1.Location = new Point(344, 143);
            labelControl1.Name = "labelControl1";
            labelControl1.Size = new Size(119, 27);
            labelControl1.TabIndex = 0;
            labelControl1.Text = "Full Name : ";
            // 
            // tabSelectTests
            // 
            tabSelectTests.Appearance.PageClient.BackColor = Color.White;
            tabSelectTests.Appearance.PageClient.Options.UseBackColor = true;
            tabSelectTests.Controls.Add(BtnBackToPatientInfo);
            tabSelectTests.Controls.Add(SearchLabel);
            tabSelectTests.Controls.Add(txtSearch);
            tabSelectTests.Controls.Add(TestCheckBox);
            tabSelectTests.Controls.Add(btnNextToValues);
            tabSelectTests.Name = "tabSelectTests";
            tabSelectTests.Size = new Size(1075, 676);
            tabSelectTests.Text = "Select Tests";
            // 
            // BtnBackToPatientInfo
            // 
            BtnBackToPatientInfo.Font = new Font("Tahoma", 11F);
            BtnBackToPatientInfo.Location = new Point(392, 471);
            BtnBackToPatientInfo.Name = "BtnBackToPatientInfo";
            BtnBackToPatientInfo.Size = new Size(128, 43);
            BtnBackToPatientInfo.TabIndex = 1;
            BtnBackToPatientInfo.Text = "Back";
            BtnBackToPatientInfo.UseVisualStyleBackColor = true;
            BtnBackToPatientInfo.Click += BtnBackToPatientInfo_Click;
            // 
            // SearchLabel
            // 
            SearchLabel.AutoSize = true;
            SearchLabel.Font = new Font("Tahoma", 11F);
            SearchLabel.Location = new Point(392, 73);
            SearchLabel.Name = "SearchLabel";
            SearchLabel.Size = new Size(78, 27);
            SearchLabel.TabIndex = 1;
            SearchLabel.Text = "Search";
            // 
            // txtSearch
            // 
            txtSearch.Font = new Font("Tahoma", 11F);
            txtSearch.Location = new Point(478, 73);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(233, 34);
            txtSearch.TabIndex = 2;
            txtSearch.Click += txtSearch_Click;
            txtSearch.TextChanged += txtSearch_TextChanged;
            txtSearch.Enter += txtSearch_Enter;
            // 
            // TestCheckBox
            // 
            TestCheckBox.Font = new Font("Tahoma", 11F);
            TestCheckBox.FormattingEnabled = true;
            TestCheckBox.Location = new Point(394, 129);
            TestCheckBox.Name = "TestCheckBox";
            TestCheckBox.Size = new Size(317, 314);
            TestCheckBox.TabIndex = 1;
            TestCheckBox.SelectedIndexChanged += TestCheckBox_SelectedIndexChanged;
            // 
            // btnNextToValues
            // 
            btnNextToValues.Appearance.Font = new Font("Tahoma", 11F);
            btnNextToValues.Appearance.Options.UseFont = true;
            btnNextToValues.Location = new Point(583, 470);
            btnNextToValues.Name = "btnNextToValues";
            btnNextToValues.Size = new Size(128, 43);
            btnNextToValues.TabIndex = 1;
            btnNextToValues.Text = "Next";
            btnNextToValues.Click += btnNextToValues_Click;
            // 
            // tabEnterValues
            // 
            tabEnterValues.Controls.Add(BtnBackToSelectTest);
            tabEnterValues.Controls.Add(PatientName3);
            tabEnterValues.Controls.Add(Date3);
            tabEnterValues.Controls.Add(EditPagePatientTests);
            tabEnterValues.Controls.Add(label6);
            tabEnterValues.Controls.Add(label5);
            tabEnterValues.Controls.Add(label4);
            tabEnterValues.Controls.Add(label3);
            tabEnterValues.Controls.Add(label2);
            tabEnterValues.Controls.Add(btnSavePatient);
            tabEnterValues.Name = "tabEnterValues";
            tabEnterValues.Size = new Size(1075, 676);
            tabEnterValues.Text = "Enter Test Values";
            // 
            // BtnBackToSelectTest
            // 
            BtnBackToSelectTest.Font = new Font("Tahoma", 11F);
            BtnBackToSelectTest.Location = new Point(282, 590);
            BtnBackToSelectTest.Name = "BtnBackToSelectTest";
            BtnBackToSelectTest.Size = new Size(128, 43);
            BtnBackToSelectTest.TabIndex = 1;
            BtnBackToSelectTest.Text = "Back";
            BtnBackToSelectTest.UseVisualStyleBackColor = true;
            BtnBackToSelectTest.Click += BtnBackToSelectTest_Click;
            // 
            // PatientName3
            // 
            PatientName3.Location = new Point(494, 25);
            PatientName3.Name = "PatientName3";
            PatientName3.Properties.Appearance.Font = new Font("Tahoma", 11F);
            PatientName3.Properties.Appearance.Options.UseFont = true;
            PatientName3.Properties.ReadOnly = true;
            PatientName3.Size = new Size(225, 34);
            PatientName3.TabIndex = 1;
            // 
            // Date3
            // 
            Date3.Location = new Point(494, 65);
            Date3.Name = "Date3";
            Date3.Properties.Appearance.Font = new Font("Tahoma", 11F);
            Date3.Properties.Appearance.Options.UseFont = true;
            Date3.Properties.ReadOnly = true;
            Date3.Size = new Size(225, 34);
            Date3.TabIndex = 2;
            // 
            // EditPagePatientTests
            // 
            EditPagePatientTests.Font = new Font("Tahoma", 11F);
            EditPagePatientTests.Location = new Point(20, 105);
            EditPagePatientTests.Name = "EditPagePatientTests";
            EditPagePatientTests.Size = new Size(1035, 457);
            EditPagePatientTests.TabIndex = 0;
            EditPagePatientTests.Click += TestGridView_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Tahoma", 11F);
            label6.Location = new Point(367, 68);
            label6.Name = "label6";
            label6.Size = new Size(80, 27);
            label6.TabIndex = 1;
            label6.Text = "Date : ";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Tahoma", 11F);
            label5.Location = new Point(282, 28);
            label5.Name = "label5";
            label5.Size = new Size(165, 27);
            label5.TabIndex = 1;
            label5.Text = "Patient Name : ";
            // 
            // label4
            // 
            label4.Location = new Point(0, 0);
            label4.Name = "label4";
            label4.Size = new Size(100, 23);
            label4.TabIndex = 3;
            // 
            // label3
            // 
            label3.Location = new Point(0, 0);
            label3.Name = "label3";
            label3.Size = new Size(100, 23);
            label3.TabIndex = 4;
            // 
            // label2
            // 
            label2.Location = new Point(0, 0);
            label2.Name = "label2";
            label2.Size = new Size(100, 23);
            label2.TabIndex = 5;
            // 
            // btnSavePatient
            // 
            btnSavePatient.Appearance.Font = new Font("Tahoma", 11F);
            btnSavePatient.Appearance.Options.UseFont = true;
            btnSavePatient.Location = new Point(591, 589);
            btnSavePatient.Name = "btnSavePatient";
            btnSavePatient.Size = new Size(128, 43);
            btnSavePatient.TabIndex = 1;
            btnSavePatient.Text = "Save Patient";
            btnSavePatient.Click += btnSavePatient_Click;
            // 
            // AddPatientForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1077, 711);
            Controls.Add(xtraTabControl);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "AddPatientForm";
            ((System.ComponentModel.ISupportInitialize)xtraTabControl).EndInit();
            xtraTabControl.ResumeLayout(false);
            tabPatientInfo.ResumeLayout(false);
            tabPatientInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dateEditVisitDate.Properties.CalendarTimeProperties).EndInit();
            ((System.ComponentModel.ISupportInitialize)dateEditVisitDate.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)textEditFullName.Properties).EndInit();
            tabSelectTests.ResumeLayout(false);
            tabSelectTests.PerformLayout();
            tabEnterValues.ResumeLayout(false);
            tabEnterValues.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PatientName3.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)Date3.Properties).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl;
        private DevExpress.XtraTab.XtraTabPage tabPatientInfo;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraTab.XtraTabPage tabSelectTests;
        private DevExpress.XtraTab.XtraTabPage tabEnterValues;
        private DevExpress.XtraEditors.DateEdit dateEditVisitDate;
        private DevExpress.XtraEditors.TextEdit textEditFullName;
        private DevExpress.XtraEditors.SimpleButton btnNextToValues;
        private DevExpress.XtraEditors.SimpleButton btnSavePatient;
        private DevExpress.XtraEditors.XtraScrollableControl EditPagePatientTests;
        private Button BtnNextToTests;
        private CheckedListBox TestCheckBox;
        private Label SearchLabel;
        private TextBox txtSearch;
        private Label label4;
        private Label label3;
        private Label label2;
        private DevExpress.XtraEditors.TextEdit PatientName3;
        private DevExpress.XtraEditors.TextEdit Date3;
        private Label label6;
        private Label label5;
        private Button BtnBackToPatientInfo;
        private Button BtnBackToSelectTest;
        private Button FromAddPatientToMenu;
    }
}