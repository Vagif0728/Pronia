using System.ComponentModel.DataAnnotations;

namespace Pronia.Areas.AdminPanel.ViewModels
{
    public class CreateProductVM
    {


        public string Name { get; set; }
        public decimal Price { get; set; }

        public string Description { get; set; }

        public string SKU { get; set; }

        public IFormFile MainPhoto { get; set; }
        public IFormFile HoverPhoto { get; set; }
        public List<IFormFile>? Photos { get; set; }
        [Required]
        public int? CategoryId { get; set; }

        public List<int> TagIds { get; set; }

        public List<int> SizeId { get; set; }
     

        public List<int> ColorId { get; set; }
        
    }
}