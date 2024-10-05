using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareerHub.Model;

namespace CareerHub.Services.Interface
{
    public interface IJobsService
    {
        void AddJob(Jobs jobData);
        List<Jobs> GetAllJobs();
        void InitializeDatabase(); // Initialize the database schema and tables
        void InsertJobListing(Jobs job); // Inserts a new job listing
        List<Jobs> GetJobListings(); // Retrieves all job listings
        List<Jobs> GetJobsWithinSalaryRange(decimal minSalary, decimal maxSalary);
    }
}

