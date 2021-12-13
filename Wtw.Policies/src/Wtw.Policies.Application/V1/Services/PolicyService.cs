using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wtw.Policies.Application.V1.Commands;
using Wtw.Policies.Application.V1.Interfaces;
using Wtw.Policies.Application.V1.Queries;
using Wtw.Policies.Domain.Dtos;
using Wtw.Policies.Infrastructure.Repositories.Interfaces;

namespace Wtw.Policies.Application.V1.Services
{
    public class PolicyService : IPolicyService
    {
        private readonly IPolicyHolderRepository _policyHolderRepository;
        private readonly IPoliciesRepository _policiesRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IValidationHelper _validationHelper;
        private readonly IMediator _mediator;

        public PolicyService(IPolicyHolderRepository policyHolderRepository, IPoliciesRepository policiesRepository, 
            IMapper mapper, ILogger logger, IValidationHelper validationHelper, IMediator mediator)
        {
            _policyHolderRepository = policyHolderRepository;
            _policiesRepository = policiesRepository;
            _mapper = mapper;
            _logger = logger;
            _validationHelper = validationHelper;
            _mediator = mediator;
        }

        public async Task<Guid> CreatePolicyAsync(CreatePolicyHolderCommand command)
        {
            try
            {
                //store policy holder
                var policyHolderResponse = await _mediator.Send(command);

                //Check if saved
                if (policyHolderResponse == null)
                {
                    _validationHelper.HandleValidation("Unable to create policy. Failed creating the policy holder");
                }

                //store policy
                var policyCommand = new CreatePolicyCommand
                {
                    PolicyHolder = policyHolderResponse
                };

                var policyUuid = await _mediator.Send(policyCommand);

                //Check if saved
                if (policyUuid == null)
                {
                    _validationHelper.HandleValidation("Unable to create policy. Failed creating the policy");
                }

                _logger.LogInformation(String.Format("Policy created for {0}", policyUuid));

                return policyUuid.Response;
            }
            catch(Exception ex)
            {
                _logger.LogWarning("Failed to create policy: CreatePolicyAsync");
                throw new BusinessException("Unable to create policy.");
            }
        }

        public async Task RemovePolicyAsync(DeletePolicyCommand command)
        {
            try
            {
                var uuid = await _mediator.Send(command);
                _logger.LogInformation(String.Format("Policy remove for {0}", command.UUID));
                return;
            }
            catch(Exception ex)
            {
                _logger.LogWarning("Failed to removed policy: RemovePolicyAsync");
                throw new BusinessException("Unable to remove policy.");
            }
        }

        public async Task<Guid> UpdatePolicyAsync(UpdatePolicyCommand command)
        {
            var commandHolder = new UpdatePolicyHolderCommand
            {
                Uuid = command.PolicyHolderCommand.Uuid,
                Name = command.PolicyHolderCommand.Name,
                Age = command.PolicyHolderCommand.Age,
                Gender = command.PolicyHolderCommand.Gender
            };

            //Validate command
            var validator = new UpdatePolicyHolderCommandValidator();
            var result = validator.Validate(commandHolder);

            if (!result.IsValid)
            {
                _validationHelper.HandleValidation(result.ToString());
            }

            try
            {
                //update policy holder information
                var policyHolderResponse = await _mediator.Send(commandHolder);
                
                if(policyHolderResponse == null)
                {
                    _validationHelper.HandleValidation("Unable to update policy holder.");
                }

                _logger.LogInformation(String.Format("Policy updated for {0}", command.Uuid));

                return command.Uuid;
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
                //retrieve all policies
                var response = await _mediator.Send(new GetAllPoliciesQuery());

                //foreach policy get policy holder details
                if(response == null || response.policies == null)
                {
                    _validationHelper.HandleValidation("Unable to get all policies.");
                }
                foreach (var policy in response.policies)
                {
                    if (policy.PolicyHolder_UUID != Guid.Empty)
                    {
                        var command = new GetPolicyHolderQuery { UUID = policy.PolicyHolder_UUID };
                        var result = await _mediator.Send(command);
                        policy.PolicyHolder = result.policyHolder;
                    }
                }

                return response.policies;
            }
            catch(Exception ex)
            {
                _logger.LogWarning("Failed retrieving policies: GetPoliciesAsync");
                throw new BusinessException("Unable to retrieve policies. Please try again later");
            }
        }

        public async Task<PolicyDto> GetPolicyAsync(GetPolicyQuery command)
        {
            var response = await _mediator.Send(command);

            if (response == null)
            {
                _validationHelper.HandleValidation(String.Format("Could not find policy for uuid: {0}", command.UUID));
            }
            var holderCommand = new GetPolicyHolderQuery { UUID = response.policy.PolicyHolder_UUID };
            var holderResponse = await _mediator.Send(holderCommand);
            response.policy.PolicyHolder = holderResponse.policyHolder;

            return response.policy;
        }
    }
}
