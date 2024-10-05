using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareerHub.Model;

namespace CareerHub.Repository
{
    public interface IJobsRepository
    {
        void AddJob(Jobs job);
        List<Jobs> GetAllJobs();
        int InsertJobListing(Jobs job);
        List<Jobs> GetJobListings();
        void InitializeDatabase();
        


    }
}
