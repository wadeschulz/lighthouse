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
}