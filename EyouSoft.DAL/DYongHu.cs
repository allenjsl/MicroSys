//采购商、供应商、平台用户信息相关DAL 汪奇志 2015-04-21
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
    /// 采购商、供应商、平台用户信息相关DAL
    /// </summary>
    public class DYongHu : DALBase
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
        public DYongHu()
        {
            _db = SystemStore;
        }
        #endregion

        #region private members
        #endregion

        #region public members
        /// <summary>
        /// 采购商、供应商、平台用户信息添加、修改，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int YongHu_CU(EyouSoft.Model.MYongHuInfo info)
        {
            var cmd = _db.GetStoredProcCommand("proc_YongHu_CU");

            _db.AddInParameter(cmd, "@YongHuId", DbType.AnsiStringFixedLength, info.YongHuId);
            _db.AddInParameter(cmd, "@LeiXing", DbType.Int32, info.LeiXing);
            _db.AddInParameter(cmd, "@GongSiId", DbType.AnsiStringFixedLength, info.GongSiId);
            _db.AddInParameter(cmd, "@Username", DbType.String, info.Username);
            _db.AddInParameter(cmd, "@PasswordMD5", DbType.String, info.PasswordMD5);
            _db.AddInParameter(cmd, "@JueSeId", DbType.AnsiStringFixedLength, info.JueSeId);
            _db.AddInParameter(cmd, "@Status", DbType.Int32, info.Status);
            _db.AddInParameter(cmd, "@BuMenName", DbType.String, info.BuMenName);
            _db.AddInParameter(cmd, "@Name", DbType.String, info.Name);
            _db.AddInParameter(cmd, "@ZhaoPianFilepath", DbType.String, info.ZhaoPianFilepath);
            _db.AddInParameter(cmd, "@ZhiWu", DbType.String, info.ZhiWu);
            _db.AddInParameter(cmd, "@XingBie", DbType.Int32, info.XingBie);
            _db.AddInParameter(cmd, "@ChuShengRiQi", DbType.DateTime, info.ChuShengRiQi);
            _db.AddInParameter(cmd, "@ShouJi", DbType.String, info.ShouJi);
            _db.AddInParameter(cmd, "@DianHua", DbType.String, info.DianHua);
            _db.AddInParameter(cmd, "@Fax", DbType.String, info.Fax);
            _db.AddInParameter(cmd, "@Email", DbType.String, info.Email);
            _db.AddInParameter(cmd, "@DiZhi", DbType.String, info.DiZhi);
            _db.AddInParameter(cmd, "@RuZhiRiQi", DbType.DateTime, info.RuZhiRiQi);
            _db.AddInParameter(cmd, "@CaoZuoRenId", DbType.AnsiStringFixedLength, info.CaoZuoRenId);
            _db.AddInParameter(cmd, "@IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddInParameter(cmd, "@Privs", DbType.String, "");
            _db.AddInParameter(cmd, "@LaiYuan", DbType.Int32, info.LaiYuan);
            _db.AddInParameter(cmd, "@ShenHeStatus", DbType.Int32, info.ShenHeStatus);
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
        /// 采购商、供应商、平台用户信息删除，返回1成功，其它失败
        /// </summary>
        /// <param name="gongSiId">采购商、供应商编号</param>
        /// <param name="yongHuId">用户编号</param>
        /// <returns></returns>
        public int YongHu_D(string gongSiId, string yongHuId)
        {
            var cmd = _db.GetStoredProcCommand("proc_YongHu_D");

            _db.AddInParameter(cmd, "@YongHuId", DbType.AnsiStringFixedLength, yongHuId);
            _db.AddInParameter(cmd, "@GongSiId", DbType.AnsiStringFixedLength, gongSiId);
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
        /// 获取采购商、供应商、平台用户信息业务实体
        /// </summary>
        /// <param name="yongHuId">用户编号</param>
        /// <returns></returns>
        public EyouSoft.Model.MYongHuInfo GetInfo(string yongHuId)
        {
            EyouSoft.Model.MYongHuInfo info = null;
            var cmd = _db.GetSqlStringCommand("SELECT A.*,(SELECT A1.Name FROM tbl_GongSi AS A1 WHERE A1.GongSiId=A.GongSiId) AS GongSiName FROM tbl_YongHu AS A WHERE A.YongHuId=@YongHuId");
            _db.AddInParameter(cmd, "YongHuId", DbType.AnsiStringFixedLength, yongHuId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.MYongHuInfo();

                    info.BuMenName = rdr["BuMenName"].ToString();
                    info.CaoZuoRenId = rdr["CaoZuoRenId"].ToString();
                    info.ChuShengRiQi = rdr.GetDateTime(rdr.GetOrdinal("ChuShengRiQi"));
                    info.DianHua = rdr["DianHua"].ToString();
                    info.DiZhi = rdr["DiZhi"].ToString();
                    info.Email = rdr["Email"].ToString();
                    info.Fax = rdr["Fax"].ToString();
                    info.GongSiId = rdr["GongSiId"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.JueSeId = rdr["JueSeId"].ToString();
                    info.LeiXing = (EyouSoft.Model.YongHuLeiXing)rdr.GetInt32(rdr.GetOrdinal("LeiXing"));
                    info.Name = rdr["Name"].ToString();
                    info.PasswordMD5 = rdr["PasswordMD5"].ToString();
                    info.RuZhiRiQi = rdr.GetDateTime(rdr.GetOrdinal("RuZhiRiQi"));
                    info.ShouJi = rdr["ShouJi"].ToString();
                    info.Status = (EyouSoft.Model.YongHuStatus)rdr.GetInt32(rdr.GetOrdinal("Status"));
                    info.Username = rdr["Username"].ToString();
                    info.XingBie = (EyouSoft.Model.XingBie)rdr.GetInt32(rdr.GetOrdinal("XingBie"));
                    info.YongHuId = rdr["YongHuId"].ToString();
                    info.ZhaoPianFilepath = rdr["ZhaoPianFilepath"].ToString();
                    info.ZhiWu = rdr["ZhiWu"].ToString();
                    info.GongSiName = rdr["GongSiName"].ToString();
                }
            }

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
            IList<EyouSoft.Model.MYongHuInfo> items = new List<EyouSoft.Model.MYongHuInfo>();

            string fields = "*,(SELECT G.Name FROM tbl_GongSi G WHERE G.GongSiId=tbl_YongHu.GongSiId) GongSiName";
            StringBuilder sql = new StringBuilder();
            string tableName = "tbl_YongHu";
            string orderByString = " IssueTime DESC ";
            string heJiString = "";

            #region sql 
            sql.Append(" IsDelete='0' ");

            if (chaXun != null)
            {
                if (chaXun.LeiXing.HasValue)
                {
                    sql.AppendFormat(" AND LeiXing={0} ", (int)chaXun.LeiXing.Value);
                }
                if (!string.IsNullOrEmpty(chaXun.Name))
                {
                    sql.AppendFormat(" AND Name LIKE '%{0}%' ", chaXun.Name);
                }
                if (!string.IsNullOrEmpty(chaXun.GongSiId))
                {
                    sql.AppendFormat(" AND GongSiId='{0}' ", chaXun.GongSiId);
                }
                else if (!string.IsNullOrEmpty(chaXun.GongSiName))
                {
                    sql.AppendFormat(" AND EXISTS(SELECT 1 FORM tbl_GongSi AS A1 WHERE A1.GongSiId=tbl_YongHu.GongSiId AND A1.Name LIKE '%{0}%') ", chaXun.GongSiName);
                }
                if (!string.IsNullOrEmpty(chaXun.Username))
                {
                    sql.AppendFormat(" AND Username LIKE '%{0}%' ", chaXun.Username);
                }
                if (chaXun.Status.HasValue)
                {
                    sql.AppendFormat(" AND Status={0} ", (int)chaXun.Status.Value);
                }
                if (!string.IsNullOrEmpty(chaXun.BuMen))
                {
                    sql.AppendFormat(" AND BuMenName LIKE '%{0}%' ", chaXun.BuMen);
                }
                if (chaXun.ShenHeStatus.HasValue)
                {
                    sql.AppendFormat(" AND ShenHeStatus={0} ", (int)chaXun.ShenHeStatus.Value);
                }
            }

            #endregion

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields, sql.ToString(), orderByString, heJiString))
            {
                while (rdr.Read())
                {
                    var info = new EyouSoft.Model.MYongHuInfo();
                    
                    info.BuMenName = rdr["BuMenName"].ToString();
                    info.CaoZuoRenId = rdr["CaoZuoRenId"].ToString();
                    info.ChuShengRiQi = rdr.GetDateTime(rdr.GetOrdinal("ChuShengRiQi"));
                    info.DianHua = rdr["DianHua"].ToString();
                    info.DiZhi = rdr["DiZhi"].ToString();
                    info.Email = rdr["Email"].ToString();
                    info.Fax = rdr["Fax"].ToString();
                    info.GongSiId = rdr["GongSiId"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.JueSeId = rdr["JueSeId"].ToString();
                    info.LeiXing = (EyouSoft.Model.YongHuLeiXing)rdr.GetInt32(rdr.GetOrdinal("LeiXing"));
                    info.Name = rdr["Name"].ToString();
                    info.PasswordMD5 = rdr["PasswordMD5"].ToString();
                    info.RuZhiRiQi = rdr.GetDateTime(rdr.GetOrdinal("RuZhiRiQi"));
                    info.ShouJi = rdr["ShouJi"].ToString();
                    info.Status = (EyouSoft.Model.YongHuStatus)rdr.GetInt32(rdr.GetOrdinal("Status"));
                    info.Username = rdr["Username"].ToString();
                    info.XingBie = (EyouSoft.Model.XingBie)rdr.GetInt32(rdr.GetOrdinal("XingBie"));
                    info.YongHuId = rdr["YongHuId"].ToString();
                    info.ZhaoPianFilepath = rdr["ZhaoPianFilepath"].ToString();
                    info.ZhiWu = rdr["ZhiWu"].ToString();
                    info.GongSiName = rdr["GongSiName"].ToString();

                    items.Add(info);
                }
            }

            return items;
        }

        /// <summary>
        /// 设置用户状态，返回1成功，其它失败
        /// </summary>
        /// <param name="gongSiId">公司编号</param>
        /// <param name="yongHuId">用户编号</param>
        /// <param name="status">用户状态</param>
        /// <returns></returns>
        public int SheZhiStatus(string gongSiId, string yongHuId, EyouSoft.Model.YongHuStatus status)
        {
            var cmd = _db.GetSqlStringCommand("UPDATE tbl_YongHu SET Status=@Status WHERE /*GongSiId=@GongSiId AND*/ YongHuId=@YongHuId");
            _db.AddInParameter(cmd, "Status", DbType.Int32, status);
            _db.AddInParameter(cmd, "GongSiId", DbType.String, gongSiId);
            _db.AddInParameter(cmd, "YongHuId", DbType.String, yongHuId);

            DbHelper.ExecuteSql(cmd, _db);

            return 1;
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
            var items = new List<EyouSoft.Model.MZhuCeYongHuInfo>();

            string fields = "*";
            StringBuilder sql = new StringBuilder();
            string tableName = "view_ZhuCeYongHu";
            string orderByString = " IssueTime DESC ";
            string heJiString = "";

            #region sql
            sql.Append(" 1=1 ");

            if (chaXun != null)
            {
                if (chaXun.LeiXing.HasValue)
                {
                    sql.AppendFormat(" AND LeiXing={0} ", (int)chaXun.LeiXing.Value);
                }
                if (!string.IsNullOrEmpty(chaXun.GongSiName))
                {
                    sql.AppendFormat(" AND GongSiName LIKE '%{0}%' ", chaXun.GongSiName);
                }
                if (!string.IsNullOrEmpty(chaXun.YongHuMing))
                {
                    sql.AppendFormat(" AND YongHuMing LIKE '%{0}%' ", chaXun.YongHuMing);
                }
                if (chaXun.ShenHeStatus.HasValue)
                {
                    sql.AppendFormat(" AND ShenHeStatus={0} ", (int)chaXun.ShenHeStatus.Value);
                }
                if (!string.IsNullOrEmpty(chaXun.YongHuName))
                {
                    sql.AppendFormat(" AND YongHuName LIKE '%{0}%' ", chaXun.YongHuName);
                }
                if (chaXun.LaiYuan.HasValue)
                {
                    sql.AppendFormat(" AND LaiYuan={0} ", (int)chaXun.LaiYuan.Value);
                }
            }

            #endregion

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields, sql.ToString(), orderByString, heJiString))
            {
                while (rdr.Read())
                {
                    var info = new EyouSoft.Model.MZhuCeYongHuInfo();
                    info.FaRenName = rdr["FanRenName"].ToString();
                    info.FuZeRenDianHua = rdr["FuZeRenDianHua"].ToString();
                    info.FuZeRenName = rdr["FuZeRenName"].ToString();
                    info.GongSiId = rdr["GongSiId"].ToString();
                    info.GongSiName = rdr["GongSiName"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.LaiYuan = (EyouSoft.Model.LaiYuan)rdr.GetInt32(rdr.GetOrdinal("LaiYuan"));
                    info.LeiXing = (EyouSoft.Model.GongSiLeiXing)rdr.GetInt32(rdr.GetOrdinal("LeiXing"));
                    info.ShenHeStatus = (EyouSoft.Model.ShenHeStatus)rdr.GetInt32(rdr.GetOrdinal("ShenHeStatus"));
                    info.YongHuId = rdr["YongHuId"].ToString();
                    info.YongHuMing = rdr["YongHuMing"].ToString();

                    items.Add(info);
                }
            }

            return items;
        }

        /// <summary>
        /// 用户注册，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int YongHu_ZhuCe(EyouSoft.Model.MZhuCeYongHuInfo info)
        {
            var cmd = _db.GetStoredProcCommand("proc_YongHu_ZhuCe");
            _db.AddInParameter(cmd, "@GongSiId", DbType.AnsiStringFixedLength, info.GongSiId);
            _db.AddInParameter(cmd, "@YongHuId", DbType.AnsiStringFixedLength, info.YongHuId);
            _db.AddInParameter(cmd, "@GongSiName", DbType.String, info.GongSiName);
            _db.AddInParameter(cmd, "@FaRenName", DbType.String, info.FaRenName);
            _db.AddInParameter(cmd, "@FuZeRenName", DbType.String, info.FuZeRenName);
            _db.AddInParameter(cmd, "@FuZeRenDianHua", DbType.String, info.FuZeRenDianHua);
            _db.AddInParameter(cmd, "@YongHuMing", DbType.String, info.YongHuMing);
            _db.AddInParameter(cmd, "@PasswordMD5", DbType.String, info.PasswordMD5);
            _db.AddInParameter(cmd, "@LeiXing", DbType.Int32, info.LeiXing);
            _db.AddInParameter(cmd, "@ShenHeStatus", DbType.Int32, info.ShenHeStatus);
            _db.AddInParameter(cmd, "@IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddInParameter(cmd, "@LaiYuan", DbType.Int32, info.LaiYuan);
            _db.AddInParameter(cmd, "@DiZhi", DbType.String, info.DiZhi);
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
        /// 用户审核，返回1成功，其它失败
        /// </summary>
        /// <param name="gongSiId">公司编号</param>
        /// <param name="yongHuId">用户编号</param>
        /// <param name="shenHeRenId">审核人编号</param>
        /// <returns></returns>
        public int YongHu_ShenHe(string gongSiId, string yongHuId,string shenHeRenId)
        {
            var cmd = _db.GetStoredProcCommand("proc_YongHu_ShenHe");
            _db.AddInParameter(cmd, "@GongSiId", DbType.AnsiStringFixedLength, gongSiId);
            _db.AddInParameter(cmd, "@YongHuId", DbType.AnsiStringFixedLength, yongHuId);
            _db.AddInParameter(cmd, "@ShenHeStatus", DbType.Int32, EyouSoft.Model.ShenHeStatus.已审核);
            _db.AddInParameter(cmd, "@ShenHeTime", DbType.DateTime, DateTime.Now);
            _db.AddInParameter(cmd, "@ShenHeRenId", DbType.AnsiStringFixedLength, shenHeRenId);
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
        #endregion
    }
}
