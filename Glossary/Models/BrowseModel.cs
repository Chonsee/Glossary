using Glossary.Domain;
using System.Collections.Generic;

namespace Glossary.Models
{
    public class BrowseModel : BaseGlossaryModel
    {
        public IList<Term> TermModels { get; set; }

        public BrowseModel()
        {
            TermModels = new List<Term>();
        }
    }
}
