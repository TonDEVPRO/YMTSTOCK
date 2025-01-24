using System.ComponentModel.DataAnnotations.Schema;
namespace RfqEbr.Models
{
    public class MasterProvince
    {
        public int Id { get; set; }  
        public string Provinces { get; set; }
        public string Districts { get; set; }
        public string SubDistricts { get; set; }
        public string ZipCode { get; set; }
    }
}
