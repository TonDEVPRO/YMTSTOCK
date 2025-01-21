using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using RfqEbr.Models.Table;
using System.Reflection;

namespace RfqEbr.Data.Configuration
{

    public class YMTGUserConfiguration : EntityTypeConfiguration<YMTGUser>
    {

        public YMTGUserConfiguration()
        {
            ToTable("YMTG_USER");
            // กำหนดชื่อของตาราง
   

            // กำหนดคีย์หลัก
           HasKey(e => e.Id);

            // กำหนดการแมปของคอลัมน์กับ Properties
           Property(e => e.Id)
                .HasColumnName("Id")
            .IsRequired();

           Property(e => e.YPTUser)
                .HasColumnName("YPTUser")
                .HasMaxLength(50)
                .IsRequired();  // คอลัมน์นี้เป็น Required (Not Null)

           Property(e => e.YPTName)
                .HasColumnName("YPTName")
                .HasMaxLength(100);

            Property(e => e.YPTLevel)
                 .HasColumnName("YPTLevel");


           Property(e => e.YPTPass)
                .HasColumnName("YPTPass")
                .HasMaxLength(255);

           Property(e => e.UserGroup)
                .HasColumnName("UserGroup")
                .HasMaxLength(50);

           Property(e => e.Factory)
                .HasColumnName("Factory")
                .HasMaxLength(50);

           Property(e => e.Dept)
                .HasColumnName("Dept")
                .HasMaxLength(50);

           Property(e => e.YPTPODepartment)
                .HasColumnName("YPTPODepartment")
                .HasMaxLength(50);

           Property(e => e.GNXPODepartment)
                .HasColumnName("GNXPODepartment")
                .HasMaxLength(50);

           Property(e => e.Email)
                .HasColumnName("Email")
                .HasMaxLength(100);

           Property(e => e.Resign)
                .HasColumnName("Resign")
                .IsRequired();  // คอลัมน์นี้เป็น Required (Not Null)

           Property(e => e.Status)
                .HasColumnName("Status")
                .HasMaxLength(20);

           Property(e => e.EmployeeId)
                .HasColumnName("EmployeeId")
                .HasMaxLength(50);
 
        }
    }
}















