using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
namespace ChefsNDishes.Models
{
    public class Chef
    {
        [Key]
        public int ChefId { get; set; }

        [Required]
        [MinLength(2)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [BirthdayValidation]
        public DateTime DateOfBirth { get; set; }

        public List<Dish> CreatedDishes {get;set;}

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }

    public class BirthdayValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if ((DateTime)value > DateTime.Now)
            {
                return new ValidationResult("Chef was not born in the future!");
            }
            DateTime CheckDate = DateTime.Now.AddYears(-18);
            if ((DateTime)value > CheckDate )
            {
                return new ValidationResult("Chef must be at least 18 years old!");
            }
            return ValidationResult.Success;
        }
    }
}