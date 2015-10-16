using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lighthouse.Storage.Models
{
    public class Variant
    {
        public long Id { get; set; }
        public string CaseId { get; set; }
        public string Panel { get; set; }
        
        [MaxLength(255)]
        public string CaseGuid { get; set; }

        [MaxLength(255)]
        public string Chromosome { get; set; }
        public int Location { get; set; }
        public string ReferenceAllele { get; set; }
        public int ReadDepth { get; set; }
        [MaxLength(255)]
        public string Gene { get; set; }
        public string AlternateAllele { get; set; }
        public int AlternateAlleleReads { get; set; }
        public double AllelicFrequency { get; set; }
        [MaxLength(255)]
        public string CodingHgvs { get; set; }
        [MaxLength(255)]
        public string ProteinHgvs { get; set; }
        [MaxLength(255)]
        public string Region { get; set; }
        [MaxLength(255)]
        public string Effect { get; set; }
        [MaxLength(255)]
        public string VariantType { get; set; }
    }
}
