using AutoMapper;
using LakDerana_HotelChainManagement.Data;
using LakDerana_HotelChainManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LakDerana_HotelChainManagement.Controllers
{
    public class IdentityController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly AccountService service;

        public IdentityController(ApplicationDbContext context,IMapper mapper)
        {
            this.context = context;
            this.service = new AccountService(context,mapper);
        }
        public IActionResult Login()
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

        public IActionResult Logout()
        {
            DestroySessions();
            TempData["message"] = "Session Cleared and Returned to Login";
            TempData["ToastrType"] = "error";
            return RedirectToAction(nameof(Login), "Identity", "SessionCleared-Successful");
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            DestroySessions();
            if (username != null && password != null)
            {
                var Verify = service.verify(username, password);
                if (Verify.Status)
                {
                    InitalizeSessions(Verify);
                    TempData["message"] = Verify.Role+ " Access Granted";
                    TempData["ToastrType"] = "success";
                    return RedirectToAction(nameof(Index), "Home", "LoginSuccess");
                }
                else
                {
                    TempData["message"] = "Session Cleared and Returned to Login (Error : LoginFailed-UserCredentialsError)";
                    TempData["ToastrType"] = "error";
                    return RedirectToAction(nameof(Login), "Identity", "LoginFailed-UserCredentialsError");
                }
            }
            else
            {
                TempData["message"] = "Session Cleared and Returned to Login (Error : LoginFailed-UserCredentialsError)";
                TempData["ToastrType"] = "error";
                return RedirectToAction(nameof(Login), "Identity", "LoginFailed-UserCredentialsNull");
            }
        }


        public void InitalizeSessions(AccountService.SessionData type)
        {
            HttpContext.Session.SetString("UserLogin", "true");
            HttpContext.Session.SetString("UserType", type.Role);
            HttpContext.Session.SetString("UserID", type.ID);
            HttpContext.Session.SetString("UserHotelID", type.HotelID);
            HttpContext.Session.SetString("UserEmail", type.Email);
        }
        public void DestroySessions()
        {
            HttpContext.Session.SetString("UserLogin", "false");
            HttpContext.Session.SetString("UserType", "");
            HttpContext.Session.SetString("UserID", "");
            HttpContext.Session.SetString("UserHotelID", "");
            HttpContext.Session.SetString("UserEmail", "");
        }
    }
}
