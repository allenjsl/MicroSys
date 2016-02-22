//消息相关BLL 汪奇志 2015-04-24
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL
{
    /// <summary>
    /// 消息相关BLL
    /// </summary>
    public class BXiaoXi
    {
        private readonly EyouSoft.DAL.DXiaoXi dal = new EyouSoft.DAL.DXiaoXi();

        #region constructure
        /// <summary>
        /// default constructor
        /// </summary>
        public BXiaoXi() { }
        #endregion

        #region private members
        #endregion

        #region public members
        /// <summary>
        /// 添加消息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        internal static int XiaoXi_C(EyouSoft.Model.MXiaoXiInfo info)
        {
            if (info == null || string.IsNullOrEmpty(info.GuanLianId) || string.IsNullOrEmpty(info.FaChuGongSiId) || string.IsNullOrEmpty(info.JieShouGongSiId)) return 0;

            info.XiaoXiId = Guid.NewGuid().ToString();
            info.FaChuTime = DateTime.Now;
            info.Status = EyouSoft.Model.XiaoXiStatus.未读;

            int dalRetCode = new EyouSoft.DAL.DXiaoXi().XiaoXi_C(info);
            return dalRetCode;
        }

        /// <summary>
        /// 获取消息集合
        /// </summary>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.MXiaoXiInfo> GetXiaoXis(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.MXiaoXiChaXunInfo chaXun)
        {
            if (pageSize <= 0) pageSize = 1;
            if (pageIndex <= 0) pageIndex = 1;

            var items = dal.GetXiaoXis(pageSize, pageIndex, ref recordCount, chaXun);
            return items;
        }

        /*/// <summary>
        /// 获取消息集合
        /// </summary>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.MXiaoXiInfo> GetXiaoXis(EyouSoft.Model.MXiaoXiChaXunInfo chaXun)
        {
            int pageSize = 2000;
            int recordCount = 0;

            return GetXiaoXis(pageSize, 1, ref recordCount, chaXun);
        }*/

        /// <summary>
        /// 获取消息数
        /// </summary>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public int GetXiaoXiShu(EyouSoft.Model.MXiaoXiChaXunInfo chaXun)
        {
            int recordCount = 0;

            var items = GetXiaoXis(1, 1, ref recordCount, chaXun);

            return recordCount;
        }

        /// <summary>
        /// 标记消息为已读，返回1成功，其它失败
        /// </summary>
        /// <param name="xiaoXiId">消息编号</param>
        /// <param name="caoZuoRenId">操作人编号</param>
        /// <returns></returns>
        public int SheZhiYiDu(string xiaoXiId, string caoZuoRenId)
        {
            if (string.IsNullOrEmpty(xiaoXiId) || string.IsNullOrEmpty(caoZuoRenId)) return 0;
            int dalRetCode = dal.SheZhiStatus(xiaoXiId, EyouSoft.Model.XiaoXiStatus.已读, caoZuoRenId, DateTime.Now);
            return dalRetCode;
        }
        #endregion
    }
}
