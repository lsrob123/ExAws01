using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace EsAws.WebApi.Models
{
    public class UploadFormProvider : MultipartFormDataStreamProvider
    {
        public UploadFormProvider(string rootPath) : base(rootPath)
        {
        }

        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            if (headers == null || headers.ContentDisposition == null)
                return base.GetLocalFileName(headers);

            var localFileName = headers.ContentDisposition.FileName.TrimEnd('"').TrimStart('"');
            return localFileName;
        }

        public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
        {
            var stream = base.GetStream(parent, headers);

            //var streamReader = new StreamReader(stream);
            //var text = streamReader.ReadToEnd();

            return stream;
        }
    }
}