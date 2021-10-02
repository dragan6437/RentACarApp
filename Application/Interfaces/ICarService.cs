using Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICarService
    {
        CarViewModel GetCar(Guid id);
        List<CarViewModel> GetCars();
        CarViewModel AddCar(CarViewModel carRequest);
        void EditCar(CarViewModel carRequest);
        void DeleteCar(Guid id);
        List<CarViewModel> FullTextSearch(string searchTerm);
        CarRentClientViewModel GetCarWithRentedClient(Guid id);
        void RentClient(Guid carId, Guid clientId);
        void ReturnClient(Guid carId, Guid clientId);
    }
}
