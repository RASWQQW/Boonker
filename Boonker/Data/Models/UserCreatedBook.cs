using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Boonker.Data.Models
{
    public class UserCreatedBook
    {
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User CUser { get; set; }
        [ForeignKey("Book")]
        public int BookId { get; set; }
        public Book CreatedBook { get; set; }
    }
}
