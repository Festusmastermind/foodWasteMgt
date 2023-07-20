using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace foodWasteMgt.Models
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        [Required]
        [Display(Name="Full Name")]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string  Phone { get; set; }
        [Required]
        public string  City { get; set; }
        [Required]
        public string  State { get; set; }
        [Required]
        public UserType?  UserType { get; set; }
        [Required]
        public string Occupation { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public ICollection<FoodItem> FoodItems { get; set; }



    }
}
