using System.ComponentModel.DataAnnotations;

namespace BarMenuBoardAPI.Models
{
    public class GarnishCategory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Category { get; set; }
    }
}
