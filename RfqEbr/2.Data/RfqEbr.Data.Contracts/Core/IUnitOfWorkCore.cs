
namespace RfqEbr.Data.Contracts.Core
{
    /// <summary>
    /// Interface for the "Unit of Work"
    /// </summary>
    public interface IUnitOfWorkCore
    {
        // Save pending changes to the data store.
        void Commit();
    }
}