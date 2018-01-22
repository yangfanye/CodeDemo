using FluorineFx.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace WeiXinZhuaFaWang
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();

            jslogin();

            qrcode();

            login();
        }

        private void login()
        {
            ShowMsg("login.....");
            if (__3)
            {
                //第三步，等待扫描
                var time = new System.Windows.Forms.Timer();
                time.Interval = sleeptime;
                time.Stop();

                int count = 0;
                time.Tick += new EventHandler(delegate
                {
                    if (count++ > login_try_times)
                    {
                        time.Stop();
                        throw new Exception("错误的次数超过了:" + login_try_times + "次");
                    }

                    SendHeader(httpclient, url_login[1]);
                    var task = httpclient.GetStringAsync(ReplaceKey(url_login[0]));
                    var result = task.Result;

                    if (result.IndexOf("window.redirect_uri=") != -1)
                    {
                        time.Stop();
                        redirect_uri = GetResultString(result, "\"(.*?)\"");

                        if (redirect_uri.IndexOf("wx2.qq.com")!=-1)
                            WXNUMBER = "2";

                        //执行第四部
                        doStep4();
                    }
                });
                time.Start();
            }
        }

        private void qrcode()
        {
            ShowMsg("qrcode");
            if (__2)
            {
                SendHeader(httpclient, url_qrcode[1]);
                var task = httpclient.GetStreamAsync(ReplaceKey(url_qrcode[0]));

                var result = task.Result;
                this.pictureBox1.BackgroundImage = Image.FromStream(result);
            }
        }

        private void jslogin()
        {
            ShowMsg("islogin");
            if (__1)
            {
                SendHeader(httpclient, url_jslogin[1]);
                var task = httpclient.GetStreamAsync(ReplaceKey(url_jslogin[0]));
                var result = GetDeflateByStream(task.Result, "GBK");
                UUID = GetCodeString(result, "200", "\"(.*?)\"");

                
            }
        }

       
        private void doStep4()
        {
            this.pictureBox1.Visible = false;
            this.listBox1.Visible = true;
            this.lstBoxUser.Visible = true;
            this.btnSend.Visible = true;
            this.txtBoxMessage.Visible = true;
            this.btnGetUserList.Visible = true;

            redirect_uri_fun();

            webwxinit_new();

            webwxgetcontact();

            //循环检查状态，如果得到了最新信息，然后开始执行
            synccheck();

            urldownload();

            //开启一个timer，一直给我发送信息
            var timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += (o, e) => {
                if (!bFirst)
                {
                    timer.Interval = 10 * 60 * 1000;
                    bool b = false;
                    foreach (var s in USER_DI)
                    {
                        if (s.Value.IndexOf("都好") != -1)
                        {
                            b = true;
                            SendMsg(s.Key,USER_INFO, DateTime.Now.ToString(), false);
                            break;
                        }
                    }

                    if (b == false)
                    {
                        //如果走到这一步，就随机一个人发
                        SendMsg(USER_DI.ElementAt((new Random()).Next(USER_DI.Count())).Key, USER_INFO,DateTime.Now.ToString(), false);
                    }
                }
            };

            timer.Start();
        }


        bool bFirst = true;
        private void redirect_uri_fun()
        {
            ShowMsg("redirect_uri_fun");
            if (__4)
            {
                HttpWebRequest h = (HttpWebRequest)HttpWebRequest.Create(redirect_uri);
                h.AllowAutoRedirect = false;
                h.CookieContainer = cookieContainer;
                HttpWebResponse r = (HttpWebResponse)h.GetResponse();

                COOKIES = GetAllCookiesA(cookieContainer);

                using (System.IO.StreamReader read = new System.IO.StreamReader(r.GetResponseStream()))
                {
                    string value = read.ReadToEnd();
                    if (value.IndexOf("pass_ticket") == -1) throw new Exception("没有得到wxsid信息");
                    step4xml = Xml2Json<Step4XML>(value);
                }
                r.Close();
            }
        }

        private void synccheck()
        {
            ShowMsg("synccheck");
            if (__6)
            {
                var urlA = "https://webpush[number].weixin.qq.com/cgi-bin/mmwebwx-bin/synccheck?r={time}&skey={SKEY}&sid={SID}&uin={UIN}&deviceid={DeviceID}&synckey={synckey}&_={time}";

                bool bRun = true;
                
                ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
                {
                    while (bRun)
                    {
                        Thread.Sleep(2000);
                        try
                        {
                            var url = ReplaceKey(urlA);
                            if (bFirst) ShowMsg("正在解析中...等待!");
                            JavaScriptObject obj = JavaScriptConvert.DeserializeObject(SyncKey) as JavaScriptObject;
                            JavaScriptArray list = obj["List"] as JavaScriptArray;
                            var k = "";
                            foreach (JavaScriptObject o in list)
                                k += "|" + o["Key"] + "_" + o["Val"];

                            url = url.Replace("{synckey}", k.Substring(1));

                            HttpWebRequest h = (HttpWebRequest)HttpWebRequest.Create(url);
                            h.AllowAutoRedirect = false;
                            h.CookieContainer = cookieContainer;
                            h.Accept = "application/javascript, */*;q=0.8";
                            HttpWebResponse r = (HttpWebResponse)h.GetResponse();
                            using (System.IO.StreamReader read = new System.IO.StreamReader(r.GetResponseStream()))
                            {

                                string value = read.ReadToEnd();
                                if (value.Contains("1101"))
                                {
                                    bRun = false;

                                    ShowMsg("请重新打开!" + url);
                                }
                                else
                                {
                                    if (bFirst)
                                    {
                                        bFirst = false;
                                        ShowMsg("正常运行!");
                                    }
                                    Console.WriteLine("1=>" + value);
                                    //string ret = value;
                                    //window.synccheck={retcode:"0",selector:"6"}
                                    if (value.IndexOf("selector:\"0\"") == -1 && value.IndexOf("retcode:\"0\"") != -1)
                                        doStep7();
                                }

                            }
                            r.Close();
                        }
                        catch
                        {
                        }
                    }
                }));
            }
        }

        

        private void webwxgetcontact()
        {
            ShowMsg("webwxgetcontact");
            if (true)
            {
                var url = "https://wx[number].qq.com/cgi-bin/mmwebwx-bin/webwxgetcontact?pass_ticket={pass_ticket}&r={time}&skey={SKEY}";

                HttpWebRequest h = (HttpWebRequest)HttpWebRequest.Create(ReplaceKey(url));
                h.AllowAutoRedirect = false;
                h.CookieContainer = cookieContainer;
                h.Accept = "application/json, text/plain, */*";
                HttpWebResponse r = (HttpWebResponse)h.GetResponse();
                using (System.IO.StreamReader read = new System.IO.StreamReader(r.GetResponseStream()))
                {
                    string value = read.ReadToEnd();
                    USER_LIST = JavaScriptConvert.DeserializeObject(value) as JavaScriptObject;

                    //显示到list中
                    var arr = USER_LIST["MemberList"] as JavaScriptArray;
                    lstBoxUser.Items.Clear();

                    foreach (JavaScriptObject o in arr)
                    {
                        USER_DI[o["UserName"] + ""] = o["NickName"] + "";
                        //NickName
                        lstBoxUser.Items.Add(o["NickName"] + ">" + o["UserName"]); 
                    }
                }
                r.Close();

            }
        }

        private void webwxinit_new()
        {
            ShowMsg("webwxinit_new");
            if (__5)
            {
                SendHeader(httpclient, ReplaceHeaderKey(url_webwxinit[1]));
                 byte[] bs = Encoding.UTF8.GetBytes(ReplaceHeaderKey(@"{""BaseRequest"":{""Uin"":""{UIN}"",""Sid"":""{SID}"",""Skey"":""{SKEY}"",""DeviceID"":""{DeviceID}""}}"));

                var task = httpclient.PostAsync(ReplaceKey(url_webwxinit[0]), new ByteArrayContent(bs));
                {
                    string value = GetDeflateByStream(task.Result.Content.ReadAsStreamAsync().Result);
                    //"Ret": 1100,
                    if (!value.Contains("\"Ret\": 0"))
                    {
                        ShowMsg("没有返回正确的数据，webwxinit错误!");
                        throw new Exception("没有返回正确的数据，webwxinit错误!");
                    }

                    //USER_INFO
                    USER_INFO = SubString(value.Replace("\r", "").Replace("\n", ""), "\"User\": {", "NickName");
                    USER_INFO = SubString(USER_INFO, "\"UserName\": \"", "\",");


                    USER_NICKNAME = SubString(value.Replace("\r", "").Replace("\n", ""), "\"User\": {", "HeadImgUrl");
                    USER_NICKNAME = SubString(USER_NICKNAME, "\"NickName\": \"", "\",");


                    label1.Text = USER_INFO;
                    USER_DI.Add(USER_INFO, USER_NICKNAME);

                    this.Text = USER_NICKNAME+">>>转发微信机器人 V0.5 20151007  By LXW";

                    //SyncKey
                    SyncKey = SubString(value.Replace("\r", "").Replace("\n", ""), "\"SyncKey\": ", "}]}");
                    SyncKey += "}]}";
#if DEBUG
                    this.textBox1.Text = SyncKey;
#endif
                }
            }
        }

        /// <summary>
        /// 发送文件
        /// </summary>
        private void btnSendFile_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                sendFileMsg(openFileDialog.FileName);
            }
        }

        /// <summary>
        /// 发送文件
        /// </summary>
        /// <param name="filePath"></param>
        void sendFileMsg(string filePath)
        {
            HttpClient newClient = new HttpClient();
            {
                //执行第一个
                SendHeader(newClient, webwxuploadmedia0[1]);
                var result = newClient.PostAsync(ReplaceKey(webwxuploadmedia0[0]),new StringContent(""));
                Console.WriteLine(result.Result);
            }
            {
                FileStream fs = File.OpenRead(filePath);
               
                var requestContent = new MultipartFormDataContent();

                var txtContent = new ByteArrayContent(Encoding.UTF8.GetBytes(ReplaceHeaderKey(webwxuploadmedia1body2).Replace("[CD]", fs.Length + "")));
                txtContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = "uploadmediarequest"
                };
                requestContent.Add(txtContent);
                Dictionary<string, string> di = new Dictionary<string, string>();
                di["id"] = "WU_FILE_0";
                di["name"] = "webwxgetvoice.mp3";
                di["type"] = "audio/mpeg";
                di["size"] = fs.Length+"";
                di["mediatype"] = "doc";
                di["webwx_data_ticket"] = COOKIES["webwx_data_ticket"];
                di["pass_ticket"] = this.step4xml.pass_ticket;
                foreach (string s in di.Keys)
                {
                    txtContent = new ByteArrayContent(Encoding.UTF8.GetBytes(di[s]));
                    txtContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                    {
                        Name = s
                    };
                    requestContent.Add(txtContent);
                }



                //var imageContent = new ByteArrayContent(temp);
                //imageContent.Headers.ContentType =
                //    MediaTypeHeaderValue.Parse("audio/mpeg");                
                //requestContent.Add(imageContent, "filename","webwxgetvoice.mp3");
                var t = new StreamContent(fs);
                t.Headers.ContentType = MediaTypeHeaderValue.Parse("audio/mp3");
                requestContent.Add(t, "filename", "webwxgetvoice.mp3");

                SendHeader(newClient, webwxuploadmedia1[1]);

                var task = newClient.PostAsync(ReplaceHeaderKey(webwxuploadmedia1[0]), requestContent);
                var value = task.Result.Content.ReadAsStringAsync().Result;
                Console.WriteLine(value);

                //requestContent.Add(imageContent, "image", "image.jpg");
            }
            {
                //SendHeader(httpclient, ReplaceHeaderKey(webwxuploadmedia1[1]));

                //FileStream fs = File.OpenRead(filePath);

                //byte[] temp = new byte[fs.Length];
                //fs.Read(temp, 0, temp.Length);

                //byte[] bs = Encoding.UTF8.GetBytes(ReplaceHeaderKey(webwxuploadmedia1body0).Replace("[CD]", temp.Length + "") + "\r\n\r\n");

                //byte[] bsA = Encoding.UTF8.GetBytes("\r\n\r\n-----------------------------7e0d1f760df0");
                //byte[] resArr = new byte[bs.Length + temp.Length + bsA.Length];
                //bs.CopyTo(resArr, 0);
                //temp.CopyTo(resArr, bs.Length);
                //bsA.CopyTo(resArr, temp.Length + bs.Length);


                //var task = httpclient.PostAsync(ReplaceKey(webwxuploadmedia1[0]), new ByteArrayContent(resArr));
                //{
                //    var value = task.Result.Content.ReadAsStringAsync().Result;
                //    Console.WriteLine(value);
                //}
            }
        }

        private void webwxinit_old()
        {
            ShowMsg("webwxinit_old");
            if (__5)
            {
                HttpWebRequest h = (HttpWebRequest)HttpWebRequest.Create(ReplaceKey(url_webwxinit[0]));
                h.AllowAutoRedirect = false;
                h.CookieContainer = cookieContainer;
                h.Method = "POST";
                h.Accept = "application/json, text/plain, */*";
                h.ContentType = "application/json;charset=utf-8";

                byte[] bs = Encoding.UTF8.GetBytes(ReplaceHeaderKey(@"{""BaseRequest"":{""Uin"":""{UIN}"",""Sid"":""{SID}"",""Skey"":""{SKEY}"",""DeviceID"":""{DeviceID}""}}"));
                using (Stream reqStream = h.GetRequestStream())
                {
                    reqStream.Write(bs, 0, bs.Length);
                    reqStream.Close();
                }
                HttpWebResponse r = (HttpWebResponse)h.GetResponse();


                using (System.IO.StreamReader read = new System.IO.StreamReader(r.GetResponseStream()))
                {
                    string value = read.ReadToEnd();
                    //"Ret": 1100,
                    if (!value.Contains("\"Ret\": 0"))
                    {
                        ShowMsg("没有返回正确的数据，webwxinit错误!");
                        throw new Exception("没有返回正确的数据，webwxinit错误!");
                    }

                    //USER_INFO
                    USER_INFO = SubString(value.Replace("\r", "").Replace("\n", ""), "\"User\": {", "NickName");
                    USER_INFO = SubString(USER_INFO, "\"UserName\": \"", "\",");

                    label1.Text = USER_INFO;
                    USER_DI.Add(USER_INFO, "我自己");

                    //SyncKey
                    SyncKey = SubString(value.Replace("\r", "").Replace("\n", ""), "\"SyncKey\": ", "}]}");
                    SyncKey += "}]}";
#if DEBUG
                    this.textBox1.Text = SyncKey;
#endif
                }
                r.Close();


            }
        }


        //https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxgetvoice?msgid=8332473900244757099&skey=@crypt_39864b32_3b5756470b541ab03f03f519c0f1d2a9

        void UploadWxImage(string MsgID, string Form, string FormName, string ad)
        {
            try
            {
                //下载图片
                //https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxgetmsgimg?&MsgID=8055351800675473074&skey=%40crypt_671d6fec_1cd01b65296c06ef559ae98b0646725a&type=slave
                //image/png,image/*;q=0.8,*/*;q=0.5
                //&type=slave 获取缩略图的意思
                Image img = DownLoadImage(MsgID);

                //SendImage(Form, FormName, ad, img);

            }
            catch (Exception err)
            {
#if DEBUG
                SendMsg(Form, USER_INFO, err.Message, false);
#else
                SendMsg(Form, USER_INFO, "接收图片出现错误!", false);
#endif
            }
        }

        private Image DownLoadImage(string MsgID,string slave="&type=slave")
        {
            var url = "https://wx[number].qq.com/cgi-bin/mmwebwx-bin/webwxgetmsgimg?&MsgID={0}&skey={1}" + slave;


            HttpWebRequest h = (HttpWebRequest)HttpWebRequest.Create(ReplaceHeaderKey(string.Format(url, MsgID, step4xml.skey)));
            h.AllowAutoRedirect = false;
            h.CookieContainer = cookieContainer;
            h.Accept = "image/png,image/*;q=0.8,*/*;q=0.5";
            HttpWebResponse r = (HttpWebResponse)h.GetResponse();
            Image img = Image.FromStream(r.GetResponseStream());
            r.Close();
            return img;
        }

        //https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxgetvoice?msgid=8332473900244757099&skey=@crypt_39864b32_3b5756470b541ab03f03f519c0f1d2a9
        //private Image DownLoadvoice(string MsgID)
        //{
        //    var url = "https://wx[number].qq.com/cgi-bin/mmwebwx-bin/webwxgetvoice?&msgid={0}&skey={1}";


        //    HttpWebRequest h = (HttpWebRequest)HttpWebRequest.Create(ReplaceHeaderKey(string.Format(url, MsgID, step4xml.skey)));
        //    h.AllowAutoRedirect = false;
        //    h.CookieContainer = cookieContainer;
        //    //h.Accept = "image/png,image/*;q=0.8,*/*;q=0.5";
        //    HttpWebResponse r = (HttpWebResponse)h.GetResponse();
        //    MemoryStream ms = new MemoryStream(r.GetResponseStream());
        //    r.Close();
        //    return ms;
        //}

        

        void doStep7()
        {
            if (__7)
            {
                //https://webpush.weixin.qq.com/cgi-bin/mmwebwx-bin/synccheck?r=1444004057053&skey=%40crypt_39864b32_c5aaad7d38892d44fde5da7e97b32e69&sid=iM3uR2da1I2t0Upy&uin=840648442&deviceid=e013657496826621&synckey=1_634979925%7C2_634981902%7C3_634981851%7C11_634981845%7C201_1444003994%7C1000_1443952994&_=1444003858911
                //var url = "https://webpush.weixin.qq.com/cgi-bin/mmwebwx-bin/synccheck?r={time}&skey={SKEY}&sid={SID}&uin={UIN}&deviceid={DeviceID}&synckey=1_634979925%7C2_634981902%7C3_634981851%7C11_634981845%7C201_1444003994%7C1000_1443952994&_=1444003858911

                //var url = "https://wx2.qq.com/cgi-bin/mmwebwx-bin/webwxsync?sid={SID}&r={time}&skey={SKEY}";
                var url = "https://wx[number].qq.com/cgi-bin/mmwebwx-bin/webwxsync?sid={SID}&skey={SKEY}&lang=zh_CN&pass_ticket={pass_ticket}";

                HttpWebRequest h = (HttpWebRequest)HttpWebRequest.Create(ReplaceKey(url));
                h.AllowAutoRedirect = false;
                h.CookieContainer = cookieContainer;
                h.Method = "POST";
                h.Accept = "application/json, text/plain, */*";
                h.ContentType = "application/json;charset=utf-8";

                byte[] bs = Encoding.ASCII.GetBytes(ReplaceHeaderKey(@"{""BaseRequest"":{""Uin"":{UIN},""Sid"":""{SID}"",""Skey"":""{SKEY}"",""DeviceID"":""{DeviceID}""},""SyncKey"":{SyncKey},""rr"":{time}}"));
                using (Stream reqStream = h.GetRequestStream())
                {
                    reqStream.Write(bs, 0, bs.Length);
                    reqStream.Close();
                }

                HttpWebResponse r = (HttpWebResponse)h.GetResponse();

                //var list = GetAllCookies(cookieContainer);

                using (System.IO.StreamReader read = new System.IO.StreamReader(r.GetResponseStream()))
                {
                    string value = read.ReadToEnd();

                    if (value.IndexOf("\"SyncKey\": ") == -1) throw new Exception("SyncKey 没有捕获到");

                    SyncKey = SubString(value.Replace("\r", "").Replace("\n", ""), "\"SyncKey\": ", "}]}");
                    SyncKey += "}]}";

                    //得到用户的消息
                    JavaScriptObject ret = JavaScriptConvert.DeserializeObject(value) as JavaScriptObject;
                    JavaScriptArray arr = ret["AddMsgList"] as JavaScriptArray;
                    foreach (JavaScriptObject obj in arr)
                    {
                        var content = obj["Content"].ToString().Replace("&gt;", ">").Replace("&lt;", "<").Replace("<br/>", "");
                        ShowMsg(content, obj);

                        //处理消息
                        DoMsg(content, obj, obj["MsgId"]+"");
                    }
                }
                r.Close();
            }
        }

        void ShowMsg(string msg, JavaScriptObject obj = null)
        {
            DelegateFun.ExeControlFun(listBox1, new DelegateFun.delegateExeControlFun(delegate
            {
                //全部清除
                if (listBox1.Items.Count > 3000) listBox1.Items.Clear();

                Console.WriteLine("2=>" + msg);
                listBox1.Items.Add(DateTime.Now + ">2=>" + msg);
                if (obj != null)
                {
                    var _FormUserName = obj["FromUserName"] + "";
                    var _ToUserName = obj["ToUserName"] + "";
                    var FormUserName = GetDIName(_FormUserName);
                    var ToUserName = GetDIName(_ToUserName);

                    listBox1.Items.Add(DateTime.Now + ">" + FormUserName + ">" + ToUserName + ":" + msg);

                    this.listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                }

                //listBox1.

            }));
        }

        /// <summary>
        /// 给用户发送信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSend_Click(object sender, EventArgs e)
        {
            object user = lstBoxUser.SelectedItem;
            if (user == null)
            {
                MessageBox.Show("请选择用户！");
                return;
            }

            if (txtBoxMessage.Text == "")
            {
                MessageBox.Show("请输入信息！");
                return;
            }

            string userid = user.ToString().Substring(user.ToString().LastIndexOf('>')+1);

            SendMsg(userid, USER_INFO, txtBoxMessage.Text, false);

            txtBoxMessage.Text = "";
        }

        public string USER_NICKNAME { get; set; }

        private void btnGetUserList_Click(object sender, EventArgs e)
        {
            webwxgetcontact();
        }



        public Dictionary<string, string> COOKIES { get; set; }
    }
}
