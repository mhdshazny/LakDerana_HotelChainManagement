using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LakDerana_HotelChainManagement.ViewModels
{
    public class CheckoutViewModel
    {
        public ReservationViewModel Reservation { get; set; }
        public List<ReservedRoomViewModel> ReservedRooms { get; set; }
        public PaymentViewModel PaymentData { get; set; }
        public HotelViewModel HotelData { get; set; }
        public CustomerViewModel Customer { get; set; }

    }
}
