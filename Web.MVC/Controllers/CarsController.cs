using Application.Interfaces;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Web.MVC.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        public IActionResult Index(int? page)
        {
            var cars = _carService.GetCars().ToPagedList(page ?? 1, 6);
            return View(cars);
        }

        public IActionResult Details(Guid id)
        {
            var model = _carService.GetCar(id);
            return View(model);
        }

        [HttpGet]
        public IActionResult AddOrEdit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return View();
            }

            var model = _carService.GetCar(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult AddOrEdit(CarViewModel carVm)
        {
            ModelState.Remove("Id");
            if (!ModelState.IsValid)
            {
                return View();
            }

            var carId = carVm.Id;

            if (carVm.Id == Guid.Empty)
            {
                var addedCar = _carService.AddCar(carVm);
                carId = addedCar.Id;
            }
            else
            {
                _carService.EditCar(carVm);
            }

            return RedirectToAction("Details", new { id = carId});
        }

        public IActionResult Delete(Guid id)
        {
            var model = _carService.GetCar(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult ConfirmDelete(Guid id)
        {
            _carService.DeleteCar(id);
            return RedirectToAction("Index");
        }

        public IActionResult SearchCar(string searchTerm)
        {
            var model = _carService.FullTextSearch(searchTerm);
            return Json(model);
        }

        [HttpGet]
        public IActionResult Rent(Guid id)
        {
            var model = _carService.GetCarWithRentedClient(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Rent(Guid carId, Guid clientId)
        {
            _carService.RentClient(carId, clientId);
            return RedirectToAction("Rent", new { Id = carId });
        }

        [HttpPost]
        public IActionResult Return(Guid carId, Guid clientId)
        {
            _carService.ReturnClient(carId, clientId);
            return RedirectToAction("Rent", new { Id = carId });
        }
    }
}
