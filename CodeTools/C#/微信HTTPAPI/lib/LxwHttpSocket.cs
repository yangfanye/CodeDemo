using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;


namespace WeiXinZhuaFaWang.lib
{
    class LxwHttpSocket
    {
        /// <summary>
        /// 存储cookies
        /// </summary>
        public List<LxwCookie> LST_COOKIE = new List<LxwCookie>();

        /// <summary>
        /// 设置默认编码
        /// </summary>
        /// <param name="Encoding"></param>
        public LxwHttpSocket(Encoding Encoding = null,int WriteTimeOut = 3,int ReadTimeOut = 30)
        {
            this.Encoding = Encoding??Encoding.UTF8;
            this.WriteTimeOut = WriteTimeOut;
            this.ReadTimeOut = ReadTimeOut;
        }

        /// <summary>
        /// 存储一些全局信息
        /// </summary>
        Dictionary<string, string> DI_KEYS = new Dictionary<string, string>();
        public void Add(string Key, string Value)
        {
            DI_KEYS[Key] = Value;
        }

        /// <summary>
        /// 编码
        /// </summary>
        public Encoding Encoding { get; set; }

        /// <summary>
        /// request 里面第一条必须是method
        /// </summary>
        /// <param name="request"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public LxwResponse SendRequest(string request, string body = null)
        {
            var sendBody = FormatBody(body);
            var sendHeader = FormatHeader(request, sendBody);

            TcpClient client = new TcpClient(sendHeader.Uri.Host, sendHeader.Uri.Port);

            try
            {
                if (client.Connected)
                {
                    SslStream sslStream = new SslStream(client.GetStream(), true
                        , new RemoteCertificateValidationCallback((sender, certificate, chain, sslPolicyErrors)
                           =>
                        {
                            return sslPolicyErrors == SslPolicyErrors.None;
                        }
                            ), null);

                    sslStream.ReadTimeout = ReadTimeOut * 1000;
                    sslStream.WriteTimeout = WriteTimeOut * 1000;

                    X509Store store = new X509Store(StoreName.My);

                    sslStream.AuthenticateAsClient(
                        sendHeader.Uri.Host, 
                        store.Certificates, 
                        System.Security.Authentication.SslProtocols.Default, 
                        false);

                    if (sslStream.IsAuthenticated)
                    {
                        sslStream.Write(sendHeader.HeaderByte);
                        sslStream.Write(LINE);
                        if (sendBody != null)
                            sslStream.Write(sendBody);
                        sslStream.Write(LINE);

                        sslStream.Flush();

                        return ReadResponse(sslStream);
                    }
                }
            }
            finally
            {
                client.Close();
            }

            return null;
        }

        class TaskArguments
        {
            public TaskArguments(CancellationTokenSource cancelSource, Stream sm)
            {
                this.CancelSource = cancelSource;
                this.Stream = sm;
            }
            public CancellationTokenSource CancelSource { get; private set; }
            public Stream Stream { get; private set; }
        }

        private static LxwResponse ReadResponse(Stream sm)
        {
            LxwResponse response = null;
            CancellationTokenSource cancelSource = new CancellationTokenSource();
            Task<string> myTask = Task.Factory.StartNew<string>(
                new Func<object, string>(ReadHeaderProcess),
                new TaskArguments(cancelSource, sm),
                cancelSource.Token);
            if (myTask.Wait(3 * 1000)) //尝试3秒时间读取协议头
            {
                string header = myTask.Result;
                if (!string.IsNullOrEmpty(header))
                {
                    if (header.StartsWith("HTTP/1.1 100"))
                    {
                        return ReadResponse(sm);
                    }

                    byte[] buff = null;
                    int start = header.ToUpper().IndexOf("CONTENT-LENGTH");
                    int content_length = -1;  //fix bug
                    if (start > 0)
                    {
                        string temp = header.Substring(start, header.Length - start);
                        string[] sArry = Regex.Split(temp, "\r\n");
                        content_length = Convert.ToInt32(sArry[0].Split(':')[1]);
                        if (content_length > 0)
                        {
                            buff = new byte[content_length];
                            int inread = sm.Read(buff, 0, buff.Length);
                            while (inread < buff.Length)
                            {
                                inread += sm.Read(buff, inread, buff.Length - inread);
                            }
                        }
                    }
                    else
                    {
                        start = header.ToUpper().IndexOf("TRANSFER-ENCODING: CHUNKED");
                        if (start > 0)
                        {
                            //buff = ChunkedReadResponse(sm);
                        }
                        else
                        {
                            //buff = SpecialReadResponse(sm);//例外
                        }
                    }
                    response = new LxwResponse(header, buff);
                }
            }
            else
            {
                cancelSource.Cancel(); //超时的话，别忘记取消任务哦
            }
            return response;
        }

        static string ReadHeaderProcess(object args)
        {
            TaskArguments argument = args as TaskArguments;
            StringBuilder bulider = new StringBuilder();
            if (argument != null)
            {
                Stream sm = argument.Stream;
                while (!argument.CancelSource.IsCancellationRequested)
                {
                    try
                    {
                        int read = sm.ReadByte();
                        if (read != -1)
                        {
                            byte b = (byte)read;
                            bulider.Append((char)b);
                            string temp = bulider.ToString();
                            if (temp.EndsWith("\r\n\r\n"))//Http协议头尾
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                        break;
                    }
                }
            }
            return bulider.ToString();
        }

        byte[] LINE
        {
            get {
                return this.Encoding.GetBytes("\r\n");
            }
        }

        LxwRequest FormatHeader(string request, byte[] body = null)
        {
            if (request == null)
                throw new ArgumentNullException("byte[] FormatHeader(string request), request is null");

            request = FormatKeys(request);

            LxwRequest req = new LxwRequest();
            var bodys = Regex.Split(request, "\r\n");
            var list = new List<string>();
            var start = 0;
            var end = bodys.Length - 1;
            {
                for (; start < bodys.Length; start++)
                {
                    if (bodys[start] != "") break;
                }
                for (; end > start; end--)
                {
                    if (bodys[end] != "") break;
                }
                
                for (; start <= end; start++)
                {
                    if (bodys[start].StartsWith("Content-Length", StringComparison.OrdinalIgnoreCase))
                        continue;

                    {
                        //这里还可以扩充
                        var arr = bodys[start].Split(' ');
                        if (arr[0] == "GET" ||
                            arr[0] == "POST" ||
                            arr[0] == "OPTIONS")
                        {
                            req.SSL = arr[1].StartsWith("https://", StringComparison.OrdinalIgnoreCase);
                            req.Uri = new Uri(arr[1]);
                        }
                    }

                    list.Add(bodys[start]);
                }
                if (body != null)
                    list.Add("Content-Length: " + body.Length);

                req.HeaderByte = Encoding.GetBytes(string.Join("\r\n", list.ToArray()));
            }

            //返回
            return req;
        }


        byte[] FormatBody(string body = null)
        {
            if (body == null) return null;
            body = FormatKeys(body);

            var bodys = Regex.Split(body, "\r\n");
            var list = new List<string>();
            var start = 0;
            var end = bodys.Length - 1;
            {
                for (; start < bodys.Length; start++)
                {
                    if (bodys[start] != "") break;                    
                }
                for (; end > start; end--)
                {
                    if (bodys[end] != "") break;                    
                }

                for (; start <= end; start++)
                {
                    list.Add(bodys[start]);
                }
            }

            //返回
            return Encoding.GetBytes(string.Join("\r\n", list.ToArray()));
        }

        /// <summary>
        /// 格式化信息
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        string FormatKeys(string body)
        {
            foreach (string key in DI_KEYS.Keys)
                body = body.Replace("{" + key + "}", DI_KEYS[key]);

            foreach (var cookies in LST_COOKIE)
            {
                if (cookies.Key != "")
                {
                    body = body.Replace("{" + cookies.Key + "}", cookies.Value);
                }
            }

            return body;
        }


        public int WriteTimeOut { get; set; }

        public int ReadTimeOut { get; set; }
    }

    public class LxwRequest
    {
        public Uri Uri { get; set; }
        public byte[] HeaderByte { get; set; }

        public bool SSL { get; set; }
    }

    /// <summary>
    /// 返回信息
    /// </summary>
    public class LxwResponse
    {
        internal LxwResponse(string header,
            byte[] body)
        {
            this.Header = header;
            this.Body = body;


        }

        //暂未将回应HTTP协议头转换为HttpHeader类型
        public string Header
        {
            get;
            private set;
        }

        public byte[] Body
        {
            get;
            private set;
        }

    }
    /// <summary>
    /// 存储Cookies
    /// </summary>
    public class LxwCookie
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Domain { get; set; }
        public string Date { get; set; }
    }
}
