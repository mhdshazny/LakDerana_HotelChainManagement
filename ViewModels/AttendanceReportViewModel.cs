using LakDerana_HotelChainManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LakDerana_HotelChainManagement.ViewModels
{
    public class AttendanceReportViewModel
    {
        public AttendanceMaintenanceModel AttendaceLog { get; set; }
        public List<AttendanceSheetModel> AttendaceSheet { get; set; }
        public List<EmployeeModel> Employees { get; set; }
    }
}
