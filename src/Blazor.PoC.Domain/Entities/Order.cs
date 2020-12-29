using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.PoC.Domain.Entities
{
    public class Order : AuditableEntity
    {
        public Order()
        {
            Items = new HashSet<OrderItem>();
        }

        public int Id { get; set; }

        public Client Client { get; set; }

        public ICollection<OrderItem> Items { get; set; }
    }
}
