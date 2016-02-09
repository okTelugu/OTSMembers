﻿using System.Web;
using System.Web.Optimization;

namespace OTSMembers
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/OTSMembers").Include(
                        "~/Scripts/directoryformvalidation.js",
                        "~/Scripts/OTSDataTables.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-2.1.4.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/jquery.validate.unobtrusive.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                        "~/Scripts/DataTables/jquery.dataTables.min.js",
                        "~/Scripts/DataTables/dataTables.jqueryui.js",
                        "~/Scripts/DataTables/dataTables.tableTools.js"));

            bundles.Add(new StyleBundle("~/bundles/datatables/css").Include(
                        "~/Content/DataTables/css/jquery.dataTables.min.css",
                        "~/Content/DataTables/css/dataTables.jqueryui.css",
                        "~/Content/DataTables/css/dataTables.tableTools.css"
                        ));
//            •- jQuery-2.1.0.min.js
//•- jquery-ui.min.js
//•- jquery.dataTables.min.js
//•- dataTables.jqueryui.js
//•- dataTables.tableTools.js


            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                 "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-social.css",
                      "~/Content/font-awesome.css",
                      "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                          "~/Content/themes/base/jquery.ui.core.css",
                          "~/Content/themes/base/jquery.ui.resizable.css",
                          "~/Content/themes/base/jquery.ui.selectable.css",
                          "~/Content/themes/base/jquery.ui.accordion.css",
                          "~/Content/themes/base/jquery.ui.autocomplete.css",
                          "~/Content/themes/base/jquery.ui.button.css",
                          "~/Content/themes/base/jquery.ui.dialog.css",
                          "~/Content/themes/base/jquery.ui.slider.css",
                          "~/Content/themes/base/jquery.ui.tabs.css",
                          "~/Content/themes/base/jquery.ui.datepicker.css",
                          "~/Content/themes/base/jquery.ui.progressbar.css",
                          "~/Content/themes/base/jquery.ui.theme.css"));
            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //                     "~/Content/bootstrap-social.css"));
            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = false;

        }
    }
}
