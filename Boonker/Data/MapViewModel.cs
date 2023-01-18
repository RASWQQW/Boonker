using Boonker.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boonker.Data
{
    public class MapViewModel
    {
        public int Id { get; set; }
        public string Cookies { get; set; }
        public User User { get; set; }
    }
}
