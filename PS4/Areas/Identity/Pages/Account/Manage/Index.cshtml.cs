using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PS4.Areas.Identity.Data;

namespace PS4.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<TAUser> _userManager;
        private readonly SignInManager<TAUser> _signInManager;

        public IndexModel(
            UserManager<TAUser> userManager,
            SignInManager<TAUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
            [Display(Name = "University ID", ShortName = "Unid", Prompt = "u1234567")]
            [RegularExpression(@"^(u{1}[0-9]+)", ErrorMessage = "A uID should start with u and number. e.g. u1234567")]
            public string uID { get; set; }
        }

        private async Task LoadAsync(TAUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var _uid = user.uID;
            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                uID = _uid
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }
            //check uid and update uid
            if (Input.uID != user.uID)
            {
                user.uID = Input.uID;
                var setUidResult =  await _userManager.UpdateAsync(user);
                if(!setUidResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set uID.";
                    return RedirectToPage();
                }
                else
                    await _signInManager.RefreshSignInAsync(user);
            }
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
