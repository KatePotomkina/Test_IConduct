using System.Collections.Generic;

namespace EmployeeService.DTO
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Enable { get; set; }

        public List<EmployeeDto> Employees { get; set; } = new();
    }
}