namespace Glossary.Models
{
    public class BaseGlossaryModel : IBaseModel
    {
        public int Id { get; set; }

        public virtual bool IsValid()
        {
            throw new System.NotImplementedException();
        }
    }
}