using System.Web.Http;
using EsAws.WebApi.Models;

namespace EsAws.WebApi.Controllers.Api
{
    [RoutePrefix("Api")]
    public class UploaderController : ApiController
    {
        private const string _uploadFolder = @"c:\emails";

        public UploaderController()
        {

        }

        [Route("Upload/{uploadId}")]
        [HttpGet]
        [HttpPut]
        public UploadResponse Upload([FromUri] string uploadId)
        {
            var response = new UploadResponse {UploadId = uploadId};
            return response;
        }
    }
}
