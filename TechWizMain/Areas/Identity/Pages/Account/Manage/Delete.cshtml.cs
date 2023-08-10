using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TechWizMain.Areas.Identity.Data;

namespace TechWizMain.Areas.Identity.Pages.Account.Manage
{
    public class DeleteModel : PageModel
    {
        private readonly UserManager<UserManager> _userManager;
        private readonly SignInManager<UserManager> _signInManager;

        public DeleteModel(UserManager<UserManager> userManager, SignInManager<UserManager> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            user.status = false;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                await _signInManager.SignOutAsync();
                if (returnUrl != null)
                {
                    return LocalRedirect(returnUrl);
                }
                else
                {
                    // This needs to be a redirect so that the browser performs a new
                    // request and the identity for the user gets updated.
                    return RedirectToPage();
                }
            }
            return Page();
        }
    }
}
