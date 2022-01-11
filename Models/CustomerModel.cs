using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LakDerana_HotelChainManagement.Models
{
    [Table("Tbl_Customer")]
    public class CustomerModel
    {
        [Key]
        public string ID { get; set; }
        [Required]
        public string CusfName { get; set; }
        [Required]
        public string CuslName { get; set; }
        [Required]
        public string CusNIC { get; set; }
        [Required]
        public string CusGender { get; set; }
        [Required]
        public string CusAddress { get; set; }
        [Required]
        public string CusContact { get; set; }
        [Required]
        public string CusEmail { get; set; }
        [Required]
        public string CusStatus { get; set; }
    }
}
