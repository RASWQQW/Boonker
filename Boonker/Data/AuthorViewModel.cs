using Boonker.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boonker.Data
{
    public class AuthorViewModel
    {
        public Author Author { get; set; }
        public int BooksAmount { get; set; }
        public List<string> AllJanres { get; set; }
        public List<Book> AuthorBooks { get; set; }
    }
}
