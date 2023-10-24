using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Repositories;
using System.ComponentModel.DataAnnotations;

namespace CageShopManagementApp.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public LoginModel(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        [BindProperty]
        [Required(ErrorMessage = "UserName is required.")]
        public string UserName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var loginResult = _userRepository.GetUserByUserNameAndPassword(UserName, Password);

                    if (loginResult != null)
                    {
                        var role = _roleRepository.GetRoleById(loginResult.RoleId);

                        HttpContext.Session.SetString("UserId", loginResult.UserId.ToString());
                        HttpContext.Session.SetString("UserName", loginResult.UserName);
                        HttpContext.Session.SetString("Role", role.RoleName);

                        if (role.RoleName == "Employee")
                        {
                            return RedirectToPage("/Employee/Dashboard");
                        }
                        if (role.RoleName == "Customer")
                        {
                            return RedirectToPage("/Index");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Incorrect UserName or Password!");
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