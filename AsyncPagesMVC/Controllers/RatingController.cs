using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NServiceBus;

namespace AsyncPagesMVC.Controllers
{
    public class RatingController : ApiController
    {
        IEndpointInstance endpoint;

        public RatingController(IEndpointInstance endpoint)
        {
            this.endpoint = endpoint;
        }
        // GET: api/Rating
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Rating/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Rating
        public async void Post([FromBody]string textField)
        {

            if (int.TryParse(textField, out var number))
            {
            
            var command = new Command
            {
                Id = number
            };


            var sendOptions = new SendOptions();
            sendOptions.SetDestination("Samples.Mvc.Server");
                var status = await endpoint.Request<ErrorCodes>(command, sendOptions)
                .ConfigureAwait(false);
                //using (StreamWriter sw = new StreamWriter(@"C:\upw\sdasdfasd2.txt", true))
                //{
                //    sw.WriteLine("first request sent: " + DateTime.Now);
                //}
            }
        }

        // PUT: api/Rating/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Rating/5
        public void Delete(int id)
        {
        }
    }
}
