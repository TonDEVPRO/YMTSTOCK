using System.ComponentModel.DataAnnotations.Schema;

namespace RfqEbr.Models
{
    //[Table("YMTG_NDS_QuoMasterRemark")]

    public class MasterRemark
    {

        public int Id { get; set; } = 0;                        // กำหนดค่าเริ่มต้นเป็น 0
        public string RemarkQuo { get; set; } = "";
    }
}
