using System;
using Wtw.Policies.Domain.Enums;

namespace Wtw.Policies.Domain.Dtos
{
    public class ApplicationDto
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public GenderType GenderType { get; set; }
    }
}
