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
            string message = "Failed to send email"; //Default message
            try
            {
                string requestUrl = ConfigurationManager.AppSettings["RequestUrl"];
                
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("CallingAppName", "APIUser");
                    var formcontent = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("To",model.to),
                        new KeyValuePair<string, string>("Subject",model.subject),
                        new KeyValuePair<string, string>("CC",model.cc),
                        new KeyValuePair<string, string>("Body",model.body)
                    });
                    var response = await client.PostAsync(requestUrl, formcontent);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        return Request.CreateResponse((HttpStatusCode)200, new { StatusCode = 200, msg = "Email has been sent successfully." });
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return Request.CreateResponse((HttpStatusCode)401, new { StatusCode = 401,  msg = message });
        }
        public string Get()
        {
            return "Hello world";
        }
    }
}