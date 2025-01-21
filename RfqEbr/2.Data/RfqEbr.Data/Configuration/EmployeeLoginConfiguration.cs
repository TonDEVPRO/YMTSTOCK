using System.Data.Entity.ModelConfiguration;
using RfqEbr.Models.Table;
using System.ComponentModel.DataAnnotations.Schema;

namespace RfqEbr.Data.Configuration
{
    public class EmployeeLoginConfiguration : EntityTypeConfiguration<EmployeeLogin>
    {

        public EmployeeLoginConfiguration()
        {
            ToTable("EmployeeLogin");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.EmpNo).HasColumnName("EmpNo");
            Property(t => t.EmpName).HasColumnName("EmpName");
            Property(t => t.EmpPass).HasColumnName("EmpPass");
            Property(t => t.EmpStatus).HasColumnName("EmpStatus");
            HasKey(t => t.Id);
        }
    }
}
