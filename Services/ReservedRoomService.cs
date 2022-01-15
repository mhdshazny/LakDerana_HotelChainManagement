using AutoMapper;
using LakDerana_HotelChainManagement.Data;
using LakDerana_HotelChainManagement.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LakDerana_HotelChainManagement.Services
{
    public class ReservedRoomService : IService<ReservedRoomViewModel>
    {
        private ApplicationDbContext context;
        private IMapper mapper;
        private PaymentService PaymentService;
        private HotelRoomService HotelRoomService;
        private EmployeeService EmployeeService;
        private HotelService HotelService;
        private ReservationService Reservations;
        private InquiryService Customers;

        public ReservedRoomService(ApplicationDbContext context, IMapper mapper): base(mapper)
        {
            this.context = context;
            this.mapper = mapper;
            HotelRoomService = new HotelRoomService(context, mapper);
            PaymentService = new PaymentService(context, mapper);
            EmployeeService = new EmployeeService(context, mapper);
            HotelService = new HotelService(context, mapper);
            Reservations = new ReservationService(context, mapper);
            Customers = new InquiryService(context, mapper);
        }

        public override bool Add(ReservedRoomViewModel collection)
        {
            try
            {
                if (collection != null)
                {
                    collection.FromDate = Convert.ToDateTime(collection.DateRage.Substring(0, 10));
                    collection.FromDate.AddHours(14);

                    collection.ToDate = Convert.ToDateTime(collection.DateRage.Substring(13, 10));
                    collection.ToDate = collection.ToDate.AddHours(14);

                    if (UpdateCalc(ref collection))
                    {
                        var MapData = GetMapped(collection);
                        context.Add(MapData);
                        int changes = context.SaveChanges();
                        bool RoomReserved = false;
                        if (changes > 0)
                        {
                            RoomReserved = ReserveRoom(collection);
                            RoomReserved = UpdatePayment(collection);
                            RoomReserved = EmailRSManager(collection, "NewRecord");
                        }
                        return RoomReserved;
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

        private bool UpdateCalc(ref ReservedRoomViewModel collection)
        {
            try
            {
                var RoomData = context.HotelRoomData.Find(collection.RoomID);
                collection.CurrentRoomCharge = RoomData.RoomPricePerNight;

                var Days = (collection.ToDate - collection.FromDate).TotalDays;
                var DateCount = int.Parse(Math.Round(Days).ToString());
                collection.TotalPayableAmount = DateCount * collection.CurrentRoomCharge;
                return true;
            }
            catch (Exception er)
            {
                return false;
            }
        }

        //public override bool Add(ReservedRoomViewModel collection)
        //{
        //    try
        //    {
        //        if (collection != null)
        //        {
        //            if (UpdatePayment(collection)&&EmailRSManager(collection))
        //            {
        //                var MapData = GetMapped(collection);
        //                context.Add(MapData);
        //                int changes = context.SaveChanges();
        //                bool RoomReserved = false;
        //                if (changes > 0)
        //                    RoomReserved = ReserveRoom(collection.RoomID);
        //                return true;
        //            }
        //            return false;
        //        }
        //        else
        //            return false;
        //    }
        //    catch (Exception err)
        //    {
        //        err.ToString();
        //        return false;
        //    }
        //}

        private bool ReserveRoom(ReservedRoomViewModel collection)
        {
            try
            {
                if (collection!=null)
                {
                    bool updated = HotelRoomService.UpdateStatus(collection);
                    if (updated)
                        return true;
                    else
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

        internal bool EmailRSManager(ReservedRoomViewModel collection,string reason)
        {
            try
            {
                if (reason=="RoomReturned")
                {
                    var HotelRoom = HotelRoomService.Find(collection.RoomID);
                    var Hotel = HotelService.Find(HotelRoom.HotelID);
                    var RSManager = EmployeeService.Find(Hotel.RoomServiceManager);
                    var Reservation = Reservations.Find(collection.ReservationID);
                    var Customer = Customers.Find(Reservation.Customer);

                    string subject;
                    string body;

                    string to = RSManager.Email;

                    subject = "The Room Number :" + collection.RoomID + " at Hotel :" + Hotel.HotelSpecialName + " Reservation Closed and Returned On :" + DateTime.Now;
                    body = "The Room Number :" + collection.RoomID + " From Hotel :" + Hotel.HotelSpecialName + " as Earlier Reserved On :" + collection.FromDate + " for the Customer :" + Customer.CusfName + " | " + Customer.ID + " (" + Customer.CusNIC + ") is Returned for New Reservation";
                    MailMessage mm = new MailMessage();
                    mm.To.Add(to);
                    mm.Subject = subject;
                    mm.Body = body;
                    mm.From = new MailAddress("lakderanalk@gmail.com");
                    mm.IsBodyHtml = false;
                    SmtpClient smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        UseDefaultCredentials = true,
                        EnableSsl = true,

                        Credentials = new System.Net.NetworkCredential("lakderanalk@gmail.com", "lakderanalk2022")
                    };
                    smtp.Send(mm);
                    return true;
                }
                else
                {
                    var HotelRoom = HotelRoomService.Find(collection.RoomID);
                    var Hotel = HotelService.Find(HotelRoom.HotelID);
                    var RSManager = EmployeeService.Find(Hotel.RoomServiceManager);
                    var Reservation = Reservations.Find(collection.ReservationID);
                    var Customer = Customers.Find(Reservation.Customer);

                    string subject;
                    string body;

                    string to = RSManager.Email;
                    if (collection.Status == "Reservation Approved")
                    {
                        subject = "The Room Number :" + collection.RoomID + " at Hotel :" + Hotel.HotelSpecialName + " is Reserved On :" + collection.FromDate;
                        body = "The Room Number :" + collection.RoomID + " From Hotel :" + Hotel.HotelSpecialName + " is Reserved On :" + collection.FromDate + " for the Customer :" + Customer.CusfName + " | " + Customer.ID + " (" + Customer.CusNIC + ")";
                    }
                    else
                    {
                        subject = "The Room Number :" + collection.RoomID + " at Hotel :" + Hotel.HotelSpecialName + " Reservation Cancelled On :" + DateTime.Now;
                        body = "The Room Number :" + collection.RoomID + " From Hotel :" + Hotel.HotelSpecialName + " as Earlier Reserved On :" + collection.FromDate + " for the Customer :" + Customer.CusfName + " | " + Customer.ID + " (" + Customer.CusNIC + ") is Cancelled For Some Reason";
                    }
                    MailMessage mm = new MailMessage();
                    mm.To.Add(to);
                    mm.Subject = subject;
                    mm.Body = body;
                    mm.From = new MailAddress("lakderanalk@gmail.com");
                    mm.IsBodyHtml = false;
                    SmtpClient smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        UseDefaultCredentials = true,
                        EnableSsl = true,

                        Credentials = new System.Net.NetworkCredential("lakderanalk@gmail.com", "lakderanalk2022")
                    };
                    smtp.Send(mm);
                    return true;
                }
            }
            catch (Exception er)
            {
                return false;
            }
        }

        public bool UpdatePayment(ReservedRoomViewModel collection)
        {
            try
            {
                if (collection != null)
                {
                    //var Days = (collection.ToDate - collection.FromDate).TotalDays;
                    //var DateCount = int.Parse(Math.Round(Days).ToString());
                    //var MapData = context.PaymentData.Where(i=>i.ReservationID==collection.ReservationID).FirstOrDefault();
                    //MapData.NetAmount += (collection.CurrentRoomCharge * DateCount);
                    //bool PayStatus = PaymentService.UpdateAmounts(ref MapData);
                    //if (PayStatus)
                    //{
                    //    context.Update(MapData);
                    //    context.SaveChanges();
                    //    return true;
                    //}
                    //else return false;

                    var PayData = context.PaymentData.Where(i => i.ReservationID == collection.ReservationID).FirstOrDefault();
                    var ReservationData = context.ReservationData.Where(i => i.ID == collection.ReservationID).FirstOrDefault();
                    var ReservedRoomsData = context.ReservedRoomData.Where(i => i.ReservationID == collection.ReservationID&&i.Status== "Reservation Approved").ToList();

                    var TotalRoomsCharge = ReservedRoomsData.Sum(i=>i.TotalPayableAmount);
                    PayData.NetAmount = TotalRoomsCharge;
                    bool PayStatus = PaymentService.UpdateAmounts(ref PayData);
                    if (PayStatus)
                    {
                        context.Update(PayData);
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
                    var FoundRecord = context.ReservedRoomData.Find(int.Parse(id));
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

        public override ReservedRoomViewModel Find(string id)
        {
            var FoundRecord = context.ReservedRoomData.Where(i=>i.ID==int.Parse(id)).FirstOrDefault();            

            var VM = GetMapped(FoundRecord);

            VM.DateRage = VM.FromDate.ToString("MM/dd/yyyy") +" - "+ VM.ToDate.ToString("MM/dd/yyyy");

            return VM;
        }

        public override List<ReservedRoomViewModel> GetDDList()
        {
            var DataList = context.ReservedRoomData.ToList();
            var GetList = new List<ReservedRoomViewModel>();
            foreach (var item in DataList)
            {
                var VM = GetMapped(item);
                VM.SpecialDescription = VM.ReservationID + " " + VM.RoomID + ". (" + VM.Status + ")";
                GetList.Add(VM);
            }
            return GetList;
        }

        internal List<ReservedRoomViewModel> GetListByReservationID(string iD)
        {
            var DataList = context.ReservedRoomData.Where(i=>i.ReservationID==iD).ToList();
            var GetList = new List<ReservedRoomViewModel>();
            foreach (var item in DataList)
            {
                var VM = GetMapped(item);
                GetList.Add(VM);
            }
            return GetList;
        }

        public override List<ReservedRoomViewModel> GetList()
        {
            var DataList = context.ReservedRoomData.ToList();
            var GetList = new List<ReservedRoomViewModel>();
            foreach (var item in DataList)
            {
                var VM = GetMapped(item);
                GetList.Add(VM);
            }
            return GetList;
        }

        public override string NewID()
        {
            throw new NotImplementedException();
        }

        public async override Task<bool> Update(ReservedRoomViewModel collection)
        {
            try
            {
                if (collection != null)
                {
                    collection.FromDate = Convert.ToDateTime(collection.DateRage.Substring(0, 10));
                    collection.FromDate = collection.FromDate.AddHours(14);

                    collection.ToDate = Convert.ToDateTime(collection.DateRage.Substring(13, 10));
                    collection.ToDate = collection.ToDate.AddHours(14);

                    if (UpdateCalc(ref collection))
                    {
                        var MapData = GetMapped(collection);
                        context.Update(MapData);
                        int changes = await context.SaveChangesAsync();
                        bool FinalUpdates = false;
                        if (changes > 0)
                        {
                            FinalUpdates = ReserveRoom(collection);
                            FinalUpdates = UpdatePayment(collection);
                            FinalUpdates = EmailRSManager(collection,"Update");
                        }
                        return FinalUpdates;
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
    }
}
