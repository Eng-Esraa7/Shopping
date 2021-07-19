using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Shopping.Models;
namespace Shopping.Controllers
{
    public class DressesController : Controller
    {
        // GET: Dresses
        private ApplicationDbContext DressDb;
        public DressesController()
        {
            DressDb = new ApplicationDbContext();
        }
        public ActionResult Dresses(string searchBy, String Searching, int ?page)
        {
            var Dress = DressDb.Dresses.ToList().ToPagedList(page ?? 1, 3);
            if (searchBy == "color")
            {
                return View(DressDb.Dresses.Where(x => x.description.Contains(Searching) || Searching == null).ToList().ToPagedList(page ?? 1, 3));
            }
            else
            {
                return View(DressDb.Dresses.Where(x => x.newPrice.ToString().Contains(Searching) || Searching == null).ToList().ToPagedList(page ?? 1, 3));
            }
            return View(Dress);
        }

        public ActionResult Details(int id)
        {
            var Dress = DressDb.Dresses.SingleOrDefault(s => s.id == id);
            if (Dress == null)
            {
                return HttpNotFound();
            }
            return View(Dress);
        }

        /*add dresses*/
        public ActionResult New()
        {
            var newDress = new ViewModelCategory()
            {
                dress = new Dresses()
            };
            return View("New", newDress);
        }

        [HttpPost]
        public ActionResult Save(ViewModelCategory d)
        {
            if (!ModelState.IsValid)   //validation
            {
                var dress = new Dresses
                {
                    photo = d.dress.photo,
                    oldPrice = d.dress.oldPrice,
                    newPrice = d.dress.newPrice,
                    description = d.dress.description,
                };
                return View("New", d);
            }

            var extentions = new List<String>
            {
                ".jbg",".png",".jpg",".jpeg",".jfif",".html"
            };

            var filename = Path.GetFileName(d.file.FileName);
            var fileEx = Path.GetExtension(d.file.FileName);
            if (extentions.Contains(fileEx))
            {
                string name = Path.GetFileNameWithoutExtension(filename);
                string newfilename = name + "_" + d.dress.id+d.dress.newPrice.ToString()+"dress" + fileEx;
                var newPath = Path.Combine(Server.MapPath("~/uploadFiles"), newfilename);
                d.dress.photo = newPath;
                d.file.SaveAs(newPath);
                var extrapath = d.dress.photo.Split('\\');
                d.dress.photo = extrapath[extrapath.Length - 1];
                DressDb.Dresses.Add(d.dress);
                DressDb.SaveChanges();
            }

            return RedirectToAction("Dresses", "Dresses");
        }

        /*Edit*/
        public ActionResult Edit(int id)
        {
            var d = DressDb.Dresses.SingleOrDefault(c => c.id == id);
            if (d == null)
            {
                return HttpNotFound();
            }
            ViewModelCategory viewmodel = new ViewModelCategory(){
                dress = d
            };
            return View("newEdit", viewmodel);
        }

        public ActionResult SaveEdit(ViewModelCategory d)
        {
            if (!ModelState.IsValid)   //validation
            {
                var dr = new Dresses
                {
                    photo = d.dress.photo,
                    oldPrice = d.dress.oldPrice,
                    newPrice = d.dress.newPrice,
                    description = d.dress.description,
                };
                return View("newedit", d);
            }

            var dress = DressDb.Dresses.Single(m => m.id == d.dress.id);
            dress.photo = dress.photo;
            dress.newPrice = d.dress.newPrice;
            dress.oldPrice = d.dress.oldPrice;
            dress.description = d.dress.description;

            var extentions = new List<String>
            {
                ".jbg",".png",".jpg",".jpeg",".jfif",".html"
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
                        string newfilename = name + "_" + d.dress.id+"dressEdit" + fileEx;
                        var newPath = Path.Combine(Server.MapPath("~/uploadFiles"), newfilename);
                        d.dress.photo = newPath;
                        d.file.SaveAs(newPath);
                        var extrapath = d.dress.photo.Split('\\');
                        dress.photo = extrapath[extrapath.Length - 1];
                    }
                }
            }
            catch
            {

            }
            DressDb.SaveChanges();
            return RedirectToAction("Dresses", "Dresses");
        }

        public ActionResult newEdit()
        {
            var editD = new ViewModelCategory();
            return View("newEdit", editD);
        }

        public ActionResult Delete(int id)
        {
            var d = DressDb.Dresses.SingleOrDefault(c => c.id == id);
            if (d == null)
                return HttpNotFound();
            return View(d);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult SaveDelete(int id)
        {
            var d = DressDb.Dresses.Find(id);
            DressDb.Dresses.Remove(d);
            DressDb.SaveChanges();
            return RedirectToAction("Dresses", "Dresses");
        }

    }
}