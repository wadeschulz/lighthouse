using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lighthouse.Storage;
using Lighthouse.Web.Models;

namespace Lighthouse.Web.Controllers
{
    public class AnnotationController : Controller
    {
        private readonly LighthouseContext _db = new LighthouseContext();
        private readonly IVariantRepository _variants;

        public AnnotationController()
        {
            _variants = new VariantRepository(_db);
        }

        // GET: Annotation
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            var variantAnnotation = new SelectListItem() { Text = "Variant", Value = "variant" };
            var caseAnnotation = new SelectListItem() { Text = "Case", Value = "case" };
            var model = new CreateAnnotation()
            {
                AvailableAnnotationTypes = new List<SelectListItem>() { variantAnnotation, caseAnnotation }
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CreateAnnotation model)
        {
            return RedirectToAction("Index");
        }
    }
}