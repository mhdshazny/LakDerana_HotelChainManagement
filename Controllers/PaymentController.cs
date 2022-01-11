using AutoMapper;
using LakDerana_HotelChainManagement.Data;
using LakDerana_HotelChainManagement.Services;
using LakDerana_HotelChainManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LakDerana_HotelChainManagement.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ApplicationDbContext context;
        private PaymentService service;
        private ReservationService ReservationService;
        private InquiryService CusService;

        public PaymentController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            service = new PaymentService(context, mapper);
            ReservationService = new ReservationService(context, mapper);
            CusService = new InquiryService(context, mapper);
        }
        public IActionResult Index(PaymentViewModel data)
        {
            if (data.ID >0 && data.PaymentStatus != null)
            {
                ViewBag.InOut = "Up";
            }
            else
            {
                ViewBag.InOut = "In";
            }

            var GetList = service.GetList();
            ViewData["NewID"] = service.NewID();
            ViewData["PendingReservationList"] = new SelectList(ReservationService.GetPayPendingDDList(), "ID", "Hotel");
            //ViewData["HotelList"] = new SelectList(HotelService.GetDDList(), "ID", "HotelSpecialName");


            return View(GetList);
        }


        public IActionResult Details(string id)
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

        public ActionResult Create()
        {
            ViewData["NewID"] = service.NewID();
            var VM = new PaymentViewModel();
            VM.ID = int.Parse(service.NewID());
            return View(VM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PaymentViewModel collection)
        {

            try
            {
                if (collection != null)
                {
                    //collection.ID = service.NewID();
                    bool AddData = service.Add(collection);

                    if (AddData)
                    {
                        return RedirectToAction(nameof(Index), "Payment", "Data Successfully Added.");
                    }
                    else
                    {
                        return RedirectToAction(nameof(Index), collection);
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> upsert(PaymentViewModel collection)
        {

            try
            {
                if (collection != null)
                {
                    bool AddData;
                    var newID = int.Parse(service.NewID());



                    if (collection.ID == newID)
                    {
                        AddData = service.Add(collection);

                        if (AddData)
                        {
                            ViewBag.ReturnMsg = "Data Successfully Added.";
                            return RedirectToAction(nameof(Index), "Payment", "Data Successfully Added.");
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
                            return RedirectToAction(nameof(Index), "Payment", "Data Successfully Updated.");
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


        public ActionResult Edit(string id)
        {
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
