using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LakDerana_HotelChainManagement.ViewModels
{
    public class BarViewModel
    {
        public int ID { get; set; }
        public string HotelID { get; set; }
        public string EmpUpdatedBy { get; set; }
        public DateTime IncomeExpenseUpdatedDate { get; set; }
        [Required(ErrorMessage ="Please Provide a Valid Value")]
        [Column(TypeName = "money")]
        public decimal Expense { get; set; }
        [Required(ErrorMessage = "Please Provide a Valid Value")]
        [Column(TypeName = "money")]
        public decimal Sales { get; set; }
        public string Status { get; set; }
    }
}
