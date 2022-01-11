using AutoMapper;
using LakDerana_HotelChainManagement.Data;
using LakDerana_HotelChainManagement.Models;
using LakDerana_HotelChainManagement.Services;
using LakDerana_HotelChainManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LakDerana_HotelChainManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly DashboardService service;

        public HomeController(ApplicationDbContext context,IMapper mapper)
        {
            this.context = context;
            service = new DashboardService(context,mapper);
        }

        public IActionResult Index()
        {
            //var RegStd = _context.Reg_Student_Model.OrderByDescending(i => i.Reg_Date).Take(5);
            //var DistStdFeedback = _context.StdFeedbackData.Where(i => i.ClassID != null).ToList();
            //var StdFeedback = DistStdFeedback.Select(i => i.ClassID).Distinct().ToList();
            //List<StdFeedbackViewModel> StdFBList = new List<StdFeedbackViewModel>();
            //foreach (var data in StdFeedback)
            //{
            //    var CID = _context.ClassData.Where(i => i.Class_ID == data).FirstOrDefault();
            //    var FBData = _context.StdFeedbackData.Where(m => m.ClassID == data).FirstOrDefault();
            //    var Sum = _context.StdFeedbackData.Where(m => m.ClassID == data).Sum(i => i.Rating);
            //    //var CName = await FindCourseName(data.Course_ID);
            //    StdFBList.Add(new StdFeedbackViewModel
            //    {
            //        Rating = Sum,
            //        ClassID = data,
            //        CourseID = FindClassName(CID.Class_ID),
            //        FB_ID = FBData.FB_ID,
            //        Feedback = FBData.Feedback
            //    });
            //}

            //ViewBag.CafeSales = CafeSales();
            //ViewBag.Sales = Sales();
            //ViewBag.EmpDestrib = EmpDestrList();
            //ViewBag.RegStd = RegStd;
            //ViewBag.StdFeedback = StdFBList.OrderByDescending(m => m.Rating).Take(5);
            //ViewBag.TotProfit = TotProfit();
            //ViewBag.TotIncome = TotIncome();
            //ViewBag.TotEmpCount = TotEmpCount();
            //ViewBag.TotStdCount = TotStdCount();

            //ViewBag.DayIncomePercentage = IP("Day");
            //ViewBag.OverallIncomePercentage = IP("OveralPerc");
            //ViewBag.OverallCafeIncome = IP("OveralInc");
            ////ViewBag.OveralIncomePercentage = OIP();

            ////Sales
            //ViewBag.DayAvgIncome = Inc("DayAvg");
            //ViewBag.MonthlyIncome = Inc("MonthlyInc");
            //ViewBag.OverallIncome = Inc("OverallInc");
            //ViewBag.Months = Inc("Months");

            ////Target
            //ViewBag.TotTarget = Target("Achieved");
            //ViewBag.RemTarget = Target("Rem");
            ////Target
            //ViewBag.TotCafeTarget = Target("CafeAchieved");
            //ViewBag.RemCafeTarget = Target("CafeRem");

            ////Cafe Cards
            //ViewBag.TotCafe = CafeCards("TotSales");
            //ViewBag.TotCafeProfit = CafeCards("TotProfit");

            ////Cards
            //ViewBag.TotCourseCount = CardsData("CourseCount");




            //View
            if (TempData["message"] != null && TempData["ToastrType"] != null)
            {
                ViewBag.message = TempData["message"].ToString();
                ViewBag.ToastrType = TempData["ToastrType"].ToString();
                TempData.Remove("message");
                TempData.Remove("ToastrType");
            }

            DashboardViewModel VM = new DashboardViewModel();
            VM.HotelsAndSales = service.GetHotelAndSales();
            VM.Bar = service.GetBarSales();
            VM.HotelSales = service.GetHotelSales();
            VM.Employee = service.GetEmployeeChartData();
            VM.ReservationsCount = service.ReservationsCount();
            return View(VM);

        }

    }
}
