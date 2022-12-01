using Boonker.Data;
using Boonker.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boonker.Controllers
{
    public class SearchController : Controller
    {
        public readonly BooksAddData context;
        public SearchController(BooksAddData Data)
        {
            context = Data;
        }

        [HttpPost]
        public IActionResult Index(string keyword)
        {

            var ss = keyword;

            var model = context.Books
                .Include(w => w.ImgEntry)
                .Include(s => s.Category)
                .Include(s => s.Author)
                .Where(

                w => w.Title.ToLower().Trim().Contains(keyword.ToLower().Trim()) ||
                w.Author.FirstName.ToLower().Trim().Contains(keyword.ToLower().Trim()) ||
                w.Author.LastName.ToLower().Trim().Contains(keyword.ToLower().Trim()) ||
                w.Category.Name.ToLower().Trim().Contains(keyword.ToLower().Trim()) ||
                w.DSCR.ToLower().Trim().Contains(keyword.ToLower().Trim())
                
                );

            ViewModel viewModel = new ViewModel
            {

                Allbooks = model,
                AllCats = context.Cats.OrderBy(c => c.Name),
            };

            return View(viewModel);
        }
    }
}
