using System;
using System.Collections.Generic;
using System.Text;
using Wtw.Policies.Domain.Enums;

namespace Wtw.Policies.Domain.Dtos
{
    public class PolicyHolderDto
    {
        public Guid Uuid { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public GenderType Gender { get; set; }
    }
}
