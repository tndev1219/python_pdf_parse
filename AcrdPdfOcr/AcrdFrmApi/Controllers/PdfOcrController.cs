using AccordCL.BCL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;

namespace AcrdFrmApi.Controllers
{
    public class PdfOcrController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Post()
        {
            var httpContext = HttpContext.Current;
            string msg = " File count is " + httpContext.Request.Files.Count.ToString();
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
                            return Ok(new { results = ex.Message + " > " + ex.ToString() });
                            // lblOcrStatus.Text = "Fail : " + ex.Message + " > " + ex.StackTrace;

                        }

                    }
                }
            }

            // Return status code  
            return Ok(new { results = "no Json " + msg});
        }

    }
}
