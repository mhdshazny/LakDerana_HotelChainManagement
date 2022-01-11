using AutoMapper;
using LakDerana_HotelChainManagement.Data;
using LakDerana_HotelChainManagement.Models;
using LakDerana_HotelChainManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LakDerana_HotelChainManagement.Services
{
    public class CheckoutService
    {
        private ApplicationDbContext context;
        private IMapper mapper;
        private readonly HotelRoomService HotelService;
        private readonly ReservedRoomService ReservedRoomService;
        private readonly ReservationService Reservations;
        public CheckoutService(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
            ReservedRoomService = new ReservedRoomService(context, mapper);
            HotelService = new HotelRoomService(context, mapper);
            Reservations = new ReservationService(context, mapper);
        }


        internal bool Checkout(string id)
        {
            try
            {
                var Reservation = context.ReservationData.Find(id);
                Reservation.Status = "Reservation Closed";

                //var Customer = context.CusData.Find(Reservation.Customer);
                //CustomerViewModel CustomerVM = mapper.Map<CustomerViewModel>(Customer);

                //var Hotel = context.HotelData.Find(Reservation.Hotel);
                //HotelViewModel HotelVM = mapper.Map<HotelViewModel>(Hotel);

                //var Payment = context.PaymentData.Where(i => i.ReservationID == Reservation.ID).FirstOrDefault();
                //PaymentViewModel PaymentVM = mapper.Map<PaymentViewModel>(Payment);

                var ReservedRooms = context.ReservedRoomData.Where(i => i.ReservationID == Reservation.ID).ToList();

                bool reservationStatus = false;
                foreach (var item in ReservedRooms)
                {
                    ReservedRoomViewModel ReservedRoomVM = mapper.Map<ReservedRoomViewModel>(item);
                    ReservedRoomVM.Status = "Reservation Closed";
                    reservationStatus = HotelService.UpdateStatus(ReservedRoomVM);
                    reservationStatus = ReservedRoomService.EmailRSManager(ReservedRoomVM, "RoomReturned");
                }

                context.Update(Reservation);
                context.SaveChanges();
                //CheckoutViewModel Final = new CheckoutViewModel
                //{
                //    Reservation = ReservationVM,
                //    Customer = CustomerVM,
                //    HotelData = HotelVM,
                //    PaymentData = PaymentVM,
                //    ReservedRooms = ReservedRoomsVM
                //};


                return reservationStatus;
            }
            catch (Exception er)
            {
                return false;
            }


        }

        internal CheckoutViewModel GetDataForReport(string iD)
        {
            try
            {
                ReservationViewModel Reservation = Reservations.Find(iD);

                var Customer = context.CusData.Find(Reservation.Customer);
                CustomerViewModel CustomerVM = mapper.Map<CustomerViewModel>(Customer);

                var Hotel = context.HotelData.Find(Reservation.Hotel);
                HotelViewModel HotelVM = mapper.Map<HotelViewModel>(Hotel);

                var Payment = context.PaymentData.Where(i => i.ReservationID == Reservation.ID).FirstOrDefault();
                PaymentViewModel PaymentVM = mapper.Map<PaymentViewModel>(Payment);

                var ReservedRooms = context.ReservedRoomData.Where(i => i.ReservationID == Reservation.ID).ToList();
                List<ReservedRoomViewModel> ReservedRoomsVM = new List<ReservedRoomViewModel>();
                foreach (var item in ReservedRooms)
                {
                    ReservedRoomViewModel ReservedRoomVM = mapper.Map<ReservedRoomViewModel>(item);
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
