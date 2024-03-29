﻿using Boonker.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boonker.Data
{
    public class BooksAddData : IdentityDbContext<User>
    {
        public BooksAddData(DbContextOptions<BooksAddData> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.UseSerialColumns();
            base.OnModelCreating(modelbuilder);
            //modelbuilder.Ignore<IdentityUserLogin<string>>();
            //modelbuilder.Ignore<IdentityUserRole<string>>();
            //modelbuilder.Ignore<IdentityUserClaim<string>>();
            //modelbuilder.Ignore<IdentityUserToken<string>>();
            //modelbuilder.Ignore<IdentityUser<string>>();
            //modelbuilder.Ignore<RoleManager<string>>();
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Cat> Cats { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<ImgEntry> ImgEntry { get; set; }
        public DbSet<UserFavBook> UserFavBook { get; set; }
        public DbSet<UserCreatedBook> UserCreatedBook { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<BookSearched> Searched { get; set; }
        public DbSet<Follows> Follows { get; set; }
        public DbSet<ResOps> PassResOps { get; set; }
        public DbSet<Basket> Basket { get; set; }
    }
}
