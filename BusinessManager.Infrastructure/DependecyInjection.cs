
using BusinessManager.Domain.Interfaces.HR.Employee;
using BusinessManager.Infrastructure.Repository.HR.Employee;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Mail;
using System.Net;

namespace BusinessManager.Infrastructure
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection service)
        {
            service.AddTransient<IEmployeeRepository, EmployeeRepository>();
            service.AddTransient<ILeaveRequestRepository, LeaveRequestRepository>();
            service.AddTransient<IMedicalLeaveRepository, MedicalLeaveRepository>();
            return service;
        }
    }
}