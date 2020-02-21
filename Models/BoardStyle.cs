using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BarMenuBoardAPI.Models
{
    public class BoardStyle
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime Updated { get; set; }
        [Required]
        public bool Active { get; set; }
        [Required]
        public string TitleText { get; set; }
        [Required]
        public string TitleTextColor { get; set; }
        [Required]
        public bool ShowBanner { get; set; }
        public string BannerText { get; set; }
        public string BannerTextColor { get; set; }
        public string BannerColor { get; set; }
        public string HeaderImage { get; set; }

        public string MainBackgroundColor { get; set; }
        public string MainBackgroundImage { get; set; }
        public bool UseMainBackgroundColor { get; set; }
        public string RecipeBackgroundImage { get; set; }
        public string RecipeBackgroundColor { get; set; }
        public bool UseRecipeBackgroundColor { get; set; }
        public string RecipeProfileBackgroundImage { get; set; }
        public string RecipeProfileBackgroundColor { get; set; }
        public bool UseRecipeProfileBackgroundColor { get; set; }

        [Required]
        public string RecipeBorderColor { get; set; }
        [Required]
        public string RecipeHeaderColor { get; set; }
        [Required]
        public string RecipeTextColor { get; set; }
        [Required]
        public string RecipeImageBorderColor { get; set; }
        [Required]
        public string RecipeDescriptionHeaderTextColor { get; set; }
        [Required]
        public string RecipeProfileLabelColor { get; set; }
        [Required]
        public string RecipeProfileItemColor { get; set; }
    }
}
