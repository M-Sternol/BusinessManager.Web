using BusinessManager.Application.ViewModel.HR.Employee.Work.ScheduleWork.MedicalLeave;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Application.Interfaces.HR.Employee
{
    public interface IMedicalLeaveService
    {
        Task <IPagedList<MedicalLeaveForListViewModel>> GetMedicalLeavesListAsync(int pageNumber, int pageSize, string searchString, string sortOrder);
        Task<MedicalLeaveViewModel> GetMedicalLeaveByIdAsync(int medicalLeaveId);
        Task<MedicalLeaveViewModel> GetMedicalLeaveForEdit(int medicalLeaveId);
        Task<int> CreateMedicalLeaveAsync(MedicalLeaveViewModel medicalLeaveViewModel);
        Task<IEnumerable<MedicalLeaveViewModel>> FindOverlappingMedicalLeaves(int employeeId, DateTime start, DateTime end);
        Task UpdateMedicalLeaveAsync(MedicalLeaveViewModel medicalLeaveViewModel);
        Task<bool> DeleteMedicalLeaveAsync(int medicalLeaveId);
    }
}
