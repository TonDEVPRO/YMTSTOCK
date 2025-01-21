using System.Data.Entity.ModelConfiguration;
using RfqEbr.Models.Table;

namespace RfqEbr.Data.Configuration
{
    public class AuthorizeSystemMasterConfiguration : EntityTypeConfiguration<AuthorizeSystemMaster>
    {
        public AuthorizeSystemMasterConfiguration()
        {
            ToTable("EIS.AUTHORIZE_SYSTEM_MSTR");
            Property(t => t.SystemId).HasColumnName("SYSTEM_ID");
            Property(t => t.SystemName).HasColumnName("SYSTEM_NAME");
            Property(t => t.SystemDesc).HasColumnName("SYSTEM_DESC");
            Property(t => t.Url).HasColumnName("URL");
            Property(t => t.IsOpen).HasColumnName("IS_OPEN");
            Property(t => t.FlagStatus).HasColumnName("FLAG_STATUS");
            HasKey(t => t.SystemId);
        }
    }
}
