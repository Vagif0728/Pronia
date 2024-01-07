using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace Pronia.Areas.AdminPanel.ViewModels
{
    public class CreateCategoryVM
    {
        [MaxLength(15, ErrorMessage = "Max length is 15")]
        public string Name { get; set; }
    }
}
