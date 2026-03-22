using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using EmployeeService.DAL;
using EmployeeService.DTO;
using Newtonsoft.Json;

namespace EmployeeService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IEmployeeService
    {
        private static readonly EmployeeRepository _repository = new EmployeeRepository();
        
        public EmployeeDto GetEmployeeById(int id)
        {
            try
            {
                var allEmployees = _repository.GetAllEmployees();
                var employeeDict = allEmployees.ToDictionary(e => e.ID);
                
                foreach (var emp in allEmployees)
                {
                    if (emp.ManagerID.HasValue && employeeDict.ContainsKey(emp.ManagerID.Value))
                    {
                        employeeDict[emp.ManagerID.Value].Subordinates.Add(emp);
                    }
                }
                if (!employeeDict.ContainsKey(id))
                {
                    throw new WebFaultException<string>("Employee not found", HttpStatusCode.NotFound);
                }

                return employeeDict[id];
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public Task<bool> UpdateEnableStatusAsync(int id, bool enable)
        {
            try
            {
              return _repository.UpdateEnableStatusAsync(id, enable);
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }

      
}