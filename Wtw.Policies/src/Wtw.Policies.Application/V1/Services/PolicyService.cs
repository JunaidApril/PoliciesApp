using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wtw.Policies.Application.BFF.Interfaces;
using Wtw.Policies.Domain.Dtos;
using Wtw.Policies.Domain.Models;
using Wtw.Policies.Infrastructure.Repositories.Interfaces;

namespace Wtw.Policies.Application.BFF.Services
{
    public class PolicyService : IPolicyService
    {
        private readonly IPolicyHolderRepository _policyHolderRepository;
        private readonly IPoliciesRepository _policiesRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public PolicyService(IPolicyHolderRepository policyHolderRepository, IPoliciesRepository policiesRepository, IMapper mapper, ILogger logger)
        {
            _policyHolderRepository = policyHolderRepository;
            _policiesRepository = policiesRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Guid> CreatePolicyAsync(ApplicationDto policyApplicationDto)
        {
            //validate input
            ValidatePolicyHolderInfo(policyApplicationDto);

            try
            {
                var policyHolder = _mapper.Map<PolicyHolder>(policyApplicationDto);
                var policyHolderSaved = await _policyHolderRepository.CreateAsync(policyHolder);

                var policy = new Policy
                {
                    PolicyHolder = policyHolderSaved
                };

                var policyUuid = await _policiesRepository.CreateAsync(policy);

                return policyUuid;
            }
            catch(Exception ex)
            {
                _logger.LogWarning("Failed to create policy: CreatePolicyAsync");
                throw new BusinessException("Unable to create policy.");
            }
        }

        public async Task<bool> RemovePolicyAsync(Guid policyUUID)
        {
            if(policyUUID == Guid.Empty)
            {
                HandleValidation("Unable to remove policy. There was no policy uuid provided.");
            }

            try
            {
                await _policiesRepository.DeleteAsync(policyUUID);
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogWarning("Failed to remove policy: RemovePolicyAsync");
                throw new BusinessException("Unable to remove policy.");
            }
        }

        public async Task<bool> UpdatePolicyAsync(PolicyDto policyDto)
        {
            //Validate input
            var appDto = _mapper.Map<ApplicationDto>(policyDto.PolicyHolder);
            ValidatePolicyHolderInfo(appDto);

            try
            {
                var policy = _mapper.Map<Policy>(policyDto);

                await _policyHolderRepository.UpdateAsync(policy.PolicyHolder);

                await _policiesRepository.UpdateAsync(policy);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Failed to update policy: UpdatePolicyAsync");
                throw new BusinessException("Unable to update policy.");
            }
        }

        public async Task<IEnumerable<PolicyDto>> GetPoliciesAsync()
        {
            try
            {
                var response = await _policiesRepository.GetAllAsync();

                foreach (var policy in response)
                {
                    policy.PolicyHolder = await _policyHolderRepository.FindByIdAsync(policy.PolicyHolder_UUID);
                }

                var policies = _mapper.Map<IEnumerable<PolicyDto>>(response);
                return policies;
            }
            catch(Exception ex)
            {
                _logger.LogWarning("Failed retrieving policies: GetPoliciesAsync");
                throw new BusinessException("Unable to retrieve policies. Please try again later");
            }
        }

        public async Task<PolicyDto> GetPolicyAsync(Guid policyUUID)
        {
            if (policyUUID == Guid.Empty)
            {
                HandleValidation("Unable to retrieve policy. Please provide a policy uuid");
            }
            try
            {
                var response = await _policiesRepository.FindByIdAsync(policyUUID);

                response.PolicyHolder = await _policyHolderRepository.FindByIdAsync(response.PolicyHolder_UUID);

                var policy = _mapper.Map<PolicyDto>(response);
                return policy;
            }
            catch(Exception ex)
            {
                _logger.LogWarning("Failed retrieving policy: GetPolicyAsync");
                throw new BusinessException("Unable to retreive policy. Please try again later");
            }
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
