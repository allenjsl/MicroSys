//操作日志相关DAL 汪奇志 2015-04-24
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
    /// 操作日志相关DAL
    /// </summary>
    public class DCaoZuoLog:DALBase
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
        public DCaoZuoLog()
        {
            _db = SystemStore;
        }
        #endregion

        #region private members
        #endregion

        #region public members
        /// <summary>
        /// 操作日志添加，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int Log_C(EyouSoft.Model.MCaoZuoLogInfo info)
        {
            var cmd = _db.GetSqlStringCommand("INSERT INTO [tbl_CaoZuoLog]([LogId],[GongSiId],[CaoZuoRenId],[LeiXing],[BiaoTi],[NeiRong],[IssueTime],[IP],[GuanLianId]) VALUES(@LogId,@GongSiId,@CaoZuoRenId,@LeiXing,@BiaoTi,@NeiRong,@IssueTime,@IP,@GuanLianId)");

            _db.AddInParameter(cmd, "@LogId", DbType.AnsiStringFixedLength, info.LogId);
            _db.AddInParameter(cmd, "@GongSiId", DbType.AnsiStringFixedLength, info.GongSiId);
            _db.AddInParameter(cmd, "@CaoZuoRenId", DbType.AnsiStringFixedLength, info.CaoZuoRenId);
            _db.AddInParameter(cmd, "@LeiXing", DbType.Int32, info.LeiXing);
            _db.AddInParameter(cmd, "@BiaoTi", DbType.String, info.BiaoTi);
            _db.AddInParameter(cmd, "@NeiRong", DbType.String, info.NeiRong);
            _db.AddInParameter(cmd, "@IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddInParameter(cmd, "@IP", DbType.String, info.IP);
            _db.AddInParameter(cmd, "@GuanLianId", DbType.AnsiStringFixedLength, info.GuanLianId);


            DbHelper.ExecuteSql(cmd, _db);
            return 1;
        }
        
        /// <summary>
        /// 获取操作日志信息集合
        /// </summary>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.MCaoZuoLogInfo> GetLogs(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.MCaoZuoLogChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.MCaoZuoLogInfo> items = new List<EyouSoft.Model.MCaoZuoLogInfo>();

            string fields = "*,(SELECT A1.Name FROM tbl_YongHu AS A1 WHERE A1.YongHuId=tbl_CaoZuoLog.CaoZuoRenId) AS CaoZuoRenName";
            StringBuilder sql = new StringBuilder();
            string tableName = "tbl_CaoZuoLog";
            string orderByString = " IssueTime DESC ";
            string heJiString = "";

            #region sql
            sql.Append(" 1=1 ");

            if (chaXun != null)
            {
                if (!string.IsNullOrEmpty(chaXun.GongSiId))
                {
                    sql.AppendFormat(" AND GongSiId='{0}' ", chaXun.GongSiId);
                }
            }

            #endregion

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields, sql.ToString(), orderByString, heJiString))
            {
                while (rdr.Read())
                {
                    var info = new EyouSoft.Model.MCaoZuoLogInfo();
                    info.BiaoTi = rdr["BiaoTi"].ToString();
                    info.CaoZuoRenId = rdr["CaoZuoRenId"].ToString();
                    info.CaoZuoRenName = rdr["CaoZuoRenName"].ToString();
                    info.GongSiId = rdr["GongSiId"].ToString();
                    info.GuanLianId = rdr["GuanLianId"].ToString();
                    info.IP = rdr["IP"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.LeiXing = (EyouSoft.Model.CaoZuoLogLeiXing)rdr.GetInt32(rdr.GetOrdinal("LeiXing"));
                    info.LogId = rdr["LogId"].ToString();
                    info.NeiRong = rdr["NeiRong"].ToString();
                    
                    items.Add(info);
                }
            }

            return items;
        }
        #endregion
    }
}
