using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LakDerana_HotelChainManagement.ViewModels
{
    public class HotelViewModel
    {
        [DisplayName("Hotel ID")]
        [Required(ErrorMessage = "Please provide a valid Hotel ID")]
        public string ID { get; set; }
        [DisplayName("Hotel Name")]
        [Required]
        public string HotelSpecialName { get; set; }
        [DisplayName("Location/Address")]
        [Required]
        public string HotelLocation { get; set; }
        [DisplayName("Manager")]
        [Required]
        public string HotelManager { get; set; }
        [DisplayName("Room Service Manager")]
        [Required]
        public string RoomServiceManager { get; set; }
        [DisplayName("Hotel Status")]
        [Required]
        public string Status { get; set; }
    }
}
