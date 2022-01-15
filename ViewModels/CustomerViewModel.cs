using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LakDerana_HotelChainManagement.ViewModels
{
    public class CustomerViewModel
    {
        [DisplayName("Customer ID")]
        public string ID { get; set; }
        [Required(ErrorMessage = "Please provide a valid Customer First Name.")]
        [DisplayName("First Name")]
        public string CusfName { get; set; }
        [Required(ErrorMessage = "Please provide a valid Customer Last Name.")]
        [DisplayName("Last Name")]
        public string CuslName { get; set; }
        [Required(ErrorMessage = "Please provide a valid Customer NIC.")]
        [DisplayName("Customer NIC")]
        public string CusNIC { get; set; }
        [Required(ErrorMessage = "Please provide a valid Customer Gender.")]
        [DisplayName("Gender")]
        public string CusGender { get; set; }
        [Required(ErrorMessage = "Please provide a valid Customer Address.")]
        [DisplayName("Permenent Address")]
        [DataType(DataType.MultilineText)]
        public string CusAddress { get; set; }
        [Required(ErrorMessage = "Please provide a valid Customer Mobile.")]
        [DisplayName("Contact No.")]
        public string CusContact { get; set; }
        [Required(ErrorMessage = "Please provide a valid Customer Email.")]
        [DisplayName("Email ID")]
        [DataType(DataType.EmailAddress)]
        public string CusEmail { get; set; }
        //[Required(ErrorMessage = "Please provide a valid Customer Password.")]
        //[DisplayName("Password")]
        //[DataType(DataType.Password)]
        //public string CusPassw { get; set; }
        [Required(ErrorMessage = "Please provide a valid Customer Status.")]
        [DisplayName("Status")]
        public string CusStatus { get; set; }

        [NotMapped]
        [DisplayName("Reservations Count")]
        public int ReservationCount { get; set; }
    }
}
