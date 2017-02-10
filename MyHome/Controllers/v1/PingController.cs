using System;
using System.Web.Http;

namespace MyHome.Controllers.v1
{
    public class PingController : ApiController
    {
        // GET: Ping
        public IHttpActionResult Get()
        {
            return Ok("Pong");
        }
    }
}