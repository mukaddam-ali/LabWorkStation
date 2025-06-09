namespace Lab
{
    partial class ViewPatient
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
            ViewPatientPage = new DevExpress.XtraTab.XtraTabControl();
            SearchPatientPage = new DevExpress.XtraTab.XtraTabPage();
            DeleteSelectedPatient = new Button();
            NextToPreview = new Button();
            checkedListBox1 = new CheckedListBox();
            textBox1 = new TextBox();
            EditPatientPage = new DevExpress.XtraTab.XtraTabPage();
            ModifySelectedTests = new CheckedListBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            SaveEditedPatient = new Button();
            EditPagePatientTests = new DevExpress.XtraEditors.XtraScrollableControl();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            EditPagePatientDate = new TextBox();
            EditPagePatientName = new TextBox();
            SearchToModifySelectedTest = new TextBox();
            ((System.ComponentModel.ISupportInitialize)ViewPatientPage).BeginInit();
            ViewPatientPage.SuspendLayout();
            SearchPatientPage.SuspendLayout();
            EditPatientPage.SuspendLayout();
            SuspendLayout();
            // 
            // ViewPatientPage
            // 
            ViewPatientPage.Dock = DockStyle.Fill;
            ViewPatientPage.Location = new Point(0, 0);
            ViewPatientPage.Name = "ViewPatientPage";
            ViewPatientPage.SelectedTabPage = SearchPatientPage;
            ViewPatientPage.Size = new Size(978, 744);
            ViewPatientPage.TabIndex = 0;
            ViewPatientPage.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] { SearchPatientPage, EditPatientPage });
            // 
            // SearchPatientPage
            // 
            SearchPatientPage.Controls.Add(DeleteSelectedPatient);
            SearchPatientPage.Controls.Add(NextToPreview);
            SearchPatientPage.Controls.Add(checkedListBox1);
            SearchPatientPage.Controls.Add(textBox1);
            SearchPatientPage.Name = "SearchPatientPage";
            SearchPatientPage.Size = new Size(976, 709);
            SearchPatientPage.Text = "Search Patient";
            // 
            // DeleteSelectedPatient
            // 
            DeleteSelectedPatient.Location = new Point(400, 461);
            DeleteSelectedPatient.Name = "DeleteSelectedPatient";
            DeleteSelectedPatient.Size = new Size(202, 34);
            DeleteSelectedPatient.TabIndex = 1;
            DeleteSelectedPatient.Text = "Delete Selected Patient";
            DeleteSelectedPatient.UseVisualStyleBackColor = true;
            DeleteSelectedPatient.Click += DeleteSelectedPatient_Click;
            // 
            // NextToPreview
            // 
            NextToPreview.Location = new Point(445, 421);
            NextToPreview.Name = "NextToPreview";
            NextToPreview.Size = new Size(112, 34);
            NextToPreview.TabIndex = 1;
            NextToPreview.Text = "Preview";
            NextToPreview.UseVisualStyleBackColor = true;
            NextToPreview.Click += NextToPreview_Click;
            // 
            // checkedListBox1
            // 
            checkedListBox1.FormattingEnabled = true;
            checkedListBox1.Location = new Point(279, 147);
            checkedListBox1.Name = "checkedListBox1";
            checkedListBox1.Size = new Size(418, 268);
            checkedListBox1.TabIndex = 1;
            checkedListBox1.SelectedIndexChanged += checkedListBox1_SelectedIndexChanged;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(400, 114);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(202, 27);
            textBox1.TabIndex = 1;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // EditPatientPage
            // 
            EditPatientPage.Controls.Add(SearchToModifySelectedTest);
            EditPatientPage.Controls.Add(ModifySelectedTests);
            EditPatientPage.Controls.Add(label3);
            EditPatientPage.Controls.Add(label2);
            EditPatientPage.Controls.Add(label1);
            EditPatientPage.Controls.Add(SaveEditedPatient);
            EditPatientPage.Controls.Add(EditPagePatientTests);
            EditPatientPage.Controls.Add(label7);
            EditPatientPage.Controls.Add(label6);
            EditPatientPage.Controls.Add(label5);
            EditPatientPage.Controls.Add(EditPagePatientDate);
            EditPatientPage.Controls.Add(EditPagePatientName);
            EditPatientPage.Name = "EditPatientPage";
            EditPatientPage.Size = new Size(976, 709);
            EditPatientPage.Text = "Edit Patient";
            // 
            // ModifySelectedTests
            // 
            ModifySelectedTests.FormattingEnabled = true;
            ModifySelectedTests.Location = new Point(618, 57);
            ModifySelectedTests.Name = "ModifySelectedTests";
            ModifySelectedTests.Size = new Size(225, 148);
            ModifySelectedTests.TabIndex = 1;
            ModifySelectedTests.Click += ModifySelectedTests_Click;
            ModifySelectedTests.ItemCheck += ModifySelectedTests_ItemCheck;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(535, 24);
            label3.Name = "label3";
            label3.Size = new Size(51, 19);
            label3.TabIndex = 1;
            label3.Text = "label3";
            // 
            // label2
            // 
            label2.Location = new Point(0, 0);
            label2.Name = "label2";
            label2.Size = new Size(100, 23);
            label2.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(11, 320);
            label1.Name = "label1";
            label1.Size = new Size(50, 19);
            label1.TabIndex = 1;
            label1.Text = "Name";
            // 
            // SaveEditedPatient
            // 
            SaveEditedPatient.Location = new Point(423, 601);
            SaveEditedPatient.Name = "SaveEditedPatient";
            SaveEditedPatient.Size = new Size(112, 34);
            SaveEditedPatient.TabIndex = 1;
            SaveEditedPatient.Text = "Save";
            SaveEditedPatient.UseVisualStyleBackColor = true;
            SaveEditedPatient.Click += SaveEditedPatient_Click;
            // 
            // EditPagePatientTests
            // 
            EditPagePatientTests.Location = new Point(11, 342);
            EditPagePatientTests.Name = "EditPagePatientTests";
            EditPagePatientTests.Size = new Size(954, 253);
            EditPagePatientTests.TabIndex = 1;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(608, 320);
            label7.Name = "label7";
            label7.Size = new Size(38, 19);
            label7.TabIndex = 1;
            label7.Text = "Unit";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(423, 320);
            label6.Name = "label6";
            label6.Size = new Size(78, 19);
            label6.TabIndex = 1;
            label6.Text = "Reference";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(237, 320);
            label5.Name = "label5";
            label5.Size = new Size(48, 19);
            label5.TabIndex = 1;
            label5.Text = "Value";
            // 
            // EditPagePatientDate
            // 
            EditPagePatientDate.Location = new Point(270, 86);
            EditPagePatientDate.Name = "EditPagePatientDate";
            EditPagePatientDate.Size = new Size(150, 27);
            EditPagePatientDate.TabIndex = 1;
            // 
            // EditPagePatientName
            // 
            EditPagePatientName.Location = new Point(270, 28);
            EditPagePatientName.Name = "EditPagePatientName";
            EditPagePatientName.Size = new Size(150, 27);
            EditPagePatientName.TabIndex = 1;
            // 
            // SearchToModifySelectedTest
            // 
            SearchToModifySelectedTest.Location = new Point(617, 17);
            SearchToModifySelectedTest.Name = "SearchToModifySelectedTest";
            SearchToModifySelectedTest.Size = new Size(225, 27);
            SearchToModifySelectedTest.TabIndex = 1;
            SearchToModifySelectedTest.TextChanged += SearchToModifySelectedTest_TextChanged;
            // 
            // ViewPatient
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(978, 744);
            Controls.Add(ViewPatientPage);
            Name = "ViewPatient";
            Text = "ViewPatient";
            ((System.ComponentModel.ISupportInitialize)ViewPatientPage).EndInit();
            ViewPatientPage.ResumeLayout(false);
            SearchPatientPage.ResumeLayout(false);
            SearchPatientPage.PerformLayout();
            EditPatientPage.ResumeLayout(false);
            EditPatientPage.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl ViewPatientPage;
        private DevExpress.XtraTab.XtraTabPage SearchPatientPage;
        private DevExpress.XtraTab.XtraTabPage EditPatientPage;
        private TextBox textBox1;
        private CheckedListBox checkedListBox1;
        private Button NextToPreview;
        private Label label7;
        private Label label6;
        private Label label5;
        private TextBox EditPagePatientDate;
        private TextBox EditPagePatientName;
        private Button SaveEditedPatient;
        private DevExpress.XtraEditors.XtraScrollableControl EditPagePatientTests;
        private Button DeleteSelectedPatient;
        private Label label1;
        private Label label2;
        private CheckedListBox ModifySelectedTests;
        private Label label3;
        private TextBox SearchToModifySelectedTest;
    }
}