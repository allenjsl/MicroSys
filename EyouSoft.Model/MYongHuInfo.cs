using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model
{
    #region 采购商、供应商、平台用户信息业务实体
    /// <summary>
    /// 采购商、供应商、平台用户信息业务实体
    /// </summary>
    public class MYongHuInfo
    {
        string _GongSiId = string.Empty;
        string _JueSeId = string.Empty;
        DateTime _ChuShengRiQi = new DateTime(1980, 01, 01);
        DateTime _RuZhiRiQi = DateTime.Today;

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
        public string GongSiId { get { return _GongSiId; } set { _GongSiId = value; } }
        /// <summary>
        /// 用户账号
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// 用户密码（MD5）
        /// </summary>
        public string PasswordMD5 { get; set; }
        /// <summary>
        /// 角色编号
        /// </summary>
        public string JueSeId { get { return _JueSeId; } set { _JueSeId = value; } }
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
        /// 职务
        /// </summary>
        public string ZhiWu { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public XingBie XingBie { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime ChuShengRiQi { get { return _ChuShengRiQi; } set { _ChuShengRiQi = value; } }
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
        /// 居住地址
        /// </summary>
        public string DiZhi { get; set; }
        /// <summary>
        /// 入职时间
        /// </summary>
        public DateTime RuZhiRiQi { get { return _RuZhiRiQi; } set { _RuZhiRiQi = value; } }
        /// <summary>
        /// 操作人编号
        /// </summary>
        public string CaoZuoRenId { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string GongSiName { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public LaiYuan LaiYuan { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public ShenHeStatus ShenHeStatus { get; set; }
    }
    #endregion

    #region 采购商、供应商、平台用户信息查询业务实体
    /// <summary>
    /// 采购商、供应商、平台用户信息查询业务实体
    /// </summary>
    public class MYongHuChaXunInfo
    {
        /// <summary>
        /// 采购商、供应商编号
        /// </summary>
        public string GongSiId { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 用户账号
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// 用户类型
        /// </summary>
        public YongHuLeiXing? LeiXing { get; set; }
        /// <summary>
        /// 用户状态
        /// </summary>
        public YongHuStatus? Status { get; set; }
        /// <summary>
        /// 所属部门
        /// </summary>
        public string BuMen { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public ShenHeStatus? ShenHeStatus { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string GongSiName { get; set; }
    }
    #endregion

    #region 注册用户信息业务实体
    /// <summary>
    /// 注册用户信息业务实体
    /// </summary>
    public class MZhuCeYongHuInfo
    {
        /// <summary>
        /// 公司编号
        /// </summary>
        public string GongSiId { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public string YongHuId { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string GongSiName { get; set; }
        /// <summary>
        /// 法人姓名
        /// </summary>
        public string FaRenName { get; set; }
        /// <summary>
        /// 负责人姓名
        /// </summary>
        public string FuZeRenName { get; set; }
        /// <summary>
        /// 负责人电话
        /// </summary>
        public string FuZeRenDianHua { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string YongHuMing { get; set; }
        /// <summary>
        /// 密码（MD5）
        /// </summary>
        public string PasswordMD5 { get; set; }
        /// <summary>
        /// 公司类型
        /// </summary>
        public GongSiLeiXing LeiXing { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public ShenHeStatus ShenHeStatus { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 公司、用户来源
        /// </summary>
        public LaiYuan LaiYuan { get; set; }
        /// <summary>
        /// 公司地址
        /// </summary>
        public string DiZhi { get; set; }
    }
    #endregion

    #region 注册用户信息查询业务实体
    /// <summary>
    /// 注册用户信息查询业务实体
    /// </summary>
    public class MZhuCeYongHuChaXunInfo
    {
        /// <summary>
        /// 公司名称
        /// </summary>
        public string GongSiName { get; set; }
        /// <summary>
        /// 公司类型
        /// </summary>
        public GongSiLeiXing? LeiXing { get; set; }
        /// <summary>
        /// 公司来源
        /// </summary>
        public LaiYuan? LaiYuan { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public ShenHeStatus? ShenHeStatus { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string YongHuMing { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string YongHuName { get; set; }
    }
    #endregion
}
