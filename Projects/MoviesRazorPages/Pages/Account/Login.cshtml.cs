using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MoviesRazorPages.Models;
using MoviesRazorPages.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace MoviesRazorPages.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IRepository<User> _userRepository;

        public LoginModel(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        [BindProperty]
        public MoviesRazorPages.Models.LoginModel Login { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var users = await _userRepository.GetAllAsync();
            var user = users.FirstOrDefault(u => u.Login == Login.Login);

            if (user == null || !VerifyPassword(Login.Password, user.Password, user.Salt))
            {
                ModelState.AddModelError("", "Wrong login or password!");
                return Page();
            }

            HttpContext.Session.SetString("IsLoggedIn", "true");
            HttpContext.Session.SetString("Name", user.Name);
            return RedirectToPage("/Index");
        }

        private bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            byte[] passwordBytes = Encoding.Unicode.GetBytes(storedSalt + enteredPassword);
            byte[] hashBytes = SHA256.HashData(passwordBytes);

            string hash = Convert.ToBase64String(hashBytes);
            return storedHash == hash;
        }
    }
}
