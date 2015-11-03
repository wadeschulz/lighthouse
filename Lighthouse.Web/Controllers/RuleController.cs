using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lighthouse.Storage;
using Lighthouse.Storage.Models;
using Lighthouse.Web.Models;

namespace Lighthouse.Web.Controllers
{
    public class RuleController : Controller
    {
        private readonly LighthouseContext _db = new LighthouseContext();
        private readonly IRuleRepository _rules;

        public RuleController()
        {
            _rules = new RuleRepository(_db);
        }

        // GET: Annotation
        public ActionResult Index()
        {
            var rules = _rules.FindAll();

            return View(rules.ToList());
        }

        public ActionResult Create()
        {
            var variantAnnotation = new SelectListItem() { Text = "Variant", Value = "variant" };
            var caseAnnotation = new SelectListItem() { Text = "Case", Value = "case" };
            var bpaAnnotation = new SelectListItem() { Text = "Best Practice Alert", Value = "bpa" };

            var model = new CreateRule()
            {
                AvailableAnnotationTypes = new List<SelectListItem>() { variantAnnotation, caseAnnotation, bpaAnnotation }
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CreateRule model)
        {
            var rule = new AnnotationRule()
            {
                Annotation = model.Annotation,
                AnnotationType = model.SelectedAnnotationType,
                Description = model.Description,
                Name = model.Name,
                Panel = model.Panel,
                Query = model.Query
            };
            _rules.Create(rule);

            return RedirectToAction("Index");
        }

        public ActionResult Validate(long id)
        {
            _rules.ValidateRule(id);
            return RedirectToAction("Index");
        }

        public ActionResult Archive(long id)
        {
            _rules.ArchiveRule(id);
            return RedirectToAction("Index");
        }
    }
}