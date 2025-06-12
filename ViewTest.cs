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
using Lab.Models;

namespace Lab
{
    public partial class ViewTest : Form
    {
        private readonly List<Test> allTests = new List<Test>();
        private Test? selectedTest;
        private bool isButtonNavigation;

        public ViewTest()
        {
            InitializeComponent();
            ViewTestToMenu.Click += ViewTestToMenu_Click;
            BackToSelectTest.Click += BackToSelectTest_Click;
            // Initialize UI
            BtnPreviewTest.Text = "Preview";
            BtnPreviewTest.Enabled = false;
            BtnSaveTest.Enabled = true; // Always keep save button enabled
            TestsList.CheckOnClick = true;
            
            // Disable Preview Test tab initially
            PreviewTest.PageEnabled = false;
            
            // Set up Enter key handling for each field
            EditTestName.KeyDown += (s, e) => HandleEnterKey(s, e, EditTestUnit);
            EditTestUnit.KeyDown += (s, e) => HandleEnterKey(s, e, EditTestRef);
            EditTestRef.KeyDown += (s, e) => HandleEnterKey(s, e, BtnSaveTest);
            
            // Load tests
            LoadAllTests();

            // Set up tab control behavior
            xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.True;
            xtraTabControl1.HeaderOrientation = DevExpress.XtraTab.TabOrientation.Horizontal;
            xtraTabControl1.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Top;
            xtraTabControl1.MouseDown += XtraTabControl1_MouseDown;
            xtraTabControl1.SelectedPageChanging += XtraTabControl1_SelectedPageChanging;
        }

        private void HandleEnterKey(object? sender, KeyEventArgs? e, Control? nextControl)
        {
            if (e?.KeyCode == Keys.Enter && nextControl != null)
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
            var tests = DatabaseHelper.GetAllTests();
            if (tests != null)
            {
                allTests.Clear();
                allTests.AddRange(tests);
            }
            UpdateTestList(SearchTest?.Text ?? string.Empty);
        }

        private void UpdateTestList(string searchTerm)
        {
            if (TestsList == null) return;

            TestsList.Items.Clear();
            var filtered = allTests
                .Where(t => t?.Name != null && t.Name.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
                .OrderBy(t => t.Name)
                .ToList();

            foreach (var test in filtered)
            {
                if (!string.IsNullOrEmpty(test.Name))
                {
                    TestsList.Items.Add(test.Name);
                }
            }

            // Uncheck all items
            for (int i = 0; i < TestsList.Items.Count; i++)
            {
                TestsList.SetItemChecked(i, false);
            }
        }

        private void SearchTest_TextChanged(object? sender, EventArgs? e)
        {
            UpdateTestList(SearchTest?.Text ?? string.Empty);
        }

        private void TestsList_SelectedIndexChanged(object? sender, EventArgs? e)
        {
            if (TestsList == null) return;

            // Ensure only one item is checked
            for (int i = 0; i < TestsList.Items.Count; i++)
            {
                if (i != TestsList.SelectedIndex)
                {
                    TestsList.SetItemChecked(i, false);
                }
            }

            // Enable/disable preview button and tab based on selection
            bool hasSelection = TestsList.CheckedItems.Count > 0;
            if (BtnPreviewTest != null)
            {
                BtnPreviewTest.Enabled = hasSelection;
            }
            if (PreviewTest != null)
            {
                PreviewTest.PageEnabled = hasSelection;
            }
        }

        private void XtraTabControl1_SelectedPageChanging(object? sender, DevExpress.XtraTab.TabPageChangingEventArgs? e)
        {
            if (!isButtonNavigation && e != null)
            {
                e.Cancel = true;
                if (e.Page == PreviewTest)
                {
                    if (TestsList?.CheckedItems.Count == 0)
                    {
                        MessageBox.Show("Please select a test and use the Preview button to continue.",
                            "No Test Selected",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Please use the Preview button to continue.",
                            "Use Preview Button",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            }
            isButtonNavigation = false;
        }

        private void XtraTabControl1_MouseDown(object? sender, MouseEventArgs? e)
        {
            // Empty handler - navigation and messages are handled in SelectedPageChanging
        }

        private void BtnPreviewTest_Click(object sender, EventArgs e)
        {
            if (TestsList?.CheckedItems.Count == 0)
            {
                MessageBox.Show("Please select a test to preview.", 
                              "No Test Selected", 
                              MessageBoxButtons.OK, 
                              MessageBoxIcon.Warning);
                return;
            }

            string? selectedTestName = TestsList?.CheckedItems[0]?.ToString();
            if (string.IsNullOrEmpty(selectedTestName)) return;

            selectedTest = allTests.FirstOrDefault(t => t.Name == selectedTestName);

            if (selectedTest != null)
            {
                // Fill the edit fields
                if (EditTestName != null) EditTestName.Text = selectedTest.Name;
                if (EditTestRef != null) EditTestRef.Text = selectedTest.ReferenceRange;
                if (EditTestUnit != null) EditTestUnit.Text = selectedTest.Unit;

                // Set navigation flag before switching tabs
                isButtonNavigation = true;
                // Programmatically switch to preview tab
                if (xtraTabControl1 != null && PreviewTest != null)
                {
                    xtraTabControl1.SelectedTabPage = PreviewTest;
                }
            }
        }

        private void EditTestName_TextChanged(object sender, EventArgs e)
        {
            // Remove validation on text change
        }

        private void EditTestRef_TextChanged(object sender, EventArgs e)
        {
            // Remove validation on text change
        }

        private void EditTestUnit_TextChanged(object sender, EventArgs e)
        {
            // Remove validation on text change
        }

        private void ValidateInputs()
        {
            // Remove this method as we'll validate only when saving
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
                    
                    // Clear search and selection
                    SearchTest.Text = "";
                    TestsList.Items.Clear();
                    
                    // Clear preview fields
                    EditTestName.Text = "";
                    EditTestRef.Text = "";
                    EditTestUnit.Text = "";
                    
                    // Reset selected test
                    selectedTest = null;
                    
                    // Reset to first tab
                    xtraTabControl1.SelectedTabPage = SelectTest;

                    // Show success message
                    MessageBox.Show("Test deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Return to menu
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
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
                string newName = EditTestName?.Text?.Trim() ?? string.Empty;
                string newUnit = EditTestUnit?.Text?.Trim() ?? string.Empty;
                string newRef = EditTestRef?.Text?.Trim() ?? string.Empty;

                // Validate test name
                if (string.IsNullOrWhiteSpace(newName))
                {
                    MessageBox.Show("Please enter a test name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    EditTestName?.Focus();
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
                if (xtraTabControl1 != null && SelectTest != null)
                {
                    xtraTabControl1.SelectedTabPage = SelectTest;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating test: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ViewTestToMenu_Click(object sender, EventArgs e)
        {
            // Clear search and selection
            if (SearchTest != null) SearchTest.Text = "";
            if (TestsList != null) TestsList.Items.Clear();
            
            // Clear preview fields
            if (EditTestName != null) EditTestName.Text = "";
            if (EditTestRef != null) EditTestRef.Text = "";
            if (EditTestUnit != null) EditTestUnit.Text = "";
            
            // Reset selected test
            selectedTest = null;
            
            // Reset to first tab
            if (xtraTabControl1 != null && SelectTest != null)
            {
                xtraTabControl1.SelectedTabPage = SelectTest;
            }

            // Return to menu
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void BackToSelectTest_Click(object sender, EventArgs e)
        {
            // Clear the edit fields
            if (EditTestName != null) EditTestName.Text = selectedTest?.Name ?? "";
            if (EditTestRef != null) EditTestRef.Text = selectedTest?.ReferenceRange ?? "";
            if (EditTestUnit != null) EditTestUnit.Text = selectedTest?.Unit ?? "";

            // Set navigation flag before switching tabs
            isButtonNavigation = true;
            // Switch back to select tab
            if (xtraTabControl1 != null && SelectTest != null)
            {
                xtraTabControl1.SelectedTabPage = SelectTest;
            }
        }
    }
}
