using System;
using System.Collections.Generic;
using System.Data.Entity;
using RfqEbr.Data.Contracts.Repository;
using RfqEbr.Data.Helpers;
using RfqEbr.Data.Repository;

namespace RfqEbr.Data
{
    /// <summary>
    /// A maker of Code Camper Repositories.
    /// </summary>
    /// <remarks>
    /// An instance of this class contains repository factory functions for different types.
    /// Each factory function takes an EF <see cref="DbContext"/> and returns
    /// a repository bound to that DbContext.
    /// <para>
    /// Designed to be a "Singleton", configured at web application start with
    /// all of the factory functions needed to create any type of repository.
    /// Should be thread-safe to use because it is configured at app start,
    /// before any request for a factory, and should be immutable thereafter.
    /// </para>
    /// </remarks>
    public class Factories : RepositoryFactories
    {
        public Factories()
            : base()
        {

        }

        protected override IDictionary<Type, Func<DbContext, object>> GetFactories()
        {
            return new Dictionary<Type, Func<DbContext, object>>
				{
					{typeof(IAuthorizeSystemMasterRepository), dbContext => new AuthorizeSystemMasterRepository(dbContext)},
					{typeof(ICustomRepository), dbContext => new CustomRepository(dbContext)},
				};
        }
    }
}
