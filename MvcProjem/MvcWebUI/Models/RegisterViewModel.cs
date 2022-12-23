using Microsoft.Build.Framework;

namespace MvcWebUI.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        
        
        [Required]
        public string Email { get; set; }
    }
}
