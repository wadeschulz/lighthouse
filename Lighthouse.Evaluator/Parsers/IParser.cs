using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lighthouse.Evaluator.Parsers
{
    public interface IParser
    {
        IList<Lighthouse.Evaluator.Models.VariantModel> ParseVariants(StreamReader vcfFile);
    }
}
