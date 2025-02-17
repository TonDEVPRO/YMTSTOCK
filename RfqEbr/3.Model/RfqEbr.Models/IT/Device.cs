using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RfqEbr.Models.IT
{
    public class Device
    {
        public int DeviceId { get; set; }
        public string DeviceName { get; set; } = ""; 
        public string MacAddress { get; set; }
        public string IPAddressL { get; set; } 
        public string IPAddressW { get; set; } = "";
        public string AssetNumber { get; set; }
        public string Dept { get; set; } = "";
        public string Com_UserName { get; set; } = "";
        public string SewLine { get; set; } = "";
        public string Remark { get; set; } = "";
        public DateTime? DateCreate { get; set; }
        public string UserCreate { get; set; } = "";
        public DateTime? DateEdit { get; set; } // nullable DateTime
        public string UserEdit { get; set; } = "";
        public bool StatusNotUse { get; set; }
        public string RemarkNotUse { get; set; } = "";
        public string Location { get; set; } = "";
        public string DeviceType { get; set; } = "";
        public string Factory { get; set; } = "";
        public string OSType { get; set; } = "";
        public string OSVersion { get; set; } = "";
        public string OSProductKey { get; set; } = "";
        public string Brand { get; set; } = "";
        public string Model { get; set; } = "";
        public string Processor { get; set; } = "";
        public int? RAM { get; set; }
        public string StorageDetail { get; set; } = "";
        public string SerialNumber { get; set; } = "";
        public string DeviceStatus { get; set; } = "";
        public string LoginUser { get; set; } = "";
        public string LoginPassword { get; set; } = "";
        public bool USBWireless { get; set; }
        public bool WirelessInclude { get; set; }
        public DateTime? WarrantyStart { get; set; } // nullable DateTime
        public DateTime? WarrantyExpire { get; set; } // nullable DateTime
    }

}
