using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace EyouSoft.Toolkit
{
    #region meiju info
    /// <summary>
    /// meiju info
    /// </summary>
    public class MMeiJuInfo
    {
        /// <summary>
        /// 文本
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
    }
    #endregion

    #region meiju helper
    /// <summary>
    /// meiju helper
    /// </summary>
    public class MeiJuHelper
    {
        /// <summary>
        /// meiju to list
        /// </summary>
        /// <param name="type">typeof(enum)</param>
        /// <param name="removeValues">remove values</param>
        /// <param name="replaces">要替换显示Text的KV集</param>
        /// <returns></returns>
        public static List<MMeiJuInfo> GetList(Type type, string[] removeValues, List<MMeiJuInfo> replaces)
        {
            if (!type.IsEnum) return new List<MMeiJuInfo>();

            List<MMeiJuInfo> list = new List<MMeiJuInfo>();
            System.Reflection.FieldInfo[] fields = type.GetFields();

            foreach (FieldInfo field in fields)
            {
                if (field.FieldType.IsEnum == true)
                {
                    MMeiJuInfo obj = new MMeiJuInfo();

                    string _value = ((int)type.InvokeMember(field.Name, BindingFlags.GetField, null, null, null)).ToString();
                    string _text = field.Name;

                    if (removeValues != null && removeValues.Length > 0 && removeValues.Contains(_value)) continue;

                    if (replaces != null && replaces.Count > 0)
                    {
                        foreach (var replace in replaces)
                        {
                            if (replace.Value == _value && !string.IsNullOrEmpty(replace.Text))
                            {
                                _text = replace.Text;
                                break;
                            }
                        }
                    }

                    obj.Value = _value;
                    obj.Text = _text;

                    list.Add(obj);
                }
            }

            return list;
        }

        /// <summary>
        /// meiju to list
        /// </summary>
        /// <param name="type">typeof(enum)</param>
        /// <param name="removeValues">remove values</param>
        /// <returns></returns>
        public static List<MMeiJuInfo> GetList(Type type, string[] removeValues)
        {
            return GetList(type, removeValues, null);
        }

        /// <summary>
        /// enum to list
        /// </summary>
        /// <param name="type">typeof(enum)</param>
        /// <returns></returns>
        public static List<MMeiJuInfo> GetList(Type type)
        {
            return GetList(type, null);
        }

        /// <summary>
        /// get select option
        /// </summary>
        /// <param name="type">typeof(enum)</param>
        /// <param name="removeValues">remove values</param>
        /// <param name="replaces">要替换显示Text的KV集</param>
        /// <returns></returns>
        public static string GetSelectOption(Type type, string[] removeValues, List<MMeiJuInfo> replaces)
        {
            if (!type.IsEnum) return string.Empty;

            StringBuilder s = new StringBuilder();

            var items = GetList(type,removeValues,replaces);

            if (items == null || items.Count == 0) return string.Empty;

            foreach (var item in items)
            {
                s.AppendFormat("<option value=\"{0}\">{1}</option>", item.Value, item.Text);
            }

            return s.ToString();
        }

        /// <summary>
        /// get select option
        /// </summary>
        /// <param name="type">typeof(enum)</param>
        /// <param name="removeValues">remove values</param>
        /// <returns></returns>
        public static string GetSelectOption(Type type, string[] removeValues)
        {
            return GetSelectOption(type, removeValues, null);
        }

        /// <summary>
        /// get select option
        /// </summary>
        /// <param name="type">typeof(enum)</param>
        /// <returns></returns>
        public static string GetSelectOption(Type type)
        {
            return GetSelectOption(type, null, null);
        }
    }
    #endregion
}
