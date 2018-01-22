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
using System.Web;
using System.Windows.Forms;
using System.Xml;

namespace WeiXinZhuaFaWang
{
    public partial class MainForm : Form
    {

        /// <summary>
        /// 收钱发送到远程服务器处理
        /// </summary>
        /// <param name="Form"></param>
        /// <param name="FormName"></param>
        /// <param name="ad"></param>
        /// <param name="img"></param>
        void SendMoney(string Form, string FormName, string msg)
        {
            var xm = SubString(msg,"<des><![CDATA[", "向你付钱成功，已存入零钱时间");

            //z.etuling.com:10001/money
            //把图片上传到服务器上。
            using (WebClient wc = new WebClient())
            {
                wc.Encoding = Encoding.UTF8;
                var ret = wc.UploadString("http://hz1.yunto.cc/money2?t=" + DateTime.Now ,
                    "post",

                    JavaScriptConvert.SerializeObject(new{
                    Form = Form,
                    FormName = FormName,
                    msg = msg
                    }));



                if (ret == "")
                    ret = "处理授权失败";

                try
                {
                    var userform = USER_DI.Where(o => o.Value == xm).Select(o => { return o.Key; }).FirstOrDefault();
                    if (userform != null)
                        SendMsg(userform, USER_INFO, ret, false);
                }
                catch { }
            }
        }

        void SendJiHuo(string Form, string FormName, string msg)
        {

            SendMsg(Form, USER_INFO, "正在激活...", false);
            //z.etuling.com:10001/money
            //把图片上传到服务器上。
            using (WebClient wc = new WebClient())
            {
                wc.Encoding = Encoding.UTF8;
                var ret = wc.UploadString("http://hz1.yunto.cc/jihuo2?t=" + DateTime.Now,
                    "post",

                    JavaScriptConvert.SerializeObject(new
                    {
                        Form = Form,
                        FormName = FormName,
                        msg = msg
                    }));



                if (ret == "")
                    ret = "处理激活失败";
                else
                {
                    Thread.Sleep(1000);
                    SendMsg(Form, USER_INFO, ret, false);
                }
             
            }
        }


        ///// <summary>
        ///// 发送远程服务器图片
        ///// </summary>
        ///// <param name="Form"></param>
        ///// <param name="FormName"></param>
        ///// <param name="ad"></param>
        ///// <param name="img"></param>
        //void SendImage(string Form, string FormName, string ad, Image img)
        //{
        //    //把图片上传到服务器上。
        //    using (WebClient wc = new WebClient())
        //    {
        //        wc.Encoding = Encoding.UTF8;
        //        var byt = wc.UploadData(domain + "/zf/u?t=" + DateTime.Now + "&uid=" + HttpUtility.UrlEncode(Form) + "&name=" + HttpUtility.UrlEncode(FormName) + "&ad=" + System.Web.HttpUtility.UrlEncode(ad),
        //            Encoding.UTF8.GetBytes(GetBase64FromImage(img)));
        //        var ret = Encoding.UTF8.GetString(byt);


        //        if (ret == "")
        //            ret = "图片处理失败，请重新发送图片";

        //        SendMsg(Form, USER_INFO, ret, false);
        //    }
        //}

        /// <summary>
        /// 修改远程服务器链接
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="Form"></param>
        /// <param name="FormName"></param>
        /// <param name="ad"></param>
        //void ChangeUrl(string msg, string Form, string FormName, string ad)
        //{
        //    try
        //    {
        //        using (WebClient wc = new WebClient())
        //        {
        //            wc.Encoding = Encoding.UTF8;
        //            var ret = wc.UploadString(domain + "/zf/a?t=" + DateTime.Now,
        //                "post",
        //                "uid=" + HttpUtility.UrlEncode(Form) + "&name=" + HttpUtility.UrlEncode(FormName) + "&ad=" + HttpUtility.UrlEncode(ad) + "&purl=" + HttpUtility.UrlEncode(msg)
        //                );

        //            if(ret != "")
        //                SendMsg(Form, USER_INFO, ret, false);
        //        }
        //    }
        //    catch
        //    {
        //        SendMsg(Form, USER_INFO, "发送广告链接出现错误!", false);
        //    }
        //}
    }
}