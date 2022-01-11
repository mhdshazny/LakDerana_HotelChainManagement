using AutoMapper;
using LakDerana_HotelChainManagement.Data;
using LakDerana_HotelChainManagement.Services;
using LakDerana_HotelChainManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LakDerana_HotelChainManagement.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly AttendanceService service;

        public AttendanceController(ApplicationDbContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
            service = new AttendanceService(context, mapper);
        }
        public IActionResult Index(string Message)
        {
            if (TempData["message"]!=null && TempData["ToastrType"]!=null)
            {
                ViewBag.message = TempData["message"].ToString();
                ViewBag.ToastrType = TempData["ToastrType"].ToString();
                TempData.Remove("message");
                TempData.Remove("ToastrType");
            }

            if (Message!=null)
            {
                ViewBag.Message = Message;
            }
            ViewBag.NewID = service.newID();
            return View();
        }
        [HttpPost]
        public string Present(int data)
        {
            if (data>0)
            {
                bool Updated = service.Present(data);
                if (Updated)
                {
                    return "Success";
                }
                else
                {
                    return "Failed";
                }
            }
            return "Error";

        }
        [HttpPost]
        public string Absent(int data)
        {
            if (data > 0)
            {
                bool Updated = service.Absent(data);
                if (Updated)
                {
                    return "Success";
                }
                else
                {
                    return "Failed";
                }
            }
            return "Error";
        }

        [HttpPost]
        public IActionResult SetAttendance(string LogID, string DayDescription, DateTime DateTimeForLog)
        {
            try
            {
                if (DateTimeForLog!=null)
                {
                    var Exist = service.Exist(LogID,DateTimeForLog);
                    if (Exist.Exist)
                    {
                        ViewBag.message = "This Record Already Exists";
                        ViewBag.ToastrType = "success";
                        return View(Exist.AttendanceModel);
                    }
                    else if(LogID != null)
                    {
                        if (DayDescription==null)
                        {
                            DayDescription = "Typical Day";
                        }
                        var Result = service.SetAttendance(LogID, DayDescription, DateTimeForLog);
                        ViewBag.message = "New Roecord For"+DateTimeForLog.ToString("dd-MMM-yyyy")+" Successfully";
                        ViewBag.ToastrType = "success";
                        return View(Result);
                    }
                    else
                    {
                        TempData["message"] = "SomeDataMissingError";
                        TempData["ToastrType"] = "error";

                        return RedirectToAction(nameof(Index), "Attendance", "SomeDataMissingError");
                    }
                }
                else
                {
                    ViewBag.message = "SomeDataMissingError[DateRelated]";
                    ViewBag.ToastrType = "error";
                    return RedirectToAction(nameof(Index),"Attendance","SomeError");
                }
            }
            catch (Exception er)
            {
                return RedirectToAction(nameof(Index), "Attendance", "SomeError");
            }
        }
    }
}
