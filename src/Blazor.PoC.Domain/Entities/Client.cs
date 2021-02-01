using Blazor.PoC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.PoC.Domain.Entities
{
    public class Client : AuditableEntity
    {
        public Client()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
