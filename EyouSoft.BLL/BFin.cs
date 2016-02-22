//财务相关BLL 汪奇志 2015-04-23
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL
{
    /// <summary>
    /// 财务相关BLL
    /// </summary>
    public class BFin
    {
        private readonly EyouSoft.DAL.DFin dal = new EyouSoft.DAL.DFin();

        #region constructure
        /// <summary>
        /// default constructor
        /// </summary>
        public BFin() { }
        #endregion

        #region private members
        #endregion

        #region public members
        /// <summary>
        /// 设置采购商付款状态，返回1成功，其它失败
        /// </summary>
        /// <param name="cgsId">采购商编号</param>
        /// <param name="dingDanId">订单编号</param>
        /// <param name="status">付款状态</param>
        /// <param name="caoZuoRenId">操作人编号</param>
        /// <returns></returns>
        public int SheZhiCgsFuKuanStatus(string cgsId, string dingDanId, EyouSoft.Model.FuKuanStatus status, string caoZuoRenId)
        {
            if (string.IsNullOrEmpty(cgsId) || string.IsNullOrEmpty(dingDanId) || string.IsNullOrEmpty(caoZuoRenId)) return 0;

            int dalRetCode = dal.SheZhiCgsFuKuanStatus(cgsId, dingDanId, status, caoZuoRenId, DateTime.Now);

            if (dalRetCode == 1)
            {
                var logInfo = new EyouSoft.Model.MCaoZuoLogInfo();
                logInfo.BiaoTi = "采购商设置付款状态";
                logInfo.NeiRong = "采购商设置付款状态，订单编号：" + dingDanId + "，付款状态：" + status + "。";
                logInfo.GuanLianId = dingDanId;
                BCaoZuoLog.Log_C(logInfo);
            }

            return dalRetCode;
        }
        #endregion
    }
}
