using IdleMachinery.Extensions.Standard.Stubs.Domain;
using Microsoft.AspNetCore.Mvc;

namespace IdleMachinery.Extensions.Core.Stubs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerData customerData;

        public CustomersController(ICustomerData customerData)
        {
            this.customerData = customerData;
        }

        [HttpGet]
        public ActionResult GetCustomers()
        {
            var customerEntities = customerData.GetCustomersByName();
            return Ok(customerEntities);
        }

        [HttpGet]
        [Route("{id}", Name = "GetCustomer")]
        public IActionResult GetCustomer(int id)
        {
            var customerEntity = customerData.GetById(id);
            if (customerEntity == null)
            {
                return NotFound();
            }          

            return Ok(customerEntity);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Customer customer)
        {
            customerData.Add(customer);
            customerData.Commit();

            // This does the same as our responsemessage extension
            return CreatedAtRoute("GetCustomer", 
                new { id = customer.Id }, 
                customer);
        }
    }
}