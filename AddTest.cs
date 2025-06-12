using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using DevExpress.CodeParser;
using Lab.Models;

namespace Lab
{
    public partial class AddTest : Form
    {
        public AddTest()
        {
            InitializeComponent();
            AddTestToMenu.Click += AddTestToMenu_Click;
            // Set up Enter key handling for each field
            TestName.KeyDown += (s, e) => HandleEnterKey(s, e, TestUnit);
            TestUnit.KeyDown += (s, e) => HandleEnterKey(s, e, TestRef);
            TestRef.KeyDown += (s, e) => HandleEnterKey(s, e, BtnSaveTest);
        }

        private void HandleEnterKey(object sender, KeyEventArgs e, Control nextControl)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                nextControl.Focus();
                
                // If next control is the save button, click it
                if (nextControl is Button button)
                {
                    button.PerformClick();
                }
            }
        }

        private void TestName_TextChanged(object sender, EventArgs e)
        {
            ValidateInputs();
        }

        private void TestRef_TextChanged(object sender, EventArgs e)
        {
            ValidateInputs();
        }

        private void TestUnit_TextChanged(object sender, EventArgs e)
        {
            ValidateInputs();
        }

        private void ValidateInputs()
        {
            bool isValid = !string.IsNullOrWhiteSpace(TestName.Text);
            BtnSaveTest.Enabled = isValid;
        }

        private void BtnSaveTest_Click(object sender, EventArgs e)
        {
            try
            {
                string testName = TestName.Text.Trim();
                string unit = TestUnit.Text.Trim();
                string referenceRange = TestRef.Text.Trim();

                // Validate test name
                if (string.IsNullOrWhiteSpace(testName))
                {
                    MessageBox.Show("Please enter a test name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Check if test name already exists
                var existingTests = DatabaseHelper.GetAllTests();
                if (existingTests.Any(t => t.Name.Equals(testName, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show("A test with this name already exists.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Get the next available ID
                int nextId = 1;
                if (existingTests.Any())
                {
                    nextId = existingTests.Max(t => t.Id) + 1;
                }

                // Get the path to the Test List database
                string testListDbPath = Path.GetFullPath(Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Test List",
                    "Tests.db"
                ));
                string testListDbConnStr = $"Data Source={testListDbPath}";

                // Insert the new test into the Test List database
                using (var conn = new SQLiteConnection(testListDbConnStr))
                {
                    conn.Open();
                    using (var cmd = new SQLiteCommand(@"
                        INSERT INTO TestList (Id, Name, Unit, Ref)
                        VALUES (@id, @name, @unit, @ref)", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", nextId);
                        cmd.Parameters.AddWithValue("@name", testName);
                        cmd.Parameters.AddWithValue("@unit", string.IsNullOrWhiteSpace(unit) ? DBNull.Value : (object)unit);
                        cmd.Parameters.AddWithValue("@ref", string.IsNullOrWhiteSpace(referenceRange) ? DBNull.Value : (object)referenceRange);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Test added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // Clear the form
                TestName.Text = "";
                TestUnit.Text = "";
                TestRef.Text = "";
                
                // Refresh the test list in other forms
                DatabaseHelper.RefreshTestList();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving test: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddTestToMenu_Click(object sender, EventArgs e)
        {
            // Clear all fields
            TestName.Text = "";
            TestUnit.Text = "";
            TestRef.Text = "";

            // Return to menu
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
