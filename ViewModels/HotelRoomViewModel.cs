using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LakDerana_HotelChainManagement.ViewModels
{
    public class HotelRoomViewModel
    {
        [DisplayName("Room ID")]
        [Required(ErrorMessage = "Please provide a valid Room ID")]
        public int ID { get; set; }
        [DisplayName("Relevant Hotel ID")]
        [Required(ErrorMessage = "Please provide the respective Hotel ID")]
        public string HotelID { get; set; }
        [DisplayName("Room Type")]
        [Required]
        public string RoomType { get; set; }
        [DisplayName("Room Charge /Night")]
        [Required]
        [Column(TypeName = "money")]
        public decimal RoomPricePerNight { get; set; }
        [DisplayName("Room Description")]
        [Required]
        public string RoomDescription { get; set; }
        [DisplayName("Room Status")]
        [Required]
        public string RoomStatus { get; set; }
    }
}
