using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boonker.Data.Models
{
    public class Cat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DSCR { get; set; }
        public List<Book> Books { get; set; }
        public int CatViews { get; set; }
    }
}
