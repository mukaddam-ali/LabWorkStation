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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using Font = System.Drawing.Font;
using ITextFont = iTextSharp.text.Font;

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
        private HashSet<string> selectedTestNames = new HashSet<string>();

        public ViewPatient()
        {
            InitializeComponent();
            LoadAllPatients();
            LoadAllTests();
            DeleteSelectedPatient.Click += DeleteSelectedPatient_Click;
            SreachPatientToMenu.Click += SreachPatientToMenu_Click;
            EditPatientToMenu.Click += EditPatientToMenu_Click;

            // Configure date editor
            EditPagePatientDate.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            EditPagePatientDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            EditPagePatientDate.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            EditPagePatientDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            EditPagePatientDate.Properties.Mask.EditMask = "dd/MM/yyyy";

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

            // Store all selected test names
            selectedTestNames.Clear();
            foreach (var test in patientTests)
            {
                selectedTestNames.Add(test.TestName);
            }

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

                // Add all available tests and set their check states
                foreach (var test in allTests)
                {
                    ModifySelectedTests.Items.Add(test.Name);
                    int index = ModifySelectedTests.Items.Count - 1;
                    
                    if (selectedTestNames.Contains(test.Name))
                    {
                        ModifySelectedTests.SetItemChecked(index, true);
                        var patientTest = patientTests.First(pt => pt.TestName == test.Name);
                        AddTestField(test, patientTest.Value);
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
                var fullName = EditPagePatientName.Text.Trim();
                var visitDate = EditPagePatientDate.Text.Trim();

                if (string.IsNullOrWhiteSpace(fullName))
                {
                    MessageBox.Show("Please enter full name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Update patient information
                DatabaseHelper.UpdatePatient(currentPatientId, fullName, visitDate);

                // Get all currently selected tests from ModifySelectedTests
                var selectedTestNames = new HashSet<string>();
                for (int i = 0; i < ModifySelectedTests.Items.Count; i++)
                {
                    if (ModifySelectedTests.GetItemChecked(i))
                    {
                        selectedTestNames.Add(ModifySelectedTests.Items[i].ToString());
                    }
                }

                // First, get existing patient tests to determine which ones to add/update/remove
                var existingTests = DatabaseHelper.GetPatientTests(currentPatientId);
                var existingTestNames = existingTests.Select(t => t.TestName).ToHashSet();

                // Tests to remove (tests that exist but are no longer selected)
                var testsToRemove = existingTestNames.Except(selectedTestNames);
                foreach (var testName in testsToRemove)
                {
                    var test = allTests.FirstOrDefault(t => t.Name == testName);
                    if (test != null)
                    {
                        DatabaseHelper.DeletePatientTest(currentPatientId, test.Id);
                    }
                }

                // Update or add tests
                foreach (var testName in selectedTestNames)
                {
                    var test = allTests.FirstOrDefault(t => t.Name == testName);
                    if (test != null)
                    {
                        // Find the test's values in the UI
                        int fieldIndex = -1;
                        for (int i = 0; i < nameFields.Count; i++)
                        {
                            if (nameFields[i].Text == testName)
                            {
                                fieldIndex = i;
                                break;
                            }
                        }

                        string value = fieldIndex >= 0 ? valueFields[fieldIndex].Text.Trim() : "";
                        string unit = fieldIndex >= 0 ? unitFields[fieldIndex].Text.Trim() : test.Unit;
                        string referenceRange = fieldIndex >= 0 ? referenceFields[fieldIndex].Text.Trim() : test.ReferenceRange;

                        // If the test exists, update it; otherwise, insert it
                        if (existingTestNames.Contains(testName))
                        {
                            DatabaseHelper.UpdatePatientTest(currentPatientId, test.Id, value, unit, referenceRange);
                        }
                        else
                        {
                            DatabaseHelper.InsertPatientTest(currentPatientId, test.Id, value, unit, referenceRange);
                        }
                    }
                }

                MessageBox.Show("Patient data saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Return to menu
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving patient data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            // Store currently checked items
            selectedTestNames.Clear();
            for (int i = 0; i < ModifySelectedTests.Items.Count; i++)
            {
                if (ModifySelectedTests.GetItemChecked(i))
                {
                    selectedTestNames.Add(ModifySelectedTests.Items[i].ToString());
                }
            }

                ModifySelectedTests.Items.Clear();
            ModifySelectedTests.ItemCheck -= ModifySelectedTests_ItemCheck; // Remove event handler temporarily

            // Add filtered tests and restore check states
            foreach (var test in allTests)
            {
                if (test.Name.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    ModifySelectedTests.Items.Add(test.Name);
                    int index = ModifySelectedTests.Items.Count - 1;
                    if (selectedTestNames.Contains(test.Name))
                    {
                        ModifySelectedTests.SetItemChecked(index, true);
                    }
                }
            }

            // Add the ItemCheck handler back
            ModifySelectedTests.ItemCheck += ModifySelectedTests_ItemCheck;
        }
        private void SearchToModifySelectedTest_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = SearchToModifySelectedTest.Text.Trim();
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
            EditPagePatientTests.SuspendLayout();
            try
            {
                const int VERTICAL_SPACING = 15;
                const int TEXTBOX_HEIGHT = 30;
                int currentY = startIndex == 0 ? 30 : 30 + (startIndex * (TEXTBOX_HEIGHT + VERTICAL_SPACING));

                for (int i = startIndex; i < nameFields.Count; i++)
                {
                    // Update positions for all controls in this row
                    nameFields[i].Top = currentY;
                    valueFields[i].Top = currentY;
                    referenceFields[i].Top = currentY;
                    unitFields[i].Top = currentY;

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
                                deleteButton.Top = currentY;
                                break;
                            }
                        }
                    }

                    currentY += TEXTBOX_HEIGHT + VERTICAL_SPACING;
                }
            }
            finally
            {
                EditPagePatientTests.ResumeLayout(true);
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

        private void PrintPdf_Click(object sender, EventArgs e)
        {
            try
            {
                // First, save all changes
                var fullName = EditPagePatientName.Text.Trim();
                var visitDate = EditPagePatientDate.Text.Trim();

                if (string.IsNullOrWhiteSpace(fullName))
                {
                    MessageBox.Show("Please enter full name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Update patient information
                DatabaseHelper.UpdatePatient(currentPatientId, fullName, visitDate);

                // Get all currently selected tests from ModifySelectedTests
                var selectedTestNames = new HashSet<string>();
                for (int i = 0; i < ModifySelectedTests.Items.Count; i++)
                {
                    if (ModifySelectedTests.GetItemChecked(i))
                    {
                        selectedTestNames.Add(ModifySelectedTests.Items[i].ToString());
                    }
                }

                // First, get existing patient tests to determine which ones to add/update/remove
                var existingTests = DatabaseHelper.GetPatientTests(currentPatientId);
                var existingTestNames = existingTests.Select(t => t.TestName).ToHashSet();

                // Tests to remove (tests that exist but are no longer selected)
                var testsToRemove = existingTestNames.Except(selectedTestNames);
                foreach (var testName in testsToRemove)
                {
                    var test = allTests.FirstOrDefault(t => t.Name == testName);
                    if (test != null)
                    {
                        DatabaseHelper.DeletePatientTest(currentPatientId, test.Id);
                    }
                }

                // Update or add tests
                foreach (var testName in selectedTestNames)
                {
                    var test = allTests.FirstOrDefault(t => t.Name == testName);
                    if (test != null)
                    {
                        // Find the test's values in the UI
                        int fieldIndex = -1;
                        for (int i = 0; i < nameFields.Count; i++)
                        {
                            if (nameFields[i].Text == testName)
                            {
                                fieldIndex = i;
                                break;
                            }
                        }

                        string value = fieldIndex >= 0 ? valueFields[fieldIndex].Text.Trim() : "";
                        string unit = fieldIndex >= 0 ? unitFields[fieldIndex].Text.Trim() : test.Unit;
                        string referenceRange = fieldIndex >= 0 ? referenceFields[fieldIndex].Text.Trim() : test.ReferenceRange;

                        // If the test exists, update it; otherwise, insert it
                        if (existingTestNames.Contains(testName))
                        {
                            DatabaseHelper.UpdatePatientTest(currentPatientId, test.Id, value, unit, referenceRange);
                        }
                        else
                        {
                            DatabaseHelper.InsertPatientTest(currentPatientId, test.Id, value, unit, referenceRange);
                        }
                    }
                }

                // Get fresh data from database after saving
                var patientTests = DatabaseHelper.GetPatientTests(currentPatientId);

                // Now proceed with PDF generation
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF files (*.pdf)|*.pdf",
                    FilterIndex = 1,
                    RestoreDirectory = true,
                    FileName = $"{fullName}_{visitDate.Replace('/', '-')}.pdf"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create))
                    {
                        Document document = new Document(PageSize.A4, 50, 50, 50, 50);
                        PdfWriter writer = PdfWriter.GetInstance(document, fs);
                        document.Open();

                        // Add title
                        var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
                        var headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
                        var normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);

                        // Add laboratory name at the top
                        Paragraph labName = new Paragraph("Laboratory Report", titleFont);
                        labName.Alignment = Element.ALIGN_CENTER;
                        labName.SpacingAfter = 20f;
                        document.Add(labName);

                        // Add patient information
                        Paragraph patientInfo = new Paragraph();
                        patientInfo.Add(new Chunk("Patient Name: ", headerFont));
                        patientInfo.Add(new Chunk(fullName + "\n", normalFont));
                        patientInfo.Add(new Chunk("Visit Date: ", headerFont));
                        patientInfo.Add(new Chunk(visitDate + "\n\n", normalFont));
                        document.Add(patientInfo);

                        // Create table for test results
                        PdfPTable table = new PdfPTable(4);
                        table.WidthPercentage = 100;
                        float[] columnWidths = { 3f, 2f, 2f, 2f };
                        table.SetWidths(columnWidths);

                        // Add table headers
                        string[] headers = { "Test Name", "Value", "Reference Range", "Unit" };
                        foreach (string header in headers)
                        {
                            PdfPCell cell = new PdfPCell(new Phrase(header, headerFont));
                            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell.Padding = 5;
                            table.AddCell(cell);
                        }

                        // Add test results
                        foreach (var test in patientTests)
                        {
                            // Test name
                            PdfPCell nameCell = new PdfPCell(new Phrase(test.TestName, normalFont));
                            nameCell.HorizontalAlignment = Element.ALIGN_LEFT;
                            nameCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                            nameCell.Padding = 5;
                            table.AddCell(nameCell);

                            // Value
                            PdfPCell valueCell = new PdfPCell(new Phrase(test.Value, normalFont));
                            valueCell.HorizontalAlignment = Element.ALIGN_CENTER;
                            valueCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                            valueCell.Padding = 5;
                            table.AddCell(valueCell);

                            // Reference Range
                            PdfPCell refCell = new PdfPCell(new Phrase(test.ReferenceRange, normalFont));
                            refCell.HorizontalAlignment = Element.ALIGN_CENTER;
                            refCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                            refCell.Padding = 5;
                            table.AddCell(refCell);

                            // Unit
                            PdfPCell unitCell = new PdfPCell(new Phrase(test.Unit, normalFont));
                            unitCell.HorizontalAlignment = Element.ALIGN_CENTER;
                            unitCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                            unitCell.Padding = 5;
                            table.AddCell(unitCell);
                        }

                        document.Add(table);
                        document.Close();
                        MessageBox.Show("Changes saved and PDF file has been created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Return to menu
                        this.DialogResult = DialogResult.Cancel;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving changes and creating the PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SreachPatientToMenu_Click(object sender, EventArgs e)
        {
            // Clear all fields and selections
            textBox1.Text = "";
            checkedListBox1.Items.Clear();
            EditPagePatientName.Text = "";
            EditPagePatientDate.Text = "";
            EditPagePatientTests.Controls.Clear();
            ModifySelectedTests.Items.Clear();

            // Clear all collections
            currentPatients.Clear();
            testInputs.Clear();
            nameFields.Clear();
            valueFields.Clear();
            referenceFields.Clear();
            unitFields.Clear();

            // Reset tab to search page
            ViewPatientPage.SelectedTabPage = SearchPatientPage;
            isEditingAllowed = false;

            // Close the form
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void EditPatientToMenu_Click(object sender, EventArgs e)
        {
            // Clear all fields and selections
            EditPagePatientName.Text = "";
            EditPagePatientDate.EditValue = null;
            EditPagePatientTests.Controls.Clear();
            ModifySelectedTests.Items.Clear();

            // Clear all collections
            testInputs.Clear();
            nameFields.Clear();
            valueFields.Clear();
            referenceFields.Clear();
            unitFields.Clear();

            // Reset tab to search page and reload patients
            ViewPatientPage.SelectedTabPage = SearchPatientPage;
            isEditingAllowed = false;
            LoadAllPatients(); // Refresh the patient list
        }
    }
}
