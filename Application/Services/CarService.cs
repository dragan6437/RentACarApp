using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IClientRepository _clientRepository;

        public CarService(ICarRepository carRepository,
                          IMapper mapper,
                          IHostingEnvironment hostingEnvironment,
                          IClientRepository clientRepository)
        {
            _carRepository = carRepository;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
            _clientRepository = clientRepository;
        }

        public CarViewModel AddCar(CarViewModel carRequest)
        {
            string uniqueImageName = ImageHandler(carRequest);
            carRequest.ImagePath = uniqueImageName;

            var car = _mapper.Map<Car>(carRequest);
            var addedCar = _carRepository.Add(car);
            return _mapper.Map<CarViewModel>(addedCar);
        }

        public void DeleteCar(Guid id)
        {
            var car = _carRepository.GetById(id);

            if (car.ImagePath != null)
            {
                string imagePath = Path.Combine(_hostingEnvironment.WebRootPath,
                    "images", car.ImagePath);
                File.Delete(imagePath);
            }

            _carRepository.Delete(car);
        }

        public void EditCar(CarViewModel carRequest)
        {
            var car = _mapper.Map<Car>(carRequest);

            if (carRequest.Image != null)
            {
                if (carRequest.ImagePath != null)
                {
                    string imagePath = Path.Combine(_hostingEnvironment.WebRootPath,
                        "images", carRequest.ImagePath);
                    File.Delete(imagePath);
                }
                carRequest.ImagePath = ImageHandler(carRequest);
            }

            _carRepository.Update(car);
        }

        public List<CarViewModel> FullTextSearch(string searchTerm)
        {
            var carsVm = new List<CarViewModel>();

            var cars = string.IsNullOrEmpty(searchTerm) ? _carRepository.GetAvailableCars() : _carRepository.FullTextSearch(searchTerm);

            if (cars != null && cars.Any())
                carsVm = _mapper.Map<List<CarViewModel>>(cars);

            return carsVm;
        }

        public CarViewModel GetCar(Guid id)
        {
            var car = _carRepository.GetById(id);
            return _mapper.Map<CarViewModel>(car);
        }

        public List<CarViewModel> GetCars()
        {
            var cars = _carRepository.GetAll();
            return _mapper.Map<List<CarViewModel>>(cars);
        }

        public CarRentClientViewModel GetCarWithRentedClient(Guid id)
        {
            var car = _carRepository.GetCarWithRentedClient(id);

            var carVm = _mapper.Map<CarViewModel>(car);
            var clientVm = _mapper.Map<ClientViewModel>(car.Client);

            var carRentClientVm = new CarRentClientViewModel
            {
                Car = carVm,
                Client = clientVm
            };

            return carRentClientVm;
        }

        public void RentClient(Guid carId, Guid clientId)
        {
            var car = _carRepository.GetCarWithRentedClient(carId);

            var client = _clientRepository.GetById(clientId);

            if (car.Client != null)
                throw new Exception("The car can be rented by one client!");

            car.RentClient(client);

            _carRepository.Update(car);
        }

        public void ReturnClient(Guid carId, Guid clientId)
        {
            var car = _carRepository.GetCarWithRentedClient(carId);

            var client = _clientRepository.GetById(clientId);

            car.ReturnClient(client);

            _carRepository.Update(car);
        }

        private string ImageHandler(CarViewModel carRequest)
        {
            string uniqueImageName = null;

            if (carRequest.Image != null)
            {
                string imagesFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                uniqueImageName = Guid.NewGuid().ToString() + "_" + carRequest.Image.FileName;
                string filePath = Path.Combine(imagesFolder, uniqueImageName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    carRequest.Image.CopyTo(fileStream);
                }
            }

            return uniqueImageName;
        }
    }
}
