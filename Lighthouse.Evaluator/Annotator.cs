using System.Collections.Generic;
using System.Linq;
using Lighthouse.Annotate.Models;
using Lighthouse.Storage;

namespace Lighthouse.Annotate
{
    public class Annotator
    {
        private static readonly LighthouseContext Db = new LighthouseContext();
        private static readonly IVariantRepository Variants = new VariantRepository(Db);
        private static readonly IRuleRepository Rules = new RuleRepository(Db);

        public static AnnotationModel Annotate(string caseId, string guid, string panel)
        {
            var rules = Rules.FindByPanel(panel);
            
            var result = new AnnotationModel()
            {
                CaseId = caseId,
                CaseAnnotations = new List<string>(),
                Panel = panel,
                VariantAnnotationModels = new List<VariantAnnotationModel>()
            };

            foreach (var rule in rules)
            {
                var matched = Variants.Query(guid, rule.Query);
                if (matched.Any())
                {
                    foreach (var annotation in matched)
                    {
                        switch (rule.AnnotationType)
                        {
                            case "case":
                                result.CaseAnnotations.Add(rule.Annotation);
                                break;
                            case "variant":
                                var variantAnnotation = new VariantAnnotationModel()
                                {
                                    Chromosome = annotation.Chromosome,
                                    Location = annotation.Location,
                                    Annotation = rule.Annotation
                                };
                                result.VariantAnnotationModels.Add(variantAnnotation);
                                break;
                        }
                    }
                }
            }

            return result;
        }
    }
}
