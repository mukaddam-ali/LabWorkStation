using System;
using System.Data.SQLite;
using System.IO;

namespace Lab
{
    public static class TestDatabaseInitializer
    {
        public static void Initialize()
        {
            string testListDir = Path.GetFullPath(Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Test List"
            ));

            if (!Directory.Exists(testListDir))
            {
                Directory.CreateDirectory(testListDir);
            }

            string dbPath = Path.Combine(testListDir, "Tests.db");
            string connectionString = $"Data Source={dbPath};Version=3;";

            Console.WriteLine($"Initializing test database at: {dbPath}");

            // Only create database if it doesn't exist
            bool needsInit = !File.Exists(dbPath);
            if (needsInit)
            {
                SQLiteConnection.CreateFile(dbPath);
            }

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                if (needsInit)
                {
                    Console.WriteLine("Creating test database for the first time...");
                    // Create table
                    using (var command = new SQLiteCommand(
                        @"CREATE TABLE IF NOT EXISTS TestList (
                            Id INTEGER PRIMARY KEY,
                            Name TEXT NOT NULL,
                            Unit TEXT,
                            Ref TEXT
                        )", connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    // Insert all tests
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            var insertCmd = new SQLiteCommand(
                                "INSERT INTO TestList (Id, Name, Unit, Ref) VALUES (@id, @name, @unit, @ref)",
                                connection
                            );

                            // All 65 tests from your database
                            var tests = new[]
                            {
                                new { Id = 1, Name = "Glucose", Unit = "mg/dl", Ref = "70 - 120" },
                                new { Id = 2, Name = "Creatinine", Unit = "mg/dl", Ref = "0.4 - 1.3" },
                                new { Id = 3, Name = "Uric Acid (Women)", Unit = "mg/dl", Ref = "2.4 - 5.7" },
                                new { Id = 4, Name = "Uric Acid (Men)", Unit = "mg/dl", Ref = "2.5 - 7" },
                                new { Id = 5, Name = "Cholesterol", Unit = "mg/dl", Ref = "Less Than 200" },
                                new { Id = 6, Name = "Triglycerides (Women)", Unit = "mg/dl", Ref = "50 - 170" },
                                new { Id = 7, Name = "Triglycerides (Men)", Unit = "mg/dl", Ref = "60 - 220" },
                                new { Id = 8, Name = "Amylase (Total)", Unit = "u/L", Ref = "20 - 98" },
                                new { Id = 9, Name = "Alkaline Phosphatase", Unit = "u/L", Ref = "21 - 92" },
                                new { Id = 10, Name = "AST (SGOT)", Unit = "u/L", Ref = "5 - 45" },
                                new { Id = 11, Name = "ALT (SGPT)", Unit = "u/L", Ref = "5 - 45" },
                                new { Id = 12, Name = "LDH", Unit = "u/L", Ref = "207 - 414" },
                                new { Id = 13, Name = "CK (Women)", Unit = "u/L", Ref = "< 195" },
                                new { Id = 14, Name = "CK (Men)", Unit = "u/L", Ref = "< 177" },
                                new { Id = 15, Name = "Calcium", Unit = "mg/dl", Ref = "8.4 - 10.5" },
                                new { Id = 16, Name = "Phosphorus", Unit = "mg/dl", Ref = "2.7 - 5.5" },
                                new { Id = 17, Name = "Sodium", Unit = "mmol/L", Ref = "135 - 145" },
                                new { Id = 18, Name = "Potassium", Unit = "mmol/L", Ref = "3.5 - 5.5" },
                                new { Id = 19, Name = "Magnesium", Unit = "mmol/L", Ref = "1.5 - 2.5" },
                                new { Id = 20, Name = "Chloride", Unit = "mmol/L", Ref = "96 - 106" },
                                new { Id = 21, Name = "Protein", Unit = "g/dl", Ref = "6 - 8" },
                                new { Id = 22, Name = "Iron", Unit = "ug/dl", Ref = "60 - 160" },
                                new { Id = 23, Name = "TIBC", Unit = "ug/dl", Ref = "200 - 400" },
                                new { Id = 24, Name = "LDL Cholesterol", Unit = "mg/dl", Ref = "60 - 130" },
                                new { Id = 25, Name = "Urea", Unit = "mg/dl", Ref = "20 - 50" },
                                new { Id = 26, Name = "Free T4", Unit = "pmol/L", Ref = "12 - 22" },
                                new { Id = 27, Name = "Free T3", Unit = "pmol/L", Ref = "2.8 - 7.1" },
                                new { Id = 28, Name = "TSH (US)", Unit = "uIu/ml", Ref = "0.3 - 4" },
                                new { Id = 29, Name = "FSH", Unit = "mu/ml", Ref = "1.5 - 15" },
                                new { Id = 30, Name = "LH", Unit = "mu/ml", Ref = "1.7 - 15" },
                                new { Id = 31, Name = "Prolactin", Unit = "ng/ml", Ref = "3.8 - 23" },
                                new { Id = 32, Name = "Testosterone (Men)", Unit = "ng/ml", Ref = "2.8 - 8" },
                                new { Id = 33, Name = "Testosterone (Women)", Unit = "ng/ml", Ref = "0.06 - 0.82" },
                                new { Id = 34, Name = "Ferritin (Men)", Unit = "ng/ml", Ref = "30 - 350" },
                                new { Id = 35, Name = "Platelets Count", Unit = "/mm3", Ref = "150000 - 400000" },
                                new { Id = 36, Name = "Vitamin D", Unit = "ng/ml", Ref = "25 - 80" },
                                new { Id = 37, Name = "ASLO", Unit = "Todd Units", Ref = "Up to 200" },
                                new { Id = 38, Name = "CRP", Unit = "mg/L", Ref = "Up to 6.0" },
                                new { Id = 39, Name = "RA Test", Unit = "iu/ml", Ref = "Up to 20.0" },
                                new { Id = 40, Name = "HBs Ag", Unit = "", Ref = "Negative" },
                                new { Id = 41, Name = "VDRL", Unit = "", Ref = "Negative" },
                                new { Id = 42, Name = "PSA", Unit = "ng/ml", Ref = "Less Than 4.0" },
                                new { Id = 43, Name = "CEA", Unit = "ng/ml", Ref = "Less Than 6.0" },
                                new { Id = 44, Name = "Clorine", Unit = "m.equ/L", Ref = "97 - 107" },
                                new { Id = 45, Name = "Vitamin B12", Unit = "pg/ml", Ref = "180 - 900" },
                                new { Id = 46, Name = "Estradiol (Luteal)", Unit = "pg/ml", Ref = "40 - 261" },
                                new { Id = 47, Name = "Growth Hormone (Fasting)", Unit = "ng/ml", Ref = "Up to 7.0" },
                                new { Id = 48, Name = "Insulin", Unit = "IUI/ml", Ref = "Up to 30" },
                                new { Id = 49, Name = "PTH", Unit = "pg/ml", Ref = "10 - 70" },
                                new { Id = 50, Name = "CMV Ab IgG", Unit = "u/ml", Ref = "pos more than 7" },
                                new { Id = 51, Name = "CMV Ab IgM", Unit = "Index", Ref = "pos more than 0.5" },
                                new { Id = 52, Name = "IgG", Unit = "mg/dl", Ref = "800 - 1800" },
                                new { Id = 53, Name = "IgM", Unit = "mg/dl", Ref = "60 - 350" },
                                new { Id = 54, Name = "Vitamin C", Unit = "micromol/L", Ref = "20 - 100" },
                                new { Id = 55, Name = "Vitamin E", Unit = "micromol/L", Ref = "12 - 42" },
                                new { Id = 56, Name = "Vitamin K", Unit = "nmol/L", Ref = "0.29 - 2.64" },
                                new { Id = 57, Name = "BhCG (Pregnancy)", Unit = "mIu/ml", Ref = "Above 25" },
                                new { Id = 58, Name = "HDL Cholesterol", Unit = "mg/dl", Ref = "35 - 60" },
                                new { Id = 59, Name = "Albumin", Unit = "g/dl", Ref = "3.8 - 5.1" },
                                new { Id = 60, Name = "Total Bilirubin", Unit = "mg/dl", Ref = "0.2 - 1" },
                                new { Id = 61, Name = "Direct Bilirubin", Unit = "mg/dl", Ref = "0 - 0.2" },
                                new { Id = 62, Name = "Indirect Bilirubin", Unit = "mg/dl", Ref = "0.2 - 0.8" },
                                new { Id = 63, Name = "R.B.C", Unit = "x10^6/mm3", Ref = "4.0 - 6.2" },
                                new { Id = 64, Name = "Hemoglobin", Unit = "g/dl", Ref = "12 - 17" },
                                new { Id = 65, Name = "Hematocrit", Unit = "%", Ref = "37 - 50" }
                            };

                            foreach (var test in tests)
                            {
                                insertCmd.Parameters.Clear();
                                insertCmd.Parameters.AddWithValue("@id", test.Id);
                                insertCmd.Parameters.AddWithValue("@name", test.Name);
                                insertCmd.Parameters.AddWithValue("@unit", test.Unit);
                                insertCmd.Parameters.AddWithValue("@ref", test.Ref);
                                insertCmd.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            Console.WriteLine("Successfully initialized test database with default values");
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            Console.WriteLine($"Failed to initialize test database: {ex.Message}");
                            throw;
                        }
                    }
                }
                else
                {
                    // Verify the database has the correct structure
                    using (var cmd = new SQLiteCommand("SELECT COUNT(*) FROM TestList", connection))
                    {
                        try
                        {
                            int count = Convert.ToInt32(cmd.ExecuteScalar());
                            Console.WriteLine($"Found {count} tests in existing database");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error verifying test database: {ex.Message}");
                            throw;
                        }
                    }
                }
            }
        }
    }
} 