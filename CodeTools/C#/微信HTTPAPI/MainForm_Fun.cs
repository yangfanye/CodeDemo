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
        string SubString(string objValue, string indexStr = "", string lastStr = "", string iDefault = "", bool throwE = false)
        {
            //Hashids hs = new Hashids();
            //string ss = hs.Encode(27920);

            //int[] dd = hs.Decode("0nzJSm");

            //ShortUrl("www.yunto.cc");
            try
            {
                int index = objValue.IndexOf(indexStr);
                if (lastStr != "" && index > -1)
                {
                    objValue = objValue.Remove(0, index);
                    index = objValue.IndexOf(indexStr);
                }
                int last = objValue.IndexOf(lastStr);
                last = last == 0 ? objValue.Length : last;
                if (index > -1 && last > -1)
                {
                    objValue = objValue.Substring(index + indexStr.Length, last - (index + indexStr.Length));
                    return objValue;
                }
                else
                {
                    return iDefault;
                }

            }
            catch (Exception error)
            {
                if (throwE) throw error;
                return iDefault;
            }
        }

        string GetDIName(object key)
        {
            string skey = key.ToString();
            if (USER_DI.ContainsKey(skey))
                return USER_DI[skey];

            return skey;
        }

        T Xml2Json<T>(string xml, string root = "error")
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            JavaScriptObject obj = new JavaScriptObject();
            foreach (XmlNode node in doc.SelectSingleNode(root).ChildNodes)
            {
                //获取内容
                obj[node.Name] = node.InnerText;
            }

            return JavaScriptConvert.DeserializeObject<T>(JavaScriptConvert.SerializeObject(obj));
        }

        /// <summary>
        /// 模拟机器码
        /// </summary>
        /// <returns></returns>
        static String generateDeviceId()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("e");
            int count = 15;//15个随机数字
            Random ran = new Random();
            for (int i = 0; i < count; i++)
            {
                int num = (int)(ran.Next(10));
                sb.Append(num);
            }

            return sb.ToString();
        }

        string GetBase64FromImage(System.Drawing.Image image)
        {
            string strbaser64 = "";
            try
            {
                MemoryStream ms = new MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                strbaser64 = Convert.ToBase64String(arr);
            }
            catch (Exception)
            {
                throw new Exception("Something wrong during convert!");
            }
            return strbaser64;
        }

        bool CheckUrl(string s)
        {
            //地址验证的不对
            s = s.Trim();

            string reg = @"(?<![\w@]+)((http|https)://)?(www.)?[a-z0-9\.]+(\.(com|net|cn|com\.cn|com\.net|net\.cn|\.cc|\.tv|\.org))(/[^\s\n<>]*)?";
            Regex r = new Regex(reg);
            //给网址去所有空格
            Match m = r.Match(s);

            return m.Success;
        }

    }
}