using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lighthouse.Evaluator.Parsers;
using Lighthouse.Web.Models;

namespace Lighthouse.Web.Controllers
{
    public class AnnotationController : Controller
    {
        // GET: Annotate
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateAnnotationModel model)
        {
            var guid = Guid.NewGuid().ToString("N");
            var tempPath = Server.MapPath("~/App_Data/guid");
            model.VcfFile.SaveAs(tempPath);

            var parser = new IonReporterParser();
            IList<Lighthouse.Evaluator.Models.VariantModel> variants;
            using (var sr = new StreamReader(tempPath))
            {
                variants = parser.ParseVariants(sr);
            }

            foreach (var variant in variants)
            {
                // TODO: add each variant to database
            }

            // TODO: load rules for submitted panel

            // TODO: generate annotations for VCF file

            // TODO: cleanup variants from DB and file from disk

            // TODO: return JSON response of variant annotations

            return View();
        }
    }
}