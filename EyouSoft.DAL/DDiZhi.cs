//地址相关DAL 汪奇志 2015-05-29
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
    /// 地址相关DAL
    /// </summary>
    public class DDiZhi : DALBase
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
        public DDiZhi()
        {
            _db = SystemStore;
        }
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
            var cmd = _db.GetSqlStringCommand("INSERT INTO [tbl_DiZhi]([DiZhiId],[GongSiId],[YongHuId],[Name],[DiZhi],[ShouJi],[DianHua],[CaoZuoRenId],[IssueTime],[IsMoRen],[IsDelete])VALUES(@DiZhiId,@GongSiId,@YongHuId,@Name,@DiZhi,@ShouJi,@DianHua,@CaoZuoRenId,@IssueTime,@IsMoRen,@IsDelete)");
            _db.AddInParameter(cmd, "@DiZhiId", DbType.AnsiStringFixedLength, info.DiZhiId);
            _db.AddInParameter(cmd, "@GongSiId", DbType.AnsiStringFixedLength, info.GongSiId);
            _db.AddInParameter(cmd, "@YongHuId", DbType.AnsiStringFixedLength, info.YongHuId);
            _db.AddInParameter(cmd, "@Name", DbType.String, info.Name);
            _db.AddInParameter(cmd, "@DiZhi", DbType.String, info.DiZhi);
            _db.AddInParameter(cmd, "@ShouJi", DbType.String, info.ShouJi);
            _db.AddInParameter(cmd, "@DianHua", DbType.String, info.DianHua);
            _db.AddInParameter(cmd, "@CaoZuoRenId", DbType.AnsiStringFixedLength, info.CaoZuoRenId);
            _db.AddInParameter(cmd, "@IssueTime", DbType.AnsiStringFixedLength, info.IssueTime);
            _db.AddInParameter(cmd, "@IsMoRen", DbType.AnsiStringFixedLength, info.IsMoRen ? "1" : "0");
            _db.AddInParameter(cmd, "@IsDelete", DbType.AnsiStringFixedLength, "0");
            DbHelper.ExecuteSql(cmd, _db);
            return 1;
        }

        /// <summary>
        /// 地址修改，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int DiZhi_U(EyouSoft.Model.MDiZhiInfo info)
        {
            var cmd = _db.GetSqlStringCommand("UPDATE tbl_DiZhi SET Name=@Name,DiZhi=@DiZhi,ShouJi=@ShouJi,DianHua=@DianHua WHERE DiZhiId=@DiZhiId AND GongSiId=@GongSiId");
            _db.AddInParameter(cmd, "@DiZhiId", DbType.AnsiStringFixedLength, info.DiZhiId);
            _db.AddInParameter(cmd, "@GongSiId", DbType.AnsiStringFixedLength, info.GongSiId);
            _db.AddInParameter(cmd, "@Name", DbType.String, info.Name);
            _db.AddInParameter(cmd, "@DiZhi", DbType.String, info.DiZhi);
            _db.AddInParameter(cmd, "@ShouJi", DbType.String, info.ShouJi);
            _db.AddInParameter(cmd, "@DianHua", DbType.String, info.DianHua);
            DbHelper.ExecuteSql(cmd, _db);
            return 1;
        }

        /// <summary>
        /// 地址删除，返回1成功，其它失败
        /// </summary>
        /// <param name="gongSiId">公司编号</param>
        /// <param name="diZhiId">地址编号</param>
        /// <returns></returns>
        public int DiZhi_D(string gongSiId,string diZhiId)
        {
            var cmd = _db.GetSqlStringCommand("UPDATE tbl_DiZhi SET IsDelete='1' WHERE GongSiId=@GongSiId AND DiZhiId=@DiZhiId");
            _db.AddInParameter(cmd, "@DiZhiId", DbType.AnsiStringFixedLength, diZhiId);
            _db.AddInParameter(cmd, "@GongSiId", DbType.AnsiStringFixedLength, gongSiId);
            DbHelper.ExecuteSql(cmd, _db);
            return 1;
        }

        /// <summary>
        /// 获取地址实体
        /// </summary>
        /// <param name="diZhiId">地址编号</param>
        /// <returns></returns>
        public EyouSoft.Model.MDiZhiInfo GetInfo(string diZhiId)
        {
            EyouSoft.Model.MDiZhiInfo info = null;
            var cmd = _db.GetSqlStringCommand("SELECT * FROM tbl_DiZhi WHERE DiZhiId=@DiZhiId");
            _db.AddInParameter(cmd, "@DiZhiId", DbType.AnsiStringFixedLength, diZhiId);
            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.MDiZhiInfo();
                    info.CaoZuoRenId = rdr["CaoZuoRenId"].ToString();
                    info.DianHua = rdr["DianHua"].ToString();
                    info.DiZhi = rdr["DiZhi"].ToString();
                    info.DiZhiId = rdr["DiZhiId"].ToString();
                    info.GongSiId = rdr["GongSiId"].ToString();
                    info.IsMoRen = rdr["IsMoRen"].ToString() == "1";
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.Name = rdr["Name"].ToString();
                    info.ShouJi = rdr["ShouJi"].ToString();
                    info.YongHuId = rdr["YongHuId"].ToString();
                }
            }

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
            IList<EyouSoft.Model.MDiZhiInfo> items = new List<EyouSoft.Model.MDiZhiInfo>();

            string fields = "*";
            StringBuilder sql = new StringBuilder();
            string tableName = "tbl_DiZhi";
            string orderByString = " IsMoRen DESC,IssueTime DESC ";
            string heJiString = "";

            #region sql
            sql.Append(" IsDelete='0' ");

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
                    var item = new EyouSoft.Model.MDiZhiInfo();
                    item.CaoZuoRenId = rdr["CaoZuoRenId"].ToString();
                    item.DianHua = rdr["DianHua"].ToString();
                    item.DiZhi = rdr["DiZhi"].ToString();
                    item.DiZhiId = rdr["DiZhiId"].ToString();
                    item.GongSiId = rdr["GongSiId"].ToString();
                    item.IsMoRen = rdr["IsMoRen"].ToString() == "1";
                    item.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    item.Name = rdr["Name"].ToString();
                    item.ShouJi = rdr["ShouJi"].ToString();
                    item.YongHuId = rdr["YongHuId"].ToString();

                    items.Add(item);
                }
            }

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
            var cmd = _db.GetSqlStringCommand("UPDATE tbl_DiZhi SET IsMoRen='0' WHERE GongSiId=@GongSiId;UPDATE tbl_DiZhi SET IsMoRen='1' WHERE GongSiId=@GongSiId AND DiZhiId=@DiZhiId ");
            _db.AddInParameter(cmd, "@DiZhiId", DbType.AnsiStringFixedLength, diZhiId);
            _db.AddInParameter(cmd, "@GongSiId", DbType.AnsiStringFixedLength, gongSiId);
            DbHelper.ExecuteSql(cmd, _db);
            return 1;
        }
        #endregion
    }
}
