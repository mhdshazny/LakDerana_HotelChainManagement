using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LakDerana_HotelChainManagement.ViewModels
{
    public class ReservationViewModel
    {
        public string ID { get; set; }
        [Required]
        public string Hotel { get; set; }
        [Required]
        public string Customer { get; set; }
        public DateTime CheckInDate { get; set; }        
        [DisplayFormat(DataFormatString = "{mm/dd/yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime CheckoutDate { get; set; }
        [Required]
        public string PaymentStatus { get; set; }
        [Required]
        public string Status { get; set; }
        [NotMapped]
        public string DateRage { get; set; }
    }
}
