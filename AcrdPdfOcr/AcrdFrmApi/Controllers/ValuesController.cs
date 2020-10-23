using AccordCL.BCL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;

namespace AcrdFrmApi.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        
        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }


        private async Task<string> PostAttachment(byte[] data, Uri url, string contentType)
        {
            HttpContent content = new ByteArrayContent(data);

            content.Headers.ContentType = new MediaTypeHeaderValue(contentType);

            using (var form = new MultipartFormDataContent())
            {
                form.Add(content);

                using (var client = new HttpClient())
                {
                    var response = await client.PostAsync(url, form);
                    return await response.Content.ReadAsStringAsync();
                }
            }
        }


        [HttpPost]
        public IHttpActionResult Post()
        {
            var httpContext = HttpContext.Current;

            // Check for any uploaded file  
            if (httpContext.Request.Files.Count > 0)
            {
                //Loop through uploaded files  
                for (int i = 0; i < httpContext.Request.Files.Count; i++)
                {
                    HttpPostedFile httpPostedFile = httpContext.Request.Files[i];
                    if (httpPostedFile != null)
                    {
                        // Construct file save path  
                        var fileSavePath = Path.Combine(HostingEnvironment.MapPath("~/pdfUpload/"), httpPostedFile.FileName);

                        // Save the uploaded file  
                        httpPostedFile.SaveAs(fileSavePath);

                        if (fileSavePath == "")
                        {
                           // lblOcrStatus.Text = "Error : Kindly upload file before clicking on Process OCR";
                           // return;
                        }
                       

                        try
                        {
                           // string pdfFilePath = Server.MapPath("~/pdfUpload/" + pdfFileNameUploaded);
                            AccordForm acrdFrm = new AccordForm(fileSavePath);
                            return Ok(new { results = acrdFrm });
                           // return (JsonConvert.SerializeObject(acrdFrm));

                          
                        }
                        catch (Exception ex)
                        {
                           // lblOcrStatus.Text = "Fail : " + ex.Message + " > " + ex.StackTrace;
                            
                        }

                    }
                }
            }

            // Return status code  
            return Ok(new { results = "no Json" });
        }




    }
}
