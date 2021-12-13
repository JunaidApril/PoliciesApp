using System;

namespace Wtw.Policies.Domain.Dtos
{
    public class PolicyDto
    {
        public Guid Policy_UUID { get; set; }
        public Guid PolicyHolder_UUID { get; set; }
        public virtual PolicyHolderDto PolicyHolder { get; set; }
    }
}
