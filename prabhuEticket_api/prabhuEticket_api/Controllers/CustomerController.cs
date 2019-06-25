using prabhuEticket_api.Models;
using prabhuEticket_api.Repository;
using System.Web.Http;

namespace prabhuEticket_api.Controllers
{
    [RoutePrefix("api/v1")]
    public class CustomerController : ApiController
    {
        CustomerRepo repo = new CustomerRepo();
        [Route("company/{agent_code}/customers"),HttpGet]
        public CustomerProfile_output GetCustomer([FromUri] string agent_code)
        {
            CustomerProfile_output output = repo.getcustomer(company_id: agent_code);
            return output;
        }
        [Route("company/{agent_code}/customers/{customer_id}"),HttpGet]
        public CustomerProfile_output GetCustomer([FromUri] string agent_code,[FromUri]string customer_id)
        {
            CustomerProfile_output output = repo.getcustomer(company_id: agent_code,customer_id:customer_id);
            return output;
        }
        [Route("company/{agent_code}/customers/"),HttpPost]
        public CustomerProfile_output GetCustomer([FromUri] string agent_code, [FromBody]CustomerProfile_request item)
        {
            CustomerProfile_output output = repo.getcustomer(company_id: agent_code, card_number: item.card_number);
            return output;
        }
        [Route("company/{agent_code}/customers/"), HttpPost]
        public CustomerProfile_output registerCustomer([FromUri] string agent_code, [FromBody]customer_register item)
        {
            CustomerProfile_output output = repo.customer_register(agent_code: agent_code, item:item);
            return output;
        }
        [Route("company/{agent_code}/customers/{customer_id}"), HttpPut]
        public CustomerProfile_output updateCustomer([FromUri] string agent_code,[FromUri]string customer_id, [FromBody]CustomerProfile_edit_request item)
        {
            CustomerProfile_output output = repo.customer_update(agent_code: agent_code, item: item,customer_id:customer_id);
            return output;
        }
        [Route("company/{agent_code}/customers/{customer_id}"), HttpDelete]
        public returnMain deleteCustomer([FromUri] string agent_code, [FromUri]string customer_id,[FromBody]customer_delete item)
        {
            returnMain output = repo.customer_delete(agent_code: agent_code, customer_id: customer_id,item:item);
            return output;
        }
    }
}
