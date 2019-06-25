using prabhuEticket_api.Models;
using prabhuEticket_api.Repository;
using System.Web.Http;


namespace prabhuEticket_api.Controllers
{
    [RoutePrefix("api/v1")]
    public class CompanyController : ApiController
    {
        CompanyRepo repo = new CompanyRepo();
        [Route("company_groups"), HttpGet]
        public company_Groups GetCompanyGroup()
        {
            company_Groups company_Groups = new company_Groups();
            company_Groups = repo.GetGroups();
            return company_Groups;
        }
        [Route("company_details"), HttpGet]
        public company_details_output GetCompanyDetails()
        {
            company_details_output output = new company_details_output();
            output = repo.company_Details_Output();
            return output;
        }
        [Route("company_details/{agent_code}"), HttpGet]
        public company_details_output GetCompanyDetails([FromUri]string agent_code)
        {
            company_details_output output = new company_details_output();
            output = repo.company_Details_Output(agent_code: agent_code);
            return output;
        }
        [Route("company_detail/{agent_code}/agents"), HttpGet]
        public company_details_output GetCompanyAgentDetails([FromUri]string agent_code)
        {
            company_details_output output = new company_details_output();
            output = repo.company_Details_Output(primary_agent_code: agent_code);
            return output;
        }
        [Route("company_detail/{agent_code}/agents/{sub_agent_id}"), HttpGet]
        public company_details_output GetCompanyAgentDetail([FromUri]string agent_code, [FromUri]string sub_agent_id)
        {
            company_details_output output = new company_details_output();
            output = repo.company_Details_Output(agent_code: sub_agent_id, primary_agent_code: agent_code);
            return output;
        }
        [Route("company_detail"), HttpPost]
        public company_details_output registerCompany([FromBody]register_company item)
        {
            company_details_output output = new company_details_output();
            output = repo.registerCompanyOrSub(item);
            return output;
        }
        [Route("company_detail/{agent_code}/agents"), HttpPost]
        public company_details_output registerCompanyAgent([FromUri]string agent_code,[FromBody]register_company item)
        {
            company_details_output output = new company_details_output();
            output = repo.registerCompanyOrSub(item,agent_code);
            return output;
        }
        [Route("company_detail/{agent_code}"), HttpPut]
        public company_details_output updateCompany([FromBody]update_company item,string agent_code)
        {
            company_details_output output = new company_details_output();
            output = repo.updateCompanyOrSub(item,agent_code);
            return output;
        }
        [Route("company_detail/{agent_code}/agents/{sub_agent_id}"), HttpPut]
        public company_details_output updateCompanyAgent([FromUri]string agent_code,[FromUri]string sub_agent_id, [FromBody]update_company item)
        {
            company_details_output output = new company_details_output();
            output = repo.updateCompanyOrSub(item, sub_agent_id,agent_code);
            return output;
        }
        [Route("company_detail/{agent_code}/update_username"), HttpPut]
        public returnMain updateCompanyUsername([FromBody]updateCompany_user_name item,[FromUri] string agent_code)
        {
            returnMain output = new returnMain();
            output = repo.updateUsername(item, agent_code);
            return output;
        }
        [Route("company_detail/{agent_code}/agents/{sub_agent_id}/update_username"), HttpPut]
        public returnMain updateCompanyAgentUsername([FromBody]updateCompany_user_name item, [FromUri] string agent_code,[FromUri]string sub_agent_id)
        {
            returnMain output = new returnMain();
            output = repo.updateUsername(item, sub_agent_id,agent_code);
            return output;
        }
        [Route("company_detail/{agent_code}/update_password"), HttpPut]
        public returnMain updateCompanyPassword([FromBody]updateCompany_password item, [FromUri] string agent_code)
        {
            returnMain output = new returnMain();
            output = repo.updatePassword(item, agent_code);
            return output;
        }
        [Route("company_detail/{agent_code}/agents/{sub_agent_id}/update_password"), HttpPut]
        public returnMain updateCompanyAgentPassword([FromBody]updateCompany_password item, [FromUri] string agent_code, [FromUri]string sub_agent_id)
        {
            returnMain output = new returnMain();
            output = repo.updatePassword(item, sub_agent_id, agent_code);
            return output;
        }
        [Route("company_detail/{agent_code}"), HttpDelete]
        public returnMain deleteCompany([FromBody]deleteCompany item,string agent_code)
        {
            returnMain output = new returnMain();
            output = repo.deleteCompany(item.user_name,agent_code);
            return output;
        }
        [Route("company_detail/{agent_code}/agents/{sub_agent_id}"), HttpDelete]
        public returnMain deleteCompanyAgent([FromBody]deleteCompany item,[FromUri]string agent_code, [FromUri]string sub_agent_id)
        {
            returnMain output = new returnMain();
            output = repo.deleteCompany(item.user_name,sub_agent_id, agent_code);
            return output;
        }
        [Route("company_detail/{agent_code}/contacts"), HttpGet]
        public company_contacts GetCompany_contacts([FromUri]string agent_code)
        {
            company_contacts contacts = new company_contacts();
            contacts = repo.GetCompany_Contacts(agent_code: agent_code);
            return contacts;
        }
        [Route("company_detail/{agent_code}/contacts/{contact_id}"), HttpGet]
        public company_contacts GetCompany_contacts([FromUri]string agent_code,[FromUri]string contact_id)
        {
            company_contacts contacts = new company_contacts();
            contacts = repo.GetCompany_Contacts(agent_code: agent_code,contact_id:contact_id);
            return contacts;
        }
        [Route("company_detail/{agent_code}/contacts"), HttpPost]
        public company_contacts registerCompany_contacts([FromUri]string agent_code,[FromBody]register_company_contact item)
        {
            company_contacts contacts = new company_contacts();
            contacts = repo.registerCompany_Contacts(item,agent_code: agent_code);
            return contacts;
        }
        [Route("company_detail/{agent_code}/contacts/{contact_id}"), HttpPut]
        public company_contacts updateCompany_contacts([FromUri]string agent_code, [FromBody]update_company_contact item,[FromUri]string contact_id)
        {
            company_contacts contacts = new company_contacts();
            contacts = repo.updateCompany_Contacts(item,agent_code: agent_code,contact_id:contact_id);
            return contacts;
        }
        [Route("company_detail/{agent_code}/contacts/{contact_id}"), HttpDelete]
        public returnMain deleteCompanyContact([FromBody]deleteCompany item, [FromUri]string agent_code, [FromUri]string contact_id)
        {
            returnMain output = new returnMain();
            output = repo.deleteCompanyContact(item.user_name, agent_code, contact_id);
            return output;
        }
    }
}
