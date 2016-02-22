using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Security.Membership
{
    /// <summary>
    /// 用户登录interface
    /// </summary>
    interface IYongHu
    {
        /// <summary>
        /// 用户登录，根据用户名、用户密码获取用户信息
        /// </summary>
        /// <param name="username">登录账号</param>
        /// <param name="pwd">登录密码</param>
        /// <returns></returns>
        EyouSoft.Model.SSO.MYongHuInfo Login(string username, string pwd_md5);

        /// <summary>
        /// 用户登录，根据公司编号、用户名、用户编号获取用户信息
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="username">登录账号</param>
        /// <param name="yongHuId">用户编号</param>
        /// <param name="keHuId">客户编号</param>
        /// <returns></returns>
        EyouSoft.Model.SSO.MYongHuInfo Login(string gongSiId, string username, string yongHuId);
        /// <summary>
        /// 写登录日志
        /// </summary>
        /// <param name="info">登录用户信息</param>
        /// <param name="leiXing">登录类型</param>
        void LoginLogwr(EyouSoft.Model.SSO.MYongHuInfo info, EyouSoft.Model.LoginLeiXing leiXing);
    }
}
