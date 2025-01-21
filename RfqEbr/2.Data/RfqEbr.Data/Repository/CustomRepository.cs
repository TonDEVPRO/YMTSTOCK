using System;
using System.Data.Entity;
using System.Linq;
using RfqEbr.Data.Contracts.Repository;
using RfqEbr.Data.Core;
using RfqEbr.Models.Custom;
using System.Collections.Generic;

namespace RfqEbr.Data.Repository
{
	public class CustomRepository : CustomCore, ICustomRepository
	{
		public CustomRepository() : base() { }
		public CustomRepository(DbContext context) : base(context) { }

		public string GetDbInfo()
		{
			//var sql = "SELECT HOST_NAME ||  '-' || VERSION AS DBNAME FROM V$INSTANCE";
			//var result = Db.Database.SqlQuery<string>(sql).FirstOrDefault();


			var result = "";
			return result;
		}
		public DateTime GetSysDate()
		{
			var sql = "SELECT SYSDATE FROM dual";
			var result = Db.Database.SqlQuery<DateTime>(sql).FirstOrDefault();
			return result;
		}
		public DateTime GetCurrentDateTime()
		{
			var result = new DateTime();
			try
			{
				var sql = "SELECT SYSDATE FROM dual";
				result = Db.Database.SqlQuery<DateTime>(sql).FirstOrDefault();
			}
			catch
			{
				result = new DateTime();
			}
			return result;
		}
		//public RfqUser GetRequestor(string User)
		//{
		//	var sql = string.Format(@"SELECT Emp.EN En,Emp.ACCOUNT_ID AccountId,Emp.USERNAME UserName,Emp.DISPLAY_NAME DisplayName,Emp.EMAIL Email,
  //          Emp.DEPARTMENT Department,Emp.EXTENSION Extension,Emp.POSITION Position,Emp.ORG_NAME OrgName,Emp.COST_CENTER CostCenter,
		//	Emp.SITE Site,Emp.SITE_PAYROLL SitePayroll,Emp.FULLNAME FullName,Emp.SHIFT Shift,Supv.EN SupvEn,
		//	Supv.ACCOUNT_ID SupvAccountId,Supv.FULLNAME SupvName,Supv.USERNAME SupvUsername,Supv.DISPLAY_NAME SupvDisplayName,
		//	AsstManager.EN AsstManagerEn,AsstManager.ACCOUNT_ID AsstManagerAccountId,AsstManager.FULLNAME AsstManagerName,AsstManager.USERNAME AsstManagerUsername,AsstManager.DISPLAY_NAME AsstManagerDisplayName,
		//	Manager.EN ManagerEn,
		//	Manager.ACCOUNT_ID ManagerAccountId,
		//	Manager.FULLNAME ManagerName,
		//	Manager.USERNAME ManagerUsername,
		//	Manager.DISPLAY_NAME ManagerDisplayName,
		//	SrManager.EN SrManagerEn,
		//	SrManager.ACCOUNT_ID SrManagerAccountId,
		//	SrManager.FULLNAME ManagerFullName,
		//	SrManager.USERNAME SrManagerUserName,
		//	SrManager.DISPLAY_NAME SrManagerDisplayName,
		//	Director.EN DirectorEn,
		//	Director.ACCOUNT_ID DirectorAccountId,
		//	Director.FULLNAME DirectorFullName,
		//	Director.USERNAME DirectorUserName,
		//	Director.DISPLAY_NAME DirectorDisplayName,
		//	Vp.EN VpEn,
		//	Vp.ACCOUNT_ID VpAccountId,
		//	Vp.FULLNAME VpFullName,
		//	Vp.USERNAME VpUserName,
		//	Vp.DISPLAY_NAME VpDisplayName
		//	FROM EMPLOYEE_ACCOUNT Emp

		//	LEFT JOIN EMPLOYEE_LAYER Layer ON Layer.EN = Emp.EN

		//	LEFT JOIN EMPLOYEE_ACCOUNT Supv ON Layer.SUPV_EN = Supv.EN

		//	LEFT JOIN EMPLOYEE_ACCOUNT AsstManager ON Layer.ASSTMANAGER_EN = AsstManager.EN

		//	LEFT JOIN EMPLOYEE_ACCOUNT Manager ON Layer.MANAGER_EN = Manager.EN

		//	LEFT JOIN EMPLOYEE_ACCOUNT SrManager ON Layer.SRMANAGER_EN = SrManager.EN

		//	LEFT JOIN EMPLOYEE_ACCOUNT Director ON Layer.DIRECTOR_EN = Director.EN

		//	LEFT JOIN EMPLOYEE_ACCOUNT Vp ON Layer.VP_EN = Vp.EN

		//	WHERE Emp.EN = '{0}' ", User);


		//	var result = Db.Database.SqlQuery<RfqUser>(sql).FirstOrDefault();
		//	return result;
		//}

		public string GetMaxRfqNo()
		{
			const string sql = @"select RFQ_ITEM.nextval from dual";
			return Db.Database.SqlQuery<string>(sql).FirstOrDefault();
		}
	}
}
