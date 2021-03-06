﻿using System.Web.Optimization;

namespace MvcPL
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

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
            bundles.Add(new ScriptBundle("~/bundles/fineuploader")
                    .Include("~/Scripts/fine-uploader/header.js")
                    .Include("~/Scripts/fine-uploader/util.js")
                    .Include("~/Scripts/fine-uploader/button.js")
                    .Include("~/Scripts/fine-uploader/ajax.requester.js")
                    .Include("~/Scripts/fine-uploader/deletefile.ajax.requester.js")
                    .Include("~/Scripts/fine-uploader/handler.base.js")
                    .Include("~/Scripts/fine-uploader/window.receive.message.js")
                    .Include("~/Scripts/fine-uploader/handler.form.js")
                    .Include("~/Scripts/fine-uploader/handler.xhr.js")
                    .Include("~/Scripts/fine-uploader/uploader.basic.js")
                    .Include("~/Scripts/fine-uploader/dnd.js")
                    .Include("~/Scripts/fine-uploader/uploader.js")
                    .Include("~/Scripts/fine-uploader/jquery-plugin.js")
                    );
            bundles.Add(new StyleBundle("~/Content/css/fineuploader")
                             .Include("~/Content/fineuploader.css"));
        }
    }
}