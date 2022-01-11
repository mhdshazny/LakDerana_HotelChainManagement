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
using Microsoft.AspNetCore.Http;

namespace LakDerana_HotelChainManagement.Controllers
{
    public class HotelController : Controller
    {
        private readonly ApplicationDbContext context;
        private HotelService service;
        private EmployeeService EmpService;

        public HotelController(ApplicationDbContext context,IMapper mapper)
        {
            this.context = context;
            service = new HotelService(context, mapper);
            EmpService = new EmployeeService(context, mapper);
        }

        // GET: Hotel
        public IActionResult Index(HotelViewModel data)
        {
            if (data.ID != null&&data.Status!=null)
            {
                ViewBag.InOut = "Up";
            }
            else
            {
                ViewBag.InOut = "In";
            }

            var GetList = service.GetList();
            ViewData["NewID"] = service.NewID();
            ViewData["ExecutiveEmployeeList"] = new SelectList(EmpService.GetDDList(), "ID", "lName");


            return View(GetList);
        }

        // GET: Hotel/Details/5
        public ActionResult Details(string id)
        {
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

        // GET: Hotel/Create
        public ActionResult Create()
        {
            ViewData["NewID"] = service.NewID();
            var VM = new EmployeeViewModel();
            VM.ID = service.NewID();
            return View(VM);
        }

        // POST: Hotel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HotelViewModel collection)
        {

            try
            {
                if (collection != null)
                {
                    collection.ID = service.NewID();
                    bool AddData = service.Add(collection);

                    if (AddData)
                    {
                        return RedirectToAction(nameof(Index), "Hotel", "Data Successfully Added.");
                    }
                    else
                        return RedirectToAction(nameof(Index), collection);

                }
                else
                {
                    return RedirectToAction(nameof(Index), collection);
                }
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index), collection);
            }
        }

        // POST: HotelController/Upsert
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> upsert(HotelViewModel collection)
        {

            try
            {
                if (collection != null)
                {
                    bool AddData;
                    var newID = service.NewID();



                    if (collection.ID == newID)
                    {
                        AddData = service.Add(collection);

                        if (AddData)
                        {
                            ViewBag.ReturnMsg = "Data Successfully Added.";
                            return RedirectToAction(nameof(Index), "Hotel", "Data Successfully Added.");
                        }
                        else
                        {
                            ViewBag.ReturnMsg = "Data Insertion Failed.";
                            return RedirectToAction(nameof(Index), collection);
                        }

                    }
                    else
                    {
                        AddData = await service.Update(collection);
                        if (AddData)
                        {
                            ViewBag.ReturnMsg = "Data Successfully Updated.";
                            return RedirectToAction(nameof(Index), "Hotel", "Data Successfully Updated.");
                        }
                        else
                        {
                            ViewBag.ReturnMsg = "Data Updation Failed.";
                            return RedirectToAction(nameof(Index), collection);
                        }
                    }



                }
                else
                {
                    return RedirectToAction(nameof(Index), collection);
                }
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index), collection);
            }
        }
        // GET: Hotel/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                //ViewData["EmpCatList"] = new SelectList(EmpCategoryService.EmpCatList(), "EmpCat_ID", "EmpCat_Type");

                var Data = service.Find(id);
                return RedirectToAction(nameof(Index), Data);
            }
            catch (Exception err)
            {
                err.ToString();
                return RedirectToAction(nameof(Index));

            }
        }

        // POST: Hotel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(HotelViewModel collection)
        {
            try
            {
                if (collection != null)
                {

                    bool AddData = await service.Update(collection);

                    if (AddData)
                    {
                        return RedirectToAction(nameof(Index), "Hotel", "Data Successfully Updated.");
                    }
                    else
                        return RedirectToAction(nameof(Edit), collection);

                }
                else
                {
                    return RedirectToAction(nameof(Edit), collection);
                }
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Edit), collection);
            }
        }

        // POST: Hotel/Delete/5
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
