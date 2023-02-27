using MahApps.Metro.Controls.Dialogs;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class BasketController : Controller
    {
        IRepository<Customer> customers;
        IBasketService basketService;
        IOrderService orderService;
        public BasketController(IBasketService BasketService, IOrderService OderService, IRepository<Customer> Customers)
        {
            this.basketService = BasketService;
            this.orderService = OderService;
            this.customers = Customers;
        }


        // GET: Basket
        public ActionResult Index()
        {
            var model = basketService.GetBasketItems(this.HttpContext);
            return View(model);
        }

        public ActionResult AddToBasket(string Id)
        {
            basketService.AddToBasket(this.HttpContext, Id);
            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromBasket(string Id)
        {
            basketService.RemoveFromBasket(this.HttpContext, Id);
            return RedirectToAction("Index");
        }

        public PartialViewResult BasketSummary()
        {
            var basketSummary = basketService.GetBasketSummary(this.HttpContext);

            return PartialView(basketSummary);
        }

        [Authorize]
        public ActionResult Checkout()
        {
            Customer customer = customers.Collection().FirstOrDefault(c => c.Email == User.Identity.Name);

            if (customer != null)
            {
                Order order = new Order()
                {
                    Email = customer.Email,
                    City = customer.City,
                    State = customer.State,
                    Street = customer.Street,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    ZipCode = customer.ZipCode
                };

                while (basketService.GetBasketItems(this.HttpContext).Count != 0)
                {
                    return View(order);
                }
                return RedirectToAction("Index", "Home");
               /* if (basketService.GetBasketItems(this.HttpContext).Count != 0)
                {
                    return View(order);
                }
                else
                {
                    
                    ModelState.AddModelError("Key", "Cart is Empty..!");
                    *//*return RedirectToAction("Empty_Cart", "Basket");
                    return RedirectToAction("Index", "Home");*//*
                }*/

            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        public ActionResult Empty_Cart()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Checkout(Order order)
        {
            var basketItems = basketService.GetBasketItems(this.HttpContext);
            order.OrderStatus = "Order Created";
            order.Email = User.Identity.Name;

            //process payment

            order.OrderStatus = "Payment Processed";
            orderService.CreateOrder(order, basketItems);
            basketService.ClearBasket(this.HttpContext);

            return RedirectToAction("Thankyou",new { OrderId = order.Id});
        }

        public ActionResult Thankyou(string OrderId)
        {
            ViewBag.OrderId = OrderId;
            return View();
        }
    }
}