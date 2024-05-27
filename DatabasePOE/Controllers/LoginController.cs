using CLDV_POE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CLDV_POE.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginModel login;

        // Constructor to initialize the LoginModel
        public LoginController()
        {
            login = new LoginModel();
        }

        // Action method for handling the POST request when user tries to log in
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            try
            {
                // Create an instance of LoginModel
                var loginModel = new LoginModel();

                // Call SelectUser method to check if the user exists and retrieve user ID
                int userID = loginModel.SelectUser(email, password);

                // Check if a valid user ID was returned
                if (userID != -1)
                {
                    // Store the user ID in session
                    HttpContext.Session.SetInt32("UserID", userID);

                    // Redirect to the home page with the user ID parameter
                    return RedirectToAction("Index", "Home", new { userID = userID });
                }
                else
                {
                    // If user not found, return a view indicating login failure
                    return View("LoginFailed");
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                // For now, return an error view or message
                return View("Error");
            }
        }
    }
}
