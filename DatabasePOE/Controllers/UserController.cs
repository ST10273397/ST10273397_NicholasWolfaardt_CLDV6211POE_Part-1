using CLDV_POE.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CLDV_POE.Controllers
{
    public class UserController : Controller
    {
        // Create an instance of UserTable to interact with the database
        public UserTable usrtbl = new UserTable();

        // Action method for handling the POST request when signing up
        [HttpPost]
        public ActionResult SignUp(UserTable users)
        {
            // Check if the model state is valid (i.e., if the data passed validation)
            if (ModelState.IsValid)
            {
                try
                {
                    // Try to insert the user into the database
                    var result = usrtbl.insert_User(users);
                    if (result == 1)
                    {
                        // Redirect to the SignUp action of the Home controller on success
                        return RedirectToAction("SignUp", "Home");
                    }
                    else
                    {
                        // If insert_User returns 0 or any other unexpected value, add an error
                        ModelState.AddModelError(string.Empty, "Failed to insert user. Please try again.");
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception (not shown in this code snippet)
                    ModelState.AddModelError(string.Empty, "An error occurred. Please try again.");
                }
            }

            // If we reach here, it means something went wrong or model state is invalid
            // Return the view with the user data so they can correct their input
            return View(users);
        }

        // Action method for handling the GET request to show the sign-up form
        [HttpGet]
        public ActionResult SignUp()
        {
            // Return the view with an empty UserTable object to capture user input
            return View(new UserTable());
        }
    }
}
