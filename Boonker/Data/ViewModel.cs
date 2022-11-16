using Boonker.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boonker.Data
{
    public class ViewModel
    {

        public IEnumerable<Book> Allbooks { get; set; }
        public Cat cats { get; set; }
        public string title { get; set; }
        public IEnumerable<Cat> AllCats { get; set; }
    }
}
