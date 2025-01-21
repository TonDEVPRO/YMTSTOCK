using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace RfqEbr.Data.Core
{
    public class ContextCore : DbContext
    {
        public static string ConnectionString = "MyDb";

        // ToDo: Move Initializer to Global.asax; don't want dependence on SampleData
        static ContextCore()
        {
            //#Wansao
            Database.SetInitializer<Context>(null);
        }

        public ContextCore()
            : base(nameOrConnectionString: ConnectionString)
        {
            //Configuration.ProxyCreationEnabled = false;
            //Configuration.LazyLoadingEnabled = false;
            //Configuration.ValidateOnSaveEnabled = false;
            Configuration.AutoDetectChangesEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Use singular table names
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}