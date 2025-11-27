using EHS_Benjamin_Pasic.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EHS_Benjamin_Pasic.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public RegisterModel(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string ConfirmPassword { get; set; }

        public string ErrorMessage { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var existingUserByEmail = await _userManager.FindByEmailAsync(Email);
            if (existingUserByEmail != null)
            {
                ErrorMessage = "Email is already in use.";
                return Page();
            }

            var existingUserByUsername = await _userManager.FindByNameAsync(Username);
            if (existingUserByUsername != null)
            {
                ErrorMessage = "Username is already taken.";
                return Page();
            }


            if (Password != ConfirmPassword)
            {
                ErrorMessage = "Passwords do not match.";
                return Page();
            }

            var user = new AppUser
            {
                UserName = Username,
                Email = Email
            };

            var result = await _userManager.CreateAsync(user, Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToPage("/Index");
            }

            ErrorMessage = string.Join(" | ", result.Errors.Select(e => e.Description));
            return Page();
        }
    }
}
