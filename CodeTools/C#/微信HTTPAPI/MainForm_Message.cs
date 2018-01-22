using FluorineFx.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        /// <summary>
        /// 处理消息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="obj"></param>
        void DoMsg(string msg, JavaScriptObject obj, string MsgId)
        {
            var _FormUserName = obj["FromUserName"] + "";
            var _ToUserName = obj["ToUserName"] + "";
            var FormUserName = GetDIName(_FormUserName);
            var ToUserName = GetDIName(_ToUserName);

            /*
            //启动机器人模式
            if (msg == "闭嘴" || msg == "张嘴" || USER_ROBOT.ContainsKey(_FormUserName))
            {
                if (msg == "闭嘴")
                {
                    USER_ROBOT[_FormUserName] = false;
                    SendMsg(_FormUserName, _ToUserName, "已经关闭机器人!", false);
                    USER_ROBOT.Remove(_FormUserName);
                    return;
                }

                if (msg == "张嘴")
                {
                    USER_ROBOT[_FormUserName] = true;
                    SendMsg(_FormUserName, _ToUserName, "已经打开机器人!", false);
                    return;
                }

                //机器人对话
                SendMsg(_FormUserName, _ToUserName, msg);
            }


            //处理消息反馈
            if (msg.StartsWith("<msg><op id='1'>")) return;

            //处理广告图片
            if (msg.StartsWith("<msg><img length="))
            {
                //if (USER_AD.ContainsKey(_FormUserName))
                //{
                //    var MsgID = obj["MsgId"] + "";
                //    SendMsg(_FormUserName, _ToUserName, "正在处理广告，请稍后...", false);
                //    UploadWxImage(MsgID, _FormUserName, FormUserName, USER_AD[_FormUserName]);
                //    Console.WriteLine("3=>" + MsgID);
                //}
                return;
            }
            */

            //处理接收到语音
            //"Content": "&lt;msg&gt;&lt;voicemsg endflag=\"1\" cancelflag=\"0\" forwardflag=\"0\" voiceformat=\"4\" voicelength=\"1540\" length=\"2789\" bufid=\"219566597153423785\" clientmsgid=\"49343fff7846e778c6fbdc3b8925dedewxid_ccveannudood212913_1452169862\" fromusername=\"wxid_ywwn0puqkhms22\" /&gt;&lt;/msg&gt;",

            
            var money = ConfigurationManager.AppSettings["ismoney"];
            if (money != null && money.ToString() == "1")
            {

                //处理支付信息
                if (msg.Contains("面对面收钱到账通知"))
                {
                    SendMsg(_FormUserName, _ToUserName, "正在处理收款信息...", false);
                    SendMoney(_FormUserName, FormUserName, msg);
                    return;
                }

                if (msg.Contains("刚刚把你添加到通讯录，现在可以开始聊天了。") || msg.Contains("我要激活"))
                {
                    SendMsg(_FormUserName, _ToUserName, "正在处理激活流程信息...", false);
                    SendJiHuo(_FormUserName, FormUserName, msg);
                    return;
                }
            }

            var title = "";
            var des = "";
            var img = "";

            //处理连接
            if (msg.StartsWith("<msg>") && msg.Contains("<url>"))
            {
                //如何得到原来的标题和描述
                ShowMsg(msg);
                var url = SubString(msg, "<url>", "</url>");
                //键	值
                //请求	GET /cgi-bin/mmwebwx-bin/webwxgetmsgimg?&MsgID=567846155428648917&skey=%40crypt_688681e4_72dbe72e2947fd5ecbca14ab4637d962&type=slave HTTP/1.1
                //下载图片
                Console.WriteLine("4=>" + url);

                //正在转换文章
                //if (url.StartsWith("<![CDATA", StringComparison.OrdinalIgnoreCase)) return;

                if (!url.StartsWith("<![CDATA", StringComparison.OrdinalIgnoreCase) && !url.StartsWith("http://mp.weixin.qq.com", StringComparison.OrdinalIgnoreCase))
                {
                    title = SubString(msg, "<title>", "</title>");
                    des = SubString(msg, "<des>", "</des>");
                    img = GetBase64FromImage(DownLoadImage(MsgId));
                }

                
                //SendMsg(_FormUserName, _ToUserName, "正在转换文章，请稍后...", false);
                DownLoadPage(msg, _FormUserName, FormUserName, title, des, img,_ToUserName);

                return;
            }
            else
            {
                ShowMsg(msg);
                DownLoadPage(msg, _FormUserName, FormUserName, title, des, img, _ToUserName);
            }

            /*
            //未识别消息
            if (msg.StartsWith("<msg>"))
            {
                return;
            }

            //如果是自己，就直接退出
            if (_FormUserName == USER_INFO)
            {
                Console.WriteLine("5=>" + _FormUserName + ">" + _ToUserName);
                return;
            }

            if (_FormUserName.StartsWith("@@")
            || _ToUserName.StartsWith("@@"))
            {
                Console.WriteLine("6=>" + _FormUserName + ">" + _ToUserName);
                return;
            }

            //微信团队不要发
            if (FormUserName.Contains("微信团队") )
            {
                //Console.WriteLine("5=>" + _FormUserName + ">" + _ToUserName);
                return;
            }

            //如果来自微信团队，也不要回复内容

            //if (Regex.Match(msg,"^@[a-z0-9]+:").Success)
            //{
            //    Console.WriteLine("5=>" + _FormUserName + ">" + _ToUserName);
            //    return;
            //}
            //遇到这三个关键词，进行广告提示
            //if (msg == "1" || msg == "2" || msg == "3")
            //{
            //    USER_AD[_FormUserName] = msg;
            //    SendMsg(_FormUserName, _ToUserName, "请发送广告图片过来，建议大小为640x119", false);
            //    return;
            //}

            //如果是最后是连接地址
            //if (USER_AD.ContainsKey(_FormUserName))
            //{
            //    //判断msg是否是url地址，如果是url地址，就认为是修改链接地址
            //    if (CheckUrl(msg))
            //    {
            //        ChangeUrl(msg, _FormUserName, FormUserName, USER_AD[_FormUserName]);
            //        USER_AD.Remove(_FormUserName);
            //        return;
            //    }

            //    USER_AD.Remove(_FormUserName);

            //}

            //说任何话，都先发一个帮助
            ShowHelp(msg, _FormUserName, _ToUserName);
            */
        }

        private bool ShowHelp(string msg, string _FormUserName, string _ToUserName)
        {
//            if (msg == "10")
//            {
//                var neirong = @"------ 开始提示 ------
//请直接在微信里面反馈问题，也可以加QQ:771065251 反馈问题
//也可以访问我们的网站:z.zituibao.com
//";
//                SendMsg(_FormUserName, _ToUserName, neirong, false);
//                return true;
//            }

//            if (msg == "11")
//            {
//                var neirong = @"------ 开始提示 ------
//广告加互联网科技有限公司，专注微信第三方扩展及应用。
//";
//                SendMsg(_FormUserName, _ToUserName, neirong, false);
//                return true;
//            }

//            if (msg == "12")
//            {
//                var neirong = @"------ 开始提示 ------
//加盟请联系:tel 13153256623。
//";
//                SendMsg(_FormUserName, _ToUserName, neirong, false);
//                return true;
//            }

//            if (msg == "00")
//            {
//                var neirong = @"------ 开始提示 ------
//未开放功能，请稍后。
//";
//                SendMsg(_FormUserName, _ToUserName, neirong, false);
//                return true;
//            }

            if (msg == "0")
            {
                var neirong = @"------ 开始提示 ------
* <<广告+>>给您带来分享新体验；
* 如需帮助、更改广告、操作说明，请回复“0” 

------ 功能提示 ------
广告家介绍
www.etuling.com/hello
更换广告
www.etuling.com/set
广告统计
www.etuling.com/r
视频讲解
www.etuling.com/tv


";
//                ------ 其他信息 ------
//问题反馈
//www.etuling.com/q
//关于我们
//www.etuling.com/about
//诚邀加盟
//www.etuling.com/add
//高级设置
//www.etuling.com/option
                SendMsg(_FormUserName, _ToUserName, neirong.Replace("\r","\n").Replace("\n\n","\n"), false);
                return true;
            }

            return true;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="FromUserName">接收者 不要搞错了</param>
        /// <param name="ToUserName">发送者</param>
        /// <param name="msg">消息</param>
        /// <param name="robot">true 机器人 false 普通用户</param>
        private void SendMsg(string FromUserName, string ToUserName, string msg, bool robot = true)
        {
            //发消息
            var p = @"{""BaseRequest"":{""Uin"":{UIN},""Sid"":""{SID}"",""Skey"":""{SKEY}"",""DeviceID"":""{DeviceID}""},""Msg"":{""Type"":1,""Content"":""{msg}"",""FromUserName"":""{FromUserName}"",""ToUserName"":""{ToUserName}"",""LocalID"":""{LocalID}"",""ClientMsgId"":""{ClientMsgId}""}}";

            var url = "https://wx[number].qq.com/cgi-bin/mmwebwx-bin/webwxsendmsg?pass_ticket={pass_ticket}";
            var temp = ReplaceKey(url);

            HttpWebRequest h = (HttpWebRequest)HttpWebRequest.Create(temp);
            h.AllowAutoRedirect = false;
            h.CookieContainer = cookieContainer;
            h.Method = "POST";
            h.Accept = "application/json, text/plain, */*";
            h.ContentType = "application/json;charset=utf-8";

            temp = p;
            temp = ReplaceHeaderKey(temp);

            var _msg = msg;
            if (robot)
                _msg = GetMSG(msg, ToUserName);

            temp = temp.Replace("{msg}", _msg);
            
            //对外输出信息
            ShowMsg(msg);

            temp = temp.Replace("{FromUserName}", ToUserName);
            temp = temp.Replace("{ToUserName}", FromUserName);
            temp = temp.Replace("{LocalID}", DateTimeToStamp(DateTime.Now) + "");
            Thread.Sleep(1);
            temp = temp.Replace("{ClientMsgId}", DateTimeToStamp(DateTime.Now) + "");

            byte[] bs = Encoding.UTF8.GetBytes(temp);
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

            }
            r.Close();

        }
    }
}