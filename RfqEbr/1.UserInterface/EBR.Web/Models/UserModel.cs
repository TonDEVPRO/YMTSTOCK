using System;
using System.Collections.Generic;
using System.Linq;
using EBR.Web.Services;
using EISAuthorize.Common.Dto;
using AuthEisUserDto = EISAuthorize.Common.Dto.EisUserDto;
using AuthEmployeeDto = EISAuthorize.Common.Dto.EmployeeDto;

namespace EBR.Web.Models
{
	[Serializable()]
	public class UserModel
	{
		private AuthEisUserDto _Authen { get; set; }
		private EisSystemDto _System { get; set; }
		private string _SystemName = TTools.SystemName;
		public string Role { get; set; }

		public List<string> ListRoles
		{
			get
			{
				if (this._System == null)
				{
					return new List<string>();

				}
				else
				{
					return _System.Roles.Select(s => s.RoleName).ToList();
				}
			}
		}
		public AuthEmployeeDto Employee
		{
			get { return (this.IsAuth == false) ? null : this._Authen.Employee; }
		}

		public bool CheckAuthorize(string authorizeName)
		{
			return this.IsAuth && (this._System.Roles.Any(w => (w.RoleName.ToUpper() == authorizeName.ToUpper()) && (w.FlagStatus == "1")));
		}

		public RoleDto GetRole(int i)
		{
			return this._System.Roles[i];
		}

		public bool IsAuth
		{
			get { return (this._System != null); }
		}

		public bool IsRoles
		{
			get { return (this.ListRoles.Count > 0); }
		}

		/*================= Role ===========================*/
		public bool IsExample
		{
			get { return this.IsAuth && this.CheckAuthorize("Example"); }
		}

		/*================= Role ===========================*/

		public UserModel(AuthEisUserDto userDto = null)
		{
			if (userDto == null)
			{
				this._Authen = null;//new AuthEisUserDto();
				this._System = null;
			}
			else
			{
				this._Authen = userDto;
				this._System = this._Authen.Systems.Where(w => w.SystemId == _SystemName).FirstOrDefault();
			}
		}
	}
}