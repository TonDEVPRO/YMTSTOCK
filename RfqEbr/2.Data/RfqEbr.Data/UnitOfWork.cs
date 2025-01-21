
using RfqEbr.Data.Contracts;
using RfqEbr.Data.Contracts.Core;
using RfqEbr.Data.Contracts.Repository;
using RfqEbr.Data.Core;
using RfqEbr.Data.Helpers;
using RfqEbr.Models;
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

        public IRepository<EmployeeLogin> EmployeeLogins { get { return GetStandardRepo<EmployeeLogin>(); } }
        public IRepository<EmployeeInno> EmployeeInnos { get { return GetStandardRepo<EmployeeInno>(); } }


        public ICustomRepository Custom { get { return GetRepo<ICustomRepository>(); } }



        //YMTG
        public IRepository<YMTGUser> YMTGUsers { get { return GetStandardRepo<YMTGUser>(); } }
        public IRepository<YMTGOrderDetail> YMTGOrderDetails { get { return GetStandardRepo<YMTGOrderDetail>(); } }
        public IRepository<YPTGUploadfile> YPTGUploadfiles { get { return GetStandardRepo<YPTGUploadfile>(); } }

        public IRepository<YPTGUploadDetail> YPTGUploadDetails { get { return GetStandardRepo<YPTGUploadDetail>(); } }

        public IRepository<YPTGUploadData> YPTGUploadDatas { get { return GetStandardRepo<YPTGUploadData>(); } }

        public IRepository<YPTGUploadfileData> YPTGUploadfileDatas { get { return GetStandardRepo<YPTGUploadfileData>(); } }

        public IRepository<YMTGOrderGroup> YMTGOrderGroups { get { return GetStandardRepo<YMTGOrderGroup>(); } }

        public IRepository<YPTGUploadfileDataLog> YPTGUploadfileDataLogs { get { return GetStandardRepo<YPTGUploadfileDataLog>(); } }



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


        
    }
}