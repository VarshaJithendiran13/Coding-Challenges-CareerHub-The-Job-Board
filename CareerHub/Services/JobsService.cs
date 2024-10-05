using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareerHub.Services.Interface;
using CareerHub.Exceptions;
using CareerHub.Repository;
using CareerHub.Model;

namespace CareerHub.Services
{  
    public class JobsService : IJobsService
    {
        private readonly IJobsRepository _jobRepository;

        public JobsService(IJobsRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public void AddJob(Jobs jobData)
        {
            try
            {
                // Call the repository method to insert the job, expecting it to be a void method
                _jobRepository.AddJob(jobData);
                Console.WriteLine("Job posted successfully.");
            }
            catch (DBConnectionHandling ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }


        public List<Jobs> GetAllJobs()
        {
            try
            {
                return _jobRepository.GetAllJobs();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving jobs: {ex.Message}");
                throw;
            }
        }

        public void InitializeDatabase()
        {
            try
            {
                _jobRepository.InitializeDatabase();
                Console.WriteLine("Database initialized successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing database: {ex.Message}");
                throw;
            }
        }

        public void InsertJobListing(Jobs job)
        {
            try
            {
                int result = _jobRepository.InsertJobListing(job);
                if (result > 0)
                {
                    Console.WriteLine("Job listing inserted successfully.");
                }
                else
                {
                    throw new DBConnectionHandling("Failed to insert job listing.");
                }
            }
            catch (DBConnectionHandling ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public List<Jobs> GetJobListings()
        {
            try
            {
                return _jobRepository.GetJobListings();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving job listings: {ex.Message}");
                throw;
            }
        }

        public List<Jobs> GetJobsWithinSalaryRange(decimal minSalary, decimal maxSalary)
        {
            try
            {
                // Retrieve all job listings first
                var allJobs = _jobRepository.GetJobListings();

                // Filter jobs within the specified salary range
                var filteredJobs = allJobs.FindAll(job => job.Salary >= minSalary && job.Salary <= maxSalary);

                return filteredJobs;
            }
            catch (Exception ex)
            {
                throw new DBConnectionHandling("Failed to retrieve jobs within salary range.", ex);
            }
        }
    }
}

