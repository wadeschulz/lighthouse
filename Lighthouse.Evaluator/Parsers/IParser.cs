using System.Collections.Generic;
using System.IO;
using Lighthouse.Annotate.Models;

namespace Lighthouse.Annotate.Parsers
{
    public interface IParser
    {
        IList<VariantModel> ParseVariants(StreamReader vcfFile);
    }
}
