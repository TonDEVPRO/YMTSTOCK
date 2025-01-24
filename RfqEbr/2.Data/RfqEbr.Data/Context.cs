using System.Data.Entity;
using System.Security.Cryptography.X509Certificates;
using RfqEbr.Data.Configuration;
using RfqEbr.Data.Core;
using RfqEbr.Models;
using RfqEbr.Models.Table;



namespace RfqEbr.Data
{
    public class Context : ContextCore
    {
        public Context()
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //#Wansao - If you have relation table
            modelBuilder.Configurations.Add(new AuthorizeSystemMasterConfiguration());

            modelBuilder.Configurations.Add(new EmployeeLoginConfiguration());
            modelBuilder.Configurations.Add(new EmployeeInnoConfiguration());



            modelBuilder.Configurations.Add(new YMTGUserConfiguration());

            modelBuilder.Configurations.Add(new YMTGOrderDetailConfiguration());


            modelBuilder.Configurations.Add(new YPTGUploadfileConfiguration());

            modelBuilder.Configurations.Add(new YPTGUploadDetailConfiguration());

            modelBuilder.Configurations.Add(new YPTGUploadDataConfiguration());


            modelBuilder.Configurations.Add(new YPTGUploadfileDataConfiguration());

            modelBuilder.Configurations.Add(new YMTGOrderGroupConfiguration());

            modelBuilder.Configurations.Add(new YPTGUploadfileDataLogConfiguration());

            //NDS
            modelBuilder.Configurations.Add(new YMTGNDSShopStockConfig());
            modelBuilder.Configurations.Add(new YMTGNDSShopStockLogConfig());

            //NDSHoliday 
            modelBuilder.Configurations.Add(new YMTGNDSShopHolidayConfig());
            modelBuilder.Configurations.Add(new YMTGNDSShopHolidayLogConfig());

            modelBuilder.Configurations.Add(new YMTGNDSShopSaveHolidayConfig());

            modelBuilder.Configurations.Add(new YmtgOrderNdsConfig());

            modelBuilder.Configurations.Add(new MasterStyleConfig());

            modelBuilder.Configurations.Add(new TypeOrderFromConfig());


            modelBuilder.Configurations.Add(new MasterProvinceConfig());

            modelBuilder.Configurations.Add(new MasterRemarkConfig());

            modelBuilder.Configurations.Add(new YmtgProductNdsConfig());
            modelBuilder.Configurations.Add(new QuotationFileConfig());

            //OrderInformation
            modelBuilder.Configurations.Add(new YmtgOrderModelConfig());
            modelBuilder.Configurations.Add(new ProductModelConfig());

            modelBuilder.Configurations.Add(new AttachmentsModelConfig());

            //RFID
            modelBuilder.Configurations.Add(new ProductConfig());

            modelBuilder.Configurations.Add(new RFIDTagsConfig());

            modelBuilder.Configurations.Add(new PaymentConfig());
            
        }

        //#Wansao - Table
        public DbSet<AuthorizeSystemMaster> AuthorizeSystemMasters { get; set; }

        public DbSet<EmployeeLogin> EmployeeLogins { get; set; }
        public DbSet<EmployeeInno> EmployeeInnos { get; set; }


        //YMTG

        public DbSet<YMTGUser> YMTGUsers { get; set; }
        public DbSet<YMTGOrderDetail> YMTGOrderDetails { get; set; }

        public DbSet<YPTGUploadfile> YPTGUploadfiles { get; set; }

        public DbSet<YPTGUploadDetail> YPTGUploadDetails { get; set; }

        public DbSet<YPTGUploadData> YPTGUploadDatas { get; set; }

        public DbSet<YPTGUploadfileData> YPTGUploadfileDatas { get; set; }

        public DbSet<YMTGOrderGroup> YMTGOrderGroups { get; set; }


        public DbSet<YPTGUploadfileDataLog> YPTGUploadfileDataLogs { get; set; }



        //NDS
        public DbSet<YMTGNDSShopStock> YMTGNDSShopStock { get; set; }
        public DbSet<YMTGNDSShopStockLog> YMTGNDSShopStockLogs { get; set; }

        //NDS holiday 

        public DbSet<YMTGNDSShopHoliday> YMTGNDSShopHoliday { get; set; }
        public DbSet<YMTGNDSShopHolidayLog> YMTGNDSShopHolidayLogs { get; set; }


        public DbSet<YMTGNDSShopSaveHoliday> YMTGNDSShopSaveHolidays { get; set; }


        //NDS SYSTEM
        public DbSet<YmtgOrderNds> YmtgOrderNds { get; set; }
        public DbSet<MasterStyle> MasterStyles { get; set; }

        public DbSet<TypeOrderFrom> TypeOrderFroms { get; set; }

        public DbSet<MasterProvince> MasterProvinces { get; set; }

        public DbSet<MasterRemark> MasterRemarks { get; set; }

        public DbSet<YmtgProductNds> YmtgProductNdss { get; set; }

        public DbSet<QuotationFile> QuotationFiles { get; set; }

        //OrderInformation
        public DbSet<YmtgOrderModel> YmtgOrderModels { get; set; }
        public DbSet<ProductModel> ProductModels { get; set; }

        public DbSet<AttachmentsModel> AttachmentsModels { get; set; }

        //RFID

        public DbSet<Product> Products { get; set; }
        public DbSet<RFIDTag> RFIDTags { get; set; }
        public DbSet<Payment> Payments { get; set; }
        
    }
}