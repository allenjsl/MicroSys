using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;
using System.Globalization;
using System.Text.RegularExpressions;
using System.IO;

namespace EyouSoft.Toolkit
{
    #region Utils
    /// <summary>
    /// utils
    /// </summary>
    public class Utils
    {
        #region static constants
        //static constants
        /// <summary>
        /// 允许上传的文件类型
        /// </summary>
        public const string UploadFileExtensions = "*.xls;*.rar;*.pdf;*.doc;*.swf;*.jpg;*.gif;*.jpeg;*.png;*.dot;*.bmp;*.zip;*.7z;*.docx;*.xlsx";
        #endregion

        #region private members
        /// <summary>
        /// 确保用户的输入没有恶意代码
        /// </summary>
        /// <param name="s">要过滤的字符串</param>
        /// <param name="maxLength">最大长度</param>
        /// <returns>过滤后的字符串</returns>
        static string GetString(string s, int maxLength)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            s = s.Trim();
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            if (s.Length > maxLength)
            {
                s = s.Substring(0, maxLength);
            }
            //text = Regex.Replace(text, "[\\s]{2,}", " ");	//将连续的空格转换为一个空格
            s = Regex.Replace(s, "(<[b|B][r|R]/*>)+|(<[p|P](.|\\n)*?>)", "\n");	//<br>
            s = Regex.Replace(s, "(\\s*&[n|N][b|B][s|S][p|P];\\s*)+", " ");	//&nbsp;
            s = Regex.Replace(s, "<(.|\\n)*?>", string.Empty);	//any other tags
            s = s.Replace("'", "''");
            return s;
        }

        /// <summary>
        /// 确保用户的输入没有恶意代码
        /// </summary>
        /// <param name="s">要过滤的字符串</param>
        /// <returns>过滤后的字符串</returns>
        static string GetString(string s)
        {
            return GetString(s, int.MaxValue);
        }

        /// <summary>
        /// 获取表单的值
        /// </summary>
        /// <param name="key">表单的key</param>
        /// <param name="maxLength">接受的最大长度</param>
        /// <returns></returns>
        static string GetFormValue(string key, int maxLength)
        {
            var s = HttpContext.Current.Request.Form[key];
            if (string.IsNullOrEmpty(s)) return string.Empty;

            return GetString(s, maxLength);
        }
        #endregion

        #region public members
        /// <summary>
        /// 获取网站根目录的绝对地址。
        /// </summary>
        /// <value>返回的地址以'/'结束.</value>
        public static Uri AbsoluteWebRoot
        {
            get
            {
                HttpContext context = HttpContext.Current;
                if (context == null)
                    throw new System.Net.WebException("The current HttpContext is null");

                if (context.Items["absoluteurl"] == null)
                    context.Items["absoluteurl"] = new Uri(context.Request.Url.GetLeftPart(UriPartial.Authority) + RelativeWebRoot);

                return context.Items["absoluteurl"] as Uri;
            }
        }

        private static string _RelativeWebRoot;

        /// <summary>
        /// 获取网站根目录的相对路径。
        /// </summary>
        /// <value>返回的地址以'/'结束.</value>
        public static string RelativeWebRoot
        {
            get
            {
                if (_RelativeWebRoot == null)
                    _RelativeWebRoot = VirtualPathUtility.ToAbsolute("~/");

                return _RelativeWebRoot;
            }
        }

        /// <summary>
        /// 获得当前绝对路径
        /// </summary>
        /// <param name="strPath">指定的路径</param>
        /// <returns>绝对路径</returns>
        public static string GetMapPath(string strPath)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            else //非web程序引用
            {
                strPath = strPath.Replace("/", "\\");
                if (strPath.IndexOf("\\") == 0)
                {
                    strPath = strPath.Substring(1);
                }
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
            }
        }

        /// <summary>
        /// 取得客户的IP数据
        /// </summary>
        /// <returns>客户的IP</returns>
        public static string GetRemoteIP()
        {
            string Remote_IP = "";
            try
            {
                if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                {
                    Remote_IP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                }
                else
                {
                    Remote_IP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                }
            }
            catch
            {
            }
            return Remote_IP;
        }

        /// <summary>
        /// 获取当前页面地址
        /// </summary>
        /// <returns></returns>
        public static string GetRequestUrl()
        {
            string RequestUrl = "";
            try
            {
                if (HttpContext.Current.Request.Url != null)
                {
                    RequestUrl = HttpContext.Current.Request.Url.ToString();
                }
            }
            catch
            {
            }
            return RequestUrl;
        }

        /// <summary>
        /// 替换XML敏感字符
        /// </summary>
        /// <param name="s">输入字符串</param>
        /// <returns></returns>
        public static string ReplaceXmlSpecialCharacter(string s)
        {
            //Replace("", string.Empty);  处理特殊字符的 你看不到不代表没有，不是空替换空
            if (!string.IsNullOrEmpty(s))
            {
                return
                    s.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("'", "&apos;").Replace(
                        "\"", "&quot;").Replace("", string.Empty);
            }

            return s;
        }

        /// <summary>
        /// 将字符串转化为数字(无符号整数) 若值不是数字返回defaultValue
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int GetInt(string s, int defaultValue)
        {
            if (string.IsNullOrEmpty(s))
            {
                return defaultValue;
            }

            int result = 0;
            bool b = Int32.TryParse(s, out result);

            if (b) return result;

            return defaultValue;
        }

        /// <summary>
        /// 将字符串转化为数字(无符号整数) 若值不是数字返回0
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int GetInt(string s)
        {
            return GetInt(s, 0);
        }

        /// <summary>
        /// get Nullable<int>
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int? GetIntNullable(string s, int? defaultValue)
        {
            if (string.IsNullOrEmpty(s)) return defaultValue;

            int result = 0;
            bool b = int.TryParse(s, out result);

            if (b) return result;

            return defaultValue;
        }

        /// <summary>
        /// get Nullable<int>
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int? GetIntNullable(string s)
        {
            if (string.IsNullOrEmpty(s)) return null;

            int result = 0;
            bool b = int.TryParse(s, out result);

            if (b) return result;

            return null; ;
        }

        /// <summary>
        /// 将字符串转化为decimal 若值不是数字返回defaultValue
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static decimal GetDecimal(string key, decimal defaultValue)
        {
            if (string.IsNullOrEmpty(key))
            {
                return defaultValue;
            }

            decimal result = 0;
            bool b = decimal.TryParse(key, out result);

            if (b) return result;

            return defaultValue;
        }

        /// <summary>
        /// 将字符串转化为decimal 若值不是数字返回0
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static decimal GetDecimal(string s)
        {
            return GetDecimal(s, 0);
        }

        /// <summary>
        /// 将字符串转化为double 若值不是数字返回defaultValue
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static double GetDouble(string s, double defaultValue)
        {
            if (string.IsNullOrEmpty(s))
            {
                return defaultValue;
            }

            double result = 0;
            bool b = double.TryParse(s, out result);

            if (b) return result;

            return defaultValue;
        }

        /// <summary>
        /// 将字符串转化为double 若值不是数字返回0
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static double GetDouble(string s)
        {
            return GetDouble(s, 0);
        }

        /// <summary>
        /// 将字符串转换成日期格式 若不能转换成日期将返回defaultValue
        /// </summary>
        /// <param name="s">要转换的字符串</param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(string s, DateTime defaultValue)
        {
            if (string.IsNullOrEmpty(s))
            {
                return defaultValue;
            }

            DateTime result = defaultValue;
            bool b = DateTime.TryParse(s, out result);

            if (b) return result;

            return defaultValue;
        }

        /// <summary>
        /// 将字符串转换成日期格式 若不能转换成日期将返回DateTime.MinValue
        /// </summary>
        /// <param name="s">要转换的字符串</param>
        /// <returns></returns>
        public static DateTime GetDateTime(string s)
        {
            return GetDateTime(s, DateTime.MinValue);
        }

        /// <summary>
        /// get enum value
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <param name="s">转换的字符串</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static int? GetEnumValueNullable(Type enumType, string s, int? defaultValue)
        {
            int? _enum = GetIntNullable(s, null);
            if (!_enum.HasValue) return defaultValue;

            if (!Enum.IsDefined(enumType, _enum)) return defaultValue;

            return _enum;
        }

        /// <summary>
        /// get enum value
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <param name="s">转换的字符串</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static int? GetEnumValueNullable(Type enumType, string s)
        {
            return GetEnumValueNullable(enumType, s, null);
        }

        /// <summary>
        /// 获取当前农历日期
        /// </summary>
        /// <returns></returns>
        public static MNongLiInfo GetNongLiInfo()
        {
            return GetNongLiInfo(DateTime.Today);
        }

        /// <summary>
        /// 将指定日期转换成农历日期
        /// </summary>
        /// <param name="d">公历日期</param>
        /// <returns></returns>
        public static MNongLiInfo GetNongLiInfo(DateTime d)
        {
            MNongLiInfo lunarCalendar = new MNongLiInfo();

            ChineseLunisolarCalendar chineseLunisolarCalendar = new ChineseLunisolarCalendar();
            lunarCalendar.Year = chineseLunisolarCalendar.GetYear(d);
            lunarCalendar.Month = chineseLunisolarCalendar.GetMonth(d);
            lunarCalendar.Day = chineseLunisolarCalendar.GetDayOfMonth(d);
            lunarCalendar.DaysInMonth = chineseLunisolarCalendar.GetDaysInMonth(lunarCalendar.Year, lunarCalendar.Month);
            lunarCalendar.DaysInYear = chineseLunisolarCalendar.GetDaysInYear(lunarCalendar.Year);

            return lunarCalendar;
        }

        #region XElement
        /// <summary>
        /// Get XAttribute Value
        /// </summary>
        /// <param name="XAttribute">xAttribute</param>
        /// <returns>Value</returns>
        public static string GetXAttributeValue(XAttribute xAttribute)
        {
            if (xAttribute == null)
                return string.Empty;

            return xAttribute.Value;
        }

        /// <summary>
        /// Get XAttribute Value
        /// </summary>
        /// <param name="xElement">XElement</param>
        /// <param name="attributeName">Attribute Name</param>
        /// <returns></returns>
        public static string GetXAttributeValue(XElement xElement, string attributeName)
        {
            return GetXAttributeValue(xElement.Attribute(attributeName));
        }

        /// <summary>
        /// Get XElement
        /// </summary>
        /// <param name="xElement">parent xElement</param>
        /// <param name="xName">xName</param>
        /// <returns>XElement</returns>
        public static XElement GetXElement(XElement xElement, string xName)
        {
            XElement x = xElement.Element(xName);

            if (x != null) return x;

            return new XElement(xName);
        }

        /// <summary>
        /// Get XElements
        /// </summary>
        /// <param name="xElement">parent xElement</param>
        /// <param name="xName">xName</param>
        /// <returns>XElements</returns>
        public static IEnumerable<XElement> GetXElements(XElement xElement, string xName)
        {
            var x = xElement.Elements(xName);
            if (x == null)
                return new List<XElement>();

            return x;
        }
        #endregion

        /// <summary>
        /// get enum value
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <param name="s">转换的字符串</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static int GetEnumValue(Type enumType, string s, int defaultValue)
        {
            int? _enum = GetIntNullable(s, null);
            if (!_enum.HasValue) return defaultValue;

            if (!Enum.IsDefined(enumType, _enum)) return defaultValue;

            return _enum.Value;
        }

        /// <summary>
        /// get enum value
        /// </summary>
        /// <param name="s">转换的字符串</param>
        /// <param name="defaultValue">默认值</param>
        /// <typeparam name="T">typeof(T).IsEnum==true</typeparam>
        /// <returns></returns>
        public static T GetEnumValue<T>(string s, T defaultValue)
        {
            if (typeof(T).IsEnum)
            {
                int? _enum = GetIntNullable(s, null);
                if (!_enum.HasValue) return defaultValue;

                if (!Enum.IsDefined(typeof(T), _enum.Value)) return defaultValue;

                return (T)(object)_enum.Value;
            }

            return defaultValue;
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="s">内容</param>
        /// <param name="path">相对路径，确保目录实际存在</param>
        public static void WLog(string s, string path)
        {
            string fPath = EyouSoft.Toolkit.Utils.GetMapPath(path);

            string extension = System.IO.Path.GetExtension(fPath);

            if (!string.IsNullOrEmpty(extension))
            {
                fPath = fPath.Substring(0, fPath.LastIndexOf(extension)) + "." + DateTime.Today.ToString("yyyyMMdd") + extension;
            }

            if (!File.Exists(fPath))
            {
                FileStream fs = File.Create(fPath);
                fs.Close();
            }

            try
            {
                var sw = new StreamWriter(fPath, true, System.Text.Encoding.UTF8);
                sw.Write(DateTime.Now.ToString("o") + "\t" + s + "\r\n");
                sw.Close();
            }
            catch { }
        }

        /// <summary>
        /// 分割字符串成int数组
        /// </summary>
        /// <param name="s">要分割的字符串</param>
        /// <param name="separator">分隔符</param>
        /// <returns></returns>
        public static int[] Split1(string s, string separator)
        {
            if (string.IsNullOrEmpty(s)) return new int[] { };
            if (string.IsNullOrEmpty(separator)) return new int[] { };

            string[] _tmp = s.Split(separator.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            if (_tmp == null || _tmp.Length == 0) return new int[] { };

            int _length = _tmp.Length;
            int[] _tmp1 = new int[_length];

            for (int i = 0; i < _length; i++)
            {
                _tmp1[i] = GetInt(_tmp[i]);
            }

            return _tmp1;
        }

        /// <summary>
        /// 分割字符串成IList&lt;int&gt;
        /// </summary>
        /// <param name="s">要分割的字符串</param>
        /// <param name="separator">分隔符</param>
        /// <returns></returns>
        public static IList<int> Split2(string s, string separator)
        {
            if (string.IsNullOrEmpty(s)) return new List<int>();
            if (string.IsNullOrEmpty(separator)) return new List<int>();

            string[] _tmp = s.Split(separator.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            if (_tmp == null || _tmp.Length == 0) return new List<int>();

            int _length = _tmp.Length;
            IList<int> _tmp1 = new List<int>();

            for (int i = 0; i < _length; i++)
            {
                _tmp1.Add(GetInt(_tmp[i]));
            }

            return _tmp1;
        }

        /// <summary>
        /// 将字符串转换为可空的日期类型，如果字符串不是有效的日期格式，则返回null
        /// </summary>
        /// <param name="s">进行转换的字符串</param>
        /// <returns></returns>
        public static DateTime? GetDateTimeNullable(string s)
        {
            return GetDateTimeNullable(s, null);
        }

        /// <summary>
        /// 将字符串转换为可空的日期类型，如果字符串不是有效的日期格式，则返回defaultValue
        /// </summary>
        /// <param name="s">进行转换的字符串</param>
        /// <param name="defaultValue">要返回的默认值</param>
        /// <returns></returns>
        public static DateTime? GetDateTimeNullable(string s, DateTime? defaultValue)
        {
            if (string.IsNullOrEmpty(s))
            {
                return defaultValue;
            }

            DateTime tmp;
            bool b = DateTime.TryParse(s, out tmp);

            if (b) return DateTime.Parse(s);

            return defaultValue;
        }

        /// <summary>  
        /// 替换字符串中低序位ASCII字符(非打印控制字符)为string.Empty
        /// 转换 ASCII  0 - 8
        /// 转换 ASCII 11 - 12
        /// 转换 ASCII 14 - 31
        /// </summary>  
        /// <param name="s"></param>  
        /// <returns></returns>  
        public static string ReplaceNonPrintingASCIICharacters(string s)
        {

            if (string.IsNullOrEmpty(s)) return string.Empty;

            StringBuilder s1 = new StringBuilder();

            foreach (char c in s)
            {
                int i = (int)c;
                if (((i >= 0) && (i <= 8)) || ((i >= 11) && (i <= 12)) || ((i >= 14) && (i < 32)))
                {
                    //s1.AppendFormat(string.Empty);
                    continue;
                }
                else
                {
                    s1.Append(c);
                }
            }

            return s1.ToString();
        }

        /// <summary>
        /// 获取IP地址信息
        /// </summary>
        /// <returns></returns>
        public static MIpInfo GetIpInfo()
        {
            MIpInfo info = new MIpInfo();
            string url = "http://ip.taobao.com/service/getIpInfo.php?ip=";
            info.ip = GetRemoteIP();
            if (string.IsNullOrEmpty(info.ip)) info.ip = "127.0.0.1";
            info.ip = "222.46.19.34";
            url += info.ip;

            string json = request.create(url, string.Empty);

            if (!string.IsNullOrEmpty(json))
            {
                var taoBaoIpInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<MTaoBaoIpInfo>(json);

                if (taoBaoIpInfo.code == 0) info = taoBaoIpInfo.data;
            }

            return info;
        }

        /// <summary>
        /// 32位MD5 加密
        /// </summary>
        /// <param name="s">要加密的字符串</param>
        /// <returns>返回加密后的字符串</returns>
        public static string MD5Encrypt(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                throw new System.Exception("要加密的字符串不能为空");
            }

            try
            {
                System.Security.Cryptography.MD5CryptoServiceProvider hashMD5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                return BitConverter.ToString(hashMD5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(s))).Replace("-", "").ToUpper();
            }
            catch (System.Exception e)
            {
                throw new System.Exception("加密过程中发生错误:" + e.Message);
            }

        }

        /// <summary>
        /// Response.Clear(),Response.Write(),Response.End()
        /// </summary>
        /// <param name="s">要写入 HTTP 输出流的字符串</param>
        public static void RCWE(string s)
        {
            var response = HttpContext.Current.Response;
            response.Clear();
            response.Write(s);
            response.End();
        }

        /// <summary>
        /// ajax response,json:{"result":"","msg":"","obj":{}}
        /// </summary>
        /// <param name="result">result</param>
        /// <param name="msg">msg</param>
        public static void RCWE_AJAX(string result, string msg)
        {
            RCWE_AJAX(result, msg, null);
        }

        /// <summary>
        /// ajax response,json:{"result":"","msg":"","obj":{}}
        /// </summary>
        /// <param name="result">result</param>
        /// <param name="msg">msg</param>
        /// <param name="obj">obj</param>
        public static void RCWE_AJAX(string result, string msg, object obj)
        {
            string output = "{}";

            if (obj != null)
            {
                output = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            }

            RCWE(string.Format("{{\"result\":\"{0}\",\"msg\":\"{1}\",\"obj\":{2}}}", result, msg, output));
        }

        /// <summary>
        /// 获取表单的值
        /// </summary>
        /// <param name="key">表单的key</param>
        /// <returns></returns>
        public static string GetFormValue(string key)
        {
            return GetFormValue(key, Int32.MaxValue);
        }
        

        /// <summary>
        /// 获取表单值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string[] GetFormValues(string key)
        {
            string[] items = HttpContext.Current.Request.Form.GetValues(key);
            if (items == null)
            {
                return new string[] { };
            }

            for (int i = 0; i < items.Length; i++)
            {
                items[i] = GetString(items[i]);
            }
            return items;
        }

        /// <summary>
        /// 获取编辑器表单值
        /// </summary>
        /// <param name="key">input name</param>
        /// <returns></returns>
        public static string GetFormEditorValue(string key)
        {
            if (string.IsNullOrEmpty(key)) return string.Empty;
            string _s = HttpContext.Current.Request.Form[key];
            if (string.IsNullOrEmpty(_s)) return string.Empty;

            _s = Microsoft.Security.Application.AntiXss.GetSafeHtmlFragment(_s);
            return _s;
        }

        /// <summary>
        /// 获取编辑器表单值
        /// </summary>
        /// <param name="key">input name</param>
        /// <returns></returns>
        public static string[] GetFormEditorValues(string key)
        {
            if (string.IsNullOrEmpty(key)) return new string[] { };
            string[] _values = HttpContext.Current.Request.Form.GetValues(key);
            if (_values == null) return new string[] { };

            for (int i = 0; i < _values.Length; i++)
            {
                _values[i] = Microsoft.Security.Application.AntiXss.GetSafeHtmlFragment(_values[i]);
            }

            return _values;
        }

        /// <summary>
        /// HttpContext.Current.Request.QueryString[key]
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetQueryStringValue(string key)
        {
            var s = HttpContext.Current.Request.QueryString[key];
            if (string.IsNullOrEmpty(s)) return string.Empty;

            return GetString(s);
        }

        /// <summary>
        /// 将英文星期几转化为中文星期几
        /// </summary>
        /// <param name="DayOfWeek"></param>
        /// <returns></returns>
        public static string ConvertWeekDayToChinese(DateTime time)
        {
            string DayOfWeek = time.DayOfWeek.ToString();
            switch (DayOfWeek)
            {
                case "Monday":
                    DayOfWeek = "星期一";
                    break;
                case "Tuesday":
                    DayOfWeek = "星期二";
                    break;
                case "Wednesday":
                    DayOfWeek = "星期三";
                    break;
                case "Thursday":
                    DayOfWeek = "星期四";
                    break;
                case "Friday":
                    DayOfWeek = "星期五";
                    break;
                case "Saturday":
                    DayOfWeek = "星期六";
                    break;
                case "Sunday":
                    DayOfWeek = "星期日";
                    break;
                default:
                    break;
            }
            return DayOfWeek;
        }
        #region 获取分页页索引
        /// <summary>
        /// 获取分页页索引，url页索引查询参数为Page
        /// </summary>
        /// <returns></returns>
        public static int GetPadingIndex()
        {
            return GetPadingIndex("Page");
        }

        /// <summary>
        /// 获取分页页索引
        /// </summary>
        /// <param name="s">url页索引查询参数</param>
        /// <returns></returns>
        public static int GetPadingIndex(string s)
        {
            int index = Utils.GetInt(Utils.GetQueryStringValue(s), 1);

            return index < 1 ? 1 : index;
        }
        #endregion

        /// <summary>
        /// 根据整型数组生成半角逗号分割的Sql字符串
        /// </summary>
        /// <param name="arrIds">整型数组</param>
        /// <returns>半角逗号分割的Sql字符串</returns>
        public static string GetSqlIdStrByArray(int[] arrIds)
        {
            if (arrIds == null || arrIds.Length <= 0)
                return string.Empty;

            string strTmp = arrIds.Where(i => i > 0).Aggregate(string.Empty, (current, i) => current + (i + ","));
            strTmp = strTmp.Trim(',');

            return strTmp;
        }

        /// <summary>
        /// 根据整型集合生成半角逗号分割的的Sql字符串
        /// </summary>
        /// <param name="ids">整形集合</param>
        /// <returns></returns>
        public static string GetSqlIdStrByList(IList<int> ids)
        {
            if (ids == null || ids.Count <= 0) return "0";
            StringBuilder s = new StringBuilder();
            s.AppendFormat("{0}", ids[0].ToString());

            for (int i = 1; i < ids.Count; i++)
            {
                s.AppendFormat(",{0}", ids[i].ToString());
            }

            return s.ToString();
        }

        /// <summary>
        /// 获取SQL IN 字符串
        /// </summary>
        /// <param name="ids">匹配字符串数组</param>
        /// <returns></returns>
        public static string GetSqlInExpression(string[] ids)
        {
            if (ids == null || ids.Length == 0) return "''";

            StringBuilder s = new StringBuilder();
            s.AppendFormat("'{0}'", ids[0]);

            for (int i = 1; i < ids.Length; i++)
            {
                s.AppendFormat(",'{0}'", ids[i]);
            }

            return s.ToString();
        }

        /// <summary>
        /// 获取SQL IN 字符串
        /// </summary>
        /// <param name="ids">匹配字符串集合</param>
        /// <returns></returns>
        public static string GetSqlInExpression(IList<string> ids)
        {
            if (ids == null || ids.Count == 0) return "''";

            StringBuilder s = new StringBuilder();
            s.AppendFormat("'{0}'", ids[0]);

            for (int i = 1; i < ids.Count; i++)
            {
                s.AppendFormat(",'{0}'", ids[i]);
            }

            return s.ToString();
        }
        #endregion

        /// <summary>
        /// 获取允许上传的文件类型信息集合
        /// </summary>
        /// <returns></returns>
        public static string[] GetUploadFileExtensions()
        {
            /*string[] s = UploadFileExtensions.Split(';');
            int length = s.Length;

            for (int i = 0; i < length; i++)
            {
                s[i] = s[i].Replace("*", "");
            }

            return s;*/

            return UploadFileExtensions.Replace("*", "").Split(';');
        }

        /// <summary>
        /// get lxqq
        /// </summary>
        /// <param name="lxQQ"></param>
        /// <returns></returns>
        public static string GetLxQQ(object lxQQ)
        {
            if (lxQQ == null) return string.Empty;

            string _lxQQ = lxQQ.ToString();
            if (string.IsNullOrEmpty(_lxQQ)) return string.Empty;

            string s = string.Empty;

            s = string.Format("<a href=\"http://wpa.qq.com/msgrd?v=3&amp;uin={0}&amp;site=联速交易&amp;menu=yes\" target=\"_blank\">&nbsp;<img src=\"http://wpa.qq.com/pa?p=2:{0}:52\"></a>", _lxQQ);

            return s.ToString();
        }
    }
    #endregion

    #region 农历信息业务实体
    /// <summary>
    /// 农历信息业务实体
    /// </summary>
    public class MNongLiInfo
    {
        /// <summary>
        /// 年
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// 月
        /// </summary>
        public int Month { get; set; }
        /// <summary>
        /// 日
        /// </summary>
        public int Day { get; set; }
        /// <summary>
        /// 月份中的天数
        /// </summary>
        public int DaysInMonth { get; set; }
        /// <summary>
        /// 年份中的天数
        /// </summary>
        public int DaysInYear { get; set; }
    }
    #endregion

    #region 浏览器信息业务实体
    /// <summary>
    /// 浏览器信息业务实体
    /// </summary>
    [Serializable]
    public class BrowserInfo
    {
        string _browser, _version, _platform, _useragent;

        /// <summary>
        /// default constructor
        /// </summary>
        public BrowserInfo()
        {
            var request = HttpContext.Current.Request;
            if (request == null) return;
            var browser = request.Browser;
            if (browser == null) return;

            _useragent = request.UserAgent;
            _browser = browser.Browser;
            _version = browser.Version;
            _platform = browser.Platform;

            request = null;
            browser = null;
        }

        /// <summary>
        /// 获取由浏览器在 User-Agent 请求标头中发送的浏览器字符串（如果有）。
        /// </summary>
        public string Browser { get { return _browser; } }
        /// <summary>
        /// 获取浏览器的完整（整数和小数）版本号。
        /// </summary>
        public string Version { get { return _version; } }
        /// <summary>
        /// 获取客户端使用的平台的名称（如果已知）。
        /// </summary>
        public string Platform { get { return _platform; } }
        /// <summary>
        /// 获取客户端浏览器的原始用户代理信息。
        /// </summary>
        public string UserAgent { get { return _useragent; } }

        /// <summary>
        /// to json string
        /// </summary>
        /// <returns></returns>
        public string ToJsonString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
    #endregion

    #region IP地址信息业务实体
    /// <summary>
    /// IP地址信息业务实体
    /// </summary>
    public class MTaoBaoIpInfo
    {
        /// <summary>
        /// 请求成功0/失败1
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// IP地址信息
        /// </summary>
        public MIpInfo data { get; set; }
    }

    /// <summary>
    /// IP地址信息业务实体
    /// </summary>
    public class MIpInfo
    {
        /// <summary>
        /// 国家
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// 国家代码
        /// </summary>
        public string country_id { get; set; }
        /// <summary>
        /// 地区
        /// </summary>
        public string area { get; set; }
        /// <summary>
        /// 地区代码
        /// </summary>
        public string area_id { get; set; }
        /// <summary>
        /// 省（自治区、直辖市、特别行政区）
        /// </summary>
        public string region { get; set; }
        /// <summary>
        /// 省代码
        /// </summary>
        public string region_id { get; set; }
        /// <summary>
        ///  市（地区、自治州、盟及国家直辖市所属市辖区和县）
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 市代码
        /// </summary>
        public string city_id { get; set; }
        /// <summary>
        /// 县（市辖区、县级市、旗）
        /// </summary>
        public string county { get; set; }
        /// <summary>
        /// 县代码
        /// </summary>
        public string county_id { get; set; }
        /// <summary>
        /// 运营商
        /// </summary>
        public string isp { get; set; }
        /// <summary>
        /// 运营商代码
        /// </summary>
        public string isp_id { get; set; }
        /// <summary>
        /// ipv4/ipv6
        /// </summary>
        public string ip { get; set; }
    }
    #endregion
}
