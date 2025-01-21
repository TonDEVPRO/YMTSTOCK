using System.Data.Entity.ModelConfiguration;
using RfqEbr.Models.Table;
using System.ComponentModel.DataAnnotations.Schema;

namespace RfqEbr.Data.Configuration
{
      [Table("EmployeeInnoValuesDB", Schema = "admin")]
    public class EmployeeInnoConfiguration : EntityTypeConfiguration<EmployeeInno>
    {
             
        public EmployeeInnoConfiguration()
        {
            ToTable("EmployeeInnoValuesDB");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.EmpNo).HasColumnName("EmpNo");
            Property(t => t.EmpNameTh).HasColumnName("EmpNameTh");
            Property(t => t.EmpNameEng).HasColumnName("EmpNameEng");
            Property(t => t.DivisionCode).HasColumnName("DivisionCode");
            Property(t => t.DivsionName).HasColumnName("DivsionName");
            Property(t => t.DepartmentCode).HasColumnName("DepartmentCode");
            Property(t => t.DepartmentName).HasColumnName("DepartmentName");
            Property(t => t.SectionCode).HasColumnName("SectionCode");
            Property(t => t.SectionName).HasColumnName("SectionName");
            Property(t => t.PositionCode).HasColumnName("PositionCode");
            Property(t => t.PositionName).HasColumnName("PositionName");
            Property(t => t.StartDate).HasColumnName("StartDate");
            Property(t => t.ProbationDate).HasColumnName("ProbationDate");
           
            Property(t => t.Type).HasColumnName("Type");
            Property(t => t.CreateDate).HasColumnName("CreateDate");
            Property(t => t.CreateBy).HasColumnName("CreateBy");
            Property(t => t.Remark).HasColumnName("Remark");
            Property(t => t.Status).HasColumnName("Status");
            HasKey(t => t.Id);
        }
    }
}
