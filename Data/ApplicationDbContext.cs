using LakDerana_HotelChainManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LakDerana_HotelChainManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }
        public DbSet<CustomerModel> CusData { get; set; }
        public DbSet<EmpIdentityModel> EmpIdentityData { get; set; }
        public DbSet<EmployeeModel> EmpData { get; set; }
        public DbSet<HotelRoomModel> HotelRoomData { get; set; }
        public DbSet<HotelModel> HotelData { get; set; }
        public DbSet<ReservationModel> ReservationData { get; set; }
        public DbSet<ReservedRoomModel> ReservedRoomData { get; set; }
        public DbSet<PaymentModel> PaymentData { get; set; }
        public DbSet<BarModel> BarData { get; set; }
        public DbSet<AttendanceMaintenanceModel> AttendanceLogData { get; set; }
        public DbSet<AttendanceSheetModel> AttendanceSheetData { get; set; }
    }
}
