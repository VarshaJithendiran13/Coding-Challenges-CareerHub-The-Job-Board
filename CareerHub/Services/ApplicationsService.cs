using CareerHub.Services;
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
    public class ApplicationsService : IApplicationsService
    {
        private readonly IApplicationsRepository _applicationRepository;

        public ApplicationsService(IApplicationsRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }
        public void AddApplication(Applications applicationData)
        {
            try
            {
                _applicationRepository.AddApplication(applicationData); 
                Console.WriteLine("Application submitted successfully."); 
            }
            catch (DBConnectionHandling ex) 
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }


        public List<Applications> GetAllApplications()
        {
            try
            {
                return _applicationRepository.GetAllApplications();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving applications: {ex.Message}");
                throw;
            }
        }

        public void InsertJobApplication(Applications application)
        {
            try
            {
                _applicationRepository.InsertJobApplication(application); 
                Console.WriteLine("Job application inserted successfully.");
            }
            catch (DBConnectionHandling ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }


        public List<Applications> GetApplicationsForJob(int jobID)
        {
            try
            {
                return _applicationRepository.GetApplicationsForJob(jobID);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving applications: {ex.Message}");
                throw;
            }
        }
    }
}
