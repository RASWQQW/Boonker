using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boonker.Data.Models
{
    public class UserFavBook
    {
        public int Id { get; set; }
        public User UserId { get; set; }
        public Book FavBook { get; set; }

    }
}
