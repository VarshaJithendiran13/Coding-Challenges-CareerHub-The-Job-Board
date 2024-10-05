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
    public class CompaniesService : ICompaniesService
    {
        private readonly ICompaniesRepository _companiesRepository; 

        // Constructor
        public CompaniesService(ICompaniesRepository companiesRepository)
        {
            _companiesRepository = companiesRepository;
        }

        public void AddCompany(Companies company)
        {
            try
            {
                _companiesRepository.AddCompany(company); // Insert company into the repository
                Console.WriteLine("Company added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding company: {ex.Message}");
                throw new DBConnectionHandling("Failed to add the company.", ex); // Custom exception for database operations
            }
        }

        public List<Companies> GetAllCompanies()
        {
            try
            {
                return _companiesRepository.GetAllCompanies(); // Retrieve all companies from the repository
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving companies: {ex.Message}");
                throw new DBConnectionHandling("Failed to retrieve companies.", ex); // Custom exception for database operations
            }
        }

        public Companies GetCompanyById(int companyId)
        {
            try
            {
                var company = _companiesRepository.GetCompanyById(companyId); // Get company by ID from the repository
                if (company == null)
                {
                    throw new RecordNotFoundException("Company not found."); // Custom exception for record not found
                }
                return company;
            }
            catch (RecordNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null; // Returning null if company is not found
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving company: {ex.Message}");
                throw new DBConnectionHandling("Failed to retrieve the company.", ex); // Custom exception for database operations
            }
        }
    }
}

