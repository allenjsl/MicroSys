//采购商、供应商信息相关DAL 汪奇志 2015-04-21
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
    /// 采购商、供应商信息相关DAL
    /// </summary>
    public class DGongSi : DALBase
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
        public DGongSi()
        {
            _db = SystemStore;
        }
        #endregion

        #region private members
        #endregion

        #region public members
        /// <summary>
        /// 采购商、供应商信息添加、修改，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int GongSi_CU(EyouSoft.Model.MGongSiInfo info)
        {
            var cmd = _db.GetStoredProcCommand("proc_GongSi_CU");
            _db.AddInParameter(cmd, "@GongSiId", DbType.AnsiStringFixedLength, info.GongSiId);
            _db.AddInParameter(cmd, "@LeiXing", DbType.Int32, info.LeiXing);
            _db.AddInParameter(cmd, "@Name", DbType.String, info.Name);
            _db.AddInParameter(cmd, "@FanRenName", DbType.String, info.FanRenName);
            _db.AddInParameter(cmd, "@ShengFenId", DbType.Int32, info.ShengFenId);
            _db.AddInParameter(cmd, "@ChengShiId", DbType.Int32, info.ChengShiId);
            _db.AddInParameter(cmd, "@DiZhi", DbType.String, info.DiZhi);
            _db.AddInParameter(cmd, "@YingYeZhiZhaoFilepath", DbType.String, info.YingYeZhiZhaoFilepath);
            _db.AddInParameter(cmd, "@ZuZhiJiGouFilepath", DbType.String, info.ZuZhiJiGouFilepath);
            _db.AddInParameter(cmd, "@FuZeRenName", DbType.String, info.FuZeRenName);
            _db.AddInParameter(cmd, "@FuZeRenDianHua", DbType.String, info.FuZeRenDianHua);
            _db.AddInParameter(cmd, "@FuZeRenShenFenZhengHao", DbType.String, info.FuZeRenShenFenZhengHao);
            _db.AddInParameter(cmd, "@FuZeRenZhaoPianFilepath", DbType.String, info.FuZeRenZhaoPianFilepath);
            _db.AddInParameter(cmd, "@CaiWuName", DbType.String, info.CaiWuName);
            _db.AddInParameter(cmd, "@CaiWuDianHua", DbType.String, info.CaiWuDianHua);
            _db.AddInParameter(cmd, "@CaiWuShenFenZhengHao", DbType.String, info.CaiWuShenFenZhengHao);
            _db.AddInParameter(cmd, "@CaiWuZhaoPianFilepath", DbType.String, info.CaiWuZhaoPianFilepath);
            _db.AddInParameter(cmd, "@CaoZuoRenId", DbType.AnsiStringFixedLength, info.CaoZuoRenId);
            _db.AddInParameter(cmd, "@IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddInParameter(cmd, "@LogoFilepath", DbType.String, info.LogoFilepath);
            _db.AddInParameter(cmd, "@LxQQ", DbType.String, info.LxQQ);
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
        /// 采购商、供应商信息删除，返回1成功，其它失败
        /// </summary>
        /// <param name="gongSiId">采购商、供应商公司编号</param>
        /// <returns></returns>
        public int GongSi_D(string gongSiId)
        {
            var cmd = _db.GetStoredProcCommand("proc_GongSi_D");
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
        /// 获取采购商、供应商信息业务实体
        /// </summary>
        /// <param name="gongSiId">采购商、供应商公司编号</param>
        /// <returns></returns>
        public EyouSoft.Model.MGongSiInfo GetInfo(string gongSiId)
        {
            EyouSoft.Model.MGongSiInfo info = null;
            var cmd = _db.GetSqlStringCommand("SELECT A.*,B.Name AS CaoZuoRenName FROM tbl_GongSi AS A LEFT OUTER JOIN tbl_YongHu AS B ON A.CaoZuoRenId=B.YongHuId WHERE A.GongSiId=@GongSiId");
            _db.AddInParameter(cmd, "@GongSiId", DbType.AnsiStringFixedLength, gongSiId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.MGongSiInfo();

                    info.CaiWuDianHua = rdr["CaiWuDianHua"].ToString();
                    info.CaiWuName = rdr["CaiWuName"].ToString();
                    info.CaiWuShenFenZhengHao = rdr["CaiWuShenFenZhengHao"].ToString();
                    info.CaiWuZhaoPianFilepath = rdr["CaiWuZhaoPianFilepath"].ToString();
                    info.CaoZuoRenId = rdr["CaoZuoRenId"].ToString();
                    info.ChengShiId = rdr.GetInt32(rdr.GetOrdinal("ChengShiId"));
                    info.DiZhi = rdr["DiZhi"].ToString();
                    info.FanRenName = rdr["FanRenName"].ToString();
                    info.FuZeRenDianHua = rdr["FuZeRenDianHua"].ToString();
                    info.FuZeRenName = rdr["FuZeRenName"].ToString();
                    info.FuZeRenShenFenZhengHao = rdr["FuZeRenShenFenZhengHao"].ToString();
                    info.FuZeRenZhaoPianFilepath = rdr["FuZeRenZhaoPianFilepath"].ToString();
                    info.GongSiId = rdr["GongSiId"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.LeiXing = (EyouSoft.Model.GongSiLeiXing)rdr.GetInt32(rdr.GetOrdinal("LeiXing"));
                    info.Name = rdr["Name"].ToString();
                    info.ShengFenId = rdr.GetInt32(rdr.GetOrdinal("ShengFenId"));
                    info.YingYeZhiZhaoFilepath = rdr["YingYeZhiZhaoFilepath"].ToString();
                    info.ZuZhiJiGouFilepath = rdr["ZuZhiJiGouFilepath"].ToString();
                    info.CaoZuoRenName = rdr["CaoZuoRenName"].ToString();
                    info.LogoFilepath = rdr["LogoFilepath"].ToString();
                    info.LxQQ = rdr["LxQQ"].ToString();
                    info.LaiYuan = (EyouSoft.Model.LaiYuan)rdr.GetInt32(rdr.GetOrdinal("LaiYuan"));
                    info.ShenHeStatus = (EyouSoft.Model.ShenHeStatus)rdr.GetInt32(rdr.GetOrdinal("ShenHeStatus"));
                }
            }

            return info;
        }

        /// <summary>
        /// 获取采购商、供应商信息集合
        /// </summary>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <param name="heJi">合计信息</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.MGongSiInfo> GetGongSis(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.MGongSiChaXunInfo chaXun,out object[] heJi)
        {
            heJi = new object[] { };

            IList<EyouSoft.Model.MGongSiInfo> items = new List<EyouSoft.Model.MGongSiInfo>();

            string fields = "*,(SELECT A1.Name FROM tbl_YongHu AS A1 WHERE A1.YongHuId=tbl_GongSi.CaoZuoRenId) AS CaoZuoRenName";
            StringBuilder sql = new StringBuilder();
            string tableName = "tbl_GongSi";
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
                if (!string.IsNullOrEmpty(chaXun.CgsId))
                {
                    sql.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_GongSiGuanXi AS A1 WHERE A1.GongSiId2=tbl_GongSi.GongSiId AND A1.GongSiId1='{0}') ", chaXun.CgsId);
                }
                if (chaXun.LaiYuan.HasValue)
                {
                    sql.AppendFormat(" AND LaiYuan={0} ", (int)chaXun.LaiYuan.Value);
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
                    var info = new EyouSoft.Model.MGongSiInfo();

                    info.CaiWuDianHua = rdr["CaiWuDianHua"].ToString();
                    info.CaiWuName = rdr["CaiWuName"].ToString();
                    info.CaiWuShenFenZhengHao = rdr["CaiWuShenFenZhengHao"].ToString();
                    info.CaiWuZhaoPianFilepath = rdr["CaiWuZhaoPianFilepath"].ToString();
                    info.CaoZuoRenId = rdr["CaoZuoRenId"].ToString();
                    info.ChengShiId = rdr.GetInt32(rdr.GetOrdinal("ChengShiId"));
                    info.DiZhi = rdr["DiZhi"].ToString();
                    info.FanRenName = rdr["FanRenName"].ToString();
                    info.FuZeRenDianHua = rdr["FuZeRenDianHua"].ToString();
                    info.FuZeRenName = rdr["FuZeRenName"].ToString();
                    info.FuZeRenShenFenZhengHao = rdr["FuZeRenShenFenZhengHao"].ToString();
                    info.FuZeRenZhaoPianFilepath = rdr["FuZeRenZhaoPianFilepath"].ToString();
                    info.GongSiId = rdr["GongSiId"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.LeiXing = (EyouSoft.Model.GongSiLeiXing)rdr.GetInt32(rdr.GetOrdinal("LeiXing"));
                    info.Name = rdr["Name"].ToString();
                    info.ShengFenId = rdr.GetInt32(rdr.GetOrdinal("ShengFenId"));
                    info.YingYeZhiZhaoFilepath = rdr["YingYeZhiZhaoFilepath"].ToString();
                    info.ZuZhiJiGouFilepath = rdr["ZuZhiJiGouFilepath"].ToString();
                    info.CaoZuoRenName = rdr["CaoZuoRenName"].ToString();
                    info.LogoFilepath = rdr["LogoFilepath"].ToString();
                    info.LxQQ = rdr["LxQQ"].ToString();
                    info.LaiYuan = (EyouSoft.Model.LaiYuan)rdr.GetInt32(rdr.GetOrdinal("LaiYuan"));
                    info.ShenHeStatus = (EyouSoft.Model.ShenHeStatus)rdr.GetInt32(rdr.GetOrdinal("ShenHeStatus"));

                    items.Add(info);
                }

                /*
                rdr.NextResult();

                if (rdr.Read())
                {
                    if (!rdr.IsDBNull(0)) heJi[0] = rdr.GetDecimal(0);
                }*/
            }

            return items;
        }

        /*/// <summary>
        /// 获取采购商、供应商信息集合
        /// </summary>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.MGongSiInfo> GetGongSis(EyouSoft.Model.MGongSiChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.MGongSiInfo> items = new List<EyouSoft.Model.MGongSiInfo>();

            StringBuilder sql = new StringBuilder();

            #region sql
            sql.Append("SELECT *,(SELECT A1.Name FROM tbl_YongHu AS A1 WHERE A1.YongHuId=tbl_GongSi.CaoZuoRenId) AS CaoZuoRenName FROM tbl_GongSi WHERE IsDelete='0' ");

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
            }

            sql.Append(" ORDER BY IssueTime DESC");
            #endregion

            DbCommand dc = _db.GetSqlStringCommand(sql.ToString());

            using (IDataReader rdr = DbHelper.ExecuteReader(dc,_db))
            {
                while (rdr.Read())
                {
                    var info = new EyouSoft.Model.MGongSiInfo();

                    info.CaiWuDianHua = rdr["CaiWuDianHua"].ToString();
                    info.CaiWuName = rdr["CaiWuName"].ToString();
                    info.CaiWuShenFenZhengHao = rdr["CaiWuShenFenZhengHao"].ToString();
                    info.CaiWuZhaoPianFilepath = rdr["CaiWuZhaoPianFilepath"].ToString();
                    info.CaoZuoRenId = rdr["CaoZuoRenId"].ToString();
                    info.ChengShiId = rdr.GetInt32(rdr.GetOrdinal("ChengShiId"));
                    info.DiZhi = rdr["DiZhi"].ToString();
                    info.FanRenName = rdr["FanRenName"].ToString();
                    info.FuZeRenDianHua = rdr["FuZeRenDianHua"].ToString();
                    info.FuZeRenName = rdr["FuZeRenName"].ToString();
                    info.FuZeRenShenFenZhengHao = rdr["FuZeRenShenFenZhengHao"].ToString();
                    info.FuZeRenZhaoPianFilepath = rdr["FuZeRenZhaoPianFilepath"].ToString();
                    info.GongSiId = rdr["GongSiId"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.LeiXing = (EyouSoft.Model.GongSiLeiXing)rdr.GetInt32(rdr.GetOrdinal("LeiXing"));
                    info.Name = rdr["Name"].ToString();
                    info.ShengFenId = rdr.GetInt32(rdr.GetOrdinal("ShengFenId"));
                    info.YingYeZhiZhaoFilepath = rdr["YingYeZhiZhaoFilepath"].ToString();
                    info.ZuZhiJiGouFilepath = rdr["ZuZhiJiGouFilepath"].ToString();
                    info.CaoZuoRenName = rdr["CaoZuoRenName"].ToString();
                    info.LogoFilepath = rdr["LogoFilepath"].ToString();

                    items.Add(info);
                }
            }

            return items;
        }*/

        /// <summary>
        /// 设置公司关系，返回1成功，其它失败
        /// </summary>
        /// <param name="gongSiId1">公司编号1</param>
        /// <param name="gongSiId2">公司编号2</param>
        /// <param name="caoZuoRenId">操作人编号</param>
        /// <returns></returns>
        public int GuanXi_C(string gongSiId1,string gongSiId2,string caoZuoRenId)
        {
            var cmd = _db.GetSqlStringCommand("INSERT INTO [tbl_GongSiGuanXi]([GongSiId1],[GongSiId2],[CaoZuoRenId],[IssueTime]) VALUES (@GongSiId1,@GongSiId2,@CaoZuoRenId,@IssueTime)");
            _db.AddInParameter(cmd, "@GongSiId1", DbType.AnsiStringFixedLength, gongSiId1);
            _db.AddInParameter(cmd, "@GongSiId2", DbType.AnsiStringFixedLength, gongSiId2);
            _db.AddInParameter(cmd, "@CaoZuoRenId", DbType.AnsiStringFixedLength, caoZuoRenId);
            _db.AddInParameter(cmd, "@IssueTime", DbType.DateTime, DateTime.Now);

            DbHelper.ExecuteSql(cmd, _db);
            return 1;
        }

        /// <summary>
        /// 删除公司关系，返回1成功，其它失败
        /// </summary>
        /// <param name="gongSiId1">公司编号1</param>
        /// <param name="gongSiId2">公司编号2</param>
        /// <returns></returns>
        public int GuanXi_D(string gongSiId1, string gongSiId2)
        {
            var cmd = _db.GetSqlStringCommand("DELETE FROM [tbl_GongSiGuanXi] WHERE GongSiId1=@GongSiId1 AND GongSiId2=@GongSiId2");
            _db.AddInParameter(cmd, "@GongSiId1", DbType.AnsiStringFixedLength, gongSiId1);
            _db.AddInParameter(cmd, "@GongSiId2", DbType.AnsiStringFixedLength, gongSiId2);

            DbHelper.ExecuteSql(cmd, _db);
            return 1;
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
            var items = new List<EyouSoft.Model.MGongSiGuanXiInfo>();

            string fields = "*";
            if (chaXun != null)
            {
                if (!string.IsNullOrEmpty(chaXun.GongSiId))
                {
                    fields += string.Format(",ISNULL((SELECT '1' FROM tbl_GongSiGuanXi AS A1 WHERE A1.GongSiId1='{0}' AND A1.GongSiId2=tbl_GongSi.GongSiId),'0') AS IsGuanZhu", chaXun.GongSiId);
                }
            }

            StringBuilder sql = new StringBuilder();
            string tableName = "tbl_GongSi";
            string orderByString = " IssueTime DESC ";
            string heJiString = "";

            #region sql 
            sql.Append(" IsDelete='0' AND ShenHeStatus=1 ");

            if (chaXun != null)
            {               
                if (!string.IsNullOrEmpty(chaXun.Name))
                {
                    sql.AppendFormat(" AND Name LIKE '%{0}%' ", chaXun.Name);
                }
                if (chaXun.IsGuanZhu.HasValue)
                {
                    sql.AppendFormat(" AND {0} EXISTS(SELECT 1 FROM tbl_GongSiGuanXi AS A1 WHERE A1.GongSiId2=tbl_GongSi.GongSiId AND A1.GongSiId1='{1}') ",chaXun.IsGuanZhu.Value?"":"NOT", chaXun.GongSiId);
                }
                if (chaXun.LeiXing.HasValue)
                {
                    sql.AppendFormat(" AND LeiXing={0} ", (int)chaXun.LeiXing.Value);
                }                
            }

            #endregion

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields, sql.ToString(), orderByString, heJiString))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.MGongSiGuanXiInfo();
                    item.FanRenName = rdr["FanRenName"].ToString();
                    item.GongSiId = rdr["GongSiId"].ToString();
                    item.IsGuanZhu = rdr["IsGuanZhu"].ToString() == "1";
                    item.LeiXing = (EyouSoft.Model.GongSiLeiXing)rdr.GetInt32(rdr.GetOrdinal("LeiXing"));
                    item.Name = rdr["Name"].ToString();
                    item.FuZeRenName = rdr["FuZeRenName"].ToString();
                    item.FuZeRenDianHua = rdr["FuZeRenDianHua"].ToString();
                    
                    items.Add(item);
                }
            }

            return items;
        }
        #endregion
    }
}
