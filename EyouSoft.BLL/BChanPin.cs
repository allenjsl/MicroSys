//供应商产品信息相关BLL 汪奇志 2015-04-22
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL
{
    /// <summary>
    /// 供应商产品信息相关BLL
    /// </summary>
    public class BChanPin
    {
        private readonly EyouSoft.DAL.DChanPin dal = new EyouSoft.DAL.DChanPin();

        #region constructure
        /// <summary>
        /// default constructor
        /// </summary>
        public BChanPin() { }
        #endregion

        #region private members
        #endregion

        #region public members
        /// <summary>
        /// 产品添加，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int ChanPin_C(EyouSoft.Model.MChanPinInfo info)
        {
            if (info == null 
                || string.IsNullOrEmpty(info.CaoZuoRenId) 
                || string.IsNullOrEmpty(info.GysId) 
                || string.IsNullOrEmpty(info.Name)) return 0;

            info.ChanPinId = Guid.NewGuid().ToString();
            info.IssueTime = DateTime.Now;

            int dalRetCode = dal.ChanPin_CU(info);
            if (dalRetCode == 1)
            {
                var logInfo = new EyouSoft.Model.MCaoZuoLogInfo();
                logInfo.BiaoTi = "添加产品信息";
                logInfo.NeiRong = "添加产品信息，产品编号：" + info.ChanPinId + "。";
                logInfo.GuanLianId = info.ChanPinId;
                BCaoZuoLog.Log_C(logInfo);
            }
            return dalRetCode;
        }

        /// <summary>
        /// 产品修改，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int ChanPin_U(EyouSoft.Model.MChanPinInfo info)
        {
            if (info == null
                || string.IsNullOrEmpty(info.CaoZuoRenId)
                || string.IsNullOrEmpty(info.GysId)
                || string.IsNullOrEmpty(info.Name)
                || string.IsNullOrEmpty(info.ChanPinId)) return 0;

            info.IssueTime = DateTime.Now;

            int dalRetCode = dal.ChanPin_CU(info);
            if (dalRetCode == 1)
            {
                var logInfo = new EyouSoft.Model.MCaoZuoLogInfo();
                logInfo.BiaoTi = "修改产品信息";
                logInfo.NeiRong = "修改产品信息，产品编号：" + info.ChanPinId + "。";
                logInfo.GuanLianId = info.ChanPinId;
                BCaoZuoLog.Log_C(logInfo);
            }
            return dalRetCode;
        }

        /// <summary>
        /// 产品删除，返回1成功，其它失败
        /// </summary>
        /// <param name="gysId">供应商编号</param>
        /// <param name="chanPinId">产品编号</param>
        /// <returns></returns>
        public int ChanPin_D(string gysId, string chanPinId)
        {
            if (string.IsNullOrEmpty(gysId) || string.IsNullOrEmpty(chanPinId)) return 0;
            int dalRetCode = dal.ChanPin_D(gysId, chanPinId);
            if (dalRetCode == 1)
            {
                var logInfo = new EyouSoft.Model.MCaoZuoLogInfo();
                logInfo.BiaoTi = "删除产品信息";
                logInfo.NeiRong = "删除产品信息，产品编号：" + chanPinId + "。";
                logInfo.GuanLianId = chanPinId;
                BCaoZuoLog.Log_C(logInfo);
            }
            return dalRetCode;
        }

        /// <summary>
        /// 获取产品信息业务实体
        /// </summary>
        /// <param name="chanPinId">产品编号</param>
        /// <returns></returns>
        public EyouSoft.Model.MChanPinInfo GetInfo(string chanPinId)
        {
            if (string.IsNullOrEmpty(chanPinId)) return null;
            var info = dal.GetInfo(chanPinId);
            return info;
        }

        /// <summary>
        /// 获取产品信息集合
        /// </summary>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.MChanPinInfo> GetChanPins(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.MChanPinChaXunInfo chaXun)
        {
            if (pageSize <= 0) pageSize = 1;
            if (pageIndex <= 0) pageIndex = 1;

            var items = dal.GetChanPins(pageSize, pageIndex, ref recordCount, chaXun);
            return items;
        }

        /// <summary>
        /// 产品价格新增，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int ChanPinJiaGe_C(EyouSoft.Model.MChanPinJiaGeInfo info)
        {
            if (info == null
                || string.IsNullOrEmpty(info.CaoZuoRenId)
                || string.IsNullOrEmpty(info.ChanPinId)) return 0;

            info.JiaGeId = Guid.NewGuid().ToString();
            info.IssueTime = DateTime.Now;

            int dalRetCode = dal.ChanPinJiaGe_C(info);
            if (dalRetCode == 1)
            {
                var logInfo = new EyouSoft.Model.MCaoZuoLogInfo();
                logInfo.BiaoTi = "维护产品价格";
                logInfo.NeiRong = "维护产品价格，价格编号：" + info.JiaGeId + "。";
                logInfo.GuanLianId = info.JiaGeId;
                BCaoZuoLog.Log_C(logInfo);
            }
            return dalRetCode;
        }

        /// <summary>
        /// 获取产品价格信息集合
        /// </summary>
        /// <param name="chanPinId">产品编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.MChanPinJiaGeInfo> GetChanPinJiaGes(string chanPinId)
        {
            if (string.IsNullOrEmpty(chanPinId)) return null;
            var items = dal.GetChanPinJiaGes(chanPinId);
            return items;
        }
        #endregion

    }
}
