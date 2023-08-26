using System.ComponentModel.DataAnnotations;

namespace WebAppExpenses.Models
{
    public class UpdateCategoryModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Amount { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
