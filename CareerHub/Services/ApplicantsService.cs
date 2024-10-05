using CareerHub.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareerHub.Services.Interface;
using CareerHub.Exceptions;
using CareerHub.Repository;

namespace CareerHub.Services
{
    public class ApplicantsService : IApplicantsService
    {
        private readonly IApplicantsRepository _applicantRepository;

        public ApplicantsService(IApplicantsRepository applicantRepository)
        {
            _applicantRepository = applicantRepository;
        }

        public void AddApplicant(Applicants applicantData)
        {
            try
            {
                _applicantRepository.AddApplicant(applicantData); 
                Console.WriteLine("Applicant added successfully."); 
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


        public List<Applicants> GetAllApplicants()
        {
            try
            {
                return _applicantRepository.GetAllApplicants();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving applicants: {ex.Message}");
                throw;
            }
        }



        public void InitializeDatabase()
        {
            try
            {
                _applicantRepository.InitializeDatabase();
                Console.WriteLine("Database initialized successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing database: {ex.Message}");
                throw;
            }
        }

         public List<Applicants> GetApplicants()
        {
            try
            {
                return _applicantRepository.GetAllApplicants();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving applicants: {ex.Message}");
                throw;
            }
        }
    }
}
