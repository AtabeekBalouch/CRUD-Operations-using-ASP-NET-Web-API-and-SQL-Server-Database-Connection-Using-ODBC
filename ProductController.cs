using CRUDAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CRUDAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly string connectionstring;
        public ProductController(IConfiguration configuration)
        {
            connectionstring = configuration["ConnectionStrings:SqlServerDB"] ?? "";
        }
        //Action Method to get the data from the Client
        [HttpPost]
        public IActionResult CreateProduct(ProductDto productDto)
        {
            try
            {
                using (var connection = new SqlConnection(connectionstring))
                {
                    using (var command = new SqlCommand("InsertProcedure", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;



                        command.Parameters.AddWithValue("@Name", productDto.Name);
                        command.Parameters.AddWithValue("@Brand", productDto.Brand);
                        command.Parameters.AddWithValue("@Category", productDto.Category);
                        command.Parameters.AddWithValue("@Price", productDto.Price);
                        command.Parameters.AddWithValue("@Description", productDto.Description);

                        connection.Open();

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Product", "Sorry, But we have an Exception");
                return BadRequest(ModelState); 
            }
            return Created("", productDto);
        }

        //Action Method to Read the Data from the DataBase
        [HttpGet]
        public IActionResult GetProduct()
        {
            List<Products> products = new List<Products>();
            try
            {
                using (var connection = new SqlConnection(connectionstring))
                {
                    using (var command = new SqlCommand("GetAll_Product",connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Products Product  = new Products();

                                Product.Id = reader.GetInt32(0);
                                Product.Name = reader.GetString(1);
                                Product.Brand = reader.GetString(2);
                                Product.Category = reader.GetString(3);
                                Product.Price = reader.GetDecimal(4);
                                Product.CreatedAt = reader.GetDateTime(5);

                                products.Add (Product);
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("Product", "Sorry, but we have an exception");
                return BadRequest(ModelState);
            }
            return Ok(products);
        }
    }
}
