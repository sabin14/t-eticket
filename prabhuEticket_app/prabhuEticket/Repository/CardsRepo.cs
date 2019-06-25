using prabhuEticket.AppCode;
using prabhuEticket.Models;
using System.Collections.Generic;
using System.Data;
namespace prabhuEticket.Repository
{
    public class CardsRepo
    {
        globFunction func = new globFunction();
        public get_cards getCardsDetail(DataTable dt = null, string card_number = "")
        {
            if (dt == null)
            {
                dt = new DataTable();
                string sql = "spa_card_detail @flag='s',@card_number=" + func.singleQuote(card_number);
                dt = func.RunSQL(sql);
            }
            get_cards allObj = new get_cards();
            allObj.error_Lists = new List<error_list>();
            allObj.data = new List<get_cards.cards>();
            foreach (DataRow rows in dt.Rows)
            {
                if (rows["code"].ToString() == "0")
                {
                    get_cards.cards cards = new get_cards.cards();
                    cards.card_number = rows["card_number"].ToString();
                    cards.card_nfc = rows["card_nfc"].ToString();
                    cards.balance = rows["balance"].ToString();
                    cards.type = rows["type"].ToString();
                    cards.status = rows["status"].ToString();
                    cards.create_by = rows["create_by"].ToString();
                    cards.create_ts = rows["create_ts"].ToString();
                    cards.qr_code = rows["qr_code"].ToString();
                    cards.credit_limit = rows["credit_limit"].ToString();
                    cards.initial_balance = rows["initial_balance"].ToString();
                    allObj.data.Add(cards);
                    allObj.status_code = 200;
                    allObj.status = true;
                    allObj.message = rows["message"].ToString();
                }
                else
                {
                    error_list error_List = new error_list();
                    error_List.error_code = rows["code"].ToString();
                    error_List.error_message = rows["message"].ToString();
                    allObj.error_Lists.Add(error_List);
                    allObj.status_code = 200;
                    allObj.status = true;
                    allObj.message = "failed";
                }
            }
            return allObj;
        }
        public get_cards registerCards(register_cards item)
        {
            string sql = "spa_card_detail @flag='i',@card_number=" + func.singleQuote(item.card_number) +
            ",@card_nfc=" + func.singleQuote(item.card_nfc) +
            ",@qr_code=" + func.singleQuote(item.qr_code) +
            ",@balance=" + func.singleQuote(item.balance) +
            ",@type=" + func.singleQuote(item.type) +
            ",@status=" + func.singleQuote(item.status) +
            ",@user_name=" + func.singleQuote(item.create_by)+
            ",@credit_limit=" + func.singleQuote(item.credit_limit);
            DataTable dt = func.RunSQL(sql);
            get_cards cards = getCardsDetail(dt);
            return cards;
        }
        public get_cards updateCardsDetail(update_cards item,string card_number)
        {
            string sql = "spa_card_detail @flag='u',@card_number=" + func.singleQuote(card_number) +
            ",@qr_code=" + func.singleQuote(item.qr_code) +
            ",@type=" + func.singleQuote(item.type) +
            ",@status=" + func.singleQuote(item.status) +
            ",@user_name=" + func.singleQuote(item.update_by)+
            ",@credit_limit="+func.singleQuote(item.credit_limit);
            DataTable dt = func.RunSQL(sql);
            get_cards cards = getCardsDetail(dt);
            return cards;
        }
        public get_cards balance(balance_class item, string card_number)
        {
            string sql = "spa_card_detail @flag='b',@card_number=" + func.singleQuote(card_number) +
            ",@balance_type=" + func.singleQuote(item.type) +
            ",@balance=" + func.singleQuote(item.balance) +
            ",@user_name=" + func.singleQuote(item.user_name);
            DataTable dt = func.RunSQL(sql);
            get_cards cards = getCardsDetail(dt);
            return cards;
        }
    }
}