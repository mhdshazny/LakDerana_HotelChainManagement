using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LakDerana_HotelChainManagement.Data;
using LakDerana_HotelChainManagement.Services;
using AutoMapper;
using LakDerana_HotelChainManagement.ViewModels;
using Microsoft.AspNetCore.Http;

namespace LakDerana_HotelChainManagement.Controllers
{
    public class ReservedRoomController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ReservedRoomService service;
        private readonly ReservationService RsvService;
        private readonly HotelRoomService RoomService;

        public ReservedRoomController(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            service = new ReservedRoomService(context, mapper);
            RsvService = new ReservationService(context, mapper);
            RoomService = new HotelRoomService(context, mapper);
        }

        // GET: ReservedRoom
        public IActionResult Index(ReservedRoomViewModel data)
        {
            if (data.ID != 0 && data.Status != null)
            {
                ViewBag.InOut = "Up";
            }
            else
            {
                ViewBag.InOut = "In";
            }

            var GetList = service.GetList();
            //ViewData["NewID"] = service.NewID();
            ViewData["RerservationList"] = new SelectList(RsvService.GetUpdatedDDList(HttpContext.Session.GetString("UserHotelID")), "ID", "Hotel");
            ViewData["RoomList"] = new SelectList(RoomService.GetUpdatedDDList(HttpContext.Session.GetString("UserHotelID")), "ID", "HotelID");


            return View(GetList);
        }

        // GET: ReservedRoom/Details/5
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

        // GET: ReservedRoom/Create
        public ActionResult Create()
        {
            //ViewData["NewID"] = service.NewID();
            var VM = new ReservedRoomViewModel();
            VM.ID = 0;
            return View(VM);
        }


        // POST: ReservedRoomController/Upsert
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> upsert(ReservedRoomViewModel collection)
        {

            try
            {
                if (collection != null)
                {
                    bool AddData;
                    var newID = 0;



                    if (collection.ID == newID)
                    {
                        AddData = service.Add(collection);

                        if (AddData)
                        {
                            ViewBag.ReturnMsg = "Data Successfully Added.";
                            return RedirectToAction(nameof(Index), "ReservedRoom", "Data Successfully Added.");
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
                            return RedirectToAction(nameof(Index), "ReservedRoom", "Data Successfully Updated.");
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

        // GET: ReservedRoom/Edit/5
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

        // GET: ReservedRoom/Delete/5
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
