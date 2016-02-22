//地址相关信息业务实体 汪奇志 2015-05-29
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model
{
    #region 地址信息业务实体
    /// <summary>
    /// 地址信息业务实体
    /// </summary>
    public class MDiZhiInfo
    {
        /// <summary>
        /// 地址编号
        /// </summary>
        public string DiZhiId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string GongSiId { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public string YongHuId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string DiZhi { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string ShouJi { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string DianHua { get; set; }
        /// <summary>
        /// 操作人编号
        /// </summary>
        public string CaoZuoRenId { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 是否默认
        /// </summary>
        public bool IsMoRen { get; set; }
    }
    #endregion

    #region 地址信息查询业务实体
    /// <summary>
    /// 地址信息查询业务实体
    /// </summary>
    public class MDiZhiChaXunInfo
    {
        /// <summary>
        /// 公司编号
        /// </summary>
        public string GongSiId { get; set; }
        /// <summary>
        /// 联系人姓名
        /// </summary>
        public string Name { get; set; }
    }
    #endregion
}
