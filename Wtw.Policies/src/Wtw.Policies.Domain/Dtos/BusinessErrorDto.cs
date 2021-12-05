using System;
using System.Net;
using Wtw.Policies.Domain.Enums;

namespace Wtw.Policies.Domain.Dtos
{
    public class BusinessErrorDto
    {
        public string Message { get; set; }
        public HttpStatusCode? HttpStatusCode { get; set; }
        public BusinessErrorCode? BusinessErrorCode { get; set; }
        public DateTimeOffset ExceptionTime { get; set; } = DateTimeOffset.UtcNow;
    }
}
