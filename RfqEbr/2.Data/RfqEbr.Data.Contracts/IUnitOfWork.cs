
using RfqEbr.Data.Contracts.Core;
using RfqEbr.Data.Contracts.Repository;
using RfqEbr.Models;
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







        IRepository<EmployeeLogin> EmployeeLogins { get; }

        IRepository<EmployeeInno> EmployeeInnos { get; }



        //Custom
        ICustomRepository Custom { get; }




        //YMTG
        IRepository<YMTGUser> YMTGUsers { get; }
        IRepository<YMTGOrderDetail> YMTGOrderDetails { get; }

        IRepository<YPTGUploadfile> YPTGUploadfiles { get; }

        IRepository<YPTGUploadDetail> YPTGUploadDetails { get; }

        IRepository<YPTGUploadData> YPTGUploadDatas { get; }


        IRepository<YPTGUploadfileData> YPTGUploadfileDatas { get; }

        IRepository<YMTGOrderGroup> YMTGOrderGroups { get; }

        IRepository<YPTGUploadfileDataLog> YPTGUploadfileDataLogs { get; }


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
        

    }
}