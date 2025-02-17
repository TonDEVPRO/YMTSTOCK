
using RfqEbr.Data.Contracts;
using RfqEbr.Data.Contracts.Core;
using RfqEbr.Data.Contracts.Repository;
using RfqEbr.Data.Core;
using RfqEbr.Data.Helpers;
using RfqEbr.Models;
using RfqEbr.Models.IT.StockDeviceIT;
using RfqEbr.Models.IT.StockDeviceITInLocation;
using RfqEbr.Models.IT;
using RfqEbr.Models.Table;

//using Architecture.Model;

namespace RfqEbr.Data
{
    public class UnitOfWork : UnitOfWorkCore, IUnitOfWork
    {
        public UnitOfWork(IRepositoryProvider repositoryProvider)
            : base(repositoryProvider)
        {
        }

        //Table
        public IRepository<AuthorizeSystemMaster> AuthorizeSystemMasters { get { return GetStandardRepo<AuthorizeSystemMaster>(); } }
     

		//Table Custom
		public IAuthorizeSystemMasterRepository AuthorizeSystemMasterRepo { get { return GetRepo<IAuthorizeSystemMasterRepository>(); } }
        public IRfqDocumentRepository RfqDocumentRepo { get { return GetRepo<IRfqDocumentRepository>(); } }

        public IRfqItemRepository RfqItemRepo { get { return GetRepo<IRfqItemRepository>(); } }
	    public IRfqItemRepository RfqWorkRepo { get { return GetRepo<IRfqItemRepository>(); } }
	    public IRfqItemRepository EmployeeAccountRepo { get { return GetRepo<IRfqItemRepository>(); } }

        public IRfqItemRepository EbrAttachFileRepo { get { return GetRepo<IRfqItemRepository>(); } }




        //MRB DISPOSITION REPORT

        public ICustomRepository Custom { get { return GetRepo<ICustomRepository>(); } }



        //YMTG
        public IRepository<YMTGUser> YMTGUsers { get { return GetStandardRepo<YMTGUser>(); } }
        public IRepository<YMTGOrderDetail> YMTGOrderDetails { get { return GetStandardRepo<YMTGOrderDetail>(); } }
        public IRepository<YMTGOrderGroup> YMTGOrderGroups { get { return GetStandardRepo<YMTGOrderGroup>(); } }


        //NDS
        public IRepository<YMTGNDSShopStock> YMTGNDSShopStocks { get { return GetStandardRepo<YMTGNDSShopStock>(); } }

        public IRepository<YMTGNDSShopStockLog> YMTGNDSShopStockLogs { get { return GetStandardRepo<YMTGNDSShopStockLog>(); } }


        //NDS Holiday 

        public IRepository<YMTGNDSShopHoliday> YMTGNDSShopHolidays { get { return GetStandardRepo<YMTGNDSShopHoliday>(); } }
        public IRepository<YMTGNDSShopHolidayLog> YMTGNDSShopHolidayLogs { get { return GetStandardRepo<YMTGNDSShopHolidayLog>(); } }


        public IRepository<YMTGNDSShopSaveHoliday> YMTGNDSShopSaveHolidays { get { return GetStandardRepo<YMTGNDSShopSaveHoliday>(); } }

        //NDS SYSTEM 
        public IRepository<YmtgOrderNds> YmtgOrderNdss { get { return GetStandardRepo<YmtgOrderNds>(); } }
        public IRepository<MasterStyle> MasterStyles { get { return GetStandardRepo<MasterStyle>(); } }

        public IRepository<TypeOrderFrom> TypeOrderFroms { get { return GetStandardRepo<TypeOrderFrom>(); } }

        public IRepository<MasterProvince> MasterProvinces { get { return GetStandardRepo<MasterProvince>(); } }


        public IRepository<MasterRemark> MasterRemarks { get { return GetStandardRepo<MasterRemark>(); } }

        public IRepository<YmtgProductNds> YmtgProductNdss { get { return GetStandardRepo<YmtgProductNds>(); } }

        public IRepository<QuotationFile> QuotationFiles { get { return GetStandardRepo<QuotationFile>(); } }


        //OrderInformation
        public IRepository<YmtgOrderModel> YmtgOrderModels { get { return GetStandardRepo<YmtgOrderModel>(); } }
        public IRepository<ProductModel> ProductModels { get { return GetStandardRepo<ProductModel>(); } }

        public IRepository<AttachmentsModel> AttachmentsModels { get { return GetStandardRepo<AttachmentsModel>(); } }

        //RFID

        public IRepository<Product> Products { get { return GetStandardRepo<Product>(); } }

        public IRepository<RFIDTag> RFIDTags { get { return GetStandardRepo<RFIDTag>(); } }

        public IRepository<Payment> Payments { get { return GetStandardRepo<Payment>(); } }

        public IRepository<Warehouse> Warehouses { get {return GetStandardRepo<Warehouse>(); } }

        public IRepository<BarcodeTags> BarcodeTagss { get { return GetStandardRepo<BarcodeTags>(); } }


        //IT
        public IRepository<StockDeviceIT> StockDeviceITs { get { return GetStandardRepo<StockDeviceIT>(); } }
        public IRepository<StockDeviceITInQty> StockDeviceITInQtys { get { return GetStandardRepo<StockDeviceITInQty>(); } }
        public IRepository<StockTransactionIT> StockTransactionITs { get { return GetStandardRepo<StockTransactionIT>(); } }
        public IRepository<MasterSupplier> MasterSuppliers { get { return GetStandardRepo<MasterSupplier>(); } }
        public IRepository<MasterTransactionType> MasterTransactionTypes { get { return GetStandardRepo<MasterTransactionType>(); } }
        public IRepository<MasterLocation> MasterLocations { get { return GetStandardRepo<MasterLocation>(); } }
        public IRepository<MasterDepartment> MasterDepartments { get { return GetStandardRepo<MasterDepartment>(); } }
        public IRepository<Device> Devices { get { return GetStandardRepo<Device>(); } } 
    }
}