using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace mcClusterConsole.Controllers
{
    public class tenantInfoController : ApiController
    {
        // GET api/values
        [SwaggerOperation("GetAll")]
        public string Get()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://openhackt04.westeurope.cloudapp.azure.com:19080");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string nodeName = null;
            HttpResponseMessage response = client.GetAsync(@"/Nodes?api-version=6.0").Result;
            if (response.IsSuccessStatusCode)
            {
                nodeName = response.Content.ReadAsStringAsync().Result;
            }
            JArray nodes = JObject.Parse(nodeName).Value<JArray>("Items");

            return nodeName;
        }



        // GET api/values/5
        //[SwaggerOperation("GetById")]
        //[SwaggerResponse(HttpStatusCode.OK)]
        //[SwaggerResponse(HttpStatusCode.NotFound)]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/values
        //[SwaggerOperation("Create")]
        //[SwaggerResponse(HttpStatusCode.Created)]
        //public void Post([FromBody]string value)
        //{
        //}

        // PUT api/values/5
        //[SwaggerOperation("Update")]
        //[SwaggerResponse(HttpStatusCode.OK)]
        //[SwaggerResponse(HttpStatusCode.NotFound)]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/values/5
        //[SwaggerOperation("Delete")]
        //[SwaggerResponse(HttpStatusCode.OK)]
        //[SwaggerResponse(HttpStatusCode.NotFound)]
        //public void Delete(int id)
        //{
        //}
    }
}
