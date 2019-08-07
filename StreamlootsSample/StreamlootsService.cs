using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StreamlootsSample
{
    public interface IStreamlootsService
    {
        Task<bool> Connect();

        Task Disconnect();
    }

    public class StreamlootsService : IStreamlootsService, IDisposable
    {
        private string streamlootsID;

        private WebRequest webRequest;
        private Stream responseStream;

        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        public StreamlootsService(string streamlootsID)
        {
            this.streamlootsID = streamlootsID;
        }

        public Task<bool> Connect()
        {
            try
            {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                Task.Run(this.BackgroundCheck, this.cancellationTokenSource.Token);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return Task.FromResult(false);
        }

        public Task Disconnect()
        {
            this.cancellationTokenSource.Cancel();
            if (this.webRequest != null)
            {
                this.webRequest.Abort();
                this.webRequest = null;
            }
            if (this.responseStream != null)
            {
                this.responseStream.Close();
                this.responseStream = null;
            }
            return Task.FromResult(0);
        }

        private async Task BackgroundCheck()
        {
            this.webRequest = WebRequest.Create(string.Format("https://widgets.streamloots.com/alerts/{0}/media-stream", this.streamlootsID));
            ((HttpWebRequest)this.webRequest).AllowReadStreamBuffering = false;
            var response = this.webRequest.GetResponse();
            this.responseStream = response.GetResponseStream();

            UTF8Encoding encoder = new UTF8Encoding();
            var buffer = new byte[100000];
            while (true)
            {
                try
                {
                    if (this.responseStream.CanRead)
                    {
                        int len = this.responseStream.Read(buffer, 0, 100000);
                        if (len > 10)
                        {
                            string text = encoder.GetString(buffer, 0, len);
                            if (!string.IsNullOrEmpty(text))
                            {
                                JObject jobj = JObject.Parse("{ " + text + " }");
                                if (jobj != null && jobj.ContainsKey("data"))
                                {
                                    StreamlootsCardModel card = jobj["data"].ToObject<StreamlootsCardModel>();
                                    if (card != null)
                                    {
                                        Console.WriteLine("Card Name: " + card.data.cardName);
                                        Console.WriteLine("Card Image:" + card.imageUrl);
                                        Console.WriteLine("Redeemed By: " + card.data.Username);
                                        Console.WriteLine("Message: " + card.data.Message);
                                        Console.WriteLine();
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                await Task.Delay(1000);
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // Dispose managed state (managed objects).
                    this.cancellationTokenSource.Dispose();
                    if (this.webRequest != null)
                    {
                        this.webRequest.Abort();
                        this.webRequest = null;
                    }
                    if (this.responseStream != null)
                    {
                        this.responseStream.Close();
                        this.responseStream = null;
                    }
                }

                // Free unmanaged resources (unmanaged objects) and override a finalizer below.
                // Set large fields to null.

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}
