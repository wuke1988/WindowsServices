using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _5OrleansWebApiDemo_WebAPI.Controllers
{

    public class PersonController : ApiController
    {
        [HttpGet]
        public String SayHello(string name)
        {
            var person = new _5OrleansWebAPIDemo_Business.Person();
            return  person.SayHello(name);
        }
    }
}
