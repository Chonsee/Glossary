using Glossary.Data;
using Glossary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Glossary
{
    /// <summary>
    /// Service class used for interacting with the Term table
    /// </summary>
    public class TermService
    {
        /// <summary>
        /// Adds a term
        /// </summary>
        /// <param name="term">The term to be added.</param>
        /// <exception cref="ArgumentNullException">Term cannot be null</exception>
        public void Add(Term term)
        {
            if(term == null)
            {
                throw new ArgumentNullException(nameof(Term));
            }

            using (var db = new GlossaryContext())
            {
                db.Add(term);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Updates an exising term
        /// </summary>
        /// <param name="term">The term to be updated</param>
        /// <exception cref="ArgumentNullException">Term cannot be null</exception>
        public void Update(Term term)
        {
            if (term == null)
            {
                throw new ArgumentNullException(nameof(Term));
            }

            using (var db = new GlossaryContext())
            {
                db.Update(term);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes a Term from the DB
        /// </summary>
        /// <param name="ArgumentNullException">Term cannot be null</param>
        public void Delete(Term term)
        {
            if (term == null)
            {
                throw new ArgumentNullException(nameof(Term));
            }

            using (var db = new GlossaryContext())
            {
                db.Remove(term);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Gets a term by its primary key Id. 
        /// </summary>
        /// <param name="id">The unique identifier of this term.</param>
        /// <returns>The term with an equivelant Id</returns>
        /// <exception cref="ArgumentException">id must be valid</exception>
        public Term GetTermById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(Term));
            }

            Term term;
            using (var db = new GlossaryContext())
            {
                term = db.Terms.Where(x => x.Id == id).FirstOrDefault();
            }

            return term;
        }

        /// <summary>
        /// Returns all Terms in the table. 
        /// </summary>
        /// <param name="alphebetical">true to order by TermNameOrPhrase ascending, false to order by Id ascending.</param>
        /// <returns>All Terms in the table.</returns>
        public IList<Term> GetAllTerms(bool alphebetical = true)
        {
            IList<Term> terms = new List<Term>();
            using (var db = new GlossaryContext())
            {
                terms = alphebetical ? db.Terms.OrderBy(x => x.TermNameOrPhrase).ToList() : db.Terms.ToList();
            }
            return terms;
        }
    }
}
