namespace Lab
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnAddPatient = new Button();
            button1 = new Button();
            AddTest = new Button();
            ViewTest = new Button();
            SuspendLayout();
            // 
            // btnAddPatient
            // 
            btnAddPatient.Location = new Point(376, 272);
            btnAddPatient.Margin = new Padding(4);
            btnAddPatient.Name = "btnAddPatient";
            btnAddPatient.Size = new Size(125, 38);
            btnAddPatient.TabIndex = 0;
            btnAddPatient.Text = "Add Patient";
            btnAddPatient.UseVisualStyleBackColor = true;
            btnAddPatient.Click += btnAddPatient_Click;
            // 
            // button1
            // 
            button1.Location = new Point(376, 317);
            button1.Name = "button1";
            button1.Size = new Size(125, 38);
            button1.TabIndex = 1;
            button1.Text = "View Patient";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // AddTest
            // 
            AddTest.Location = new Point(376, 361);
            AddTest.Name = "AddTest";
            AddTest.Size = new Size(125, 34);
            AddTest.TabIndex = 2;
            AddTest.Text = "Add Test";
            AddTest.UseVisualStyleBackColor = true;
            AddTest.Click += AddTest_Click;
            // 
            // ViewTest
            // 
            ViewTest.Location = new Point(376, 401);
            ViewTest.Name = "ViewTest";
            ViewTest.Size = new Size(125, 34);
            ViewTest.TabIndex = 3;
            ViewTest.Text = "View Test";
            ViewTest.UseVisualStyleBackColor = true;
            ViewTest.Click += ViewTest_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 700);
            Controls.Add(ViewTest);
            Controls.Add(AddTest);
            Controls.Add(button1);
            Controls.Add(btnAddPatient);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4);
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Lab Management";
            ResumeLayout(false);
        }

        #endregion

        private Button btnAddPatient;
        private Button button1;
        private Button AddTest;
        private Button ViewTest;
    }
}
