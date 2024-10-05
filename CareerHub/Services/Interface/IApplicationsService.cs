using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareerHub.Model;


namespace CareerHub.Services.Interface
{
    public interface IApplicationsService
    {
        void AddApplication(Applications applicationData);
        List<Applications> GetAllApplications();
        void InsertJobApplication(Applications application); 
        List<Applications> GetApplicationsForJob(int jobID); 
    }
}

