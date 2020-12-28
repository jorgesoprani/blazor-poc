using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.PoC.Domain.Entities
{
    public abstract class AuditableEntity
    {
        //public string CreatedBy { get; set; }//TO DO: Add authentication?

        public DateTime Created { get; set; }

        //public string LastModifiedBy { get; set; }//TO DO: Add authentication?

        public DateTime? LastModified { get; set; }
    }
}
