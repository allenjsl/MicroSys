//采购商、供应商信息相关BLL 汪奇志 2015-04-21
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL
{
    /// <summary>
    /// 采购商、供应商信息相关BLL
    /// </summary>
    public class BGongSi
    {
        private readonly EyouSoft.DAL.DGongSi dal = new EyouSoft.DAL.DGongSi();

        #region constructure
        /// <summary>
        /// default constructor
        /// </summary>
        public BGongSi() { }
        #endregion

        #region private members
        #endregion

        #region public members
        /// <summary>
        /// 采购商、供应商信息添加，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int GongSi_C(EyouSoft.Model.MGongSiInfo info)
        {
            if (info == null || string.IsNullOrEmpty(info.Name) || string.IsNullOrEmpty(info.CaoZuoRenId)) return 0;

            info.GongSiId = Guid.NewGuid().ToString();
            info.IssueTime = DateTime.Now;

            int dalRetCode = dal.GongSi_CU(info);
            if (dalRetCode == 1)
            {
                var logInfo = new EyouSoft.Model.MCaoZuoLogInfo();
                logInfo.BiaoTi = "添加公司信息";
                logInfo.NeiRong = "添加公司信息，公司编号：" + info.GongSiId + "，公司类型："+info.LeiXing+"。";
                logInfo.GuanLianId = info.GongSiId;
                BCaoZuoLog.Log_C(logInfo);
            }
            return dalRetCode;
        }

        /// <summary>
        /// 采购商、供应商信息修改，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int GongSi_U(EyouSoft.Model.MGongSiInfo info)
        {
            if (info == null || string.IsNullOrEmpty(info.Name) || string.IsNullOrEmpty(info.GongSiId) || string.IsNullOrEmpty(info.CaoZuoRenId)) return 0;
            info.IssueTime = DateTime.Now;
            int dalRetCode = dal.GongSi_CU(info);
            if (dalRetCode == 1)
            {
                var logInfo = new EyouSoft.Model.MCaoZuoLogInfo();
                logInfo.BiaoTi = "修改公司信息";
                logInfo.NeiRong = "修改公司信息，公司编号：" + info.GongSiId + "。";
                logInfo.GuanLianId = info.GongSiId;
                BCaoZuoLog.Log_C(logInfo);
            }

            #region 消息处理
            if (dalRetCode == 1
                && info.ShenHeStatus == EyouSoft.Model.ShenHeStatus.未审核
                && !string.IsNullOrEmpty(info.YingYeZhiZhaoFilepath)
                && !string.IsNullOrEmpty(info.ZuZhiJiGouFilepath))
            {
                var xiaoXiInfo = new EyouSoft.Model.MXiaoXiInfo();
                xiaoXiInfo.BiaoTi = "公司注册待审核";
                xiaoXiInfo.ChuLiRenId = string.Empty;
                xiaoXiInfo.ChuLiTime = null;
                xiaoXiInfo.FaChuGongSiId = info.GongSiId;
                xiaoXiInfo.FaChuRenId = info.CaoZuoRenId;
                xiaoXiInfo.FaChuTime = DateTime.Now;
                xiaoXiInfo.GuanLianId = info.GongSiId;
                xiaoXiInfo.JieShouGongSiId = EyouSoft.Model.MGongSiInfo.PingTaiGongSiId;
                xiaoXiInfo.JieShouRenId = string.Empty;
                xiaoXiInfo.LeiXing = EyouSoft.Model.XiaoXiLeiXing.公司注册待审核;
                xiaoXiInfo.NeiRong = "您有一个注册公司信息需要审核，公司名称：" + info.Name + "。";
                xiaoXiInfo.Status = EyouSoft.Model.XiaoXiStatus.未读;
                xiaoXiInfo.XiaoXiId = string.Empty;

                BXiaoXi.XiaoXi_C(xiaoXiInfo);
            }
            #endregion

            return dalRetCode;
        }


        /// <summary>
        /// 采购商、供应商信息删除，返回1成功，其它失败
        /// </summary>
        /// <param name="gongSiId">采购商、供应商公司编号</param>
        /// <returns></returns>
        public int GongSi_D(string gongSiId)
        {
            if (string.IsNullOrEmpty(gongSiId)) return 0;
            int dalRetCode = dal.GongSi_D(gongSiId);
            if (dalRetCode == 1)
            {
                var logInfo = new EyouSoft.Model.MCaoZuoLogInfo();
                logInfo.BiaoTi = "删除公司信息";
                logInfo.NeiRong = "删除公司信息，公司编号：" + gongSiId + "。";
                logInfo.GuanLianId = gongSiId;
                BCaoZuoLog.Log_C(logInfo);
            }
            return dalRetCode;
        }

        /// <summary>
        /// 获取采购商、供应商信息业务实体
        /// </summary>
        /// <param name="gongSiId">采购商、供应商公司编号</param>
        /// <returns></returns>
        public EyouSoft.Model.MGongSiInfo GetInfo(string gongSiId)
        {
            if (string.IsNullOrEmpty(gongSiId)) return null;
            var info = dal.GetInfo(gongSiId);
            return info;
        }

        /// <summary>
        /// 获取采购商、供应商信息集合
        /// </summary>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.MGongSiInfo> GetGongSis(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.MGongSiChaXunInfo chaXun)
        {
            if (pageSize <= 0) pageSize = 1;
            if (pageIndex <= 0) pageIndex = 1;

            object[] heJi;
            var items = dal.GetGongSis(pageSize, pageIndex, ref recordCount, chaXun, out heJi);
            return items;
        }

        /// <summary>
        /// 获取采购商、供应商信息集合
        /// </summary>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.MGongSiInfo> GetGongSis(EyouSoft.Model.MGongSiChaXunInfo chaXun)
        {
            int recordCount = 0;

            var items = GetGongSis(2000, 1, ref recordCount, chaXun);

            //var items = dal.GetGongSis(chaXun);
            return items;
        }

        /// <summary>
        /// 设置公司关系，返回1成功，其它失败
        /// </summary>
        /// <param name="gongSiId1">公司编号1</param>
        /// <param name="gongSiId2">公司编号2</param>
        /// <param name="caoZuoRenId">操作人编号</param>
        /// <returns></returns>
        public int GuanXi_C(string gongSiId1, string gongSiId2, string caoZuoRenId)
        {
            if (string.IsNullOrEmpty(gongSiId1) 
                || string.IsNullOrEmpty(gongSiId2) 
                || string.IsNullOrEmpty(caoZuoRenId)) return 0;
            int dalRetCode = dal.GuanXi_C(gongSiId1, gongSiId2, caoZuoRenId);
            if (dalRetCode == 1)
            {
                var logInfo = new EyouSoft.Model.MCaoZuoLogInfo();
                logInfo.BiaoTi = "设置公司关系";
                logInfo.NeiRong = "设置公司关系，对方公司编号：" + gongSiId2 + "。";
                logInfo.GuanLianId =string.Empty;
                BCaoZuoLog.Log_C(logInfo);
            }
            return dalRetCode;
        }

        /// <summary>
        /// 删除公司关系，返回1成功，其它失败
        /// </summary>
        /// <param name="gongSiId1">公司编号1</param>
        /// <param name="gongSiId2">公司编号2</param>
        /// <returns></returns>
        public int GuanXi_D(string gongSiId1, string gongSiId2)
        {
            if (string.IsNullOrEmpty(gongSiId1)
               || string.IsNullOrEmpty(gongSiId2)) return 0;
            int dalRetCode = dal.GuanXi_D(gongSiId1, gongSiId2);
            if (dalRetCode == 1)
            {
                var logInfo = new EyouSoft.Model.MCaoZuoLogInfo();
                logInfo.BiaoTi = "删除公司关系";
                logInfo.NeiRong = "删除公司关系，对方公司编号：" + gongSiId2 + "。";
                logInfo.GuanLianId = string.Empty;
                BCaoZuoLog.Log_C(logInfo);
            }
            return dalRetCode;
        }

        /// <summary>
        /// 获取查询实体中指定公司编号的公司关系集合
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.MGongSiGuanXiInfo> GetGongSiGuanXis(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.MGongSiGuanXiChaXunInfo chaXun)
        {
            if (pageSize <= 0) pageSize = 1;
            if (pageIndex <= 0) pageIndex = 1;

            if (chaXun == null || string.IsNullOrEmpty(chaXun.GongSiId)) return null;

            var items = dal.GetGongSiGuanXis(pageSize, pageIndex, ref recordCount, chaXun);
            return items;
        }
        #endregion
    }
}
