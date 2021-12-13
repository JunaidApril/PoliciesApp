using MediatR;
using System;

namespace Wtw.Policies.Application.V1.Queries
{
    public class GetPolicyQuery : IRequest<GetPolicyQueryResponse>
    {
        public Guid UUID { get; set; }
    }
}
