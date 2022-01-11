using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LakDerana_HotelChainManagement.Models
{
    [Table("Tbl_Payments")]
    public class PaymentModel
    {
        [Key]
        public int ID { get; set; }
        public string ReservationID { get; set; }
        [Column(TypeName = "money")]
        public decimal ReservationFee { get; set; }
        [Column(TypeName = "money")]
        public decimal HotelServiceCharge { get; set; }
        [Column(TypeName = "money")]
        public decimal NetAmount { get; set; }
        [Column(TypeName = "money")]
        public decimal DiscountAmount { get; set; }
        [Column(TypeName = "money")]
        public decimal TotalPayableAmount { get; set; }
        [Column(TypeName = "money")]
        public decimal AdvancePaymentAmount { get; set; }
        public DateTime FinalPaymentDate { get; set; }
        public string PaymentStatus { get; set; }

    }
}
