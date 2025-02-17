
using RfqEbr.Data.Contracts.Core;
using RfqEbr.Data.Contracts.Repository;
using RfqEbr.Models;
using RfqEbr.Models.IT.StockDeviceIT;
using RfqEbr.Models.IT.StockDeviceITInLocation;
using RfqEbr.Models.IT;
using RfqEbr.Models.Table;

namespace RfqEbr.Data.Contracts
{
    /// <summary>
    /// Interface for the "Unit of Work"
    /// </summary>
    public interface IUnitOfWork : IUnitOfWorkCore
    {

        IRepository<AuthorizeSystemMaster> AuthorizeSystemMasters { get; }
        IAuthorizeSystemMasterRepository AuthorizeSystemMasterRepo { get; }
        IRfqItemRepository RfqItemRepo { get; }
        IRfqDocumentRepository RfqDocumentRepo { get; }


        //Custom
        ICustomRepository Custom { get; }

        //YMTG
        IRepository<YMTGUser> YMTGUsers { get; }
        IRepository<YMTGOrderDetail> YMTGOrderDetails { get; }
        IRepository<YMTGOrderGroup> YMTGOrderGroups { get; }
        //NDS
        IRepository<YMTGNDSShopStock> YMTGNDSShopStocks { get; }
        IRepository<YMTGNDSShopStockLog> YMTGNDSShopStockLogs { get; }

        //NDS Holiday

        IRepository<YMTGNDSShopHoliday> YMTGNDSShopHolidays { get; }
        IRepository<YMTGNDSShopHolidayLog> YMTGNDSShopHolidayLogs { get; }
        IRepository<YMTGNDSShopSaveHoliday> YMTGNDSShopSaveHolidays { get; }



        //NDS SYSTEM
        IRepository<YmtgOrderNds> YmtgOrderNdss { get; }
        IRepository<MasterStyle> MasterStyles { get; }
        IRepository<TypeOrderFrom> TypeOrderFroms { get; }
        IRepository<MasterProvince> MasterProvinces { get; }
        IRepository<MasterRemark> MasterRemarks { get; }
        IRepository<YmtgProductNds> YmtgProductNdss { get; }
        IRepository<QuotationFile> QuotationFiles { get; }



        //OrderInformation
        IRepository<YmtgOrderModel> YmtgOrderModels { get; }
        IRepository<ProductModel> ProductModels { get; }
        IRepository<AttachmentsModel> AttachmentsModels { get;  }

        //RFID
        IRepository<Product> Products { get; }
        IRepository<RFIDTag> RFIDTags { get; }
        IRepository<Payment> Payments { get; }

        //NDS 
        IRepository<Warehouse> Warehouses { get; }
        IRepository<BarcodeTags> BarcodeTagss { get; }

        //StockIT 
        IRepository<StockDeviceIT> StockDeviceITs { get; }
        IRepository<StockDeviceITInQty> StockDeviceITInQtys { get; }
        IRepository<StockTransactionIT> StockTransactionITs { get; }
        IRepository<MasterSupplier> MasterSuppliers { get; }
        IRepository<MasterTransactionType> MasterTransactionTypes { get; }
        IRepository<MasterLocation> MasterLocations { get; }
        IRepository<MasterDepartment> MasterDepartments { get; }
        IRepository<Device> Devices { get; }

    }
}