using CareerHub.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareerHub.Model;
using CareerHub.Repository;
using CareerHub.Exceptions;

namespace CareerHub.Repository
{
    public class JobsRepository : IJobsRepository
    {
        private readonly SqlConnection _connection;
        private readonly SqlCommand _cmd;

        public JobsRepository()
        {
            _connection = new SqlConnection(DbConnUtil.GetConnString());
            _cmd = new SqlCommand();
        }

        public void AddJob(Jobs job)
        {
            string query = "INSERT INTO Jobs (CompanyID, JobTitle, JobDescription, JobLocation, Salary, JobType, PostedDate) VALUES (@CompanyID, @JobTitle, @JobDescription, @JobLocation, @Salary, @JobType, @PostedDate)";
            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                _cmd.Parameters.AddWithValue("@CompanyID", job.CompanyID);
                _cmd.Parameters.AddWithValue("@JobTitle", job.JobTitle);
                _cmd.Parameters.AddWithValue("@JobDescription", job.JobDescription);
                _cmd.Parameters.AddWithValue("@JobLocation", job.JobLocation);
                _cmd.Parameters.AddWithValue("@Salary", job.Salary);
                _cmd.Parameters.AddWithValue("@JobType", job.JobType);
                _cmd.Parameters.AddWithValue("@PostedDate", job.PostedDate);

                _connection.Open();
                _cmd.ExecuteNonQuery();
                _connection.Close();
            }
        }

        public List<Jobs> GetAllJobs()
        {
            List<Jobs> jobs = new List<Jobs>();
            string query = "SELECT * FROM Jobs";

            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    jobs.Add(new Jobs
                    {
                        JobID = (int)reader["JobID"],
                        CompanyID = (int)reader["CompanyID"],
                        JobTitle = reader["JobTitle"].ToString(),
                        JobDescription = reader["JobDescription"].ToString(),
                        JobLocation = reader["JobLocation"].ToString(),
                        Salary = (decimal)reader["Salary"],
                        JobType = reader["JobType"].ToString(),
                        PostedDate = (DateTime)reader["PostedDate"]
                    });
                }

                _connection.Close();
            }

            return jobs;
        }

        public void InitializeDatabase()
        {
            try
            {
                // Code to create the Jobs table if it doesn't exist
                string createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS Jobs (
                        JobID INT PRIMARY KEY IDENTITY(1,1),
                        CompanyID INT,
                        JobTitle NVARCHAR(100),
                        JobDescription NVARCHAR(MAX),
                        JobLocation NVARCHAR(100),
                        Salary DECIMAL(18,2),
                        JobType NVARCHAR(50),
                        PostedDate DATETIME DEFAULT GETDATE()
                    );";

                using (SqlCommand command = new SqlCommand(createTableQuery, _connection))
                {
                    _connection.Open();
                    command.ExecuteNonQuery();
                }
                Console.WriteLine("Jobs table initialized successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing Jobs table: {ex.Message}");
                throw;
            }
            finally
            {
                _connection.Close();
            }
        }

        public int InsertJobListing(Jobs job)
        {
            try
            {
                string query = @"
                    INSERT INTO Jobs (CompanyID, JobTitle, JobDescription, JobLocation, Salary, JobType)
                    VALUES (@CompanyID, @JobTitle, @JobDescription, @JobLocation, @Salary, @JobType);
                    
                    SELECT SCOPE_IDENTITY();"; // Get the inserted JobID

                using (SqlCommand command = new SqlCommand(query, _connection))
                {
                    _cmd.Parameters.AddWithValue("@CompanyID", job.CompanyID);
                    _cmd.Parameters.AddWithValue("@JobTitle", job.JobTitle);
                    _cmd.Parameters.AddWithValue("@JobDescription", job.JobDescription);
                    _cmd.Parameters.AddWithValue("@JobLocation", job.JobLocation);
                    _cmd.Parameters.AddWithValue("@Salary", job.Salary);
                    _cmd.Parameters.AddWithValue("@JobType", job.JobType);

                    _connection.Open();
                    int insertedId = Convert.ToInt32(command.ExecuteScalar()); // Execute the query and get the inserted JobID
                    return insertedId; // Return the inserted JobID
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting job listing: {ex.Message}");
                throw new DBConnectionHandling("Failed to insert job listing.", ex);
            }
            finally
            {
                _connection.Close();
            }
        }

        public List<Jobs> GetJobListings()
        {
            List<Jobs> jobListings = new List<Jobs>();
            try
            {
                string query = "SELECT * FROM Jobs;";
                using (SqlCommand command = new SqlCommand(query, _connection))
                {
                    _connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Jobs job = new Jobs
                            {
                                JobID = (int)reader["JobID"],
                                CompanyID = (int)reader["CompanyID"],
                                JobTitle = reader["JobTitle"].ToString(),
                                JobDescription = reader["JobDescription"].ToString(),
                                JobLocation = reader["JobLocation"].ToString(),
                                Salary = (decimal)reader["Salary"],
                                JobType = reader["JobType"].ToString(),
                                PostedDate = (DateTime)reader["PostedDate"]
                            };
                            jobListings.Add(job);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving job listings: {ex.Message}");
                throw;
            }
            finally
            {
                _connection.Close();
            }

            return jobListings;
        }
    }
}

