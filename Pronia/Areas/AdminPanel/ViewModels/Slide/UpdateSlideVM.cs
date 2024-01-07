using System.ComponentModel.DataAnnotations;

namespace Pronia.Areas.AdminPanel.ViewModels
{
    public class UpdateSlideVM
    {

        [Required]
        public string Title { get; set; }
        public string SubTitle { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public int Order { get; set; }

        public IFormFile? Photo { get; set; }
    }
}
