using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.IO;
using System.Drawing;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Font = System.Drawing.Font;
using ITextFont = iTextSharp.text.Font;

namespace Lab
{
    public partial class AddPatientForm : Form
    {
        private List<Test> allTests = new List<Test>();
        private List<Test> selectedTests = new List<Test>();
        private HashSet<string> persistentSelectedTests = new HashSet<string>();
        private Dictionary<int, TextEdit> testInputs = new Dictionary<int, TextEdit>();
        private List<TextEdit> nameFields = new List<TextEdit>();
        private List<TextEdit> valueFields = new List<TextEdit>();
        private List<TextEdit> referenceFields = new List<TextEdit>();
        private List<TextEdit> unitFields = new List<TextEdit>();
        private string storedPatientName = "";
        private string storedVisitDate = "";
        private HashSet<string> selectedTestNames = new HashSet<string>();
        private bool isButtonNavigation = false;

        public AddPatientForm()
        {
            InitializeComponent();
            LoadTestsFromDatabase();
            xtraTabControl.SelectedPageChanging += XtraTabControl_SelectedPageChanging;
            xtraTabControl.SelectedPageChanged += XtraTabControl_SelectedPageChanged;
            xtraTabControl.MouseDown += XtraTabControl_MouseDown;

            // Configure tab control behavior
            xtraTabControl.ShowTabHeader = DevExpress.Utils.DefaultBoolean.True;
            xtraTabControl.HeaderOrientation = DevExpress.XtraTab.TabOrientation.Horizontal;
            xtraTabControl.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Top;

            // Configure date editors
            dateEditVisitDate.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            dateEditVisitDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dateEditVisitDate.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            dateEditVisitDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dateEditVisitDate.Properties.Mask.EditMask = "dd/MM/yyyy";

            // Configure TestCheckBox
            TestCheckBox.CheckOnClick = true;
            TestCheckBox.MouseClick += TestCheckBox_MouseClick;
            TestCheckBox.ItemCheck += TestCheckBox_ItemCheck;
            TestCheckBox.ItemHeight = 25; // Increased height for better spacing
            TestCheckBox.Font = new Font(TestCheckBox.Font.FontFamily, 10); // Slightly larger font

            // Initially disable the Save button
            btnSavePatient.Enabled = false;

            // Add text changed handler for name fields
            textEditFullName.TextChanged += ValidateInputs;
            PatientName3.TextChanged += ValidateInputs;
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

            TestCheckBox.BeginUpdate();
            try
            {
                TestCheckBox.Items.Clear();
                foreach (var test in filtered)
                {
                    TestCheckBox.Items.Add(test.Name);
                    // Check the item if it's in our persistent selection
                    if (persistentSelectedTests.Contains(test.Name))
                    {
                        TestCheckBox.SetItemChecked(TestCheckBox.Items.Count - 1, true);
                    }
                }
            }
            finally
            {
                TestCheckBox.EndUpdate();
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

            // Set the flag before changing tabs
            isButtonNavigation = true;
            xtraTabControl.SelectedTabPage = tabEnterValues;

            int currentY = 60;
            const int VERTICAL_SPACING = 15;
            const int COLUMN_WIDTH = 150;
            const int TEXTBOX_HEIGHT = 30;
            const int HORIZONTAL_SPACING = 30;
            const int DELETE_BUTTON_WIDTH = 80;
            int startX = 10;

            // Use persistentSelectedTests to create fields for ALL selected tests
            foreach (var test in allTests)
            {
                if (persistentSelectedTests.Contains(test.Name))
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

                    var deleteButton = new Button
                    {
                        Text = "Delete",
                        Location = new Point(startX + (COLUMN_WIDTH + HORIZONTAL_SPACING) * 4, currentY),
                        Width = DELETE_BUTTON_WIDTH,
                        Height = TEXTBOX_HEIGHT,
                        Tag = test
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
        }

        private void DeleteSelectedTest_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var test = (Test)button.Tag;

            // Remove from persistent selection
            persistentSelectedTests.Remove(test.Name);

            // Find the index of the test in our collections
            int index = selectedTests.IndexOf(test);
            if (index >= 0)
            {
                EditPagePatientTests.SuspendLayout();
                try
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

                    // Uncheck the test in the TestCheckBox if it's visible
                    for (int i = 0; i < TestCheckBox.Items.Count; i++)
                    {
                        if (TestCheckBox.Items[i].ToString() == test.Name)
                        {
                            TestCheckBox.SetItemChecked(i, false);
                            break;
                        }
                    }

                    // Reposition remaining controls
                    const int VERTICAL_SPACING = 15;
                    const int TEXTBOX_HEIGHT = 30;
                    int currentY = 60; // Starting Y position for the first test

                    for (int i = 0; i < nameFields.Count; i++)
                    {
                        // Update positions for all controls in this row
                        nameFields[i].Top = currentY;
                        valueFields[i].Top = currentY;
                        referenceFields[i].Top = currentY;
                        unitFields[i].Top = currentY;

                        // Find and update the delete button position
                        foreach (Control control in EditPagePatientTests.Controls)
                        {
                            if (control is Button deleteButton && deleteButton.Tag is Test buttonTest && buttonTest.Name == nameFields[i].Text)
                            {
                                deleteButton.Top = currentY;
                                break;
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

                // List to store patient tests for PDF generation
                List<PatientTest> patientTests = new List<PatientTest>();

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

                        // Add to list for PDF generation
                        patientTests.Add(new PatientTest
                        {
                            TestName = test.Name,
                            Value = value,
                            Unit = unit,
                            ReferenceRange = referenceRange
                        });
                    }
                }

                MessageBox.Show("Patient data saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Ask if they want to print
                var printResult = MessageBox.Show("Would you like to print this patient's report?", 
                    "Print Report", 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question);

                if (printResult == DialogResult.Yes)
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog
                    {
                        Filter = "PDF files (*.pdf)|*.pdf",
                        FilterIndex = 1,
                        RestoreDirectory = true,
                        FileName = $"{fullName}_{visitDate.ToString("yyyy-MM-dd")}.pdf"
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
                            patientInfo.Add(new Chunk(visitDate.ToString("yyyy-MM-dd") + "\n\n", normalFont));
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
                            MessageBox.Show("PDF file has been created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }

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
                
                // Set the flag before changing tabs
                isButtonNavigation = true;
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
            if (!isButtonNavigation)
            {
                e.Cancel = true;
                if (e.Page == tabEnterValues)
                {
                    if (TestCheckBox.CheckedItems.Count == 0)
                    {
                        MessageBox.Show("Please select at least one test and use the Next button to continue.",
                            "No Tests Selected",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Please use the Next button to continue.",
                            "Use Next Button",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
                else if (e.Page == tabSelectTests)
                {
                    if (string.IsNullOrWhiteSpace(textEditFullName.Text) || dateEditVisitDate.EditValue == null)
                    {
                        MessageBox.Show("Please complete patient information and use the Next button to continue.",
                            "Incomplete Patient Information",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Please use the Next button to continue.",
                            "Use Next Button",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            }
            isButtonNavigation = false;
        }

        private void XtraTabControl_MouseDown(object sender, MouseEventArgs e)
        {
            // Empty handler - all navigation and messages are handled in SelectedPageChanging
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

        private void TestCheckBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string itemText = TestCheckBox.Items[e.Index].ToString();
            if (e.NewValue == CheckState.Checked)
            {
                persistentSelectedTests.Add(itemText);
            }
            else
            {
                persistentSelectedTests.Remove(itemText);
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

        private void BtnBackToPatientInfo_Click(object sender, EventArgs e)
        {
            // Restore the stored values when going back
            textEditFullName.Text = storedPatientName;
            dateEditVisitDate.Text = storedVisitDate;
            isButtonNavigation = true;
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

            storedPatientName = textEditFullName.Text;
            storedVisitDate = dateEditVisitDate.Text;
            isButtonNavigation = true;
            xtraTabControl.SelectedTabPage = tabSelectTests;
        }

        private void ValidateInputs(object sender, EventArgs e)
        {
            // Enable save button only if the name is not empty
            btnSavePatient.Enabled = !string.IsNullOrWhiteSpace(PatientName3.Text);
        }
    }
}
