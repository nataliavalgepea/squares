﻿using System.Web;
using System.Web.Optimization;

namespace KongOrange.Squares.WebInterface
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/squareTool").Include(
                        "~/Scripts/grid.js",
                        "~/Scripts/resize.js",
                        "~/Scripts/carousel.js",
                        "~/Scripts/html2canvas.js")); //this is a library for saving a canvas as an image. It is a work in progress and therefore not included in the report.

            bundles.Add(new ScriptBundle("~/bundles/site").Include(
                        "~/Scripts/mobileMenu.js",
                        "~/Scripts/login.js"));  

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/style").Include(
                      "~/Content/style.css"));
        }
    }
}
