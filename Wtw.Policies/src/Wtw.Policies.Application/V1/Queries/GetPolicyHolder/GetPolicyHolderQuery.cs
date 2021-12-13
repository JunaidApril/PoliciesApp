using MediatR;
using System;

namespace Wtw.Policies.Application.V1.Queries
{
    public class GetPolicyHolderQuery : IRequest<GetPolicyHolderQueryResponse>
    {
        public Guid UUID { get; set; }
    }
}
