using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lighthouse.Storage.Models
{
    public class AnnotationRuleVersion
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Query { get; set; }
        public string AnnotationType { get; set; }
        public string Annotation { get; set; }
        public DateTime CreatedUtc { get; set; }
        public DateTime? ValidatedUtc { get; set; }
        public bool Validated { get; set; }
    }
}
