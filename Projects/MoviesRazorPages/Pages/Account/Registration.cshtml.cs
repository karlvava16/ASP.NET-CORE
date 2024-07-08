using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MoviesRazorPages.Models;
using MoviesRazorPages.Models;
using MoviesRazorPages.Repositories;
using System.Security.Cryptography;
using System.Text;
using MoviesRazorPages.Repositories;

namespace MoviesRazorPages.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly IRepository<User> _userRepository;

        public RegisterModel(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        [BindProperty]
        public MoviesRazorPages.Models.RegistrModel Register { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            User user = new User
            {
                Name = Register.Name,
                Login = Register.Login,
            };

            byte[] saltBytes = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            string salt = Convert.ToBase64String(saltBytes);

            byte[] passwordBytes = Encoding.Unicode.GetBytes(salt + Register.Password);
            byte[] hashBytes = SHA256.HashData(passwordBytes);
            string hash = Convert.ToBase64String(hashBytes);

            user.Password = hash;
            user.Salt = salt;

            await _userRepository.AddAsync(user);

            HttpContext.Session.SetString("IsLoggedIn", "true");
            HttpContext.Session.SetString("Name", user.Name);

            return RedirectToPage("/Index");
        }
    }
}
