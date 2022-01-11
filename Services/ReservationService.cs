using AutoMapper;
using LakDerana_HotelChainManagement.Data;
using LakDerana_HotelChainManagement.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LakDerana_HotelChainManagement.Services
{
    public class ReservationService : IService<ReservationViewModel>
    {
        private ApplicationDbContext context;
        private IMapper mapper;
        //private readonly ReservedRoomService ReservedRoomService;
        //private readonly InquiryService InquiryService;
        //private readonly PaymentService PaymentService;
        //private readonly HotelService HotelService;
        public ReservationService(ApplicationDbContext context, IMapper mapper):base(mapper)
        {
            this.context = context;
            this.mapper = mapper;
            //InquiryService = new InquiryService(context, mapper);
            //PaymentService = new PaymentService(context, mapper);
            //ReservedRoomService = new ReservedRoomService(context, mapper);
            //HotelService = new HotelService(context, mapper);
        }

        public override bool Add(ReservationViewModel collection)
        {
            try
            {
                if (collection != null)
                {
                    collection.CheckInDate = Convert.ToDateTime(collection.DateRage.Substring(0,10));
                    collection.CheckInDate.AddHours(14);

                    collection.CheckoutDate = Convert.ToDateTime(collection.DateRage.Substring(13,10));
                    collection.CheckoutDate = collection.CheckoutDate.AddHours(14.0);
                    if (UpdatePayment(collection))
                    {
                        var MapData = GetMapped(collection);
                        context.Add(MapData);
                        context.SaveChanges();
                        return true;
                    }
                    return false;

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

        internal List<ReservationViewModel> GetPayPendingDDList()
        {
            var DataList = context.ReservationData.ToList();
            var PayList = context.PaymentData.ToList();

            var GetList = new List<ReservationViewModel>();

            foreach (var RsvItem in DataList)
            {
                foreach (var PayItem in PayList)
                {
                    if (RsvItem.ID==PayItem.ReservationID)
                    {
                        var VM = GetMapped(RsvItem);
                        VM.Hotel = VM.Hotel + " " + VM.Customer + ". (" + VM.Status + ")";
                        GetList.Add(VM);
                    }
                }
            }
            return GetList;
        }

        public bool UpdatePayment(ReservationViewModel collection)
        {
            try
            {
                if (collection != null)
                {
                    PaymentViewModel PayVM = new PaymentViewModel
                    {
                        ReservationID=collection.ID,
                        AdvancePaymentAmount=0,
                        DiscountAmount=0,
                        HotelServiceCharge=0,
                        ReservationFee=2000,
                        NetAmount=0,
                        TotalPayableAmount=0,
                        FinalPaymentDate=collection.CheckoutDate,
                        PaymentStatus="Advanced Payment Pending"
                    };
                    
                    var MapData = GetMapped(PayVM);
                    context.Add(MapData);
                    context.SaveChanges();
                    return true;
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
                    var FoundRecord = context.ReservationData.Find(id);
                    context.Remove(FoundRecord);
                    await context.SaveChangesAsync();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public override ReservationViewModel Find(string id)
        {
            var FoundRecord = context.ReservationData.Find(id);
            var VM = GetMapped(FoundRecord);

            return VM;
        }

        public override List<ReservationViewModel> GetDDList()
        {
            //var DataList = context.ReservationData.Where(i=>i.Status== "Reservation Approved").ToList();
            //var GetList = new List<ReservationViewModel>();
            //foreach (var item in DataList)
            //{
            //    var VM = GetMapped(item);
            //    VM.Hotel = VM.ID + " " + VM.Customer + ". (" + VM.Status + ")";
            //    GetList.Add(VM);
            //}
            return null;
        }
        public List<ReservationViewModel> GetUpdatedDDList(string HotelID)
        {
            var DataList = context.ReservationData.Where(i=>i.Status== "Reservation Approved"&&i.Hotel==HotelID).ToList();
            var GetList = new List<ReservationViewModel>();
            foreach (var item in DataList)
            {
                var VM = GetMapped(item);
                VM.Hotel = VM.ID + " " + VM.Customer + ". (" + VM.Status + ")";
                GetList.Add(VM);
            }
            return GetList;
        }

        internal async Task<bool> UpdateStatusAsync(PaymentViewModel collection)
        {
            try
            {
                var Reservation = Find(collection.ReservationID);
                Reservation.PaymentStatus = collection.PaymentStatus;
                return await Update(Reservation);
            }
            catch (Exception er)
            {
                return false;
            }


        }

        public override List<ReservationViewModel> GetList()
        {
            var DataList = context.ReservationData.ToList();
            var GetList = new List<ReservationViewModel>();
            foreach (var item in DataList)
            {
                var VM = GetMapped(item);
                GetList.Add(VM);
            }
            return GetList;
        }

        public override string NewID()
        {
            var Id = context.ReservationData
                .Max(i => i.ID);
            string NewID = "RSV0000001";
            if (Id != null)
            {
                return (GetNewID(int.Parse(Id.Substring(3, 7)), "RSV"));

            }
            return NewID;
        }

        public async override Task<bool> Update(ReservationViewModel collection)
        {
            try
            {
                if (collection != null)
                {
                    if (collection.DateRage!=null)
                    {
                        collection.CheckInDate = Convert.ToDateTime(collection.DateRage.Substring(0, 10));
                        collection.CheckInDate.AddHours(14);

                        collection.CheckoutDate = Convert.ToDateTime(collection.DateRage.Substring(13, 10));
                        collection.CheckoutDate = collection.CheckoutDate.AddHours(14);
                    }
                    var MapData = GetMapped(collection);
                    context.Update(MapData);
                    int Changes = await context.SaveChangesAsync();

                    bool AllStatusUpdated = false;
                    if (Changes>0)
                    {
                        AllStatusUpdated = await UpdateAllStatusAsync(collection);
                    }
                    return AllStatusUpdated;
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

        private async Task<bool> UpdateAllStatusAsync(ReservationViewModel collection)
        {
            try
            {
                if (collection != null)
                {
                    var PaymentRecords = context.PaymentData.Where(i=>i.ReservationID==collection.ID).FirstOrDefault();
                    //var ReservedRoomsRecord = context.ReservedRoomData.Where(i=>i.ReservationID==collection.ID).ToList();
                    if (collection.Status == "Reservation Cancelled")
                    {
                        //foreach (var item in ReservedRoomsRecord)
                        //{
                        //    bool state = await ReservedRoomService.Update(GetMapped(item));
                        //}


                        PaymentRecords.PaymentStatus = "Reservation Cancelled";
                        context.Update(PaymentRecords);
                        await context.SaveChangesAsync();


                    }
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {

                return false;
            }
        }

        internal CheckoutViewModel FindCheckout(string id)
        {
            try
            {
                ReservationViewModel Reservation = Find(id);

                var Customer = context.CusData.Find(Reservation.Customer);
                CustomerViewModel CustomerVM = GetMapped(Customer);

                var Hotel = context.HotelData.Find(Reservation.Hotel);
                HotelViewModel HotelVM = GetMapped(Hotel);

                var Payment = context.PaymentData.Where(i=>i.ReservationID==Reservation.ID).FirstOrDefault();
                PaymentViewModel PaymentVM = GetMapped(Payment);

                var ReservedRooms = context.ReservedRoomData.Where(i=>i.ReservationID==Reservation.ID).ToList();
                List<ReservedRoomViewModel> ReservedRoomsVM = new List<ReservedRoomViewModel>();
                foreach (var item in ReservedRooms)
                {
                    ReservedRoomViewModel ReservedRoomVM = GetMapped(item);
                    ReservedRoomsVM.Add(ReservedRoomVM);
                }


                CheckoutViewModel Final = new CheckoutViewModel
                {
                    Reservation = Reservation,
                    Customer = CustomerVM,
                    HotelData = HotelVM,
                    PaymentData = PaymentVM,
                    ReservedRooms = ReservedRoomsVM
                };

                return Final;
            }
            catch (Exception er)
            {
                throw;
            }


        }
    }
}
