using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public ActionResult Create(CreateAnnotation model)
        {
            
        }
    }
}