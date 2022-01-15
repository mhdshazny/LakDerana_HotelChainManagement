using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LakDerana_HotelChainManagement.Data;
using LakDerana_HotelChainManagement.ViewModels;
using AutoMapper;
using LakDerana_HotelChainManagement.Services;

namespace LakDerana_HotelChainManagement.Controllers
{
    public class InquiryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly InquiryService service;

        public InquiryController(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            service = new InquiryService(context, mapper);
        }

        // GET: Inquiry
        public IActionResult Index(CustomerViewModel data)
        {

            if (TempData["Instruction"] != null)
            {
                if (TempData["Instruction"].ToString() == "Up")
                {
                    ViewBag.message = TempData["Message"].ToString();
                    ViewBag.ToastrType = TempData["ToastrType"].ToString();
                    ViewBag.InOut = "Up";
                    ViewBag.ReturnMsg = TempData["Message"].ToString();

                    TempData.Remove("Message");
                    TempData.Remove("ToastrType");
                    TempData.Remove("Message");
                    TempData.Remove("Instruction");
                }
                else
                {
                    ViewBag.message = TempData["Message"].ToString();
                    ViewBag.ToastrType = TempData["ToastrType"].ToString();
                    ViewBag.ReturnMsg = TempData["Message"].ToString();

                    ViewBag.InOut = "In";
                    ViewData["NewID"] = service.NewID();

                    TempData.Remove("Message");
                    TempData.Remove("ToastrType");
                    TempData.Remove("Message");
                    TempData.Remove("Instruction");

                }
            }
            else
            {
                ViewBag.InOut = "In";

                ViewData["NewID"] = service.NewID();
            }

            var GetList = service.GetList();


            return View(GetList);
        }

        // GET: Inquiry/Details/5
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var VM = service.Find(id);
            if (VM == null)
            {
                return NotFound();
            }

            return View(VM);
        }

        // GET: Inquiry/Create
        public IActionResult Create()
        {
            ViewData["NewID"] = service.NewID();
            var VM = new EmployeeViewModel();
            VM.ID = service.NewID();
            return View(VM);
        }

        // GET: Inquiry/Edit/5
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var CusVM = service.Find(id);
            if (CusVM == null)
            {
                return NotFound();
            }
            TempData["Instruction"] = "Up";
            TempData["Message"] = "Ready to Update";
            TempData["ToastrType"] = "success";
            return RedirectToAction(nameof(Index),CusVM);
        }


        // POST: InquiryController/Upsert
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Upsert(CustomerViewModel collection)
        {
            try
            {
                if (collection != null)
                {
                    bool AddData;
                    var newID = service.NewID();



                    if (collection.ID == newID)
                    {
                        if (ValidateModel(collection))
                        {
                            AddData = service.Add(collection);

                            if (AddData)
                            {
                                TempData["Instruction"] = "In";
                                TempData["Message"] = "Data Successfully Added.";
                                TempData["ToastrType"] = "success";
                                return RedirectToAction(nameof(Index), "Inquiry", "Data Successfully Added.");
                            }
                            else
                            {
                                TempData["Instruction"] = "In";
                                TempData["Message"] = "Data Insertion Failed.";
                                TempData["ToastrType"] = "error";
                                return RedirectToAction(nameof(Index), collection);
                            }
                        }
                        else
                        {
                            TempData["Instruction"] = "In";
                            TempData["Message"] = "Please Provide All Necessary Data to Proceed.";
                            TempData["ToastrType"] = "error";
                            return RedirectToAction(nameof(Index), collection);
                        }
                    }
                    else
                    {
                        AddData = await service.Update(collection);
                        if (AddData)
                        {
                            TempData["Instruction"] = "In";
                            TempData["Message"] = "Data Successfully Updated.";
                            TempData["ToastrType"] = "success";

                            return RedirectToAction(nameof(Index), "Inquiry", "Data Successfully Updated.");
                        }
                        else
                        {
                            TempData["Instruction"] = "Up";
                            TempData["Message"] = "Data Updation Failed.";
                            TempData["ToastrType"] = "error";
                            return RedirectToAction(nameof(Index),collection);
                        }
                    }



                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public bool ValidateModel(CustomerViewModel collection) 
        {
            bool valid = true;
            if (collection.CusAddress==null||collection.CusContact==null||collection.CusEmail==null||
                collection.CusfName==null||collection.CusGender==null||collection.CuslName==null||collection.CusNIC==null)
            {
                valid = false;
            }

            return valid;
        }


        // POST: InquiryController/Delete/5
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
    }
}
