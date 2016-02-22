using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SSO
{
    /// <summary>
    /// 用户信息业务实体
    /// </summary>
    public class MYongHuInfo
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public string YongHuId { get; set; }
        /// <summary>
        /// 用户类型
        /// </summary>
        public YongHuLeiXing LeiXing { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string GongSiId { get; set; }
        /// <summary>
        /// 用户账号
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// 角色编号
        /// </summary>
        public string JueSeId { get; set; }
        /// <summary>
        /// 用户状态
        /// </summary>
        public YongHuStatus Status { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string BuMenName { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 照片附件
        /// </summary>
        public string ZhaoPianFilepath { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public XingBie XingBie { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string ShouJi { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string DianHua { get; set; }
        /// <summary>
        /// 传真
        /// </summary>
        public string Fax { get; set; }
        /// <summary>
        /// 邮件
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 用户权限
        /// </summary>
        public IList<int> Privs { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public ShenHeStatus ShenHeStatus { get; set; }

        /// <summary>
        /// 公司logo filepath
        /// </summary>
        public string GS_LogoFilepath { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string GS_Name { get; set; }

        /// <summary>
        /// 权限验证
        /// </summary>
        /// <param name="privs"></param>
        /// <returns></returns>
        public bool IsPrivs(EyouSoft.Model.CGS_Privs1 _privs)
        {
            if (Privs == null || Privs.Count == 0) return false;

            return Privs.Contains((int)_privs);
        }

        /// <summary>
        /// 权限验证
        /// </summary>
        /// <param name="privs"></param>
        /// <returns></returns>
        public bool IsPrivs(EyouSoft.Model.GYS_Privs1 _privs)
        {
            if (Privs == null || Privs.Count == 0) return false;

            return Privs.Contains((int)_privs);
        }

        /// <summary>
        /// 权限验证
        /// </summary>
        /// <param name="privs"></param>
        /// <returns></returns>
        public bool IsPrivs(EyouSoft.Model.GK_Privs1 _privs)
        {
            if (Privs == null || Privs.Count == 0) return false;

            return Privs.Contains((int)_privs);
        }
    }
}
