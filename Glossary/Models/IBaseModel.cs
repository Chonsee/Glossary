namespace Glossary.Models
{
    interface IBaseModel
    {
        /// <summary>
        /// Tells you if the model being used in its current configuration is valid or not. 
        /// </summary>
        /// <returns></returns>
        bool IsValid();
    }
}
