using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using prabhuEticket.AppCode;
namespace prabhuEticket.Repository
{
    interface iGetRepo<IModel> where IModel : class, new()
    {
        List<IModel> GetAllDataByType(string data);
        List<IModel> GetAllData();
    }
}