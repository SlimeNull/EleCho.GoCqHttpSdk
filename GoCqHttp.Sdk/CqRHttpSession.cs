using EleCho.GoCqHttpSdk.Model;
using EleCho.GoCqHttpSdk.Post;
using EleCho.GoCqHttpSdk.Post.Model;
using EleCho.GoCqHttpSdk.Util;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EleCho.GoCqHttpSdk
{
    public class CqRHttpSession : ICqPostSession
    {
        readonly Uri baseUri;
        readonly string? accessToken;
        CqPostPipeline postPipeline;
        HMACSHA1 sha1;

        private HttpListener listener;

        public Uri BaseUri => baseUri;
        public string? AccessToken { get; set; }
        public HttpListener Listener => listener;

        public CqPostPipeline PostPipeline => postPipeline;

        public CqRHttpSession(CqRHttpSessionOptions options)
        {
            baseUri = options.BaseUri;
            accessToken = options.AccessToken;
            postPipeline = new CqPostPipeline();

            listener = new HttpListener();
            listener.Prefixes.Add(baseUri.ToString());

            if (accessToken != null)
            {
                byte[] tokenBin = Encoding.UTF8.GetBytes(accessToken);
                sha1 = new HMACSHA1(tokenBin);
            }

            _ = HttpListenerLoopAsync();
        }

        private bool Verify(string? signature, byte[] data)
        {
            if (signature == null)
                return sha1 == null;
            if (sha1 == null)
                return false;

            if (signature.StartsWith("sha1="))
                signature = signature.Substring(5);

            byte[] hash = sha1.ComputeHash(data);
            string realSignature = string.Join(null, hash.Select(bt => Convert.ToString(bt, 16).PadLeft(2, '0')));
            return signature == realSignature;
        }
        private async Task HttpListenerLoopAsync()
        {
            while (true)
            {
                if (!listener.IsListening)
                {
                    await Task.Delay(100);
                    continue;
                }

                var context = await listener.GetContextAsync();

                using MemoryStream ms = new MemoryStream();
                context.Request.InputStream.CopyTo(ms);

                byte[] data = ms.ToArray();
                if (Verify(context.Request.Headers["X-Signature"], data))
                {
                    string json = GlobalConfig.TextEncoding.GetString(data);
                    CqWsDataModel? wsDataModel = JsonSerializer.Deserialize<CqWsDataModel>(json, JsonHelper.GetOptions());
                    if (wsDataModel is CqPostModel postModel)
                    {
                        CqPostContext? postContext = CqPostContext.FromModel(postModel);

                        if (postContext is CqPostContext)
                        {
                            await postPipeline.ExecuteAsync(postContext);
                            context.Response.StatusCode = (int)HttpStatusCode.OK;
                            context.Response.Close();
                            continue;
                        }
                    }

                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    context.Response.Close();
                }
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    context.Response.Close();
                }
            }
        }

        public void Start()
        {
            listener.Start();
        }

        public void Stop()
        {
            listener.Stop();
        }
    }
}