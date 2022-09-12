namespace Glossary.Models
{
    public class TermModel : BaseGlossaryModel
    {
        public string TermNameOrPhrase { get; set; }

        public string Definition { get; set; }

        public override bool IsValid()
        {
            if(string.IsNullOrWhiteSpace(TermNameOrPhrase))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(Definition))
            {
                return false;
            }

            return true;
        }
    }
}