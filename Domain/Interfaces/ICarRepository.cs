using Domain.Interfaces.Base;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICarRepository : IBaseRepository<Car>
    {
        IEnumerable<Car> GetAvailableCars();
        IEnumerable<Car> FullTextSearch(string searchTerm);
        Car GetCarWithRentedClient(Guid id);
    }
}
