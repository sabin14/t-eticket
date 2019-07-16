using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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
        CustomerRepo _repo = new CustomerRepo();
        [System.Web.Mvc.HttpGet]
        public System.Web.Mvc.ActionResult Index()
        {
            return View();
        }
        [System.Web.Mvc.HttpPost]
        public System.Web.Mvc.JsonResult Index(CustomerProfile_request request)
        {
            CustomerProfile_output output = new CustomerProfile_output();
            if (ModelState.IsValid)
            {
                CustomerRepo _repo = new CustomerRepo();
                output = _repo.getcustomer(card_number: request.card_number);
                if (output.status_code == 200)
                {
                    ViewBag.detail = output.data[0];
                }

                return Json(output);
            }
            return Json(request);
        }

        public System.Web.Mvc.ActionResult CreateCustomer()
        {
            return View();
        }

        [System.Web.Mvc.HttpPost]
        public System.Web.Mvc.ActionResult CreateCustomer(customer_register customer, HttpPostedFileBase pic_path)
        {
            if (ModelState.IsValid)
            {
                CustomerProfile_output output = new CustomerProfile_output();
                CustomerRepo _repo = new CustomerRepo();
                if (pic_path != null && pic_path.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(pic_path.FileName);
                    var path = Path.Combine(Server.MapPath("~/Image/"), fileName);
                    pic_path.SaveAs(path);
                    customer.pic_path = "Image/" + pic_path.FileName;
                }
                customer.create_by = Session["user"].ToString();
                output = _repo.customer_register(customer);
                ModelState.Clear();
                if (output.status_code != 200 || output.error_Lists.Count > 0)
                {
                    foreach (error_list error in output.error_Lists)
                    {
                        if (String.IsNullOrEmpty(error.error_field) == false)
                        {
                            ModelState.AddModelError(error.error_field, error.error_message);
                            ViewBag.validationSummary = output.message;
                        }
                        else
                        {
                            ModelState.AddModelError("validationSummary", error.error_message);
                            ViewBag.validationSummary = output.message;
                        }
                    }

                }
                else
                {
                    return RedirectToAction("Index");
                }
            }

            return View(customer);

        }
        [System.Web.Mvc.HttpGet,System.Web.Mvc.Route("Customer/UpdateCustomer/{customer_id}")]
        public System.Web.Mvc.ActionResult UpdateCustomer(int customer_id)
        {
            CustomerProfile_output output = new CustomerProfile_output();
            internal_CustomerProfile_edit_request edit_Request = new internal_CustomerProfile_edit_request();
            output = _repo.getcustomer(customer_id: customer_id.ToString());
            if (output.data.Count > 0)
            {
                edit_Request.customer_id = output.data[0].id.ToString();
                edit_Request.companyID = output.data[0].companyID;
                edit_Request.first_name = output.data[0].firstName;
                edit_Request.middle_name = output.data[0].middleName;
                edit_Request.last_name = output.data[0].lastName;
                edit_Request.customer_type = output.data[0].card_detail.type;
                edit_Request.doc_path = output.data[0].doc_path;
                edit_Request.doc_type = output.data[0].doc_type;
                edit_Request.email_address = output.data[0].email;
                edit_Request.mobile_no = output.data[0].mobile;
                edit_Request.phone_no = output.data[0].phone;
                edit_Request.pic_path = output.data[0].imagePath;
                edit_Request.qr_code = output.data[0].card_detail.qr_code;
            }
            return View(edit_Request);
        }

        [System.Web.Mvc.HttpPost, System.Web.Mvc.Route("Customer/UpdateCustomer/{customer_id}")]
        public System.Web.Mvc.ActionResult UpdateCustomer(internal_CustomerProfile_edit_request customer, HttpPostedFileBase pic_path,string customer_id)
        {
            if (ModelState.IsValid)
            {
                CustomerProfile_output output = new CustomerProfile_output();
                CustomerRepo _repo = new CustomerRepo();
                if (pic_path != null && pic_path.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(pic_path.FileName);
                    var path = Path.Combine(Server.MapPath("~/Image/"), fileName);
                    pic_path.SaveAs(path);
                    customer.pic_path = "Image/" + pic_path.FileName;
                }
                CustomerProfile_edit_request req = new CustomerProfile_edit_request();
                if (String.IsNullOrEmpty(customer.customer_id) == false)
                {
                    req = globFunction.Cast<CustomerProfile_edit_request>(customer);
                }
                req.update_by = Session["user"].ToString();
                output = _repo.customer_update(req,customer_id:customer_id);
                ModelState.Clear();
                if (output.status_code != 200 || output.error_Lists.Count > 0)
                {
                    foreach (error_list error in output.error_Lists)
                    {
                        if (String.IsNullOrEmpty(error.error_field) == false)
                        {
                            ModelState.AddModelError(error.error_field, error.error_message);
                            ViewBag.validationSummary = output.message;
                        }
                        else
                        {
                            ModelState.AddModelError("validationSummary", error.error_message);
                            ViewBag.validationSummary = output.message;
                        }
                    }

                }
                else
                {
                    return RedirectToAction("Index");
                }
            }

            return View(customer);

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
        public CustomerProfile_output GetCustomer([FromUri]string customer_id)
        {
            CustomerProfile_output output = repo.getcustomer(customer_id: customer_id);
            return output;
        }
        [Route("customers"), HttpPost]
        public CustomerProfile_output GetCustomer([FromBody]CustomerProfile_request item)
        {
            CustomerProfile_output output = repo.getcustomer(card_number: item.card_number);
            return output;
        }
        [Route("customers/"), HttpPost, ApiExplorerSettings(IgnoreApi = true), NonAction]
        public CustomerProfile_output registerCustomer([FromBody]customer_register item)
        {
            CustomerProfile_output output = repo.customer_register(item: item);
            return output;
        }
        [Route("customers/{customer_id}"), HttpPut]
        public CustomerProfile_output updateCustomer([FromUri]string customer_id, [FromBody]CustomerProfile_edit_request item)
        {
            CustomerProfile_output output = repo.customer_update(item: item, customer_id: customer_id);
            return output;
        }
        [Route("customers/{customer_id}"), HttpDelete]
        public returnMain deleteCustomer([FromUri]string customer_id, [FromBody]customer_delete item)
        {
            returnMain output = repo.customer_delete(customer_id: customer_id, item: item);
            return output;
        }
    }
}