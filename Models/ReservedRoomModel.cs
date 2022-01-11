using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LakDerana_HotelChainManagement.Models
{
    [Table("Tbl_ReservedRoom")]
    public class ReservedRoomModel
    {
        [Key]
        public int ID { get; set; }
        public int RoomID { get; set; }
        public string ReservationID { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string SpecialDescription { get; set; }
        [Column(TypeName = "money")]
        public decimal CurrentRoomCharge { get; set; }
        [Column(TypeName = "money")]
        public decimal TotalPayableAmount { get; set; }
        public string Status { get; set; }
    }
}
