using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
            var pathInfo = "(empty)";

            if (requestFromContext.AppRelativeCurrentExecutionFilePath != null)
            {
                var pathElements = requestFromContext.AppRelativeCurrentExecutionFilePath.Split('/', '?');
                var index = -1;
                var elemenCount = pathElements.Length;
                for (var i=0; i<elemenCount;i++)
                {
                    if (!pathElements[i].Equals("StreamUploadX", StringComparison.OrdinalIgnoreCase))
                        continue;

                    if (i != elemenCount - 1)
                    {
                        index = i + 1;
                        break;
                    }
                }

                if (index >= 0)
                    pathInfo = pathElements[index];
            }

            var filePath = Path.Combine(DocumentDirectory, "UploadedFile.txt");

            HttpResponseMessage response;
            var message = "[" + requestFromContext.HttpMethod + "] " + pathInfo + "\r\n";
            try
            {
                SaveRequestBodyAsFile(requestFromContext, filePath);

                response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(message + "Uploaded at " + DateTime.Now.ToLongTimeString())
                };
            }
            catch (Exception ex)
            {
                response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content =
                        new StringContent(message + "Upload failed at " + DateTime.Now.ToLongTimeString() + "\r\n" + ex)
                };
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response); // Also sets the task state to "RanToCompletion"
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