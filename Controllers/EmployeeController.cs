using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LakDerana_HotelChainManagement.Data;
using LakDerana_HotelChainManagement.Models;
using AutoMapper;
using LakDerana_HotelChainManagement.Services;
using LakDerana_HotelChainManagement.ViewModels;
using Microsoft.AspNetCore.Http;

namespace LakDerana_HotelChainManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly EmployeeService service;

        public EmployeeController(ApplicationDbContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
            service = new EmployeeService(context, mapper);
        }

        // GET: Employee
        public IActionResult Index(EmployeeViewModel data)
        {
            //var NewID = service.NewID();
            //if (data.ID != NewID && data.ID!=null)
            //{

            //    ViewBag.InOut = "Up";
            //}
            //else
            //{
            //    ViewBag.InOut = "In";
            //}

            //var GetList = service.GetList();
            //ViewData["NewID"] = NewID;


            //return View(GetList);
            if (data.ID != null && data.Status!=null)
            {
                ViewBag.InOut = "Up";

            }
            else
            {
                ViewBag.InOut = "In";
            }

            var GetList = service.GetList();
            ViewData["NewID"] = service.NewID();
            return View(GetList);

        }

        // GET: Employee/Details/5
        public IActionResult Details(string id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var VM = service.Find(id);
            //if (VM == null)
            //{
            //    return NotFound();
            //}

            //return View(VM);
            try
            {
                var Data = service.Find(id);
                return View(Data);
            }
            catch (Exception er)
            {
                er.ToString();
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Employee/Details/5
        public IActionResult Profile()
        {
            try
            {
                if (TempData["message"] != null && TempData["ToastrType"] != null)
                {
                    ViewBag.message = TempData["message"].ToString();
                    ViewBag.ToastrType = TempData["ToastrType"].ToString();
                    TempData.Remove("message");
                    TempData.Remove("ToastrType");
                }

                var EmpID = HttpContext.Session.GetString("UserID");
                var Data = service.GetProfileData(EmpID);
                return View(Data);
            }
            catch (Exception er)
            {
                er.ToString();
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Employee/Create
        public IActionResult Create()
        {
            ViewData["NewID"] = service.NewID();
            var VM = new EmployeeViewModel();
            VM.ID = service.NewID();
            return View(VM);
        }

        public IActionResult Edit(string id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //EmployeeViewModel CusVM = service.Find(id);
            //if (CusVM == null)
            //{
            //    return NotFound();
            //}
            //PassCollection obj = new PassCollection();
            //obj.Model = CusVM;
            //obj.Message = "Up";
            //obj.Value = id;
            //return RedirectToAction(nameof(Index), CusVM);
            try
            {

                var Data = service.Find(id);
                return RedirectToAction(nameof(Index), Data);
            }
            catch (Exception err)
            {
                err.ToString();
                return RedirectToAction(nameof(Index));

            }
        }

        // POST: Employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Upsert(EmployeeViewModel collection)
        {
            try
            {
                
                if (!ModelState.IsValid)
                {
                    bool AddData;
                    var newID = service.NewID();
                    if (collection.ID == newID)
                    {
                        AddData = service.Add(collection);

                        if (AddData)
                        {
                            ViewBag.ReturnMsg = "Data Successfully Added.";
                            return RedirectToAction(nameof(Index), "Employee", "Data Successfully Added.");
                        }
                        else
                        {
                            ViewBag.ReturnMsg = "Data Insertion Failed.";
                            //return RedirectToAction(nameof(Index), new PassCollection { Model = collection, Message = "In", Value = null, State = true });
                            var VM = collection;
                            
                            return RedirectToAction(nameof(Index), VM);
                        }

                    }
                    else
                    {
                        AddData = await service.Update(collection);
                        if (AddData)
                        {
                            ViewBag.ReturnMsg = "Data Successfully Updated.";
                            return RedirectToAction(nameof(Index), "Employee", "Data Successfully Updated.");
                        }
                        else
                        {
                            ViewBag.ReturnMsg = "Data Updation Failed.";
                            //return RedirectToAction(nameof(Index), new PassCollection { Model = collection, Message = "Up", Value = null, State = true });
                            var VM = collection;

                            return RedirectToAction(nameof(Index), VM);
                        }
                    }
                }
                else
                {

                    //return RedirectToAction(nameof(Index),new PassCollection{ Model=collection,Message="In",Value=null, State = true });
                    var VM = collection;

                    return RedirectToAction(nameof(Index), VM);
                }
            }
            catch (Exception)
            {
                //return RedirectToAction(nameof(Index), new PassCollection { Model = collection, Message = "In", Value = null,State=true });
                var VM = collection;

                return RedirectToAction(nameof(Index), VM);
            }
        }

        public class PassCollection
        {
            public EmployeeViewModel Model { get; set; }
            public string Message { get; set; }
            public string Value { get; set; }
            public bool State { get; set; }
        }

        // GET: Employee/Delete/5
        [HttpPost]
        public async Task<string> Delete(string id)
        {
            try
            {
                if (id != null)
                {


                    bool AddData = await service.Delete(id);

                    if (AddData)
                    {
                        return "Success";
                    }
                    else
                        return "Failed";

                }
                else
                {
                    return "Failed";
                }
            }
            catch (Exception err)
            {
                return err.Message;
            }
        }

        [HttpPost]
        public IActionResult ChangePass(int ID, string Email,string Password)
        {
            if (Email!=null&&Password!=null)
            {
                bool ChangePassword = service.ChangePass(ID.ToString(),Email,Password);
                if (ChangePassword)
                {
                    TempData["message"] = "Password Changed";
                    TempData["ToastrType"] = "success";
                    return RedirectToAction(nameof(Profile));
                }
                else
                {
                    TempData["message"] = "Password Change Error";
                    TempData["ToastrType"] = "error";
                    return RedirectToAction(nameof(Profile));
                }
            }
            else
            {
                TempData["message"] = "Password Change Error";
                TempData["ToastrType"] = "error";
                return RedirectToAction(nameof(Profile));
            }

        }
    }
}
