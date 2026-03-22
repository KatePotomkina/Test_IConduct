using System.Collections.Generic;
using System.Runtime.Serialization;

namespace EmployeeService.DTO
{
    [DataContract]
    public class EmployeeDto
    {
        [DataMember] public int ID { get; set; }

        [DataMember] public string Name { get; set; }

        [DataMember] public bool Enable { get; set; }

        [DataMember] public int? ManagerID { get; set; }

        [DataMember] public List<EmployeeDto> Subordinates { get; set; }
    }
}