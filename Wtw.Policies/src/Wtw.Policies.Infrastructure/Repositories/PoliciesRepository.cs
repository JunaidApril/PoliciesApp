using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wtw.Policies.Domain.Dtos;
using Wtw.Policies.Domain.Models;
using Wtw.Policies.Infrastructure.Data;
using Wtw.Policies.Infrastructure.Repositories.Interfaces;

namespace Wtw.Policies.Infrastructure.Repositories
{
    public class PoliciesRepository : IPoliciesRepository
    {
        private readonly PoliciesContext _context;
        private readonly ILogger _logger;

        public PoliciesRepository(PoliciesContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public Task<Policy> FindByIdAsync(Guid policyUUID)
        {
            try
            {
                return _context.Policies
                    .SingleOrDefaultAsync(policy => policy.UUID == policyUUID);
            }
            catch(Exception ex)
            {
                throw new BusinessException("Unable to find policy");
            }
        }

        public async Task<IEnumerable<Policy>> GetAllAsync()
        {
            return await _context.Policies.ToListAsync();
        }

        public async Task<Guid> CreateAsync(Policy policy)
        {
            try
            {
                await _context.Policies.AddAsync(policy);
                await _context.SaveChangesAsync();

                return policy.UUID;
            }
            catch(Exception ex)
            {
                throw new BusinessException("Unable to create policy");
            }
        }

        public Task UpdateAsync(Policy policy)
        {
            try
            {
                _context.Policies.Update(policy);
                return _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new BusinessException("Unable to update policy");
            }
        }

        public async Task DeleteAsync(Guid policyUUID)
        {
            try
            {
                var policy =
                    await _context.Policies.SingleOrDefaultAsync(reason => reason.UUID == policyUUID);
                if (policy == null)
                {
                    _logger.LogWarning("Cannot delete policy {policyUUID} entity does not exist", policyUUID);
                    throw new BusinessException();
                }

                policy.Active = false;
                _context.Policies.Update(policy);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new BusinessException("Unable to remove policy");
            }
        }
    }
}
