using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Boonker.Data.Models
{
    public class BookSearched
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [ForeignKey("Book")]
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<FoundObjects> ResultList { get; set; }
        public DateTime Date { get; set; } = new DateTime().Date;
    }
}
