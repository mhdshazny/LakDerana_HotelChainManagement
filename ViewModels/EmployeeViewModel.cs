using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LakDerana_HotelChainManagement.ViewModels
{
    public class EmployeeViewModel
    {
        public EmployeeViewModel()
        {
            DoB = dt();
        }
        [DisplayName("Emp ID")]
        [Required(ErrorMessage = "Please provide a valid Employee ID.")]
        public string ID { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "Please provide your First Name.")]
        public string fName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Please provide your Last Name.")]
        public string lName { get; set; }

        [DisplayName("Gender")]
        [Required(ErrorMessage = "Please provide your Gender.")]
        public string Gender { get; set; }

        [DisplayName("Date of Birth")]
        [DataType(DataType.DateTime, ErrorMessage = "Format Error")]
        [Required(ErrorMessage = "Please provide a valid Date of Birth.")]
        public DateTime DoB { get; set; }
        DateTime dt()
        {
            DateTime dt = DateTime.Now;
            return dt;
        }

        [DisplayName("NIC No.")]
        [Required(ErrorMessage = "Please provide a valid Email address.")]
        public string NIC { get; set; }

        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please provide a valid Email address.")]
        public string Email { get; set; }

        [DisplayName("Permenent Address")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please provide your Permenent Address.")]
        public string Address { get; set; }

        [DisplayName("Phone")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Please provide a valid Phone Number.")]
        public string Contact { get; set; }

        [DisplayName("Status")]
        [Required(ErrorMessage = "Please provide a valid Employee Status.")]
        public string Status { get; set; }



        [NotMapped]
        [Required(ErrorMessage = "Please provide a valid Hotel.")]
        public EmpIdentityViewModel EmployeeIdentityModel { get; set; }


    }
}