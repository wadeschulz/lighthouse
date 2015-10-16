using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lighthouse.Storage.Models
{
    public class Variant
    {
        [Column("id")]
        public long Id { get; set; }
        public string CaseId { get; set; }
        public string Panel { get; set; }
        [Column("caseguid")]
        [MaxLength(255)]
        public string CaseGuid { get; set; }

        [Column("chr")]
        [MaxLength(255)]
        public string Chromosome { get; set; }
        [Column("loc")]
        public int Location { get; set; }
        [Column("ref")]
        public string ReferenceAllele { get; set; }
        [Column("rd")]
        public int ReadDepth { get; set; }
        
        [Column("gene")]
        public string Gene { get; set; }
        [Column("alt")]
        public string AlternateAllele { get; set; }
        [Column("altreads")]
        public int AlternateAlleleReads { get; set; }
        [Column("af")]
        public double AllelicFrequency { get; set; }
        [Column("chgvs")]
        [MaxLength(255)]
        public string CodingHgvs { get; set; }
        [Column("phgvs")]
        [MaxLength(255)]
        public string ProteinHgvs { get; set; }
        [Column("region")]
        [MaxLength(255)]
        public string Region { get; set; }
        [Column("effect")]
        [MaxLength(255)]
        public string Effect { get; set; }
        [Column("vartype")]
        [MaxLength(255)]
        public string VariantType { get; set; }
    }
}
