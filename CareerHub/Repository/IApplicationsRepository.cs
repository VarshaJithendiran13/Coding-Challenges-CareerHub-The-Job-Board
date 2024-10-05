using CareerHub.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerHub.Repository
{
    public interface IApplicationsRepository
    {
        void AddApplication(Applications application);
        List<Applications> GetApplicationsForJob(int jobID);
        List<Applications> GetAllApplications();
        void InsertJobApplication(Applications application);
    }

}
