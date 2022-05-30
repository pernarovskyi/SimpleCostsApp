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
            CreateMap<Cost, CostDto>().ReverseMap();
            
        }
    }
}
