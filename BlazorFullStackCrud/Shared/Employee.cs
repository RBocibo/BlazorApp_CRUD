using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorFullStackCrud.Shared
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty ;
        public string position { get; set; } = string.Empty;
        public Department? Department { get; set; }
        public int DepartmentId { get; set; }
    }
}
