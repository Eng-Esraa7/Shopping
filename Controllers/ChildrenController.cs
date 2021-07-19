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
    public class ChildrenController : Controller
    {
        // GET: Children
        private ApplicationDbContext childDb;
        public ChildrenController()
        {
            childDb = new ApplicationDbContext();
        }
        public ActionResult children(String searchBy, String Searching, int? page)
        {
            var sh = childDb.childrens.ToList().ToPagedList(page ?? 1, 3);
            if (searchBy == "color")
            {
                return View(childDb.childrens.Where(x => x.description.Contains(Searching) || Searching == null).ToList().ToPagedList(page ?? 1, 3));
            }
            else
            {
                return View(childDb.childrens.Where(x => x.newPrice.ToString().Contains(Searching) || Searching == null).ToList().ToPagedList(page ?? 1, 3));
            }
            return View(sh);
        }
        public ActionResult Details(int id)
        {
            var sh = childDb.childrens.SingleOrDefault(m => m.id == id);
            if (sh == null)
                return HttpNotFound();
            return View(sh);
        }
        /*add children*/
        public ActionResult New()
        {
            var newDress = new ViewModelCategory()
            {
                children = new children()
            };
            return View("New", newDress);
        }

        [HttpPost]
        public ActionResult Save(ViewModelCategory d)
        {
            if (!ModelState.IsValid)   //validation
            {
                var child = new children
                {
                    photo = d.children.photo,
                    oldPrice = d.children.oldPrice,
                    newPrice = d.children.newPrice,
                    description = d.children.description,
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
                string newfilename = name + "_" + d.children.id + d.children.newPrice.ToString() + "child" + fileEx;
                var newPath = Path.Combine(Server.MapPath("~/uploadFiles"), newfilename);
                d.children.photo = newPath;
                d.file.SaveAs(newPath);
                var extrapath = d.children.photo.Split('\\');
                d.children.photo = extrapath[extrapath.Length - 1];
                childDb.childrens.Add(d.children);
                childDb.SaveChanges();
            }

            return RedirectToAction("children", "Children");
        }

        /*Edit*/
        public ActionResult Edit(int id)
        {
            var d = childDb.childrens.SingleOrDefault(c => c.id == id);
            if (d == null)
            {
                return HttpNotFound();
            }
            ViewModelCategory viewmodel = new ViewModelCategory()
            {
                children = d
            };
            return View("newEdit", viewmodel);
        }

        public ActionResult SaveEdit(ViewModelCategory d)
        {
            if (!ModelState.IsValid)   //validation
            {
                var child = new children
                {
                    photo = d.children.photo,
                    oldPrice = d.children.oldPrice,
                    newPrice = d.children.newPrice,
                    description = d.children.description,
                };
                return View("newedit", d);
            }

            var dress = childDb.childrens.Single(m => m.id == d.children.id);
            dress.photo = dress.photo;
            dress.newPrice = d.children.newPrice;
            dress.oldPrice = d.children.oldPrice;
            dress.description = d.children.description;

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
                        string newfilename = name + "_" + d.children.id + "childEdit" + fileEx;
                        var newPath = Path.Combine(Server.MapPath("~/uploadFiles"), newfilename);
                        d.children.photo = newPath;
                        d.file.SaveAs(newPath);
                        var extrapath = d.children.photo.Split('\\');
                        dress.photo = extrapath[extrapath.Length - 1];
                    }
                }
            }
            catch
            {

            }
            childDb.SaveChanges();
            return RedirectToAction("children", "Children");
        }

        public ActionResult newEdit()
        {
            var editD = new ViewModelCategory();
            return View("newEdit", editD);
        }

        public ActionResult Delete(int id)
        {
            var d = childDb.childrens.SingleOrDefault(c => c.id == id);
            if (d == null)
                return HttpNotFound();
            return View(d);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult SaveDelete(int id)
        {
            var d = childDb.childrens.Find(id);
            childDb.childrens.Remove(d);
            childDb.SaveChanges();
            return RedirectToAction("children", "Children");
        }

    }
}