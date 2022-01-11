using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LakDerana_HotelChainManagement.Models
{
    [Table("Tbl_Reservation")]
    public class ReservationModel
    {
        [Key]
        public string ID { get; set; }
        public string Hotel { get; set; }
        public string Customer { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckoutDate { get; set; }
        public string PaymentStatus { get; set; }
        public string Status { get; set; }
    }
}
