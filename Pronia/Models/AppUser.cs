using Microsoft.AspNetCore.Identity;
using Pronia.Models;

namespace Pronia.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<BasketItem> BasketItems { get; set; }

        public List<Order> Orders { get; set; }
    }
}

