using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Xml;

namespace EyouSoft.Toolkit.DAL
{
    /// <summary>
    /// 数据层访问基类
    /// 读取配置文件，生成数据库可用连接
    /// </summary>
    public class DALBase
    {
        private readonly Database _systemstore = DatabaseFactory.CreateDatabase("SystemStore");

        /// <summary>
        /// 系统库
        /// </summary>
        protected Database SystemStore
        {
            get
            {
                return _systemstore;
            }
        }


        /// <summary>
        /// 数据库CHAR(1)转换成布尔类型，1→true 其它→false
        /// </summary>
        /// <param name="s">CHAR(1)</param>
        /// <returns></returns>
        protected bool GetBoolean(string s)
        {
            return s == "1" ? true : false;
        }

        /// <summary>
        /// 将bool转换成char(1) true:1 false:0
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        protected string GetBooleanToStr(bool s)
        {
            return s ? "1" : "0";
        }

        /// <summary>
        /// 获取SQL IN 字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ids"></param>
        /// <returns></returns>
        protected static string GetSqlIn<T>(ICollection<T> ids)
        {

            if (typeof(T).IsEnum)
            {
                if (ids == null || ids.Count < 1) return "-1";
                StringBuilder s1 = new StringBuilder();
                foreach (var item in ids)
                {
                    s1.AppendFormat(",{0}", Convert.ToInt32(item));
                }

                return s1.ToString().Substring(1);
            }

            if (typeof(T) == typeof(int) || typeof(T) == typeof(byte))
            {
                if (ids == null || ids.Count < 1) return "0";
                StringBuilder s2 = new StringBuilder();
                foreach (var item in ids)
                {
                    s2.AppendFormat(",{0}", item);
                }
                return s2.ToString().Substring(1);
            }

            if (typeof(T) == typeof(string))
            {
                if (ids == null || ids.Count < 1) return "''";
                StringBuilder s3 = new StringBuilder();
                foreach (var item in ids)
                {
                    s3.AppendFormat(",'{0}'", item);
                }
                return s3.ToString().Substring(1);
            }

            return "-1";
        }

    }
}
