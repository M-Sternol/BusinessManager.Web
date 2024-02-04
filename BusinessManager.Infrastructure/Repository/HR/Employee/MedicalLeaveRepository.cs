using BusinessManager.Domain.Interfaces.HR.Employee;
using BusinessManager.Domain.Models.HR.Employee.Work.ScheduleWork;
using BusinessManager.Infrastructure.DbContext.HR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Infrastructure.Repository.HR.Employee
{
    public class MedicalLeaveRepository : IMedicalLeaveRepository
    {
        private readonly Context _context;
        public MedicalLeaveRepository(Context context) 
        {  
            _context = context;
        }
        public async Task<int> AddMedicalLeaveAsync(MedicalLeave newMedicalLeave)
        {
            try
            {
                _context.MedicalLeaves.Add(newMedicalLeave);
                await _context.SaveChangesAsync();
                return newMedicalLeave.Id;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Błąd dodawania zwolnień lekarskich do bazy danych.", ex);
            }
        }

        public async Task DeleteMedicalleavesAsync(MedicalLeave medicalLeave)
        {
            try
            {
                _context.MedicalLeaves.Remove(medicalLeave);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Błąd usuwania zwolnienia lekarskiego z bazy danych.", ex);
            }
        }

        public IQueryable<MedicalLeave> FindByConditonAsync(Expression<Func<MedicalLeave, bool>> conditonExpression)
        {
            return _context.MedicalLeaves.Where(conditonExpression);
        }

        public IQueryable<MedicalLeave> GetAllMedicalLeaves()
        {
            try
            {
                return _context.MedicalLeaves.AsQueryable();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Błąd przy pobieraniu listy zwolnień lekarskich z bazy danych.", ex);
            }
        }

        public async Task<MedicalLeave> GetMedicalLeaveByIdAsync(int medicalLeaveId)
        {
            try
            {
                var medicalLeaves = await _context.MedicalLeaves.FirstOrDefaultAsync(e => e.EmployeeId == medicalLeaveId || e.Id == medicalLeaveId);
                if (medicalLeaves == null)
                {
                    throw new ApplicationException($"Nie znaleziono zwolnienia lekarskiego dla ID {medicalLeaveId}.");
                }
                return medicalLeaves;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Błąd podczas pobierania zwolnienia lekarskiego z bazy danych.", ex );
            }
        }

        public async Task UpdateMedicalLeavesAsync(MedicalLeave updatedMedicalLeaves)
        {
            try
            {
                _context.MedicalLeaves.Attach(updatedMedicalLeaves);
                _context.Entry(updatedMedicalLeaves).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Błąd podczas aktualizacji zwolnienia lekarskiego.", ex);
            }
        }
    }
}
