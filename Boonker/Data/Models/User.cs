using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Boonker.Data.Models
{
    public class User: IdentityUser
    {   
        public int addId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string PasswordCheck { get; set; }
        public string Role { get; set; }
        public string About { get; set; }
        public string Image { get; set; }
        public int Followers { get; set; }
    }
}
