using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyouSoft.Toolkit;

namespace EyouSoft.WEB
{
    /// <summary>
    /// 管控端页面类
    /// </summary>
    public class GkYeMian: System.Web.UI.Page
    {
        #region attributes
        EyouSoft.Model.SSO.MYongHuInfo _YongHuInfo = null;
        bool _IsLogin = false;

        /// <summary>
        /// 当前用户信息
        /// </summary>
        public EyouSoft.Model.SSO.MYongHuInfo YongHuInfo { get { return _YongHuInfo; } }
        /// <summary>
        /// 是否登录
        /// </summary>
        public bool IsLogin { get { return _IsLogin; } }
        /// <summary>
        /// 登录url
        /// </summary>
        public string LoginUrl { get { return "/login.aspx"; } }
        #endregion

        #region protected members
        /// <summary>
        /// OnInit
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            EyouSoft.Model.SSO.MYongHuInfo yongHuInfo = null;
            _IsLogin = EyouSoft.Security.Membership.YongHuProvider.IsLogin(out yongHuInfo);
            _YongHuInfo = yongHuInfo;

            if (_IsLogin)
            {
                if (yongHuInfo.LeiXing != EyouSoft.Model.YongHuLeiXing.平台)
                {
                    _IsLogin = false;
                    _YongHuInfo = null;
                }
            }

            if (!_IsLogin)
            {
                if (Utils.GetQueryStringValue("urltype") == "ajax")
                {
                    Utils.RCWE("{\"Islogin\":\"false\"}");
                }
                else
                {
                    var rurl = Server.UrlEncode(Request.Url.ToString());
                    string s = string.Empty;
                    s += "<script type='text/javascript'>";
                    s += string.Format(" if (\"{0}\" != \"\") {{", Utils.GetQueryStringValue("iframeId"));
                    s += string.Format(" window.top.location.href = \"{0}?rurl={1}\"; ", LoginUrl, rurl);
                    s += "} else {";
                    s += string.Format(" window.location.href = \"{0}?rurl={1}\"; ", LoginUrl, rurl);
                    s += "}";
                    s += "</script>";
                    Utils.RCWE(s);
                }
            }
        }

        /// <summary>
        /// OnPreRender
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }
        #endregion
    }
}
