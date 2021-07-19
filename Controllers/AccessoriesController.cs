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
    public class AccessoriesController : Controller
    {
        // GET: Accessories
            private ApplicationDbContext AccessDb;
            public AccessoriesController()
            {
                AccessDb = new ApplicationDbContext();
            }
            public ActionResult Accessories(string searchBy, String Searching,int ?page)
            {
                var Access = AccessDb.accessories.ToList().Where(x => x.Access == true).ToPagedList(page ?? 1, 3);
            if (searchBy == "color")
            {
                var s = (AccessDb.accessories.Where(x => x.description.Contains(Searching) || Searching == null));
                return View(s.Where(x => x.Access == true).ToList().ToPagedList(page ?? 1, 3));
            }
            else if (searchBy == "price")
            {
                var s = (AccessDb.accessories.Where(x => x.newPrice.ToString().Contains(Searching) || Searching == null));
                return View(s.Where(x => x.Access == true).ToList().ToPagedList(page ?? 1, 3));
            }
            return View(Access);
            }

        public ActionResult bags(string searchBy, String Searching, int? page)
        {
            var Access = AccessDb.accessories.ToList().Where(x => x.Bag == true).ToPagedList(page ?? 1, 3);
            if (searchBy == "color")
            {
                var s = (AccessDb.accessories.Where(x => x.description.Contains(Searching) || Searching == null));
                return View(s.Where(x => x.Bag == true).ToList().ToPagedList(page ?? 1, 3));
            }
            else if (searchBy == "price")
            {
                var s = (AccessDb.accessories.Where(x => x.newPrice.ToString().Contains(Searching) || Searching == null));
                return View(s.Where(x => x.Bag == true).ToList().ToPagedList(page ?? 1, 3));
            }
            return View(Access);
        }

        public ActionResult watches(string searchBy, String Searching, int? page)
        {
            var Access = AccessDb.accessories.ToList().Where(x => x.Watch == true).ToPagedList(page ?? 1, 3);
            if (searchBy == "color")
            {
                var s = (AccessDb.accessories.Where(x => x.description.Contains(Searching) || Searching == null));
                return View(s.Where(x => x.Watch == true).ToList().ToPagedList(page ?? 1, 3));
            }
            else if (searchBy == "price")
            {
                var s = (AccessDb.accessories.Where(x => x.newPrice.ToString().Contains(Searching) || Searching == null));
                return View(s.Where(x => x.Watch == true).ToList().ToPagedList(page ?? 1, 3));
            }
            return View(Access);
        }
        public ActionResult Details(int id)
            {
                var Access = AccessDb.accessories.SingleOrDefault(s => s.id == id);
                if (Access == null)
                {
                    return HttpNotFound();
                }
                return View(Access);
            }

        public ActionResult New()
        {
            var newDress = new ViewModelCategory()
            {
                accessories = new accessories()
            };
            return View("New", newDress);
        }

        [HttpPost]
        public ActionResult Save(ViewModelCategory d, string ch)
        {
            if (ch == "A")
                d.accessories.Access = true;
            else if (ch == "B")
                d.accessories.Bag = true;
            else
                d.accessories.Watch = true;


            if (!ModelState.IsValid)   //validation
            {
                var child = new accessories
                {
                    oldPrice = d.accessories.oldPrice,
                    newPrice = d.accessories.newPrice,
                    description = d.accessories.description,
                    Access = d.accessories.Access,
                    Bag = d.accessories.Bag,
                    Watch = d.accessories.Watch,
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
                string newfilename = name + "_" + d.accessories.id + d.accessories.newPrice.ToString() + "acess" + fileEx;
                var newPath = Path.Combine(Server.MapPath("~/uploadFiles"), newfilename);
                d.accessories.photo = newPath;
                d.file.SaveAs(newPath);
                var extrapath = d.accessories.photo.Split('\\');
                d.accessories.photo = extrapath[extrapath.Length - 1];
                AccessDb.accessories.Add(d.accessories);
                AccessDb.SaveChanges();
            }

            return RedirectToAction("Accessories", "Accessories");
        }

        /*Edit*/
        public ActionResult Edit(int id)
        {
            var d = AccessDb.accessories.SingleOrDefault(c => c.id == id);
            if (d == null)
            {
                return HttpNotFound();
            }
            ViewModelCategory viewmodel = new ViewModelCategory()
            {
                accessories = d
            };
            return View("newEdit", viewmodel);
        }

        public ActionResult SaveEdit(ViewModelCategory d)
        {
            if (!ModelState.IsValid)   //validation
            {
                var child = new accessories
                {
                    oldPrice = d.accessories.oldPrice,
                    newPrice = d.accessories.newPrice,
                    description = d.accessories.description,
                    Access = d.accessories.Access,
                    Bag = d.accessories.Bag,
                    Watch = d.accessories.Watch,
                };
                return View("New", d);
            }

            var dress = AccessDb.accessories.Single(m => m.id == d.accessories.id);
            dress.photo = dress.photo;
            dress.newPrice = d.accessories.newPrice;
            dress.oldPrice = d.accessories.oldPrice;
            dress.description = d.accessories.description;

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
                        string newfilename = name + "_" + d.accessories.id + "acessEdit" + fileEx;
                        var newPath = Path.Combine(Server.MapPath("~/uploadFiles"), newfilename);
                        d.accessories.photo = newPath;
                        d.file.SaveAs(newPath);
                        var extrapath = d.accessories.photo.Split('\\');
                        dress.photo = extrapath[extrapath.Length - 1];
                    }
                }
            }
            catch
            {

            }
            AccessDb.SaveChanges();
            return RedirectToAction("Accessories", "Accessories");
        }

        public ActionResult newEdit()
        {
            var editD = new ViewModelCategory();
            return View("newEdit", editD);
        }

        public ActionResult Delete(int id)
        {
            var d = AccessDb.accessories.SingleOrDefault(c => c.id == id);
            if (d == null)
                return HttpNotFound();
            return View(d);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult SaveDelete(int id)
        {
            var d = AccessDb.accessories.Find(id);
            AccessDb.accessories.Remove(d);
            AccessDb.SaveChanges();
            return RedirectToAction("Accessories", "Accessories");
        }
    }
}