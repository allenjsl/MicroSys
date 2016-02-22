//采购单相关BLL 汪奇志 2015-04-23
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL
{
    /// <summary>
    /// 采购单相关BLL
    /// </summary>
    public class BCaiGouDan
    {
        private readonly EyouSoft.DAL.DCaiGouDan dal = new EyouSoft.DAL.DCaiGouDan();

        #region constructure
        /// <summary>
        /// default constructor
        /// </summary>
        public BCaiGouDan() { }
        #endregion

        #region private members
        /// <summary>
        /// 采购单添加，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int CaiGouDan_C(EyouSoft.Model.MCaiGouDanInfo info)
        {
            if (info == null 
                || string.IsNullOrEmpty(info.CgsId) 
                || string.IsNullOrEmpty(info.CaiGouDanName) 
                || string.IsNullOrEmpty(info.CaoZuoRenId)) return 0;
            if (info.ChanPins == null || info.ChanPins.Count == 0) return 0;

            info.CaiGouDanId = Guid.NewGuid().ToString();
            info.IssueTime = DateTime.Now;

            List<EyouSoft.Model.MDingDanInfo> dingDans = new List<EyouSoft.Model.MDingDanInfo>();

            foreach (var item in info.ChanPins)
            {
                if (string.IsNullOrEmpty(item.GysId)||string.IsNullOrEmpty(item.ChanPinId)) continue;
                var item1 = dingDans.Find(temp => { if (temp.GysId == item.GysId)return true; else return false; });
                item1 = item1 ?? new EyouSoft.Model.MDingDanInfo();

                if (string.IsNullOrEmpty(item1.DingDanId))
                {
                    item1.DingDanId = Guid.NewGuid().ToString();
                    item1.GysId = item.GysId;
                    item1.ChanPins = new List<EyouSoft.Model.MDingDanChanPinInfo>();
                    item1.Status = EyouSoft.Model.DingDanStatus.计划采购;
                    item1.JinE = 0;

                    dingDans.Add(item1);
                }

                var item2 = new EyouSoft.Model.MDingDanChanPinInfo();
                item2.MingXiId = Guid.NewGuid().ToString();
                item2.ChanPinId = item.ChanPinId;
                item2.ShuLiang = item.ShuLiang;

                item1.ChanPins.Add(item2);
            }

            if (dingDans == null || dingDans.Count == 0) return 0;

            int dalRetCode = dal.CaiGouDan_CU(info, dingDans);

            if (dalRetCode == 1)
            {
                var logInfo = new EyouSoft.Model.MCaoZuoLogInfo();
                logInfo.BiaoTi = "添加采购单";
                logInfo.NeiRong = "添加采购单，采购单编号：" + info.CaiGouDanId + "。";
                logInfo.GuanLianId = info.CaiGouDanId;
                BCaoZuoLog.Log_C(logInfo);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 采购单修改，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int CaiGouDan_U(EyouSoft.Model.MCaiGouDanInfo info)
        {
            if (info == null
                || string.IsNullOrEmpty(info.CgsId)
                || string.IsNullOrEmpty(info.CaiGouDanName)
                || string.IsNullOrEmpty(info.CaoZuoRenId)
                || string.IsNullOrEmpty(info.CaiGouDanId)) return 0;
            if (info.ChanPins == null || info.ChanPins.Count == 0) return 0;
            info.IssueTime = DateTime.Now;

            List<EyouSoft.Model.MDingDanInfo> dingDans = new List<EyouSoft.Model.MDingDanInfo>();

            foreach (var item in info.ChanPins)
            {
                if (string.IsNullOrEmpty(item.GysId) || string.IsNullOrEmpty(item.ChanPinId) || string.IsNullOrEmpty(item.DingDanId) || string.IsNullOrEmpty(item.MingXiId)) continue;
                var item1 = dingDans.Find(temp => { if (temp.DingDanId == item.DingDanId && temp.GysId == item.GysId)return true; else return false; });
                item1 = item1 ?? new EyouSoft.Model.MDingDanInfo();

                if (string.IsNullOrEmpty(item1.DingDanId))
                {
                    item1.DingDanId = item.DingDanId;
                    item1.GysId = item.GysId;
                    item1.ChanPins = new List<EyouSoft.Model.MDingDanChanPinInfo>();
                    item1.Status = EyouSoft.Model.DingDanStatus.计划采购;
                    item1.JinE = 0;

                    dingDans.Add(item1);
                }

                var item2 = new EyouSoft.Model.MDingDanChanPinInfo();
                item2.MingXiId = item.MingXiId;
                item2.ChanPinId = item.ChanPinId;
                item2.ShuLiang = item.ShuLiang;

                item1.ChanPins.Add(item2);
            }

            if (dingDans == null || dingDans.Count == 0) return 0;

            int dalRetCode = dal.CaiGouDan_CU(info, dingDans);
            if (dalRetCode == 1)
            {
                var logInfo = new EyouSoft.Model.MCaoZuoLogInfo();
                logInfo.BiaoTi = "修改采购单";
                logInfo.NeiRong = "修改采购单，采购单编号：" + info.CaiGouDanId + "。";
                logInfo.GuanLianId = info.CaiGouDanId;
                BCaoZuoLog.Log_C(logInfo);
            }
            return dalRetCode;
        }

        /// <summary>
        /// 采购单删除，返回1成功，其它失败
        /// </summary>
        /// <param name="cgsId">采购商编号</param>
        /// <param name="caiGouDanId">采购单编号</param>
        /// <returns></returns>
        public int CaiGouDan_D(string cgsId, string caiGouDanId)
        {
            if (string.IsNullOrEmpty(cgsId) || string.IsNullOrEmpty(caiGouDanId)) return 0;

            int dalRetCode = dal.CaiGouDan_D(cgsId, caiGouDanId);
            if (dalRetCode == 1)
            {
                var logInfo = new EyouSoft.Model.MCaoZuoLogInfo();
                logInfo.BiaoTi = "删除采购单";
                logInfo.NeiRong = "删除采购单，采购单编号：" + caiGouDanId + "。";
                logInfo.GuanLianId = caiGouDanId;
                BCaoZuoLog.Log_C(logInfo);
            }
            return dalRetCode;
        }

        /// <summary>
        /// 获取采购单信息业务实体
        /// </summary>
        /// <param name="caiGouDanId">采购单编号</param>
        /// <returns></returns>
        public EyouSoft.Model.MCaiGouDanInfo GetInfo(string caiGouDanId)
        {
            if (string.IsNullOrEmpty(caiGouDanId)) return null;

            var info = dal.GetInfo(caiGouDanId);
            return info;
        }

        /// <summary>
        /// 获取采购单信息集合
        /// </summary>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.MCaiGouDanInfo> GetCaiGouDans(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.MCaiGouDanChaXunInfo chaXun)
        {
            if (pageSize <= 0) pageSize = 1;
            if (pageIndex <= 0) pageIndex = 1;
            var items = dal.GetCaiGouDans(pageSize, pageIndex, ref recordCount, chaXun);
            return items;
        }

        /// <summary>
        /// 采购单发布，返回1成功，其它失败
        /// </summary>
        /// <param name="cgsId">采购商编号</param>
        /// <param name="caiGouDanId">采购单编号</param>
        /// <param name="caoZuoRenId">操作人编号</param>
        /// <returns></returns>
        public int FaBu(string cgsId, string caiGouDanId, string caoZuoRenId)
        {
            if (string.IsNullOrEmpty(cgsId) || string.IsNullOrEmpty(caiGouDanId) || string.IsNullOrEmpty(caoZuoRenId)) return 0;

            int dalRetCode = dal.SheZhiStatus(cgsId, caiGouDanId, EyouSoft.Model.CaiGouDanStatus.已下单, caoZuoRenId, DateTime.Now);

            #region 消息处理
            if (dalRetCode == 1)
            {
                var xiaoXiInfo = new EyouSoft.Model.MXiaoXiInfo();

                xiaoXiInfo.BiaoTi = "待报价";
                xiaoXiInfo.ChuLiRenId = string.Empty;
                xiaoXiInfo.ChuLiTime = null;
                xiaoXiInfo.FaChuGongSiId = cgsId;
                xiaoXiInfo.FaChuRenId = caoZuoRenId;
                xiaoXiInfo.FaChuTime = DateTime.Now;
                xiaoXiInfo.GuanLianId = string.Empty;
                xiaoXiInfo.JieShouGongSiId = string.Empty;
                xiaoXiInfo.JieShouRenId = string.Empty;
                xiaoXiInfo.LeiXing = EyouSoft.Model.XiaoXiLeiXing.供应商待报价;
                xiaoXiInfo.NeiRong = "您有一个采购单需要报价";
                xiaoXiInfo.Status = EyouSoft.Model.XiaoXiStatus.未读;
                xiaoXiInfo.XiaoXiId = string.Empty;

                var caiGouDingDanItems = new EyouSoft.BLL.BDingDan().GetDingDans(caiGouDanId);
                if (caiGouDingDanItems != null && caiGouDingDanItems.Count > 0)
                {
                    foreach (var item in caiGouDingDanItems)
                    {
                        xiaoXiInfo.GuanLianId = item.DingDanId;
                        xiaoXiInfo.JieShouGongSiId = item.GysId;
                        xiaoXiInfo.NeiRong = "您有一个采购单需要报价，采购单号：" + item.CaiGouDanHao + "。";

                        BXiaoXi.XiaoXi_C(xiaoXiInfo);
                    }
                }
            }
            #endregion

            if (dalRetCode == 1)
            {
                var logInfo = new EyouSoft.Model.MCaoZuoLogInfo();
                logInfo.BiaoTi = "发布采购信息";
                logInfo.NeiRong = "发布采购信息，采购单编号：" + caiGouDanId + "。";
                logInfo.GuanLianId = caiGouDanId;
                BCaoZuoLog.Log_C(logInfo);
            }
            return dalRetCode;
        }
        #endregion
    }
}
