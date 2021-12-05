using System;

namespace Wtw.Policies.Domain.Models
{
    public class Entity
    {
        public Guid UUID { get; set; }

        public Guid CreatedBy_UUID { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Guid ModifiedBy_UUID { get; set; }

        public DateTime ModifiedAt { get; set; }

        public bool Active { get; set; } = true;
    }
}
