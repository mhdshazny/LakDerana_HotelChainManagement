using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LakDerana_HotelChainManagement.Models
{
    [Table("Tbl_AttendanceMaintenance")]
    public class AttendanceMaintenanceModel
    {
        [Key]
        public string AttendanceLogID { get; set; }
        public DateTime AttendanceDate { get; set; }
        public string DayDescription { get; set; }
        public string Status { get; set; }
    }
}
