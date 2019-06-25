using prabhuEticket_api.AppCode;
using prabhuEticket_api.Models;
using prabhuEticket_api.Repository;
using System.Web.Http;

namespace prabhuEticket_api.Controllers
{
    [RoutePrefix("api/v1"), Authentication]
    public class CardsController : ApiController
    {
        CardsRepo repo = new CardsRepo();
        [Route("cards"), HttpGet]
        public get_cards get_Cards()
        {
            get_cards output= repo.getCardsDetail();
            return output;
        }
        [Route("cards/{card_number}"), HttpGet]
        public get_cards get_Cards([FromUri]string card_number)
        {
            get_cards output = repo.getCardsDetail(card_number:card_number);
            return output;
        }
        [Route("cards"), HttpPost]
        public get_cards registerCards([FromBody]register_cards item)
        {
            get_cards output = repo.registerCards(item);
            return output;
        }
        [Route("cards/{card_number}"), HttpPost]
        public get_cards registerCards([FromBody]update_cards item,[FromUri]string card_number)
        {
            get_cards output = repo.updateCardsDetail(item,card_number);
            return output;
        }
        [Route("cards/{card_number}/balance"), HttpPost]
        public get_cards addWithdrawBalance ([FromBody]balance_class item, [FromUri]string card_number)
        {
            get_cards output = repo.balance(item, card_number);
            return output;
        }
    }
}
