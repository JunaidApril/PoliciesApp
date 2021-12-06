using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Wtw.Policies.Application.BFF.Profiles;
using Wtw.Policies.Application.BFF.Services;
using Wtw.Policies.Domain.Dtos;
using Wtw.Policies.Domain.Enums;
using Wtw.Policies.Domain.Models;
using Wtw.Policies.Infrastructure.Repositories.Interfaces;
using Xunit;

namespace Wtw.Policies.Test
{
    public class PolicyServiceTest
    {
        private readonly PolicyService _policyService;
        private readonly Mock<IPoliciesRepository> _policiesRepoMock = new Mock<IPoliciesRepository>();
        private readonly Mock<IPolicyHolderRepository> _policyHolderRepoMock = new Mock<IPolicyHolderRepository>();

        private readonly Policy _policyEF;
        private readonly PolicyHolder _policyHolderEf;
        private readonly Guid policyUuid;
        private readonly Guid policyHolderUuid;
        private readonly IMapper _mapper;

        public PolicyServiceTest()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<PoliciesProfile>());
            _mapper = config.CreateMapper();

            var logger = new Mock<ILogger<PolicyService>>();
            _policyService = new PolicyService(_policyHolderRepoMock.Object, _policiesRepoMock.Object, _mapper, logger.Object);

            //Setup guids to test with
            policyUuid = Guid.NewGuid();
            policyHolderUuid = Guid.NewGuid();

            //Store mock record in policy holder repo
            var policyHolderEf = new PolicyHolder();
            policyHolderEf.UUID = policyHolderUuid;
            policyHolderEf.Name = "John Doe";
            policyHolderEf.Age = 55;
            policyHolderEf.Gender = GenderType.Male;

            _policyHolderEf = policyHolderEf;

            //Store mock record in policies repo
            var policyEf = new Policy();
            policyEf.UUID = policyUuid;
            policyEf.PolicyHolderUUID = policyHolderEf.UUID;
            policyEf.PolicyHolder = policyHolderEf;

            _policyEF = policyEf;
        }

        [Fact]
        public async void CreatePolicyNoName_Test()
        {
            //arrange
            var applicationDto = new ApplicationDto
            {
                Name = "",
                Age = 19,
                GenderType = GenderType.Male
            };

            //act
            Task act() => _policyService.CreatePolicyAsync(applicationDto);

            //assert
            await Assert.ThrowsAsync<BusinessException>(act);          
        }

        [Fact]
        public async void CreatePolicyUnder18Age_Test()
        {
            //arrange
            var applicationDto = new ApplicationDto
            {
                Name = "Adam Barnard",
                Age = 16,
                GenderType = GenderType.Male
            };

            //act
            Task act() => _policyService.CreatePolicyAsync(applicationDto);

            //assert
            await Assert.ThrowsAsync<BusinessException>(act);
        }

        [Fact]
        public async Task GetPolicyAsync_Test()
        {
            //Arrange
            _policyHolderRepoMock.Setup(x => x.FindByIdAsync(_policyHolderEf.UUID))
                .ReturnsAsync(_policyHolderEf);

            _policiesRepoMock.Setup(x => x.FindByIdAsync(_policyEF.UUID))
                .ReturnsAsync(_policyEF);

            // Act
            var policy = await _policyService.GetPolicyAsync(_policyEF.UUID);

            //Assert
            Assert.NotNull(policy);
            Assert.Equal(_policyEF.UUID, policy.Policy_UUID);
            Assert.Equal(_policyHolderEf.Name, policy.PolicyHolder.Name);
        }

        [Fact]
        public async Task UpdatePolicyAsync_Test()
        {
            //Arrange

            //Store mock record in policy holder repo for update
            var policyHolderUpdate = new PolicyHolder();
            policyHolderUpdate.UUID = Guid.NewGuid();
            policyHolderUpdate.Name = "Jane Doe";
            policyHolderUpdate.Age = 23;
            policyHolderUpdate.Gender = GenderType.Female;

            _policyHolderRepoMock.Setup(x => x.UpdateAsync(policyHolderUpdate));

            //Store mock record in policies repo for update
            var policyUpdate = new Policy();
            policyUpdate.UUID = Guid.NewGuid();
            policyUpdate.PolicyHolder = policyHolderUpdate;

            _policiesRepoMock.Setup(x => x.UpdateAsync(policyUpdate));

            //Act
            var policyHolderDto = new PolicyHolderDto
            {
                Uuid = policyHolderUpdate.UUID,
                Name = policyHolderUpdate.Name,
                Age = policyHolderUpdate.Age,
                Gender = policyHolderUpdate.Gender
            };
            
            var policyDto = new PolicyDto
            {
                Policy_UUID = policyUpdate.UUID,
                PolicyHolder = policyHolderDto
            };

            var response = await _policyService.UpdatePolicyAsync(policyDto);

            //Assert
            Assert.Equal(response, policyUpdate.UUID);
        }

        [Fact]
        public async Task RemovePolicyAsyncEmptyGuid_Test()
        {
            //Arrange
            _policiesRepoMock.Setup(x => x.DeleteAsync(_policyEF.UUID));

            //Act
            async Task act() => await _policyService.RemovePolicyAsync(Guid.Empty);

            //assert
            await Assert.ThrowsAsync<BusinessException>(act);
        }
    }
}
