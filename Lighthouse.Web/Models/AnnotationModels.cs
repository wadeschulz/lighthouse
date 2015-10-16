using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Lighthouse.Web.Models
{
    public class CreateAnnotationModel
    {
        [Required]
        [Display(Name = "Panel")]
        public string Panel { get; set; }
        [Required]
        [Display(Name = "Deidentified Case ID")]
        public string CaseId { get; set; }
        [Required]
        [Display(Name = "VCF File")]
        public HttpPostedFileBase VcfFile { get; set; }
    }
}