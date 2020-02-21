using BarMenuBoardAPI.Enums.Inventory;
using System;
using System.ComponentModel.DataAnnotations;

namespace BarMenuBoardAPI.Models
{
    public class Inventory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public IngredientType IngredientType { get; set; }

        public int LiquorType { get; set; }

        public int MixerType { get; set; }

        public int GarnishType { get; set; }

        public int Amount { get; set; }

        public DateTime Updated { get; set; }
    }
}
