using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareerHub.Model;
using CareerHub.Utilities;
using CareerHub.Repository;
using CareerHub.Exceptions;

namespace CareerHub.Repository 
{
    public class ApplicationsRepository :IApplicationsRepository
    {
        private readonly SqlConnection _connection;
        private readonly SqlCommand _cmd;

        public ApplicationsRepository()
        {
            _connection = new SqlConnection(DbConnUtil.GetConnString());
            _cmd = new SqlCommand();
        }

        public void AddApplication(Applications application)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("INSERT INTO Applications ");
            queryBuilder.Append("(JobID, ApplicantID, ApplicationDate, CoverLetter) ");
            queryBuilder.Append("VALUES (@JobID, @ApplicantID, @ApplicationDate, @CoverLetter)");

            string query = queryBuilder.ToString();
            using (SqlCommand _cmd = new SqlCommand(query, _connection))
            {
                _cmd.Parameters.AddWithValue("@JobID", application.JobID);
                _cmd.Parameters.AddWithValue("@ApplicantID", application.ApplicantID);
                _cmd.Parameters.AddWithValue("@ApplicationDate", application.ApplicationDate);
                _cmd.Parameters.AddWithValue("@CoverLetter", application.CoverLetter);

                _connection.Open();
                _cmd.ExecuteNonQuery();
                _connection.Close();
            }
        }

        public List<Applications> GetApplicationsForJob(int jobID)
        {
            List<Applications> applications = new List<Applications>();
            string query = "SELECT * FROM Applications WHERE JobID = @JobID";

            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                _cmd.Parameters.AddWithValue("@JobID", jobID);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    applications.Add(new Applications
                    {
                        ApplicationID = (int)reader["ApplicationID"],
                        JobID = (int)reader["JobID"],
                        ApplicantID = (int)reader["ApplicantID"],
                        ApplicationDate = (DateTime)reader["ApplicationDate"],
                        CoverLetter = reader["CoverLetter"].ToString()
                    });
                }

                _connection.Close();
            }

            return applications;
        }

        public List<Applications> GetAllApplications()
        {
            List<Applications> applications = new List<Applications>();
            string query = "SELECT * FROM Applications"; // Adjust as per your table schema

            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                try
                {
                    _connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Assuming your Applications class has the following properties
                            Applications application = new Applications
                            {
                                ApplicationID = reader.GetInt32(reader.GetOrdinal("ApplicationID")),
                                JobID = reader.GetInt32(reader.GetOrdinal("JobID")),
                                ApplicantID = reader.GetInt32(reader.GetOrdinal("ApplicantID")),
                                ApplicationDate = reader.GetDateTime(reader.GetOrdinal("ApplicationDate")),
                                CoverLetter = reader.GetString(reader.GetOrdinal("CoverLetter")),
                            };
                            applications.Add(application);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new DBConnectionHandling($"Error retrieving applications: {ex.Message}");
                }
                finally
                {
                    _connection.Close();
                }
            }
            return applications;
        }

        public void InsertJobApplication(Applications application)
        {
            string query = "INSERT INTO Applications (JobID, ApplicantID, ApplicationDate, CoverLetter) VALUES (@JobID, @ApplicantID, @ApplicationDate, @CoverLetter)";

            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                _cmd.Parameters.AddWithValue("@JobID", application.JobID);
                _cmd.Parameters.AddWithValue("@ApplicantID", application.ApplicantID);
                _cmd.Parameters.AddWithValue("@ApplicationDate", application.ApplicationDate);
                _cmd.Parameters.AddWithValue("@CoverLetter", application.CoverLetter);

                try
                {
                    _connection.Open();
                    int affectedRows = _cmd.ExecuteNonQuery(); // Execute the query

                    if (affectedRows <= 0)
                    {
                        throw new DBConnectionHandling("Failed to insert job application.");
                    }
                }
                catch (Exception ex)
                {
                    throw new DBConnectionHandling($"Database error: {ex.Message}");
                }
                finally
                {
                    _connection.Close(); // Ensure the connection is closed
                }
            }
        }
    }

}
