using System.IO;
using System.Web;

namespace EsAws.WebApi.Handlers
{
    public class StreamUploadHandler : IHttpHandler
    {
        private const string DocumentDirectory = @"C:\emails";
        private const int BufferSize = 1024;

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            var filePath = Path.Combine(DocumentDirectory, "UploadedFile.txt");
            SaveRequestBodyAsFile(context.Request, filePath);
            context.Response.Write("Document uploaded!");
        }

        private static void SaveRequestBodyAsFile(HttpRequest request, string filePath)
        {
            using (var fileStream = File.Open(filePath, FileMode.Create, FileAccess.Write))
            {
                using (var requestStream = request.InputStream)
                {
                    var buffer = new byte[BufferSize];
                    var byteCount = 0;
                    while ((byteCount = requestStream.Read(buffer, 0, BufferSize)) > 0)
                    {
                        fileStream.Write(buffer, 0, byteCount);
                    }
                }
            }
        }
    }
}