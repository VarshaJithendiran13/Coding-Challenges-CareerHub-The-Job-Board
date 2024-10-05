using CareerHub.Exceptions;
using CareerHub.Model;
using CareerHub.Repository;
using CareerHub.Services.Interface;
using CareerHub.Services;
using CareerHub.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerHub
{
    class MainModule

    {
        static void Main(string[] args)
        {
            // Set console colors
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            ICompaniesService companiesService = new CompaniesService(new CompaniesRepository());
            IApplicantsService applicantsService = new ApplicantsService(new ApplicantsRepository());
            IJobsService jobsService = new JobsService(new JobsRepository());
            IApplicationsService applicationsService = new ApplicationsService(new ApplicationsRepository());

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("=====================================");
                Console.WriteLine("         Job Board Menu             ");
                Console.WriteLine("=====================================");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("1. Retrieve Job Listings");
                Console.WriteLine("2. Create Applicant Profile");
                Console.WriteLine("3. Submit Job Application");
                Console.WriteLine("4. Post New Job Listing");
                Console.WriteLine("5. Search Jobs by Salary Range");
                Console.WriteLine("0. Exit");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("=====================================");
                Console.Write("Select an option: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input! Please enter a number.");
                    Console.ResetColor();
                    Console.ReadKey();
                    continue;
                }

                switch (choice)
                {
                    case 1: // Retrieve Job Listings
                        RetrieveJobListings(jobsService);
                        break;

                    case 2: // Create Applicant Profile
                        CreateApplicantProfile(applicantsService);
                        break;

                    case 3: // Submit Job Application
                        SubmitJobApplication(applicationsService);
                        break;

                    case 4: // Post New Job Listing
                        PostNewJobListing(jobsService);
                        break;

                    case 5: // Search Jobs by Salary Range
                        SearchJobsBySalaryRange(jobsService);
                        break;

                    case 0: // Exit
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Exiting the application. Goodbye!");
                        Console.ResetColor();
                        return;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice! Please select a valid option.");
                        Console.ResetColor();
                        break;
                }

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("\nPress any key to return to the menu...");
                Console.ResetColor();
                Console.ReadKey();
            }
        }

        static void RetrieveJobListings(IJobsService jobsService)
        {
            try
            {
                Console.WriteLine("\nRetrieving job listings...");
                var jobListings = jobsService.GetAllJobs();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Job Listings:");
                Console.WriteLine("--------------------------------------------------------");
                Console.WriteLine($"{"Job Title",-30} {"Company ID",-15} {"Salary",-10}");
                Console.WriteLine("--------------------------------------------------------");
                foreach (var job in jobListings)
                {
                    Console.WriteLine($"{job.JobTitle,-30} {job.CompanyID,-15} {job.Salary,-10}");
                }
                Console.WriteLine("--------------------------------------------------------");
            }
            catch (DBConnectionHandling ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Database error: {ex.Message}");
                Console.ResetColor();
            }
        }

        static void CreateApplicantProfile(IApplicantsService applicantsService)
        {
            try
            {
                Console.WriteLine("\nCreating applicant profile...");
                Console.Write("Enter First Name: ");
                string firstName = Console.ReadLine();
                Console.Write("Enter Last Name: ");
                string lastName = Console.ReadLine();
                Console.Write("Enter Email: ");
                string email = Console.ReadLine();
                ValidateEmail(email); // Validate email

                Console.Write("Enter Phone: ");
                string phone = Console.ReadLine();

                // Collect resume file path
                Console.Write("Enter Resume Path (PDF file): ");
                string resumePath = Console.ReadLine();
                ValidatePdfFile(resumePath); // Validate resume file path

                var applicant = new Applicants
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Phone = phone,
                    Resume = resumePath // Store file path in the applicant object
                };

                applicantsService.AddApplicant(applicant);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Applicant profile created successfully.");
                Console.ResetColor();
            }
            catch (FormatException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }
            catch (DBConnectionHandling ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Database error: {ex.Message}");
                Console.ResetColor();
            }
        }


        static void SubmitJobApplication(IApplicationsService applicationsService)
        {
            try
            {
                Console.WriteLine("\nSubmitting job application...");
                Console.Write("Enter Job ID: ");
                int jobId = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter Applicant ID: ");
                int applicantId = Convert.ToInt32(Console.ReadLine());

                // Collect cover letter path
                Console.Write("Enter Cover Letter Path (PDF file): ");
                string coverLetterPath = Console.ReadLine();
                ValidatePdfFile(coverLetterPath); // Validate cover letter file path


                var application = new Applications
                {
                    JobID = jobId,
                    ApplicantID = applicantId,
                    CoverLetter = coverLetterPath,
                    ApplicationDate = DateTime.Now
                };

                applicationsService.AddApplication(application);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Job application submitted successfully.");
                Console.ResetColor();
            }
            catch (DBConnectionHandling ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Database error: {ex.Message}");
                Console.ResetColor();
            }
            catch (FormatException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }
        }

        static void ValidatePdfFile(string filePath)
        {
            if (!System.IO.File.Exists(filePath) || System.IO.Path.GetExtension(filePath).ToLower() != ".pdf")
            {
                throw new FormatException("File does not exist or is not a PDF.");
            }
        }

        static void PostNewJobListing(IJobsService jobsService)
        {
            try
            {
                Console.WriteLine("\nPosting new job...");
                Console.Write("Enter Company ID: ");
                int companyId = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter Job Title: ");
                string jobTitle = Console.ReadLine();
                Console.Write("Enter Job Description: ");
                string jobDescription = Console.ReadLine();
                Console.Write("Enter Job Location: ");
                string jobLocation = Console.ReadLine();
                Console.Write("Enter Salary: ");
                decimal salary = Convert.ToDecimal(Console.ReadLine());
                Console.Write("Enter Job Type: ");
                string jobType = Console.ReadLine();

                var job = new Jobs { CompanyID = companyId, JobTitle = jobTitle, JobDescription = jobDescription, JobLocation = jobLocation, Salary = salary, JobType = jobType, PostedDate = DateTime.Now };
                jobsService.AddJob(job);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Job posted successfully.");
                Console.ResetColor();
            }
            catch (DBConnectionHandling ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Database error: {ex.Message}");
                Console.ResetColor();
            }
        }

        static void SearchJobsBySalaryRange(IJobsService jobsService)
        {
            try
            {
                Console.WriteLine("\nSearching for jobs within a salary range...");
                Console.Write("Enter Minimum Salary: ");
                decimal minSalary = Convert.ToDecimal(Console.ReadLine());
                Console.Write("Enter Maximum Salary: ");
                decimal maxSalary = Convert.ToDecimal(Console.ReadLine());

                var matchingJobs = jobsService.GetJobsWithinSalaryRange(minSalary, maxSalary);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Matching Job Listings:");
                Console.WriteLine("--------------------------------------------------------");
                Console.WriteLine($"{"Job Title",-30} {"Company ID",-15} {"Salary",-10}");
                Console.WriteLine("--------------------------------------------------------");
                foreach (var job in matchingJobs)
                {
                    Console.WriteLine($"{job.JobTitle,-30} {job.CompanyID,-15} {job.Salary,-10}");
                }
                Console.WriteLine("--------------------------------------------------------");
            }
            catch (DBConnectionHandling ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Database error: {ex.Message}");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.ResetColor();
            }
        }

        static void ValidateEmail(string email)
        {
            if (!email.Contains("@"))
            {
                throw new FormatException("Invalid Email Format");
            }
        }
    }
}
