using CLDV_POE.Models;
using Microsoft.AspNetCore.Mvc;

namespace CLDV_POE.Controllers
{
    public class ProductController : Controller
    {
        // Create an instance of ProductTable to interact with the database
        public ProductTable prodtbl = new ProductTable();

        // Action method for handling the POST request when submitting product data
        [HttpPost]
        public ActionResult MyWork(ProductTable products)
        {
            // Insert the product data into the database
            var result2 = prodtbl.insert_product(products);

            // Redirect to the home page after the operation is completed
            return RedirectToAction("Index", "Home");
        }

        // Action method for handling the GET request to show the product form
        [HttpGet]
        public ActionResult MyWork()
        {
            // Return the view with an empty ProductTable object to capture product input
            return View(new ProductTable());
        }
    }
}
