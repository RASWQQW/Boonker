using Boonker.Data.Models;
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

    }
}
