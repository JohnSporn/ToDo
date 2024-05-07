using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Todo.Models;
using ToDoApp.Data.Repositories;

namespace ToDoApp.Components.Pages.Account.Manage
{
    public class CreateModel : PageModel
    {
        private readonly IUserRepository userRepository;

        public CreateModel(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }

        [BindProperty]
        public User User { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            PasswordHasher<User> passwordHasher = new();
            User.Password = passwordHasher.HashPassword(User, User.Password);
            var user = new User();
            if(await TryUpdateModelAsync<User>(
                user,
                "user",
                u => u.Name, u => u.Email, u => u.Username, u => u.Password))
            {
                var success = await userRepository.CreateUser(user);
                if (success > 0)
                {
                    return RedirectToPage("./Index");
                }
            }
            return Page();
        }
    }
}
