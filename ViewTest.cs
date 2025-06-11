using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab
{
    public partial class ViewTest : Form
    {
        private List<Test> allTests = new List<Test>();
        private Test selectedTest = null;

        public ViewTest()
        {
            InitializeComponent();
            
            // Initialize UI
            BtnPreviewTest.Text = "Preview";
            BtnPreviewTest.Enabled = false;
            TestsList.CheckOnClick = true;
            
            // Set up Enter key handling for each field
            EditTestName.KeyDown += (s, e) => HandleEnterKey(s, e, EditTestUnit);
            EditTestUnit.KeyDown += (s, e) => HandleEnterKey(s, e, EditTestRef);
            EditTestRef.KeyDown += (s, e) => HandleEnterKey(s, e, BtnSaveTest);
            
            // Load tests
            LoadAllTests();

            // Set up tab change handling
            xtraTabControl1.SelectedPageChanging += XtraTabControl_SelectedPageChanging;
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

        private void LoadAllTests()
        {
            allTests = DatabaseHelper.GetAllTests();
            UpdateTestList(SearchTest.Text);
        }

        private void UpdateTestList(string searchTerm)
        {
            TestsList.Items.Clear();
            var filtered = allTests
                .Where(t => t.Name.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
                .OrderBy(t => t.Name)
                .ToList();

            foreach (var test in filtered)
            {
                TestsList.Items.Add(test.Name);
            }

            // Uncheck all items
            for (int i = 0; i < TestsList.Items.Count; i++)
            {
                TestsList.SetItemChecked(i, false);
            }
        }

        private void SearchTest_TextChanged(object sender, EventArgs e)
        {
            UpdateTestList(SearchTest.Text);
        }

        private void TestsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Ensure only one item is checked
            for (int i = 0; i < TestsList.Items.Count; i++)
            {
                if (i != TestsList.SelectedIndex)
                {
                    TestsList.SetItemChecked(i, false);
                }
            }

            // Enable preview button if an item is selected
            BtnPreviewTest.Enabled = TestsList.CheckedItems.Count > 0;
        }

        private void XtraTabControl_SelectedPageChanging(object sender, DevExpress.XtraTab.TabPageChangingEventArgs e)
        {
            // If trying to switch to Preview tab
            if (e.Page == PreviewTest && e.PrevPage == SelectTest)
            {
                if (TestsList.CheckedItems.Count == 0)
                {
                    e.Cancel = true;
                    MessageBox.Show("Please select a test to preview first.", 
                                  "No Test Selected", 
                                  MessageBoxButtons.OK, 
                                  MessageBoxIcon.Warning);
                    return;
                }

                // If we have a selected test but haven't loaded it yet
                if (selectedTest == null || selectedTest.Name != TestsList.CheckedItems[0].ToString())
                {
                    string selectedTestName = TestsList.CheckedItems[0].ToString();
                    selectedTest = allTests.FirstOrDefault(t => t.Name == selectedTestName);

                    if (selectedTest != null)
                    {
                        // Fill the edit fields
                        EditTestName.Text = selectedTest.Name;
                        EditTestRef.Text = selectedTest.ReferenceRange;
                        EditTestUnit.Text = selectedTest.Unit;
                    }
                    else
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }

        private void BtnPreviewTest_Click(object sender, EventArgs e)
        {
            if (TestsList.CheckedItems.Count == 0)
            {
                MessageBox.Show("Please select a test to preview.", 
                              "No Test Selected", 
                              MessageBoxButtons.OK, 
                              MessageBoxIcon.Warning);
                return;
            }

            string selectedTestName = TestsList.CheckedItems[0].ToString();
            selectedTest = allTests.FirstOrDefault(t => t.Name == selectedTestName);

            if (selectedTest != null)
            {
                // Fill the edit fields
                EditTestName.Text = selectedTest.Name;
                EditTestRef.Text = selectedTest.ReferenceRange;
                EditTestUnit.Text = selectedTest.Unit;

                // Switch to preview tab
                xtraTabControl1.SelectedTabPage = PreviewTest;
            }
        }

        private void EditTestName_TextChanged(object sender, EventArgs e)
        {
            ValidateInputs();
        }

        private void EditTestRef_TextChanged(object sender, EventArgs e)
        {
            ValidateInputs();
        }

        private void EditTestUnit_TextChanged(object sender, EventArgs e)
        {
            ValidateInputs();
        }

        private void ValidateInputs()
        {
            bool isValid = !string.IsNullOrWhiteSpace(EditTestName.Text);
            BtnSaveTest.Enabled = isValid;
        }

        private void BtnDeleteTest_Click(object sender, EventArgs e)
        {
            if (selectedTest == null) return;

            var result = MessageBox.Show(
                $"Are you sure you want to delete the test '{selectedTest.Name}'?\n\nThis action cannot be undone.",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Get the path to the Test List database
                    string testListDbPath = Path.GetFullPath(Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory,
                        "Test List",
                        "Tests.db"
                    ));
                    string testListDbConnStr = $"Data Source={testListDbPath}";

                    using (var conn = new SQLiteConnection(testListDbConnStr))
                    {
                        conn.Open();
                        using (var cmd = new SQLiteCommand("DELETE FROM TestList WHERE Id = @id", conn))
                        {
                            cmd.Parameters.AddWithValue("@id", selectedTest.Id);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    
                    // Refresh the test list
                    DatabaseHelper.RefreshTestList();
                    LoadAllTests();
                    
                    // Return to select tab
                    xtraTabControl1.SelectedTabPage = SelectTest;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting test: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnSaveTest_Click(object sender, EventArgs e)
        {
            if (selectedTest == null) return;

            try
            {
                string newName = EditTestName.Text.Trim();
                string newUnit = EditTestUnit.Text.Trim();
                string newRef = EditTestRef.Text.Trim();

                // Validate test name
                if (string.IsNullOrWhiteSpace(newName))
                {
                    MessageBox.Show("Please enter a test name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Check if the new name already exists (excluding the current test)
                if (newName != selectedTest.Name && 
                    allTests.Any(t => t.Id != selectedTest.Id && 
                                    t.Name.Equals(newName, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show("A test with this name already exists.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Get the path to the Test List database
                string testListDbPath = Path.GetFullPath(Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Test List",
                    "Tests.db"
                ));
                string testListDbConnStr = $"Data Source={testListDbPath}";

                // Update the test
                using (var conn = new SQLiteConnection(testListDbConnStr))
                {
                    conn.Open();
                    using (var cmd = new SQLiteCommand(@"
                        UPDATE TestList 
                        SET Name = @name, Unit = @unit, Ref = @ref
                        WHERE Id = @id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", selectedTest.Id);
                        cmd.Parameters.AddWithValue("@name", newName);
                        cmd.Parameters.AddWithValue("@unit", string.IsNullOrWhiteSpace(newUnit) ? DBNull.Value : (object)newUnit);
                        cmd.Parameters.AddWithValue("@ref", string.IsNullOrWhiteSpace(newRef) ? DBNull.Value : (object)newRef);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Test updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // Refresh the test list
                DatabaseHelper.RefreshTestList();
                LoadAllTests();
                
                // Return to select tab
                xtraTabControl1.SelectedTabPage = SelectTest;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating test: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
