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

    }
}