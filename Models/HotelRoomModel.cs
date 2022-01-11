using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LakDerana_HotelChainManagement.Models
{
    [Table("Tbl_HotelRoom")]
    public class HotelRoomModel
    {
        [Key]
        public int ID { get; set; }
        public string HotelID { get; set; }
        public string RoomType { get; set; }
        [Column(TypeName = "money")]
        public decimal RoomPricePerNight { get; set; }
        public string RoomDescription { get; set; }
        public string RoomStatus { get; set; }
    }
}
