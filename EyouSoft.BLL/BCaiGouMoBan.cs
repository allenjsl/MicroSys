//采购模板相关BLL 汪奇志 2015-04-22
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL
{
    /// <summary>
    /// 采购模板相关BLL
    /// </summary>
    public class BCaiGouMoBan
    {
        private readonly EyouSoft.DAL.DCaiGouMoBan dal = new EyouSoft.DAL.DCaiGouMoBan();

        #region constructure
        /// <summary>
        /// default constructor
        /// </summary>
        public BCaiGouMoBan() { }
        #endregion

        #region private members
        #endregion

        #region public members
        /// <summary>
        /// 采购模板添加，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int MoBan_C(EyouSoft.Model.MCaiGouMoBanInfo info)
        {
            if (info == null 
                || string.IsNullOrEmpty(info.CaoZuoRenId) 
                || string.IsNullOrEmpty(info.Name)
                || string.IsNullOrEmpty(info.CgsId)) return 0;
            if (info.ChanPins == null || info.ChanPins.Count == 0) return 0;

            info.MoBanId = Guid.NewGuid().ToString();
            info.IssueTime = DateTime.Now;

            foreach (var item in info.ChanPins)
            {
                item.Id = Guid.NewGuid().ToString();
            }

            int dalRetCode = dal.MoBan_CU(info);
            if (dalRetCode == 1)
            {
                var logInfo = new EyouSoft.Model.MCaoZuoLogInfo();
                logInfo.BiaoTi = "添加采购模板";
                logInfo.NeiRong = "添加采购模板，模板编号：" + info.MoBanId + "。";
                logInfo.GuanLianId = info.MoBanId;
                BCaoZuoLog.Log_C(logInfo);
            }
            return dalRetCode;
        }

        /// <summary>
        /// 采购模板修改，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int MoBan_U(EyouSoft.Model.MCaiGouMoBanInfo info)
        {
            if (info == null
                || string.IsNullOrEmpty(info.CaoZuoRenId)
                || string.IsNullOrEmpty(info.Name)
                || string.IsNullOrEmpty(info.CgsId)
                || string.IsNullOrEmpty(info.MoBanId)) return 0;

            if (info.ChanPins == null || info.ChanPins.Count == 0) return 0;

            info.IssueTime = DateTime.Now;
            foreach (var item in info.ChanPins)
            {
                if (string.IsNullOrEmpty(item.Id))
                {
                    item.Id = Guid.NewGuid().ToString();
                }
            }

            int dalRetCode = dal.MoBan_CU(info);
            if (dalRetCode == 1)
            {
                var logInfo = new EyouSoft.Model.MCaoZuoLogInfo();
                logInfo.BiaoTi = "修改采购模板";
                logInfo.NeiRong = "修改采购模板，模板编号：" + info.MoBanId + "。";
                logInfo.GuanLianId = info.MoBanId;
                BCaoZuoLog.Log_C(logInfo);
            }
            return dalRetCode;
        }

        /// <summary>
        /// 采购模板删除，返回1成功，其它失败
        /// </summary>
        /// <param name="cgsId">采购商编号</param>
        /// <param name="moBanId">模板编号</param>
        /// <returns></returns>
        public int MoBan_D(string cgsId, string moBanId)
        {
            if (string.IsNullOrEmpty(cgsId) || string.IsNullOrEmpty(moBanId)) return 0;
            int dalRetCode = dal.MoBan_D(cgsId, moBanId);
            if (dalRetCode == 1)
            {
                var logInfo = new EyouSoft.Model.MCaoZuoLogInfo();
                logInfo.BiaoTi = "删除采购模板";
                logInfo.NeiRong = "删除采购模板，模板编号：" + moBanId + "。";
                logInfo.GuanLianId = moBanId;
                BCaoZuoLog.Log_C(logInfo);
            }
            return dalRetCode;
        }

        /// <summary>
        /// 获取采购模板信息业务实体
        /// </summary>
        /// <param name="moBanId">采购模板编号</param>
        /// <returns></returns>
        public EyouSoft.Model.MCaiGouMoBanInfo GetInfo(string moBanId)
        {
            if (string.IsNullOrEmpty(moBanId)) return null;
            var info = dal.GetInfo(moBanId);

            if (info != null && info.ChanPins != null && info.ChanPins.Count > 0)
            {
                foreach (var item in info.ChanPins)
                {
                    item.ChanPinJiaGe = dal.GetChanPinShangCiJiaGe(item.GysId, info.CgsId, item.ChanPinId);
                }
            }

            return info;
        }

        /// <summary>
        /// 获取采购模板信息集合
        /// </summary>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.MCaiGouMoBanInfo> GetMoBans(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.MCaiGouMoBanChaXunInfo chaXun)
        {
            if (pageSize <= 0) pageSize = 1;
            if (pageIndex <= 0) pageIndex = 1;
            var items = dal.GetMoBans(pageSize, pageIndex, ref recordCount, chaXun);
            return items;           
        }

        /// <summary>
        /// 获取采购模板信息信息
        /// </summary>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.MCaiGouMoBanInfo> GetMoBans(EyouSoft.Model.MCaiGouMoBanChaXunInfo chaXun)
        {
            int pageSize = 2000;
            int pageIndex = 1;
            int recordCount = 0;

            return GetMoBans(pageSize, pageIndex, ref recordCount, chaXun);
        }

        /// <summary>
        /// 设置采购商默认模板，返回1成功，其它失败
        /// </summary>
        /// <param name="cgsId">采购商编号</param>
        /// <param name="moBanId">模板编号</param>
        /// <returns></returns>
        public int SheZhiMoRenMoBan(string cgsId, string moBanId)
        {
            if (string.IsNullOrEmpty(cgsId) || string.IsNullOrEmpty(moBanId)) return 0;
            int dalRetCode = dal.SheZhiMoRenMoBan(cgsId, moBanId);
            if (dalRetCode == 1)
            {
                var logInfo = new EyouSoft.Model.MCaoZuoLogInfo();
                logInfo.BiaoTi = "设置默认采购模板";
                logInfo.NeiRong = "设置默认采购模板，模板编号：" + moBanId + "。";
                logInfo.GuanLianId = moBanId;
                BCaoZuoLog.Log_C(logInfo);
            }
            return dalRetCode;
        }

        /// <summary>
        /// 获取采购商默认模板信息业务实体
        /// </summary>
        /// <param name="cgsId">采购商编号</param>
        /// <returns></returns>
        EyouSoft.Model.MCaiGouMoBanInfo GetMoRenMoBanInfo(string cgsId)
        {
            if (string.IsNullOrEmpty(cgsId)) return null;

            var info = dal.GetMoRenMoBanInfo(cgsId);
            return info;
        }
        #endregion

    }
}
