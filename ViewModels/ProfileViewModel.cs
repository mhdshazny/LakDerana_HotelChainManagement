using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LakDerana_HotelChainManagement.ViewModels
{
    public class ProfileViewModel
    {
        public EmployeeViewModel EmployeeVM { get; set; }
        public EmpIdentityViewModel EmpIdentityVM { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Confirmation Password is required.")]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        public string ConfirmPassword { get; set; }
    }
}
