using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Lighthouse.Web.Models
{
    public class CreateAnnotation
    {
        public string Rules { get; set; }
        public List<SelectListItem> AvailableAnnotationTypes { get; set; }

        [Required]
        [Display(Name = "Annotation Type")]
        public int SelectedAnnotationType { get; set; }

        [Required]
        [Display(Name = "Annotation")]
        public string Annotation { get; set; }
    }

    public class CaseAnnotation
    {
        public string CaseId { get; set; }
        public string CaseInterpretation { get; set; }
        public IList<VariantAnnotation> VariantAnnotations { get; set; }
    }

    public class VariantAnnotation
    {
        public string VariantLocation { get; set; }
        public string Annotation { get; set; }
    }
}
