using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;

namespace SalesWebMvc.Controllers
{
    public class DepartmentsController : Controller
    {
        public IActionResult Index()
        {
            List<Department> departments = new List<Department>();
            departments.Add(new Department(1, "Recursos Humanos"));
            departments.Add(new Department(2, "Técnologia da informação"));
            departments.Add(new Department(3, "Vendas"));

            return View(departments);
        }
    }
}
