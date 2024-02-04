using AutoMapper;
using BusinessManager.Application.ViewModel.HR.Employee.Contact;
using BusinessManager.Application.ViewModel.HR.Employee.Education;
using BusinessManager.Application.ViewModel.HR.Employee.EmployeeDetails;
using BusinessManager.Application.ViewModel.HR.Employee.Work.Contract;
using BusinessManager.Application.ViewModel.HR.Employee.Work.ScheduleWork;
using BusinessManager.Application.ViewModel.HR.Employee.Work.ScheduleWork.LeaveRequest;
using BusinessManager.Application.ViewModel.HR.Employee.Work.ScheduleWork.MedicalLeave;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        { 
            // Dodatkowe mapowania z innych plików
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>))).ToList();

            foreach (var type in types)
            {
                try
                {
                    var instance = Activator.CreateInstance(type);
                    if (instance != null)
                    {
                        var methodInfo = type.GetMethod("Mapping");
                        methodInfo?.Invoke(instance, new object[] { this });
                    }
                    else
                    {
                        Log.Error($"Instance of type {type} is null.");
                    }
                }
                catch (Exception ex)
                {
                    Log.Error($"Error creating instance or invoking method for type {type}: {ex.Message}");
                }
            }
        }

    }
}
