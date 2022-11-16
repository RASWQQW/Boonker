using Boonker.Data.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boonker.Data
{
    public class DataInsert
    {
        public static void Initial(BooksAddData content)
        {

            if (!content.Cats.Any())
            {
                content.Cats.AddRange(Categories.Select(content => content.Value));
            }

            if (!content.Books.Any())
            {
                content.AddRange(
                        new Book   
                        {
                            Title = "Think and grow rich",
                            DSCR = "Bla bla",
                            Img = "https://m.media-amazon.com/images/I/51bhhK6yobL._AC_SY780_.jpg",
                            Author = new Author { Name = "Anna Decora", DSCR = "Odss" },
                            Amount = 200,
                            Price = 300,
                            Category = Categories["Bussines"]
                        },

                        new Book
                        {
                            Title = "Lucy",
                            DSCR = "Bla bla",
                            Img = "https://m.media-amazon.com/images/I/51GKurK15AL._AC_SY780_.jpg",
                            Author = new Author { Name = "Anna Decora", DSCR = "Odss" },
                            Amount = 144,
                            Price = 249,
                            Category = Categories["Drama"]
                        },

                        new Book
                        {
                            Title = "The Lord Of The Rings",
                            DSCR = "Bla bla",
                            Img = "https://usercontent.one/wp/www.alltopeverything.com/wp-content/uploads/2020/05/8134AkhQJgL-600x917.jpg?media=1659724391",
                            Author = new Author { Name = "John Tolkien", DSCR = "Adam"},
                            Amount = 1144,
                            Price = 629,
                            Category = Categories["Fantasy"]
                        },

                        new Book
                        {
                            Title = "The Alchemist",
                            DSCR = "Bla bla",
                            Img = "https://m.media-amazon.com/images/I/41ybG235TcL._SX329_BO1,204,203,200_.jpg",
                            Author = new Author { Name = "Anna Decora", DSCR = "Odss" },
                            Amount = 134,
                            Price = 450,
                            Category = Categories["Drama"]
                        }

                    );
            }

            content.SaveChanges();

        }

        private static Dictionary<string, Cat> cats;
        public static Dictionary<string, Cat> Categories
        {
            get
            {
                if (cats == null)
                {
                    var list = new Cat[]
                    {
                        new Cat{ Name="Bussines", DSCR="Just do It" },
                        new Cat{ Name="Fantasy", DSCR="Big pieace of sheet" },
                        new Cat{ Name="Action", DSCR="Satisfaction just initil step is it" },
                        new Cat{ Name="Drama", DSCR="This genre about calm entire inside" },
                        new Cat{ Name="Adventure", DSCR="How many stunny pieces of imagenation" },
                        new Cat{ Name="Thriller", DSCR="Do you wanna feel some unexpected endings or get anxiety influence of mind" }
                    };
                    cats = new Dictionary<string, Cat>();
                    foreach (var cat in list)
                        cats.Add(cat.Name, cat);

                }

                return cats;
            }
        }
    }
}
