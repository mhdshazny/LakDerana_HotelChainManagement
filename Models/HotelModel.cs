using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LakDerana_HotelChainManagement.Models
{
    [Table("Tbl_Hotel")]
    public class HotelModel
    {
        [Key]
        public string ID { get; set; }
        public string HotelSpecialName { get; set; }
        public string HotelLocation { get; set; }
        public string HotelManager { get; set; }
        public string RoomServiceManager { get; set; }
        public string Status { get; set; }
    }
}
