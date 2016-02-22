//消息相关DAL 汪奇志 2015-04-24
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using EyouSoft.Toolkit.DAL;

namespace EyouSoft.DAL
{
    /// <summary>
    /// 消息相关DAL
    /// </summary>
    public class DXiaoXi : DALBase
    {
        #region static constants
        //static constants
        #endregion

        #region constructor
        /// <summary>
        /// database
        /// </summary>
        Database _db = null;

        /// <summary>
        /// default constructor
        /// </summary>
        public DXiaoXi()
        {
            _db = SystemStore;
        }
        #endregion

        #region private members
        #endregion

        #region public members
        /// <summary>
        /// 添加消息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int XiaoXi_C(EyouSoft.Model.MXiaoXiInfo info)
        {
            var cmd = _db.GetSqlStringCommand("INSERT INTO [tbl_XiaoXi]([XiaoXiId],[LeiXing],[GuanLianId],[BiaoTi],[NeiRong],[FaChuGongSiId],[FaChuRenId],[FaChuTime],[JieShouGongSiId],[JieShouRenId],[Status],[ChuLiRenId],[ChuLiTime]) VALUES(@XiaoXiId,@LeiXing,@GuanLianId,@BiaoTi,@NeiRong,@FaChuGongSiId,@FaChuRenId,@FaChuTime,@JieShouGongSiId,@JieShouRenId,@Status,@ChuLiRenId,@ChuLiTime)");
            _db.AddInParameter(cmd, "@XiaoXiId", DbType.AnsiStringFixedLength, info.XiaoXiId);
            _db.AddInParameter(cmd, "@LeiXing", DbType.Int32, info.LeiXing);
            _db.AddInParameter(cmd, "@GuanLianId", DbType.AnsiStringFixedLength, info.GuanLianId);
            _db.AddInParameter(cmd, "@BiaoTi", DbType.String, info.BiaoTi);
            _db.AddInParameter(cmd, "@NeiRong", DbType.String, info.NeiRong);
            _db.AddInParameter(cmd, "@FaChuGongSiId", DbType.AnsiStringFixedLength, info.FaChuGongSiId);
            _db.AddInParameter(cmd, "@FaChuRenId", DbType.AnsiStringFixedLength, info.FaChuRenId);
            _db.AddInParameter(cmd, "@FaChuTime", DbType.DateTime, info.FaChuTime);
            _db.AddInParameter(cmd, "@JieShouGongSiId", DbType.AnsiStringFixedLength, info.JieShouGongSiId);
            _db.AddInParameter(cmd, "@JieShouRenId", DbType.AnsiStringFixedLength, info.JieShouRenId);
            _db.AddInParameter(cmd, "@Status", DbType.Int32, info.Status);
            _db.AddInParameter(cmd, "@ChuLiRenId", DbType.AnsiStringFixedLength, "");
            _db.AddInParameter(cmd, "@ChuLiTime", DbType.DateTime, DBNull.Value);
            DbHelper.ExecuteSql(cmd, _db);
            return 1;
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
            IList<EyouSoft.Model.MXiaoXiInfo> items = new List<EyouSoft.Model.MXiaoXiInfo>();

            string fields = "*";
            StringBuilder sql = new StringBuilder();
            string tableName = "tbl_XiaoXi";
            string orderByString = " Status ASC,FaChuTime DESC ";
            string heJiString = "";

            #region sql
            sql.Append(" 1=1 ");

            if (chaXun != null)
            {
                if (!string.IsNullOrEmpty(chaXun.FaChuGongSiId))
                {
                    sql.AppendFormat(" AND FaChuGongSiId='{0}' ", chaXun.FaChuGongSiId);
                }
                if (!string.IsNullOrEmpty(chaXun.FaChuRenId))
                {
                    sql.AppendFormat(" AND FaChuRenId='{0}' ", chaXun.FaChuRenId);
                }
                if (!string.IsNullOrEmpty(chaXun.JieShouGongSiId))
                {
                    sql.AppendFormat(" AND JieShouGongSiId='{0}' ", chaXun.JieShouGongSiId);
                }
                if (!string.IsNullOrEmpty(chaXun.JieShouRenId))
                {
                    sql.AppendFormat(" AND JieShouRenId='{0}' ", chaXun.JieShouRenId);
                }
                if (chaXun.LeiXing.HasValue)
                {
                    sql.AppendFormat(" AND LeiXing={0} ", (int)chaXun.LeiXing.Value);
                }
                if (chaXun.Status.HasValue)
                {
                    sql.AppendFormat(" AND Status={0} ", (int)chaXun.Status.Value);
                }
                if (chaXun.LeiXings != null && chaXun.LeiXings.Count > 0)
                {
                    sql.AppendFormat(" AND LeiXing IN({0}) ", GetSqlIn(chaXun.LeiXings));
                }
            }

            #endregion

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields, sql.ToString(), orderByString, heJiString))
            {
                while (rdr.Read())
                {
                    var info = new EyouSoft.Model.MXiaoXiInfo();
                    info.BiaoTi = rdr["BiaoTi"].ToString();
                    info.ChuLiRenId = rdr["ChuLiRenId"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("ChuLiTime"))) info.ChuLiTime = rdr.GetDateTime(rdr.GetOrdinal("ChuLiTime"));
                    info.FaChuGongSiId = rdr["FaChuGongSiId"].ToString();
                    info.FaChuRenId = rdr["FaChuRenId"].ToString();
                    info.FaChuTime = rdr.GetDateTime(rdr.GetOrdinal("FaChuTime"));
                    info.GuanLianId = rdr["GuanLianId"].ToString();
                    info.JieShouGongSiId = rdr["JieShouGongSiId"].ToString();
                    info.JieShouRenId = rdr["JieShouRenId"].ToString();
                    info.LeiXing = (EyouSoft.Model.XiaoXiLeiXing)rdr.GetInt32(rdr.GetOrdinal("LeiXing"));
                    info.NeiRong = rdr["NeiRong"].ToString();
                    info.Status = (EyouSoft.Model.XiaoXiStatus)rdr.GetInt32(rdr.GetOrdinal("Status"));
                    info.XiaoXiId = rdr["XiaoXiId"].ToString();
                    items.Add(info);
                }
            }

            return items;
        }

        /// <summary>
        /// 设置消息状态，返回1成功，其它失败
        /// </summary>
        /// <param name="xiaoXiId">消息编号</param>
        /// <param name="status">状态</param>
        /// <param name="caoZuoRenId">操作人编号</param>
        /// <param name="CaoZuoTime">操作时间</param>
        /// <returns></returns>
        public int SheZhiStatus(string xiaoXiId, EyouSoft.Model.XiaoXiStatus status, string caoZuoRenId, DateTime caoZuoTime)
        {
            var cmd = _db.GetSqlStringCommand("UPDATE tbl_XiaoXi SET ChuLiRenId=@CaoZuoRenId,ChuLiTime=@CaoZuoTime,[Status]=@Status WHERE XiaoXiId=@XiaoXiId");
            _db.AddInParameter(cmd, "@XiaoXiId", DbType.AnsiStringFixedLength, xiaoXiId);
            _db.AddInParameter(cmd, "@Status", DbType.Int32, status);
            _db.AddInParameter(cmd, "@CaoZuoRenId", DbType.AnsiStringFixedLength, caoZuoRenId);
            _db.AddInParameter(cmd, "@CaoZuoTime", DbType.DateTime, caoZuoTime);
            DbHelper.ExecuteSql(cmd, _db);
            return 1;
        }
        #endregion
    }
}
