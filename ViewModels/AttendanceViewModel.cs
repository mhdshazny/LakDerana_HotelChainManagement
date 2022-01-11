using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LakDerana_HotelChainManagement.ViewModels
{
    public class AttendanceViewModel
    {
        public AttendanceMaintenanceViewModel MaintenanceLog { get; set; }
        public List<AttendanceSheetViewModel> AttendanceSheet { get; set; }
    }
}
