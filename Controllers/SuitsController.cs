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
    public class SuitsController : Controller
    {
        // GET: Suits
        private ApplicationDbContext suitDb;
        public SuitsController()
        {
            suitDb = new ApplicationDbContext();
        }

        public ActionResult suits(string searchBy, String Searching, int ?page)
        {
            var s = suitDb.suits.ToList().ToPagedList(page ?? 1, 3);
            if (searchBy == "color")
            {
                return View(suitDb.suits.Where(x => x.description.Contains(Searching) || Searching == null).ToList().ToPagedList(page ?? 1, 3));
            }
            else
            {
                return View(suitDb.suits.Where(x => x.newPrice.ToString().Contains(Searching) || Searching == null).ToList().ToPagedList(page ?? 1, 3));
            }
            return View(s);
        }

        public ActionResult Details(int id)
        {
            var suit = suitDb.suits.SingleOrDefault(s => s.id == id);
            if (suit == null)
                return HttpNotFound();
            return View(suit);
        }

        /*add suit*/
        public ActionResult New()
        {
            var newsuit = new ViewModelCategory()
            {
                suits = new suits()
            };
            return View("New", newsuit);
        }

        [HttpPost]
        public ActionResult Save(ViewModelCategory d)
        {
            if (!ModelState.IsValid)   //validation
            {
                var suit = new suits
                {
                    photo = d.suits.photo,
                    oldPrice = d.suits.oldPrice,
                    newPrice = d.suits.newPrice,
                    description = d.suits.description,
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
                string newfilename = name + "_" + d.suits.id + d.suits.newPrice.ToString()+"suit" + fileEx;
                var newPath = Path.Combine(Server.MapPath("~/uploadFiles"), newfilename);
                d.suits.photo = newPath;
                d.file.SaveAs(newPath);
                var extrapath = d.suits.photo.Split('\\');
                d.suits.photo = extrapath[extrapath.Length - 1];
                suitDb.suits.Add(d.suits);
                suitDb.SaveChanges();
            }

            return RedirectToAction("suits", "Suits");
        }

        /*Edit*/
        public ActionResult Edit(int id)
        {
            var d = suitDb.suits.SingleOrDefault(c => c.id == id);
            if (d == null)
            {
                return HttpNotFound();
            }
            ViewModelCategory viewmodel = new ViewModelCategory()
            {
                suits = d
            };
            return View("newEdit", viewmodel);
        }

        public ActionResult SaveEdit(ViewModelCategory d)
        {
            if (!ModelState.IsValid)   //validation
            {
                var suit = new suits
                {
                    photo = d.suits.photo,
                    oldPrice = d.suits.oldPrice,
                    newPrice = d.suits.newPrice,
                    description = d.suits.description,
                };
                return View("newedit", d);
            }

            var dress = suitDb.suits.Single(m => m.id == d.suits.id);
            dress.photo = dress.photo;
            dress.newPrice = d.suits.newPrice;
            dress.oldPrice = d.suits.oldPrice;
            dress.description = d.suits.description;

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
                        string newfilename = name + "_" + d.suits.id+"suitEdit" + fileEx;
                        var newPath = Path.Combine(Server.MapPath("~/uploadFiles"), newfilename);
                        d.suits.photo = newPath;
                        d.file.SaveAs(newPath);
                        var extrapath = d.suits.photo.Split('\\');
                        dress.photo = extrapath[extrapath.Length - 1];
                    }
                }
            }
            catch
            {

            }
            suitDb.SaveChanges();
            return RedirectToAction("suits", "Suits");
        }

        public ActionResult newEdit()
        {
            var editD = new ViewModelCategory();
            return View("newEdit", editD);
        }

        public ActionResult Delete(int id)
        {
            var d = suitDb.suits.SingleOrDefault(c => c.id == id);
            if (d == null)
                return HttpNotFound();
            return View(d);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult SaveDelete(int id)
        {
            var d = suitDb.suits.Find(id);
            suitDb.suits.Remove(d);
            suitDb.SaveChanges();
            return RedirectToAction("suits", "Suits");
        }

    }
}