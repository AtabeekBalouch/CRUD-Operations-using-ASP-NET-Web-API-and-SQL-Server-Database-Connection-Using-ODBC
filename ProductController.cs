using CRUDAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace CRUDAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly string connectionString;
        public EmployeeController(IConfiguration configuration)
        {
            connectionString = configuration["ConnectionStrings: SqlServerDb"] ?? "";
        }

        /*This method handles POST requests. This Action Method Allow us to get a data from a client to store in database
         * we get data from client
        */
        [HttpPost("CreateEmployee")]
        public IActionResult CreateEmployee(EmployeeDto employeeDto)
        {
            try
            {
                //Object Created represents the connection to your database
                using (var connection = new SqlConnection(connectionString))

                //Object is created for represents the SQL query or stored procedure
                using (var command = new SqlCommand("InsertEmployee", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    connection.Open();

                    command.Parameters.AddWithValue("@Employee_Name", employeeDto.Employee_Name);
                    command.Parameters.AddWithValue("@Employee_Email", employeeDto.Employee_Email);
                    command.Parameters.AddWithValue("@Employee_Salary", employeeDto.Employee_Salary);

                    command.ExecuteNonQuery();
                }
            }

            catch (Exception)
            {
                ModelState.AddModelError("Employee", "Sorry, but we have an Exception.");
                return BadRequest(ModelState);
            }

            return Ok();
        }

        /*This method handles GET requests. This action method allow us to Read the Whole data from the Data base,
         * so the client can request to read it.
        */
        [HttpGet("GetAllEMployee")]
        public IActionResult GetEmployeeDetail()
        {
            //List is Created to store
            List<Employee> EMPLOYEE = new List<Employee>();

            try
            {
                //Object Created represents the connection to your database
                using (var connection = new SqlConnection(connectionString))

                //Object is created for represents the SQL query or stored procedure
                using (var command = new SqlCommand("GetAllEmployee", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //Object is created in Employee foem
                            Employee employee = new Employee();

                            employee.Id = reader.GetInt32(0);
                            employee.Employee_Name = reader.GetString(1);
                            employee.Employee_Email = reader.GetString(2);
                            employee.Employee_Salary = reader.GetDecimal(3);

                            EMPLOYEE.Add(employee);
                        }
                    }
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("Employee", "Sorry, but we have an Exception.");
                return BadRequest(ModelState);
            }

            return Ok(EMPLOYEE);

        }
        /*This method handles GET requests. This Action method allow us to Read the data by id
         * So the Client can request to read it by ID*/
        [HttpGet("{id}")]
        public IActionResult GetEmployeeByID(int id)
        {
            //CLass is created
            Employee employee = new Employee();

            try
            {
                //Object Created represents the connection to your database
                using (var connection = new SqlConnection(connectionString))

                //Object Created represents the SQL query or stored procedure
                using (var command = new SqlCommand("GetAllEmployeeByID", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            employee.Id = reader.GetInt32(0);
                            employee.Employee_Name = reader.GetString(1);
                            employee.Employee_Email = reader.GetString(2);
                            employee.Employee_Salary = reader.GetDecimal(3);
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("Employee", "Sorry, but we have an Exception.");
                return BadRequest(ModelState);
            }

            return Ok(employee);
        }

        /*
         * This method handles PUT requests. This method allow us to update the product
         * Update the data from the Client, Client can update the data from there side.
        */
        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, EmployeeDto employeeDto)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand("UpdateEmployee", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    connection.Open();

                    command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@Employee_Name", employeeDto.Employee_Name);
                    command.Parameters.AddWithValue("@Employee_Email", employeeDto.Employee_Email);
                    command.Parameters.AddWithValue("@Employee_Salary", employeeDto.Employee_Salary);

                    command.ExecuteNonQuery();
                }
            }

            catch (Exception)
            {
                ModelState.AddModelError("Employee", "Sorry, but we have an Exception.");
                return BadRequest(ModelState);
            }

            return Ok();
        }

        //This method handles DELETE requests
        //Action Method to Allows us to Delete a product
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand("DeleteEMployee",connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    connection.Open();

                    command.Parameters.AddWithValue("id",id);
                    command.ExecuteNonQuery();
                }
            }

            catch (Exception)
            {
                ModelState.AddModelError("Employee", "Sorry, but we have an Exception.");
                return BadRequest(ModelState);
            }

            return Ok();
        }
    }
}
