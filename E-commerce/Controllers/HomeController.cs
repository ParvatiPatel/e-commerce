using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SendGrid;
using System.Net.Mail;
using E_commerce.ViewModels;
namespace E_commerce.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "";

            return View();
        }
        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact([Bind(Include = "Name,Email,Phone,Message")] Mail mail)
        {
            // Create the email object first, then add the properties.
            try
            {
            

                SendGridMessage myMessage = new SendGridMessage();
                myMessage.AddTo(mail.Email);
                myMessage.From = new MailAddress("testwebritz@gmail.com", "Ritesh Patel");
                myMessage.Subject = "Contact from Game Tracking";
                myMessage.Text = " Hi " + mail.Name + "\n\n Thank you for visiting my Game Tracking\n\n Thank you. ";

                // Create a Web transport, using API Key
                var transportWeb = new Web("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");

                // Send the email.
                transportWeb.DeliverAsync(myMessage);
                // NOTE: If your developing a Console Application, use the following so that the API call has time to complete
                // transportWeb.DeliverAsync(myMessage).Wait();
               
            }
            catch (Exception) { }
            ModelState.Clear();
            ViewBag.Message = "Thank you for your comment.";
            return View();
        }
        public ActionResult AllProducts()
        {
            return View();
        }
    }
}