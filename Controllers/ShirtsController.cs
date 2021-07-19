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
    public class ShirtsController : Controller
    {
        // GET: Shirts
        private ApplicationDbContext shirtsDb;
        public ShirtsController()
        {
            shirtsDb = new ApplicationDbContext();
        }
        public ActionResult shirts(string searchBy, String Searching, int? page)
        {
            var sh = shirtsDb.shirts.ToList().ToPagedList(page ?? 1, 3);
            if (searchBy == "color")
            {
                return View(shirtsDb.shirts.Where(x => x.description.Contains(Searching) || Searching == null).ToList().ToPagedList(page ?? 1, 3));
            }
            else
            {
                return View(shirtsDb.shirts.Where(x => x.newPrice.ToString().Contains(Searching) || Searching == null).ToList().ToPagedList(page ?? 1, 3));
            }
            return View(sh);
        }
        public ActionResult Details(int id)
        {
            var sh = shirtsDb.shirts.SingleOrDefault(m => m.id == id);
            if (sh == null)
                return HttpNotFound();
            return View(sh);
        }
        /*add shirts*/
        public ActionResult New()
        {
            var newDress = new ViewModelCategory()
            {
                shirts = new shirts()
            };
            return View("New", newDress);
        }

        [HttpPost]
        public ActionResult Save(ViewModelCategory d)
        {
            if (!ModelState.IsValid)   //validation
            {
                var dress = new shirts
                {
                    photo = d.shirts.photo,
                    oldPrice = d.shirts.oldPrice,
                    newPrice = d.shirts.newPrice,
                    description = d.shirts.description,
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
                string newfilename = name + "_" + d.shirts.id + d.shirts.newPrice.ToString() + "shirt" + fileEx;
                var newPath = Path.Combine(Server.MapPath("~/uploadFiles"), newfilename);
                d.shirts.photo = newPath;
                d.file.SaveAs(newPath);
                var extrapath = d.shirts.photo.Split('\\');
                d.shirts.photo = extrapath[extrapath.Length - 1];
                shirtsDb.shirts.Add(d.shirts);
                shirtsDb.SaveChanges();
            }

            return RedirectToAction("shirts", "Shirts");
        }

        /*Edit*/
        public ActionResult Edit(int id)
        {
            var d = shirtsDb.shirts.SingleOrDefault(c => c.id == id);
            if (d == null)
            {
                return HttpNotFound();
            }
            ViewModelCategory viewmodel = new ViewModelCategory()
            {
                shirts = d
            };
            return View("newEdit", viewmodel);
        }

        public ActionResult SaveEdit(ViewModelCategory d)
        {
            if (!ModelState.IsValid)   //validation
            {
                var dr = new shirts
                {
                    oldPrice = d.shirts.oldPrice,
                    newPrice = d.shirts.newPrice,
                    description = d.shirts.description,
                };
                return View("newedit",d);
            }

            var dress = shirtsDb.shirts.Single(m => m.id == d.shirts.id);
            dress.photo = dress.photo;
            dress.newPrice = d.shirts.newPrice;
            dress.oldPrice = d.shirts.oldPrice;
            dress.description = d.shirts.description;

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
                        string newfilename = name + "_" + d.shirts.id + "shirtEdit" + fileEx;
                        var newPath = Path.Combine(Server.MapPath("~/uploadFiles"), newfilename);
                        d.shirts.photo = newPath;
                        d.file.SaveAs(newPath);
                        var extrapath = d.shirts.photo.Split('\\');
                        dress.photo = extrapath[extrapath.Length - 1];
                    }
                }
            }
            catch
            {

            }
            shirtsDb.SaveChanges();
            return RedirectToAction("shirts", "Shirts");
        }

        public ActionResult newEdit()
        {
            var editD = new ViewModelCategory();
            return View("Edit", editD);
        }

        public ActionResult Delete(int id)
        {
            var d = shirtsDb.shirts.SingleOrDefault(c => c.id == id);
            if (d == null)
                return HttpNotFound();
            return View(d);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult SaveDelete(int id)
        {
            var d = shirtsDb.shirts.Find(id);
            shirtsDb.shirts.Remove(d);
            shirtsDb.SaveChanges();
            return RedirectToAction("shirts", "Shirts");
        }

    }
}