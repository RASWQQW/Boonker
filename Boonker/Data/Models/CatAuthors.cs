using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boonker.Data.Models
{
    public class CatAuthors
    {
        public int Id { get; set; }
        public Author AuthorId { get; set; }
        public Cat CatId { get; set; }
    }
}
