//地址相关BLL 汪奇志 2015-05-29
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL
{
    /// <summary>
    /// 地址相关BLL
    /// </summary>
    public class BDiZhi
    {
        private readonly EyouSoft.DAL.DDiZhi dal = new EyouSoft.DAL.DDiZhi();

        #region constructure
        /// <summary>
        /// default constructor
        /// </summary>
        public BDiZhi() { }
        #endregion

        #region private members
        #endregion

        #region public members
        /// <summary>
        /// 地址新增，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int DiZhi_C(EyouSoft.Model.MDiZhiInfo info)
        {
            if (info == null 
                || string.IsNullOrEmpty(info.CaoZuoRenId) 
                || string.IsNullOrEmpty(info.Name)) return 0;

            info.IssueTime = DateTime.Now;
            info.DiZhiId = Guid.NewGuid().ToString();

            var dalRetCode = dal.DiZhi_C(info);
            if (dalRetCode == 1)
            {
                var logInfo = new EyouSoft.Model.MCaoZuoLogInfo();
                logInfo.BiaoTi = "添加地址";
                logInfo.NeiRong = "添加地址，地址编号：" + info.DiZhiId + "。";
                logInfo.GuanLianId = info.DiZhiId;
                BCaoZuoLog.Log_C(logInfo);
            }
            return dalRetCode;
        }

        /// <summary>
        /// 地址修改，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int DiZhi_U(EyouSoft.Model.MDiZhiInfo info)
        {
            if (info == null
                || string.IsNullOrEmpty(info.CaoZuoRenId)
                || string.IsNullOrEmpty(info.Name)
                ||string.IsNullOrEmpty(info.DiZhiId)) return 0;

            info.IssueTime = DateTime.Now;

            var dalRetCode = dal.DiZhi_U(info);
            if (dalRetCode == 1)
            {
                var logInfo = new EyouSoft.Model.MCaoZuoLogInfo();
                logInfo.BiaoTi = "修改地址";
                logInfo.NeiRong = "修改地址，地址编号：" + info.DiZhiId + "。";
                logInfo.GuanLianId = info.DiZhiId;
                BCaoZuoLog.Log_C(logInfo);
            }
            return dalRetCode;
        }

        /// <summary>
        /// 地址删除，返回1成功，其它失败
        /// </summary>
        /// <param name="gongSiId">公司编号</param>
        /// <param name="diZhiId">地址编号</param>
        /// <returns></returns>
        public int DiZhi_D(string gongSiId, string diZhiId)
        {
            if (string.IsNullOrEmpty(gongSiId) 
                || string.IsNullOrEmpty(diZhiId)) return 0;

            var dalRetCode = dal.DiZhi_D(gongSiId,diZhiId);
            if (dalRetCode == 1)
            {
                var logInfo = new EyouSoft.Model.MCaoZuoLogInfo();
                logInfo.BiaoTi = "删除地址";
                logInfo.NeiRong = "删除地址，地址编号：" + diZhiId + "。";
                logInfo.GuanLianId = diZhiId;
                BCaoZuoLog.Log_C(logInfo);
            }
            return dalRetCode;
        }

        /// <summary>
        /// 获取地址实体
        /// </summary>
        /// <param name="diZhiId">地址编号</param>
        /// <returns></returns>
        public EyouSoft.Model.MDiZhiInfo GetInfo(string diZhiId)
        {
            var info = dal.GetInfo(diZhiId);
            return info;
        }

        /// <summary>
        /// 获取地址集合
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页面序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.MDiZhiInfo> GetDiZhis(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.MDiZhiChaXunInfo chaXun)
        {
            if (pageSize <= 0) pageSize = 1;
            if (pageIndex <= 0) pageIndex = 1;
            var items = dal.GetDiZhis(pageSize, pageIndex, ref recordCount, chaXun);
            return items;
        }

        /// <summary>
        /// 设置默认地址，返回1成功，其它失败
        /// </summary>
        /// <param name="gongSiId">公司编号</param>
        /// <param name="diZhiId">地址编号</param>
        /// <returns></returns>
        public int SheZhiMoRen(string gongSiId, string diZhiId)
        {
            int dalRetCode = dal.SheZhiMoRen(gongSiId, diZhiId);
            if (dalRetCode == 1)
            {
                var logInfo = new EyouSoft.Model.MCaoZuoLogInfo();
                logInfo.BiaoTi = "设置默认地址";
                logInfo.NeiRong = "设置默认地址，地址编号：" + diZhiId + "。";
                logInfo.GuanLianId = diZhiId;
                BCaoZuoLog.Log_C(logInfo);
            }
            return dalRetCode;
        }
        #endregion
    }
}
