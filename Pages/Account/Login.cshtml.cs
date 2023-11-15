using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupermarketWEB.Models;
using SupermarketWEB._Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SupermarketWEB.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public User User { get; set; }

        private readonly UserRepository _repository;

        public LoginModel(UserRepository repository)
        {
            _repository = repository;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            IEnumerable<User> userList = _repository.GetByValue(User.Email);

            if (userList.Any())
            {
                User userFromRepository = userList.First();

                if (userFromRepository.Password == User.Password)
                {
                    var emailParts = User.Email.Split('@');
                    string userName = emailParts.Length > 0 ? emailParts[0] : User.Email;

                    var claims = new List<Claim>
                    {
                         new Claim(ClaimTypes.Name, userName),
                         new Claim(ClaimTypes.Email, User.Email),
                    };

                    var identity = new ClaimsIdentity(claims, "CookieAuth");

                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync("CookieAuth", claimsPrincipal);

                    return RedirectToPage("/Index");
                }
            }
            return Page();
        }
    }
}
