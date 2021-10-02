using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        private readonly ICarRepository _carRepository;

        public ClientService(IClientRepository clientRepository,
                             IMapper mapper,
                             ICarRepository carRepository)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
            _carRepository = carRepository;
        }

        public ClientViewModel AddClient(ClientViewModel clientRequest)
        {
            var client = _mapper.Map<Client>(clientRequest);
            var clientVm = _clientRepository.Add(client);
            return _mapper.Map<ClientViewModel>(clientVm);
        }

        public void DeleteClient(Guid id)
        {
            var client = _clientRepository.GetClientWithRentedCars(id);
            foreach (var car in client.Cars)
            {
                client.ReturnCar(car);
            }

            _clientRepository.Delete(client);
        }

        public void EditClient(ClientViewModel clientRequest)
        {
            var client = _mapper.Map<Client>(clientRequest);
            _clientRepository.Update(client);
        }

        public List<ClientViewModel> FullTextSearch(string searchTerm)
        {
            var clientsVm = new List<ClientViewModel>();
            var clients = string.IsNullOrEmpty(searchTerm) ? _clientRepository.GetAll() : _clientRepository.FullTextSearch(searchTerm);

            if (clients != null && clients.Any())
                clientsVm = _mapper.Map<List<ClientViewModel>>(clients);

            return clientsVm;
        }

        public ClientViewModel GetClient(Guid id)
        {
            var client = _clientRepository.GetById(id);
            return _mapper.Map<ClientViewModel>(client);
        }

        public List<ClientViewModel> GetClients()
        {
            var clients = _clientRepository.GetAll();
            return _mapper.Map <List<ClientViewModel>>(clients);
        }

        public ClientRentCarsViewModel GetClientWithRentedCars(Guid id)
        {
            var client = _clientRepository.GetClientWithRentedCars(id);

            var clientVm = _mapper.Map<ClientViewModel>(client);
            var carsVm = _mapper.Map<IEnumerable<CarViewModel>>(client.Cars);

            var clientRentedCarsVm = new ClientRentCarsViewModel
            {
                Client = clientVm,
                Cars = carsVm
            };

            return clientRentedCarsVm;
        }

        public void RentCar(Guid clientId, Guid carId)
        {
            var car = _carRepository.GetById(carId);
            if (!car.IsAvailable)
            {
                throw new Exception("This car is not available!");
            }

            var client = _clientRepository.GetClientWithRentedCars(clientId);

            if (!client.Cars.Any())
            {
                client.Cars = new List<Car>();
            }

            var alreadyRentedCar = client.Cars.FirstOrDefault(c => c.Id == carId);
            if(alreadyRentedCar != null) { 
                throw new Exception("This client has already rented this car!");
            }

            client.RentCar(car);
            _clientRepository.Update(client);
        }

        public void ReturnCar(Guid clientId, Guid carId)
        {
            var car = _carRepository.GetById(carId);
            var client = _clientRepository.GetClientWithRentedCars(clientId);

            client.ReturnCar(car);
            _clientRepository.Update(client);
        }
    }
}
