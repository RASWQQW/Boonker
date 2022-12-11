using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Boonker.Data.Models
{
    public class Follows
    {
        public int Id { get; set; }

        [ForeignKey("User.addId")]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("User.addId")]
        public int FollowedUserId { get; set; }
        public User FollowedUser { get; set; }
    }
}
