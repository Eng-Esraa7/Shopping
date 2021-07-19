using PagedList;
using Shopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Shopping.Controllers
{
    public class ContactUsController : Controller
    {
        ApplicationDbContext context;
        public ContactUsController()
        {
            context = new ApplicationDbContext();
        }
        // GET: ContactUs
        public ActionResult Index(int?page)
        {
            return View(context.contactUs.ToList().ToPagedList(page ?? 1, 8));
        }

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View(new ContactUs());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ContactUs model)
        {
            if (ModelState.IsValid)
            {
                context.contactUs.Add(model);
                await context.SaveChangesAsync();
                return RedirectToAction("Success", "ContactUs");
            }
            return View(model);
        }
    }
    }