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
    public class ClientsController : Controller
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        public IActionResult Index(int? page)
        {
            var model = _clientService.GetClients().OrderByDescending(x => x.CreatedOn).ToPagedList(page ?? 1, 6);
            return View(model);
        }

        [HttpGet]
        public IActionResult AddOrEdit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return View();
            }

            var model = _clientService.GetClient(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult AddOrEdit(ClientViewModel clientVm)
        {
            ModelState.Remove("Id");
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (clientVm.Id == Guid.Empty)
            {
                _clientService.AddClient(clientVm);
            }
            else
            {
                _clientService.EditClient(clientVm);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(Guid id)
        {
            var model = _clientService.GetClient(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult ConfirmDelete(Guid id)
        {
            _clientService.DeleteClient(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Rent(Guid id)
        {
            var model = _clientService.GetClientWithRentedCars(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Rent(Guid clientId, Guid carId)
        {
            _clientService.RentCar(clientId, carId);
            return RedirectToAction("Rent", new { Id = clientId });
        }

        [HttpPost]
        public IActionResult Return(Guid clientId, Guid carId)
        {
            _clientService.ReturnCar(clientId, carId);
            return RedirectToAction("Rent", new { Id = clientId });
        }

        public IActionResult SearchClient(string searchTerm)
        {
            var model = _clientService.FullTextSearch(searchTerm);
            return Json(model);
        }
    }
}
