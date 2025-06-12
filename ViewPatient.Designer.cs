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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewPatient));
            ViewPatientPage = new DevExpress.XtraTab.XtraTabControl();
            SearchPatientPage = new DevExpress.XtraTab.XtraTabPage();
            DeleteSelectedPatient = new DevExpress.XtraEditors.SimpleButton();
            SreachPatientToMenu = new DevExpress.XtraEditors.SimpleButton();
            label1 = new Label();
            NextToPreview = new Button();
            checkedListBox1 = new CheckedListBox();
            textBox1 = new TextBox();
            EditPatientPage = new DevExpress.XtraTab.XtraTabPage();
            EditPagePatientDate = new DevExpress.XtraEditors.DateEdit();
            EditPatientToMenu = new DevExpress.XtraEditors.SimpleButton();
            PrintPdf = new Button();
            label5 = new Label();
            label4 = new Label();
            SearchToModifySelectedTest = new TextBox();
            ModifySelectedTests = new CheckedListBox();
            label3 = new Label();
            label2 = new Label();
            SaveEditedPatient = new Button();
            EditPagePatientTests = new DevExpress.XtraEditors.XtraScrollableControl();
            EditPagePatientName = new TextBox();
            ((System.ComponentModel.ISupportInitialize)ViewPatientPage).BeginInit();
            ViewPatientPage.SuspendLayout();
            SearchPatientPage.SuspendLayout();
            EditPatientPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)EditPagePatientDate.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)EditPagePatientDate.Properties.CalendarTimeProperties).BeginInit();
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
            SearchPatientPage.Controls.Add(SreachPatientToMenu);
            SearchPatientPage.Controls.Add(label1);
            SearchPatientPage.Controls.Add(NextToPreview);
            SearchPatientPage.Controls.Add(checkedListBox1);
            SearchPatientPage.Controls.Add(textBox1);
            SearchPatientPage.Name = "SearchPatientPage";
            SearchPatientPage.Size = new Size(976, 709);
            SearchPatientPage.Text = "Search Patient";
            // 
            // DeleteSelectedPatient
            // 
            DeleteSelectedPatient.Appearance.BackColor = Color.FromArgb(255, 192, 192);
            DeleteSelectedPatient.Appearance.Font = new Font("Tahoma", 11F);
            DeleteSelectedPatient.Appearance.Options.UseBackColor = true;
            DeleteSelectedPatient.Appearance.Options.UseFont = true;
            DeleteSelectedPatient.Location = new Point(430, 436);
            DeleteSelectedPatient.Name = "DeleteSelectedPatient";
            DeleteSelectedPatient.Size = new Size(117, 43);
            DeleteSelectedPatient.TabIndex = 1;
            DeleteSelectedPatient.Text = "Delete";
            // 
            // SreachPatientToMenu
            // 
            SreachPatientToMenu.Appearance.Font = new Font("Tahoma", 11F);
            SreachPatientToMenu.Appearance.Options.UseFont = true;
            SreachPatientToMenu.Location = new Point(279, 436);
            SreachPatientToMenu.Name = "SreachPatientToMenu";
            SreachPatientToMenu.Size = new Size(128, 43);
            SreachPatientToMenu.TabIndex = 1;
            SreachPatientToMenu.Text = "Menu";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Tahoma", 11F);
            label1.Location = new Point(345, 106);
            label1.Name = "label1";
            label1.Size = new Size(78, 27);
            label1.TabIndex = 1;
            label1.Text = "Search";
            // 
            // NextToPreview
            // 
            NextToPreview.Font = new Font("Tahoma", 11F);
            NextToPreview.Location = new Point(569, 436);
            NextToPreview.Name = "NextToPreview";
            NextToPreview.Size = new Size(128, 43);
            NextToPreview.TabIndex = 1;
            NextToPreview.Text = "Preview";
            NextToPreview.UseVisualStyleBackColor = true;
            NextToPreview.Click += NextToPreview_Click;
            // 
            // checkedListBox1
            // 
            checkedListBox1.Font = new Font("Tahoma", 11F);
            checkedListBox1.FormattingEnabled = true;
            checkedListBox1.Location = new Point(279, 147);
            checkedListBox1.Name = "checkedListBox1";
            checkedListBox1.Size = new Size(418, 252);
            checkedListBox1.TabIndex = 1;
            checkedListBox1.SelectedIndexChanged += checkedListBox1_SelectedIndexChanged;
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Tahoma", 11F);
            textBox1.Location = new Point(446, 103);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(202, 34);
            textBox1.TabIndex = 1;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // EditPatientPage
            // 
            EditPatientPage.Controls.Add(EditPagePatientDate);
            EditPatientPage.Controls.Add(EditPatientToMenu);
            EditPatientPage.Controls.Add(PrintPdf);
            EditPatientPage.Controls.Add(label5);
            EditPatientPage.Controls.Add(label4);
            EditPatientPage.Controls.Add(SearchToModifySelectedTest);
            EditPatientPage.Controls.Add(ModifySelectedTests);
            EditPatientPage.Controls.Add(label3);
            EditPatientPage.Controls.Add(label2);
            EditPatientPage.Controls.Add(SaveEditedPatient);
            EditPatientPage.Controls.Add(EditPagePatientTests);
            EditPatientPage.Controls.Add(EditPagePatientName);
            EditPatientPage.Name = "EditPatientPage";
            EditPatientPage.Size = new Size(976, 709);
            EditPatientPage.Text = "Edit Patient";
            // 
            // EditPagePatientDate
            // 
            EditPagePatientDate.EditValue = null;
            EditPagePatientDate.Location = new Point(310, 160);
            EditPagePatientDate.Name = "EditPagePatientDate";
            EditPagePatientDate.Properties.Appearance.Font = new Font("Tahoma", 11F);
            EditPagePatientDate.Properties.Appearance.Options.UseFont = true;
            EditPagePatientDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            EditPagePatientDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            EditPagePatientDate.Size = new Size(234, 34);
            EditPagePatientDate.TabIndex = 1;
            // 
            // EditPatientToMenu
            // 
            EditPatientToMenu.Appearance.Font = new Font("Tahoma", 11F);
            EditPatientToMenu.Appearance.Options.UseFont = true;
            EditPatientToMenu.Location = new Point(203, 601);
            EditPatientToMenu.Name = "EditPatientToMenu";
            EditPatientToMenu.Size = new Size(128, 43);
            EditPatientToMenu.TabIndex = 1;
            EditPatientToMenu.Text = "Back";
            // 
            // PrintPdf
            // 
            PrintPdf.Font = new Font("Tahoma", 11F);
            PrintPdf.Location = new Point(644, 599);
            PrintPdf.Name = "PrintPdf";
            PrintPdf.Size = new Size(128, 43);
            PrintPdf.TabIndex = 1;
            PrintPdf.Text = "Print ";
            PrintPdf.UseVisualStyleBackColor = true;
            PrintPdf.Click += PrintPdf_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Tahoma", 11F);
            label5.Location = new Point(214, 163);
            label5.Name = "label5";
            label5.Size = new Size(80, 27);
            label5.TabIndex = 1;
            label5.Text = "Date : ";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Tahoma", 11F);
            label4.Location = new Point(203, 84);
            label4.Name = "label4";
            label4.Size = new Size(91, 27);
            label4.TabIndex = 1;
            label4.Text = "Name : ";
            // 
            // SearchToModifySelectedTest
            // 
            SearchToModifySelectedTest.Font = new Font("Tahoma", 11F);
            SearchToModifySelectedTest.Location = new Point(689, 24);
            SearchToModifySelectedTest.Name = "SearchToModifySelectedTest";
            SearchToModifySelectedTest.Size = new Size(225, 34);
            SearchToModifySelectedTest.TabIndex = 1;
            SearchToModifySelectedTest.TextChanged += SearchToModifySelectedTest_TextChanged;
            // 
            // ModifySelectedTests
            // 
            ModifySelectedTests.Font = new Font("Tahoma", 11F);
            ModifySelectedTests.FormattingEnabled = true;
            ModifySelectedTests.Location = new Point(605, 68);
            ModifySelectedTests.Name = "ModifySelectedTests";
            ModifySelectedTests.Size = new Size(309, 190);
            ModifySelectedTests.TabIndex = 1;
            ModifySelectedTests.ItemCheck += ModifySelectedTests_ItemCheck;
            ModifySelectedTests.Click += ModifySelectedTests_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Tahoma", 11F);
            label3.Location = new Point(605, 28);
            label3.Name = "label3";
            label3.Size = new Size(78, 27);
            label3.TabIndex = 1;
            label3.Text = "Search";
            // 
            // label2
            // 
            label2.Location = new Point(0, 0);
            label2.Name = "label2";
            label2.Size = new Size(100, 23);
            label2.TabIndex = 0;
            // 
            // SaveEditedPatient
            // 
            SaveEditedPatient.Font = new Font("Tahoma", 11F);
            SaveEditedPatient.Location = new Point(416, 599);
            SaveEditedPatient.Name = "SaveEditedPatient";
            SaveEditedPatient.Size = new Size(128, 43);
            SaveEditedPatient.TabIndex = 1;
            SaveEditedPatient.Text = "Save";
            SaveEditedPatient.UseVisualStyleBackColor = true;
            SaveEditedPatient.Click += SaveEditedPatient_Click;
            // 
            // EditPagePatientTests
            // 
            EditPagePatientTests.Location = new Point(11, 268);
            EditPagePatientTests.Name = "EditPagePatientTests";
            EditPagePatientTests.Size = new Size(954, 327);
            EditPagePatientTests.TabIndex = 1;
            // 
            // EditPagePatientName
            // 
            EditPagePatientName.Font = new Font("Tahoma", 11F);
            EditPagePatientName.Location = new Point(310, 81);
            EditPagePatientName.Name = "EditPagePatientName";
            EditPagePatientName.Size = new Size(234, 34);
            EditPagePatientName.TabIndex = 1;
            // 
            // ViewPatient
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(978, 744);
            Controls.Add(ViewPatientPage);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ViewPatient";
            Text = "ViewPatient";
            ((System.ComponentModel.ISupportInitialize)ViewPatientPage).EndInit();
            ViewPatientPage.ResumeLayout(false);
            SearchPatientPage.ResumeLayout(false);
            SearchPatientPage.PerformLayout();
            EditPatientPage.ResumeLayout(false);
            EditPatientPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)EditPagePatientDate.Properties.CalendarTimeProperties).EndInit();
            ((System.ComponentModel.ISupportInitialize)EditPagePatientDate.Properties).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl ViewPatientPage;
        private DevExpress.XtraTab.XtraTabPage SearchPatientPage;
        private DevExpress.XtraTab.XtraTabPage EditPatientPage;
        private TextBox textBox1;
        private CheckedListBox checkedListBox1;
        private Button NextToPreview;
        private TextBox EditPagePatientName;
        private Button SaveEditedPatient;
        private DevExpress.XtraEditors.XtraScrollableControl EditPagePatientTests;
        private Label label2;
        private CheckedListBox ModifySelectedTests;
        private Label label3;
        private TextBox SearchToModifySelectedTest;
        private Label label1;
        private Label label5;
        private Label label4;
        private Button PrintPdf;
        private DevExpress.XtraEditors.SimpleButton SreachPatientToMenu;
        private DevExpress.XtraEditors.SimpleButton DeleteSelectedPatient;
        private DevExpress.XtraEditors.SimpleButton EditPatientToMenu;
        private DevExpress.XtraEditors.DateEdit EditPagePatientDate;
    }
}