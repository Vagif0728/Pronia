using System.ComponentModel.DataAnnotations;

namespace Pronia.Areas.AdminPanel.ViewModels
{
    public class UpdateCategoryVM
    {
        [Required(ErrorMessage ="Name is already")]
        [MaxLength(15,ErrorMessage ="length is 15")]
        public string Name { get; set; }
    }
}
