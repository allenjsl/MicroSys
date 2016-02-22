//采购单相关DAL 汪奇志 2015-04-22
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
    /// 采购单相关DAL
    /// </summary>
    public class DCaiGouDan : DALBase
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
        public DCaiGouDan()
        {
            _db = SystemStore;
        }
        #endregion

        #region private members
        /// <summary>
        /// crate dingdan xml
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        string CreateDingDanXml(IList<EyouSoft.Model.MDingDanInfo> items)
        {
            if (items == null || items.Count == 0) return string.Empty;

            StringBuilder s = new StringBuilder();

            s.Append("<root>");
            foreach (var item in items)
            {
                s.Append("<info ");
                s.AppendFormat(" DingDanId=\"{0}\" ",item.DingDanId);
                s.AppendFormat(" GysId=\"{0}\" ",item.GysId);
                s.AppendFormat(" Status=\"{0}\" ",(int)item.Status);
                s.AppendFormat(" JinE=\"{0}\" ", item.JinE);
                s.Append(" /> ");
            }

            s.Append("</root>");

            return s.ToString();
        }

        /// <summary>
        /// create dingdan chanpin xml
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        string CreateDingDanChanPinXml(IList<EyouSoft.Model.MDingDanInfo> items)
        {
            if (items == null || items.Count == 0) return string.Empty;

            StringBuilder s = new StringBuilder();

            s.Append("<root>");
            foreach (var item in items)
            {
                if (item.ChanPins == null || item.ChanPins.Count == 0) continue;

                foreach (var item1 in item.ChanPins)
                {
                    s.Append("<info ");
                    s.AppendFormat(" MingXiId=\"{0}\" ", item1.MingXiId);
                    s.AppendFormat(" DingDanId=\"{0}\" ", item.DingDanId);
                    s.AppendFormat(" ChanPinId=\"{0}\" ", item1.ChanPinId);
                    s.AppendFormat(" ShuLiang=\"{0}\" ", item1.ShuLiang);
                    s.Append(" /> ");
                }
            }

            s.Append("</root>");

            return s.ToString();
        }

        /// <summary>
        /// 获取采购单产品信息集合
        /// </summary>
        /// <param name="caiGouDanId">采购单编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.MCaiGouDanChanPinInfo> GetCaiGouDanChanPins(string caiGouDanId)
        {
            IList<EyouSoft.Model.MCaiGouDanChanPinInfo> items = new List<EyouSoft.Model.MCaiGouDanChanPinInfo>();
            var cmd = _db.GetSqlStringCommand("SELECT A.*,B.GysId,B.GysName FROM tbl_DingDanChanPin AS A INNER JOIN tbl_DingDan AS B ON A.DingDanId=B.DingDanId WHERE A.CaiGouDanId=@CaiGouDanId ORDER BY A.IdentityId ASC");
            _db.AddInParameter(cmd, "CaiGouDanId", DbType.AnsiStringFixedLength, caiGouDanId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.MCaiGouDanChanPinInfo();

                    item.ChanPinGuiGe = rdr["ChanPinGuiGe"].ToString();
                    item.ChanPinId = rdr["ChanPinId"].ToString();
                    item.ChanPinName = rdr["ChanPinName"].ToString();
                    item.DingDanId = rdr["DingDanId"].ToString();
                    item.GysId = rdr["GysId"].ToString();
                    item.GysName = rdr["GysName"].ToString();
                    item.JiLiangDanWei = rdr["JiLiangDanWei"].ToString();
                    item.MingXiId = rdr["MingXiId"].ToString();
                    item.ShuLiang = rdr.GetDecimal(rdr.GetOrdinal("ShuLiang"));
                    item.ChanPinJiaGe = rdr.GetDecimal(rdr.GetOrdinal("ChanPinJiaGe"));
                    item.ChanPinPinPai = rdr["ChanPinPinPai"].ToString();

                    items.Add(item);
                }
            }

            return items;
        }
        #endregion

        #region public members
        /// <summary>
        /// 采购单添加、修改，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <param name="dingDans">订单集合</param>
        /// <returns></returns>
        public int CaiGouDan_CU(EyouSoft.Model.MCaiGouDanInfo info, IList<EyouSoft.Model.MDingDanInfo> dingDans)
        {
            var cmd = _db.GetStoredProcCommand("proc_CaiGouDan_CU");
            _db.AddInParameter(cmd, "@CaiGouDanId", DbType.AnsiStringFixedLength, info.CaiGouDanId);
            _db.AddInParameter(cmd, "@CgsId", DbType.AnsiStringFixedLength, info.CgsId);
            _db.AddInParameter(cmd, "@CaiGouDanName", DbType.String, info.CaiGouDanName);
            _db.AddInParameter(cmd, "@MoBanId", DbType.AnsiStringFixedLength, info.MoBanId);
            _db.AddInParameter(cmd, "@Status", DbType.Int32, info.Status);
            _db.AddInParameter(cmd, "@CaoZuoRenId", DbType.AnsiStringFixedLength, info.CaoZuoRenId);
            _db.AddInParameter(cmd, "@IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddInParameter(cmd, "@ShouHuoDiZhi", DbType.String, info.ShouHuoDiZhi);
            _db.AddInParameter(cmd, "@ShouHuoRenName", DbType.String, info.ShouHuoRenName);
            _db.AddInParameter(cmd, "@ShouHuoRenDianHua", DbType.String, info.ShouHuoRenDianHua);
            _db.AddInParameter(cmd, "@CaiGouBuMen", DbType.String, info.CaiGouBuMen);
            _db.AddInParameter(cmd, "@DingDanXml", DbType.String, CreateDingDanXml(dingDans));
            _db.AddInParameter(cmd, "@ChanPinXml", DbType.String, CreateDingDanChanPinXml(dingDans));
            _db.AddInParameter(cmd, "@CaiGouDanShuoMing", DbType.String, info.CaiGouDanShuoMing);
            _db.AddInParameter(cmd, "@YaoQiuDaoHuoTime", DbType.DateTime, info.YaoQiuDaoHuoTime);
            _db.AddInParameter(cmd, "@ShouHuoDiZhiId", DbType.AnsiStringFixedLength, info.ShouHuoDiZhiId);
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
        /// 采购单删除，返回1成功，其它失败
        /// </summary>
        /// <param name="cgsId">采购商编号</param>
        /// <param name="caiGouDanId">采购单编号</param>
        /// <returns></returns>
        public int CaiGouDan_D(string cgsId, string caiGouDanId)
        {
            var cmd = _db.GetStoredProcCommand("proc_CaiGouDan_D");
            _db.AddInParameter(cmd, "@CaiGouDanId", DbType.AnsiStringFixedLength, caiGouDanId);
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
        /// 获取采购单信息业务实体
        /// </summary>
        /// <param name="caiGouDanId">采购单编号</param>
        /// <returns></returns>
        public EyouSoft.Model.MCaiGouDanInfo GetInfo(string caiGouDanId)
        {
            EyouSoft.Model.MCaiGouDanInfo info = null;
            var cmd = _db.GetSqlStringCommand("SELECT A.*,(SELECT A1.Name FROM tbl_YongHu AS A1 WHERE A1.YongHuId=A.CaoZuoRenId) AS CaoZuoRenName,(SELECT A1.Name FROM tbl_YongHu AS A1 WHERE A1.YongHuId=A.FaBuRenId) AS FaBuRenName FROM tbl_CaiGouDan AS A WHERE A.CaiGouDanId=@CaiGouDanId");
            _db.AddInParameter(cmd, "CaiGouDanId", DbType.AnsiStringFixedLength, caiGouDanId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.MCaiGouDanInfo();

                    info.CaiGouBuMen = rdr["CaiGouBuMen"].ToString();
                    info.CaiGouDanHao = rdr["CaiGouDanHao"].ToString();
                    info.CaiGouDanId = caiGouDanId;
                    info.CaiGouDanName = rdr["CaiGouDanName"].ToString();
                    info.CaoZuoRenId = rdr["CaoZuoRenId"].ToString();
                    info.CgsId = rdr["CgsId"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.MoBanId = rdr["MoBanId"].ToString();
                    info.ShouHuoDiZhi = rdr["ShouHuoDiZhi"].ToString();
                    info.ShouHuoRenDianHua = rdr["ShouHuoRenDianHua"].ToString();
                    info.ShouHuoRenName = rdr["ShouHuoRenName"].ToString();
                    info.Status = (EyouSoft.Model.CaiGouDanStatus)rdr.GetInt32(rdr.GetOrdinal("Status"));

                    info.CaoZuoRenName = rdr["CaoZuoRenName"].ToString();
                    info.FaBuRenId = rdr["FaBuRenId"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("FaBuTime"))) info.FaBuTime = rdr.GetDateTime(rdr.GetOrdinal("FaBuTime"));
                    info.FaBuRenName = rdr["FaBuRenName"].ToString();
                    info.ChanPins = null;
                    info.CaiGouDanShuoMing = rdr["CaiGouDanShuoMing"].ToString();

                    if (!rdr.IsDBNull(rdr.GetOrdinal("YaoQiuDaoHuoTime"))) info.YaoQiuDaoHuoTime = rdr.GetDateTime(rdr.GetOrdinal("YaoQiuDaoHuoTime"));

                    info.ShouHuoDiZhiId = rdr["ShouHuoDiZhiId"].ToString();
                }
            }

            if (info != null)
            {
                info.ChanPins = GetCaiGouDanChanPins(caiGouDanId);
            }

            return info;
        }

        /// <summary>
        /// 获取采购单信息集合
        /// </summary>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.MCaiGouDanInfo> GetCaiGouDans(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.MCaiGouDanChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.MCaiGouDanInfo> items = new List<EyouSoft.Model.MCaiGouDanInfo>();

            string fields = "*,(SELECT A1.Name FROM tbl_YongHu AS A1 WHERE A1.YongHuId=tbl_CaiGouDan.CaoZuoRenId) AS CaoZuoRenName,(SELECT A1.Name FROM tbl_YongHu AS A1 WHERE A1.YongHuId=tbl_CaiGouDan.FaBuRenId) AS FaBuRenName";
            StringBuilder sql = new StringBuilder();
            string tableName = "tbl_CaiGouDan";
            string orderByString = " IssueTime DESC ";
            string heJiString = "";

            #region sql
            sql.Append(" IsDelete='0' ");

            if (chaXun != null)
            {
                if (!string.IsNullOrEmpty(chaXun.CaiGouDanName))
                {
                    sql.AppendFormat(" AND CaiGouDanName LIKE '%{0}%' ", chaXun.CaiGouDanName);
                }
                if (!string.IsNullOrEmpty(chaXun.CaiGouDanHao))
                {
                    sql.AppendFormat(" AND CaiGouDanHao LIKE '%{0}%' ", chaXun.CaiGouDanHao);
                }
                if (chaXun.FaBuTime1.HasValue)
                {
                    sql.AppendFormat(" AND IssueTime>'{0}' ", chaXun.FaBuTime1.Value.AddMinutes(-1));
                }
                if (chaXun.FaBuTime2.HasValue)
                {
                    sql.AppendFormat(" AND IssueTime<'{0}' ", chaXun.FaBuTime2.Value.AddDays(1).AddMinutes(-1));
                }
                if (!string.IsNullOrEmpty(chaXun.CgsId))
                {
                    sql.AppendFormat(" AND CgsId='{0}' ", chaXun.CgsId);                    
                }
            }

            #endregion

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields, sql.ToString(), orderByString, heJiString))
            {
                while (rdr.Read())
                {
                    var info = new EyouSoft.Model.MCaiGouDanInfo();

                    info.CaiGouBuMen = rdr["CaiGouBuMen"].ToString();
                    info.CaiGouDanHao = rdr["CaiGouDanHao"].ToString();
                    info.CaiGouDanId = rdr["CaiGouDanId"].ToString();
                    info.CaiGouDanName = rdr["CaiGouDanName"].ToString();
                    info.CaoZuoRenId = rdr["CaoZuoRenId"].ToString();
                    info.CgsId = rdr["CgsId"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.MoBanId = rdr["MoBanId"].ToString();
                    info.ShouHuoDiZhi = rdr["ShouHuoDiZhi"].ToString();
                    info.ShouHuoRenDianHua = rdr["ShouHuoRenDianHua"].ToString();
                    info.ShouHuoRenName = rdr["ShouHuoRenName"].ToString();
                    info.Status = (EyouSoft.Model.CaiGouDanStatus)rdr.GetInt32(rdr.GetOrdinal("Status"));

                    info.CaoZuoRenName = rdr["CaoZuoRenName"].ToString();
                    info.FaBuRenId = rdr["FaBuRenId"].ToString();
                    if(!rdr.IsDBNull(rdr.GetOrdinal("FaBuTime")))info.FaBuTime = rdr.GetDateTime(rdr.GetOrdinal("FaBuTime"));
                    info.FaBuRenName = rdr["FaBuRenName"].ToString();
                    info.ChanPins = null;
                    info.CaiGouDanShuoMing = rdr["CaiGouDanShuoMing"].ToString();

                    if (!rdr.IsDBNull(rdr.GetOrdinal("YaoQiuDaoHuoTime"))) info.YaoQiuDaoHuoTime = rdr.GetDateTime(rdr.GetOrdinal("YaoQiuDaoHuoTime"));

                    items.Add(info);
                }
            }

            return items;
        }

        /// <summary>
        /// 设置采购单状态，返回1成功，其它失败
        /// </summary>
        /// <param name="cgsId">采购商编号</param>
        /// <param name="caiGouDanId">采购单编号</param>
        /// <param name="status">采购单状态</param>
        /// <param name="caoZuoRenId">操作人编号</param>
        /// <param name="caoZuoTime">操作时间</param>
        /// <returns></returns>
        public int SheZhiStatus(string cgsId, string caiGouDanId, EyouSoft.Model.CaiGouDanStatus status, string caoZuoRenId, DateTime caoZuoTime)
        {
            var cmd = _db.GetStoredProcCommand("proc_CaiGouDan_SheZhiStatus");
            _db.AddInParameter(cmd, "@CgsId", DbType.AnsiStringFixedLength, cgsId);
            _db.AddInParameter(cmd, "@CaiGouDanId", DbType.AnsiStringFixedLength, caiGouDanId);
            _db.AddInParameter(cmd, "@Status", DbType.Int32, status);
            _db.AddInParameter(cmd, "@CaoZuoRenId", DbType.AnsiStringFixedLength, caoZuoRenId);
            _db.AddInParameter(cmd, "@CaoZuoTime", DbType.DateTime, caoZuoTime);
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
