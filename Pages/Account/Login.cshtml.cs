using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupermarketWEB.Models;
using SupermarketWEB._Repositories;
using System.Security.Claims;

namespace SupermarketWEB.Pages.Account
{
    public class LoginModel : PageModel
    {

        [BindProperty]
        public new User User { get; set; }

        public void OnGet()
        {
        }

        UserRepository repository = new UserRepository();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            IEnumerable<User> userList = repository.GetByValue(User.Email);

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
            //Console.WriteLine("User: " + User.Email + "\nPassword: " + User.Password);
        }
    }
}