using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using EmployeeService.DTO;

namespace EmployeeService.DAL
{
    public class EmployeeRepository
    {
        private string _connString = ConfigurationManager.ConnectionStrings["DbConn"].ConnectionString;

        public List<EmployeeDto> GetAllEmployees()
        {
            var list = new List<EmployeeDto>();
            using (var conn = new SqlConnection(_connString))
            {
                var cmd = new SqlCommand("SELECT ID, Name, ManagerID, Enable FROM Employee", conn);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new EmployeeDto {
                            ID = (int)reader["ID"],
                            Name = reader["Name"].ToString(),
                            ManagerID = reader["ManagerID"] as int?,
                            Enable = (bool)reader["Enable"]
                        });
                    }
                }
            }
            return list;
        }

        public async Task<bool> UpdateEnableStatusAsync(int id, bool enable)
        {
            using (var conn = new SqlConnection(_connString))
            {
                const string query = "UPDATE Employee SET Enable = @enable WHERE ID = @id";
        
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add("@enable", System.Data.SqlDbType.Bit).Value = enable;
                    cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;

                    await conn.OpenAsync();
                    
                    int rowsAffected = await cmd.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }    }
}