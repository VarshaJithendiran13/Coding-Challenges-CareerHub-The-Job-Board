using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareerHub.Model;

namespace CareerHub.Repository
{
    public interface ICompaniesRepository
    {
        void AddCompany(Companies company);
        List<Companies> GetAllCompanies();
        Companies GetCompanyById(int companyId);

    }

}
