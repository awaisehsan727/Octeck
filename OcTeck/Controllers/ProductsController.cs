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
    public class ProductsController : Controller
    {
         OctDataEntities db = new OctDataEntities();
        Product p = new Product();

        // GET: Products
      
        public ActionResult Index(int? id)
        {
           
            if (id != null)
            {
                p = db.Products.Find(id);
                String price = p.Price;
                float a = float.Parse(price);
                if (a >= 30 ||price!=null)
                {
                    float Res = a * 10 / 100;
                    Result r = new Result();
                    int i = Convert.ToInt32(id);
                    r.total_Price = Convert.ToString(Res);
                    r.id = i;
                    db.Results.Add(r);
                    db.SaveChanges();
                   ViewData["Login"] = "Product is Added To the Cart"; ;
                    return View(db.Products.ToList());
                }
                else
                {
                    Result r = new Result();
                    int i = Convert.ToInt32(id);
                    r.total_Price = price;
                    r.id = i;
                    db.Results.Add(r);
                    db.SaveChanges();
                    ViewData["Login"] = "Product is Added To the Cart";
                    return View(db.Products.ToList());
                }
               
            }
            
            else
            {
                
                ViewData["Login"] = "No Product is Added To the Cart";
                return View(db.Products.ToList());
            }
           
           
        }

        // GET: Products/Details/5
       
        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Create( String Pcd,string Pn,String Pr)
        {
        
            try
            {
                Product x = new Product(); 
                x = db.Products.First(row => row.Product_Code == Convert.ToInt32(Pcd));            // Check Email Exist then Show Massge Choose another Email
                ViewData["error"] = "This Code is Already Exist";
                return View("Create");                                   // Redirect To View Signup If Email exits don't make dublicate account on same email
            }
            catch (Exception ex)
            {
                try
                {
                    Product a = new Product();
                    a = db.Products.First(row => row.Name == Pn);            // Check Email Exist then Show Massge Choose another Email
                    ViewData["error"] = "This name is Alreaady Exist Choos an other";             // View data is use to display the error on front end
                    return View("Create");
                }
                catch (Exception e)
                {
                    Product p = new Product();
                    p.Price = Pr;
                    p.Product_Code = Convert.ToInt32(Pcd);
                    p.Name = Pn;
                    db.Products.Add(p);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
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
