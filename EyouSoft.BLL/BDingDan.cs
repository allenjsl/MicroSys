//采购订单相关BLL 汪奇志 2015-04-23
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL
{
    /// <summary>
    /// 采购订单相关BLL
    /// </summary>
    public class BDingDan
    {
        private readonly EyouSoft.DAL.DDingDan dal = new EyouSoft.DAL.DDingDan();

        #region constructure
        /// <summary>
        /// default constructor
        /// </summary>
        public BDingDan() { }
        #endregion

        #region private members
        #endregion

        #region internal members
        /// <summary>
        /// 获取采购订单信息集体
        /// </summary>
        /// <param name="caiGouDanId">采购单编号</param>
        /// <returns></returns>
        internal IList<EyouSoft.Model.MDingDanInfo> GetDingDans(string caiGouDanId)
        {
            var chaXun = new EyouSoft.Model.MDingDanChaXunInfo();
            chaXun.CaiGouDanId = caiGouDanId;
            int recordCont = 0;

            var items = GetDingDans(2000, 1, ref recordCont, chaXun);

            return items;
        }
        #endregion

        #region public members
        /// <summary>
        /// 获取订单信息业务实体
        /// </summary>
        /// <param name="dingDanId">订单编号</param>
        /// <returns></returns>
        public EyouSoft.Model.MDingDanInfo GetInfo(string dingDanId)
        {
            if (string.IsNullOrEmpty(dingDanId)) return null;

            var info = dal.GetInfo(dingDanId);
            return info;
        }

        /// <summary>
        /// 获取订单信息集合
        /// </summary>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.MDingDanInfo> GetDingDans(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.MDingDanChaXunInfo chaXun)
        {
            object[] heJi;
            var items = GetDingDans(pageSize, pageIndex, ref recordCount, chaXun, out heJi);
            return items;
        }

        /// <summary>
        /// 获取订单信息集合
        /// </summary>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <param name="heJi">合计信息</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.MDingDanInfo> GetDingDans(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.MDingDanChaXunInfo chaXun,out object[] heJi)
        {
            heJi = new object[] { 0M,0M };
            if (pageSize <= 0) pageSize = 1;
            if (pageIndex <= 0) pageIndex = 1;
           
            var items = dal.GetDingDans(pageSize, pageIndex, ref recordCount, chaXun, out heJi);
            return items;
        }

        /// <summary>
        /// 设置订单状态，返回1成功，其它失败
        /// </summary>
        /// <param name="dingDanId">订单编号</param>
        /// <param name="status">订单状态</param>
        /// <param name="caoZuoRenId">操作人编号</param>
        /// <returns></returns>
        public int SheZhiStatus(string dingDanId, EyouSoft.Model.DingDanStatus status, string caoZuoRenId)
        {
            if (string.IsNullOrEmpty(dingDanId) || string.IsNullOrEmpty(caoZuoRenId)) return 0;

            var info = GetInfo(dingDanId);
            if (info == null) return -1;
            if (info.Status == EyouSoft.Model.DingDanStatus.计划采购) return -2;
            var buKeQuXiaoStatus = new[] { EyouSoft.Model.DingDanStatus.供应商发货完成, EyouSoft.Model.DingDanStatus.采购商确认收货 };
            if (status == EyouSoft.Model.DingDanStatus.取消采购 && buKeQuXiaoStatus.Contains(info.Status)) return -3;

            int dalRetCode = dal.SheZhiStatus(dingDanId, status, caoZuoRenId, DateTime.Now);

            #region 消息处理
            if (dalRetCode == 1)
            {
                var xiaoXiInfo = new EyouSoft.Model.MXiaoXiInfo();

                xiaoXiInfo.BiaoTi = string.Empty;
                xiaoXiInfo.ChuLiRenId = string.Empty;
                xiaoXiInfo.ChuLiTime = null;
                xiaoXiInfo.FaChuGongSiId = string.Empty;
                xiaoXiInfo.FaChuRenId = caoZuoRenId;
                xiaoXiInfo.FaChuTime = DateTime.Now;
                xiaoXiInfo.GuanLianId = dingDanId;
                xiaoXiInfo.JieShouGongSiId = string.Empty;
                xiaoXiInfo.JieShouRenId = string.Empty;
                xiaoXiInfo.LeiXing = EyouSoft.Model.XiaoXiLeiXing.None;
                xiaoXiInfo.NeiRong = string.Empty;
                xiaoXiInfo.Status = EyouSoft.Model.XiaoXiStatus.未读;
                xiaoXiInfo.XiaoXiId = string.Empty;

                if (status == EyouSoft.Model.DingDanStatus.供应商完成报价)
                {
                    xiaoXiInfo.BiaoTi = "待确认报价";
                    xiaoXiInfo.FaChuGongSiId = info.GysId;
                    xiaoXiInfo.JieShouGongSiId = info.CgsId;
                    xiaoXiInfo.LeiXing = EyouSoft.Model.XiaoXiLeiXing.采购商待确认报价;
                    xiaoXiInfo.NeiRong = "您有一个采购单报价信息需要确认，采购单号：" + info.CaiGouDanHao + "。";
                }

                if (status == EyouSoft.Model.DingDanStatus.采购商确认报价)
                {
                    xiaoXiInfo.BiaoTi = "待发货";
                    xiaoXiInfo.FaChuGongSiId = info.CgsId;
                    xiaoXiInfo.JieShouGongSiId = info.GysId;
                    xiaoXiInfo.LeiXing = EyouSoft.Model.XiaoXiLeiXing.供应商待发货;
                    xiaoXiInfo.NeiRong = "您有一个采购单需要发货，采购单号：" + info.CaiGouDanHao + "。";
                }

                if (status == EyouSoft.Model.DingDanStatus.供应商发货完成)
                {
                    xiaoXiInfo.BiaoTi = "待收货";
                    xiaoXiInfo.FaChuGongSiId = info.GysId;
                    xiaoXiInfo.JieShouGongSiId = info.CgsId;
                    xiaoXiInfo.LeiXing = EyouSoft.Model.XiaoXiLeiXing.采购商待确认收货;
                    xiaoXiInfo.NeiRong = "您有一个采购单供应商已发货，待收货确认，采购单号：" + info.CaiGouDanHao + "。";
                }

                BXiaoXi.XiaoXi_C(xiaoXiInfo);
            }
            #endregion

            if (dalRetCode == 1)
            {
                var logInfo = new EyouSoft.Model.MCaoZuoLogInfo();
                logInfo.BiaoTi = "设置订单状态";
                logInfo.NeiRong = "设置订单状态，订单编号：" + info.DingDanId + "，订单状态：" + status + "。";
                logInfo.GuanLianId = info.DingDanId;
                BCaoZuoLog.Log_C(logInfo);
            }
            return dalRetCode;
        }

        /// <summary>
        /// 设置订单报价信息业务实体，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int SheZhiBaoJiaInfo(EyouSoft.Model.MDingDanBaoJiaInfo info)
        {
            if (info == null || string.IsNullOrEmpty(info.DingDanId) || info.ChanPins == null || info.ChanPins.Count == 0) return 0;
            int dalRetCode = dal.SheZhiBaoJiaInfo(info);
            return dalRetCode;
        }

        /// <summary>
        /// 设置订单发货信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int SheZhiFaShuoInfo(EyouSoft.Model.MDingDanFaHuoInfo info)
        {
            if (info == null || string.IsNullOrEmpty(info.DingDanId) || info.ChanPins == null || info.ChanPins.Count == 0) return 0;

            int dalRetCode = dal.SheZhiFaShuoInfo(info);
            return dalRetCode;
        }

        /// <summary>
        /// 设置订单收货信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int SheZhiShouHuoInfo(EyouSoft.Model.MDingDanShouHuoInfo info)
        {
            if (info == null || string.IsNullOrEmpty(info.DingDanId) || info.ChanPins == null || info.ChanPins.Count == 0) return 0;

            int dalRetCode = dal.SheZhiShouHuoInfo(info);
            return dalRetCode;
        }

        /// <summary>
        /// 供应商到货确认，返回1成功，其它失败
        /// </summary>
        /// <param name="dingDanId">订单编号</param>
        /// <param name="caoZuoRenId">操作人编号</param>
        /// <returns></returns>
        public int GysDaoHuoQueRen(string dingDanId, string caoZuoRenId)
        {
            if (string.IsNullOrEmpty(dingDanId) || string.IsNullOrEmpty(caoZuoRenId)) return 0;
            var info = GetInfo(dingDanId);
            if (info == null) return -1;
            if (info.Status != EyouSoft.Model.DingDanStatus.采购商确认收货) return -2;

            int dalRetCode = dal.SheZhiGysDaoHuoQueRenStatus(dingDanId, EyouSoft.Model.QueRenStatus.已确认, caoZuoRenId, DateTime.Now);
            if (dalRetCode == 1)
            {
                var logInfo = new EyouSoft.Model.MCaoZuoLogInfo();
                logInfo.BiaoTi = "到货确认";
                logInfo.NeiRong = "到货确认，订单编号：" + info.DingDanId + "。";
                logInfo.GuanLianId = info.DingDanId;
                BCaoZuoLog.Log_C(logInfo);
            }
            return dalRetCode;
        }
        #endregion
    }
}
