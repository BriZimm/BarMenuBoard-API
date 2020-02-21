using System.ComponentModel.DataAnnotations;

namespace BarMenuBoardAPI.Models
{
    public class AmountValue
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        public int ListOrder { get; set; }
    }
}
