using System;
using System.Windows.Forms;

namespace Lab
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //InitializeButtons();
            try
            {
                DatabaseHelper.InitializeDatabase();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing database: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void InitializeButtons()
        //{
        //    // Update existing Add Patient button
        //    btnAddPatient.Location = new Point(376, 272);
        //    btnAddPatient.Size = new Size(125, 38);

        //    // Set form size
        //    this.ClientSize = new Size(900, 700);
        //}

        private void btnAddPatient_Click(object sender, EventArgs e)
        {
            var addForm = new AddPatientForm();
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                // No need to refresh list since it's moved to ViewPatientsForm
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var addForm = new ViewPatient();
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                // No need to refresh list since it's moved to ViewPatientsForm
            }
        }
    }
}
