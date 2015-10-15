using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Lighthouse.Web.Models
{
    public class CreateAnnotationModel
    {
        public string Panel { get; set; }
        public string CaseId { get; set; }
        public HttpPostedFileBase VcfFile { get; set; }
    }

    public class AnnotationResultModel
    {
        public string CaseId { get; set; }
        public string Panel { get; set; }
        public IList<string> CaseAnnotations { get; set; }
        public IList<VariantAnnotationModel> VariantAnnotationModels { get; set; } 
    }

    public class VariantAnnotationModel
    {
        public string Chromosome { get; set; }
        public string Location { get; set; }
        public IList<string> VariantAnnotations { get; set; } 
    }
}