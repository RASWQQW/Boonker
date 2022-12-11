using Boonker.Data.Models;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Boonker.Data
{
    public class UserViewModel
    {
        public User currentUser { get; set; }
        public List<Book> UserBooks { get; set; }
        public IFormFile UserImage { get; set; }
        public int UserId { get; set; }
        public List<Follows> Status { get; set; }
    }
}
