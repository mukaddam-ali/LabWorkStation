using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace Lab
{
    public class Test
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Unit { get; set; }
        public required string ReferenceRange { get; set; }
    }

    public class Patient
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string VisitDate { get; set; }
    }

    public class PatientTest
    {
        public int TestId { get; set; }
        public string TestName { get; set; }
        public string Value { get; set; }
        public string Unit { get; set; }
        public string ReferenceRange { get; set; }
    }

    public static class DatabaseHelper
    {
        private static string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
        private static string connectionString = $"Data Source={Path.Combine(appDirectory, "patients.db")}";
        private static string testListDbPath = Path.GetFullPath(Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "Test List",
            "Tests.db"
        ));
        private static string testListDbConnStr = $"Data Source={testListDbPath}";

        static DatabaseHelper()
        {
            try
            {
                // Ensure the database is initialized when the class is first used
                Console.WriteLine($"Initializing database...");
                Console.WriteLine($"App Directory: {appDirectory}");
                Console.WriteLine($"Patients DB Path: {connectionString}");
                Console.WriteLine($"Test List DB Path: {testListDbPath}");
                
                InitializeDatabase();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to initialize database: {ex.Message}\n\nDetails:\nApp Directory: {appDirectory}\nPatients DB: {connectionString}\nTest List DB: {testListDbPath}", 
                    "Database Initialization Error", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
        }

        public static string GetConnectionString()
        {
            return connectionString;
        }

        public static void InitializeDatabase()
        {
            // Create patients.db if it doesn't exist
            string dbPath = Path.Combine(appDirectory, "patients.db");
            if (!File.Exists(dbPath))
            {
                SQLiteConnection.CreateFile(dbPath);
                Console.WriteLine($"Created new patients database at: {dbPath}");
            }

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Create Patients table
                        using (var cmd = new SQLiteCommand(@"
                            CREATE TABLE IF NOT EXISTS Patients (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                FullName TEXT NOT NULL,
                                VisitDate TEXT NOT NULL
                            )", conn))
                        {
                            cmd.ExecuteNonQuery();
                            Console.WriteLine("Created Patients table");
                        }

                        // Create Tests table
                        using (var cmd = new SQLiteCommand(@"
                            CREATE TABLE IF NOT EXISTS Tests (
                                Id INTEGER PRIMARY KEY,
                                Name TEXT NOT NULL,
                                Unit TEXT,
                                ReferenceRange TEXT
                            )", conn))
                        {
                            cmd.ExecuteNonQuery();
                            Console.WriteLine("Created Tests table");
                        }

                        // Create PatientTests table with foreign keys
                        using (var cmd = new SQLiteCommand(@"
                            CREATE TABLE IF NOT EXISTS PatientTests (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                PatientId INTEGER NOT NULL,
                                TestId INTEGER NOT NULL,
                                Value TEXT NOT NULL,
                                Unit TEXT NOT NULL,
                                ReferenceRange TEXT NOT NULL,
                                FOREIGN KEY (PatientId) REFERENCES Patients(Id) ON DELETE CASCADE,
                                FOREIGN KEY (TestId) REFERENCES Tests(Id)
                            )", conn))
                        {
                            cmd.ExecuteNonQuery();
                            Console.WriteLine("Created PatientTests table");
                        }

                        // Enable foreign key support
                        using (var cmd = new SQLiteCommand("PRAGMA foreign_keys = ON;", conn))
                        {
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        Console.WriteLine("Database schema created successfully");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception($"Failed to create database schema: {ex.Message}", ex);
                    }
                }

                // Import tests if Tests table is empty
                using (var cmd = new SQLiteCommand("SELECT COUNT(*) FROM Tests", conn))
                {
                    int testCount = Convert.ToInt32(cmd.ExecuteScalar());
                    if (testCount == 0)
                    {
                        ImportTestsFromTestList(conn);
                    }
                    else
                    {
                        Console.WriteLine($"Found {testCount} existing tests in database");
                    }
                }
            }
        }

        private static void ImportTestsFromTestList(SQLiteConnection conn)
        {
            try
            {
                Console.WriteLine("Starting test import...");
                if (!File.Exists(testListDbPath))
                {
                    throw new FileNotFoundException($"Test List database not found at: {testListDbPath}");
                }

                using var testListConn = new SQLiteConnection(testListDbConnStr);
                testListConn.Open();
                
                // Get all tests from TestList
                using var cmdGet = new SQLiteCommand("SELECT Id, Name, Unit, Ref FROM TestList", testListConn);
                using var reader = cmdGet.ExecuteReader();
                
                int importCount = 0;
                using var transaction = conn.BeginTransaction();
                try
                {
                    // Insert tests into Tests table
                    using var cmdInsert = new SQLiteCommand(@"
                        INSERT INTO Tests (Id, Name, Unit, ReferenceRange)
                        VALUES (@id, @name, @unit, @ref)", conn);
                    
                    while (reader.Read())
                    {
                        cmdInsert.Parameters.Clear();
                        cmdInsert.Parameters.AddWithValue("@id", reader.GetInt32(0));
                        cmdInsert.Parameters.AddWithValue("@name", reader.GetString(1));
                        cmdInsert.Parameters.AddWithValue("@unit", reader.GetString(2));
                        cmdInsert.Parameters.AddWithValue("@ref", reader.GetString(3));
                        cmdInsert.ExecuteNonQuery();
                        importCount++;
                    }
                    
                    transaction.Commit();
                    Console.WriteLine($"Successfully imported {importCount} tests");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception($"Failed to import tests: {ex.Message}", ex);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error importing tests: {ex.Message}\n\nTest List DB Path: {testListDbPath}", ex);
            }
        }

        public static List<Test> GetAllTests()
        {
            var tests = new List<Test>();

            try
            {
                using (var conn = new SQLiteConnection(testListDbConnStr))
                {
                    conn.Open();
                    using (var cmd = new SQLiteCommand("SELECT * FROM TestList ORDER BY Id", conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tests.Add(new Test
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"]?.ToString() ?? string.Empty,
                                Unit = reader["Unit"]?.ToString() ?? string.Empty,
                                ReferenceRange = reader["Ref"]?.ToString() ?? string.Empty
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tests: {ex.Message}\nDatabase path: {testListDbPath}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return tests;
        }

        public static long InsertPatient(string name, DateTime date)
        {
            try
            {
                using var conn = new SQLiteConnection(connectionString);
                conn.Open();
                using var transaction = conn.BeginTransaction();
                try
                {
                    var cmd = new SQLiteCommand(@"
                        INSERT INTO Patients (FullName, VisitDate) 
                        VALUES (@name, @date);
                        SELECT last_insert_rowid();", conn, transaction);
                    
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-dd"));
                    
                    long patientId = (long)cmd.ExecuteScalar();
                    transaction.Commit();
                    return patientId;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inserting patient: {ex.Message}", ex);
            }
        }

        public static void InsertPatientTest(long patientId, int testId, string value, string unit, string referenceRange)
        {
            try
            {
                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    using var transaction = conn.BeginTransaction();
                    try
                    {
                        // Verify the test exists in Tests table
                        var cmdCheckTest = new SQLiteCommand(@"
                            SELECT COUNT(*) FROM Tests WHERE Id = @tid", conn, transaction);
                        cmdCheckTest.Parameters.AddWithValue("@tid", testId);
                        int testExists = Convert.ToInt32(cmdCheckTest.ExecuteScalar());

                        if (testExists == 0)
                        {
                            // Get test details from TestList and insert into Tests
                            using var testListConn = new SQLiteConnection(testListDbConnStr);
                            testListConn.Open();
                            using var cmdGetTest = new SQLiteCommand(@"
                                SELECT Name, Unit, Ref 
                                FROM TestList 
                                WHERE Id = @tid", testListConn);
                            cmdGetTest.Parameters.AddWithValue("@tid", testId);
                            
                            using var reader = cmdGetTest.ExecuteReader();
                            if (reader.Read())
                            {
                                var cmdInsertTest = new SQLiteCommand(@"
                                    INSERT INTO Tests (Id, Name, Unit, ReferenceRange)
                                    VALUES (@tid, @name, @unit, @ref)", conn, transaction);
                                cmdInsertTest.Parameters.AddWithValue("@tid", testId);
                                cmdInsertTest.Parameters.AddWithValue("@name", reader["Name"].ToString());
                                cmdInsertTest.Parameters.AddWithValue("@unit", reader["Unit"].ToString());
                                cmdInsertTest.Parameters.AddWithValue("@ref", reader["Ref"].ToString());
                                cmdInsertTest.ExecuteNonQuery();
                            }
                            else
                            {
                                throw new Exception($"Test with ID {testId} not found in TestList database");
                            }
                        }

                        // Insert the patient test result
                        var cmdInsertPatientTest = new SQLiteCommand(@"
                            INSERT INTO PatientTests (PatientId, TestId, Value, Unit, ReferenceRange) 
                            VALUES (@pid, @tid, @val, @unit, @ref)", conn, transaction);
                        
                        cmdInsertPatientTest.Parameters.AddWithValue("@pid", patientId);
                        cmdInsertPatientTest.Parameters.AddWithValue("@tid", testId);
                        cmdInsertPatientTest.Parameters.AddWithValue("@val", value);
                        cmdInsertPatientTest.Parameters.AddWithValue("@unit", unit);
                        cmdInsertPatientTest.Parameters.AddWithValue("@ref", referenceRange);
                        cmdInsertPatientTest.ExecuteNonQuery();

                        Console.WriteLine($"Inserted test for patient {patientId}: TestId={testId}, Value={value}");
                        
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception($"Database operation failed: {ex.Message}", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error saving test result. PatientId: {patientId}, TestId: {testId}, Error: {ex.Message}", ex);
            }
        }

        public static List<Patient> SearchPatients(string searchTerm = "")
        {
            var patients = new List<Patient>();
            try
            {
                using var conn = new SQLiteConnection(connectionString);
                conn.Open();
                
                string sql = @"SELECT Id, FullName, VisitDate FROM Patients 
                             WHERE FullName LIKE @search 
                             ORDER BY VisitDate DESC";
                
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@search", $"%{searchTerm}%");
                
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    patients.Add(new Patient
                    {
                        Id = Convert.ToInt64(reader["Id"]),
                        FullName = reader["FullName"].ToString() ?? string.Empty,
                        VisitDate = reader["VisitDate"].ToString() ?? string.Empty
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching patients: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            return patients;
        }

        public static List<Patient> GetAllPatients()
        {
            return SearchPatients("");
        }

        public static List<PatientTest> GetPatientTests(long patientId)
        {
            var tests = new List<PatientTest>();
            try
            {
                using var conn = new SQLiteConnection(connectionString);
                conn.Open();

                // Debug: Print all tables content
                Console.WriteLine("\nDebugging all tables content:");
                
                // Debug: Print Tests table content
                using (var cmd = new SQLiteCommand("SELECT * FROM Tests", conn))
                {
                    using var reader = cmd.ExecuteReader();
                    Console.WriteLine("\nTests table content:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"Test ID: {reader["Id"]}, Name: {reader["Name"]}, Unit: {reader["Unit"]}, Range: {reader["ReferenceRange"]}");
                    }
                }

                // Debug: Print PatientTests table content for this patient
                using (var cmd = new SQLiteCommand("SELECT * FROM PatientTests WHERE PatientId = @pid", conn))
                {
                    cmd.Parameters.AddWithValue("@pid", patientId);
                    using var reader = cmd.ExecuteReader();
                    Console.WriteLine($"\nPatientTests table content for patient {patientId}:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"Test ID: {reader["TestId"]}, Value: {reader["Value"]}, Unit: {reader["Unit"]}, Range: {reader["ReferenceRange"]}");
                    }
                }

                // Get test details with explicit columns and joins
                string sql = @"
                    SELECT 
                        pt.TestId,
                        t.Name as TestName,
                        pt.Value, 
                        pt.Unit, 
                        pt.ReferenceRange
                    FROM PatientTests pt
                    INNER JOIN Tests t ON t.Id = pt.TestId
                    WHERE pt.PatientId = @pid
                    ORDER BY t.Name";
                
                using var cmdTests = new SQLiteCommand(sql, conn);
                cmdTests.Parameters.AddWithValue("@pid", patientId);
                
                using var testReader = cmdTests.ExecuteReader();
                while (testReader.Read())
                {
                    var test = new PatientTest
                    {
                        TestId = Convert.ToInt32(testReader["TestId"]),
                        TestName = testReader["TestName"]?.ToString() ?? string.Empty,
                        Value = testReader["Value"]?.ToString() ?? string.Empty,
                        Unit = testReader["Unit"]?.ToString() ?? string.Empty,
                        ReferenceRange = testReader["ReferenceRange"]?.ToString() ?? string.Empty
                    };
                    tests.Add(test);
                    Console.WriteLine($"Retrieved test: ID={test.TestId}, Name={test.TestName}, Value={test.Value}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error getting patient tests: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            return tests;
        }

        public static bool DeletePatient(long patientId)
        {
            try
            {
                using var conn = new SQLiteConnection(connectionString);
                conn.Open();
                
                using var transaction = conn.BeginTransaction();
                try
                {
                    // First verify the patient exists
                    using (var cmdCheck = new SQLiteCommand(
                        "SELECT FullName FROM Patients WHERE Id = @pid", conn, transaction))
                    {
                        cmdCheck.Parameters.AddWithValue("@pid", patientId);
                        var patientName = cmdCheck.ExecuteScalar()?.ToString();
                        
                        if (string.IsNullOrEmpty(patientName))
                        {
                            throw new Exception("Patient not found");
                        }
                    }

                    // Delete the patient (PatientTests will be deleted automatically due to CASCADE)
                    using (var cmdDelete = new SQLiteCommand(
                        "DELETE FROM Patients WHERE Id = @pid", conn, transaction))
                    {
                        cmdDelete.Parameters.AddWithValue("@pid", patientId);
                        int rowsAffected = cmdDelete.ExecuteNonQuery();
                        
                        if (rowsAffected == 0)
                        {
                            throw new Exception("Failed to delete patient");
                        }
                    }

                    transaction.Commit();
                    Console.WriteLine($"Successfully deleted patient {patientId} and their test results");
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception($"Failed to delete patient: {ex.Message}", ex);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error deleting patient: {ex.Message}",
                    "Delete Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return false;
            }
        }

        public static bool DeletePatientTest(long patientId, int testId)
        {
            try
            {
                using var conn = new SQLiteConnection(connectionString);
                conn.Open();
                
                using var transaction = conn.BeginTransaction();
                try
                {
                    using var cmdDelete = new SQLiteCommand(
                        "DELETE FROM PatientTests WHERE PatientId = @pid AND TestId = @tid",
                        conn, transaction);
                    
                    cmdDelete.Parameters.AddWithValue("@pid", patientId);
                    cmdDelete.Parameters.AddWithValue("@tid", testId);
                    
                    int rowsAffected = cmdDelete.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new Exception("Test not found");
                    }
                    
                    transaction.Commit();
                    Console.WriteLine($"Successfully deleted test {testId} for patient {patientId}");
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception($"Failed to delete test: {ex.Message}", ex);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error deleting test: {ex.Message}",
                    "Delete Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return false;
            }
        }
    }
}
