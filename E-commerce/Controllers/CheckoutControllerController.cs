using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E_commerce.Models;
using Rotativa;
using ReportViewerForMvc;
using System.Web.Mvc.Html;
/**
* Authors: Rutvik Patel, Ritesh Patel, Himanshu Patel and  Parvati Patel
* Name: CheckoutControllerController.cs
* Description: This file mainly controls the checkout activity of the user it add the item to cart, remove from cart and finally gives option for checkout.
*/
namespace E_commerce.Controllers
{
    //[Authorize]
    public class CheckoutControllerController : Controller
    {
        EcommerceModel storeDB = new EcommerceModel();
        const string PromoCode = "FREE";// the checkout is only successful if we enter free in promo code
        
        //
         // GET: /Checkout/AddressAndPayment
        public ActionResult AddressAndPayment()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            ViewBag.Total = cart.GetTotal();
            return View();
        }

        //
        // POST: /Checkout/AddressAndPayment
        [HttpPost]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            var order = new Order();
            TryUpdateModel(order);

            try
            {
                if (string.Equals(values["PromoCode"], PromoCode,
                    StringComparison.OrdinalIgnoreCase) == false)
                {
                    return View(order);
                }
                else
                {
                    order.Username = User.Identity.Name;
                    order.OrderDate = DateTime.Now;

                    //Save Order
                    storeDB.Orders.Add(order);
                    storeDB.SaveChanges();
                    //Process the order
                    var cart = ShoppingCart.GetCart(this.HttpContext);
                    cart.CreateOrder(order);

                    return RedirectToAction("Complete",
                        new { id = order.OrderId });
                }
            }
            catch
            {
                //Invalid - redisplay with errors
                return View(order);
            }
        }

        //
        // GET: /Checkout/Complete
        [AllowAnonymous]
        public ActionResult Complete(int id)
        {
            // Validate customer owns this order
            bool isValid = storeDB.Orders.Any(
                o => o.OrderId == id );
            OrderDetail Details = storeDB.OrderDetails.Find(id);
            
            Order Detail = storeDB.Orders.Include("OrderDetails").Single(o => o.OrderId == id);
            if (isValid)
            {
                return View(Detail);
            }
            else
            {
                return View("Error");
            }
        }
        [ChildActionOnly]
        public ActionResult CompleteCheckout(int id)
        {
            // Validate customer owns this order
            bool isValid = storeDB.Orders.Any(
                o => o.OrderId == id);
            OrderDetail Details = storeDB.OrderDetails.Find(id);
            Order Detail = storeDB.Orders.Include("OrderDetails").Single(o => o.OrderId == id);
            if (isValid)
            {
                return PartialView(Detail);
            }
            else
            {
                return View("Error");
            }
        }
        public Order CompletevCheckout(int id=9)
        {
            // Validate customer owns this order
            bool isValid = storeDB.Orders.Any(
                o => o.OrderId == id);
            OrderDetail Details = storeDB.OrderDetails.Find(id);
            Order Detail = storeDB.Orders.Include("OrderDetails").Single(o => o.OrderId == id);
          
                return Detail;
          
        }
        public ActionResult ExportToPDF(int id)
        {

            return new Rotativa.ActionAsPdf("Complete",new { id = id})
            {
                FileName = Server.MapPath("~/Content/Invoice.pdf")
            };
        }

        public ActionResult OrderList()
        {
            List<Order> ordelist = storeDB.Orders.Where(o => o.Username == User.Identity.Name).ToList();

            return View(ordelist);
        }

    }

}