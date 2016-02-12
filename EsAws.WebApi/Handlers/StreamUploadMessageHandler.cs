using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace EsAws.WebApi.Handlers
{
    public class StreamUploadMessageHandler : DelegatingHandler      
    {
        private const string DocumentDirectory = @"C:\emails";
        private const int BufferSize = 1024;
        
        protected override Task<HttpResponseMessage> SendAsync(
                HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var requestFromContext = HttpContext.Current.Request;
            var method = requestFromContext.HttpMethod;

            var filePath = Path.Combine(DocumentDirectory, "UploadedFile.txt");
            SaveRequestBodyAsFile(requestFromContext, filePath);

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("Uploaded!")
            };

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);   // Also sets the task state to "RanToCompletion"
            return tsc.Task;
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