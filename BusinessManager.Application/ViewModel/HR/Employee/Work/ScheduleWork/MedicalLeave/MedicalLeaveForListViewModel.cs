using AutoMapper;
using BusinessManager.Application.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Application.ViewModel.HR.Employee.Work.ScheduleWork.MedicalLeave
{
    public class MedicalLeaveForListViewModel : IMapFrom<Domain.Models.HR.Employee.Work.ScheduleWork.MedicalLeave>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public void Mapper(Profile profile)
        {
            profile.CreateMap<Domain.Models.HR.Employee.Work.ScheduleWork.MedicalLeave, MedicalLeaveViewModel>();
        }
    }
}
