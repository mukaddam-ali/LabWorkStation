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
            xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            SelectTest = new DevExpress.XtraTab.XtraTabPage();
            PreviewTest = new DevExpress.XtraTab.XtraTabPage();
            SearchTest = new TextBox();
            TestsList = new CheckedListBox();
            BtnPreviewTest = new Button();
            EditTestName = new TextBox();
            EditTestRef = new TextBox();
            EditTestUnit = new TextBox();
            BtnDeleteTest = new Button();
            BtnSaveTest = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
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
            SelectTest.Controls.Add(BtnPreviewTest);
            SelectTest.Controls.Add(TestsList);
            SelectTest.Controls.Add(SearchTest);
            SelectTest.Name = "SelectTest";
            SelectTest.Size = new Size(798, 415);
            SelectTest.Text = "Select Test";
            // 
            // PreviewTest
            // 
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
            // SearchTest
            // 
            SearchTest.Location = new Point(325, 61);
            SearchTest.Name = "SearchTest";
            SearchTest.Size = new Size(150, 27);
            SearchTest.TabIndex = 4;
            SearchTest.TextChanged += SearchTest_TextChanged;
            // 
            // TestsList
            // 
            TestsList.FormattingEnabled = true;
            TestsList.Location = new Point(310, 94);
            TestsList.Name = "TestsList";
            TestsList.Size = new Size(180, 124);
            TestsList.TabIndex = 4;
            TestsList.SelectedIndexChanged += TestsList_SelectedIndexChanged;
            // 
            // BtnPreviewTest
            // 
            BtnPreviewTest.Location = new Point(340, 224);
            BtnPreviewTest.Name = "BtnPreviewTest";
            BtnPreviewTest.Size = new Size(112, 34);
            BtnPreviewTest.TabIndex = 4;
            BtnPreviewTest.Text = "button1";
            BtnPreviewTest.UseVisualStyleBackColor = true;
            BtnPreviewTest.Click += BtnPreviewTest_Click;
            // 
            // EditTestName
            // 
            EditTestName.Location = new Point(325, 82);
            EditTestName.Name = "EditTestName";
            EditTestName.Size = new Size(150, 27);
            EditTestName.TabIndex = 4;
            EditTestName.TextChanged += EditTestName_TextChanged;
            // 
            // EditTestRef
            // 
            EditTestRef.Location = new Point(325, 148);
            EditTestRef.Name = "EditTestRef";
            EditTestRef.Size = new Size(150, 27);
            EditTestRef.TabIndex = 4;
            EditTestRef.TextChanged += EditTestRef_TextChanged;
            // 
            // EditTestUnit
            // 
            EditTestUnit.Location = new Point(325, 212);
            EditTestUnit.Name = "EditTestUnit";
            EditTestUnit.Size = new Size(150, 27);
            EditTestUnit.TabIndex = 4;
            EditTestUnit.TextChanged += EditTestUnit_TextChanged;
            // 
            // BtnDeleteTest
            // 
            BtnDeleteTest.Location = new Point(276, 269);
            BtnDeleteTest.Name = "BtnDeleteTest";
            BtnDeleteTest.Size = new Size(112, 34);
            BtnDeleteTest.TabIndex = 4;
            BtnDeleteTest.Text = "Delete Test";
            BtnDeleteTest.UseVisualStyleBackColor = true;
            BtnDeleteTest.Click += BtnDeleteTest_Click;
            // 
            // BtnSaveTest
            // 
            BtnSaveTest.Location = new Point(417, 268);
            BtnSaveTest.Name = "BtnSaveTest";
            BtnSaveTest.Size = new Size(112, 34);
            BtnSaveTest.TabIndex = 4;
            BtnSaveTest.Text = "Save";
            BtnSaveTest.UseVisualStyleBackColor = true;
            BtnSaveTest.Click += BtnSaveTest_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(213, 90);
            label1.Name = "label1";
            label1.Size = new Size(85, 19);
            label1.TabIndex = 4;
            label1.Text = "Test Name";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(185, 151);
            label2.Name = "label2";
            label2.Size = new Size(113, 19);
            label2.TabIndex = 4;
            label2.Text = "Test Reference";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(225, 211);
            label3.Name = "label3";
            label3.Size = new Size(73, 19);
            label3.TabIndex = 4;
            label3.Text = "Test Unit";
            // 
            // ViewTest
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(xtraTabControl1);
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
    }
}