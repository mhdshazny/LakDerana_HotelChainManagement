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
    public class ReservationController : Controller
    {
        private readonly ApplicationDbContext context;
        private ReservationService service;
        private HotelService HotelService;
        private InquiryService CusService;

        public ReservationController(ApplicationDbContext context,IMapper mapper)
        {
            this.context = context;
            service = new ReservationService(context, mapper);
            HotelService = new HotelService(context, mapper);
            CusService = new InquiryService(context, mapper);
        }

        // GET: Reservation
        public IActionResult Index(ReservationViewModel data)
        {
            if (data.ID != null && data.Status != null)
            {
                ViewBag.InOut = "Up";
            }
            else
            {
                ViewBag.InOut = "In";
            }

            var GetList = service.GetList();
            ViewData["NewID"] = service.NewID();
            ViewData["CustomerList"] = new SelectList(CusService.GetDDList(), "ID", "CuslName");
            ViewData["HotelList"] = new SelectList(HotelService.GetDDList(), "ID", "HotelSpecialName", HttpContext.Session.GetString("UserHotelID").ToString());


            return View(GetList);
        }

        // GET: Reservation/Details/5
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

        // GET: Reservation/Create
        public ActionResult Create()
        {
            ViewData["NewID"] = service.NewID();
            var VM = new ReservationViewModel();
            VM.ID = service.NewID();
            VM.DateRage = VM.CheckInDate.ToString("MM/dd/yyyy") + " = " + VM.CheckoutDate.ToString("MM/dd/yyyy");
            return View(VM);
        }

        // POST: Reservation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReservationViewModel collection)
        {

            try
            {
                if (collection != null)
                {
                    collection.ID = service.NewID();
                    bool AddData = service.Add(collection);

                    if (AddData)
                    {
                        return RedirectToAction(nameof(Index), "Reservation", "Data Successfully Added.");
                    }
                    else
                    {
                        collection.DateRage = collection.CheckInDate.ToString("MM/dd/yyyy") + " = " + collection.CheckoutDate.ToString("MM/dd/yyyy");
                        return RedirectToAction(nameof(Index), collection);
                    }

                }
                else
                {
                    collection.DateRage = collection.CheckInDate.ToString("MM/dd/yyyy") + " = " + collection.CheckoutDate.ToString("MM/dd/yyyy");
                    return RedirectToAction(nameof(Index), collection);
                }
            }
            catch (Exception)
            {
                collection.DateRage = collection.CheckInDate.ToString("MM/dd/yyyy") + " = " + collection.CheckoutDate.ToString("MM/dd/yyyy");
                return RedirectToAction(nameof(Index), collection);
            }
        }


        // POST: ReservationController/Upsert
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> upsert(ReservationViewModel collection)
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
                            return RedirectToAction(nameof(Index), "Reservation", "Data Successfully Added.");
                        }
                        else
                        {
                            ViewBag.ReturnMsg = "Data Insertion Failed.";
                            collection.DateRage = collection.CheckInDate.ToString("MM/dd/yyyy") + " = " + collection.CheckoutDate.ToString("MM/dd/yyyy");
                            return RedirectToAction(nameof(Index), collection);
                        }

                    }
                    else
                    {
                        AddData = await service.Update(collection);
                        if (AddData)
                        {
                            ViewBag.ReturnMsg = "Data Successfully Updated.";
                            return RedirectToAction(nameof(Index), "Reservation", "Data Successfully Updated.");
                        }
                        else
                        {
                            ViewBag.ReturnMsg = "Data Updation Failed.";
                            collection.DateRage = collection.CheckInDate.ToString("MM/dd/yyyy") + " = " + collection.CheckoutDate.ToString("MM/dd/yyyy");
                            return RedirectToAction(nameof(Index), collection);
                        }
                    }



                }
                else
                {
                    collection.DateRage = collection.CheckInDate.ToString("MM/dd/yyyy") + " = " + collection.CheckoutDate.ToString("MM/dd/yyyy");
                    return RedirectToAction(nameof(Index), collection);
                }
            }
            catch (Exception)
            {
                collection.DateRage = collection.CheckInDate.ToString("MM/dd/yyyy") + " = " + collection.CheckoutDate.ToString("MM/dd/yyyy");
                return RedirectToAction(nameof(Index), collection);
            }
        }


        // GET: Reservation/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {

                var Data = service.Find(id);
                Data.DateRage = Data.CheckInDate.ToString("MM/dd/yyyy") + " = " + Data.CheckoutDate.ToString("MM/dd/yyyy");
                return RedirectToAction(nameof(Index), Data);
            }
            catch (Exception err)
            {
                err.ToString();
                return RedirectToAction(nameof(Index));

            }
        }
        // POST: Reservation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(ReservationViewModel collection)
        {
            try
            {
                if (collection != null)
                {

                    bool AddData = await service.Update(collection);

                    if (AddData)
                    {
                        return RedirectToAction(nameof(Index), "Reservation", "Data Successfully Updated.");
                    }
                    else
                    {
                        collection.DateRage = collection.CheckInDate.ToString("MM/dd/yyyy") + " = " + collection.CheckoutDate.ToString("MM/dd/yyyy");
                        return RedirectToAction(nameof(Edit), collection);
                    }

                }
                else
                {
                    collection.DateRage = collection.CheckInDate.ToString("MM/dd/yyyy") + " = " + collection.CheckoutDate.ToString("MM/dd/yyyy");
                    return RedirectToAction(nameof(Edit), collection);
                }
            }
            catch (Exception)
            {
                collection.DateRage = collection.CheckInDate.ToString("MM/dd/yyyy") + " = " + collection.CheckoutDate.ToString("MM/dd/yyyy");
                return RedirectToAction(nameof(Edit), collection);
            }
        }

        // POST: Reservation/Delete/5
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


        // GET: Reservation/Edit/5

        public ActionResult Checkout(string id)
        {
            try
            {

                var Data = service.FindCheckout(id);
                return View(Data);
            }
            catch (Exception err)
            {
                err.ToString();
                return RedirectToAction(nameof(Index));

            }
        }

    }
}
