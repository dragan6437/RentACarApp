using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data.Context;
using Infrastructure.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        private readonly RentACarDbContext _dbContext;

        public ClientRepository(RentACarDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Client> FullTextSearch(string searchTerm)
        {
            return _dbContext.Clients.AsEnumerable().Where(c => (c.FirstName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
            || c.LastName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
            || c.Email.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)));
        }

        public Client GetClientWithRentedCars(Guid id)
        {
            var client = _dbContext.Clients.Include(x => x.Cars).FirstOrDefault(client => client.Id == id);
            return client;
        }
    }
}
