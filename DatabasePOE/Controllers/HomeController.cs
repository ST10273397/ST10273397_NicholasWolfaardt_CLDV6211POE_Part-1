using CLDV_POE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CLDV_POE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        // Constructor to initialize ILogger and IHttpContextAccessor
        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        // Action method for the home page
        public IActionResult Index()
        {
            try
            {
                // Retrieve all products from the database
                List<ProductTable> products = ProductTable.GetAllProducts();

                // Retrieve UserID from session
                int? userID = _httpContextAccessor.HttpContext.Session.GetInt32("UserID");

                // Pass products and UserID to the view
                ViewData["Products"] = products;
                ViewData["UserID"] = userID;

                return View();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                // For now, return an error view or message
                _logger.LogError(ex, "An error occurred while processing the home page.");
                return View("Error");
            }
        }

        // Action method for the about page
        public IActionResult About()
        {
            return View();
        }

        // Action method for the contact page
        public IActionResult Contact()
        {
            return View();
        }

        // Action method for the MyWork page
        public IActionResult MyWork()
        {
            return View();
        }

        // Action method for the sign-up page
        public IActionResult SignUp()
        {
            return View();
        }

        // Action method for handling errors
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
