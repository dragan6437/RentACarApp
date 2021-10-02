using Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IClientService
    {
        ClientViewModel GetClient(Guid id);
        List<ClientViewModel> GetClients();
        ClientViewModel AddClient(ClientViewModel clientRequest);
        void EditClient(ClientViewModel clientRequest);
        void DeleteClient(Guid id);
        ClientRentCarsViewModel GetClientWithRentedCars(Guid id);
        void RentCar(Guid clientId, Guid carId);
        void ReturnCar(Guid clientId, Guid carId);
        List<ClientViewModel> FullTextSearch(string searchTerm);
    }
}
