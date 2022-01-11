using AutoMapper;
using LakDerana_HotelChainManagement.Data;
using LakDerana_HotelChainManagement.Models;
using LakDerana_HotelChainManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LakDerana_HotelChainManagement.Services
{
    public class ReportService
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly CheckoutService CheckoutService;

        public ReportService(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
            CheckoutService = new CheckoutService(context, mapper);
        }

        internal List<ReservationModel> ReservationsReport()
        {
            var Data = context.ReservationData.ToList();
            return Data;
        }
        internal List<ReservationModel> ReservationsCustomReport(string from, string to)
        {
            var Data = context.ReservationData.ToList();
            Data = Data.Where(i => i.CheckoutDate >= DateTime.Parse(from) && i.CheckoutDate <= DateTime.Parse(to)).ToList();
            return Data;
        }

        internal List<PaymentModel> SalesReport()
        {
            var Data = context.PaymentData.ToList();
            return Data;
        }

        internal List<EmployeeModel> EmployeeReport()
        {
            var Data = context.EmpData.ToList();
            return Data;
        }

        internal CheckoutViewModel Invoice(string iD)
        {
            CheckoutViewModel Data =  CheckoutService.GetDataForReport(iD);
            return Data;
        }

        internal List<BarModel> BarSalesReport()
        {
            var Data = context.BarData.ToList();
            return Data;
        }

        internal List<BarModel> BarSalesCustomReport(string from, string to)
        {
            var Data = context.BarData.ToList();
            Data = Data.Where(i => i.IncomeExpenseUpdatedDate >= DateTime.Parse(from) && i.IncomeExpenseUpdatedDate <= DateTime.Parse(to)).ToList();
            return Data;
        }

        internal List<PaymentModel> SalesCustomReport(string from, string to)
        {
            var Data = context.PaymentData.ToList();
            Data = Data.Where(i => i.FinalPaymentDate >= DateTime.Parse(from)&&i.FinalPaymentDate <= DateTime.Parse(to)).ToList();
            return Data;
        }

        internal List<AttendanceReportViewModel> AllAttendanceReport()
        {
            var AttendanceSheetData = context.AttendanceSheetData.ToList();
            var AttendanceLogData = context.AttendanceLogData.ToList();
            var EmpList = context.EmpData.ToList();

            List<AttendanceReportViewModel> data = new List<AttendanceReportViewModel>();

            foreach (var item in AttendanceLogData)
            {
                AttendanceReportViewModel vm = new AttendanceReportViewModel()
                {
                    AttendaceLog = item,
                    AttendaceSheet = AttendanceSheetData.Where(i => i.AttendanceLogID == item.AttendanceLogID).ToList(),
                    Employees = EmpList
                };

                data.Add(vm);
            }
            return data;
        }

        internal List<AttendanceReportViewModel> EmployeeAttendanceCustomReport(string dt)
        {
            var AttendanceLogData = context.AttendanceLogData.Where(i=>i.AttendanceDate.Date==DateTime.Parse(dt).Date&& i.AttendanceDate.Month == DateTime.Parse(dt).Month&& i.AttendanceDate.Year == DateTime.Parse(dt).Year).ToList();
            var AttendanceSheetData = context.AttendanceSheetData.ToList();
            var EmpList = context.EmpData.ToList();

            List<AttendanceReportViewModel> data = new List<AttendanceReportViewModel>();

            foreach (var item in AttendanceLogData)
            {
                AttendanceReportViewModel vm = new AttendanceReportViewModel()
                {
                    AttendaceLog = item,
                    AttendaceSheet = AttendanceSheetData.Where(i => i.AttendanceLogID == item.AttendanceLogID).ToList(),
                    Employees = EmpList
                };

                data.Add(vm);
            }

            return data;
        }
    }
}
