using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LakDerana_HotelChainManagement.ViewModels
{
    public class PaymentViewModel
    {
        public int ID { get; set; }
        [Required]
        public string ReservationID { get; set; }
        [Required]

        [Column(TypeName = "money")]
        public decimal ReservationFee { get; set; }
        [Required]
        [Column(TypeName = "money")]
        public decimal HotelServiceCharge { get; set; }
        [Required]

        [Column(TypeName = "money")]
        public decimal NetAmount { get; set; }
        [Required]

        [Column(TypeName = "money")]
        public decimal DiscountAmount { get; set; }
        [Required]

        [Column(TypeName = "money")]
        public decimal TotalPayableAmount { get; set; }
        [Required]

        [Column(TypeName = "money")]
        public decimal AdvancePaymentAmount { get; set; }
        [Required]

        public DateTime FinalPaymentDate { get; set; }
        [Required]

        public string PaymentStatus { get; set; }

    }
}
