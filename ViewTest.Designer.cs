namespace Lab
{
    partial class ViewTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewTest));
            xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            SelectTest = new DevExpress.XtraTab.XtraTabPage();
            ViewTestToMenu = new DevExpress.XtraEditors.SimpleButton();
            label4 = new Label();
            BtnPreviewTest = new Button();
            TestsList = new CheckedListBox();
            SearchTest = new TextBox();
            PreviewTest = new DevExpress.XtraTab.XtraTabPage();
            BackToSelectTest = new DevExpress.XtraEditors.SimpleButton();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            BtnSaveTest = new Button();
            BtnDeleteTest = new Button();
            EditTestUnit = new TextBox();
            EditTestRef = new TextBox();
            EditTestName = new TextBox();
            ((System.ComponentModel.ISupportInitialize)xtraTabControl1).BeginInit();
            xtraTabControl1.SuspendLayout();
            SelectTest.SuspendLayout();
            PreviewTest.SuspendLayout();
            SuspendLayout();
            // 
            // xtraTabControl1
            // 
            xtraTabControl1.Dock = DockStyle.Fill;
            xtraTabControl1.Location = new Point(0, 0);
            xtraTabControl1.Name = "xtraTabControl1";
            xtraTabControl1.SelectedTabPage = SelectTest;
            xtraTabControl1.Size = new Size(800, 450);
            xtraTabControl1.TabIndex = 3;
            xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] { SelectTest, PreviewTest });
            // 
            // SelectTest
            // 
            SelectTest.Controls.Add(ViewTestToMenu);
            SelectTest.Controls.Add(label4);
            SelectTest.Controls.Add(BtnPreviewTest);
            SelectTest.Controls.Add(TestsList);
            SelectTest.Controls.Add(SearchTest);
            SelectTest.Name = "SelectTest";
            SelectTest.Size = new Size(798, 415);
            SelectTest.Text = "Select Test";
            // 
            // ViewTestToMenu
            // 
            ViewTestToMenu.Appearance.Font = new Font("Tahoma", 11F);
            ViewTestToMenu.Appearance.Options.UseFont = true;
            ViewTestToMenu.Location = new Point(275, 346);
            ViewTestToMenu.Name = "ViewTestToMenu";
            ViewTestToMenu.Size = new Size(128, 43);
            ViewTestToMenu.TabIndex = 4;
            ViewTestToMenu.Text = "Menu";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Tahoma", 11F);
            label4.Location = new Point(275, 66);
            label4.Name = "label4";
            label4.Size = new Size(78, 27);
            label4.TabIndex = 4;
            label4.Text = "Search";
            // 
            // BtnPreviewTest
            // 
            BtnPreviewTest.Font = new Font("Tahoma", 11F);
            BtnPreviewTest.Location = new Point(417, 346);
            BtnPreviewTest.Name = "BtnPreviewTest";
            BtnPreviewTest.Size = new Size(128, 43);
            BtnPreviewTest.TabIndex = 4;
            BtnPreviewTest.Text = "Preview";
            BtnPreviewTest.UseVisualStyleBackColor = true;
            BtnPreviewTest.Click += BtnPreviewTest_Click;
            // 
            // TestsList
            // 
            TestsList.Font = new Font("Tahoma", 11F);
            TestsList.FormattingEnabled = true;
            TestsList.Location = new Point(279, 103);
            TestsList.Name = "TestsList";
            TestsList.Size = new Size(266, 221);
            TestsList.TabIndex = 4;
            TestsList.SelectedIndexChanged += TestsList_SelectedIndexChanged;
            // 
            // SearchTest
            // 
            SearchTest.Font = new Font("Tahoma", 11F);
            SearchTest.Location = new Point(359, 63);
            SearchTest.Name = "SearchTest";
            SearchTest.Size = new Size(186, 34);
            SearchTest.TabIndex = 4;
            SearchTest.TextChanged += SearchTest_TextChanged;
            // 
            // PreviewTest
            // 
            PreviewTest.Controls.Add(BackToSelectTest);
            PreviewTest.Controls.Add(label3);
            PreviewTest.Controls.Add(label2);
            PreviewTest.Controls.Add(label1);
            PreviewTest.Controls.Add(BtnSaveTest);
            PreviewTest.Controls.Add(BtnDeleteTest);
            PreviewTest.Controls.Add(EditTestUnit);
            PreviewTest.Controls.Add(EditTestRef);
            PreviewTest.Controls.Add(EditTestName);
            PreviewTest.Name = "PreviewTest";
            PreviewTest.Size = new Size(798, 415);
            PreviewTest.Text = "Preview Test";
            // 
            // BackToSelectTest
            // 
            BackToSelectTest.Appearance.Font = new Font("Tahoma", 11F);
            BackToSelectTest.Appearance.Options.UseFont = true;
            BackToSelectTest.Location = new Point(185, 336);
            BackToSelectTest.Name = "BackToSelectTest";
            BackToSelectTest.Size = new Size(128, 43);
            BackToSelectTest.TabIndex = 4;
            BackToSelectTest.Text = "Back";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Tahoma", 11F);
            label3.Location = new Point(246, 211);
            label3.Name = "label3";
            label3.Size = new Size(121, 27);
            label3.TabIndex = 4;
            label3.Text = "Test Unit : ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Tahoma", 11F);
            label2.Location = new Point(185, 151);
            label2.Name = "label2";
            label2.Size = new Size(182, 27);
            label2.TabIndex = 4;
            label2.Text = "Test Reference : ";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Tahoma", 11F);
            label1.Location = new Point(227, 90);
            label1.Name = "label1";
            label1.Size = new Size(140, 27);
            label1.TabIndex = 4;
            label1.Text = "Test Name : ";
            // 
            // BtnSaveTest
            // 
            BtnSaveTest.Font = new Font("Tahoma", 11F);
            BtnSaveTest.Location = new Point(506, 336);
            BtnSaveTest.Name = "BtnSaveTest";
            BtnSaveTest.Size = new Size(128, 43);
            BtnSaveTest.TabIndex = 4;
            BtnSaveTest.Text = "Save";
            BtnSaveTest.UseVisualStyleBackColor = true;
            BtnSaveTest.Click += BtnSaveTest_Click;
            // 
            // BtnDeleteTest
            // 
            BtnDeleteTest.Font = new Font("Tahoma", 11F);
            BtnDeleteTest.Location = new Point(346, 336);
            BtnDeleteTest.Name = "BtnDeleteTest";
            BtnDeleteTest.Size = new Size(128, 43);
            BtnDeleteTest.TabIndex = 4;
            BtnDeleteTest.Text = "Delete Test";
            BtnDeleteTest.UseVisualStyleBackColor = true;
            BtnDeleteTest.Click += BtnDeleteTest_Click;
            // 
            // EditTestUnit
            // 
            EditTestUnit.Font = new Font("Tahoma", 11F);
            EditTestUnit.Location = new Point(395, 204);
            EditTestUnit.Name = "EditTestUnit";
            EditTestUnit.Size = new Size(177, 34);
            EditTestUnit.TabIndex = 4;
            EditTestUnit.TextChanged += EditTestUnit_TextChanged;
            // 
            // EditTestRef
            // 
            EditTestRef.Font = new Font("Tahoma", 11F);
            EditTestRef.Location = new Point(395, 144);
            EditTestRef.Name = "EditTestRef";
            EditTestRef.Size = new Size(177, 34);
            EditTestRef.TabIndex = 4;
            EditTestRef.TextChanged += EditTestRef_TextChanged;
            // 
            // EditTestName
            // 
            EditTestName.Font = new Font("Tahoma", 11F);
            EditTestName.Location = new Point(395, 87);
            EditTestName.Name = "EditTestName";
            EditTestName.Size = new Size(177, 34);
            EditTestName.TabIndex = 4;
            EditTestName.TextChanged += EditTestName_TextChanged;
            // 
            // ViewTest
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(xtraTabControl1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ViewTest";
            Text = "ViewTest";
            ((System.ComponentModel.ISupportInitialize)xtraTabControl1).EndInit();
            xtraTabControl1.ResumeLayout(false);
            SelectTest.ResumeLayout(false);
            SelectTest.PerformLayout();
            PreviewTest.ResumeLayout(false);
            PreviewTest.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage SelectTest;
        private Button BtnPreviewTest;
        private CheckedListBox TestsList;
        private TextBox SearchTest;
        private DevExpress.XtraTab.XtraTabPage PreviewTest;
        private Label label3;
        private Label label2;
        private Label label1;
        private Button BtnSaveTest;
        private Button BtnDeleteTest;
        private TextBox EditTestUnit;
        private TextBox EditTestRef;
        private TextBox EditTestName;
        private Label label4;
        private DevExpress.XtraEditors.SimpleButton ViewTestToMenu;
        private DevExpress.XtraEditors.SimpleButton BackToSelectTest;
    }
}