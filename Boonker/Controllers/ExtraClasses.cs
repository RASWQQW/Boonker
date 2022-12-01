using Boonker.Data;
using Boonker.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Boonker.Controllers
{
    public class ExtraClasses
    {
       
        public string UploadFile(   IWebHostEnvironment Enviro,
                                    IFormFile und,
                                    CreateAuthorViewModel fileN, 
                                    string option
                                )
        {
            IFormFile file = null; string path = "files"; 
            if(und == null) { file = fileN.Image;}
            else { file = und; }

            string filename = "SS.jpg";
            if(file != null)
            {
                string uploadDir = Path.Combine(Enviro.WebRootPath, path, path3: option);
                filename = Guid.NewGuid().ToString() + "_" 
                    + DateTime.Now.ToString("yymmff") + "_" + file.FileName;

                string filePath = Path.Combine(uploadDir, filename);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            return filename;
        }
    }
}
