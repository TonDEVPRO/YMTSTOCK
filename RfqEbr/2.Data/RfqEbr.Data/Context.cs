﻿using System.Data.Entity;
using System.Security.Cryptography.X509Certificates;
using RfqEbr.Data.Configuration;
using RfqEbr.Data.Core;
using RfqEbr.Models;
using RfqEbr.Models.IT.StockDeviceIT;
using RfqEbr.Models.IT.StockDeviceITInLocation;
using RfqEbr.Models.IT;
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
            modelBuilder.Configurations.Add(new AuthorizeSystemMasterConfiguration());


            modelBuilder.Configurations.Add(new YMTGUserConfiguration());

            modelBuilder.Configurations.Add(new YMTGOrderDetailConfiguration());
            modelBuilder.Configurations.Add(new YMTGOrderGroupConfiguration());

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

            modelBuilder.Configurations.Add(new WarehouseConfig());
            modelBuilder.Configurations.Add(new BarcodeTagsConfig());


            //IT
            modelBuilder.Configurations.Add(new StockDeviceITConfig());
            modelBuilder.Configurations.Add(new StockDeviceITInQtyConfig());
            modelBuilder.Configurations.Add(new StockDeviceTransactionITConfig());


            // MasterData
            modelBuilder.Configurations.Add(new MasterSupplierConfig());
            modelBuilder.Configurations.Add(new MasterTransactionTypeConfig());
            modelBuilder.Configurations.Add(new MasterLocationConfig());
            modelBuilder.Configurations.Add(new MasterDepartmentConfig());
            modelBuilder.Configurations.Add(new DeviceConfig());

        }

 
        public DbSet<AuthorizeSystemMaster> AuthorizeSystemMasters { get; set; }

        //YMTG

        public DbSet<YMTGUser> YMTGUsers { get; set; }
        public DbSet<YMTGOrderDetail> YMTGOrderDetails { get; set; }
        public DbSet<YMTGOrderGroup> YMTGOrderGroups { get; set; }

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
        

        public DbSet<Warehouse> Warehouses { get; set; }

        //
        public DbSet<BarcodeTags> BarcodeTagss { get; set; }


        // IT
        public DbSet<StockDeviceIT> StockDeviceITs { get; set; }
        public DbSet<StockTransactionIT> StockTransactionITs { get; set; }
        public DbSet<MasterSupplier> MasterSuppliers { get; set; }
        public DbSet<StockDeviceITInQty> StockDeviceITInQtys { get; set; }
        public DbSet<MasterTransactionType> MasterTransactionTypes { get; set; }
        public DbSet<MasterLocation> MasterLocations { get; set; }
        public DbSet<Device> Devices { get; set; }

    }
}