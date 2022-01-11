using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LakDerana_HotelChainManagement.Models
{
    [Table("Tbl_BarIncomeExpense")]
    public class BarModel
    {
        [Key]
        public int ID { get; set; }
        public string HotelID { get; set; }
        public string EmpUpdatedBy { get; set; }
        public DateTime IncomeExpenseUpdatedDate { get; set; }
        [Column(TypeName = "money")]
        public decimal Expense { get; set; }
        [Column(TypeName = "money")]
        public decimal Sales { get; set; }
        public string Status { get; set; }
    }
}
