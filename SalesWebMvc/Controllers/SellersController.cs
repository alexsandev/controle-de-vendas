using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {

        private SellerService _sellerService;
        private DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var list = await _sellerService.FindAllAsync();
            return View(list);
        }

        [Authorize]
        public async Task <IActionResult> Create()
        {
            var departments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel 
            { 
                Departments = departments
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(Seller seller)
        {
            if(!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Departments = departments, Seller = seller };
                return View(viewModel);
            }
            await _sellerService.InsertAsync(seller);
            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided"});
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);

            if(obj == null) 
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found"});
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sellerService.DeleteAsync(id);
                return RedirectToAction("Index");
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message});
            }
        }

        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null) 
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided"});
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);

            if(obj == null) 
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found"});
            }

            return View(obj);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided"});
            }

            var seller = await _sellerService.FindByIdAsync(id.Value);

            if(seller == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found"});
            }

            List<Department> departments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments};
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int? id, Seller seller)
        {
            if(!ModelState.IsValid)
            {   
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Departments = departments, Seller = seller };
                return View(viewModel);
            }
            if(id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch"});
            }
            try
            {
                await _sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message});
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel 
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}
