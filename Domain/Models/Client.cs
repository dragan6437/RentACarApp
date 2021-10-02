using Domain.Enums;
using Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Client : AuditableBaseEntry
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public Gender Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<Car> Cars { get; set; }

        public void RentCar(Car car)
        {
            car.IsAvailable = false;
            Cars.Add(car);
        }

        public void ReturnCar(Car car)
        {
            car.IsAvailable = true;
            Cars.Remove(car);
        }
    }
}
