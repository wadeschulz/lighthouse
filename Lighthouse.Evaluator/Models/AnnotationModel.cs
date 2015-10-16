using System.Collections.Generic;

namespace Lighthouse.Annotate.Models
{
    public class AnnotationModel
    {
        public string CaseId { get; set; }
        public string Panel { get; set; }
        public IList<string> CaseAnnotations { get; set; }
        public IList<VariantAnnotationModel> VariantAnnotationModels { get; set; }
    }

    public class VariantAnnotationModel
    {
        public string Chromosome { get; set; }
        public int Location { get; set; }
        public string Annotation { get; set; }
    }
}
