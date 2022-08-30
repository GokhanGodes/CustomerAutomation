using AutoMapper;
using CustomerAutomation.DTOs;
using CustomerAutomation.Models;

namespace CustomerAutomation.Mapping
{
    public class CustomMapping : Profile
    {
        public CustomMapping()
        {
            CreateMap<Customer,CustomerAddDto>().ReverseMap();
            CreateMap<CustomerGetDto,Customer>().ReverseMap();
        }
    }
}
