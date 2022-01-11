using AutoMapper;
using LakDerana_HotelChainManagement.Data;
using LakDerana_HotelChainManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LakDerana_HotelChainManagement.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly ReportService service;
        public ReportsController(ApplicationDbContext context,IMapper mapper)
        {
            this.context = context;
            service = new ReportService(context, mapper);
        }

        public IActionResult ReservationsReport()
        {
            var data = service.ReservationsReport();
            return new ViewAsPdf(data);
        }
        public IActionResult AllSalesReport()
        {
            var data = service.SalesReport();
            return new ViewAsPdf(data);
        }
        [HttpPost]
        public IActionResult SalesCustomReport(string from, string to)
        {
            var data = service.SalesCustomReport(from,to);
            if (data.Count()!=0)
            {
                return new ViewAsPdf(data);
            }
            TempData["message"] = "Invalid Data Provided / No Data Found";
            TempData["ToastrType"] = "error";
            return RedirectToAction(nameof(CustomReport));
        }
        public IActionResult BarSalesReport()
        {
            var data = service.BarSalesReport();
            return new ViewAsPdf(data);
        }
        [HttpPost]
        public IActionResult BarSalesCustomReport(string from, string to)
        {
            var data = service.BarSalesCustomReport(from,to);
            if (data.Count()!=0)
            {
                return new ViewAsPdf(data);
            }
            TempData["message"] = "Invalid Data Provided / No Data Found";
            TempData["ToastrType"] = "error";
            return RedirectToAction(nameof(CustomReport));
        }
        [HttpPost]
        public IActionResult ReservationsCustomReport(string from, string to)
        {
            var data = service.ReservationsCustomReport(from,to);
            if (data.Count()!=0)
            {
                return new ViewAsPdf(data);
            }
            TempData["message"] = "Invalid Data Provided / No Data Found";
            TempData["ToastrType"] = "error";
            return RedirectToAction(nameof(CustomReport));
        }
        public IActionResult EmployeeReport()
        {
            var data = service.EmployeeReport();
            return new ViewAsPdf(data);
        }
        public IActionResult Invoice(string ID)
        {
            var data = service.Invoice(ID);
            if (!data.Equals(null))
            {
                return new ViewAsPdf(data);
            }
            return NotFound();
        }

        public IActionResult CustomReport()
        {
            if (TempData["message"] != null && TempData["ToastrType"] != null)
            {
                ViewBag.message = TempData["message"].ToString();
                ViewBag.ToastrType = TempData["ToastrType"].ToString();
                TempData.Remove("message");
                TempData.Remove("ToastrType");
            }

            return View();
        }

        public IActionResult AllAttendanceReport()
        {
            var data = service.AllAttendanceReport();
            return new ViewAsPdf(data);
        }
        [HttpPost]
        public IActionResult EmployeeAttendanceCustomReport(string dt)
        {
            var data = service.EmployeeAttendanceCustomReport(dt);
            if (data.Count() != 0)
            {
                return new ViewAsPdf(data);
            }
            TempData["message"] = "Invalid Data Provided / No Data Found";
            TempData["ToastrType"] = "error";
            return RedirectToAction(nameof(CustomReport));
        }
        public IActionResult EmployeeAttendanceTodayReport(string dt)
        {
            var data = service.EmployeeAttendanceCustomReport(dt);
            if (data.Count() != 0)
            {
                return new ViewAsPdf(data);
            }
            TempData["message"] = "Invalid Data Provided / No Data Found";
            TempData["ToastrType"] = "error";
            return RedirectToAction(nameof(CustomReport));
        }
    }
}
