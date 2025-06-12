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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddTest));
            BtnSaveTest = new Button();
            TestUnit = new TextBox();
            TestRef = new TextBox();
            TestName = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            AddTestToMenu = new DevExpress.XtraEditors.SimpleButton();
            SuspendLayout();
            // 
            // BtnSaveTest
            // 
            BtnSaveTest.Font = new Font("Tahoma", 11F);
            BtnSaveTest.Location = new Point(451, 300);
            BtnSaveTest.Name = "BtnSaveTest";
            BtnSaveTest.Size = new Size(128, 43);
            BtnSaveTest.TabIndex = 0;
            BtnSaveTest.Text = "Save";
            BtnSaveTest.UseVisualStyleBackColor = true;
            BtnSaveTest.Click += BtnSaveTest_Click;
            // 
            // TestUnit
            // 
            TestUnit.Font = new Font("Tahoma", 11F);
            TestUnit.Location = new Point(400, 220);
            TestUnit.Name = "TestUnit";
            TestUnit.Size = new Size(214, 34);
            TestUnit.TabIndex = 1;
            TestUnit.TextChanged += TestUnit_TextChanged;
            // 
            // TestRef
            // 
            TestRef.Font = new Font("Tahoma", 11F);
            TestRef.Location = new Point(400, 159);
            TestRef.Name = "TestRef";
            TestRef.Size = new Size(214, 34);
            TestRef.TabIndex = 2;
            TestRef.TextChanged += TestRef_TextChanged;
            // 
            // TestName
            // 
            TestName.Font = new Font("Tahoma", 11F);
            TestName.Location = new Point(400, 87);
            TestName.Name = "TestName";
            TestName.Size = new Size(214, 34);
            TestName.TabIndex = 3;
            TestName.TextChanged += TestName_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Tahoma", 11F);
            label1.Location = new Point(226, 94);
            label1.Name = "label1";
            label1.Size = new Size(140, 27);
            label1.TabIndex = 4;
            label1.Text = "Test Name : ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Tahoma", 11F);
            label2.Location = new Point(184, 159);
            label2.Name = "label2";
            label2.Size = new Size(182, 27);
            label2.TabIndex = 5;
            label2.Text = "Test Reference : ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Tahoma", 11F);
            label3.Location = new Point(245, 223);
            label3.Name = "label3";
            label3.Size = new Size(121, 27);
            label3.TabIndex = 6;
            label3.Text = "Test Unit : ";
            // 
            // AddTestToMenu
            // 
            AddTestToMenu.Appearance.Font = new Font("Tahoma", 11F);
            AddTestToMenu.Appearance.Options.UseFont = true;
            AddTestToMenu.Location = new Point(259, 300);
            AddTestToMenu.Name = "AddTestToMenu";
            AddTestToMenu.Size = new Size(128, 43);
            AddTestToMenu.TabIndex = 7;
            AddTestToMenu.Text = "Menu";
            // 
            // AddTest
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(AddTestToMenu);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(TestName);
            Controls.Add(TestRef);
            Controls.Add(TestUnit);
            Controls.Add(BtnSaveTest);
            Icon = (Icon)resources.GetObject("$this.Icon");
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
        private DevExpress.XtraEditors.SimpleButton AddTestToMenu;
    }
}