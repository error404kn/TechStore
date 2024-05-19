using System.ComponentModel.DataAnnotations;

namespace TechStore.Models
{
    public class Technics
    {
        public int Id { get; set; }

        [Required]
        public String ModelName { get; set; }

        [Required]
        public string Data { get; set; }

        [MaxLength(80)]
        public string Description { get; set; }

        [Required, DataType(DataType.Currency)]
        public int Price { get; set; }

        [Required]
        public string Manufacturer { get; set; }

        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }
    }
}
