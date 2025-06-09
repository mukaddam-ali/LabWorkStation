using System;
using System.Windows.Forms;
using System.IO;

namespace Lab
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                ApplicationConfiguration.Initialize();
                
                // Initialize databases in correct order
                TestDatabaseInitializer.Initialize();  // First ensure test data exists
                DatabaseHelper.InitializeDatabase();   // Then initialize patients database
                
                Application.Run(new Form1());
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Application startup error: {ex.Message}\n\nPlease ensure you have write permissions to the application directory.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}