using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boonker.Data.Models
{
    public class Basket
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Book Book { get; set; }
        public String Review { get; set; }
        public int AmountOf { get; set; }
    }
}
