using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LakDerana_HotelChainManagement.Models
{
    [Table("Tbl_Employee")]
    public class EmployeeModel
    {
        [Key]
        public string ID { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public string Gender { get; set; }
        public DateTime DoB { get; set; }
        public string NIC { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Contact { get; set; }
        public string Status { get; set; }
    }
}
