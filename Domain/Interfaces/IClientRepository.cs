using Domain.Interfaces.Base;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IClientRepository : IBaseRepository<Client>
    {
        void Update(Client entity);
        Client GetClientWithRentedCars(Guid id);
        IEnumerable<Client> FullTextSearch(string searchTerm);
    }
}
