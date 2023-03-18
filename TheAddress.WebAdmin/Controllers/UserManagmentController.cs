using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using TheAddress.DAL.DBModel;
using TheAddress.WebAdmin.ViewModels;

namespace TheAddress.WebAdmin.Controllers
{
    [Authorize(Roles ="Admin")]
    public class UserManagmentController : Controller

    {
            private UserManager<AppUser> _userManager;
            private RoleManager<AppRole> _roleManager;
            private readonly ILogger<UserManagmentController> _logger;
            public UserManagmentController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, ILogger<UserManagmentController> logger)
            {
                _userManager = userManager;
                _roleManager = roleManager;
                _logger = logger;
            }


            #region UserOperation
            public IActionResult UserIndex()
            {

                List<UserViewModel> viewModels = new();

                List<AppUser> appUsers = _userManager.Users.ToList();
                foreach (var item in appUsers)
                {
                    UserViewModel viewModel = new()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Surname = item.Surname,
                        Email = item.Email,
                        UserName = item.UserName,
                        GenderId = item.GenderId,
                        GenderDesc = item.GenderId == 1 ? "Kişi" : "Qadın",

                    };
                    viewModels.Add(viewModel);
                }

                return View(viewModels);
            }

            public IActionResult UserCreate()
            {

                return View();

            }

            [HttpPost]
            public async Task<IActionResult> UserCreate(UserViewModel viewModel)
            {

                if (ModelState.IsValid)
                {
                    AppUser user = new()
                    {

                        Name = viewModel.Name,
                        Surname = viewModel.Surname,
                        UserName = viewModel.UserName,
                        Email = viewModel.Email,
                        GenderId = viewModel.GenderId

                    };
                    IdentityResult result = await _userManager.CreateAsync(user, viewModel.Password);
                    if (result.Succeeded)
                    {

                        TempData["success"] = "Istifadəçi uğurla əlavə edildi";
                        _logger.LogInformation($"Sistemə {viewModel.Id} N-li Id yaradılmışdır");
                        return RedirectToAction("UserIndex");
                    }

                }
                return View(viewModel);

            }

            [HttpGet]
            public async Task<IActionResult> UserUpdate(string id)
            {
                AppUser user = await _userManager.FindByIdAsync(id);

                if (user == null)
                {
                    _logger.LogError($"Sistemdə olmayan {id} N-li Id çağrılmışdır");
                    return NotFound();
                }

                UserViewModel viewModel = new()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    UserName = user.UserName,
                    Email = user.Email,
                    GenderId = user.GenderId,

                };

                return View(viewModel);
            }

            [HttpPost]
            public async Task<IActionResult> UserUpdate(UserViewModel model)
            {
                if (ModelState.IsValid)
                {
                    AppUser user = await _userManager.FindByIdAsync(model.Id);

                    user.Name = model.Name;
                    user.Surname = model.Surname;
                    user.GenderId = model.GenderId;
                    user.UserName = model.UserName;
                    user.Email = model.Email;

                    IdentityResult result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(model.Password))
                        {
                            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                            var resetResult = await _userManager.ResetPasswordAsync(user, token, model.Password);
                            if (!resetResult.Succeeded)
                            {
                                ModelState.AddModelError(string.Empty, "The password could not be reset.");
                            }
                        }

                        TempData["success"] = "Istifadəçi məlumatları dəyişdirildi";
                        _logger.LogInformation($"Sistemə {model.Id} N-li Id yaradılmışdır");
                        return RedirectToAction("UserIndex");
                    }
                }
                return View(model);
            }


            public async Task<IActionResult> UserDelete(string id)
            {

                AppUser user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {

                    IdentityResult result = await _userManager.DeleteAsync(user);
                    if (result.Succeeded)
                    {
                        TempData["success"] = "Istifadəçi uğurla silindi";
                        _logger.LogInformation($"Sistemdə {id} N-li Id silinmişdir");
                        return RedirectToAction("UserIndex");
                    }

                }
                return View();
            }

            public async Task<string> UserRole(string id)
            {

                AppUser user = await _userManager.FindByIdAsync(id);
                IList<string> roles = await _userManager.GetRolesAsync(user);

                StringBuilder builder = new StringBuilder();
                foreach (var item in roles)
                {
                    builder.Append(item + "; ");
                }
                return builder.ToString();
            }
            #endregion

            #region RoleOperation
            public IActionResult RoleIndex()
            {

                List<RoleViewModel> viewModels = new();

                List<AppRole> appRoles = _roleManager.Roles.ToList();
                foreach (var item in appRoles)
                {
                    RoleViewModel viewModel = new()

                    {
                        Id = item.Id,
                        Name = item.Name

                    };
                    viewModels.Add(viewModel);
                }

                return View(viewModels);
            }
            public IActionResult RoleCreate()
            {

                return View();

            }
            [HttpPost]
            public async Task<IActionResult> RoleCreate(RoleViewModel viewModel)
            {
                if (ModelState.IsValid)
                {
                    AppRole role = new AppRole()
                    {
                        Name = viewModel.Name
                    };
                    IdentityResult result = await _roleManager.CreateAsync(role);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation($"Sistemə {viewModel.Id} N-li Id yaradılmışdır");
                        return RedirectToAction("RoleIndex");
                    }

                }
                return View(viewModel);

            }

            public async Task<IActionResult> RoleAssign(string Id)
            {
                AppUser user = await _userManager.FindByIdAsync(Id);

                UserRoleViewModel viewModel = new()
                {
                    UserFullName = user.Name + "  " + user.Surname,
                    UserId = user.Id
                };
                List<AppRole> roles = _roleManager.Roles.ToList();

                List<RoleViewModel> roleViewModels = new();
                foreach (var item in roles)
                {
                    RoleViewModel roleView = new()
                    {
                        Id = item.Id,
                        Name = item.Name,
                    };
                    roleViewModels.Add(roleView);
                }
                viewModel.Roles = roleViewModels;
                return View(viewModel);

            }

            [HttpPost]
            public async Task<IActionResult> RoleAssign(UserRoleViewModel viewModel)
            {
                AppUser user = await _userManager.FindByIdAsync(viewModel.UserId);
                if (user != null)
                {
                    IdentityResult result = await _userManager.AddToRoleAsync(user, viewModel.RoleName);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("UserIndex");
                    }
                }


                return View(viewModel);

            }
            [HttpGet]
            public async Task<IActionResult> RoleUpdate(string Id)
            {
                AppRole role = await _roleManager.FindByIdAsync(Id);
                if (role == null)
                {
                    _logger.LogError($"Sistemdə olmayan {role.Id} N-li Id çağrılmışdır");
                    return NotFound();
                }

                RoleViewModel viewModel = new()
                {
                    Id = role.Id,
                    Name = role.Name
                };

                return View(viewModel);
            }

            [HttpPost]
            public async Task<IActionResult> RoleUpdate(RoleViewModel viewModel)
            {
                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }

                AppRole role = await _roleManager.FindByIdAsync(viewModel.Id);
                if (role == null)
                {
                    _logger.LogError($"Sistemdə olmayan {role.Id} N-li Id çağrılmışdır");
                    return NotFound();
                }

                role.Name = viewModel.Name;
                IdentityResult result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    TempData["success"] = "Role məlumatları dəyişdirildi";
                    _logger.LogInformation($"Sistemdə {role.Id} N-li Id yenilənmişdir");
                    return RedirectToAction("RoleIndex");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(viewModel);
            }

            public async Task<IActionResult> RoleDelete(string id)
            {
                var role = await _roleManager.FindByIdAsync(id);

                if (role == null)
                {
                    _logger.LogError($"Sistemdə olmayan {id} N-li Id çağrılmışdır");
                    return NotFound();
                }

                var result = await _roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    TempData["success"] = "Istifadəçi uğurla silindi";

                    _logger.LogInformation($"Sistemdə {id} N-li Id silinmişdir");
                    return RedirectToAction("RoleIndex");
                }

                return View();
            }

            #endregion
        }
    }