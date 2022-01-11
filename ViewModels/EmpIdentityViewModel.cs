using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LakDerana_HotelChainManagement.ViewModels
{
    public class EmpIdentityViewModel
    {
        public int Id { get; set; }
        public string EmployeeID { get; set; }
        [NotMapped]
        [DisplayName("Employee ID")]
        public EmployeeViewModel EmpID { get; set; }
        [DisplayName("Hotel ID")]
        [Required(ErrorMessage = "Please provide a valid Email address.")]
        public string HotelID { get; set; }

        [DisplayName("Employee Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please provide a valid Email address.")]
        public string EmpEmail { get; set; }

        [DisplayName("Employee Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter a valid Password.")]
        public string EmpPassWord { get; set; }

        [NotMapped]
        [DisplayName("Employee Role")]
        public string EmpRole { get; set; }

        //[NotMapped]
        //[DisplayName("Employee Name")]
        //public string EmpName { get; set; }

        [DisplayName("Employee Status")]
        public string EmpStatus { get; set; }

        //[NotMapped]
        //[Required(ErrorMessage = "Password is required.")]
        //public string Password { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Confirmation Password is required.")]
        [Compare("EmpPassWord", ErrorMessage = "Password and Confirmation Password must match.")]
        public string ConfirmPassword { get; set; }

    }
}
