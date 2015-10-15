using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarBase.Binf.NextGenParser.Models.IonReporter;

namespace Lighthouse.Evaluator.Models
{
    public class VariantModel
    {
        public string Chromosome { get; set; }
        public int Location { get; set; }
        public string ReferenceAllele { get; set; }
        public int ReferenceReads { get; set; }
        public int TotalReads { get; set; }

        public IList<AlternateAlleleModel> AlternateAlleles { get; set; } 
    }

    public class AlternateAlleleModel
    {
        public string AlternateAllele { get; set; }
        public int AlleleReads { get; set;}
        public double AlleleFrequency { get; set; }
        public VcfVariantFunction VariantFunction { get; set; }
    }
}
