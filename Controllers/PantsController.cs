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
    public class PantsController : Controller
    {
        // GET: Pants
        private ApplicationDbContext pantsDb;
        public PantsController()
        {
            pantsDb = new ApplicationDbContext();
        }
        public ActionResult pants(string searchBy, String Searching, int? page)
        {
            var p = pantsDb.pants.ToList().ToPagedList(page ?? 1, 3);
            if(searchBy == "color")
            {
                return View(pantsDb.pants.Where(x => x.description.Contains(Searching) || Searching == null).ToList().ToPagedList(page ?? 1, 3));
            }
            else
            {
                return View(pantsDb.pants.Where(x => x.newPrice.ToString().Contains(Searching) || Searching == null).ToList().ToPagedList(page ?? 1, 3));
            }
            return View(p);
        }


        public ActionResult Details(int id)
        {
            var p = pantsDb.pants.SingleOrDefault(m => m.id == id);
            if (p == null)
                return HttpNotFound();
            return View(p);
        }
        /*add pants*/
        public ActionResult New()
        {
            var newDress = new ViewModelCategory()
            {
                pants = new pants()
            };
            return View("New", newDress);
        }

        [HttpPost]
        public ActionResult Save(ViewModelCategory d)
        {
            if (!ModelState.IsValid)   //validation
            {
                var dress = new pants
                {
                    photo = d.pants.photo,
                    oldPrice = d.pants.oldPrice,
                    newPrice = d.pants.newPrice,
                    description = d.pants.description,
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
                string newfilename = name + "_" + d.pants.id + d.pants.newPrice.ToString() + "pants" + fileEx;
                var newPath = Path.Combine(Server.MapPath("~/uploadFiles"), newfilename);
                d.pants.photo = newPath;
                d.file.SaveAs(newPath);
                var extrapath = d.pants.photo.Split('\\');
                d.pants.photo = extrapath[extrapath.Length - 1];
                pantsDb.pants.Add(d.pants);
                pantsDb.SaveChanges();
            }

            return RedirectToAction("pants", "Pants");
        }

        /*Edit*/
        public ActionResult Edit(int id)
        {
            var d = pantsDb.pants.SingleOrDefault(c => c.id == id);
            if (d == null)
            {
                return HttpNotFound();
            }
            ViewModelCategory viewmodel = new ViewModelCategory()
            {
                pants = d
            };
            return View("newEdit", viewmodel);
        }

        public ActionResult SaveEdit(ViewModelCategory d)
        {
            if (!ModelState.IsValid)   //validation
            {
                var dr = new pants
                {
                    photo = d.pants.photo,
                    oldPrice = d.pants.oldPrice,
                    newPrice = d.pants.newPrice,
                    description = d.pants.description,
                };
                return View("newedit", d);
            }

            var dress = pantsDb.pants.Single(m => m.id == d.pants.id);
            dress.photo = dress.photo;
            dress.newPrice = d.pants.newPrice;
            dress.oldPrice = d.pants.oldPrice;
            dress.description = d.pants.description;

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
                        string newfilename = name + "_" + d.pants.id + "pantsEdit" + fileEx;
                        var newPath = Path.Combine(Server.MapPath("~/uploadFiles"), newfilename);
                        d.pants.photo = newPath;
                        d.file.SaveAs(newPath);
                        var extrapath = d.pants.photo.Split('\\');
                        dress.photo = extrapath[extrapath.Length - 1];
                    }
                }
            }
            catch
            {

            }
            pantsDb.SaveChanges();
            return RedirectToAction("pants", "Pants");
        }

        public ActionResult newEdit()
        {
            var editD = new ViewModelCategory();
            return View("newEdit", editD);
        }

        public ActionResult Delete(int id)
        {
            var d = pantsDb.pants.SingleOrDefault(c => c.id == id);
            if (d == null)
                return HttpNotFound();
            return View(d);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult SaveDelete(int id)
        {
            var d = pantsDb.pants.Find(id);
            pantsDb.pants.Remove(d);
            pantsDb.SaveChanges();
            return RedirectToAction("pants", "Pants");
        }

    }
}