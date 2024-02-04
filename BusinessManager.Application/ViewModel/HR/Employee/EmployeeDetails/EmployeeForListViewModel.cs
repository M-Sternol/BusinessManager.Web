using AutoMapper;
using BusinessManager.Application.Mapper;
using BusinessManager.Domain.Models.HR.Employee.EmployeeDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Application.ViewModel.HR.Employee.EmployeeDetails
{
    public class EmployeeForListViewModel : IMapFrom<EmployeeModel>
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EmployeeModel, EmployeeForListViewModel>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position));
        }
    }
}
