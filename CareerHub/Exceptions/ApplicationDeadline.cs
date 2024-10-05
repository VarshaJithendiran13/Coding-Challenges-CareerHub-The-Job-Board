using CareerHub.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CareerHub.Exceptions
{
    public class ApplicationDeadline
    {
        public void SubmitApplication(Applications application, DateTime deadline)
        {
            if (DateTime.Now > deadline)
            {
                throw new Exception("Application deadline has passed");
            }
        }

    }
}
