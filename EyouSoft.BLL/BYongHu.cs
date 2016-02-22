//采购商、供应商、平台用户信息相关BLL 汪奇志 2015-04-21
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL
{
    /// <summary>
    /// 采购商、供应商、平台用户信息相关BLL
    /// </summary>
    public class BYongHu
    {
        private readonly EyouSoft.DAL.DYongHu dal = new EyouSoft.DAL.DYongHu();

        #region constructure
        /// <summary>
        /// default constructor
        /// </summary>
        public BYongHu() { }
        #endregion

        #region private members
        #endregion

        #region public members
        /// <summary>
        /// 采购商、供应商、平台用户信息添加，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int YongHu_C(EyouSoft.Model.MYongHuInfo info)
        {
            if (info == null
                || string.IsNullOrEmpty(info.Username)
                || string.IsNullOrEmpty(info.PasswordMD5)
                || string.IsNullOrEmpty(info.CaoZuoRenId)
                || string.IsNullOrEmpty(info.GongSiId)) return 0;

            info.YongHuId = Guid.NewGuid().ToString();
            info.IssueTime = DateTime.Now;

            int dalRetCode = dal.YongHu_CU(info);
            if (dalRetCode == 1)
            {
                var logInfo = new EyouSoft.Model.MCaoZuoLogInfo();
                logInfo.BiaoTi = "添加用户信息";
                logInfo.NeiRong = "添加用户信息，用户编号：" + info.YongHuId + "。";
                logInfo.GuanLianId = info.YongHuId;
                BCaoZuoLog.Log_C(logInfo);
            }
            return dalRetCode;
        }

        /// <summary>
        /// 采购商、供应商、平台用户信息修改，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int YongHu_U(EyouSoft.Model.MYongHuInfo info)
        {
            if (info == null
                || string.IsNullOrEmpty(info.Username)
                || string.IsNullOrEmpty(info.YongHuId)
                || string.IsNullOrEmpty(info.CaoZuoRenId)
                || string.IsNullOrEmpty(info.GongSiId)) return 0;

            info.IssueTime = DateTime.Now;

            int dalRetCode = dal.YongHu_CU(info);
            if (dalRetCode == 1)
            {
                var logInfo = new EyouSoft.Model.MCaoZuoLogInfo();
                logInfo.BiaoTi = "修改用户信息";
                logInfo.NeiRong = "修改用户信息，用户编号：" + info.YongHuId + "。";
                logInfo.GuanLianId = info.YongHuId;
                BCaoZuoLog.Log_C(logInfo);
            }
            return dalRetCode;
        }

        /// <summary>
        /// 采购商、供应商、平台用户信息删除，返回1成功，其它失败
        /// </summary>
        /// <param name="gongSiId">采购商、供应商编号</param>
        /// <param name="yongHuId">用户编号</param>
        /// <returns></returns>
        public int YongHu_D(string gongSiId, string yongHuId)
        {
            if (string.IsNullOrEmpty(yongHuId)) return 0;

            int dalRetCode = dal.YongHu_D(gongSiId, yongHuId);
            if (dalRetCode == 1)
            {
                var logInfo = new EyouSoft.Model.MCaoZuoLogInfo();
                logInfo.BiaoTi = "删除用户信息";
                logInfo.NeiRong = "删除用户信息，用户编号：" + yongHuId + "。";
                logInfo.GuanLianId = yongHuId;
                BCaoZuoLog.Log_C(logInfo);
            }
            return dalRetCode;
        }

        /// <summary>
        /// 获取采购商、供应商、平台用户信息业务实体
        /// </summary>
        /// <param name="yongHuId">用户编号</param>
        /// <returns></returns>
        public EyouSoft.Model.MYongHuInfo GetInfo(string yongHuId)
        {
            if (string.IsNullOrEmpty(yongHuId)) return null;

            var info = dal.GetInfo(yongHuId);

            return info;
        }

        /// <summary>
        /// 获取采购商、供应商、平台用户信息集合
        /// </summary>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.MYongHuInfo> GetYongHus(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.MYongHuChaXunInfo chaXun)
        {
            if (pageSize <= 0) pageSize = 1;
            if (pageIndex <= 0) pageIndex = 1;

            var items = dal.GetYongHus(pageSize, pageIndex, ref recordCount, chaXun);
            return items;
        }

        /// <summary>
        /// 启用用户，返回1成功，其它失败
        /// </summary>
        /// <param name="gongSiId">公司编号</param>
        /// <param name="yongHuId">用户编号</param>
        /// <returns></returns>
        public int QiYong(string gongSiId, string yongHuId)
        {
            if (string.IsNullOrEmpty(gongSiId) || string.IsNullOrEmpty(yongHuId)) return 0;
            int dalRetCode = dal.SheZhiStatus(gongSiId, yongHuId, EyouSoft.Model.YongHuStatus.启用);
            if (dalRetCode == 1)
            {
                var logInfo = new EyouSoft.Model.MCaoZuoLogInfo();
                logInfo.BiaoTi = "启用用户";
                logInfo.NeiRong = "启用用户，用户编号：" + yongHuId + "。";
                logInfo.GuanLianId = yongHuId;
                BCaoZuoLog.Log_C(logInfo);
            }
            return dalRetCode;
        }

        /// <summary>
        /// 禁用用户，返回1成功，其它失败
        /// </summary>
        /// <param name="gongSiId">公司编号</param>
        /// <param name="yongHuId">用户编号</param>
        /// <returns></returns>
        public int JinYong(string gongSiId, string yongHuId)
        {
            if (string.IsNullOrEmpty(gongSiId) || string.IsNullOrEmpty(yongHuId)) return 0;
            int dalRetCode = dal.SheZhiStatus(gongSiId, yongHuId, EyouSoft.Model.YongHuStatus.禁用);
            if (dalRetCode == 1)
            {
                var logInfo = new EyouSoft.Model.MCaoZuoLogInfo();
                logInfo.BiaoTi = "禁用用户";
                logInfo.NeiRong = "禁用用户，用户编号：" + yongHuId + "。";
                logInfo.GuanLianId = yongHuId;
                BCaoZuoLog.Log_C(logInfo);
            }
            return dalRetCode;
        }

        /// <summary>
        /// 获取注册用户信息集合
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.MZhuCeYongHuInfo> GetZhuCeYongHus(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.MZhuCeYongHuChaXunInfo chaXun)
        {
            if (pageSize <= 0) pageSize = 1;
            if (pageIndex <= 0) pageIndex = 1;

            var items = dal.GetZhuCeYongHus(pageSize, pageIndex, ref recordCount, chaXun);
            return items;
        }

        /// <summary>
        /// 用户注册，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int YongHu_ZhuCe(EyouSoft.Model.MZhuCeYongHuInfo info)
        {
            if (info == null
                || string.IsNullOrEmpty(info.GongSiName)
                || string.IsNullOrEmpty(info.YongHuMing)
                || string.IsNullOrEmpty(info.PasswordMD5)) return 0;

            info.GongSiId = Guid.NewGuid().ToString();
            info.YongHuId = Guid.NewGuid().ToString();
            info.IssueTime = DateTime.Now;

            int dalRetCode = dal.YongHu_ZhuCe(info);
            if (dalRetCode == 1)
            {
                var logInfo = new EyouSoft.Model.MCaoZuoLogInfo();
                logInfo.BiaoTi = "用户注册";
                logInfo.NeiRong = "用户注册，用户编号：" + info.YongHuId + "。";
                logInfo.GuanLianId = info.YongHuId;
                BCaoZuoLog.Log_C(logInfo);
            }
            return dalRetCode;
        }

        /// <summary>
        /// 用户审核，返回1成功，其它失败
        /// </summary>
        /// <param name="gongSiId">公司编号</param>
        /// <param name="yongHuId">用户编号</param>
        /// <param name="shenHeRenId">审核人编号</param>
        /// <returns></returns>
        public int YongHu_ShenHe(string gongSiId, string yongHuId, string shenHeRenId)
        {
            if (string.IsNullOrEmpty(gongSiId) 
                //|| string.IsNullOrEmpty(yongHuId) 
                || string.IsNullOrEmpty(shenHeRenId)) return 0;

            int dalRetCode = dal.YongHu_ShenHe(gongSiId, yongHuId, shenHeRenId);
            if (dalRetCode == 1)
            {
                var logInfo = new EyouSoft.Model.MCaoZuoLogInfo();
                logInfo.BiaoTi = "注册用户审核";
                logInfo.NeiRong = "注册用户审核，用户编号：" + yongHuId + "。";
                logInfo.GuanLianId = yongHuId;
                BCaoZuoLog.Log_C(logInfo);
            }
            return dalRetCode;
        }
        #endregion
    }
}
