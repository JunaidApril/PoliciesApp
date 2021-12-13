using Wtw.Policies.Domain.Dtos;

namespace Wtw.Policies.Application.V1.Interfaces
{
    public interface IValidationHelper
    {
        void ValidatePolicyHolderInfo(ApplicationDto policyApplicationDto);

        void HandleValidation(string message);
    }
}
