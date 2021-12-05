using AutoMapper;
using Wtw.Policies.Domain.Dtos;
using Wtw.Policies.Domain.Models;

namespace Wtw.Policies.Application.BFF.Profiles
{
    public class PoliciesProfile : Profile
    {
        public PoliciesProfile()
        {
            CreateMap<ApplicationDto, PolicyHolder>();
            CreateMap<PolicyHolderDto, PolicyHolder>();
            CreateMap<PolicyDto, Policy>();
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
                    opts => opts.MapFrom(src => src.Gender))
                .ForMember(
                    dest => dest.Policies,
                    opts => opts.MapFrom(src => src.Policies));

            CreateMap<Policy, PolicyDto>()
                .ForMember(
                    dest => dest.Policy_UUID,
                    opts => opts.MapFrom(src => src.UUID)
                )
                .ForMember(
                    dest => dest.PolicyHolder,
                    opts => opts.MapFrom(src => src.PolicyHolder));
        }
    }
}
