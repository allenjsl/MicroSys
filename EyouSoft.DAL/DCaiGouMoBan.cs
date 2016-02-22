//采购模板相关DAL 汪奇志 2015-04-22
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
    /// 采购模板相关DAL
    /// </summary>
    public class DCaiGouMoBan:DALBase
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
        public DCaiGouMoBan()
        {
            _db = SystemStore;
        }
        #endregion

        #region private members
        /// <summary>
        /// create moban chanpin xml
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        string CreateMoBanChanPinXml(IList<EyouSoft.Model.MCaiGouMoBanChanPinInfo> items)
        {
            if (items == null || items.Count == 0) return string.Empty;

            StringBuilder s = new StringBuilder();
            s.Append("<root>");
            foreach (var item in items)
            {
                s.AppendFormat("<info Id=\"{0}\" ",item.Id);
                s.AppendFormat(" ChanPinId=\"{0}\" ", item.ChanPinId);
                s.AppendFormat(" GysId=\"{0}\" ", item.GysId);
                s.AppendFormat(" ShuLiang=\"{0}\" ", item.ShuLiang);
                s.AppendFormat("/>");
            }
            s.Append("</root>");

            //<root><info><LxrName><![CDATA[{0}]]></LxrName></info></root>

            return s.ToString();
        }

        /// <summary>
        /// get moban chanpins
        /// </summary>
        /// <param name="moBanId"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.MCaiGouMoBanChanPinInfo> GetMoBanChanPins(string moBanId)
        {
            IList<EyouSoft.Model.MCaiGouMoBanChanPinInfo> items = new List<EyouSoft.Model.MCaiGouMoBanChanPinInfo>();
            var cmd = _db.GetSqlStringCommand("SELECT A.*,B.Name AS ChanPinName,C.Name AS GysName,B.JiLiangDanWei,B.GuiGe,B.JiaGe2,B.PinPai  FROM tbl_CaiGouMoBanChanPin AS A INNER JOIN tbl_ChanPin AS B ON A.ChanPinId=B.ChanPinId INNER JOIN tbl_GongSi AS C ON C.GongSiId=A.GysId WHERE A.MoBanId=@MoBanId ORDER BY A.IdentityId ASC");
            _db.AddInParameter(cmd, "MoBanId", DbType.AnsiStringFixedLength, moBanId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.MCaiGouMoBanChanPinInfo();
                    item.ChanPinId = rdr["ChanPinId"].ToString();
                    item.ChanPinName = rdr["ChanPinName"].ToString();
                    item.GysId = rdr["GysId"].ToString();
                    item.GysName = rdr["GysName"].ToString();
                    item.Id = rdr["Id"].ToString();
                    item.ShuLiang = rdr.GetDecimal(rdr.GetOrdinal("ShuLiang"));
                    item.JiLiangDanWei = rdr["JiLiangDanWei"].ToString();
                    item.GuiGe = rdr["GuiGe"].ToString();
                    //item.ChanPinJiaGe = rdr.GetDecimal(rdr.GetOrdinal("JiaGe2"));
                    item.ChanPinPinPai = rdr["PinPai"].ToString();

                    items.Add(item);
                }
            }

            return items;
        }
        #endregion

        #region public members
        /// <summary>
        /// 采购模板添加、修改，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int MoBan_CU(EyouSoft.Model.MCaiGouMoBanInfo info)
        {
            var cmd = _db.GetStoredProcCommand("proc_CaiGouMoBan_CU");
            _db.AddInParameter(cmd, "@MoBanId", DbType.AnsiStringFixedLength, info.MoBanId);
            _db.AddInParameter(cmd, "@CgsId", DbType.AnsiStringFixedLength, info.CgsId);
            _db.AddInParameter(cmd, "@Name", DbType.String, info.Name);
            _db.AddInParameter(cmd, "@CaoZuoRenId", DbType.AnsiStringFixedLength, info.CaoZuoRenId);
            _db.AddInParameter(cmd, "@IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddInParameter(cmd, "@ChanPinXml", DbType.String, CreateMoBanChanPinXml(info.ChanPins));
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
        /// 采购模板删除，返回1成功，其它失败
        /// </summary>
        /// <param name="cgsId">采购商编号</param>
        /// <param name="moBanId">模板编号</param>
        /// <returns></returns>
        public int MoBan_D(string cgsId, string moBanId)
        {
            var cmd = _db.GetStoredProcCommand("proc_CaiGouMoBan_D");
            _db.AddInParameter(cmd, "@MoBanId", DbType.AnsiStringFixedLength, moBanId);
            _db.AddInParameter(cmd, "@CgsId", DbType.AnsiStringFixedLength, cgsId);
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
        /// 获取采购模板信息业务实体
        /// </summary>
        /// <param name="moBanId">采购模板编号</param>
        /// <returns></returns>
        public EyouSoft.Model.MCaiGouMoBanInfo GetInfo(string moBanId)
        {
            EyouSoft.Model.MCaiGouMoBanInfo info = null;

            var cmd = _db.GetSqlStringCommand("SELECT A.*,B.Name AS CaoZuoRenName,C.Name AS CgsName FROM tbl_CaiGouMoBan AS A INNER JOIN tbl_YongHu AS B ON A.CaoZuoRenId=B.YongHuId INNER JOIN tbl_GongSi AS C ON C.GongSiId=A.CgsId WHERE A.MoBanId=@MoBanId");
            _db.AddInParameter(cmd, "@MoBanId", DbType.AnsiStringFixedLength, moBanId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.MCaiGouMoBanInfo();

                    info.CaoZuoRenId = rdr["CaoZuoRenId"].ToString();
                    info.CgsId = rdr["CgsId"].ToString();
                    info.ChanPins = null;
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.MoBanId = rdr["MoBanId"].ToString();
                    info.Name = rdr["Name"].ToString();
                    info.CaoZuoRenName = rdr["CaoZuoRenName"].ToString();
                    info.CgsName = rdr["CgsName"].ToString();
                    info.IsMoRen = rdr["IsMoRen"].ToString() == "1";
                }
            }

            if (info != null)
            {
                info.ChanPins = GetMoBanChanPins(moBanId);
            }

            return info;
        }

        /// <summary>
        /// 获取采购模板信息集合
        /// </summary>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.MCaiGouMoBanInfo> GetMoBans(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.MCaiGouMoBanChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.MCaiGouMoBanInfo> items = new List<EyouSoft.Model.MCaiGouMoBanInfo>();

            string fields = "*,(SELECT A1.Name FROM tbl_YongHu AS A1 WHERE A1.YongHuId=tbl_CaiGouMoBan.CaoZuoRenId) AS CaoZuoRenName,(SELECT A1.Name FROM tbl_GongSi AS A1 WHERE A1.GongSiId=tbl_CaiGouMoBan.CgsId) AS CgsName";
            StringBuilder sql = new StringBuilder();
            string tableName = "tbl_CaiGouMoBan";
            string orderByString = " IssueTime DESC ";
            string heJiString = "";

            #region sql
            sql.Append(" IsDelete='0' ");

            if (chaXun != null)
            {
                if (!string.IsNullOrEmpty(chaXun.CgsId))
                {
                    sql.AppendFormat(" AND (CgsId='{0}') ", chaXun.CgsId);
                }
                if (!string.IsNullOrEmpty(chaXun.Name))
                {
                    sql.AppendFormat(" AND Name LIKE '%{0}%' ", chaXun.Name);
                }
            }

            #endregion

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields, sql.ToString(), orderByString, heJiString))
            {
                while (rdr.Read())
                {
                    var info = new EyouSoft.Model.MCaiGouMoBanInfo();

                    info.CaoZuoRenId = rdr["CaoZuoRenId"].ToString();
                    info.CgsId = rdr["CgsId"].ToString();
                    info.ChanPins = null;
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.MoBanId = rdr["MoBanId"].ToString();
                    info.Name = rdr["Name"].ToString();
                    info.CaoZuoRenName = rdr["CaoZuoRenName"].ToString();
                    info.CgsName = rdr["CgsName"].ToString();
                    info.IsMoRen = rdr["IsMoRen"].ToString() == "1";

                    items.Add(info);
                }
            }

            return items;
        }

        /// <summary>
        /// 设置采购商默认模板，返回1成功，其它失败
        /// </summary>
        /// <param name="cgsId">采购商编号</param>
        /// <param name="moBanId">模板编号</param>
        /// <returns></returns>
        public int SheZhiMoRenMoBan(string cgsId, string moBanId)
        {
            var cmd = _db.GetSqlStringCommand("UPDATE tbl_CaiGouMoBan SET IsMoRen='0' WHERE CgsId=@CgsId AND IsMoRen='1';UPDATE tbl_CaiGouMoBan SET IsMoRen='1' WHERE CgsId=@CgsId AND MoBanId=@MoBanId");
            _db.AddInParameter(cmd, "CgsId", DbType.AnsiStringFixedLength, cgsId);
            _db.AddInParameter(cmd, "MoBanId", DbType.AnsiStringFixedLength, moBanId);

            DbHelper.ExecuteSql(cmd, _db);

            return 1;
        }

        /// <summary>
        /// 获取采购商默认模板信息业务实体
        /// </summary>
        /// <param name="cgsId">采购商编号</param>
        /// <returns></returns>
        public EyouSoft.Model.MCaiGouMoBanInfo GetMoRenMoBanInfo(string cgsId)
        {
            EyouSoft.Model.MCaiGouMoBanInfo info = null;

            var cmd = _db.GetSqlStringCommand("SELECT TOP(1) A.*,B.Name AS CaoZuoRenName,C.Name AS CgsName FROM tbl_CaiGouMoBan AS A INNER JOIN tbl_YongHu AS B ON A.CaoZuoRenId=B.YongHuId INNER JOIN tbl_GongSi AS C ON C.GongSiId=A.CgsId WHERE A.CgsId=@CgsId AND IsMoRen='1'");
            _db.AddInParameter(cmd, "@CgsId", DbType.AnsiStringFixedLength, cgsId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.MCaiGouMoBanInfo();

                    info.CaoZuoRenId = rdr["CaoZuoRenId"].ToString();
                    info.CgsId = rdr["CgsId"].ToString();
                    info.ChanPins = null;
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.MoBanId = rdr["MoBanId"].ToString();
                    info.Name = rdr["Name"].ToString();
                    info.CaoZuoRenName = rdr["CaoZuoRenName"].ToString();
                    info.CgsName = rdr["CgsName"].ToString();
                    info.IsMoRen = rdr["IsMoRen"].ToString() == "1";
                }
            }

            if (info != null)
            {
                info.ChanPins = GetMoBanChanPins(info.MoBanId);
            }

            return info;
        }

        /// <summary>
        /// 获取产品上次报价
        /// </summary>
        /// <param name="gysId">供应商编号</param>
        /// <param name="cgsId">采购商编号</param>
        /// <param name="chanPinId">产品编号</param>
        /// <returns></returns>
        public decimal GetChanPinShangCiJiaGe(string gysId, string cgsId, string chanPinId)
        {
            decimal _jiaGe = 0;
            var cmd = _db.GetSqlStringCommand("SELECT TOP 1 ChanPinJiaGe FROM tbl_DingDanChanPinJiaGe WHERE GysId=@GysId AND CgsId=@CgsId AND ChanPinId=@ChanPinId ORDER BY IdentityId DESC");
            _db.AddInParameter(cmd, "GysId", DbType.AnsiStringFixedLength, gysId);
            _db.AddInParameter(cmd, "CgsId", DbType.AnsiStringFixedLength, cgsId);
            _db.AddInParameter(cmd, "ChanPinId", DbType.AnsiStringFixedLength, chanPinId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    _jiaGe = rdr.GetDecimal(rdr.GetOrdinal("ChanPinJiaGe"));
                }
            }

            return _jiaGe;
        }
        #endregion
    }
}
