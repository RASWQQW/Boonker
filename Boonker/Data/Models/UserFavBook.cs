using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Boonker.Data.Models
{
    public class UserFavBook
    {
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public Book FavBook { get; set; }

    }
}
