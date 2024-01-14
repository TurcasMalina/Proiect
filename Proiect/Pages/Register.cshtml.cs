using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proiect.Data;
using Proiect.Models;
using System.ComponentModel.DataAnnotations;

namespace Proiect.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly CarContext _context;

        [BindProperty]
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public RegisterModel(CarContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Here, you would normally hash the password before saving it
            var user = new User { UserName = Username, Password = Password }; // Use password hashing in production
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // After registering, you could sign in the user directly or redirect to a login page
            // For now, we redirect to the login page
            return RedirectToPage("/Login");
        }
    }
}
