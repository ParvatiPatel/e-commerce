using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
/**
 * Authors: Rutvik Patel, Ritesh Patel, Himanshu Patel and  Parvati Patel
 * Name: ErrorController.cs
 * Description: This file gives the user an appropriate error so that the user used dont see thecode behind if the site break
 */
namespace E_commerce.Controllers
{
    public class ErrorController : Controller
    {

        // GET: Error
        public ViewResult Index()
        {
            return View("Error");
        }

        public ViewResult NotFound()
        {
            Response.StatusCode = 404;
            return View("NotFound");
        }
    }
}