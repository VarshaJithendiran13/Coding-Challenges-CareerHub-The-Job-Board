using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CareerHub.Model;
using System.Threading.Tasks;

namespace CareerHub.Exceptions
{
    public class EmailValidator
    {
        public void ValidateEmail(string email)
        {
            if (!email.Contains("@"))
            {
                throw new FormatException("Invalid Email Format");
            }
        }
    }

}
