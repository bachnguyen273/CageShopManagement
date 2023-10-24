using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Repositories;
using System.ComponentModel.DataAnnotations;

namespace CageShopManagementApp.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly IUserRepository _userRepository;

        public RegisterModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [BindProperty]
        [Required(ErrorMessage = "UserName is required.")]
        public string UserName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirmation Password is required.")]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        public string ConfirmPassword { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Phone is required.")]
        public string Phone { get; set; }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var checkExistUser = _userRepository.GetUserByUserName(UserName);
                    if (checkExistUser == null)
                    {
                        var newUser = new User
                        {
                            UserName = UserName,
                            Password = Password,
                            Email = Email,
                            Phone = Phone,
                            RoleId = 2, // Customer
                            IsActive = true
                        };

                        _userRepository.InsertUser(newUser);

                        return RedirectToPage("/Users/Login");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Duplicate UserName!");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return Page();
        }
    }
}