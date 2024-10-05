using CareerHub.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareerHub.Repository;

namespace CareerHub.Services.Interface
{
    public interface IApplicantsService
        {
            void AddApplicant(Applicants applicantData);
            List<Applicants> GetAllApplicants();
            void InitializeDatabase(); // Initialize the database schema and tables
            List<Applicants> GetApplicants(); // Retrieves a list of all applicants
        }
    }
