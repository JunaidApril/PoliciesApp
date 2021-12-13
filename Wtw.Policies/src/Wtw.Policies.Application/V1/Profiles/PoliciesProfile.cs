using AutoMapper;
using System.Collections.Generic;
using Wtw.Policies.Domain.Dtos;
using Wtw.Policies.Domain.Models;

namespace Wtw.Policies.Application.V1.Profiles
{
    public class PoliciesProfile : Profile
    {
        public PoliciesProfile()
        {
            CreateMap<ApplicationDto, PolicyHolder>();
            CreateMap<PolicyHolderDto, PolicyHolder>();
            CreateMap<PolicyDto, Policy>()
                .ForMember(
                    dest => dest.UUID,
                    opts => opts.MapFrom(src => src.Policy_UUID)
                );
            CreateMap<PolicyHolderDto, ApplicationDto>();
            CreateMap<PolicyHolder, PolicyHolderDto>()
                .ForMember(
                    dest => dest.Name,
                    opts => opts.MapFrom(src => src.Name)
                )
                .ForMember(
                    dest => dest.Age,
                    opts => opts.MapFrom(src => src.Age))
                .ForMember(
                    dest => dest.Gender,
                    opts => opts.MapFrom(src => src.Gender));

            CreateMap<Policy, PolicyDto>()
                .ForMember(
                    dest => dest.Policy_UUID,
                    opts => opts.MapFrom(src => src.UUID)
                )
                .ForMember(
                    dest => dest.PolicyHolder_UUID,
                    opts => opts.MapFrom(src => src.PolicyHolderUUID)
                )
                .ForMember(
                    dest => dest.PolicyHolder,
                    opts => opts.MapFrom(src => src.PolicyHolder));
        }
    }
}
