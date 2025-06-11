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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            pictureBox4 = new PictureBox();
            label2 = new Label();
            label1 = new Label();
            btnAddPatient = new DevExpress.XtraEditors.SimpleButton();
            ViewPatient = new DevExpress.XtraEditors.SimpleButton();
            AddTest = new DevExpress.XtraEditors.SimpleButton();
            ViewTest = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(82, 155);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(51, 51);
            pictureBox1.TabIndex = 9;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(82, 421);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(51, 51);
            pictureBox2.TabIndex = 10;
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(82, 234);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(51, 51);
            pictureBox3.TabIndex = 11;
            pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(82, 342);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(51, 51);
            pictureBox4.TabIndex = 12;
            pictureBox4.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Tahoma", 18F, FontStyle.Bold);
            label2.Location = new Point(82, 54);
            label2.Name = "label2";
            label2.Size = new Size(313, 43);
            label2.TabIndex = 14;
            label2.Text = "LabWrokStation";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(82, 297);
            label1.Name = "label1";
            label1.Size = new Size(285, 25);
            label1.TabIndex = 15;
            label1.Text = "_______________________________________";
            // 
            // btnAddPatient
            // 
            btnAddPatient.Appearance.Font = new Font("Tahoma", 13F);
            btnAddPatient.Appearance.Options.UseFont = true;
            btnAddPatient.Location = new Point(139, 155);
            btnAddPatient.Name = "btnAddPatient";
            btnAddPatient.Size = new Size(220, 51);
            btnAddPatient.TabIndex = 20;
            btnAddPatient.Text = "Add Patient";
            // 
            // ViewPatient
            // 
            ViewPatient.Appearance.Font = new Font("Tahoma", 13F);
            ViewPatient.Appearance.Options.UseFont = true;
            ViewPatient.Location = new Point(139, 234);
            ViewPatient.Name = "ViewPatient";
            ViewPatient.Size = new Size(220, 51);
            ViewPatient.TabIndex = 21;
            ViewPatient.Text = "Edit Patient";
            // 
            // AddTest
            // 
            AddTest.Appearance.Font = new Font("Tahoma", 13F);
            AddTest.Appearance.Options.UseFont = true;
            AddTest.Location = new Point(139, 342);
            AddTest.Name = "AddTest";
            AddTest.Size = new Size(220, 51);
            AddTest.TabIndex = 22;
            AddTest.Text = "Add Test";
            // 
            // ViewTest
            // 
            ViewTest.Appearance.Font = new Font("Tahoma", 13F);
            ViewTest.Appearance.Options.UseFont = true;
            ViewTest.Location = new Point(139, 421);
            ViewTest.Name = "ViewTest";
            ViewTest.Size = new Size(220, 51);
            ViewTest.TabIndex = 23;
            ViewTest.Text = "Edit Test";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(478, 544);
            Controls.Add(ViewTest);
            Controls.Add(AddTest);
            Controls.Add(ViewPatient);
            Controls.Add(btnAddPatient);
            Controls.Add(label1);
            Controls.Add(label2);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox4);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Lab Management";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton ViewTest1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.HyperlinkLabelControl hyperlinkLabelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton AddTest;
        private DevExpress.XtraEditors.SimpleButton ViewPatient;
        private DevExpress.XtraEditors.SimpleButton ViewTest;
        private DevExpress.XtraEditors.SimpleButton btnAddPatient;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private PictureBox pictureBox4;
        private Label label2;
        private Label label1;
    }
}
