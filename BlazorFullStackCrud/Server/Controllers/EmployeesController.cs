
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorFullStackCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly DataContext _context;

        public EmployeesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetEmployees()
        {
            var emp = await _context.Employees
                .Include(sh => sh.Department).ToListAsync();
            return Ok(emp);
        }

        [HttpGet("departments")]
        public async Task<ActionResult<List<Department>>> GetDepartments()
        {
            var dep = await _context.Departments.ToListAsync();
            return Ok(dep);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetSingleEmployee(int id)
        {
            var emp = await _context.Employees
                .Include(h => h.Department)
                .FirstOrDefaultAsync(h => h.Id == id);
            if (emp == null)
            {
                return NotFound("Sorry, no employee here. :/");
            }
            return Ok(emp);
        }

        [HttpPost]
        public async Task<ActionResult<List<Employee>>> CreateEmployee(Employee Employee)
        {
            Employee.Department = null;
            _context.Employees.Add(Employee);
            await _context.SaveChangesAsync();

            return Ok(await GetDbEmployees());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Employee>>> UpdateEmployee(Employee employee, int id)
        {
            var dbEmployee = await _context.Employees
                .Include(sh => sh.Department)
                .FirstOrDefaultAsync(sh => sh.Id == id);
            if (dbEmployee == null)
                return NotFound("Sorry, but no employee for you. :/");

            dbEmployee.FirstName = employee.FirstName;
            dbEmployee.LastName = employee.LastName;
            dbEmployee.position = employee.position;
            dbEmployee.DepartmentId = employee.DepartmentId;

            await _context.SaveChangesAsync();

            return Ok(await GetDbEmployees());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Employee>>> DeleteEmployee(int id)
        {
            var dbEmp = await _context.Employees
                .Include(sh => sh.Department)
                .FirstOrDefaultAsync(sh => sh.Id == id);
            if (dbEmp == null)
                return NotFound("Sorry, but no employee for you. :/");

            _context.Employees.Remove(dbEmp);
            await _context.SaveChangesAsync();

            return Ok(await GetDbEmployees());
        }

        private async Task<List<Employee>> GetDbEmployees()
        {
            return await _context.Employees
                .Include(sh => sh.Department).ToListAsync();
        }
    }
}
