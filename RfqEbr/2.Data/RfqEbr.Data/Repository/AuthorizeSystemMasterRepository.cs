using System.Data.Entity;
using System.Linq;
using RfqEbr.Data.Contracts.Repository;
using RfqEbr.Data.Core;
using RfqEbr.Models.Custom;
using RfqEbr.Models.Table;

namespace RfqEbr.Data.Repository
{
    public class AuthorizeSystemMasterRepository : EfRepository<AuthorizeSystemMaster>, IAuthorizeSystemMasterRepository
    {
        public AuthorizeSystemMasterRepository(DbContext context) : base(context) { }
        public string GetText()
        {
            var sql = "SELECT A.SYSTEM_ID SystemId, A.SYSTEM_NAME SystemName FROM EIS.AUTHORIZE_SYSTEM_MSTR A WHERE A.SYSTEM_ID='ETAG' ";
            var x = DbContext.Database.SqlQuery<Test>(sql).FirstOrDefault();
            return x.SystemName;
        }
    }
}
