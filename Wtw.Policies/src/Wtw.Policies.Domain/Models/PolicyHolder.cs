using System.Collections.Generic;
using Wtw.Policies.Domain.Enums;

namespace Wtw.Policies.Domain.Models
{
    public class PolicyHolder : Entity
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public GenderType Gender { get; set; }

        public virtual IEnumerable<Policy> Policies { get; set; }
    }
}
