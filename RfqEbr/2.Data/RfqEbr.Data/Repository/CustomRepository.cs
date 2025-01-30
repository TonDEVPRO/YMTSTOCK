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
		public string GetMaxRfqNo()
		{
			const string sql = @"select RFQ_ITEM.nextval from dual";
			return Db.Database.SqlQuery<string>(sql).FirstOrDefault();
		}
	}
}
