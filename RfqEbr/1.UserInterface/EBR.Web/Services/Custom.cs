using System;
using RfqEbr.Data.Repository;

namespace EBR.Web.Services
{

	public static class Custom
	{
		public static string GetDbInfo()
		{
			using (var db = new CustomRepository())
			{
				return db.GetDbInfo();
			}
		}
		public static DateTime GetSysDate()
		{
			using (var db = new CustomRepository())
			{
				return db.GetSysDate();
			}
		}


	}
}