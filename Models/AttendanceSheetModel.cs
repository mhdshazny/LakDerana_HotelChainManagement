using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LakDerana_HotelChainManagement.Models
{
    [Table("Tbl_AttendanceSheet")]
    public class AttendanceSheetModel
    {
        [Key]
        public int ID { get; set; }
        public string AttendanceLogID { get; set; }
        public string EmpID { get; set; }
        public string AttendanceStatus { get; set; }
    }
}
