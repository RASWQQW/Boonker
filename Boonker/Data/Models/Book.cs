using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Boonker.Data.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string DSCR { get; set; }
        
        public string Img { get; set; }

        [ForeignKey("ImgEntry")]
        public int ImgEntryId { get; set; }
        public ImgEntry ImgEntry { get; set; }

        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public Author Author { get; set; }

        [ForeignKey("Cat")]
        public int CategoryId { get; set; }
        public Cat Category { get; set; }
        public int Amount { get; set; }
        public int Price { get; set; }
        public bool IsFav { get; set; }
        public int Views { get; set; }
        public int Likes { get; set; }
        public int tirage { get; set; }

    }
}
