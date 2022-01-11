using AutoMapper;
using LakDerana_HotelChainManagement.Data;
using LakDerana_HotelChainManagement.Models;
using LakDerana_HotelChainManagement.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LakDerana_HotelChainManagement.Services
{
    public class PaymentService : IService<PaymentViewModel>
    {
        private readonly ApplicationDbContext context;
        private readonly ReservationService ReservationService;

        public PaymentService(ApplicationDbContext context, IMapper mapper):base(mapper)
        {
            this.context = context;
            ReservationService = new ReservationService(context,mapper);
        }

        public override bool Add(PaymentViewModel collection)
        {
            try
            {
                if (collection != null)
                {
                    bool status = UpdateAmounts(ref collection);
                    if (status)
                    {
                        var MapData = GetMapped(collection);
                        MapData.ID = 0;
                        context.Add(MapData);
                        context.SaveChanges();
                        return true;
                    }
                    else return false;
                }
                else
                    return false;
            }
            catch (Exception err)
            {
                err.ToString();
                return false;
            }
        }

        public async override Task<bool> Delete(string id)
        {
            try
            {
                if (id != null)
                {
                    var FoundRecord = context.PaymentData.Find(int.Parse(id));
                    context.Remove(FoundRecord);
                    await context.SaveChangesAsync();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception er)
            {

                return false;
            }
        }

        public override PaymentViewModel Find(string id)
        {
            var FoundRecord = context.PaymentData.Where(i=>i.ID==int.Parse(id)).FirstOrDefault();
            var VM = GetMapped(FoundRecord);
            return VM;
        }

        public override List<PaymentViewModel> GetDDList()
        {
            var DataList = context.PaymentData.ToList();
            var GetList = new List<PaymentViewModel>();
            foreach (var item in DataList)
            {
                var VM = GetMapped(item);
                VM.PaymentStatus = VM.ReservationID + " " + VM.PaymentStatus + ". (" + VM.TotalPayableAmount + ")";
                GetList.Add(VM);
            }
            return GetList;
        }

        public override List<PaymentViewModel> GetList()
        {
            var DataList = context.PaymentData.ToList();
            var GetList = new List<PaymentViewModel>();
            foreach (var item in DataList)
            {
                var VM = GetMapped(item);
                GetList.Add(VM);
            }
            return GetList;
        }

        public override string NewID()
        {
            var data = context.PaymentData.ToList();
            int NewID = 1;
            if (data.Count > 0)
            {
                NewID = (data.Select(i => i.ID).Max()) + 1;
            }
            return NewID.ToString();
        }

        public async override Task<bool> Update(PaymentViewModel collection)
        {
            try
            {
                if (collection != null)
                {
                    bool AmountStatus = UpdateAmounts(ref collection);
                    bool PaymentStatusInReservation = await UpdateReservationPaymentStatusAsync(collection);
                    if (AmountStatus)
                    {
                        var MapData = GetMapped(collection);
                        context.Update(MapData);
                        await context.SaveChangesAsync();
                        return true;
                    }
                    else return false;
                }
                else
                    return false;
            }
            catch (Exception err)
            {
                err.ToString();
                return false;
            }
        }

        private async Task<bool> UpdateReservationPaymentStatusAsync(PaymentViewModel collection)
        {
            try
            {
                bool ReservationStatusUpdated = await ReservationService.UpdateStatusAsync(collection);
                return ReservationStatusUpdated;
            }
            catch (Exception er)
            {
                return false;
            }
        }

        internal bool UpdateAmounts(ref PaymentViewModel collection)
        {
            try
            {
                collection.TotalPayableAmount = (collection.ReservationFee + collection.HotelServiceCharge + collection.NetAmount) - collection.DiscountAmount;
                return true;
            }
            catch (Exception er)
            {
                throw;
            }
        }
        internal bool UpdateAmounts(ref PaymentModel collection)
        {
            try
            {
                collection.TotalPayableAmount = (collection.ReservationFee + collection.HotelServiceCharge + collection.NetAmount) - collection.DiscountAmount;
                return true;
            }
            catch (Exception er)
            {
                throw;
            }
        }
    }
}
