using AutoMapper;
using CostApplication.DTO;
using CostApplication.Models;


namespace CostApplication.Profiles
{
    public class CostProfile : Profile
    {
        public CostProfile()
        {
            CreateMap<Cost, CostDto>();
            CreateMap<CostDto, Cost>()
                .ForMember(c => c.Id, opt => opt.Ignore());


            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>()
                .ForMember(u => u.Id, opt => opt.Ignore());
        }
    }
}
