namespace Lab
{
    partial class AddTest
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
            BtnSaveTest = new Button();
            TestUnit = new TextBox();
            TestRef = new TextBox();
            TestName = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // BtnSaveTest
            // 
            BtnSaveTest.Location = new Point(354, 285);
            BtnSaveTest.Name = "BtnSaveTest";
            BtnSaveTest.Size = new Size(112, 34);
            BtnSaveTest.TabIndex = 0;
            BtnSaveTest.Text = "Save";
            BtnSaveTest.UseVisualStyleBackColor = true;
            BtnSaveTest.Click += BtnSaveTest_Click;
            // 
            // TestUnit
            // 
            TestUnit.Location = new Point(336, 224);
            TestUnit.Name = "TestUnit";
            TestUnit.Size = new Size(150, 31);
            TestUnit.TabIndex = 1;
            TestUnit.TextChanged += TestUnit_TextChanged;
            // 
            // TestRef
            // 
            TestRef.Location = new Point(336, 163);
            TestRef.Name = "TestRef";
            TestRef.Size = new Size(150, 31);
            TestRef.TabIndex = 2;
            TestRef.TextChanged += TestRef_TextChanged;
            // 
            // TestName
            // 
            TestName.Location = new Point(336, 98);
            TestName.Name = "TestName";
            TestName.Size = new Size(150, 31);
            TestName.TabIndex = 3;
            TestName.TextChanged += TestName_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(215, 98);
            label1.Name = "label1";
            label1.Size = new Size(94, 25);
            label1.TabIndex = 4;
            label1.Text = "Test Name";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(186, 163);
            label2.Name = "label2";
            label2.Size = new Size(123, 25);
            label2.TabIndex = 5;
            label2.Text = "Test Reference";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(230, 227);
            label3.Name = "label3";
            label3.Size = new Size(79, 25);
            label3.TabIndex = 6;
            label3.Text = "Test Unit";
            // 
            // AddTest
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(TestName);
            Controls.Add(TestRef);
            Controls.Add(TestUnit);
            Controls.Add(BtnSaveTest);
            Name = "AddTest";
            Text = "AddTest";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BtnSaveTest;
        private TextBox TestUnit;
        private TextBox TestRef;
        private TextBox TestName;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}