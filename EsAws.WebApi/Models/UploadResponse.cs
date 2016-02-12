using System.Collections.Generic;

namespace EsAws.WebApi.Models
{
    public class UploadResponse
    {
        public string UploadId { get; set; }
        public List<string> FileNames { get; set; }
        public List<string> ContentTypes { get; set; }
        public List<string> Names { get; set; }
    }
}