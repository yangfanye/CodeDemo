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
        /// <summary>
        /// 远程调用小I 获取交谈内容
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private string GetMSG(string msg, string yonghu)
        {
            try
            {
                var content = "";
                using (WebClient wb = new WebClient())
                {
                    wb.Encoding = Encoding.UTF8;
                    content = wb.DownloadString("http://www.tuling123.com/openapi/api?userid=" + yonghu + "&key=335d358c3d4e94fd0f8fd3e13dcd81e1&info=" +
                        msg
                        );
                }

                JavaScriptObject obj = JavaScriptConvert.DeserializeObject(content) as JavaScriptObject;

                return obj["text"] + "";
            }
            catch
            {
                return "回答不了!";
            }
        }
    }
}