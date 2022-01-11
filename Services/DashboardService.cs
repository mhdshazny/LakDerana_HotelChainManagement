using AutoMapper;
using LakDerana_HotelChainManagement.Data;
using LakDerana_HotelChainManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LakDerana_HotelChainManagement.Services
{
    public class DashboardService
    {
        private ApplicationDbContext context;
        private IMapper mapper;

        public DashboardService(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        internal List<HotelsAndSales> GetHotelAndSales()
        {
            var Hotels = context.HotelData.ToList();
            var Reservations = context.ReservationData.ToList();
            var Payments = context.PaymentData.ToList();

            var HotelReservation = from e in Hotels join d in Reservations on e.ID equals d.Hotel
                                   select new
                                   {
                                       HotelID = e.ID,
                                       HotelName = e.HotelSpecialName,
                                       ReservationID = d.ID
                                   };




            var AllJoined = from e in HotelReservation join p in Payments on e.ReservationID equals p.ReservationID
                            select new
                            {
                                HotelID = e.HotelID,
                                HotelName = e.HotelName,
                                ReservationID = e.ReservationID,
                                Payments =p.TotalPayableAmount
                            };


            List<HotelsAndSales> FinalList = new List<HotelsAndSales>();

            foreach (var item in Hotels)
            {
                HotelsAndSales Obj = new HotelsAndSales();

                var ID = item.ID;
                Obj.Hotel = item.HotelSpecialName;
                Obj.Sales = AllJoined.Where(i => i.HotelID == ID).Sum(i => i.Payments);

                FinalList.Add(Obj);
            }

            
            return FinalList;
        }

        internal Sales GetBarSales()
        {
            var BarData = context.BarData.ToList();
            Sales BarVM = new Sales();
            int ColVal = 0;
            List<SalesView> BarSales = new List<SalesView>();

            foreach (var item in BarData)
            {
                decimal income = item.Sales - item.Expense;
                if (ColVal % 2 == 0)
                {
                    SalesView BarSale = new SalesView
                    {
                        Date = item.IncomeExpenseUpdatedDate.ToString("yyyy,MM,dd"),
                        Income = int.Parse(Math.Round(income).ToString()),
                        Color = "#497b92"
                    };
                    BarSales.Add(BarSale);

                }
                else
                {
                    SalesView BarSale = new SalesView
                    {
                        Date = item.IncomeExpenseUpdatedDate.ToString("yyyy,MM,dd"),
                        Income = int.Parse(Math.Round(income).ToString()),
                        Color = "#6e93d0"
                    };
                    BarSales.Add(BarSale);
                }
                ColVal++;
            }

            BarVM.SalesList = BarSales;
            
            decimal TotalIncome = BarData.Sum(i => i.Sales - i.Expense);

            int Days = BarData.Select(i=>i.IncomeExpenseUpdatedDate).Distinct().Count();
            int Months = BarData.Select(i => i.IncomeExpenseUpdatedDate.Month).Distinct().Count();

            BarVM.HotelID = "Hotekl ID";
            BarVM.OverallIncome = TotalIncome;
            BarVM.Months = Months;
            BarVM.MonthlyIncome = TotalIncome/Months;
            BarVM.AverageDayIncome = TotalIncome/Days;
            BarVM.GraphMin = BarSales.Select(i=>i.Income).Min() - 100;
            BarVM.GraphMax = BarSales.Select(i => i.Income).Max() + 1000;

            return BarVM;
        }

        internal Sales GetHotelSales()
        {
            Sales FinalVM = new Sales();
            List<SalesView> SalesViewList = new List<SalesView>();

            var Hotels = context.HotelData.ToList();
            var Reservations = context.ReservationData.ToList();
            var Payments = context.PaymentData.ToList();

            var HotelReservation = from e in Hotels
                                   join d in Reservations on e.ID equals d.Hotel
                                   select new
                                   {
                                       HotelID = e.ID,
                                       HotelName = e.HotelSpecialName,
                                       ReservationID = d.ID
                                   };




            var AllJoined = from e in HotelReservation
                            join p in Payments on e.ReservationID equals p.ReservationID
                            select new
                            {
                                HotelID = e.HotelID,
                                HotelName = e.HotelName,
                                ReservationID = e.ReservationID,
                                PaymentDate = p.FinalPaymentDate,
                                Payments = p.TotalPayableAmount
                            };

            var DatesList = AllJoined.Select(i => i.PaymentDate).Distinct();

            int ColVal = 0;
            foreach (var item in DatesList)
            {
                decimal income = AllJoined.Where(i=>i.PaymentDate==item).Sum(i=>i.Payments);
                if (ColVal % 2 == 0)
                {
                    SalesView BarSale = new SalesView
                    {
                        Date = item.ToString("yyyy,MM,dd"),
                        Income = int.Parse(Math.Round(income).ToString()),
                        Color = "#497b92"
                    };
                    SalesViewList.Add(BarSale);

                }
                else
                {
                    SalesView BarSale = new SalesView
                    {
                        Date = item.ToString("yyyy,MM,dd"),
                        Income = int.Parse(Math.Round(income).ToString()),
                        Color = "#6e93d0"
                    };
                    SalesViewList.Add(BarSale);
                }
                ColVal++;
            }

            FinalVM.SalesList = SalesViewList;

            decimal TotalIncome = Payments.Sum(i => i.TotalPayableAmount);

            int Days = Payments.Select(i => i.FinalPaymentDate).Distinct().Count();
            int Months = Payments.Select(i => i.FinalPaymentDate.Month).Distinct().Count();

            FinalVM.HotelID = "Hotekl ID";
            FinalVM.OverallIncome = TotalIncome;
            FinalVM.Months = Months;
            FinalVM.MonthlyIncome = TotalIncome / Months;
            FinalVM.AverageDayIncome = TotalIncome / Days;
            FinalVM.GraphMin = SalesViewList.Select(i => i.Income).Min() - 100;
            FinalVM.GraphMax = SalesViewList.Select(i => i.Income).Max() + 1000;

            return FinalVM;

        }

        internal int ReservationsCount()
        {
            try
            {
                var data = context.ReservationData.ToList();
                return data.Count;
            }
            catch (Exception er)
            {
                return 0;
            }
        }

        internal Employee GetEmployeeChartData()
        {
            Employee FinalVM = new Employee();
            List<EmployeeRoleAndCount> EmployeeRolesAndCount = new List<EmployeeRoleAndCount>();

            var Employees = context.EmpIdentityData.Where(i=>i.EmpStatus=="Active").ToList();
            var EmployeeRoles = Employees.Select(i=>i.EmpRole).Distinct().ToList();

            var LogMaxID = context.AttendanceLogData.Max(i=>i.AttendanceLogID);
            var LatestAttendance = from e in context.AttendanceLogData.Where(i=>i.AttendanceLogID==LogMaxID)
                                   join d in context.AttendanceSheetData on e.AttendanceLogID equals d.AttendanceLogID
                                   select new
                                   {
                                       LogID = e.AttendanceLogID,
                                       LogMarkedDate = e.AttendanceDate,
                                       EmployeeID= d.EmpID,
                                       Status=d.AttendanceStatus
                                   };


            foreach (var EmpRoles in EmployeeRoles)
            {
                EmployeeRoleAndCount RoleAndCount = new EmployeeRoleAndCount();
                RoleAndCount.Role = EmpRoles;
                RoleAndCount.Count = Employees.Where(i => i.EmpRole == EmpRoles).Count();

                EmployeeRolesAndCount.Add(RoleAndCount);
            }









            FinalVM.EmployeeDestribution =EmployeeRolesAndCount;
            FinalVM.AbcenteeCount =LatestAttendance.Where(i=>i.Status=="Present").Count();
            FinalVM.AttendeeCount = LatestAttendance.Where(i => i.Status != "Present").Count();
            FinalVM.AttendanceDate = LatestAttendance.Where(i => i.Status != "Present").Select(i=>i.LogMarkedDate).FirstOrDefault();
            FinalVM.EmployeeCount = Employees.Count();

            return FinalVM;
        }
    }
}
