
using System;
namespace RfqEbr.Models.Table
{
    public class EmployeeInno
    {
        public int Id { get; set; }
        public int EmpNo { get; set; }
        public string EmpNameTh { get; set; }
        public string EmpNameEng { get; set; }
        public string DivisionCode { get; set; }
        public string DivsionName { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public string SectionCode { get; set; }
        public string SectionName { get; set; }
        public string PositionCode { get; set; }

        public string PositionName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ProbationDate { get; set; }
        public string Type { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public string Remark { get; set; }
        public int Status { get; set; }
    }
}
