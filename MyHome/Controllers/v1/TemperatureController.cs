using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using MyHome.DataModel;

namespace MyHome.Controllers.v1
{
    // todo - add alias (short) to this controller 
    public class TemperatureController : ApiController
    {
        public const string ClassName = "TemperatureController";

        private DataModelContainer db = new DataModelContainer();

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            var readout = db.ReadoutSet.Find(id);
            if (readout != null)
            {
                Models.ReadoutRequestModel request = new Models.ReadoutRequestModel() { At = readout.At, DeviceId = readout.Device.DeviceId,
                                                            Value = readout.Value };
                return Ok(request);
            }
            else
                return StatusCode(HttpStatusCode.NotFound);
        }

        // POST api/<controller>
        public IHttpActionResult Post([FromBody] Models.ReadoutRequestModel model)
        {

            if (!IsSecretOk(model))
                return StatusCode(HttpStatusCode.Unauthorized);

            if (String.IsNullOrEmpty(model.DeviceId))
                return StatusCode(HttpStatusCode.BadRequest);

            Device device = db.DeviceSet.Find(model.DeviceId);

            if (device == null)
                return StatusCode(HttpStatusCode.NotFound);

            Readout readout = new Readout() { ActionOn = device.ActionState == ActionStateEnum.On, Value = model.Value,
                            At = DateTime.Now, Device = device};
            db.ReadoutSet.Add(readout);
            db.SaveChanges();

            return Ok((int)device.ActionState);
        }


        private bool IsSecretOk(Models.ReadoutRequestModel model)
        {
            return true;
        }
    }
}