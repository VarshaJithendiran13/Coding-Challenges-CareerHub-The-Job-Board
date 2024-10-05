using CareerHub.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareerHub.Utilities;
using CareerHub.Repository;

namespace CareerHub.Repository
{
    public class ApplicantsRepository : IApplicantsRepository
    {
        private readonly SqlConnection _connection;
        private readonly SqlCommand _cmd;

        public ApplicantsRepository()
        {
            _connection = new SqlConnection(DbConnUtil.GetConnString());
            _cmd = new SqlCommand();
        }

        public void AddApplicant(Applicants applicant)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("INSERT INTO Applicants ");
            queryBuilder.Append("(FirstName, LastName, Email, Phone, YearsOfExperience, Resume) ");
            queryBuilder.Append("VALUES (@FirstName, @LastName, @Email, @Phone,@YearsOfExperience, @Resume)");

            string query = queryBuilder.ToString();
            using (SqlCommand _cmd = new SqlCommand(query, _connection))
            {
                _cmd.Parameters.AddWithValue("@FirstName", applicant.FirstName);
                _cmd.Parameters.AddWithValue("@LastName", applicant.LastName);
                _cmd.Parameters.AddWithValue("@Email", applicant.Email);
                _cmd.Parameters.AddWithValue("@Phone", applicant.Phone);
                _cmd.Parameters.AddWithValue("@YearsOfExperience", applicant.YearsOfExperience);
                _cmd.Parameters.AddWithValue("@Resume", applicant.Resume);
                

                _connection.Open();
                _cmd.ExecuteNonQuery();
                _connection.Close();
            }
        }

        public List<Applicants> GetAllApplicants()
        {
            List<Applicants> applicants = new List<Applicants>();
            string query = "SELECT * FROM Applicants";

            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    applicants.Add(new Applicants
                    {
                        ApplicantID = (int)reader["ApplicantID"],
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Email = reader["Email"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        YearsOfExperience = (int)reader["YearsOfExperience"],
                        Resume = reader["Resume"].ToString()
                    });
                }

                _connection.Close();
            }

            return applicants;

        }
        public void InitializeDatabase()
        {
            try
            {
                string createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS Applicants (
                        ApplicantID INT PRIMARY KEY IDENTITY(1,1),
                        FirstName NVARCHAR(50) NOT NULL,
                        LastName NVARCHAR(50) NOT NULL,
                        Email NVARCHAR(100) NOT NULL,
                        Phone NVARCHAR(15) NOT NULL,
                        Resume NVARCHAR(MAX) NULL
                    );";

                using (SqlCommand command = new SqlCommand(createTableQuery, _connection))
                {
                    _cmd.Connection.Open();
                    _cmd.ExecuteNonQuery();
                    Console.WriteLine("Applicants table created successfully.");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                throw; // Optionally rethrow or handle it as needed
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw; // Optionally rethrow or handle it as needed
            }
            finally
            {
                // Ensure the connection is closed if it was opened here
                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
        }
    }

}

