using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shopping.Models;
using Shopping.Migrations;
using Microsoft.AspNet.Identity;

namespace Shopping.Controllers
{

    public class AddToCartController : Controller
    {
        private ApplicationDbContext context;
        public AddToCartController()
        {
            context = new ApplicationDbContext();
        }
        public ActionResult addToCart(int? id, String name)
        {
            ViewBag.name = name;
            var incart = context.infoOfCart;
            var dress = context.Dresses.SingleOrDefault(d => d.id == id);
            var suit = context.suits.SingleOrDefault(d => d.id == id);
            var shorts = context.shorts.SingleOrDefault(d => d.id == id);
            var pants = context.pants.SingleOrDefault(d => d.id == id);
            var shirts = context.shirts.SingleOrDefault(d => d.id == id);
            var children = context.childrens.SingleOrDefault(d => d.id == id);
            var shoes = context.shoes.SingleOrDefault(d => d.id == id);
            var access = context.accessories.SingleOrDefault(d => d.id == id);
            //dresses
            if (id != null && name != null && name == "dresses")
            {
                var cart = new Models.infoOfCart()
                {
                    UserId = User.Identity.GetUserId(),
                    categoryId = dress.id,
                    nameCategory = name,
                    photo = dress.photo.ToString(),
                    price = (float)dress.newPrice,
                };
                context.infoOfCart.Add(cart);
                context.SaveChanges();
            }

            //suits
            else if (id != null && name != null && name == "suit")
            {
                var cart = new Models.infoOfCart()
                {
                    UserId = User.Identity.GetUserId(),
                    categoryId = suit.id,
                    nameCategory = name,
                    photo = suit.photo,
                    price = (float)suit.newPrice,
                };
                context.infoOfCart.Add(cart);
                context.SaveChanges();
            }

            //shorts
            if (id != null && name != null && name == "shorts")
            {
                var cart = new Models.infoOfCart()
                {
                    UserId = User.Identity.GetUserId(),
                    categoryId = shorts.id,
                    nameCategory = name,
                    photo = shorts.photo.ToString(),
                    price = (float)shorts.newPrice,
                };
                context.infoOfCart.Add(cart);
                context.SaveChanges();
            }

            //pants
            else if (id != null && name != null && name == "pants")
            {
                var cart = new Models.infoOfCart()
                {
                    UserId = User.Identity.GetUserId(),
                    categoryId = pants.id,
                    nameCategory = name,
                    photo = pants.photo.ToString(),
                    price = (float)pants.newPrice,
                };

                //check
                context.infoOfCart.Add(cart);
                context.SaveChanges();
            }

            //shirts
            if (id != null && name != null && name == "shirts")
            {
                var cart = new Models.infoOfCart()
                {
                    UserId = User.Identity.GetUserId(),
                    categoryId = shirts.id,
                    nameCategory = name,
                    photo = shirts.photo.ToString(),
                    price = (float)shirts.newPrice,
                };
                context.infoOfCart.Add(cart);
                context.SaveChanges();
            }

            //children
            if (id != null && name != null && name == "child")
            {
                var cart = new Models.infoOfCart()
                {
                    UserId = User.Identity.GetUserId(),
                    categoryId = children.id,
                    nameCategory = name,
                    photo = children.photo.ToString(),
                    price = (float)children.newPrice,
                };

                context.infoOfCart.Add(cart);
                context.SaveChanges();
            }

            //shoes
            if (id != null && name != null && name == "shoes")
            {
                var cart = new Models.infoOfCart()
                {
                    UserId = User.Identity.GetUserId(),
                    categoryId = shoes.id,
                    nameCategory = name,
                    photo = shoes.photo.ToString(),
                    price = (float)shoes.newPrice,
                };

                context.infoOfCart.Add(cart);
                context.SaveChanges();
            }

            //accrssories
            if (id != null && name != null && name == "access")
            {
                var cart = new Models.infoOfCart()
                {
                    UserId = User.Identity.GetUserId(),
                    categoryId = access.id,
                    nameCategory = name,
                    photo = access.photo.ToString(),
                    price = (float)access.newPrice,
                };

                context.infoOfCart.Add(cart);
                context.SaveChanges();
            }

            //to view total
            float tot = 0;
            foreach (var d in incart)
            {
                if (d.UserId == (String)User.Identity.GetUserId() && d.check == false)
                {
                    tot += (float)d.price;
                }
            }
            ViewBag.total = tot;

            return View(incart);
        }
        public ActionResult DeleteFromCart(int id, String name)
        {
            var del = context.infoOfCart.Find(id);           
            context.infoOfCart.Remove(del);
            context.SaveChanges();
            return RedirectToAction("addToCart", "AddToCart");
        }

        //check out
        public ActionResult Newcheck(float total)
        {
            var cart = context.infoOfCart.ToList();
            if (total == 0)
                return RedirectToAction("failedCart");
            var viewmodel = new viewModel
            {
                CheckOut = new Models.checkOut()
                {
                    Total = (float)total,
                    UserId = User.Identity.GetUserId()
                },
                InfoOfCart = cart
            };
            return View("Newcheck", viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SavecheckOut(Models.checkOut checkOut)
        {
            if (!ModelState.IsValid)   //validation
            {
                var viewmodel = new viewModel
                {
                    CheckOut = checkOut,
                    InfoOfCart = context.infoOfCart.ToList()
                };
                return View("Newcheck", viewmodel);
            }
            context.checkOut.Add(checkOut);
            context.SaveChanges();
            checkOut.nameUser += checkOut.id;
            foreach (var ch in context.infoOfCart)
            {
                if (User.Identity.GetUserId() == ch.UserId && ch.check==false)
                {
                    ch.check = true;
                    ch.nameUser = checkOut.nameUser;
                }
            }
            context.SaveChanges();
            return RedirectToAction("addToCart", "AddToCart");
        }

        public ActionResult failedCart()
        {
            return View();
        }

        public ActionResult Request()
        {
            var checkOut = context.checkOut.ToList();
            var cart = context.infoOfCart.ToList();
            ViewBag.cart = cart;
            ViewBag.checkout = checkOut;
            return View();
        }
        public ActionResult finishOrder(int id) //id of checkOut
        {
            var checkout = context.checkOut.Find(id);
                foreach(var cart in context.infoOfCart.ToList())
                {
                    if (checkout.UserId == cart.UserId && cart.check == true && checkout.FullName==cart.nameUser)
                    {
                        context.infoOfCart.Remove(cart);
                    }
                checkout.finish = true;
            }

            context.SaveChanges();
            return RedirectToAction("Request", "AddToCart");
        }

    }
}