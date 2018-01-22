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
        void SendHeader(HttpClient client, string header)
        {
            string[] arr = header.Replace("\r", "\n").Replace("\n\n", "\n").Split('\n');
            foreach (string s in arr)
            {
                if (string.IsNullOrEmpty(s)) break;
                if (s.StartsWith("GET", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }
                if (s.StartsWith("POST", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }
                

                if (s.StartsWith("Cookie", StringComparison.OrdinalIgnoreCase)) continue;
                int x = s.IndexOf(":");
                if (x > 0)
                {
                    try
                    {
                        var k = s.Substring(0, x);
                        client.DefaultRequestHeaders.Remove(k);
                        client.DefaultRequestHeaders.Add(k, s.Substring(x + 1));
                    }
                    catch { }
                }
            }
        }

        int DateTimeToStamp(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }

        /// <summary>
        /// "\"(.*?)\""
        /// </summary>
        /// <param name="result"></param>
        /// <param name="code"></param>
        /// <param name="regex"></param>
        /// <returns></returns>
        string GetCodeString(string result, string code, string regex)
        {
            if (result.IndexOf(code) != -1)
            {
                Regex reg = new Regex(regex);
                Match m = reg.Match(result);
                while (m.Success)
                {
                    return m.Groups[1].Value;
                }
            }

            //没有找到
            throw new Exception("GetCodeString err");
        }

        string GetResultString(string result, string regex)
        {
            Regex reg = new Regex(regex);
            Match m = reg.Match(result);
            while (m.Success)
            {
                return m.Groups[1].Value;
            }

            return "";
        }

        string GetDeflateByStream(Stream stream, string encoding = "UTF-8")
        {
            List<Byte> list = new List<byte>();
            int b = 0;
            while ((b = stream.ReadByte()) != -1)
                list.Add((byte)b);

            string result = "";
            using (MemoryStream ms = new MemoryStream(list.ToArray()))
            {
                using (DeflateStream zipStream = new DeflateStream(ms, CompressionMode.Decompress))
                using (StreamReader sr = new StreamReader(zipStream, Encoding.GetEncoding(encoding)))
                    result = sr.ReadToEnd();
            }
            //
            return result;
        }


        string ReplaceKey(string s)
        {
            s = s.Replace("{time}", DateTimeToStamp(DateTime.Now) + "");
            s = s.Replace("{uuid}", UUID + "");
            s = s.Replace("{pass_ticket}", step4xml.pass_ticket);

            s = s.Replace("{UIN}", step4xml.wxuin + "");
            s = s.Replace("{SID}", step4xml.wxsid + "");
            s = s.Replace("{DeviceID}", DeviceID + "");
            s = s.Replace("{SKEY}", step4xml.skey + "");
            s = s.Replace("[number]", WXNUMBER);
            return s;
        }

        string ReplaceHeaderKey(string s)
        {
            s = s.Replace("{webwx_data_ticket}", COOKIES["webwx_data_ticket"]);
            s = s.Replace("{UIN}", step4xml.wxuin + "");
            s = s.Replace("{SID}", step4xml.wxsid + "");
            s = s.Replace("{DeviceID}", DeviceID + "");
            s = s.Replace("{SKEY}", step4xml.skey + "");
            s = s.Replace("{time}", DateTimeToStamp(DateTime.Now) + "");
            s = s.Replace("{pass_ticket}", step4xml.pass_ticket);
            s = s.Replace("{SyncKey}", SyncKey + "");
            s = s.Replace("[number]", WXNUMBER);

            return s;
        }
    }

    public class Step4XML
    {
        public int ret { get; set; }
        public string message { get; set; }
        public string skey { get; set; }
        public string wxsid { get; set; }
        public string wxuin { get; set; }
        public string pass_ticket { get; set; }
        public string isgrayscale { get; set; }
    }
}