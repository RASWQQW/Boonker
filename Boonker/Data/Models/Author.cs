using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boonker.Data.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DSCR { get; set; }
        public string Image { get; set; }
        public int BornDate { get; set; }
        public string HomeTown { get; set; } 
    }
}
