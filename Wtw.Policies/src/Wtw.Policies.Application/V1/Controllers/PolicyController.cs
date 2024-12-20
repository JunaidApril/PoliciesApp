﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Wtw.Policies.Application.V1.Commands;
using Wtw.Policies.Application.V1.Interfaces;
using Wtw.Policies.Application.V1.Queries;
using Wtw.Policies.Domain.Dtos;

namespace Wtw.Policies.Application.V1.Controllers
{
    
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/[controller]")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class PolicyController : ControllerBase
    {
        private readonly IPolicyService _policyService;

        public PolicyController(IPolicyService policyService)
        {
            _policyService = policyService;
        }

        /// <summary>
        /// Create a policy with policy holder
        /// </summary>
        /// <param name="policyApplicationDto"></param>
        /// <returns>Guid</returns>
        [HttpPost("Create")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreatePolicyHolderCommand command)
        {
            var result = await _policyService.CreatePolicyAsync(command);
            return Ok(result);
        }

        /// <summary>
        /// Remove a policy
        /// </summary>
        /// <param name="policyUUID"></param>
        /// <returns>bool</returns>
        [HttpPut("Remove")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<IActionResult> Remove(DeletePolicyCommand command)
        {
            await _policyService.RemovePolicyAsync(command);
            return Ok();
        }

        /// <summary>
        /// Update an existing policy and policy holder details
        /// </summary>
        /// <param name="policyDto"></param>
        /// <returns>bool</returns>
        [HttpPost("Update")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        public async Task<IActionResult> Update([FromBody] UpdatePolicyCommand command)
        {
            var result = await _policyService.UpdatePolicyAsync(command);
            return Ok(result);
        }

        /// <summary>
        /// Get all policies and the policy holders details 
        /// </summary>
        /// <returns>IEnumerable<ApplicationDto></returns>
        [HttpGet("GetAll")]
        [ProducesResponseType(typeof(IEnumerable<PolicyDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _policyService.GetPoliciesAsync();
            return Ok(result);
        }

        /// <summary>
        /// Get a policy and the policy holders details
        /// </summary>
        /// <param name="policyUUID"></param>
        /// <returns>PolicyDto</returns>
        [HttpGet("Get")]
        [ProducesResponseType(typeof(PolicyDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] GetPolicyQuery command)
        {
            var result = await _policyService.GetPolicyAsync(command);
            return Ok(result);
        }
    }
}
