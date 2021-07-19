using PagedList;
using Shopping.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shopping.Controllers
{
    public class ShortsController : Controller
    {
        // GET: Shorts
        private ApplicationDbContext shortsDb;
        public ShortsController()
        {
            shortsDb = new ApplicationDbContext();
        }
        public ActionResult shorts(string searchBy, String Searching, int ?page)
        {
            var s = shortsDb.shorts.ToList().ToPagedList(page ?? 1, 3);
            if (searchBy == "color")
            {
                return View(shortsDb.shorts.Where(x => x.description.Contains(Searching) || Searching == null).ToList().ToPagedList(page ?? 1, 3));
            }
            else
            {
                return View(shortsDb.shorts.Where(x => x.newPrice.ToString().Contains(Searching) || Searching == null).ToList().ToPagedList(page ?? 1, 3));
            }
            return View(s);
        }

        public ActionResult Details(int id)
        {
            var s = shortsDb.shorts.SingleOrDefault(m => m.id == id);
            if (s == null)
                return HttpNotFound();
            return View(s);
        }
        /*add shorts*/
        public ActionResult New()
        {
            var newDress = new ViewModelCategory()
            {
                shorts = new shorts()
            };
            return View("New", newDress);
        }

        [HttpPost]
        public ActionResult Save(ViewModelCategory d)
        {
            if (!ModelState.IsValid)   //validation
            {
                var dress = new shorts
                {
                    photo = d.shorts.photo,
                    oldPrice = d.shorts.oldPrice,
                    newPrice = d.shorts.newPrice,
                    description = d.shorts.description,
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
                string newfilename = name + "_" + d.shorts.id + d.shorts.newPrice.ToString() + "shorts" + fileEx;
                var newPath = Path.Combine(Server.MapPath("~/uploadFiles"), newfilename);
                d.shorts.photo = newPath;
                d.file.SaveAs(newPath);
                var extrapath = d.shorts.photo.Split('\\');
                d.shorts.photo = extrapath[extrapath.Length - 1];
                shortsDb.shorts.Add(d.shorts);
                shortsDb.SaveChanges();
            }

            return RedirectToAction("shorts", "Shorts");
        }

        /*Edit*/
        public ActionResult Edit(int id)
        {
            var d = shortsDb.shorts.SingleOrDefault(c => c.id == id);
            if (d == null)
            {
                return HttpNotFound();
            }
            ViewModelCategory viewmodel = new ViewModelCategory()
            {
                shorts = d
            };
            return View("newEdit", viewmodel);
        }

        public ActionResult SaveEdit(ViewModelCategory d)
        {
            if (!ModelState.IsValid)   //validation
            {
                var dr = new shorts
                {
                    photo = d.shorts.photo,
                    oldPrice = d.shorts.oldPrice,
                    newPrice = d.shorts.newPrice,
                    description = d.shorts.description,
                };
                return View("newedit", d);
            }

            var dress = shortsDb.shorts.Single(m => m.id == d.shorts.id);
            dress.photo = dress.photo;
            dress.newPrice = d.shorts.newPrice;
            dress.oldPrice = d.shorts.oldPrice;
            dress.description = d.shorts.description;

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
                        string newfilename = name + "_" + d.shorts.id + "shortsEdit" + fileEx;
                        var newPath = Path.Combine(Server.MapPath("~/uploadFiles"), newfilename);
                        d.shorts.photo = newPath;
                        d.file.SaveAs(newPath);
                        var extrapath = d.shorts.photo.Split('\\');
                        dress.photo = extrapath[extrapath.Length - 1];
                    }
                }
            }
            catch
            {
                dress.photo = dress.photo;
            }
            shortsDb.SaveChanges();
            return RedirectToAction("shorts", "Shorts");
        }

        public ActionResult newEdit()
        {
            var editD = new ViewModelCategory();
            return View("newEdit", editD);
        }

        public ActionResult Delete(int id)
        {
            var d = shortsDb.shorts.SingleOrDefault(c => c.id == id);
            if (d == null)
                return HttpNotFound();
            return View(d);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult SaveDelete(int id)
        {
            var d = shortsDb.shorts.Find(id);
            shortsDb.shorts.Remove(d);
            shortsDb.SaveChanges();
            return RedirectToAction("shorts", "Shorts");
        }

    }
}