using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerHub.Model
{
    public class Companies
    {
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string Location { get; set; }

        public List<Jobs> Jobs { get; set; } = new List<Jobs>();

        public void PostJob(string jobTitle, string jobDescription, string jobLocation, decimal salary, string jobType)
        {
            // Logic to post a new job
            Jobs newJob = new Jobs()
            {
                JobID = Jobs.Count + 1,
                CompanyID = this.CompanyID,
                JobTitle = jobTitle,
                JobDescription = jobDescription,
                JobLocation = jobLocation,
                Salary = salary,
                JobType = jobType,
                PostedDate = DateTime.Now
            };
            Jobs.Add(newJob);
        }

        public List<Jobs> GetJobs()
        {
            return Jobs;
        }
    }

}
