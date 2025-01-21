using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using EBR.Web.Helpers;
using EBR.Web.Models;



namespace EBR.Web.Services
{

    public static class TTools
    {
        public static string GetConfig(string configName)
        {
            return ConfigurationManager.AppSettings[configName];
        }
        public static string SiteMessageApi
        {
            get { return GetConfig("MessageApi"); }
        }
        public static string SiteAuthorizeApi
        {
            get { return GetConfig("AuthorizeApi"); }
        }
        public static string SystemName
        {
            get { return GetConfig("SystemName"); }
        }
        public static List<string> UserRoleList
        {
            get { return User.ListRoles; }
        }
        public static UserModel User
        {
            get
            {
                var user = (UserModel)HttpContext.Current.Session[SessionName];
                return user ?? new UserModel();
            }
            set
            {
                HttpContext.Current.Session[SessionName] = value;
            }
        }
        public static void UserClear()
        {
            HttpContext.Current.Session[SessionName] = null;
        }
        public static string SessionName
        {
            get { return GetConfig("SessionName"); }
        }


        //public static string GetClientIpAddress()
        //{
        //    HttpRequest currentRequest = HttpContext.Current.Request;

        //    // ลองอ่านค่า HTTP_X_FORWARDED_FOR ก่อน (ในกรณีที่มี Proxy/Load Balancer)
        //    string ipAddress = currentRequest.ServerVariables["HTTP_X_FORWARDED_FOR"];

        //    // ตรวจสอบว่า IP Address มีค่าหรือไม่
        //    if (string.IsNullOrEmpty(ipAddress) || ipAddress.ToLower() == "unknown")
        //    {
        //        // หากไม่มีค่าใน HTTP_X_FORWARDED_FOR ให้ใช้ REMOTE_ADDR แทน
        //        ipAddress = currentRequest.ServerVariables["REMOTE_ADDR"];
        //    }
        //    else
        //    {
        //        // ในกรณีที่ HTTP_X_FORWARDED_FOR มีหลายค่า (เช่น มี Proxy หลายตัว)
        //        // ค่าแรกคือ IP Address ของ Client จริง
        //        string[] forwardedIps = ipAddress.Split(',');
        //        if (forwardedIps.Length > 0)
        //        {
        //            ipAddress = forwardedIps[0].Trim();
        //        }
        //    }

        //    // กรณีที่ IP เป็น localhost
        //    if (ipAddress == "::1")
        //    {
        //        ipAddress = "localhost";
        //    }
        //    else
        //    {
        //        // หาก IP Address เป็น IPv6 และต้องการเฉพาะ IPv4 ให้ลบส่วนที่ไม่เกี่ยวข้อง
        //        int foundColon = ipAddress.IndexOf(":");
        //        if (foundColon > -1)
        //        {
        //            ipAddress = ipAddress.Remove(foundColon);
        //        }
        //    }

        //    return ipAddress;
        //}
        public static string ClientIpAddress()
        {
            HttpRequest currentRequest = HttpContext.Current.Request;
            string ipAddress = currentRequest.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (ipAddress == null || ipAddress.ToLower() == "unknown")
                ipAddress = currentRequest.ServerVariables["REMOTE_ADDR"];

            if (ipAddress == "::1")
            {
                ipAddress = "localhost";
            }
            else
            {
                int foundColon = ipAddress.IndexOf(":");
                if (foundColon > -1)
                {
                    ipAddress = ipAddress.Remove(foundColon);
                }
            }
            return ipAddress;
        }
        public static string linkEmail
        {
            get
            {
                var url = HttpContext.Current.Request.Url.Authority;
                if (url.Contains("localhost"))
                {
                    url = HttpContext.Current.Request.Url.Authority + "/Home/";
                }
                else
                {
                    url = HttpContext.Current.Request.Url.Authority + "/MRBDisposition/Home/";
                }
                if (!url.Contains("http://"))
                {
                    url = "http://" + url;
                }
                return url;
            }
        }
        public static string linkUrl
        {
            get
            {
                var url = HttpContext.Current.Request.Url.Authority;
                if (url.Contains("localhost"))
                {
                    url = HttpContext.Current.Request.Url.Authority + "/Home/";
                }
                else
                {
                    url = HttpContext.Current.Request.Url.Authority + "/MRBDisposition/Home/";
                }
                if (!url.Contains("http://"))
                {
                    url = "http://" + url;
                }
                return url;
            }
        }






        //public static void SentEmailApprove(EbrDocument AppEbrDoc, List<EbrEmployee> EbrAcknow, string EbrSup, List<EbrBasicbuyoffApp> EbrManagerApp)
        //{
        //    var strBody = "";
        //    var subject = "EBR SYSTEM";
        //    var DearBody = "";
        //    var Emp = new com.magnecomp.tlnw.webservice.EmployeeService();
        //    var EmailApp = new EmailService.Email();

        //    var listBoxSendTo = new List<string>();
        //    var listEmail = new List<string>();



        //    if (EbrSup != null)
        //    {
        //        listEmail.Add(EbrSup);
        //    }
        //    else if (EbrManagerApp.Count != 0)
        //    {
        //        foreach (var items in EbrManagerApp)
        //        {
        //            var ManagerApp = Emp.GetEmployeeByEn(items.En).Email;
        //            listEmail.Add(ManagerApp);
        //        }
        //    }
        //    else
        //    {
        //        var EmpEn = Emp.GetEmployeeByEn(AppEbrDoc.RequestorEn);
        //        listEmail.Add(EmpEn.Email);
        //    }

        //    var linkApp = linkEmail + "EbrId=" + AppEbrDoc.Id + "";


        //    switch (AppEbrDoc.Status)
        //    {
        //        case 10:
        //            subject = "" + AppEbrDoc.EbrNo + " for [ " + AppEbrDoc.MachineType + "_" + AppEbrDoc.MachineNo + " ] Wait M/C arrived ";
        //            DearBody = "<BR/>Dear " + "ALL" + "<BR/><BR/>" + " ";
        //            strBody = AppEbrDoc.EbrNo + " " + " " + "Wait M/C arrived" + " <BR/>" +
        //                      "<BR/>Please sign for approve  : " + "" + @" by click <a href=""" + linkEmail + "EbrId=" + AppEbrDoc.Id + "" + @""">View</a>";
        //            break;
        //        case 15:
        //            subject = "" + AppEbrDoc.EbrNo + "  for [ " + AppEbrDoc.MachineType + "_" + AppEbrDoc.MachineNo + " ] Wait Confirm M/C arrived ";
        //            DearBody = "<BR/>Dear " + "ALL" + "<BR/><BR/>" + " ";
        //            strBody = AppEbrDoc.EbrNo + "  " + " " + "Wait Confirm M/C arrived" + " <BR/>" +
        //                      "<BR/>Please sign for approve  : " + "" + @" by click <a href=""" + linkEmail + "EbrId=" + AppEbrDoc.Id + "" + @""">View</a>";
        //            break;
        //        case 20:
        //            subject = "" + AppEbrDoc.EbrNo + "  for [ " + AppEbrDoc.MachineType + "_" + AppEbrDoc.MachineNo + " ] Wait Basic Buy-off ";
        //            DearBody = "<BR/>Dear " + "ALL" + "<BR/><BR/>" + " ";
        //            strBody = AppEbrDoc.EbrNo + "  " + " " + "Wait Basic Buy-off" + " <BR/>" +
        //                      "<BR/>Please sign for approve  : " + "" + @" by click <a href=""" + linkEmail + "EbrId=" + AppEbrDoc.Id + "" + @""">View</a>";
        //            break;
        //        case 21:
        //            subject = "" + AppEbrDoc.EbrNo + "  for [ " + AppEbrDoc.MachineType + "_" + AppEbrDoc.MachineNo + " ] Wait Manager assign to team";
        //            DearBody = "<BR/>Dear " + "ALL" + "<BR/><BR/>" + " ";
        //            strBody = AppEbrDoc.EbrNo + "  " + " " + "Wait Manager assign to team" + " <BR/>" +
        //                      "<BR/>Please sign for approve  : " + "" + @" by click <a href=""" + linkEmail + "EbrId=" + AppEbrDoc.Id + "" + @""">View</a>";
        //            break;
        //        case 22:
        //            subject = "" + AppEbrDoc.EbrNo + " for [ " + AppEbrDoc.MachineType + "_" + AppEbrDoc.MachineNo + " ] Wait Team Action";
        //            DearBody = "<BR/>Dear " + "ALL" + "<BR/><BR/>" + " ";
        //            strBody = AppEbrDoc.EbrNo + "  " + " " + "Wait Team Action" + " <BR/>" +
        //                      "<BR/>Please sign for approve  : " + "" + @" by click <a href=""" + linkEmail + "EbrId=" + AppEbrDoc.Id + "" + @""">View</a>";
        //            break;
        //        case 23:
        //            subject = "" + AppEbrDoc.EbrNo + "  for [ " + AppEbrDoc.MachineType + "_" + AppEbrDoc.MachineNo + " ] Wait Manager verify result";
        //            DearBody = "<BR/>Dear " + "ALL" + "<BR/><BR/>" + " ";
        //            strBody = AppEbrDoc.EbrNo + "  " + " " + "Wait Manager verify result" + " <BR/>" +
        //                      "<BR/>Please sign for approve  : " + "" + @" by click <a href=""" + linkEmail + "EbrId=" + AppEbrDoc.Id + "" + @""">View</a>";
        //            break;
        //        case 24:
        //            subject = "" + AppEbrDoc.EbrNo + "  for [ " + AppEbrDoc.MachineType + "_" + AppEbrDoc.MachineNo + " ] Wait Action M/C Arrived";
        //            DearBody = "<BR/>Dear " + "ALL" + "<BR/><BR/>" + " ";
        //            strBody = AppEbrDoc.EbrNo + "  " + " " + "Wait GSCM Action M/C Arrived" + " <BR/>" +
        //                      "<BR/>Please sign for approve  : " + "" + @" by click <a href=""" + linkEmail + "EbrId=" + AppEbrDoc.Id + "" + @""">View</a>";
        //            break;
        //        case 25:
        //            subject = "" + AppEbrDoc.EbrNo + "  for [ " + AppEbrDoc.MachineType + "_" + AppEbrDoc.MachineNo + " ] Wait Confirm M/C Arrived";
        //            DearBody = "<BR/>Dear " + "ALL" + "<BR/><BR/>" + " ";
        //            strBody = AppEbrDoc.EbrNo + "  " + " " + "Wait GSCM Confirm M/C Arrived" + " <BR/>" +
        //                      "<BR/>Please sign for approve  : " + "" + @" by click <a href=""" + linkEmail + "EbrId=" + AppEbrDoc.Id + "" + @""">View</a>";
        //            break;
        //        case 26:
        //            subject = "" + AppEbrDoc.EbrNo + " for [ " + AppEbrDoc.MachineType + "_" + AppEbrDoc.MachineNo + " ] Wait Rework ECD";
        //            DearBody = "<BR/>Dear " + "ALL" + "<BR/><BR/>" + " ";
        //            strBody = AppEbrDoc.EbrNo + "  " + " " + "Wait Rework ECD" + " <BR/>" +
        //            "<BR/>Please sign for approve  : " + "" + @" by click <a href=""" + linkEmail + "EbrId=" + AppEbrDoc.Id + "" + @""">View</a>";
        //            break;
        //        case 27:
        //            subject = "" + AppEbrDoc.EbrNo + " for [ " + AppEbrDoc.MachineType + "_" + AppEbrDoc.MachineNo + " ] Waiting RFQ-Originator action";
        //            DearBody = "<BR/>Dear " + "ALL" + "<BR/><BR/>" + " ";
        //            strBody = AppEbrDoc.EbrNo + "  " + " " + "Waiting RFQ-Originator action" + " <BR/>" +
        //                      "<BR/>Please sign for approve  : " + "" + @" by click <a href=""" + linkEmail + "EbrId=" + AppEbrDoc.Id + "" + @""">View</a>";
        //            break;
        //        case 28:
        //            subject = "" + AppEbrDoc.EbrNo + " for [ " + AppEbrDoc.MachineType + "_" + AppEbrDoc.MachineNo + " ] Waiting RFQ-originator confirm M/C intalled";
        //            DearBody = "<BR/>Dear " + "ALL" + "<BR/><BR/>" + " ";
        //            strBody = AppEbrDoc.EbrNo + "  " + " " + "Waiting RFQ-originator confirm M/C intalled" + " <BR/>" +
        //                      "<BR/>Please sign for approve  : " + "" + @" by click <a href=""" + linkEmail + "EbrId=" + AppEbrDoc.Id + "" + @""">View</a>";
        //            break;
        //        case 30:
        //            subject = "" + AppEbrDoc.EbrNo + " for [ " + AppEbrDoc.MachineType + "_" + AppEbrDoc.MachineNo + " ] Waiting Final Buy-off";
        //            DearBody = "<BR/>Dear " + "ALL" + "<BR/><BR/>" + " ";
        //            strBody = AppEbrDoc.EbrNo + "  " + " " + "Waiting Final Buy-off" + " <BR/>" +
        //                      "<BR/>Please sign for approve  : " + "" + @" by click <a href=""" + linkEmail + "EbrId=" + AppEbrDoc.Id + "" + @""">View</a>";
        //            break;
        //        case 31:
        //            subject = "" + AppEbrDoc.EbrNo + "  for [ " + AppEbrDoc.MachineType + "_" + AppEbrDoc.MachineNo + " ] Wait Manager assign to team (Final BuyOff)";
        //            DearBody = "<BR/>Dear " + "ALL" + "<BR/><BR/>" + " ";
        //            strBody = AppEbrDoc.EbrNo + "  " + " " + "Wait Manager assign to team (Final BuyOff)" + " <BR/>" +
        //                      "<BR/>Please sign for approve  : " + "" + @" by click <a href=""" + linkEmail + "EbrId=" + AppEbrDoc.Id + "" + @""">View</a>";
        //            break;
        //        default:
        //            break;
        //    }

        //    foreach (var items in EbrAcknow)
        //    {
        //        var AppEmail = Emp.GetEmployeeByEn(items.En).Email;
        //        listBoxSendTo.Add(AppEmail);
        //    }

        //    var sendEmail = EmailApp.SendEmail(subject, DearBody + strBody, "eis@magnecomp.com", string.Join(";", listEmail), string.Join(";", listBoxSendTo));
        //}



    }
}