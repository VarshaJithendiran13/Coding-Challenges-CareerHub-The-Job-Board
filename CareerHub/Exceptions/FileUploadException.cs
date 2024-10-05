using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareerHub.Model;

namespace CareerHub.Exceptions
{
    public class FileUploadException
    {
        public void UploadResume(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Resume file not found");
            }
            if (new FileInfo(filePath).Length > 5000000) // 5MB size limit
            {
                throw new Exception("File size exceeds limit");
            }
            if (!filePath.EndsWith(".pdf"))
            {
                throw new Exception("Unsupported file format");
            }
        }

    }
}
