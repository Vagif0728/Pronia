using System.ComponentModel.DataAnnotations;

namespace Pronia.Areas.AdminPanel.ViewModels
{
    public class UpdateTagVM
    {
        [MaxLength(10, ErrorMessage = "Max length is  10")]
        public string Name { get; set; }
    }
}
