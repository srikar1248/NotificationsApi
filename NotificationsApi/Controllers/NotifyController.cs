using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace NotificationsApi.Controllers
{
    public class NotifyController : ApiController
    {
        // POST: api/Notification/send
        [HttpPost]
        public async System.Threading.Tasks.Task<HttpResponseMessage> Email([FromBody] MailModel model)
        {
            string msg = "";
            try
            {
                string requestUrl = ConfigurationManager.AppSettings["RequestUrl"];
                string content = JsonConvert.SerializeObject(model);

                using (var client = new HttpClient())
                {
                    var response = await client.PostAsync(requestUrl, new StringContent(content, Encoding.UTF8, "application/json"));

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        return Request.CreateResponse((HttpStatusCode)200, new { message = "Email has been sent successfully." });
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return Request.CreateResponse((HttpStatusCode)401, new { message = msg });
        }
        public string Get()
        {
            return "Hello world";
        }
    }
}