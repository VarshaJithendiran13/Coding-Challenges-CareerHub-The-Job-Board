using CareerHub.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerHub.Repository
{
        public interface IApplicantsRepository
        {
            void AddApplicant(Applicants applicant);
            List<Applicants> GetAllApplicants();
            void InitializeDatabase();

    }

}
