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
using System.Web;
using System.Windows.Forms;
using System.Xml;

namespace WeiXinZhuaFaWang
{
    public partial class MainForm : Form
    {
        Queue<ThreadDoUrl> ThreadDoUrls = new Queue<ThreadDoUrl>();
        private void DownLoadPage(
            string url, 
            string _FormUserName, 
            string FormUserName,
            string title,
                string des,
                    string img, string _ToUserName
          )
        {
            var robotname = ConfigurationManager.AppSettings["robotname"];

            //应该加入到线程里面
            ThreadDoUrls.Enqueue(new ThreadDoUrl()
            {
                url = url,
                _FormUserName = _FormUserName,
                FormUserName = FormUserName,
                title =title,
                des = des,
                img = img,
                _ToUserName = _ToUserName,
                USER_INFO = USER_INFO,
                USER_NICKNAME = robotname,
            });
        }

        /// <summary>
        /// 转换文章
        /// </summary>
        private void urldownload()
        {
            ShowMsg("urldownload");

            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
            {
                ThreadDoUrl one = null;
                var url = domain;
                while (true)
                {
                    Thread.Sleep(1000);
                    try
                    {
                        if (ThreadDoUrls != null && ThreadDoUrls.Count > 0)
                        {
                            one = ThreadDoUrls.Dequeue();
                            if (one != null)
                            {
                                using (WebClient wc = new WebClient())
                                {
                                    //编码问题
                                    wc.Encoding = Encoding.UTF8;
                                    var ret = wc.UploadString(url + "/zf/msg?t="+DateTime.Now,
                                    //var ret = wc.UploadString(url + "/api/G/msg?t=" + DateTime.Now,

                                    "post",
                                    JavaScriptConvert.SerializeObject(one)
                                    );




                                    //如果是0 转换失败
                                    //如果是1 代表不是我们能够转换的页面
                                    //其他代表成功!

                                    ret = ret.Replace("\"", "");
                                    if (ret != "")
                                    {
                                        //我直接发的，我再回一条，但是一定不能死循环
                                        if (one._FormUserName == USER_INFO && ret.StartsWith("@"))
                                            SendMsg(one._ToUserName, one._FormUserName, ret, false);
                                        else
                                            SendMsg(one._FormUserName, USER_INFO, ret, false);
                                    }
                                    //else
                                    //    SendMsg(USER_INFO, one._FormUserName, ret, false);

                                }
                            }
                        }
                    }
                    catch
                    {
                        //http://z.zituibao.com/zf/c?purl=http%3A%2F%2Fmp.weixin.qq.com%2Fs%3F__biz%3DMjM5MjE5ODA4MA%3D%3D%26amp%3Bamp%3Bmid%3D210881117%26amp%3Bamp%3Bidx%3D1%26amp%3Bamp%3Bsn%3Df2d15b68ea6f98b368ecf5b5fe642df7%26amp%3Bamp%3Bscene%3D1%26amp%3Bamp%3Bsrcid%3D1008hc3PiATP8hpzGr6XY305%23rd&uid=%4025ed09d5a631ffa69d9ea3c09f426cf7&name=%E8%AE%BE%E8%AE%A1%E7%9A%84%E7%81%B5%E6%84%9F

                        //SendMsg(one._FormUserName, USER_INFO, "转换文章失败,请重试", false);
                    }
                }
            }));
        }
    }

    class ThreadDoUrl
    {
        public string url = "";
        public string _FormUserName = "";
        public string FormUserName = "";
        /// <summary>
        /// 状态
        /// </summary>
        public int ZT = 0;
        public string title = "";
        public string des = "";
        public string img = "";

        public string _ToUserName = "";
        public string USER_INFO = "";

        public string USER_NICKNAME { get; set; }
    }
}