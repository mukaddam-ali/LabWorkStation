using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.IO;
using System.Drawing;

namespace Lab
{
    public partial class AddPatientForm : Form
    {
        private List<Test> allTests = new List<Test>();
        private List<Test> selectedTests = new List<Test>();
        private Dictionary<int, TextEdit> testInputs = new Dictionary<int, TextEdit>();
        private List<TextEdit> nameFields = new List<TextEdit>();
        private List<TextEdit> valueFields = new List<TextEdit>();
        private List<TextEdit> referenceFields = new List<TextEdit>();
        private List<TextEdit> unitFields = new List<TextEdit>();
        private string storedPatientName = "";
        private string storedVisitDate = "";

        public AddPatientForm()
        {
            InitializeComponent();
            LoadTestsFromDatabase();
            xtraTabControl.SelectedPageChanging += XtraTabControl_SelectedPageChanging;
            xtraTabControl.SelectedPageChanged += XtraTabControl_SelectedPageChanged;

            // Configure TestCheckBox
            TestCheckBox.CheckOnClick = true;
            TestCheckBox.MouseClick += TestCheckBox_MouseClick;
            TestCheckBox.ItemHeight = 25; // Increased height for better spacing
            TestCheckBox.Font = new Font(TestCheckBox.Font.FontFamily, 10); // Slightly larger font
        }

        private void LoadTestsFromDatabase()
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
            var filtered = allTests
                .Where(t => t.Name.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();

            TestCheckBox.Items.Clear();
            foreach (var test in filtered)
            {
                TestCheckBox.Items.Add(test.Name);
            }
        }

        private void HandleEnterKey(object sender, KeyEventArgs e, List<TextEdit> fieldsGroup)
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

        private void btnNextToValues_Click(object sender, EventArgs e)
        {
            EditPagePatientTests.Controls.Clear();
            selectedTests.Clear();
            nameFields.Clear();
            valueFields.Clear();
            referenceFields.Clear();
            unitFields.Clear();

            PatientName3.Text = storedPatientName;
            PatientName3.Properties.ReadOnly = true;

            Date3.Text = storedVisitDate;
            Date3.Properties.ReadOnly = true;

            storedPatientName = textEditFullName.Text;
            storedVisitDate = dateEditVisitDate.Text;

            int currentY = 60;
            const int VERTICAL_SPACING = 15;
            const int COLUMN_WIDTH = 150;
            const int TEXTBOX_HEIGHT = 30;
            const int HORIZONTAL_SPACING = 30;
            const int DELETE_BUTTON_WIDTH = 80;
            int startX = 10;

            foreach (var checkedItem in TestCheckBox.CheckedItems)
            {
                var testName = checkedItem.ToString();
                var test = allTests.FirstOrDefault(t => t.Name == testName);
                if (test != null)
                {
                    selectedTests.Add(test);

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
                        Location = new Point(startX + COLUMN_WIDTH + HORIZONTAL_SPACING, currentY),
                        Width = COLUMN_WIDTH,
                        Text = testInputs.ContainsKey(test.Id) ? testInputs[test.Id].Text : ""
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

                    // Add delete button for this test
                    var deleteButton = new SimpleButton
                    {
                        Text = "Remove",
                        Location = new Point(startX + (COLUMN_WIDTH + HORIZONTAL_SPACING) * 4 + 50, currentY),
                        Width = DELETE_BUTTON_WIDTH,
                        Height = TEXTBOX_HEIGHT,
                        Tag = test,
                        Appearance = {
                            BackColor = Color.LightPink
                        }
                    };
                    deleteButton.Click += DeleteSelectedTest_Click;

                    EditPagePatientTests.Controls.AddRange(new Control[]
                    {
                        nameValue,
                        valueInput,
                        refValue,
                        unitValue,
                        deleteButton
                    });

                    currentY += TEXTBOX_HEIGHT + VERTICAL_SPACING;
                }
            }

            xtraTabControl.SelectedTabPage = tabEnterValues;
        }

        private void DeleteSelectedTest_Click(object sender, EventArgs e)
        {
            var button = (SimpleButton)sender;
            var test = (Test)button.Tag;

            // Find the index of the test in our collections
            int index = selectedTests.IndexOf(test);
            if (index >= 0)
            {
                // Remove the controls for this test
                var controlsToRemove = new Control[]
                {
                    nameFields[index],
                    valueFields[index],
                    referenceFields[index],
                    unitFields[index],
                    button
                };

                foreach (var control in controlsToRemove)
                {
                    EditPagePatientTests.Controls.Remove(control);
                    control.Dispose();
                }

                // Remove from our tracking collections
                nameFields.RemoveAt(index);
                valueFields.RemoveAt(index);
                referenceFields.RemoveAt(index);
                unitFields.RemoveAt(index);
                testInputs.Remove(test.Id);
                selectedTests.RemoveAt(index);

                // Uncheck the test in the TestCheckBox
                int checkBoxIndex = TestCheckBox.Items.IndexOf(test.Name);
                if (checkBoxIndex >= 0)
                {
                    TestCheckBox.SetItemChecked(checkBoxIndex, false);
                }

                // Reposition remaining controls
                const int VERTICAL_SPACING = 15;
                const int TEXTBOX_HEIGHT = 30;
                int currentY = 60;

                for (int i = 0; i < nameFields.Count; i++)
                {
                    nameFields[i].Top = currentY;
                    valueFields[i].Top = currentY;
                    referenceFields[i].Top = currentY;
                    unitFields[i].Top = currentY;

                    // Find and update the delete button position
                    var deleteBtn = EditPagePatientTests.Controls.OfType<SimpleButton>()
                        .FirstOrDefault(b => b.Top == nameFields[i].Top);
                    if (deleteBtn != null)
                    {
                        deleteBtn.Top = currentY;
                    }

                    currentY += TEXTBOX_HEIGHT + VERTICAL_SPACING;
                }
            }
        }

        private void btnSavePatient_Click(object sender, EventArgs e)
        {
            try
            {
                var fullName = PatientName3.Text.Trim();
                DateTime visitDate;

                if (!DateTime.TryParse(Date3.Text, out visitDate))
                {
                    MessageBox.Show("Please enter a valid visit date.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(fullName))
                {
                    MessageBox.Show("Please enter full name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (selectedTests.Count == 0)
                {
                    MessageBox.Show("Please select at least one test.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var patientId = DatabaseHelper.InsertPatient(fullName, visitDate);

                for (int i = 0; i < selectedTests.Count; i++)
                {
                    var test = selectedTests[i];
                    if (!testInputs.ContainsKey(test.Id))
                    {
                        throw new Exception($"Test input not found for test: {test.Name}");
                    }

                    var value = valueFields[i].Text.Trim();
                    var unit = unitFields[i].Text.Trim();
                    var referenceRange = referenceFields[i].Text.Trim();

                    if (!string.IsNullOrEmpty(value))
                    {
                        DatabaseHelper.InsertPatientTest(
                            patientId,
                            test.Id,
                            value,
                            unit,
                            referenceRange
                        );
                    }
                }

                MessageBox.Show("Patient data saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving patient data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TestGridView_Click(object sender, EventArgs e)
        {
            // Unused
        }

        private void BtnNextToTests_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textEditFullName.Text) &&
                !string.IsNullOrWhiteSpace(dateEditVisitDate.Text))
            {
                // Store the values when moving to test selection
                storedPatientName = textEditFullName.Text;
                storedVisitDate = dateEditVisitDate.Text;
                xtraTabControl.SelectedTabPage = tabSelectTests;
            }
            else
            {
                MessageBox.Show("The fields above shouldn't be empty.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void TestCheckBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TestCheckBox.SelectedItem == null) return;
            var selectedName = TestCheckBox.SelectedItem.ToString();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text;
            UpdateTestList(searchTerm);
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            txtSearch.SelectAll();
        }

        private void txtSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.SelectionLength == 0)
            {
                txtSearch.SelectAll();
            }
        }

        private void XtraTabControl_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            // Store values when leaving patient info
            if (e.PrevPage == tabPatientInfo)
            {
                storedPatientName = textEditFullName.Text;
                storedVisitDate = dateEditVisitDate.Text;
            }
            // Restore values when returning to patient info
            else if (e.Page == tabPatientInfo)
            {
                textEditFullName.Text = storedPatientName;
                dateEditVisitDate.Text = storedVisitDate;
            }
        }

        private void XtraTabControl_SelectedPageChanging(object sender, DevExpress.XtraTab.TabPageChangingEventArgs e)
        {
            // Always store current values when leaving patient info tab
            if (xtraTabControl.SelectedTabPage == tabPatientInfo)
            {
                storedPatientName = textEditFullName.Text;
                storedVisitDate = dateEditVisitDate.Text;
            }

            // Always restore values when going back to patient info tab
            if (e.Page == tabPatientInfo)
            {
                textEditFullName.Text = storedPatientName;
                dateEditVisitDate.Text = storedVisitDate;
            }

            if (e.Page == tabSelectTests)
            {
                if (string.IsNullOrWhiteSpace(textEditFullName.Text) ||
                    string.IsNullOrWhiteSpace(dateEditVisitDate.Text))
                {
                    e.Cancel = true;
                    MessageBox.Show("Please complete patient information first.",
                                  "Incomplete Information",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Warning);
                    return;
                }
                // Store values when moving to test selection
                storedPatientName = textEditFullName.Text;
                storedVisitDate = dateEditVisitDate.Text;
            }
            else if (e.Page == tabEnterValues)
            {
                if (TestCheckBox.CheckedItems.Count == 0)
                {
                    e.Cancel = true;
                    MessageBox.Show("Please select at least one test.",
                                  "No Tests Selected",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        private void BtnBackToPatientInfo_Click(object sender, EventArgs e)
        {
            // Restore the stored values when going back
            textEditFullName.Text = storedPatientName;
            dateEditVisitDate.Text = storedVisitDate;
            xtraTabControl.SelectedTabPage = tabPatientInfo;
        }

        private void BtnBackToSelectTest_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < selectedTests.Count; i++)
            {
                var test = selectedTests[i];
                if (testInputs.ContainsKey(test.Id))
                {
                    testInputs[test.Id].Text = valueFields[i].Text;
                }
            }

            storedPatientName = PatientName3.Text;
            storedVisitDate = Date3.Text;

            textEditFullName.Text = storedPatientName;
            dateEditVisitDate.Text = storedVisitDate;

            xtraTabControl.SelectedTabPage = tabSelectTests;
        }

        private void TestCheckBox_MouseClick(object sender, MouseEventArgs e)
        {
            // Get the index of the clicked item
            int index = TestCheckBox.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                // Set the selected index without manually toggling the check state
                // The CheckOnClick property will handle the toggling automatically
                TestCheckBox.SelectedIndex = index;
            }
        }

        private void FromAddPatientToMenu_Click(object sender, EventArgs e)
        {
            // Clear all fields and collections
            textEditFullName.Text = "";
            dateEditVisitDate.Text = "";
            PatientName3.Text = "";
            Date3.Text = "";
            storedPatientName = "";
            storedVisitDate = "";

            // Clear all test selections
            TestCheckBox.Items.Clear();
            selectedTests.Clear();
            testInputs.Clear();
            nameFields.Clear();
            valueFields.Clear();
            referenceFields.Clear();
            unitFields.Clear();

            // Clear the test values page
            EditPagePatientTests.Controls.Clear();

            // Uncheck all items in the test selection
            for (int i = 0; i < TestCheckBox.Items.Count; i++)
            {
                TestCheckBox.SetItemChecked(i, false);
            }

            // Reset to first tab
            xtraTabControl.SelectedTabPage = tabPatientInfo;

            // Close this form
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
