using Expense_Tracker.Db;
using Expense_Tracker.Models;
using Expense_Tracker.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
using Microsoft.EntityFrameworkCore;

namespace Expense_Tracker.Controllers.Account_Controller
{
    public class AccountController : Controller
    {
       private readonly ApplicationDbContext _dbContext;

        public AccountController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult SignUp()
        {
            var model = new SignUpModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignUpModel model)
        {
            
                if (!ModelState.IsValid)
                {
                    var errorMessages = new List<string>();
                    foreach (var modelStateValue in ModelState.Values)
                    {
                        foreach (var error in modelStateValue.Errors)
                        {
                            errorMessages.Add(error.ErrorMessage);
                        }
                    }
                    ViewBag.ErrorMessages = errorMessages;
                }
                else
                {
                try
                {
                    //Perform a check before saving the new user data
                    //EmailCheck
                    var existingUserWithEmail = _dbContext.Account.FirstOrDefault(u => u.Email == model.Email);
                    if (existingUserWithEmail != null)
                    {
                        ModelState.AddModelError("Email", "Email already Registered");
                        return View(model);
                    }
                    //MobileCheck
                    var existingUserWithPhone = _dbContext.Account.FirstOrDefault(u => u.Mobile == model.Mobile);
                    if (existingUserWithPhone != null)
                    {
                        ModelState.AddModelError("Mobile", "Phone Number already Registered");
                        return View(model);
                    }
                    //UserNameCheck
                    var existingUserWithUserName = _dbContext.Account.FirstOrDefault(u => u.userName == model.userName);
                    if (existingUserWithUserName != null)
                    {
                        ModelState.AddModelError("userName", "UserName is Already Exists.Choose any other Username");
                        return View(model);
                    }
                    if (ModelState.IsValid)
                    {
                        var users = _dbContext.Account.ToList();

                        Account newUser = new Account
                        {
                            userName = model.userName,
                            AccountType = model.AccountType,
                            Address = model.Address,
                            Email = model.Email,
                            Mobile = model.Mobile,
                            Gender = model.Gender,
                            IsRemember =true,
                            IsActive = true,
                            AccountNumber = "",
                            Amount = model.Amount,
                            Password = model.Password,
                        };

                        HttpContext.Session.SetInt32("userId", newUser.AccountId);


                        _dbContext.Account.Add(newUser);
                        _dbContext.SaveChanges();
                        return Redirect("/Dashboard/Index");
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                    return View(model);
                    
                }
                   
                }
            
            return View(model);
        }



        [HttpGet]
        public IActionResult Login()
        {
            var model = new LoginModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //Perform login logic here
            var users = _dbContext.Account.ToList();
            var user = users.FirstOrDefault(u => u.Email.Equals(model.Email, StringComparison.OrdinalIgnoreCase));

            if(user != null && user.Password == model.Password)
            {
                HttpContext.Session.SetInt32("userId", user.AccountId);
                return RedirectToAction("Index", "Dashboard");
            }
            return View(model);

        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login","Account"); //Redirect to the desired page after logout
        }
    }
}
