using CLDV_POE.Models;
using Microsoft.AspNetCore.Mvc;

namespace CLDV_POE.Controllers
{
    public class ProductDisplayController : Controller
    {
        // Action method for handling the GET request to display products
        [HttpGet]
        public ActionResult Index()
        {
            // Call the SelectProducts method from the ProductDisplayModel to retrieve products
            var products = ProductDisplayModel.SelectProducts();

            // Return the view with the list of products
            return View(products);
        }
    }
}
