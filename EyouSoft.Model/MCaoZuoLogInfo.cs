//操作日志相关实体 汪奇志 2015-04-24
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model
{
    #region 操作日志信息业务实体
    /// <summary>
    /// 操作日志信息业务实体
    /// </summary>
    public class MCaoZuoLogInfo
    {
        string _GongSiId = string.Empty;
        string _CaoZuoRenId = string.Empty;
        /// <summary>
        /// 日志编号
        /// </summary>
        public string LogId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string GongSiId { get { return _GongSiId; } set { _GongSiId = value; } }
        /// <summary>
        /// 操作人编号
        /// </summary>
        public string CaoZuoRenId { get { return _CaoZuoRenId; } set { _CaoZuoRenId = value; } }
        /// <summary>
        /// 日志类型
        /// </summary>
        public CaoZuoLogLeiXing LeiXing { get; set; }
        /// <summary>
        /// 日志标题
        /// </summary>
        public string BiaoTi { get; set; }
        /// <summary>
        /// 日志内容
        /// </summary>
        public string NeiRong { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// IP
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 关联编号
        /// </summary>
        public string GuanLianId { get; set; }
        /// <summary>
        /// 操作人姓名（OUTPUT）
        /// </summary>
        public string CaoZuoRenName { get; set; }
    }
    #endregion

    #region 操作日志信息查询业务实体
    /// <summary>
    /// 操作日志信息查询业务实体
    /// </summary>
    public class MCaoZuoLogChaXunInfo
    {
        /// <summary>
        /// 公司编号
        /// </summary>
        public string GongSiId { get; set; }
    }
    #endregion
}
