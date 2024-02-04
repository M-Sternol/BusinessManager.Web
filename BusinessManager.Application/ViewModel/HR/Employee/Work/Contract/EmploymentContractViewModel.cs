using AutoMapper;
using BusinessManager.Application.Mapper;
using BusinessManager.Application.ViewModel.HR.Employee.Work.ScheduleWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Application.ViewModel.HR.Employee.Work.Contract
{
    public class EmploymentContractViewModel : IMapFrom<Domain.Models.HR.Employee.Work.Contract.EmploymentContract>
    {
        public int Id { get; set; }
        public ContractType Type { get; set; }
        public int DurationMonths { get; set; }
        public string Position { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal MonthlyRate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Models.HR.Employee.Work.Contract.EmploymentContract, EmploymentContractViewModel>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(scr => (Domain.Models.HR.Employee.Work.Contract.ContractType)scr.Type))
                .ReverseMap();

        }
    }

    public enum ContractType
    {
        EmploymentContract,
        ContractforSpecificTask,
        ContractWork,
    }
}
