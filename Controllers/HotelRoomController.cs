using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LakDerana_HotelChainManagement.Data;
using LakDerana_HotelChainManagement.Models;
using LakDerana_HotelChainManagement.Services;
using AutoMapper;
using LakDerana_HotelChainManagement.ViewModels;

namespace LakDerana_HotelChainManagement.Controllers
{
    public class HotelRoomController : Controller
    {
        private readonly ApplicationDbContext _context;
        private HotelRoomService service;

        public HotelRoomController(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            service = new HotelRoomService(context,mapper);
        }

        // GET: HotelRoom
        public IActionResult Index(HotelRoomViewModel data)
        {
            if (data.ID != 0 && data.RoomStatus != null)
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

        // GET: HotelRoom/Details/5
        public ActionResult Details(int id)
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

        // GET: HotelRoom/Create
        public ActionResult Create()
        {
            ViewData["NewID"] = service.NewID();
            var VM = new HotelRoomViewModel();
            VM.ID = service.NewID();
            return View(VM);
        }

        // POST: HotelRoom/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HotelRoomViewModel collection)
        {

            try
            {
                if (collection != null)
                {
                    collection.ID = 0;
                    bool AddData = service.Add(collection);

                    if (AddData)
                    {
                        return RedirectToAction(nameof(Index), "HotelRoom", "Data Successfully Added.");
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


        // POST: EmployeeController/Upsert
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> upsert(HotelRoomViewModel collection)
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
                            return RedirectToAction(nameof(Index), "HotelRoom", "Data Successfully Added.");
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
                            return RedirectToAction(nameof(Index), "HotelRoom", "Data Successfully Updated.");
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

        // GET: HotelRoom/Edit/5
        public ActionResult Edit(int id)
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

        // POST: HotelRoom/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(HotelRoomViewModel collection)
        {
            try
            {
                if (collection != null)
                {

                    bool AddData = await service.Update(collection);

                    if (AddData)
                    {
                        return RedirectToAction(nameof(Index), "HotelRoom", "Data Successfully Updated.");
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
        // GET: HotelRoom/Delete/5
        [HttpPost]
        public async Task<string> Delete(int id)
        {
            try
            {
                if (id != 0)
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
