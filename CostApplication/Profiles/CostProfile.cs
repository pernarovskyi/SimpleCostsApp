using AutoMapper;
using CostApplication.DTO;
using CostApplication.Models;
using System;

namespace CostApplication.Profiles
{
    public class CostProfile : Profile
    {
        public CostProfile()
        {
            CreateMap<Cost, CostDto>();

            CreateMap<CostDto, Cost>()
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.SensetiveData, opt => opt.MapFrom(src => "SensitiveData"));
        }
    }
}
