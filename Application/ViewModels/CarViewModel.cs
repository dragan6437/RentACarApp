using Domain.Validators;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class CarViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Make { get; set; }

        [Required]
        [MaxLength(20)]
        public string Model { get; set; }

        [Required]
        [YearRangeValidator(ErrorMessage = "The car can't be older than 10 years!")]
        public int Year { get; set; }

        [Required]
        public string Color { get; set; }

        [Display(Name = "Is Available")]
        public bool IsAvailable { get; set; }

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "The field {0} must be bigger than 0.")]
        [Display(Name = "Price Per Day")]
        public decimal PricePerDay { get; set; }

        public string ImagePath { get; set; }

        public IFormFile Image { get; set; }
    }
}
