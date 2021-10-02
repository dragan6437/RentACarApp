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
    public class CarRepository : BaseRepository<Car>, ICarRepository
    {
        private readonly RentACarDbContext _dbContext;

        public CarRepository(RentACarDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Car> FullTextSearch(string searchTerm)
        {
            return _dbContext.Cars.AsEnumerable().Where(c => (c.IsAvailable == true
            && (c.Make.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
            || c.Model.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))));
        }

        public IEnumerable<Car> GetAvailableCars()
        {
            return _dbContext.Cars.Where(c => c.IsAvailable == true);
        }

        public Car GetCarWithRentedClient(Guid id)
        {
            var car = _dbContext.Cars.Include(x => x.Client).FirstOrDefault(car => car.Id == id);
            return car;
        }
    }
}
