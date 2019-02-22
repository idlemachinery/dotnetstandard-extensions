using IdleMachinery.Extensions.Framework.Stubs.Domain;
using IdleMachinery.Extensions.Standard.Stubs.Domain;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IdleMachinery.Extensions.Framework.Stubs.Api.Controllers
{
    public class CustomersController : ApiController
    {
        // GET api/customers
        public Customer Get(int id)
        {
            Customer customer = null;
            using (var db = new DomainDbContext())
            {
                db.Configuration.LazyLoadingEnabled = false;
                customer = db.Customers.Find(id);
            }
            if (customer == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return customer;
        }

        // POST api/customers
        public HttpResponseMessage Post([FromBody]Customer customer)
        {
            using (var db = new DomainDbContext())
            {
                db.Customers.Add(customer);
                db.Save();
            }
            var response = new HttpResponseMessage(HttpStatusCode.Created);
            // Using the extension method to set Location header
            response.AddLocationHeader(Request, customer.Id);
            return response;
        }
    }
}
