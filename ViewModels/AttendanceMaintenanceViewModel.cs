using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LakDerana_HotelChainManagement.ViewModels
{
    public class AttendanceMaintenanceViewModel
    {
        public string AttendanceLogID { get; set; }
        public DateTime AttendanceDate { get; set; }
        public string DayDescription { get; set; }
        public string Status { get; set; }

    }
}
