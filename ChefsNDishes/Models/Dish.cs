using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ChefsNDishes.Models
{
    public class Dish
    {
        [Key]
        public int DishId { get; set; }

        [Required]
        [MinLength(2)]
        [Display(Name = "Name of Dish")]
        public string Name { get; set; }

        [Required]
        [Range(1,10000)]
        [Display(Name = "# of Calories")]
        public int Calories { get; set; }

        [Required]
        [MinLength(2)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Range(1,5)]
        public int Tastiness { get; set; }

        public int ChefId { get; set; }

        public Chef Creator {get;set;}

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}