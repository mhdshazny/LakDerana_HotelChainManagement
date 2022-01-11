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
    public class BarController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly BarService service;

        public BarController(ApplicationDbContext context,IMapper mapper )
        {
            this.context = context;
            this.mapper = mapper;
            service = new BarService(context, mapper);
        }

        // GET: Bar
        public IActionResult Index(BarViewModel data)
        {
            if (data.ID != 0 && data.Status != null)
            {
                ViewBag.ReturnMsg = "Data Insertion Failed.";
                ViewBag.InOut = "In";
            }
            else
            {

                ViewBag.InOut = "In";
            }

            var GetList = service.GetList();
            ViewData["NewID"] = service.NewID();


            return View(GetList);
        }

        // GET: Bar/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var Data = service.Find(id.ToString());
                return View(Data);
            }
            catch (Exception er)
            {
                er.ToString();
                return RedirectToAction(nameof(Index));
            }

        }

        // GET: Bar/Create
        public ActionResult Create()
        {
            ViewData["NewID"] = service.NewID();
            var VM = new BarViewModel();
            return View(VM);
        }

        // POST: Bar/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BarViewModel collection)
        {

            try
            {
                if (collection != null)
                {
                    collection.ID = 0;
                    bool AddData = service.Add(collection);

                    if (AddData)
                    {
                        return RedirectToAction(nameof(Index), "Bar", "Data Successfully Added.");
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

        // POST: BarController/Upsert
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult upsert(BarViewModel collection)
        {

            try
            {
                if (collection != null)
                {
                    bool AddData;
                    //var newID = service.NewID();

                    string isExist = service.isExist(collection);

                    if (isExist!= "IDNoExist" && isExist!= "DateNoExist")
                    {
                        AddData = service.Add(collection);

                        if (AddData)
                        {
                            ViewBag.ReturnMsg = "Data Successfully Added.";
                            return RedirectToAction(nameof(Index), "Bar", "Data Successfully Added.");
                        }
                        else
                        {
                            ViewBag.ReturnMsg = "Data Insertion Failed.";
                            return RedirectToAction(nameof(Index), collection);
                        }

                    }
                    else if (isExist != "DateExist")
                    {
                            ViewBag.ReturnMsg = "Data Already Exists.";
                            return RedirectToAction(nameof(Index), collection);
                    }
                    else
                    {
                            ViewBag.ReturnMsg = "Data Updation Failed.";
                            return RedirectToAction(nameof(Index), collection);
                    }



                }
                else
                {
                    return RedirectToAction(nameof(Index), collection);
                }
            }
            catch (Exception er)
            {
                return RedirectToAction(nameof(Index), collection);
            }
        }


        // GET: Bar/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                //ViewData["EmpCatList"] = new SelectList(EmpCategoryService.EmpCatList(), "EmpCat_ID", "EmpCat_Type");

                var Data = service.Find(id.ToString());
                return RedirectToAction(nameof(Index), Data);
            }
            catch (Exception err)
            {
                err.ToString();
                return RedirectToAction(nameof(Index));

            }
        }

        // POST: Bar/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(BarViewModel collection)
        {
            try
            {
                if (collection != null)
                {

                    bool UpdateData = await service.Update(collection);

                    if (UpdateData)
                    {
                        return RedirectToAction(nameof(Index), "Bar", "Data Successfully Updated.");
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
        // GET: Bar/Delete/5
        [HttpPost]
        public async Task<string> Delete(int id)
        {
            try
            {
                if (id != 0)
                {


                    bool AddData = await service.Delete(id.ToString());

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
