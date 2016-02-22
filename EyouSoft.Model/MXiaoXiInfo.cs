//消息相关业务实体 汪奇志 2015-04-24
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model
{
    #region 消息业务实体
    /// <summary>
    /// 消息业务实体
    /// </summary>
    public class MXiaoXiInfo
    {
        /// <summary>
        /// 消息编号
        /// </summary>
        public string XiaoXiId { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public XiaoXiLeiXing LeiXing { get; set; }
        /// <summary>
        /// 关联编号
        /// </summary>
        public string GuanLianId { get; set; }
        /// <summary>
        /// 消息标题
        /// </summary>
        public string BiaoTi { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string NeiRong { get; set; }
        /// <summary>
        /// 发出人公司编号
        /// </summary>
        public string FaChuGongSiId { get; set; }
        /// <summary>
        /// 发出人编号
        /// </summary>
        public string FaChuRenId { get; set; }
        /// <summary>
        /// 发出时间
        /// </summary>
        public DateTime FaChuTime { get; set; }
        /// <summary>
        /// 接收人公司编号
        /// </summary>
        public string JieShouGongSiId { get; set; }
        /// <summary>
        /// 接收人编号
        /// </summary>
        public string JieShouRenId { get; set; }
        /// <summary>
        /// 消息状态
        /// </summary>
        public XiaoXiStatus Status { get; set; }
        /// <summary>
        /// 处理人编号
        /// </summary>
        public string ChuLiRenId { get; set; }
        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime? ChuLiTime { get; set; }
    }
    #endregion

    #region 消息查询业务实体
    /// <summary>
    /// 消息查询业务实体
    /// </summary>
    public class MXiaoXiChaXunInfo
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        public XiaoXiLeiXing? LeiXing { get; set; }
        /// <summary>
        /// 消息状态
        /// </summary>
        public XiaoXiStatus? Status { get; set; }
        /// <summary>
        /// 发出人公司编号
        /// </summary>
        public string FaChuGongSiId { get; set; }
        /// <summary>
        /// 发出人编号
        /// </summary>
        public string FaChuRenId { get; set; }
        /// <summary>
        /// 接收人公司编号
        /// </summary>
        public string JieShouGongSiId { get; set; }
        /// <summary>
        /// 接收人编号
        /// </summary>
        public string JieShouRenId { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public IList<XiaoXiLeiXing> LeiXings { get; set; }
    }
    #endregion
}
