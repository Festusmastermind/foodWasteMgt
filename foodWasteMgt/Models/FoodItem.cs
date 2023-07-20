using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace foodWasteMgt.Models
{
    public class FoodItem
    {

        public int Id { get; set; }
        public FoodCategory? CategoryType { get; set; }
        public double Weight { get; set; }
        public int ApplicationUserId { get; set; }
        public string Imagepath { get; set; }
 
        [DataType(DataType.Date)]
        public DateTime PostedDate { get; set; } = DateTime.Now;

        public ApplicationUser Owner { get; set; }  //reference navigation property


    }
}
