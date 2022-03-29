using BlazorFullStackCrud.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorFullStackCrud.Client.Services.EmployeeService
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        public EmployeeService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }

        public List<Employee> Employees { get; set; } = new List<Employee>();
        public List<Department> Departments { get; set; } = new List<Department>();

        public async Task CreateEmployee(Employee emp)
        {
            var result = await _http.PostAsJsonAsync("api/employees", emp);
            await SetEmployee(result);
        }

        private async Task SetEmployee(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<List<Employee>>();
            Employees = response;
            _navigationManager.NavigateTo("employees");
        }

        public async Task DeleteEmployee(int id)
        {
            var result = await _http.DeleteAsync($"api/employees/{id}");
            await SetEmployee(result);
        }

        public async Task GetDepartments()
        {
            var result = await _http.GetFromJsonAsync<List<Department>>("api/employees/departments");
            if (result != null)
                Departments = result;
        }

        public async Task<Employee> GetSingleEmployee(int id)
        {
            var result = await _http.GetFromJsonAsync<Employee>($"api/employees/{id}");
            if (result != null)
                return result;
            throw new Exception("Employee not found!");
        }

        public async Task GetEmployees()
        {
            var result = await _http.GetFromJsonAsync<List<Employee>>("api/employees");
            if (result != null)
                Employees = result;
        }

        public async Task UpdateEmployee(Employee emp)
        {
            var result = await _http.PutAsJsonAsync($"api/employees/{emp.Id}", emp);
            await SetEmployee(result);
        }
    }
}
