//供应商产品信息相关DAL 汪奇志 2015-04-22
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
    /// 供应商产品信息相关DAL 
    /// </summary>
    public class DChanPin : DALBase
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
        public DChanPin()
        {
            _db = SystemStore;
        }
        #endregion

        #region private members
        /// <summary>
        /// create fujian xml
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        string CreateFuJianXml(IList<EyouSoft.Model.MFuJianInfo> items)
        {
            if (items == null || items.Count == 0) return string.Empty;
            StringBuilder s = new StringBuilder();
            s.Append("<root>");
            foreach (var item in items)
            {
                s.AppendFormat("<info LeiXing=\"{0}\" Filepath=\"{1}\" />", item.LeiXing, item.Filepath);
            }
            s.Append("</root>");
            return s.ToString();
        }

        /// <summary>
        /// get chanpin fujians
        /// </summary>
        /// <param name="chanPinId"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.MFuJianInfo> GetChanPinFuJians(string chanPinId)
        {
            IList<EyouSoft.Model.MFuJianInfo> items = new List<EyouSoft.Model.MFuJianInfo>();
            var cmd = _db.GetSqlStringCommand("SELECT * FROM tbl_ChanPinFuJian WHERE ChanPinId=@ChanPinId ORDER BY FuJianId ASC");
            _db.AddInParameter(cmd, "@ChanPinId", DbType.AnsiStringFixedLength, chanPinId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.MFuJianInfo();
                    item.Filepath = rdr["Filepath"].ToString();
                    item.FuJianId = rdr.GetInt32(rdr.GetOrdinal("FuJianId"));
                    item.LeiXing = rdr.GetInt32(rdr.GetOrdinal("LeiXing"));
                    items.Add(item);
                }
            }

            return items;
        }
        #endregion

        #region public members
        /// <summary>
        /// 产品添加、修改，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int ChanPin_CU(EyouSoft.Model.MChanPinInfo info)
        {
            var cmd = _db.GetStoredProcCommand("proc_ChanPin_CU");
            _db.AddInParameter(cmd, "@ChanPinId", DbType.AnsiStringFixedLength, info.ChanPinId);
            _db.AddInParameter(cmd, "@GysId", DbType.AnsiStringFixedLength, info.GysId);
            _db.AddInParameter(cmd, "@Name", DbType.String, info.Name);
            _db.AddInParameter(cmd, "@PinPai", DbType.String, info.PinPai);
            _db.AddInParameter(cmd, "@GuiGe", DbType.String, info.GuiGe);
            _db.AddInParameter(cmd, "@JiLiangDanWei", DbType.String, info.JiLiangDanWei);
            _db.AddInParameter(cmd, "@JiaGe1", DbType.Currency, info.JiaGe1);
            _db.AddInParameter(cmd, "@JieShao", DbType.String, info.JieShao);
            _db.AddInParameter(cmd, "@CaoZuoRenId", DbType.AnsiStringFixedLength, info.CaoZuoRenId);
            _db.AddInParameter(cmd, "@IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddInParameter(cmd, "@FuJianXml", DbType.String, CreateFuJianXml(info.FuJians));
            _db.AddOutParameter(cmd, "@RetCode", DbType.Int32, 4);

            int sqlExceptionCode = 0;

            try
            {
                DbHelper.RunProcedure(cmd, _db);
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                sqlExceptionCode = 0 - e.Number;
            }

            if (sqlExceptionCode < 0) return sqlExceptionCode;

            return Convert.ToInt32(_db.GetParameterValue(cmd, "@RetCode"));
        }

        /// <summary>
        /// 产品删除，返回1成功，其它失败
        /// </summary>
        /// <param name="gysId">供应商编号</param>
        /// <param name="chanPinId">产品编号</param>
        /// <returns></returns>
        public int ChanPin_D(string gysId, string chanPinId)
        {
            var cmd = _db.GetStoredProcCommand("proc_ChanPin_D");
            _db.AddInParameter(cmd, "@ChanPinId", DbType.AnsiStringFixedLength, chanPinId);
            _db.AddInParameter(cmd, "@GysId", DbType.AnsiStringFixedLength,gysId);
            _db.AddOutParameter(cmd, "@RetCode", DbType.Int32, 4);

            int sqlExceptionCode = 0;

            try
            {
                DbHelper.RunProcedure(cmd, _db);
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                sqlExceptionCode = 0 - e.Number;
            }

            if (sqlExceptionCode < 0) return sqlExceptionCode;

            return Convert.ToInt32(_db.GetParameterValue(cmd, "@RetCode"));
        }

        /// <summary>
        /// 获取产品信息业务实体
        /// </summary>
        /// <param name="chanPinId">产品编号</param>
        /// <returns></returns>
        public EyouSoft.Model.MChanPinInfo GetInfo(string chanPinId)
        {
            EyouSoft.Model.MChanPinInfo info = null;
            var cmd = _db.GetSqlStringCommand("SELECT A.*,B.Name AS CaoZuoRenName,(SELECT A1.Name FROM tbl_GongSi AS A1 WHERE A1.GongSiId=A.GysId) AS GysName FROM [tbl_ChanPin] AS A INNER JOIN tbl_YongHu AS B ON A.CaoZuoRenId=B.YongHuId WHERE A.ChanPinId=@ChanPinId");
            _db.AddInParameter(cmd, "@ChanPinId", DbType.AnsiStringFixedLength, chanPinId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.MChanPinInfo();

                    info.BianMa = rdr["BianMa"].ToString();
                    info.CaoZuoRenId = rdr["CaoZuoRenId"].ToString();
                    info.ChanPinId = rdr["ChanPinId"].ToString();
                    info.FuJians = null;
                    info.GuiGe = rdr["GuiGe"].ToString();
                    info.GysId = rdr["GysId"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.JiaGe1 = rdr.GetDecimal(rdr.GetOrdinal("JiaGe1"));
                    info.JiaGe2 = rdr.GetDecimal(rdr.GetOrdinal("JiaGe2"));
                    info.JieShao = rdr["JieShao"].ToString();
                    info.JiLiangDanWei = rdr["JiLiangDanWei"].ToString();
                    info.Name = rdr["Name"].ToString();
                    info.PinPai = rdr["PinPai"].ToString();
                    info.GysName = rdr["GysName"].ToString();

                    info.CaoZuoRenName = rdr["CaoZuoRenName"].ToString();
                }
            }

            if (info != null)
            {
                info.FuJians = GetChanPinFuJians(chanPinId);
            }

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
            IList<EyouSoft.Model.MChanPinInfo> items = new List<EyouSoft.Model.MChanPinInfo>();

            string fields = "*,(SELECT A1.Name FROM tbl_YongHu AS A1 WHERE A1.YongHuId=tbl_ChanPin.CaoZuoRenId) AS CaoZuoRenName,(SELECT A1.Name FROM tbl_GongSi AS A1 WHERE A1.GongSiId=tbl_ChanPin.GysId) AS GysName";
            StringBuilder sql = new StringBuilder();
            string tableName = "tbl_ChanPin";
            string orderByString = " IssueTime DESC ";
            string heJiString = "";

            #region sql
            sql.Append(" IsDelete='0' ");

            if (chaXun != null)
            {
                if (!string.IsNullOrEmpty(chaXun.BianMa))
                {
                    sql.AppendFormat(" AND BianMa LIKE '%{0}%' ", chaXun.BianMa);
                }
                if (chaXun.FaBuTime1.HasValue)
                {
                    sql.AppendFormat(" AND IssueTime>'{0}' ", chaXun.FaBuTime1.Value.AddMinutes(-1));
                }
                if (chaXun.FaBuTime2.HasValue)
                {
                    sql.AppendFormat(" AND IssueTime<'{0}' ", chaXun.FaBuTime1.Value.AddDays(1).AddMinutes(-1));
                }
                if (!string.IsNullOrEmpty(chaXun.GysId))
                {
                    sql.AppendFormat(" AND GysId='{0}' ", chaXun.GysId);
                }
                if (!string.IsNullOrEmpty(chaXun.GysName))
                {
                    sql.AppendFormat(" AND EXISTS(SELECT 1 FROM dbo.tbl_GongSi G WHERE G.GongSiId=tbl_ChanPin.GysId AND G.Name LIKE '%{0}%') ", chaXun.GysName);
                }
                if (!string.IsNullOrEmpty(chaXun.Name))
                {
                    sql.AppendFormat(" AND Name LIKE '%{0}%' ", chaXun.Name);
                }

                if (!string.IsNullOrEmpty(chaXun.CgsId))
                {
                    sql.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_GongSiGuanXi AS A1 WHERE A1.GongSiId2=tbl_ChanPin.GysId AND A1.GongSiId1='{0}') ", chaXun.CgsId);
                }
                if (!string.IsNullOrEmpty(chaXun.PinPai))
                {
                    sql.AppendFormat(" AND PinPai LIKE '%{0}%' ", chaXun.PinPai);
                }
                if (!string.IsNullOrEmpty(chaXun.GuiGe))
                {
                    sql.AppendFormat(" AND GuiGe LIKE '%{0}%' ", chaXun.GuiGe);
                }

            }

            #endregion

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields, sql.ToString(), orderByString, heJiString))
            {
                while (rdr.Read())
                {
                    var info = new EyouSoft.Model.MChanPinInfo();

                    info.BianMa = rdr["BianMa"].ToString();
                    info.CaoZuoRenId = rdr["CaoZuoRenId"].ToString();
                    info.ChanPinId = rdr["ChanPinId"].ToString();
                    info.FuJians = null;
                    info.GuiGe = rdr["GuiGe"].ToString();
                    info.GysId = rdr["GysId"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.JiaGe1 = rdr.GetDecimal(rdr.GetOrdinal("JiaGe1"));
                    info.JiaGe2 = rdr.GetDecimal(rdr.GetOrdinal("JiaGe2"));
                    info.JieShao = rdr["JieShao"].ToString();
                    info.JiLiangDanWei = rdr["JiLiangDanWei"].ToString();
                    info.Name = rdr["Name"].ToString();
                    info.PinPai = rdr["PinPai"].ToString();
                    info.CaoZuoRenName = rdr["CaoZuoRenName"].ToString();
                    info.GysName = rdr["GysName"].ToString();

                    items.Add(info);
                }
            }

            return items;
        }

        /// <summary>
        /// 产品价格新增，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int ChanPinJiaGe_C(EyouSoft.Model.MChanPinJiaGeInfo info)
        {
            var cmd = _db.GetStoredProcCommand("proc_ChanPinJiaGe_C");
            _db.AddInParameter(cmd, "@JiaGeId", DbType.AnsiStringFixedLength, info.JiaGeId);
            _db.AddInParameter(cmd, "@ChanPinId", DbType.AnsiStringFixedLength, info.ChanPinId);
            _db.AddInParameter(cmd, "@JiaGe2", DbType.Currency, info.JiaGe2);
            _db.AddInParameter(cmd, "@ShuoMing", DbType.String, info.ShuoMing);
            _db.AddInParameter(cmd, "@CaoZuoRenId", DbType.AnsiStringFixedLength, info.CaoZuoRenId);
            _db.AddInParameter(cmd, "@IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddOutParameter(cmd, "@RetCode", DbType.Int32, 4);

            int sqlExceptionCode = 0;

            try
            {
                DbHelper.RunProcedure(cmd, _db);
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                sqlExceptionCode = 0 - e.Number;
            }

            if (sqlExceptionCode < 0) return sqlExceptionCode;

            return Convert.ToInt32(_db.GetParameterValue(cmd, "@RetCode"));
        }

        /// <summary>
        /// 获取产品价格信息集合
        /// </summary>
        /// <param name="chanPinId">产品编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.MChanPinJiaGeInfo> GetChanPinJiaGes(string chanPinId)
        {
            IList<EyouSoft.Model.MChanPinJiaGeInfo> items = new List<EyouSoft.Model.MChanPinJiaGeInfo>();
            var cmd = _db.GetSqlStringCommand("SELECT A.*,B.Name AS CaoZuoRenName FROM tbl_ChanPinJiaGe AS A INNER JOIN tbl_YongHu AS B ON A.CaoZuoRenId=B.YongHuId WHERE A.ChanPinId=@ChanPinId ORDER BY A.IssueTime DESC");
            _db.AddInParameter(cmd, "ChanPinId", DbType.AnsiStringFixedLength, chanPinId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.MChanPinJiaGeInfo();
                    item.CaoZuoRenId = rdr["CaoZuoRenId"].ToString();
                    item.ChanPinId = rdr["ChanPinId"].ToString();
                    item.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    item.JiaGe1 = rdr.GetDecimal(rdr.GetOrdinal("JiaGe1"));
                    item.JiaGe2 = rdr.GetDecimal(rdr.GetOrdinal("JiaGe2"));
                    item.JiaGeId = rdr["JiaGeId"].ToString();
                    item.ShuoMing = rdr["ShuoMing"].ToString();
                    item.CaoZuoRenName = rdr["CaoZuoRenName"].ToString();

                    items.Add(item);
                }
            }

            return items;
        }

        #endregion
    }
}
