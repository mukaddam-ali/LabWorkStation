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
            xtraTabControl.Dock = DockStyle.Fill;
            xtraTabControl.Location = new Point(0, 0);
            xtraTabControl.Name = "xtraTabControl";
            xtraTabControl.SelectedTabPage = tabPatientInfo;
            xtraTabControl.Size = new Size(978, 744);
            xtraTabControl.TabIndex = 0;
            xtraTabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] { tabPatientInfo, tabSelectTests, tabEnterValues });
            // 
            // tabPatientInfo
            // 
            tabPatientInfo.Controls.Add(FromAddPatientToMenu);
            tabPatientInfo.Controls.Add(BtnNextToTests);
            tabPatientInfo.Controls.Add(dateEditVisitDate);
            tabPatientInfo.Controls.Add(textEditFullName);
            tabPatientInfo.Controls.Add(labelControl2);
            tabPatientInfo.Controls.Add(labelControl1);
            tabPatientInfo.Name = "tabPatientInfo";
            tabPatientInfo.Size = new Size(976, 709);
            tabPatientInfo.Text = "Patient Info";
            // 
            // FromAddPatientToMenu
            // 
            FromAddPatientToMenu.Location = new Point(224, 331);
            FromAddPatientToMenu.Name = "FromAddPatientToMenu";
            FromAddPatientToMenu.Size = new Size(112, 34);
            FromAddPatientToMenu.TabIndex = 1;
            FromAddPatientToMenu.Text = "Menu";
            FromAddPatientToMenu.UseVisualStyleBackColor = true;
            FromAddPatientToMenu.Click += FromAddPatientToMenu_Click;
            // 
            // BtnNextToTests
            // 
            BtnNextToTests.Location = new Point(603, 331);
            BtnNextToTests.Name = "BtnNextToTests";
            BtnNextToTests.Size = new Size(112, 34);
            BtnNextToTests.TabIndex = 1;
            BtnNextToTests.Text = "Select Tests";
            BtnNextToTests.UseVisualStyleBackColor = true;
            BtnNextToTests.Click += BtnNextToTests_Click;
            // 
            // dateEditVisitDate
            // 
            dateEditVisitDate.EditValue = null;
            dateEditVisitDate.Location = new Point(380, 128);
            dateEditVisitDate.Name = "dateEditVisitDate";
            dateEditVisitDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            dateEditVisitDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            dateEditVisitDate.Size = new Size(225, 26);
            dateEditVisitDate.TabIndex = 3;
            // 
            // textEditFullName
            // 
            textEditFullName.Location = new Point(380, 67);
            textEditFullName.Name = "textEditFullName";
            textEditFullName.Size = new Size(225, 26);
            textEditFullName.TabIndex = 2;
            // 
            // labelControl2
            // 
            labelControl2.Location = new Point(246, 131);
            labelControl2.Name = "labelControl2";
            labelControl2.Size = new Size(67, 19);
            labelControl2.TabIndex = 1;
            labelControl2.Text = "Visit Date";
            // 
            // labelControl1
            // 
            labelControl1.Location = new Point(246, 70);
            labelControl1.Name = "labelControl1";
            labelControl1.Size = new Size(71, 19);
            labelControl1.TabIndex = 0;
            labelControl1.Text = "Full Name";
            // 
            // tabSelectTests
            // 
            tabSelectTests.Controls.Add(BtnBackToPatientInfo);
            tabSelectTests.Controls.Add(SearchLabel);
            tabSelectTests.Controls.Add(txtSearch);
            tabSelectTests.Controls.Add(TestCheckBox);
            tabSelectTests.Controls.Add(btnNextToValues);
            tabSelectTests.Name = "tabSelectTests";
            tabSelectTests.Size = new Size(976, 709);
            tabSelectTests.Text = "Select Tests";
            // 
            // BtnBackToPatientInfo
            // 
            BtnBackToPatientInfo.Location = new Point(170, 354);
            BtnBackToPatientInfo.Name = "BtnBackToPatientInfo";
            BtnBackToPatientInfo.Size = new Size(120, 30);
            BtnBackToPatientInfo.TabIndex = 1;
            BtnBackToPatientInfo.Text = "Back";
            BtnBackToPatientInfo.UseVisualStyleBackColor = true;
            BtnBackToPatientInfo.Click += BtnBackToPatientInfo_Click;
            // 
            // SearchLabel
            // 
            SearchLabel.AutoSize = true;
            SearchLabel.Location = new Point(273, 6);
            SearchLabel.Name = "SearchLabel";
            SearchLabel.Size = new Size(56, 19);
            SearchLabel.TabIndex = 1;
            SearchLabel.Text = "Search";
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(335, 3);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(233, 27);
            txtSearch.TabIndex = 2;
            txtSearch.Click += txtSearch_Click;
            txtSearch.TextChanged += txtSearch_TextChanged;
            txtSearch.Enter += txtSearch_Enter;
            // 
            // TestCheckBox
            // 
            TestCheckBox.FormattingEnabled = true;
            TestCheckBox.Location = new Point(335, 39);
            TestCheckBox.Name = "TestCheckBox";
            TestCheckBox.Size = new Size(233, 340);
            TestCheckBox.TabIndex = 1;
            TestCheckBox.SelectedIndexChanged += TestCheckBox_SelectedIndexChanged;
            // 
            // btnNextToValues
            // 
            btnNextToValues.Location = new Point(617, 354);
            btnNextToValues.Name = "btnNextToValues";
            btnNextToValues.Size = new Size(120, 28);
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
            tabEnterValues.Size = new Size(976, 709);
            tabEnterValues.Text = "Enter Test Values";
            // 
            // BtnBackToSelectTest
            // 
            BtnBackToSelectTest.Location = new Point(314, 630);
            BtnBackToSelectTest.Name = "BtnBackToSelectTest";
            BtnBackToSelectTest.Size = new Size(120, 54);
            BtnBackToSelectTest.TabIndex = 1;
            BtnBackToSelectTest.Text = "Back";
            BtnBackToSelectTest.UseVisualStyleBackColor = true;
            BtnBackToSelectTest.Click += BtnBackToSelectTest_Click;
            // 
            // PatientName3
            // 
            PatientName3.Location = new Point(380, 26);
            PatientName3.Name = "PatientName3";
            PatientName3.Properties.ReadOnly = true;
            PatientName3.Size = new Size(225, 26);
            PatientName3.TabIndex = 1;
            // 
            // Date3
            // 
            Date3.Location = new Point(380, 57);
            Date3.Name = "Date3";
            Date3.Properties.ReadOnly = true;
            Date3.Size = new Size(225, 26);
            Date3.TabIndex = 2;
            // 
            // EditPagePatientTests
            // 
            EditPagePatientTests.Location = new Point(37, 182);
            EditPagePatientTests.Name = "EditPagePatientTests";
            EditPagePatientTests.Size = new Size(928, 442);
            EditPagePatientTests.TabIndex = 0;
            EditPagePatientTests.Click += TestGridView_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(317, 60);
            label6.Name = "label6";
            label6.Size = new Size(57, 19);
            label6.TabIndex = 1;
            label6.Text = "Date : ";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(255, 29);
            label5.Name = "label5";
            label5.Size = new Size(119, 19);
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
            btnSavePatient.Location = new Point(440, 630);
            btnSavePatient.Name = "btnSavePatient";
            btnSavePatient.Size = new Size(120, 54);
            btnSavePatient.TabIndex = 1;
            btnSavePatient.Text = "Save Patient";
            btnSavePatient.Click += btnSavePatient_Click;
            // 
            // AddPatientForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(978, 744);
            Controls.Add(xtraTabControl);
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