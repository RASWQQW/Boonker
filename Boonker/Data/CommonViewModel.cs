using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boonker.Data.Models
{
    public class CommonViewModel
    {
        public List<Book> RecBooks { get; set; }
        public Book books { get; set; }
        public bool InBasket { get; set; }
        public List<Cat> AllCats { get; set; }
        public List<int> CatAmount { get; set; } 
        public List<FoundObjects> FoundObjets { get; set; }
    }
}
