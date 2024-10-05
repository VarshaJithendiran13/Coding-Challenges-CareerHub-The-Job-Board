using CareerHub.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareerHub.Model;
using CareerHub.Repository;


namespace CareerHub.Repository
{
    public class CompaniesRepository : ICompaniesRepository
    {
        private readonly SqlConnection _connection;
        private readonly SqlCommand _cmd;

        public CompaniesRepository()
        {
            _connection = new SqlConnection(DbConnUtil.GetConnString());
            _cmd = new SqlCommand();
        }

        public void AddCompany(Companies company)
        {
            string query = "INSERT INTO Companies (CompanyName, Location) VALUES (@CompanyName, @Location)";
            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                _cmd.Parameters.AddWithValue("@CompanyName", company.CompanyName);
                _cmd.Parameters.AddWithValue("@Location", company.Location);

                _connection.Open();
                _cmd.ExecuteNonQuery();
                _connection.Close();
            }
        }

        public List<Companies> GetAllCompanies()
        {
            List<Companies> companies = new List<Companies>();
            string query = "SELECT * FROM Companies";

            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    companies.Add(new Companies
                    {
                        CompanyID = (int)reader["CompanyID"],
                        CompanyName = reader["CompanyName"].ToString(),
                        Location = reader["Location"].ToString()
                    });
                }

                _connection.Close();
            }

            return companies;
        }
        public Companies GetCompanyById(int companyId)
        {
            Companies company = null;
            var query = "SELECT * FROM Companies WHERE CompanyID = @CompanyID";

            using (var command = new SqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@CompanyID", companyId);

                _connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        company = new Companies
                        {
                            CompanyID = (int)reader["CompanyID"],
                            CompanyName = (string)reader["CompanyName"],
                            Location = (string)reader["Location"]
                        };
                    }
                }
                _connection.Close();
            }

            return company;
        }
    }

}
