using System.ComponentModel.DataAnnotations;

namespace BarMenuBoardAPI.Models
{
    public class MixerCategory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Category { get; set; }
    }
}
