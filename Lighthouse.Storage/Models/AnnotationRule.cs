using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lighthouse.Storage.Models
{
    public class AnnotationRule
    {
        public long Id { get; set; }
        public IList<AnnotationRuleVersion> Versions { get; set; }
    }
}
