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
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string PasswordCheck { get; set; }
        public string Role { get; set; }
        public string About { get; set; }
    }
}
