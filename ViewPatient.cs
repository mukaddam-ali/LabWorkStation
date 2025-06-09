using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SQLite;
using DevExpress.XtraTab;

namespace Lab
{
    public partial class ViewPatient : Form
    {
        private List<Patient> currentPatients = new List<Patient>();
        private Dictionary<int, TextEdit> testInputs = new Dictionary<int, TextEdit>();
        private List<TextEdit> nameFields = new List<TextEdit>();
        private List<TextEdit> valueFields = new List<TextEdit>();
        private List<TextEdit> referenceFields = new List<TextEdit>();
        private List<TextEdit> unitFields = new List<TextEdit>();
        private long currentPatientId;
        private bool isEditingAllowed = false;
        private List<Test> allTests = new List<Test>();
        private bool isProcessingClick = false;

        public ViewPatient()
        {
            InitializeComponent();
            LoadAllPatients();
            LoadAllTests();
            // Disable preview button initially
            NextToPreview.Enabled = false;
            // Enable save button
            SaveEditedPatient.Enabled = true;

            // Add tab change handler
            ViewPatientPage.SelectedPageChanged += ViewPatientPage_SelectedPageChanged;
            
            // Add mouse click handler for the checkedListBox
            checkedListBox1.MouseClick += CheckedListBox1_MouseClick;
            ModifySelectedTests.MouseClick += ModifySelectedTests_MouseClick;

            // Set the item height to add spacing between items
            checkedListBox1.ItemHeight = 25; // Increased height for better spacing
            checkedListBox1.Font = new Font(checkedListBox1.Font.FontFamily, 10); // Slightly larger font
            ModifySelectedTests.ItemHeight = 25;
            ModifySelectedTests.Font = new Font(ModifySelectedTests.Font.FontFamily, 10);

            // Add search handler
            SearchToModifySelectedTest.TextChanged += SearchToModifySelectedTest_TextChanged;

            // Remove the ItemCheck handler and add Click handler
            checkedListBox1.Click += checkedListBox1_Click;
            
            // Set CheckOnClick to true for better UX
            checkedListBox1.CheckOnClick = true;
        }
        private void ViewPatientPage_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            // If trying to switch to Edit Patient page and it's not allowed
            if (e.Page == EditPatientPage && !isEditingAllowed)
            {
                // Switch back to Search page
                ViewPatientPage.SelectedTabPage = SearchPatientPage;

                // Show message
                MessageBox.Show(
                    "Please select a patient from the list and click the 'Preview' button to edit.",
                    "Patient Selection Required",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }
        private void LoadAllPatients()
        {
            currentPatients = DatabaseHelper.GetAllPatients();
            UpdatePatientList();
        }
        private void UpdatePatientList()
        {
            checkedListBox1.Items.Clear();
            foreach (var patient in currentPatients)
            {
                checkedListBox1.Items.Add($"{patient.FullName} - {patient.VisitDate}");
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = textBox1.Text.Trim();
            currentPatients = DatabaseHelper.SearchPatients(searchTerm);
            UpdatePatientList();
        }
        private void CheckedListBox1_MouseClick(object sender, MouseEventArgs e)
        {
            // Get the index of the clicked item
            int index = checkedListBox1.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                // Toggle the checked state
                checkedListBox1.SetItemChecked(index, !checkedListBox1.GetItemChecked(index));
                // Set the selected index
                checkedListBox1.SelectedIndex = index;
                // Enable preview button
                NextToPreview.Enabled = true;
            }
        }
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Enable preview button only when a patient is selected
            NextToPreview.Enabled = checkedListBox1.SelectedIndex != -1;
        }
        private void NextToPreview_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedIndex == -1) return;

            // Allow editing for this transition
            isEditingAllowed = true;

            var selectedPatient = currentPatients[checkedListBox1.SelectedIndex];
            currentPatientId = selectedPatient.Id;
            var patientTests = DatabaseHelper.GetPatientTests(selectedPatient.Id);

            Console.WriteLine($"\n=== Loading tests for patient {selectedPatient.FullName} (ID: {selectedPatient.Id}) ===");
            Console.WriteLine($"Found {patientTests.Count} tests");

            // Set patient info and make it editable
            EditPagePatientName.Text = selectedPatient.FullName;
            EditPagePatientName.ReadOnly = false;

            EditPagePatientDate.Text = selectedPatient.VisitDate;
            EditPagePatientDate.ReadOnly = false;

            // Suspend layout updates
            EditPagePatientTests.SuspendLayout();
            ModifySelectedTests.SuspendLayout();

            try
            {
                // Clear existing controls and lists
                EditPagePatientTests.Controls.Clear();
                testInputs.Clear();
                nameFields.Clear();
                valueFields.Clear();
                referenceFields.Clear();
                unitFields.Clear();

                // First, add all tests to the ModifySelectedTests
                ModifySelectedTests.Items.Clear();
                ModifySelectedTests.ItemCheck -= ModifySelectedTests_ItemCheck; // Remove event handler temporarily

                // Create a HashSet of patient test names for quick lookup
                var patientTestNames = new HashSet<string>(patientTests.Select(pt => pt.TestName));
                Console.WriteLine("\nPatient test names: " + string.Join(", ", patientTestNames));

                // Add all available tests first
                foreach (var test in allTests)
                {
                    ModifySelectedTests.Items.Add(test.Name);
                }

                // Now set checkbox states and add test fields
                for (int i = 0; i < ModifySelectedTests.Items.Count; i++)
                {
                    string testName = ModifySelectedTests.Items[i].ToString();
                    bool isTestSelected = patientTestNames.Contains(testName);
                    
                    if (isTestSelected)
                    {
                        var test = allTests.First(t => t.Name == testName);
                        var patientTest = patientTests.First(pt => pt.TestName == testName);
                        AddTestField(test, patientTest.Value);
                        ModifySelectedTests.SetItemChecked(i, true);
                    }
                }

                // Add the ItemCheck handler back
                ModifySelectedTests.ItemCheck += ModifySelectedTests_ItemCheck;
            }
            finally
            {
                // Resume layout updates
                EditPagePatientTests.ResumeLayout(true);
                ModifySelectedTests.ResumeLayout(true);
            }

            // Switch to the edit tab
            ViewPatientPage.SelectedTabPage = EditPatientPage;
        }
        private void ModifySelectedTests_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // If we're not processing a click, prevent the change
            if (!isProcessingClick)
            {
                e.NewValue = e.CurrentValue;
            }
        }
        private void ModifySelectedTests_Click(object sender, EventArgs e)
        {
            // Get the clicked item index
            int index = ModifySelectedTests.IndexFromPoint(ModifySelectedTests.PointToClient(Cursor.Position));
            if (index != ListBox.NoMatches)
            {
                string selectedTestName = ModifySelectedTests.Items[index].ToString();
                bool isChecked = ModifySelectedTests.GetItemChecked(index);

                // If the test is already selected, ignore the click
                if (isChecked)
                {
                    return;
                }

                // If the test isn't already in our fields, add it
                if (!nameFields.Any(field => field.Text == selectedTestName))
                {
                    var test = allTests.First(t => t.Name == selectedTestName);
                    isProcessingClick = true;
                    ModifySelectedTests.SetItemChecked(index, true);
                    isProcessingClick = false;
                    AddTestField(test);
                }
            }
        }
        private void DeleteTest_Click(object sender, EventArgs e)
        {
            if (sender is Button deleteButton)
            {
                string testName = "";
                if (deleteButton.Tag is Test test)
                {
                    testName = test.Name;
                }
                else if (deleteButton.Tag is PatientTest patientTest)
                {
                    testName = patientTest.TestName;
                }

                if (!string.IsNullOrEmpty(testName))
                {
                    Console.WriteLine($"Deleting test: {testName}");
                    // Remove the test field
                    RemoveTestField(testName);

                    // Uncheck the box for the deleted test
                    for (int i = 0; i < ModifySelectedTests.Items.Count; i++)
                    {
                        if (ModifySelectedTests.Items[i].ToString() == testName)
                        {
                            Console.WriteLine($"Unchecking test in list: {testName}");
                            isProcessingClick = true;
                            ModifySelectedTests.SetItemChecked(i, false);
                            isProcessingClick = false;
                            break;
                        }
                    }
                }
            }
        }
        private void SaveEditedPatient_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate inputs
                var fullName = EditPagePatientName.Text.Trim();
                DateTime visitDate;

                if (!DateTime.TryParse(EditPagePatientDate.Text, out visitDate))
                {
                    MessageBox.Show("Please enter a valid visit date.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(fullName))
                {
                    MessageBox.Show("Please enter full name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Update patient information in the database
                using (var conn = new SQLiteConnection(DatabaseHelper.GetConnectionString()))
                {
                    conn.Open();
                    using var transaction = conn.BeginTransaction();
                    try
                    {
                        // Update patient details
                        var updatePatientCmd = new SQLiteCommand(@"
                            UPDATE Patients 
                            SET FullName = @name, VisitDate = @date
                            WHERE Id = @pid", conn, transaction);

                        updatePatientCmd.Parameters.AddWithValue("@name", fullName);
                        updatePatientCmd.Parameters.AddWithValue("@date", visitDate.ToString("yyyy-MM-dd"));
                        updatePatientCmd.Parameters.AddWithValue("@pid", currentPatientId);
                        updatePatientCmd.ExecuteNonQuery();

                        // Update test values
                        for (int i = 0; i < nameFields.Count; i++)
                        {
                            var testName = nameFields[i].Text.Trim();
                            var value = valueFields[i].Text.Trim();
                            var unit = unitFields[i].Text.Trim();
                            var referenceRange = referenceFields[i].Text.Trim();

                            // Get TestId from the dictionary
                            var testId = testInputs.FirstOrDefault(x => x.Value == valueFields[i]).Key;

                            var updateTestCmd = new SQLiteCommand(@"
                                UPDATE PatientTests 
                                SET Value = @val, Unit = @unit, ReferenceRange = @ref
                                WHERE PatientId = @pid AND TestId = @tid", conn, transaction);

                            updateTestCmd.Parameters.AddWithValue("@val", value);
                            updateTestCmd.Parameters.AddWithValue("@unit", unit);
                            updateTestCmd.Parameters.AddWithValue("@ref", referenceRange);
                            updateTestCmd.Parameters.AddWithValue("@pid", currentPatientId);
                            updateTestCmd.Parameters.AddWithValue("@tid", testId);
                            updateTestCmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        MessageBox.Show("Patient data updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Close the form instead of switching tabs
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception($"Error updating patient data: {ex.Message}", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving changes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DeleteSelectedPatient_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Please select a patient to delete.",
                    "No Selection",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            var selectedPatient = currentPatients[checkedListBox1.SelectedIndex];
            var confirmResult = MessageBox.Show(
                $"Are you sure you want to delete patient '{selectedPatient.FullName}'?\n\nThis will also delete all their test results and cannot be undone.",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2  // No is the default
            );

            if (confirmResult == DialogResult.Yes)
            {
                if (DatabaseHelper.DeletePatient(selectedPatient.Id))
                {
                    // Just refresh the patient list without showing success message
                    LoadAllPatients();
                }
            }
        }
        private void LoadAllTests()
        {
            try
            {
                allTests = DatabaseHelper.GetAllTests();
                if (allTests == null)
                {
                    allTests = new List<Test>();
                }
                UpdateTestList("");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tests: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UpdateTestList(string searchTerm)
        {
            ModifySelectedTests.SuspendLayout();
            try
            {
                searchTerm = searchTerm.ToLower();
                
                // Get current selected tests before clearing
                var selectedTests = new HashSet<string>();
                for (int i = 0; i < ModifySelectedTests.Items.Count; i++)
                {
                    if (ModifySelectedTests.GetItemChecked(i))
                    {
                        selectedTests.Add(ModifySelectedTests.Items[i].ToString());
                    }
                }
                
                // Also add any tests that are in the nameFields (these are definitely selected)
                selectedTests.UnionWith(nameFields.Select(field => field.Text));

                ModifySelectedTests.Items.Clear();
                
                // Add filtered tests and maintain selection state
                foreach (var test in allTests.Where(t => t.Name.ToLower().Contains(searchTerm)))
                {
                    ModifySelectedTests.Items.Add(test.Name);
                    ModifySelectedTests.SetItemChecked(ModifySelectedTests.Items.Count - 1, 
                        selectedTests.Contains(test.Name));
                }
            }
            finally
            {
                ModifySelectedTests.ResumeLayout(true);
            }
        }
        private void SearchToModifySelectedTest_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = SearchToModifySelectedTest.Text;
            UpdateTestList(searchTerm);
        }
        private void ModifySelectedTests_MouseClick(object sender, MouseEventArgs e)
        {
            int index = ModifySelectedTests.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                string selectedTestName = ModifySelectedTests.Items[index].ToString();
                bool isChecked = ModifySelectedTests.GetItemChecked(index);

                // If trying to uncheck, prevent it
                if (!isChecked)
                {
                    // If the test exists in our fields, keep it checked
                    if (nameFields.Any(field => field.Text == selectedTestName))
                    {
                        ModifySelectedTests.SetItemChecked(index, true);
                        MessageBox.Show("Please use the delete button to remove tests.", "Cannot Uncheck", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                // If trying to check a new item
                else
                {
                    // Check if test is already added
                    bool isDuplicate = nameFields.Any(field => field.Text == selectedTestName);
                    if (isDuplicate)
                    {
                        // Prevent the selection and show a message
                        ModifySelectedTests.SetItemChecked(index, false);
                        return;
                    }

                    // Add the test if it's not a duplicate
                    var test = allTests.First(t => t.Name == selectedTestName);
                    AddTestField(test);
                    
                    // Ensure checkbox state is correct
                    ModifySelectedTests.SetItemChecked(index, true);
                }
            }
        }
        private void AddTestField(Test test, string value = "")
        {
            EditPagePatientTests.SuspendLayout();
            try
            {
                const int VERTICAL_SPACING = 15;
                const int COLUMN_WIDTH = 150;
                const int TEXTBOX_HEIGHT = 30;
                const int HORIZONTAL_SPACING = 30;
                const int DELETE_BUTTON_WIDTH = 80;
                int startX = 10;

                // Calculate the Y position for the new test field
                int currentY = nameFields.Count > 0 
                    ? nameFields.Max(f => f.Location.Y) + VERTICAL_SPACING + TEXTBOX_HEIGHT 
                    : 30;

                // Create all controls first
                var controlsToAdd = new List<Control>();

                var nameValue = new TextEdit
                {
                    Text = test.Name,
                    Location = new Point(startX, currentY),
                    Width = COLUMN_WIDTH
                };
                nameValue.KeyDown += (s, e) => HandleEnterKey(s, e, nameFields);
                nameFields.Add(nameValue);

                var valueInput = new TextEdit
                {
                    Text = value,  // Use the provided value
                    Location = new Point(startX + COLUMN_WIDTH + HORIZONTAL_SPACING, currentY),
                    Width = COLUMN_WIDTH
                };
                valueInput.KeyDown += (s, e) => HandleEnterKey(s, e, valueFields);
                valueFields.Add(valueInput);
                testInputs[test.Id] = valueInput;

                var refValue = new TextEdit
                {
                    Text = test.ReferenceRange,
                    Location = new Point(startX + (COLUMN_WIDTH + HORIZONTAL_SPACING) * 2, currentY),
                    Width = COLUMN_WIDTH
                };
                refValue.KeyDown += (s, e) => HandleEnterKey(s, e, referenceFields);
                referenceFields.Add(refValue);

                var unitValue = new TextEdit
                {
                    Text = test.Unit,
                    Location = new Point(startX + (COLUMN_WIDTH + HORIZONTAL_SPACING) * 3, currentY),
                    Width = COLUMN_WIDTH
                };
                unitValue.KeyDown += (s, e) => HandleEnterKey(s, e, unitFields);
                unitFields.Add(unitValue);

                var deleteButton = new Button
                {
                    Text = "Delete",
                    Location = new Point(startX + (COLUMN_WIDTH + HORIZONTAL_SPACING) * 4, currentY),
                    Width = DELETE_BUTTON_WIDTH,
                    Height = TEXTBOX_HEIGHT,
                    Tag = test
                };
                deleteButton.Click += DeleteTest_Click;

                controlsToAdd.AddRange(new Control[]
                {
                    nameValue,
                    valueInput,
                    refValue,
                    unitValue,
                    deleteButton
                });

                // Add all controls at once
                EditPagePatientTests.Controls.AddRange(controlsToAdd.ToArray());
            }
            finally
            {
                EditPagePatientTests.ResumeLayout(true);
            }
        }
        private void RemoveTestField(string testName)
        {
            EditPagePatientTests.SuspendLayout();
            try
            {
                Console.WriteLine($"Removing test field: {testName}");
                // Find the index of the test to remove
                int indexToRemove = -1;
                for (int i = 0; i < nameFields.Count; i++)
                {
                    if (nameFields[i].Text == testName)
                    {
                        indexToRemove = i;
                        break;
                    }
                }

                if (indexToRemove != -1)
                {
                    Console.WriteLine($"Found test at index: {indexToRemove}");
                    // Store the value field reference before removing it
                    var valueField = valueFields[indexToRemove];

                    // Create a list of controls to remove
                    var controlsToRemove = new List<Control>
                    {
                        nameFields[indexToRemove],
                        valueFields[indexToRemove],
                        referenceFields[indexToRemove],
                        unitFields[indexToRemove]
                    };

                    // Find the delete button for this specific test
                    Button buttonToRemove = null;
                    foreach (Control control in EditPagePatientTests.Controls)
                    {
                        if (control is Button deleteButton)
                        {
                            bool isMatch = false;
                            if (deleteButton.Tag is Test test)
                            {
                                isMatch = test.Name == testName;
                            }
                            else if (deleteButton.Tag is PatientTest patientTest)
                            {
                                isMatch = patientTest.TestName == testName;
                            }

                            if (isMatch)
                            {
                                buttonToRemove = deleteButton;
                                break;
                            }
                        }
                    }

                    if (buttonToRemove != null)
                    {
                        controlsToRemove.Add(buttonToRemove);
                    }

                    // Remove only the controls for this specific test
                        foreach (var control in controlsToRemove)
                        {
                        Console.WriteLine($"Removing control: {control.GetType().Name}");
                            EditPagePatientTests.Controls.Remove(control);
                            control.Dispose();
                        }

                    // Remove from testInputs before removing from tracking lists
                    var testIdToRemove = testInputs.FirstOrDefault(x => x.Value == valueField).Key;
                    if (testIdToRemove != 0)
                    {
                        testInputs.Remove(testIdToRemove);
                    }

                    // Remove from our tracking lists
                    nameFields.RemoveAt(indexToRemove);
                    valueFields.RemoveAt(indexToRemove);
                    referenceFields.RemoveAt(indexToRemove);
                    unitFields.RemoveAt(indexToRemove);

                    // Reposition remaining fields
                    RepositionRemainingFields(indexToRemove);
                }
            }
            finally
            {
                EditPagePatientTests.ResumeLayout(true);
            }
        }
        private void RepositionRemainingFields(int startIndex)
        {
                        const int VERTICAL_SPACING = 15;
                        const int TEXTBOX_HEIGHT = 30;
            
            // Create a list to store all controls that need updating
            var controlUpdates = new List<(Control Control, Point NewLocation)>();
            
            for (int i = startIndex; i < nameFields.Count; i++)
            {
                int newY = i == 0 ? 30 : nameFields[i - 1].Location.Y + VERTICAL_SPACING + TEXTBOX_HEIGHT;
                var newLocation = new Point(nameFields[i].Location.X, newY);
                
                controlUpdates.Add((nameFields[i], newLocation));
                controlUpdates.Add((valueFields[i], new Point(valueFields[i].Location.X, newY)));
                controlUpdates.Add((referenceFields[i], new Point(referenceFields[i].Location.X, newY)));
                controlUpdates.Add((unitFields[i], new Point(unitFields[i].Location.X, newY)));

                // Find and update the delete button position
                foreach (Control control in EditPagePatientTests.Controls)
                {
                    if (control is Button deleteButton)
                    {
                        bool matchFound = false;
                        if (deleteButton.Tag is Test test)
                        {
                            matchFound = test.Name == nameFields[i].Text;
                        }
                        else if (deleteButton.Tag is PatientTest patientTest)
                        {
                            matchFound = patientTest.TestName == nameFields[i].Text;
                        }

                        if (matchFound)
                        {
                            controlUpdates.Add((deleteButton, new Point(deleteButton.Location.X, newY)));
                            break;
                        }
                    }
                }
            }

            // Apply all updates at once
            foreach (var update in controlUpdates)
            {
                update.Control.Location = update.NewLocation;
            }
        }
        private static void HandleEnterKey(object sender, KeyEventArgs e, List<TextEdit> fieldsGroup)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                TextEdit currentField = (TextEdit)sender;
                int currentIndex = fieldsGroup.IndexOf(currentField);

                if (currentIndex < fieldsGroup.Count - 1)
                {
                    fieldsGroup[currentIndex + 1].Focus();
                }
            }
        }
        private void checkedListBox1_Click(object sender, EventArgs e)
        {
            // Get the clicked item index
            int clickedIndex = checkedListBox1.IndexFromPoint(checkedListBox1.PointToClient(Cursor.Position));
            if (clickedIndex != -1)  // -1 means click was not on an item
            {
                // Toggle the clicked item
                bool newState = !checkedListBox1.GetItemChecked(clickedIndex);
                
                // First uncheck all items
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    checkedListBox1.SetItemChecked(i, false);
                }
                
                // Then set the clicked item to its new state
                checkedListBox1.SetItemChecked(clickedIndex, newState);
            }
        }
    }
}
