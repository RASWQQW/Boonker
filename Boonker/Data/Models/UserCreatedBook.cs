using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boonker.Data.Models
{
    public class UserCreatedBook
    {
        public int Id { get; set; }
        public Book CreatedBook { get; set; }
    }
}
