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
        readonly string domain = "http://ad.zituibao.com";

        //readonly string domain = "http://localhost:2310";


        //readonly string domain = "http://z.zituibao.com";

        static CookieContainer cookieContainer = new CookieContainer();

        static HttpClientHandler httphander = new HttpClientHandler()
        {
            CookieContainer = cookieContainer,
        };

        HttpClient httpclient = new HttpClient(httphander);
        //HttpClient clientCookies = new HttpClient(httphander);

        Dictionary<string, bool> USER_ROBOT = new Dictionary<string, bool>();
        /// <summary>
        /// 广告词语
        /// </summary>
        Dictionary<string, string> USER_AD = new Dictionary<string, string>();
        string SyncKey { get; set; }


        string DeviceID = generateDeviceId();
        string UUID = "";
        //第三步返回内容
        string redirect_uri = "";
        //最多循环50次
        int login_try_times = 50;

        string USER_INFO = "";
        JavaScriptObject USER_LIST = null;
        Dictionary<string, string> USER_DI = new Dictionary<string, string>();

        Step4XML step4xml = new Step4XML()
        {
            wxsid = "",
            wxuin = "",
            pass_ticket = ""
        };

        //五秒钟尝试
        public int sleeptime = 5000;

        bool __1 = true;
        bool __2 = true;
        bool __3 = true;
        bool __4 = true;
        bool __5 = true;
        bool __6 = true;
        bool __7 = true;
        bool __8 = true;
        bool __9 = true;

        #region
        string[] url_jslogin = new string[]{"https://login.weixin.qq.com/jslogin?appid=wx782c26e4c19acffb&redirect_uri=https%3A%2F%2Fwx.qq.com%2Fcgi-bin%2Fmmwebwx-bin%2Fwebwxnewloginpage&fun=new&lang=zh_CN&_={time}",
@"GET /jslogin?appid=wx782c26e4c19acffb&redirect_uri=https%3A%2F%2Fwx.qq.com%2Fcgi-bin%2Fmmwebwx-bin%2Fwebwxnewloginpage&fun=new&lang=zh_CN&_=1443970654738 HTTP/1.1
Accept: application/javascript, */*;q=0.8
X-HttpWatch-RID: 67677-10118
Referer: https://wx.qq.com/
Accept-Language: zh-CN
User-Agent: Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; WOW64; Trident/7.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; InfoPath.3; .NET4.0E; .NET4.0C)
Accept-Encoding: gzip, deflate
Host: login.weixin.qq.com
DNT: 1
Connection: Keep-Alive
Cookie: ts_uid=4570921168; pgv_pvid=189995969; o_cookie=7103505; pt2gguin=o0007103505; ptcz=b9dd44dd0c5b11e1b3c5043ecd23cc63f52dcf6bffd1814cc863d9865f0425c9; uin_cookie=7103505; euin_cookie=B2870E2F15842E9BCF464033FC04602D53918CC9D8884358; pgv_pvi=7438691328; pgv_si=s6838500352

"};

        //https://login.weixin.qq.com/qrcode/QcNn6FzWLA==
        //4d7YDcIMSg==
        string[] url_qrcode = new string[]{"https://login.weixin.qq.com/qrcode/{uuid}",
@"GET /qrcode/AegnpZOtug== HTTP/1.1
Accept: image/png, image/svg+xml, image/*;q=0.8, */*;q=0.5
X-HttpWatch-RID: 67677-10024
Referer: https://wx.qq.com/
Accept-Language: zh-CN
User-Agent: Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; WOW64; Trident/7.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; InfoPath.3; .NET4.0E; .NET4.0C)
Accept-Encoding: gzip, deflate
Host: login.weixin.qq.com
DNT: 1
Connection: Keep-Alive
Cookie: ts_uid=4570921168; pgv_pvid=189995969; o_cookie=7103505; pt2gguin=o0007103505; ptcz=b9dd44dd0c5b11e1b3c5043ecd23cc63f52dcf6bffd1814cc863d9865f0425c9; uin_cookie=7103505; euin_cookie=B2870E2F15842E9BCF464033FC04602D53918CC9D8884358; pgv_pvi=7438691328; pgv_si=s6838500352"};


        string[] url_login = new string[] {"https://login.weixin.qq.com/cgi-bin/mmwebwx-bin/login?loginicon=true&uuid={uuid}&tip=0&r=-865889493&_={time}",
@"GET /cgi-bin/mmwebwx-bin/login?loginicon=true&uuid=4d64FS_75g==&tip=0&r=-865889493&_=1443974893349 HTTP/1.1
Accept: application/javascript, */*;q=0.8
X-HttpWatch-RID: 67677-10503
Referer: https://wx.qq.com/
Accept-Language: zh-CN
User-Agent: Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; WOW64; Trident/7.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; InfoPath.3; .NET4.0E; .NET4.0C)
Accept-Encoding: gzip, deflate
Host: login.weixin.qq.com
DNT: 1
Connection: Keep-Alive
Cookie: ts_uid=4570921168; pgv_pvid=189995969; o_cookie=7103505; pt2gguin=o0007103505; ptcz=b9dd44dd0c5b11e1b3c5043ecd23cc63f52dcf6bffd1814cc863d9865f0425c9; uin_cookie=7103505; euin_cookie=B2870E2F15842E9BCF464033FC04602D53918CC9D8884358; pgv_pvi=7438691328; pgv_si=s6838500352

" };

//        string[] url_webwxnewloginpage = new string[] {"https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxnewloginpage?ticket=Af1iye6oyvBLib2BAc8aEG-b@qrticket_0&uuid=4d64FS_75g==&lang=zh_CN&scan=1443974894&vcdataticket=AQYZMze9_5bsQnJtxcmyPe1r&vccdtstr=FbHHynSe1sdnN71bV-12XIkFcQjsbQmPH7r8NLdPn0oj-ft80x8b_ME7L0VXH8Qm&fun=new&version=v2",
//            @"GET /cgi-bin/mmwebwx-bin/webwxnewloginpage?ticket=Aco1LsN8v7b4Ue3-kMT-ro8H@qrticket_0&uuid=Idn0MO6-9g==&lang=zh_CN&scan=1444028080&vcdataticket=AQZoDF15CbSRIU9ajqWf94ww&vccdtstr=62SF1YoRawM7Nbao3yX-oj4sSkkTU0oANQq4eOkq9AccEg2dQFw7hNN7XHupfAQk&fun=new&version=v2&lang=zh_CN HTTP/1.1
//Accept: application/json, text/plain, */*
//Referer: https://wx.qq.com/?&lang=zh_CN
//Accept-Language: zh-CN
//Accept-Encoding: gzip, deflate
//User-Agent: Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; WOW64; Trident/7.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; InfoPath.3; .NET4.0E; .NET4.0C)
//Host: wx.qq.com
//DNT: 1
//Connection: Keep-Alive
//Cookie: webwxuvid=bebe27f97a88e8ce573a38b4c48984e847e73109fd973bdeb4833b566a20b421d94184581fa4a9c8165a9cfab4b5626f; mm_lang=zh_CN; wxuin=840648442; wxloadtime=1444027826_expired; wxpluginkey=1444008722; MM_WX_NOTIFY_STATE=1; MM_WX_SOUND_STATE=1; pgv_pvid=189995969; o_cookie=7103505; pt2gguin=o0007103505; ptcz=b9dd44dd0c5b11e1b3c5043ecd23cc63f52dcf6bffd1814cc863d9865f0425c9; pgv_pvi=7438691328; pgv_si=s885712896; pgv_info=ssid=s5205730367
//
//"};

        string WXNUMBER = "";

        string[] url_webwxinit = new string[] { "https://wx[number].qq.com/cgi-bin/mmwebwx-bin/webwxinit?r={time}&pass_ticket={pass_ticket}",
        @"POST /cgi-bin/mmwebwx-bin/webwxinit?r=-894847475&pass_ticket=Vyzx6Ch5dPR0509GTBB4XzQGXg34x1kQYJt7FV7XiOC8rbOIoI3aWwF59fRZlhr%252F HTTP/1.1
Accept: application/json, text/plain, */*
X-HttpWatch-RID: 27649-10012
Content-Type: application/json;charset=utf-8
Referer: https://wx.qq.com/
Accept-Language: zh-CN
Accept-Encoding: gzip, deflate
User-Agent: Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; WOW64; Trident/7.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; InfoPath.3; .NET4.0E; .NET4.0C)
Host: wx.qq.com
Content-Length: 148
DNT: 1
Connection: Keep-Alive
Cache-Control: no-cache
Cookie: webwxuvid=bebe27f97a88e8ce573a38b4c48984e847e73109fd973bdeb4833b566a20b421d94184581fa4a9c8165a9cfab4b5626f; mm_lang=zh_CN; wxuin=840648442; wxloadtime=1444003886; wxpluginkey=1443952994; MM_WX_NOTIFY_STATE=1; MM_WX_SOUND_STATE=1; wxsid=iM3uR2da1I2t0Upy; pgv_pvid=189995969; o_cookie=7103505; pt2gguin=o0007103505; ptcz=b9dd44dd0c5b11e1b3c5043ecd23cc63f52dcf6bffd1814cc863d9865f0425c9; uin_cookie=7103505; euin_cookie=B2870E2F15842E9BCF464033FC04602D53918CC9D8884358; pgv_pvi=7438691328; pgv_si=s885712896; pgv_info=ssid=s5205730367; webwx_data_ticket=AQbc2BlPz0LJ729EJSc+SzAQ

{""BaseRequest"":{""Uin"":""{UIN}"",""Sid"":""{SID}"",""Skey"":""{SKEY}"",""DeviceID"":""{DeviceID}""}}"};


        string[] webwxuploadmedia0 = new string[] { "https://file.wx.qq.com/cgi-bin/mmwebwx-bin/webwxuploadmedia?f=json",@"POST /c
Host: file.wx.qq.com
Connection: keep-alive
Access-Control-Request-Method: POST
Origin: https://wx.qq.com
User-Agent: Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.106 Safari/537.36
Access-Control-Request-Headers: content-type
Accept: */*
Referer: https://wx.qq.com/?&lang=zh_CN
Accept-Encoding: gzip, deflate, sdch
Accept-Language: zh-CN,zh;q=0.8"};

        string[] webwxuploadmedia1 = new string[] { "https://file.wx.qq.com/cgi-bin/mmwebwx-bin/webwxuploadmedia?f=json",@"POST /c
Host: file.wx.qq.com
Connection: keep-alive
Content-Length: 3386
Origin: https://wx.qq.com
User-Agent: Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.106 Safari/537.36
Content-Type: multipart/form-data; boundary=----WebKitFormBoundary96Jf64ucteclnmSP
Accept: */*
Referer: https://wx.qq.com/?&lang=zh_CN
Accept-Encoding: gzip, deflate
Accept-Language: zh-CN,zh;q=0.8"};

        string webwxuploadmedia1body = @"-----------------------------7e0d1f760df0
Content-Disposition: form-data; name=""uploadmediarequest""

{""BaseRequest"":{""Uin"":{UIN},""Sid"":""{SID}"",""Skey"":""{SKEY}"",""DeviceID"":""{DeviceID}""},""ClientMediaId"":{time}366,""TotalLen"":[CD],""StartPos"":0,""DataLen"":[CD],""MediaType"":4}
-----------------------------7e0d1f760df0
Content-Disposition: form-data; name=""filename""; filename=""webwxgetvoice.mp3""
Content-Type: audio/mpeg

";
        string webwxuploadmedia1body2 = @"{""BaseRequest"":{""Uin"":{UIN},""Sid"":""{SID}"",""Skey"":""{SKEY}"",""DeviceID"":""{DeviceID}""},""ClientMediaId"":{time},""TotalLen"":[CD],""StartPos"":0,""DataLen"":[CD],""MediaType"":4}";

        string webwxuploadmedia1body0 = @"-----------------------------7e0d1f760df0
Content-Disposition: form-data; name=""id""

WU_FILE_0
-----------------------------7e0d1f760df0
Content-Disposition: form-data; name=""name""

webwxgetvoice.mp3
-----------------------------7e0d1f760df0
Content-Disposition: form-data; name=""""

audio/mpeg
-----------------------------7e0d1f760df0
Content-Disposition: form-data; name=""size""

1906
-----------------------------7e0d1f760df0
Content-Disposition: form-data; name=""mediatype""

doc
-----------------------------7e0d1f760df0
Content-Disposition: form-data; name=""uploadmediarequest""

{""BaseRequest"":{""Uin"":{UIN},""Sid"":""{SID}"",""Skey"":""{SKEY}"",""DeviceID"":""{DeviceID}""},""ClientMediaId"":{time},""TotalLen"":[CD],""StartPos"":0,""DataLen"":[CD],""MediaType"":4}
-----------------------------7e0d1f760df0
Content-Disposition: form-data; name=""webwx_data_ticket""

{webwx_data_ticket}
-----------------------------7e0d1f760df0
Content-Disposition: form-data; name=""pass_ticket""

{pass_ticket}
-----------------------------7e0d1f760df0
Content-Disposition: form-data; name=""filename""; filename=""webwxgetvoice.mp3""
Content-Type: audio/mpeg";

     

        #endregion
    }
}