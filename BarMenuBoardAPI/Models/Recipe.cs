using System.ComponentModel.DataAnnotations;
using BarMenuBoardAPI.Enums;

namespace BarMenuBoardAPI.Models
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Instructions { get; set; }
        [Required]
        public string Ingredients { get; set; }
        [Required]
        public CocktailType Type { get; set; }
        [Required]
        public GlassType Glass { get; set; }
        [Required]
        public ServedType Served { get; set; }
        [Required]
        public string Garnish { get; set; }
        [Required]
        public string SimilarTastes { get; set; }
        [Required]
        public StrengthType Strength { get; set; }
        [Required]
        public string Image { get; set; }

        public bool CurrentSpecial { get; set; }
    }
}
