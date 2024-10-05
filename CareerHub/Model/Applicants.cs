using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerHub.Model
{
    public class Applicants
    {
        public int ApplicantID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Resume { get; set; }

        public int YearsOfExperience { get; set; }



        public void CreateProfile(string email, string firstName, string lastName, string phone)
        {
            this.Email = email;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Phone = phone;
        }

        public void ApplyForJob(int jobID, string coverLetter)
        {
            // Logic for applying to a job
        }
    }

}
