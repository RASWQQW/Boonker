//using Boonker.Data.Models;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Boonker.Data
//{
//    public class DDApplicationDbContext : IdentityDbContext<User>
//    {
//        public DDApplicationDbContext(
//            DbContextOptions<DDApplicationDbContext> options) : base(options) 
//        {
//            Database.EnsureCreated();
//        }

//        protected override void OnModelCreating(ModelBuilder modelbuilder)
//        {
//            modelbuilder.UseSerialColumns();
//        }

//        public DbSet<Book> Books { get; set; }
//        public DbSet<Cat> Cats { get; set; }
//        public DbSet<Author> Authors { get; set; }
//        public DbSet<User> Users { get; set; }
//    }
//}
