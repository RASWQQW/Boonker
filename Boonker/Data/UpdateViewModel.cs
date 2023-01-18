using Boonker.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boonker.Data
{
    public class UpdateViewModel
    {
       public Book books { get; set; }
       public List<SelectListItem> Categories { get; set; }
       public int CategoryId { get; set; }

       public List<SelectListItem> Authors { get; set; }
       public ImagesForForm Images { get; set; }
       public string CurrentPassword { get; set; }
       public string NewPassword { get; set; }

    }
}
