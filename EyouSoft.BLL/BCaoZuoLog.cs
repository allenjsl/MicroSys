//操作日志相关BLL 汪奇志 2015-04-24
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL
{
    /// <summary>
    /// 操作日志相关BLL
    /// </summary>
    public class BCaoZuoLog
    {
        private readonly EyouSoft.DAL.DCaoZuoLog dal = new EyouSoft.DAL.DCaoZuoLog();

        #region constructure
        /// <summary>
        /// default constructor
        /// </summary>
        public BCaoZuoLog() { }
        #endregion

        #region private members
        #endregion

        #region public members
        /// <summary>
        /// 添加操作日志，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        internal static int Log_C(EyouSoft.Model.MCaoZuoLogInfo info)
        {
            if (info == null) return 0;

            var loginYongHuInfo = EyouSoft.Security.Membership.YongHuProvider.GetYongHuInfo();
            if (loginYongHuInfo != null)
            {
                info.GongSiId = loginYongHuInfo.GongSiId;
                info.CaoZuoRenId = loginYongHuInfo.YongHuId;
            }
            info.IP = EyouSoft.Toolkit.Utils.GetRemoteIP();

            info.LogId = Guid.NewGuid().ToString();
            info.IssueTime = DateTime.Now;

            int dalRetCode = new EyouSoft.DAL.DCaoZuoLog().Log_C(info);
            return dalRetCode;
        }

        /// <summary>
        /// 获取操作日志信息集合
        /// </summary>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.MCaoZuoLogInfo> GetLogs(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.MCaoZuoLogChaXunInfo chaXun)
        {
            if (pageSize <= 0) pageSize = 1;
            if (pageIndex <= 0) pageIndex = 1;

            var items = dal.GetLogs(pageSize, pageIndex, ref recordCount, chaXun);
            return items;
        }
        #endregion
    }
}
