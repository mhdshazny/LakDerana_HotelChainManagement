using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LakDerana_HotelChainManagement.ViewModels
{
    public class DashboardViewModel
    {
        public DashboardViewModel()
        {
            DateToday = DateTime.Now;
        }

        public DateTime DateToday { get; set; }
        public List<HotelsAndSales> HotelsAndSales { get; set; }
        public Sales Bar { get; set; }
        public Sales HotelSales { get; set; }
        public Employee Employee { get; set; }
        public int ReservationsCount { get; set; }
    }

    /// Main Chart Hotel vs Sales
    public class HotelsAndSales
    {
        public string Hotel { get; set; }
        public decimal  Sales { get; set; }
    }

    /// Sales Common
    public class Sales
    {
        public string HotelID { get; set; }
        public int Months { get; set; }
        public List<SalesView> SalesList { get; set; }
        public decimal AverageDayIncome { get; set; }
        public decimal MonthlyIncome { get; set; }
        public decimal OverallIncome { get; set; }
        public decimal GraphMin { get; set; }
        public decimal GraphMax { get; set; }
    }
    public class SalesView
    {
        public string Date { get; set; }
        public int Income { get; set; }
        public string Color { get; set; }
    }

    /// Employee - Destribution Chart and Attendance Chart
    
    public class Employee
    {
        public List<EmployeeRoleAndCount> EmployeeDestribution { get; set; }
        public int EmployeeCount { get; set; }
        public int AttendeeCount { get; set; }
        public int AbcenteeCount { get; set; }
        public DateTime AttendanceDate { get; set; }
    }
    public class EmployeeRoleAndCount
    {
        public string Role { get; set; }
        public int Count { get; set; }
    }
}
