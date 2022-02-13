using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace awsss.Domain.Contracts
{
    public interface IAuditableEntity
    {
        public DateTime CreatedOn { get; }
    }
}
