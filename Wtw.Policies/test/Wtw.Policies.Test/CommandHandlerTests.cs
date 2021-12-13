using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Wtw.Policies.Application.V1.Commands;
using Wtw.Policies.Application.V1.Controllers;
using Wtw.Policies.Application.V1.Helpers;
using Wtw.Policies.Application.V1.Interfaces;
using Wtw.Policies.Application.V1.Profiles;
using Wtw.Policies.Application.V1.Queries;
using Wtw.Policies.Application.V1.Services;
using Wtw.Policies.Domain.Dtos;
using Wtw.Policies.Domain.Enums;
using Wtw.Policies.Domain.Models;
using Wtw.Policies.Infrastructure.Repositories.Interfaces;
using Xunit;

namespace Wtw.Policies.Test
{
    public class CommandHandlerTests
    {
        private readonly PolicyService _policyService;
        private readonly Mock<IPoliciesRepository> _policiesRepoMock = new Mock<IPoliciesRepository>();
        private readonly Mock<IPolicyHolderRepository> _policyHolderRepoMock = new Mock<IPolicyHolderRepository>();
        private readonly Mock<IMediator> _mediator = new Mock<IMediator>();

        private readonly Policy _policyEF;
        private readonly PolicyHolder _policyHolderEf;
        private readonly Guid policyUuid;
        private readonly Guid policyHolderUuid;
        private readonly IMapper _mapper;
        private readonly IValidationHelper _validationHelper;


        public CommandHandlerTests()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<PoliciesProfile>());
            _mapper = config.CreateMapper();

            var logger = new Mock<ILogger<PolicyService>>();
            _validationHelper = new ValidationHelper(logger.Object);
            _policyService = new PolicyService(_policyHolderRepoMock.Object, _policiesRepoMock.Object, _mapper, logger.Object, _validationHelper, _mediator.Object);

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
        public async Task CreatePolicyHolderCommandHandlerAsync_Test()
        {
            //arrange
            _policyHolderRepoMock.Setup(x => x.CreateAsync(It.IsAny<PolicyHolder>()))
                .ReturnsAsync(_policyHolderEf);

            var handler = new CreatePolicyHolderCommandHandler(_policyHolderRepoMock.Object);

            var command = new CreatePolicyHolderCommand
            {
                Name = _policyHolderEf.Name,
                Age = _policyHolderEf.Age,
                GenderType = _policyHolderEf.Gender
            };

            //Act
            var response = await handler.Handle(command, new CancellationToken());

            //Assert
            Assert.NotEqual(response.Uuid, Guid.Empty);
            Assert.Equal(response.Uuid, _policyHolderEf.UUID);
        }

        [Fact]
        public async Task CreatePolicyCommandHandlerAsync_Test()
        {
            //arrange
            _policiesRepoMock.Setup(x => x.CreateAsync(It.IsAny<Policy>()))
                .ReturnsAsync(_policyEF.UUID);

            var handler = new CreatePolicyCommandHandler(_policiesRepoMock.Object);

            var policyHolder = new PolicyHolderResponse
            {
                Name = _policyHolderEf.Name,
                Age = _policyHolderEf.Age,
                GenderType = _policyHolderEf.Gender
            };

            var command = new CreatePolicyCommand
            { 
                UUID = _policyEF.UUID,
                PolicyHolder = policyHolder
            };

            //Act
            var response = await handler.Handle(command, new CancellationToken());

            //Assert
            Assert.NotEqual(response.Response, Guid.Empty);
            Assert.Equal(response.Response, _policyEF.UUID);
        }

        [Fact]
        public async Task UpdatePolicyHolderCommandHandlerAsync_Test()
        {
            //arrange
            _policyHolderRepoMock.Setup(x => x.UpdateAsync(It.IsAny<PolicyHolder>()))
                .ReturnsAsync(_policyHolderEf);

            var handler = new UpdatePolicyHolderCommandHandler(_policyHolderRepoMock.Object);

            var command = new UpdatePolicyHolderCommand
            {
                Uuid = _policyHolderEf.UUID,
                Name = _policyHolderEf.Name,
                Age = _policyHolderEf.Age,
                Gender = _policyHolderEf.Gender
            };

            //Act
            var response = await handler.Handle(command, new CancellationToken());

            //Assert
            Assert.NotEqual(response.Response, Guid.Empty);
            Assert.Equal(response.Response, _policyHolderEf.UUID);
        }

        [Fact]
        public async Task UpdatePolicyCommandHandlerAsync_Test()
        {
            //arrange
            _policiesRepoMock.Setup(x => x.UpdateAsync(It.IsAny<Policy>()))
                .ReturnsAsync(_policyEF.UUID);

            var handler = new UpdatePolicyCommandHandler(_policiesRepoMock.Object);

            var policyHolder = new UpdatePolicyHolderCommand
            {
                Uuid = _policyHolderEf.UUID,
                Name = _policyHolderEf.Name,
                Age = _policyHolderEf.Age,
                Gender = _policyHolderEf.Gender
            };

            var command = new UpdatePolicyCommand
            {
                Uuid = _policyEF.UUID,
                PolicyHolderCommand = policyHolder
            };

            //Act
            var response = await handler.Handle(command, new CancellationToken());

            //Assert
            Assert.NotEqual(response.Response, Guid.Empty);
            Assert.Equal(response.Response, _policyEF.UUID);
        }

        [Fact]
        public async Task GetPolicyCommandHandlerAsync_Test()
        {
            //Arrange
            _policyHolderRepoMock.Setup(x => x.FindByIdAsync(_policyHolderEf.UUID))
                .ReturnsAsync(_policyHolderEf);

            _policiesRepoMock.Setup(x => x.FindByIdAsync(_policyEF.UUID))
                .ReturnsAsync(_policyEF);

            var policyHandler = new GetPolicyQueryHandler(_policiesRepoMock.Object);

            var command = new GetPolicyQuery
            {
                UUID = _policyEF.UUID
            };

            var policyHolderHandler = new GetPolicyHolderQueryHandler(_policyHolderRepoMock.Object);

            var commandHolder = new GetPolicyHolderQuery
            {
                UUID = _policyHolderEf.UUID
            };

            // Act
            var response = await policyHandler.Handle(command, new CancellationToken());

            var responseHolder = await policyHolderHandler.Handle(commandHolder, new CancellationToken());

            //Assert
            Assert.NotNull(response);
            Assert.Equal(_policyEF.UUID, response.policy.Policy_UUID);
            Assert.Equal(_policyHolderEf.Name, responseHolder.policyHolder.Name);
        }

        [Fact]
        public void CreatePolicyHolderCommandValidator_Fail_Test()
        {
            var validator = new CreatePolicyHolderCommandValidator();
            var model = new CreatePolicyHolderCommand { Name = "", Age = 17, GenderType = GenderType.Male };
            var result = validator.Validate(model);
            var x = result.IsValid;
            Assert.True(!result.IsValid);
            Assert.Equal("'Name' must not be empty.", result.Errors[0].ErrorMessage.ToString());
            Assert.Equal("'Age' must be greater than '17'.", result.Errors[1].ErrorMessage.ToString());
        }
    }
}
