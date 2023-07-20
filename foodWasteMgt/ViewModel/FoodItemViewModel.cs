using foodWasteMgt.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace foodWasteMgt.ViewModel
{
    public class FoodItemViewModel
    {
        [Required]
        public FoodCategory? CategoryType { get; set; }
        [Required]
        public double Weight { get; set; }
        [Required]
        public int ApplicationUserId { get; set; }
        public IFormFile Photo { get; set; }
    }
}
