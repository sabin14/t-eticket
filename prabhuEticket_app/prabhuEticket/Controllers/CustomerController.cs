using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
//using prabhuEticket.AppCode;
using prabhuEticket.AppCode;
using prabhuEticket.Models;
using prabhuEticket.Repository;

namespace prabhuEticket.Controllers
{
    [SessionCheck]
    public class CustomerController : System.Web.Mvc.Controller
    {
        // GET: Customer
        public System.Web.Mvc.ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public System.Web.Mvc.JsonResult Index(CustomerProfile_request request)
        {
            CustomerProfile_output output = new CustomerProfile_output();
            if (ModelState.IsValid)
            {
                CustomerRepo _repo = new CustomerRepo();
                output = _repo.getCustomerProfile(request, Session["token_id"].ToString());
                ViewBag.detail = output.data;
                return Json(output);
            }
            return Json(request);
        }

        public System.Web.Mvc.ActionResult CreateCustomer()
        {
            return View();
        }
    }
    [RoutePrefix("api/v1")]
    public class CustomerAPIController : ApiController
    {
        CustomerRepo repo = new CustomerRepo();
        [Route("customers"), HttpGet]
        public CustomerProfile_output GetCustomer()
        {
            CustomerProfile_output output = repo.getcustomer();
            return output;
        }
        [Route("customers/{customer_id}"), HttpGet]
        public CustomerProfile_output GetCustomer( [FromUri]string customer_id)
        {
            CustomerProfile_output output = repo.getcustomer( customer_id: customer_id);
            return output;
        }
        [Route("customers"), HttpPost]
        public CustomerProfile_output GetCustomer([FromBody]CustomerProfile_request item)
        {
            CustomerProfile_output output = repo.getcustomer( card_number: item.card_number);
            return output;
        }
        [Route("customers/"), HttpPost,ApiExplorerSettings(IgnoreApi=true),NonAction]
        public CustomerProfile_output registerCustomer( [FromBody]customer_register item)
        {
            CustomerProfile_output output = repo.customer_register(item: item);
            return output;
        }
        [Route("customers/{customer_id}"), HttpPut]
        public CustomerProfile_output updateCustomer( [FromUri]string customer_id, [FromBody]CustomerProfile_edit_request item)
        {
            CustomerProfile_output output = repo.customer_update( item: item, customer_id: customer_id);
            return output;
        }
        [Route("customers/{customer_id}"), HttpDelete]
        public returnMain deleteCustomer([FromUri]string customer_id, [FromBody]customer_delete item)
        {
            returnMain output = repo.customer_delete( customer_id: customer_id, item: item);
            return output;
        }
    }
}