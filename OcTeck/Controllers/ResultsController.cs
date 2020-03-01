using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OcTeck.Models;

namespace OcTeck.Controllers
{
    public class ResultsController : Controller
    {
        private OctDataEntities db = new OctDataEntities();
        Result R = new Result();

        // GET: Results
        public ActionResult Index(int? id)
        {
            Product p = new Product();
            p = db.Products.Find(id);
            String price = p.Price;
            float a = float.Parse(price);
            if (a >= 30)
            {
                ViewData["price"] = "Actual Price Of Product" + " " + price +"Rs";
                ViewData["Login"] = "You Got 10 % Discount";
                var results = db.Results.Include(r => r.Product);
                return View(db.Results.Where(x => x.id == id).ToList());
            }
            else
            {
                ViewData["price"] = "Actual Price Of Product"+ " " + price + "Rs";
                ViewData["Login"] = "Your Price is Less Then 30 so You Don't Get Any Discount";
                var results = db.Results.Include(r => r.Product);
                return View(db.Results.Where(x => x.id == id).ToList());

            }
           
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
