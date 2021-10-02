using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class ClientRentCarsViewModel
    {
        public ClientViewModel Client { get; set; }
        public IEnumerable<CarViewModel> Cars { get; set; }
    }
}
