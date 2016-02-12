using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using EsAws.WebApi.Models;

namespace EsAws.WebApi.Controllers.Api
{
    [System.Web.Http.RoutePrefix("Api")]
    public class UploaderController : ApiController
    {
        private const string UploadFolder = @"c:\emails";

        public UploaderController()
        {

        }

        [System.Web.Http.Route("Upload/{uploadId}")]
        [System.Web.Http.HttpGet]
        [System.Web.Http.HttpPut]
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> Upload([FromUri] string uploadId)
        {
            var streamProvider = new UploadFormProvider(UploadFolder);

            var reqStream = Request.Content.ReadAsStreamAsync().Result;
            if (reqStream.CanSeek)
            {
                reqStream.Position = 0;
            }

            //var provider = await Request.Content.ReadAsStreamAsync();
            //var text = provider.Contents.First().ReadAsStringAsync().Result;

            //var fileName = UploadFolder + "\\current.txt";

            //File.Create(fileName);
            //File.WriteAllText(fileName, text);

            //var text = Request.Content.ReadAsStringAsync().Result;

            var response = new UploadResponse
            {
                UploadId = uploadId,
                FileNames = streamProvider.FileData.Select(entry => entry.LocalFileName).ToList(),
                Names = streamProvider.FileData.Select(entry => entry.Headers.ContentDisposition.FileName).ToList(),
                ContentTypes = streamProvider.FileData.Select(entry => entry.Headers.ContentType.MediaType).ToList(),
            };
            return Ok(response);
        }
    }
}
