using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Shopping.Models;
using Shopping.Migrations;
using PagedList;
using System.IO;

namespace Shopping.Controllers
{
    public class ShoesController : Controller
    {
        // GET: Shoes
        private ApplicationDbContext shoesDb;
        public ShoesController()
        {
            shoesDb = new ApplicationDbContext();
        }
        public ActionResult Shoes(string searchBy, String Searching,int?page)
        {
            var shoesgirls = shoesDb.shoes.Where(x => x.Girls == true).ToList().ToPagedList(page ?? 1, 3);
            if (searchBy == "color")
            {
                var s = (shoesDb.shoes.Where(x => x.description.Contains(Searching) || Searching == null));
                return View(s.Where(x => x.Girls == true).ToList().ToPagedList(page ?? 1, 3));
            }
            else if (searchBy == "size")
            {
                var s = (shoesDb.shoes.Where(x => x.Size.ToString().Contains(Searching) || Searching == null));
                return View(s.Where(x => x.Girls == true).ToList().ToPagedList(page ?? 1, 3));
            }
            else if(searchBy == "price")
            {
                var s = (shoesDb.shoes.Where(x => x.newPrice.ToString().Contains(Searching) || Searching == null));
                return View(s.Where(x => x.Girls == true).ToList().ToPagedList(page ?? 1, 3));
            }
            return View(shoesgirls);
        }

        public ActionResult Shoesmale(string searchBy, String Searching,int? page)
        {
            var shoesman = shoesDb.shoes.Where(x => x.Man == true).ToList().ToPagedList(page ?? 1, 3);
            if (searchBy == "color")
            {
                var s = (shoesDb.shoes.Where(x => x.description.Contains(Searching) || Searching == null));
                return View(s.Where(x => x.Man == true).ToList().ToPagedList(page ?? 1, 3));
            }
            else if (searchBy == "size")
            {
                var s = (shoesDb.shoes.Where(x => x.Size.ToString().Contains(Searching) || Searching == null));
                return View(s.Where(x => x.Man == true).ToList().ToPagedList(page ?? 1, 3));
            }
            else if (searchBy == "price")
            {
                var s = (shoesDb.shoes.Where(x => x.newPrice.ToString().Contains(Searching) || Searching == null));
                return View(s.Where(x => x.Man == true).ToList().ToPagedList(page ?? 1, 3));
            }

            return View(shoesman);
        }

        public ActionResult Details(int id)
        {
            var shoes = shoesDb.shoes.SingleOrDefault(s => s.id == id);
            if (shoes == null)
            {
                return HttpNotFound();
            }
            return View(shoes);
        }

        public ActionResult New()
        {
            var newDress = new ViewModelCategory()
            {
                shoes = new shoes()
            };
            return View("New", newDress);
        }

        [HttpPost]
        public ActionResult Save(ViewModelCategory d, string ch)
        {
            if (ch == "Girls")
                d.shoes.Girls = true;
            else
                d.shoes.Man = true;


            if (!ModelState.IsValid)   //validation
            {
                var child = new shoes
                {
                    photo = d.shoes.photo,
                    oldPrice = d.shoes.oldPrice,
                    newPrice = d.shoes.newPrice,
                    description = d.shoes.description,
                    Size = d.shoes.Size,
                    Girls = d.shoes.Girls,
                    Man = d.shoes.Man,
                };
                return View("New", d);
            }
            var extentions = new List<String>
            {
                ".jbg",".png",".jpg",".jpeg",".jfif",".html",".webp"
            };

            var filename = Path.GetFileName(d.file.FileName);
            var fileEx = Path.GetExtension(d.file.FileName);
            if (extentions.Contains(fileEx))
            {
                string name = Path.GetFileNameWithoutExtension(filename);
                string newfilename = name + "_" + d.shoes.id + d.shoes.newPrice.ToString() + "shoes" + fileEx;
                var newPath = Path.Combine(Server.MapPath("~/uploadFiles"), newfilename);
                d.shoes.photo = newPath;
                d.file.SaveAs(newPath);
                var extrapath = d.shoes.photo.Split('\\');
                d.shoes.photo = extrapath[extrapath.Length - 1];
                shoesDb.shoes.Add(d.shoes);
                shoesDb.SaveChanges();
            }

            return RedirectToAction("Shoes", "Shoes");
        }

        /*Edit*/
        public ActionResult Edit(int id)
        {
            var d = shoesDb.shoes.SingleOrDefault(c => c.id == id);
            if (d == null)
            {
                return HttpNotFound();
            }
            ViewModelCategory viewmodel = new ViewModelCategory()
            {
                shoes = d
            };
            return View("newEdit", viewmodel);
        }

        public ActionResult SaveEdit(ViewModelCategory d, string ch)
        {
            if (!ModelState.IsValid)   //validation
            {
                var child = new shoes
                {
                    
                    photo = d.shoes.photo,
                    oldPrice = d.shoes.oldPrice,
                    newPrice = d.shoes.newPrice,
                    description = d.shoes.description,
                    Size = d.shoes.Size,
                    Girls = d.shoes.Girls,
                    Man = d.shoes.Man,
                };
                return View("newedit", d);
            }

            if (ch == "Girls")
                d.shoes.Girls = true;
            else
                d.shoes.Man = true;

            var dress = shoesDb.shoes.Single(m => m.id == d.shoes.id);
            dress.photo = dress.photo;
            dress.newPrice = d.shoes.newPrice;
            dress.oldPrice = d.shoes.oldPrice;
            dress.description = d.shoes.description;

            var extentions = new List<String>
            {
                ".jbg",".png",".jpg",".jpeg",".jfif",".html",".webp"
            };
            try
            {
                if (d.file.FileName != null)
                {
                    var filename = Path.GetFileName(d.file.FileName);
                    var fileEx = Path.GetExtension(d.file.FileName);
                    if (extentions.Contains(fileEx))
                    {
                        string name = Path.GetFileNameWithoutExtension(filename);
                        string newfilename = name + "_" + d.shoes.id + "shoesEdit" + fileEx;
                        var newPath = Path.Combine(Server.MapPath("~/uploadFiles"), newfilename);
                        d.shoes.photo = newPath;
                        d.file.SaveAs(newPath);
                        var extrapath = d.shoes.photo.Split('\\');
                        dress.photo = extrapath[extrapath.Length - 1];
                    }
                }
            }
            catch
            {

            }
            shoesDb.SaveChanges();
            return RedirectToAction("Shoes", "Shoes");
        }

        public ActionResult newEdit()
        {
            var editD = new ViewModelCategory();
            return View("newEdit", editD);
        }

        public ActionResult Delete(int id)
        {
            var d = shoesDb.shoes.SingleOrDefault(c => c.id == id);
            if (d == null)
                return HttpNotFound();
            return View(d);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult SaveDelete(int id)
        {
            var d = shoesDb.shoes.Find(id);
            shoesDb.shoes.Remove(d);
            shoesDb.SaveChanges();
            return RedirectToAction("Shoes", "Shoes");
        }

    }
}
