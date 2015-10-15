using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lighthouse.Evaluator.Models;
using Newtonsoft.Json;
using VarBase.Binf.NextGenParser.Models.IonReporter;
using VarBase.Binf.NextGenParser.Models.VariantCallFormat;
using VarBase.Binf.NextGenParser.Parsers;

namespace Lighthouse.Evaluator.Parsers
{
    public class IonReporterParser : IParser
    {
        public IList<VariantModel> ParseVariants(StreamReader vcfFile)
        {
            var variants = new List<VariantModel>();

            // Pass VCF StreamReader to VarBase NGS Parser
            var parser = new VariantCallFormatParser();
            var result = parser.ParseVariantCallFormat(vcfFile) as VariantCallFormatFile;

            // Create variant model for each item listed in file
            foreach (var variant in result.Variants)
            {
                // Set base variant information
                var entry = new VariantModel()
                {
                    Chromosome = variant.Chromosome,
                    Location = variant.Position,
                    ReferenceAllele = variant.ReferenceAllele,
                    AlternateAlleles = new List<AlternateAlleleModel>()
                };

                // Split out allele information for multiple variants at same location (comma separated in IonReporter VCF)
                var variantAlleles = variant.VariantAllele.Split(',');
                var altAlleleObs = variant.VariantCallInfo["FAO"].Value.ToString().Split(',').Select(o => Convert.ToInt32(o)).ToList();

                // Calculate total reference reads and allelic frequency for each variant
                entry.ReferenceReads = Convert.ToInt32(variant.VariantCallInfo["FRO"].Value);
                entry.TotalReads = entry.ReferenceReads + altAlleleObs.Sum();
                var freqs = altAlleleObs.Select(o => (double)o / (double)entry.TotalReads).ToList();

                // For annotated IonReporter VCF, grab variant annotation
                var variantAnnotation = JsonConvert.DeserializeObject<List<VcfVariantFunction>>(variant.VariantCallInfo["FUNC"].Value as string);

                // Generate annotation model for each alternate allele
                for (var i = 0; i < variantAlleles.Count(); i++)
                {
                    var altAllele = new AlternateAlleleModel()
                    {
                        AlternateAllele = variantAlleles[i],
                        AlleleReads = altAlleleObs[i],
                        AlleleFrequency = freqs[i]
                    };

                    // IR does a weird mapping (no mapping) for multiple effects/variant as well as multiple variants/effect.
                    // This should classify appropriately for each variant, 
                    // but will miss multiple effects/variant (upstream/downstream mutations)
                    var curFunc = i;
                    if (curFunc >= variantAnnotation.Count)
                    {
                        curFunc = variantAnnotation.Count - 1;
                    }
                    altAllele.VariantFunction = variantAnnotation[curFunc];
                    entry.AlternateAlleles.Add(altAllele);
                }
                
                // Add variant to list if the filter is passed
                if (variant.Filter.Equals("PASS"))
                {
                    variants.Add(entry);
                }
            }

            return variants;
        }
    }
}
