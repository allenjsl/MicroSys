using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Toolkit;
using System.Web;

namespace EyouSoft.Security.Membership
{
    using EyouSoft.Model;

    /// <summary>
    /// 用户登录处理类
    /// </summary>
    public class YongHuProvider
    {
        #region static constants
        /// <summary>
        /// 登录Cookie，用户编号
        /// </summary>
        public const string LoginCookieYongHuId = "LSGY_PT_UID";
        /// <summary>
        /// 登录Cookie，用户账号
        /// </summary>
        public const string LoginCookieUsername = "LSGY_PT_UN";
        /// <summary>
        /// 登录Cookie，公司编号
        /// </summary>
        public const string LoginCookieGongSiId = "LSGY_PT_GID";
        /// <summary>
        /// 登录Cookie，COOKIES保留天数
        /// </summary>
        public const string LoginCookieTian = "LSGY_PT_CTIAN";
        #endregion

        #region private members
        /// <summary>
        /// 设置登录用户cache
        /// </summary>
        /// <param name="info">登录用户信息</param>
        static void SetYongHuCache(EyouSoft.Model.SSO.MYongHuInfo info)
        {
            string cacheKey = string.Format(EyouSoft.Model.CacheKey.LoginYongHu, info.YongHuId);
            CacheHelper.Remove(cacheKey);
            CacheHelper.Add(cacheKey, info, DateTime.Now.AddHours(12));
        }

        /// <summary>
        /// 设置登录Cookies
        /// </summary>
        /// <param name="info">登录用户信息</param>
        static void SetLoginCookies(EyouSoft.Model.SSO.MYongHuInfo info, double cookieTian)
        {
            //Cookies生存周期为浏览器进程
            HttpResponse response = HttpContext.Current.Response;

            RemoveLoginCookies();

            var cookie = new HttpCookie(LoginCookieGongSiId);
            cookie.Value = info.GongSiId.ToString();
            cookie.HttpOnly = true;
            if (cookieTian > 0) cookie.Expires = DateTime.Now.AddDays(cookieTian);
            response.AppendCookie(cookie);

            cookie = new HttpCookie(LoginCookieYongHuId);
            cookie.Value = info.YongHuId.ToString();
            cookie.HttpOnly = true;
            if (cookieTian > 0) cookie.Expires = DateTime.Now.AddDays(cookieTian);
            response.AppendCookie(cookie);

            cookie = new HttpCookie(LoginCookieUsername);
            cookie.Value = HttpContext.Current.Server.UrlEncode(info.Username);
            cookie.HttpOnly = true;
            if (cookieTian > 0) cookie.Expires = DateTime.Now.AddDays(cookieTian);
            response.AppendCookie(cookie);

            cookie = new HttpCookie(LoginCookieTian);
            cookie.Value = cookieTian.ToString();
            cookie.HttpOnly = true;
            if (cookieTian > 0) cookie.Expires = DateTime.Now.AddDays(cookieTian);
            response.AppendCookie(cookie);
        }

        /// <summary>
        /// remove login cookies
        /// </summary>
        static void RemoveLoginCookies()
        {
            HttpResponse response = HttpContext.Current.Response;

            response.Cookies.Remove(LoginCookieGongSiId);
            response.Cookies.Remove(LoginCookieYongHuId);
            response.Cookies.Remove(LoginCookieUsername);
            response.Cookies.Remove(LoginCookieTian);

            DateTime cookiesExpiresDateTime = DateTime.Now.AddDays(-1);

            response.Cookies[LoginCookieGongSiId].Expires = cookiesExpiresDateTime;
            response.Cookies[LoginCookieYongHuId].Expires = cookiesExpiresDateTime;
            response.Cookies[LoginCookieUsername].Expires = cookiesExpiresDateTime;
            response.Cookies[LoginCookieTian].Expires = cookiesExpiresDateTime;
        }

        /// <summary>
        /// 获取登录用户Cookie信息
        /// </summary>
        /// <param name="name">登录Cookie名称</param>
        /// <returns></returns>
        static string GetCookie(string name)
        {
            HttpRequest request = HttpContext.Current.Request;

            if (request.Cookies[name] == null)
            {
                return string.Empty;
            }

            return HttpContext.Current.Server.UrlDecode(request.Cookies[name].Value);
        }

        /// <summary>
        /// 自动登录处理
        /// </summary>
        /// <param name="gongSiId">公司编号</param>
        /// <param name="yongHuId">用户编号</param>
        /// <param name="username">用户名</param>
        static void AutoLogin(string gongSiId, string yongHuId, string username, out EyouSoft.Model.SSO.MYongHuInfo yongHuInfo)
        {
            yongHuInfo = null;
            if (string.IsNullOrEmpty(gongSiId) || string.IsNullOrEmpty(yongHuId) || string.IsNullOrEmpty(username)) return;

            IYongHu dal = new DYongHu();

            yongHuInfo = dal.Login(gongSiId, username, yongHuId);

            if (yongHuInfo == null) return;
            if (yongHuInfo.Status != EyouSoft.Model.YongHuStatus.启用) { yongHuInfo = null; return; }

            dal.LoginLogwr(yongHuInfo, EyouSoft.Model.LoginLeiXing.自动登录);

            SetYongHuCache(yongHuInfo);
        }

        /// <summary>
        /// 移除登录用户cache
        /// </summary>
        /// <param name="yongHuId">用户编号</param>
        static void RemoveYongHuCache(string yongHuId)
        {
            string cacheKey = string.Format(EyouSoft.Model.CacheKey.LoginYongHu, yongHuId);

            CacheHelper.Remove(cacheKey);
        }

        /// <summary>
        /// 获取登录用户cache
        /// </summary>
        /// <param name="yongHuId">用户编号</param>
        /// <returns></returns>
        static EyouSoft.Model.SSO.MYongHuInfo GetYongHuCache(string yongHuId)
        {
            EyouSoft.Model.SSO.MYongHuInfo info = null;
            //从缓存查询登录用户信息
            string cacheKey = string.Format(EyouSoft.Model.CacheKey.LoginYongHu, yongHuId);
            //从缓存查询登录用户信息计数器
            int getCacheCount = 2;

            do
            {
                info = (EyouSoft.Model.SSO.MYongHuInfo)CacheHelper.GetCache(cacheKey);
                getCacheCount--;
            } while (info == null && getCacheCount > 0);

            return info;
        }
        #endregion

        #region public members
        /// <summary>
        /// 用户登录，返回1登录成功，其它失败
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="username">用户名</param>
        /// <param name="pwd_md5">登录密码MD5</param>
        /// <param name="yongHuInfo">登录用户信息</param>
        /// <param name="cookieTian">cookie保留天数</param>
        /// <returns></returns>
        public static int Login(string username, string pwd_md5, out EyouSoft.Model.SSO.MYongHuInfo yongHuInfo, double cookieTian)
        {
            IYongHu dal = new DYongHu();
            yongHuInfo = null;

            if (string.IsNullOrEmpty(username)) return -1;
            if (string.IsNullOrEmpty(pwd_md5)) return -2;

            yongHuInfo = dal.Login(username, pwd_md5);
            if (yongHuInfo == null) return -4;

            if (yongHuInfo.Status != EyouSoft.Model.YongHuStatus.启用)
            {
                yongHuInfo = null;
                return -5;
            }

            /*if (yongHuInfo.ShenHeStatus != ShenHeStatus.已审核)
            {
                yongHuInfo = null;
                return -6;
            }*/

            dal.LoginLogwr(yongHuInfo, EyouSoft.Model.LoginLeiXing.用户登录);

            SetYongHuCache(yongHuInfo);
            SetLoginCookies(yongHuInfo, cookieTian);

            return 1;
        }

        /// <summary>
        /// 获取登录用户信息
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.Model.SSO.MYongHuInfo GetYongHuInfo()
        {
            EyouSoft.Model.SSO.MYongHuInfo info = null;
            string gongSiId = GetCookie(LoginCookieGongSiId);
            string yongHuId = GetCookie(LoginCookieYongHuId);
            string username = GetCookie(LoginCookieUsername);

            if (string.IsNullOrEmpty(gongSiId)
                || string.IsNullOrEmpty(yongHuId)
                || string.IsNullOrEmpty(username))
            {
                return null;
            }

            info = GetYongHuCache(yongHuId);

            //缓存中未找到登录用户信息，自动登录处理
            if (info == null)
            {
                AutoLogin(gongSiId, yongHuId, username, out info);
            }

            if (info == null) return null;

            return info;
        }

        /// <summary>
        /// 用户是否登录
        /// </summary>
        /// <param name="info">登录用户信息</param>
        /// <returns></returns>
        public static bool IsLogin(out EyouSoft.Model.SSO.MYongHuInfo info)
        {
            info = GetYongHuInfo();

            if (info == null) return false;

            return true;
        }

        /// <summary>
        /// 用户是否登录
        /// </summary>
        /// <returns></returns>
        public static bool IsLogin()
        {
            var info = GetYongHuInfo();

            if (info == null) return false;

            return true;
        }

        /// <summary>
        /// 用户退出
        /// </summary>
        public static void Logout(string url)
        {
            string yongHuId = GetCookie(LoginCookieYongHuId);

            if (!string.IsNullOrEmpty(yongHuId))
            {
                RemoveYongHuCache(yongHuId);
            }

            RemoveLoginCookies();
            HttpContext.Current.Response.Redirect(url,true);
        }
        #endregion
    }
}
