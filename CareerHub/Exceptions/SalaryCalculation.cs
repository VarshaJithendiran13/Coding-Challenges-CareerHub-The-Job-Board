using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareerHub.Model;

namespace CareerHub.Exceptions
{
    public class SalaryCalculation
    {
        public decimal CalculateAverageSalary(List<Jobs> jobListings)
        {
            if (jobListings.Any(j => j.Salary < 0))
            {
                throw new ArgumentException("Invalid salary value detected");
            }

            return jobListings.Average(j => j.Salary);
        }

    }
}
