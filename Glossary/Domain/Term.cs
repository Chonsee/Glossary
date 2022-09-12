using System.ComponentModel.DataAnnotations;

namespace Glossary.Domain
{
    public class Term : BaseEntity
    {
        [Required]
        public string TermNameOrPhrase { get; set; }

        [Required]
        public string Definition { get; set; }

    }
}