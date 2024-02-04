using BusinessManager.Domain.Models.HR.Employee.Work.ScheduleWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Domain.Interfaces.HR.Employee
{
    public interface IMedicalLeaveRepository
    {
        Task<int> AddMedicalLeaveAsync(MedicalLeave newMedicalLeave);
        Task DeleteMedicalleavesAsync(MedicalLeave medicalLeave);
        IQueryable<MedicalLeave>FindByConditonAsync(Expression<Func<MedicalLeave, bool>> conditonExpression);
        Task<MedicalLeave>GetMedicalLeaveByIdAsync(int medicalLeaveId);
        IQueryable<MedicalLeave> GetAllMedicalLeaves();
        Task UpdateMedicalLeavesAsync(MedicalLeave updatedMedicalLeaves);

    }
}
