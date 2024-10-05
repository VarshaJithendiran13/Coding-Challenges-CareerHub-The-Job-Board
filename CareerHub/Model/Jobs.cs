using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerHub.Model
{
    public class Jobs
    {
        public int JobID { get; set; }
        public int CompanyID { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string JobLocation { get; set; }
        public decimal Salary { get; set; }
        public string JobType { get; set; }
        public DateTime PostedDate { get; set; }

        public List<int> Applicants { get; set; } = new List<int>();

        public void Apply(int applicantID, string coverLetter)
        {
            // Logic to apply for a job
            Applicants.Add(applicantID);
        }

        public List<int> GetApplicants()
        {
            return Applicants;
        }
    }

}
