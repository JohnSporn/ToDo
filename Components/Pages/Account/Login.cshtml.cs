using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Transactions;
using ToDoApp.Data.Repositories;

namespace ToDoApp.Components.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly ILogger<LoginModel> logger;
        private readonly IAuthenticateUser authenticateUser;
        public LoginModel(ILogger<LoginModel> logger, IAuthenticateUser authenticateUser)
        {
            this.logger = logger;
            this.authenticateUser = authenticateUser;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }

        public InputModel Input { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if(ModelState.IsValid)
            {
                var user = await authenticateUser.AuthenticateUser(Input.Username, Input.Password);

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true, // Allows refreshing the auth session.
                    IssuedUtc = DateTimeOffset.UtcNow, // Date issued.
                };

                //Assigns encrypted cookie and adds it to the current response.
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                logger.LogInformation("User {Username} logged in at {Time}.", user.Username, DateTime.UtcNow);

                return LocalRedirect("/");
            }
            // Something failed. Redisplay the form.
            return Page();
        }

        public class InputModel
        {
            [Required]
            public string Username { get; set; }
            [Required]
            public string Password { get; set; }
        }
    }
}
