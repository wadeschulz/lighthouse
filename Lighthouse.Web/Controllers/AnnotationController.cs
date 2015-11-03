using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lighthouse.Annotate;
using Lighthouse.Annotate.Models;
using Lighthouse.Annotate.Parsers;
using Lighthouse.Storage;
using Lighthouse.Storage.Models;
using Lighthouse.Web.Models;
using Newtonsoft.Json;

namespace Lighthouse.Web.Controllers
{
    public class AnnotationController : Controller
    {
        private readonly IVariantRepository _variants;

        public AnnotationController()
        {
            var db = new LighthouseContext();
            _variants = new VariantRepository(db);
        }

        // GET: Annotate
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateAnnotationModel model)
        {
            var guid = Guid.NewGuid().ToString("N");
            var tempPath = Server.MapPath("~/App_Data/" + guid);
            model.VcfFile.SaveAs(tempPath);

            var parser = new IonReporterParser();
            IList<VariantModel> variants;
            using (var sr = new StreamReader(tempPath))
            {
                variants = parser.ParseVariants(sr);
            }

            var variantList = new List<Variant>();

            foreach (var variant in variants)
            {
                variantList.AddRange(variant.AlternateAlleles.Select(effect => new Variant()
                {
                    CaseId = model.CaseId,
                    CaseGuid = guid,
                    Panel = model.Panel,
                    Chromosome = variant.Chromosome,
                    Location = variant.Location,
                    ReferenceAllele = variant.ReferenceAllele,
                    ReadDepth = variant.TotalReads,
                    AlternateAllele = effect.AlternateAllele,
                    AlternateAlleleReads = effect.AlleleReads,
                    AllelicFrequency = effect.AlleleFrequency,
                    CodingHgvs = effect.VariantFunction.HgvsCoding,
                    ProteinHgvs = effect.VariantFunction.HgvsProtein,
                    Region = effect.VariantFunction.Location, // exonic, intronic, etc
                    Effect = effect.VariantFunction.Function, // synonymous, missense, etc
                    //VariantType = effect.VariantFunction., // indel, snp, etc
                    Gene = effect.VariantFunction.Gene
                }));
            }

            _variants.Create(variantList);
            
            var result = Annotator.Annotate(model.CaseId, guid, model.Panel);

            _variants.Delete(guid);
            System.IO.File.Delete(tempPath);

            return new JsonResult {ContentType = "application/json", Data = JsonConvert.SerializeObject(result)};
        }
    }
}