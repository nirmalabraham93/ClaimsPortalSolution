using AutoMapper;
using ClaimsPortal.Data.Entities;
using ClaimsPortal.Web.ViewModels;

namespace ClaimsPortal.Web.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Simple mappings where property names and types match
            CreateMap<CreatePolicyViewModel, PolicyHolder>();
            CreateMap<CreatePolicyViewModel, Policy>()
                .ForMember(dest => dest.PolicyHolderId, opt => opt.Ignore()); // Ignore this because it's set later
            CreateMap<CreatePolicyViewModel, Vehicle>()
                .ForMember(dest => dest.PolicyId, opt => opt.Ignore()); // Ignore this because it's set later
        }
    }
}
