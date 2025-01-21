using System.Data.Entity.ModelConfiguration;
 
using System.ComponentModel.DataAnnotations.Schema;
using RfqEbr.Models.Table;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace RfqEbr.Data.Configuration
{

    public class YPTGUploadDataConfiguration : EntityTypeConfiguration<YPTGUploadData>
    {

        public YPTGUploadDataConfiguration()
        {
            // Specify the table name
          ToTable("YMTG_UploadData");

            // Set primary key
          HasKey(e => e.Id);

            // Configure the properties
          Property(e => e.Id)
                .HasColumnName("Id")
                .IsRequired();

          Property(e => e.BrandCode)
                .HasColumnName("BrandCode")
                .HasMaxLength(50);  // Adjust as needed

          Property(e => e.BrandName)
                .HasColumnName("BrandName")
                .HasMaxLength(255); // Adjust as needed

          Property(e => e.Season)
                .HasColumnName("Season")
                .HasMaxLength(50);  // Adjust as needed

          Property(e => e.StyleName)
                .HasColumnName("StyleName")
                .HasMaxLength(255); // Adjust as needed

          Property(e => e.OrderNo)
                .HasColumnName("OrderNo")
                .HasMaxLength(100); // Adjust as needed

          Property(e => e.TypeName)
                .HasColumnName("TypeName")
                .HasMaxLength(100); // Adjust as needed

          Property(e => e.FactoryCode)
                .HasColumnName("FactoryCode")
                .HasMaxLength(100); // Adjust as needed

          Property(e => e.FileId)
                .HasColumnName("FileId")
                .IsRequired();

          Property(e => e.FileUploadCode)
                .HasColumnName("FileUploadCode")
                .HasMaxLength(100); // Adjust as needed

          Property(e => e.OriginalFileName)
                .HasColumnName("OriginalFileName")
                .HasMaxLength(255); // Adjust as needed

          Property(e => e.NewFileName)
                .HasColumnName("NewFileName")
                .HasMaxLength(255); // Adjust as needed

          Property(e => e.FileExtension)
                .HasColumnName("FileExtension")
                .HasMaxLength(10); // Adjust as needed

          Property(e => e.FilePathUrl)
                .HasColumnName("FilePathUrl")
                .HasMaxLength(500); // Adjust as needed

          Property(e => e.FilePathName)
                .HasColumnName("FilePathName")
                .HasMaxLength(500); // Adjust as needed

          Property(e => e.Revision)
                .HasColumnName("Revision")
                .HasMaxLength(50);  // Adjust as needed

          Property(e => e.RevisionDate)
                .HasColumnName("RevisionDate")
                 .HasMaxLength(50);  // Adjust as needed

            Property(e => e.Status)
                .HasColumnName("Status")
                .IsRequired();

          Property(e => e.Remark)
                .HasColumnName("Remark")
                .HasMaxLength(500); // Adjust as needed

          Property(e => e.CreateBy)
                .HasColumnName("CreateBy")
                .HasMaxLength(50);  // Adjust as needed

          Property(e => e.CreateDate)
                .HasColumnName("CreateDate")
                .HasColumnType("datetime")
                .IsRequired();
 
        }
    }
}














