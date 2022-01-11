using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LakDerana_HotelChainManagement.ViewModels
{
    public class ReservedRoomViewModel
    {
        public int ID { get; set; }
        [DisplayName("Select Room")]
        public int RoomID { get; set; }
        [Required]
        [DisplayName("Select the Reservation")]
        public string ReservationID { get; set; }
        [DisplayName("Check In Date")]
        public DateTime FromDate { get; set; }
        [DisplayName("Check Out Date")]
        public DateTime ToDate { get; set; }
        [DisplayName("Special Description")]
        public string SpecialDescription { get; set; }
        [DisplayName("Current Room Charge")]

        [Column(TypeName = "money")]
        public decimal CurrentRoomCharge { get; set; }
        [DisplayName("Total Room Charge")]

        [Column(TypeName = "money")]
        public decimal TotalPayableAmount { get; set; }
        [DisplayName("Room Reservation Status")]
        public string Status { get; set; }
        [NotMapped]
        public string DateRage { get; set; }
    }
}
