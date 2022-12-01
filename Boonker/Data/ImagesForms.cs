using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boonker.Data.Models
{
    public class ImagesForms
    {
        public IFormFile Image1 { get; set; }  
        public IFormFile Image2 { get; set; }  
        public IFormFile Image3 { get; set; }  
        public IFormFile Image4 { get; set; }  
        public IFormFile Image5 { get; set; }  
        
    }
}
