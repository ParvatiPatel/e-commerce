﻿using System.Web;
using System.Web.Optimization;

namespace E_commerce
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                       "~/Scripts/bxslider.min.js",
                        "~/Scripts/jquery.easing.1.3.min.js",
                         "~/Scripts/jquery.sticky.js",
                          "~/Scripts/main.js",
                           "~/Scripts/owl.carousel.min.js",
                            "~/Scripts/script.slider.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/shoppingCart").Include(
                      "~/Scripts/app.js"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                       "~/Content/font-awesome.css",
                        "~/Content/owl.carousel.css",
                         "~/Content/responsive.css",
                      "~/Content/site.css"));
        }
    }
}
