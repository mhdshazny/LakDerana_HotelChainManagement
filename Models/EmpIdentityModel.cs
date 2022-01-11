using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LakDerana_HotelChainManagement.Models
{
    [Table("tbl_EmpLogin")]
    public class EmpIdentityModel
    {
        //Temporary Identity Model
        [Key]
        public int Id { get; set; }
        public string EmployeeID { get; set; }
        //public EmployeeModel EmpID { get; set; }
        public string HotelID { get; set; }
        public string EmpEmail { get; set; }
        public string EmpPassWord { get; set; }
        public string EmpRole { get; set; }
        public string EmpStatus { get; set; }
    }
}
