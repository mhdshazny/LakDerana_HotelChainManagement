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
    public class CheckoutController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly CheckoutService service;
        public CheckoutController(ApplicationDbContext context,IMapper mapper)
        {
            this.context = context;
            service = new CheckoutService(context,mapper);
        }

        [HttpPost]
        public bool CheckoutNow(string id)
        {
            try
            {
                if (id != null)
                {
                    bool ReturnData = service.Checkout(id);
                    return ReturnData;
                }
                else
                    return false;

            }
            catch (Exception er)
            {
                return false;
            }
        }

    }
}
