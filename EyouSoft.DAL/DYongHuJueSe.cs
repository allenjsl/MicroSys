//用户角色相关DAL 汪奇志 2015-04-21
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using EyouSoft.Toolkit.DAL;
using EyouSoft.Toolkit;

namespace EyouSoft.DAL
{
    /// <summary>
    /// 用户角色相关DAL
    /// </summary>
    public class DYongHuJueSe:DALBase
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
        public DYongHuJueSe()
        {
            _db = SystemStore;
        }
        #endregion

        #region private members
        #endregion

        #region public members
        /// <summary>
        /// 是否存在相同的名称
        /// </summary>
        /// <param name="gongSiId">公司编号</param>
        /// <param name="jueSeId">角色编号</param>
        /// <param name="name">角色名称</param>
        /// <returns></returns>
        public bool IsExists(string gongSiId, string jueSeId, string name)
        {
            var cmd = _db.GetSqlStringCommand("SELECT COUNT(*) FROM [tbl_YongHuJueSe] WHERE [Name]=@Name AND [GongSiId]=@GongSiId AND [JueSeId]<>@JueSeId ");
            _db.AddInParameter(cmd, "@JueSeId", DbType.AnsiStringFixedLength, jueSeId);
            _db.AddInParameter(cmd, "@Name", DbType.String,name);
            _db.AddInParameter(cmd, "@GongSiId", DbType.AnsiStringFixedLength, gongSiId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    return rdr.GetInt32(0) > 0;
                }
            }

            return false;
        }

        /// <summary>
        /// 用户角色添加，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int JueSe_C(EyouSoft.Model.MYongHuJueSeInfo info)
        {
            var cmd = _db.GetSqlStringCommand("INSERT INTO [tbl_YongHuJueSe]([JueSeId],[Name],[GongSiId],[MiaoShu],[Status],[Privs],[CaoZuoRenId],[IssueTime]) VALUES (@JueSeId,@Name,@GongSiId,@MiaoShu,@Status,@Privs,@CaoZuoRenId,@IssueTime)");
            _db.AddInParameter(cmd, "@JueSeId", DbType.AnsiStringFixedLength, info.JueSeId);
            _db.AddInParameter(cmd, "@Name", DbType.String, info.Name);
            _db.AddInParameter(cmd, "@GongSiId", DbType.AnsiStringFixedLength, info.GongSiId);
            _db.AddInParameter(cmd, "@MiaoShu", DbType.String, info.MiaoShu);
            _db.AddInParameter(cmd, "@Status", DbType.Int32, info.Status);
            _db.AddInParameter(cmd, "@Privs", DbType.String, GetSqlIn(info.Privs));
            _db.AddInParameter(cmd, "@CaoZuoRenId", DbType.AnsiStringFixedLength, info.CaoZuoRenId);
            _db.AddInParameter(cmd, "@IssueTime", DbType.DateTime, info.IssueTime);

            DbHelper.ExecuteSql(cmd, _db);

            return 1;
        }

        /// <summary>
        /// 用户角色修改，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int JueSe_U(EyouSoft.Model.MYongHuJueSeInfo info)
        {
            var cmd = _db.GetSqlStringCommand("UPDATE [tbl_YongHuJueSe] SET [Name]=@Name,[MiaoShu]=@MiaoShu,[Status]=@Status,[Privs]=@Privs WHERE [JueSeId]=@JueSeId");
            _db.AddInParameter(cmd, "@JueSeId", DbType.AnsiStringFixedLength, info.JueSeId);
            _db.AddInParameter(cmd, "@Name", DbType.String, info.Name);
            _db.AddInParameter(cmd, "@MiaoShu", DbType.String, info.MiaoShu);
            _db.AddInParameter(cmd, "@Status", DbType.Int32, info.Status);
            _db.AddInParameter(cmd, "@Privs", DbType.String, GetSqlIn(info.Privs));

            DbHelper.ExecuteSql(cmd, _db);

            return 1;
        }

        /// <summary>
        /// 用户角色删除，返回1成功，其它失败
        /// </summary>
        /// <param name="gongSiId">公司编号</param>
        /// <param name="jueSeId">角色编号</param>
        /// <returns></returns>
        public int JueSe_D(string gongSiId, string jueSeId)
        {
            var cmd = _db.GetSqlStringCommand("DELETE FROM [tbl_YongHuJueSe] WHERE [JueSeId]=@JueSeId AND [GongSiId]=@GongSiId");
            _db.AddInParameter(cmd, "@JueSeId", DbType.AnsiStringFixedLength, jueSeId);
            _db.AddInParameter(cmd, "@GongSiId", DbType.AnsiStringFixedLength, gongSiId);
            DbHelper.ExecuteSql(cmd, _db);
            return 1;
        }

        /// <summary>
        /// 获取角色信息业务实体
        /// </summary>
        /// <param name="jueSeId">角色编号</param>
        /// <returns></returns>
        public EyouSoft.Model.MYongHuJueSeInfo GetInfo(string jueSeId)
        {
            EyouSoft.Model.MYongHuJueSeInfo info = null;
            var cmd = _db.GetSqlStringCommand("SELECT * FROM [tbl_YongHuJueSe] WHERE [JueSeId]=@JueSeId");
            _db.AddInParameter(cmd, "@JueSeId", DbType.AnsiStringFixedLength, jueSeId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.MYongHuJueSeInfo();
                    info.CaoZuoRenId = rdr["CaoZuoRenId"].ToString();
                    info.GongSiId = rdr["GongSiId"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.JueSeId = rdr["JueSeId"].ToString();
                    info.MiaoShu = rdr["MiaoShu"].ToString();
                    info.Name = rdr["Name"].ToString();
                    info.Privs = Utils.Split2(rdr["Privs"].ToString(), ",");
                    info.Status = (EyouSoft.Model.YongHuJueSeStatus)rdr.GetInt32(rdr.GetOrdinal("Status"));                   
                }
            }

            return info;
        }

        /// <summary>
        /// 获取角色信息集合
        /// </summary>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.MYongHuJueSeInfo> GetJueSes(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.MYongHuJueSeChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.MYongHuJueSeInfo> items = new List<EyouSoft.Model.MYongHuJueSeInfo>();

            string fields = "*";
            StringBuilder sql = new StringBuilder();
            string tableName = "tbl_YongHuJueSe";
            string orderByString = " IssueTime DESC ";
            string heJiString = "";

            #region sql 
            sql.Append(" 1=1 ");

            if (chaXun != null)
            {
                if (!string.IsNullOrEmpty(chaXun.Name))
                {
                    sql.AppendFormat(" AND Name LIKE '%{0}%' ", chaXun.Name);
                }
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
                    var info = new EyouSoft.Model.MYongHuJueSeInfo();
                    info.CaoZuoRenId = rdr["CaoZuoRenId"].ToString();
                    info.GongSiId = rdr["GongSiId"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.JueSeId = rdr["JueSeId"].ToString();
                    info.MiaoShu = rdr["MiaoShu"].ToString();
                    info.Name = rdr["Name"].ToString();
                    info.Privs = Utils.Split2(rdr["Privs"].ToString(), ",");
                    info.Status = (EyouSoft.Model.YongHuJueSeStatus)rdr.GetInt32(rdr.GetOrdinal("Status"));
                    items.Add(info);
                }
            }

            return items;
        }

        /// <summary>
        /// 设置角色状态
        /// </summary>
        /// <param name="jueSeId">角色编号</param>
        /// <param name="status">角色状态</param>
        /// <returns></returns>
        public int SheZhiStatus(string jueSeId, EyouSoft.Model.YongHuJueSeStatus status)
        {
            var cmd = _db.GetSqlStringCommand("UPDATE tbl_YongHuJueSe SET Status=@Status WHERE JueSeId=@JueSeId");
            _db.AddInParameter(cmd, "@JueSeId", DbType.AnsiStringFixedLength, jueSeId);
            _db.AddInParameter(cmd, "@Status", DbType.Int32, status);

            DbHelper.ExecuteSql(cmd, _db);

            return 1;
        }
        #endregion
    }
}
