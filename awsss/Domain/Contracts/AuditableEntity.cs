using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace awsss.Domain.Contracts
{
    public abstract class AuditableEntity : IAuditableEntity
    {
        public DateTime CreatedOn { get; private set; }

        protected AuditableEntity()
        {
            CreatedOn = DateTime.UtcNow;
        }
    }
}
