using Glossary.Domain;
using Microsoft.EntityFrameworkCore;

namespace Glossary.Data
{
    /// <summary>
    /// This class represents the tables used by the Glossary Application.
    /// </summary>
    public class GlossaryContext : DbContext
    {
        /// <summary>
        /// This represents all terms in the Term table.
        /// </summary>
        public DbSet<Term> Terms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //Usually this would be a data connection string, not hardcoded within the application. 
            options.UseSqlite(@"Data Source=glossary.db");
        }
    }
}
