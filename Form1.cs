using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Lab
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            btnAddPatient.Click += btnAddPatient_Click;
            ViewPatient.Click += ViewPatient_Click;
            AddTest.Click += AddTest_Click;
            ViewTest.Click += ViewTest_Click;
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

        private void btnAddPatient_Click(object sender, EventArgs e)
        {
            var addForm = new AddPatientForm();
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                // No need to refresh list since it's moved to ViewPatientsForm
            }
        }

        private void ViewPatient_Click(object sender, EventArgs e)
        {
            var addForm = new ViewPatient();
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                // No need to refresh list since it's moved to ViewPatientsForm
            }
        }

        private void AddTest_Click(object sender, EventArgs e)
        {
            var addForm = new AddTest();
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                // No need to refresh list since it's moved to ViewPatientsForm
            }

        }

        private void ViewTest_Click(object sender, EventArgs e)
        {
            var addForm = new ViewTest();
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                // No need to refresh list since it's moved to ViewPatientsForm
            }

        }


    }
}
