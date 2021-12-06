using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wtw.Policies.Domain.Dtos;
using Wtw.Policies.Domain.Models;
using Wtw.Policies.Infrastructure.Data;
using Wtw.Policies.Infrastructure.Repositories.Interfaces;

namespace Wtw.Policies.Infrastructure.Repositories
{
    public class PolicyHolderRepository : IPolicyHolderRepository
    {
        private readonly PoliciesContext _context;
        private readonly ILogger _logger;

        public PolicyHolderRepository(PoliciesContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public Task<PolicyHolder> FindByIdAsync(Guid policyHolderUUID)
        {
            try
            {
                var policyHolder = _context.PolicyHolders
                    .SingleOrDefaultAsync(policyHolder => policyHolder.UUID == policyHolderUUID);

                return policyHolder;
            }
            catch(Exception ex)
            {
                throw new BusinessException("Unable to find policy holider");
            }
        }

        public async Task<IEnumerable<PolicyHolder>> GetAllAsync()
        {
            try
            {
                return await _context.PolicyHolders.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new BusinessException("Unable to get all policy holders");
            }

        }

        public async Task<PolicyHolder> CreateAsync(PolicyHolder policyHolder)
        {
            try
            {
                await _context.PolicyHolders.AddAsync(policyHolder);
                await _context.SaveChangesAsync();

                return policyHolder;
            }
            catch (Exception ex)
            {
                throw new BusinessException("Unable to create policy holder");
            }
        }

        public PolicyHolder UpdateAsync(PolicyHolder policyHolder)
        {
            try
            {
                _context.PolicyHolders.Update(policyHolder);               
                _context.SaveChangesAsync();

                return policyHolder;
            }
            catch (Exception ex)
            {
                throw new BusinessException("Unable to update policy holder");
            }
        }

        public async Task DeleteAsync(Guid policyHolderUUID)
        {
            try
            {
                var policyHolder =
                    await _context.PolicyHolders.SingleOrDefaultAsync(reason => reason.UUID == policyHolderUUID);
                if (policyHolder == null)
                {
                    _logger.LogWarning("Cannot delete policy {policyHolderUUID} entity does not exist", policyHolderUUID);
                    return;
                }

                policyHolder.Active = false;
                _context.PolicyHolders.Update(policyHolder);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new BusinessException("Unable to delete policy holder");
            }
        }
    }
}
