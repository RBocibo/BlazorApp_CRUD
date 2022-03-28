using BlazorFullStackCrud.Shared;

namespace BlazorFullStackCrud.Client.Services.EmployeeService
{
    public interface IEmployeeService
    {
        List<Employee> Employees { get; set; }
        List<Department> Departments { get; set; }
        Task GetDepartments();
        Task GetEmployees();
        Task<Employee> GetSingleEmployee(int id);
        Task CreateEmployee(Employee emp);
        Task UpdateEmployee(Employee emp);
        Task DeleteEmployee(int id);
    }
}
