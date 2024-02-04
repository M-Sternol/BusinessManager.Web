using AutoMapper;
using BusinessManager.Application.Interfaces.HR.Employee;
using BusinessManager.Application.ViewModel.HR.Employee.Work.ScheduleWork.MedicalLeave;
using BusinessManager.Domain.Interfaces.HR.Employee;
using BusinessManager.Domain.Models.HR.Employee.Work.ScheduleWork;
using Microsoft.EntityFrameworkCore;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Application.Services.HR.Employee
{
    public class MedicalLeaveService : IMedicalLeaveService
    {
        private readonly IMedicalLeaveRepository _medicalLeaveRepository;
        private readonly IMapper _mapper;
        public MedicalLeaveService(IMedicalLeaveRepository medicalLeaveRepository, IMapper mapper)
        {
            _medicalLeaveRepository = medicalLeaveRepository ?? throw new ArgumentException(nameof(medicalLeaveRepository));
            _mapper = mapper;
        }

        public async Task<int> CreateMedicalLeaveAsync(MedicalLeaveViewModel medicalLeaveViewModel)
        {
            try
            {
                var newMedicalLeave = _mapper.Map<MedicalLeave>(medicalLeaveViewModel);
                var medicalLeave = await _medicalLeaveRepository.AddMedicalLeaveAsync(newMedicalLeave);
                return medicalLeave;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Błąd podczas tworzenia zwolnienia lekarskiego.", ex);
            }
        }

        public async Task<IEnumerable<MedicalLeaveViewModel>> FindOverlappingMedicalLeaves(int employeeId, DateTime start, DateTime end)
        {
            var overlapping = await _medicalLeaveRepository.FindByConditonAsync(ml =>  ml.EmployeeId == employeeId
                                                                                       && ml.StartDate < end
                                                                                       && ml.EndDate > start).ToListAsync();
            var medicalViewModel = overlapping.Select(ml => new MedicalLeaveViewModel
            {
                Id = ml.Id,
                EmployeeId = ml.EmployeeId,
                FirstName = ml.FirstName,
                LastName = ml.LastName,
                StartDate = ml.StartDate,
                EndDate = ml.EndDate,
            }).ToList();
            return medicalViewModel;
        }

        public async Task<bool> DeleteMedicalLeaveAsync(int medicalLeaveId)
        {
            try
            {
                var medialLeave = await _medicalLeaveRepository.GetMedicalLeaveByIdAsync(medicalLeaveId);
                if (medialLeave == null)
                {
                    return false;
                }
                await _medicalLeaveRepository.DeleteMedicalleavesAsync(medialLeave);
                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Błąd podczas usuwania zwolnienia lekarskiego.", ex);
            }
        }

        public async Task<MedicalLeaveViewModel> GetMedicalLeaveByIdAsync(int medicalLeaveId)
        {
            try
            {
                var medicalLeave = await _medicalLeaveRepository.GetMedicalLeaveByIdAsync(medicalLeaveId);
                return medicalLeave != null ? _mapper.Map<MedicalLeaveViewModel>(medicalLeave) : null;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Błąd pobierania zwolnienia lekarskiego.", ex);
            }
        }

        public async Task<IPagedList<MedicalLeaveForListViewModel>> GetMedicalLeavesListAsync(int pageNumber, int pageSize, string searchString, string sortOrder)
        {
            try
            {
                var query = _medicalLeaveRepository.GetAllMedicalLeaves();
                if (!string.IsNullOrWhiteSpace(searchString))
                {
                    var searchStringLower = searchString.ToLower();
                    query = query.Where(e => (e.Employee.FirstName + " " + e.Employee.LastName).ToLower().Contains(searchStringLower));
                }
                sortOrder = string.IsNullOrEmpty(sortOrder) ? "name" : sortOrder;
                query = sortOrder switch
                {
                    "name_desc" => query.OrderByDescending(e => e.Employee.FirstName).ThenByDescending(e => e.Employee.LastName),
                    "startdate_desc" => query.OrderByDescending(e => e.StartDate),
                    "startdate" => query.OrderBy(e => e.StartDate),
                    "enddate_desc" => query.OrderByDescending(e => e.EndDate),
                    "enddate" => query.OrderBy(e => e.EndDate),
                    _ => query.OrderBy(e => e.Employee.FirstName).ThenBy(e => e.Employee.LastName),
                };
                var totalMedicalLeaves = await query.CountAsync();
                var medicalLeave = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

                var medicalLeavesViewModels = medicalLeave.Select(ml => new MedicalLeaveForListViewModel
                {
                    Id = ml.Id,
                    FirstName = ml.FirstName,
                    LastName = ml.LastName,
                    StartDate = ml.StartDate,
                    EndDate = ml.EndDate,
                }).ToList();
                return new StaticPagedList<MedicalLeaveForListViewModel>(medicalLeavesViewModels, pageNumber, pageSize, totalMedicalLeaves);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Błąd podczas pobierania listy Zwolnień lekarskich.", ex);
            }
        }
        public async Task<MedicalLeaveViewModel> GetMedicalLeaveForEdit(int medicalLeaveId)
        {
            try
            {
                var medicalLeave = await _medicalLeaveRepository.GetMedicalLeaveByIdAsync(medicalLeaveId);
                return medicalLeave != null ? _mapper.Map<MedicalLeaveViewModel>(medicalLeave) : null;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Bład pobierania zwolnienia lekarskiego do Edycji.", ex);
            }
        }
        public async Task UpdateMedicalLeaveAsync(MedicalLeaveViewModel medicalLeaveViewModel)
        {
            try
            {
                var medicalLeave = _mapper.Map<MedicalLeave>(medicalLeaveViewModel);
                await _medicalLeaveRepository.UpdateMedicalLeavesAsync(medicalLeave);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Błąd aktualizacji Medical Leave o ID {medicalLeaveViewModel.Id}.", ex);
            };
        }
    }
}
