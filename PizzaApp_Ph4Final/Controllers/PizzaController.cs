using Microsoft.AspNetCore.Mvc;
using PizzaApp_Ph4Final.Models;

namespace PizzaApp_Ph4Final.Controllers
{
    public class PizzaController : Controller
    {
        static public List<Pizza> pizzadetails = new List<Pizza>() {

                new Pizza { PizzaId = 1,Type = "chicken", Price =420,Quantity=2},
                new Pizza { PizzaId = 2,Type = "Mushroom",Price=300,Quantity=1},
                new Pizza { PizzaId = 3,Type = "Paneer",Price=450,Quantity = 2},
                new Pizza { PizzaId = 4,Type = "Corn",Price=400, Quantity = 1},
                new Pizza { PizzaId = 5,Type = "Pepperoni",Price=340, Quantity = 1},
                new Pizza { PizzaId = 6,Type = "California",Price=490,Quantity = 2}
            };
        static public List<OrderInfo> orderdetails = new List<OrderInfo>();

        public IActionResult Index()
        {
            return View(pizzadetails);
        }

        public IActionResult Cart(int id)
        {
            var found = (pizzadetails.Find(p => p.PizzaId == id));

            TempData["id"] = id;

            return View(found);

        }
        [HttpPost]
        public IActionResult Cart(IFormCollection f)
        {
            Random r = new Random();
            int id = Convert.ToInt32(TempData["id"]);
            OrderInfo o = new OrderInfo();
            var found = (pizzadetails.Find(p => p.PizzaId == id));
            o.OrderId = r.Next(100, 999);
            o.PizzaId = id;
            o.Price = found.Price;
            o.Type = found.Type;
            o.Quantity = found.Quantity;
            o.TotalPrice = (o.Price * found.Quantity);

            orderdetails.Add(o);

            return RedirectToAction("Checkout");

        }
        public IActionResult Checkout()
        {
            return View(orderdetails);
        }
        public IActionResult Buy()
        {
            return View();
        }

        [HttpGet]
    public IActionResult Delete(int id) {
            Pizza pizza = pizzadetails.SingleOrDefault(e=>e.PizzaId==id);
            return View(pizza);
        }
        [HttpPost]
        public IActionResult Delete(int? id) {
            Pizza p = pizzadetails.SingleOrDefault(e=>e.PizzaId==id);
            if (p!=null) {
                pizzadetails.Remove(p);
            }
            return RedirectToAction("Index");
        }
        public List<Pizza> DisplayAll() {
            return pizzadetails;
        }
        public List<Pizza> pizzaByid(int id) {
            List<Pizza> pizza = pizzadetails.FindAll(e => e.PizzaId== id);
            return pizza ;
        }
    }
}
