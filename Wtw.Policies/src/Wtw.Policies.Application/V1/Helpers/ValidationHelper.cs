using Microsoft.Extensions.Logging;
using System;
using Wtw.Policies.Application.V1.Interfaces;
using Wtw.Policies.Domain.Dtos;

namespace Wtw.Policies.Application.V1.Helpers
{
    public class ValidationHelper : IValidationHelper
    {
        private readonly ILogger _logger;

        public ValidationHelper(ILogger logger)
        {
            _logger = logger;
        }

        public void ValidatePolicyHolderInfo(ApplicationDto policyApplicationDto)
        {
            if (String.IsNullOrEmpty(policyApplicationDto.Name))
            {
                HandleValidation("Unable to create policy. Please enter a valid name");
            }

            if (policyApplicationDto.Age < 18)
            {
                HandleValidation("Unable to create policy. Applicant needs to be 18 or older");
            }
        }

        public void HandleValidation(string message)
        {
            _logger.LogWarning(message);
            throw new BusinessException(message);
        }
    }
}
