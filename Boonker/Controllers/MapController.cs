using Boonker.Data;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Boonker.Controllers
{
    public class MapController: Controller
    {
        private readonly BooksAddData context;

        public MapController(BooksAddData Data)
        {
            this.context = Data;
        }

        public ActionResult MapShower()
        {
            MapViewModel model = new MapViewModel();
            return View(model);
        }
    }
    
}
