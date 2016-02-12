using System;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace EsAws.Uploader
{
    public partial class Form1 : Form
    {
        private const int BufferSize = 1024;
        private const string Url = "http://localhost:62607/api/StreamUploadx";

        public Form1()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            lblFileName.Text = string.Empty;

            var openFileResult = openFileDialog1.ShowDialog();
            if (openFileResult != DialogResult.OK)
            {
                ShowResponse("File not found");
                return;
            }

            lblFileName.Text = openFileDialog1.FileName;
            Upload();
        }

        private void Upload()
        {
            var filePath = lblFileName.Text;
            txtResponse.Text = string.Empty;
            
            var request = (HttpWebRequest)WebRequest.Create(Url);
            request.Accept = "text/xml";
            request.Method = "PUT";

            string result;
            try
            {
                using (var fileStream = File.OpenRead(filePath))
                {
                    using (var requestStream = request.GetRequestStream())
                    {
                        var buffer = new byte[BufferSize];
                        var byteCount = 0;
                        while ((byteCount = fileStream.Read(buffer, 0, BufferSize)) > 0)
                        {
                            requestStream.Write(buffer, 0, byteCount);
                        }
                    }
                }


                using (var response = request.GetResponse())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        result = reader.ReadToEnd();
                    }
                }
            }
            catch (Exception exception)
            {
                result = exception.ToString();
            }
            ShowResponse(result);
        }

        private void ShowResponse(string result)
        {
            txtResponse.Text = "[" + DateTime.Now.ToLongTimeString() + "]" + result;
        }

        private void btnUploadCurrent_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(lblFileName.Text) || !File.Exists(lblFileName.Text))
            {
                ShowResponse("File not found");
            }

            Upload();
        }
    }
}