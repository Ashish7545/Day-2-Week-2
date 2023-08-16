using Microsoft.AspNetCore.Mvc;
using Practical.Models;
using Practical.Models.ViewModel;

namespace Practical.Controllers
{
    public class ProductController : Controller
    {
        private static List<Product> products = new List<Product>
        {
             new Product { Id = 1, Name = "Product 1", Price = 10.99m },
             new Product { Id = 2, Name = "Product 2", Price = 19.99m },
             new Product { Id = 3, Name = "Product 3", Price = 29.99m }
        };

        public IActionResult Index()
        {
            return View(products);
        }

        public IActionResult Details(int id)
        {
            Product product = products.Find(x => x.Id == id);
            DetailsVM detailsVM = new DetailsVM()
            {
                Product = product,
                DisplayMessage = "Details of product."
            };
            return View(detailsVM);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // helps defend against cross-site request forgery
        public IActionResult Create(Product product)
        {
            if(ModelState.IsValid)
            {
                product.Id = products.Count+1;
                products.Add(product);
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        public IActionResult Edit(int? id)
        {
            Product product = products.Find(x => x.Id == id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if(ModelState.IsValid)
            {
                int index = products.FindIndex(p =>  p.Id == product.Id);
                products[index] = product;
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Product product = products.Find(p => p.Id == id);
            products.Remove(product);

            return Json(new { success = true });
        }
    }
}
