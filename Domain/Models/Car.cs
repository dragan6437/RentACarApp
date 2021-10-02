using Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Car : AuditableBaseEntry
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public bool IsAvailable { get; set; }

        [Column(TypeName ="decimal(6,2)")]
        public decimal PricePerDay { get; set; }
        public string ImagePath { get; set; }
        public Client Client { get; set; }

        public void RentClient(Client client)
        {
            IsAvailable = false;
            Client = client;
        }
        public void ReturnClient(Client client)
        {
            IsAvailable = true;
            Client = null;
        }
    }
}
