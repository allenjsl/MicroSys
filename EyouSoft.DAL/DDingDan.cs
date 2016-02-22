//采购订单相关DAL 汪奇志 2015-04-23
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
    using EyouSoft.Model;
    using EyouSoft.Toolkit;

    /// <summary>
    /// 采购订单相关DAL
    /// </summary>
    public class DDingDan : DALBase
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
        public DDingDan()
        {
            _db = SystemStore;
        }
        #endregion

        #region private members
        /// <summary>
        /// get dingdan chanpins
        /// </summary>
        /// <param name="dingDanId"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.MDingDanChanPinInfo> GetDingDanChanPins(string dingDanId)
        {
            IList<EyouSoft.Model.MDingDanChanPinInfo> items = new List<EyouSoft.Model.MDingDanChanPinInfo>();
            var cmd = _db.GetSqlStringCommand("SELECT * FROM tbl_DingDanChanPin WHERE DingDanId=@DingDanId ORDER BY IdentityId ASC");
            _db.AddInParameter(cmd, "DingDanId", DbType.AnsiStringFixedLength, dingDanId);
            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.MDingDanChanPinInfo();
                    item.ChanPinGuiGe = rdr["ChanPinGuiGe"].ToString();
                    item.ChanPinId = rdr["ChanPinId"].ToString();
                    item.ChanPinJiaGe = rdr.GetDecimal(rdr.GetOrdinal("ChanPinJiaGe"));
                    item.ChanPinName = rdr["ChanPinName"].ToString();
                    item.DaoHuoShuLiang = rdr.GetDecimal(rdr.GetOrdinal("DaoHuoShuLiang"));
                    item.CgsDaoHuoShuoMing = rdr["CgsDaoHuoShuoMing"].ToString();
                    item.FaHuoShuLiang = rdr.GetDecimal(rdr.GetOrdinal("FaHuoShuLiang"));
                    item.JiLiangDanWei = rdr["JiLiangDanWei"].ToString();
                    item.JinE = rdr.GetDecimal(rdr.GetOrdinal("JinE"));
                    item.MingXiId = rdr["MingXiId"].ToString();
                    item.ShuLiang = rdr.GetDecimal(rdr.GetOrdinal("ShuLiang"));
                    item.GysBaoJiaShuoMing = rdr["GysBaoJiaShuoMing"].ToString();
                    item.ChanPinJiaGe1 = rdr.GetDecimal(rdr.GetOrdinal("ChanPinJiaGe1"));
                    items.Add(item);
                }
            }
            return items;
        }

        /// <summary>
        /// create fahuo chanpin xml
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        string CreateFaHuoChanPinXml(IList<EyouSoft.Model.MDingDanChanPinInfo> items)
        {
            if (items == null || items.Count == 0) return string.Empty;

            StringBuilder s = new StringBuilder();
            s.AppendFormat("<root>");
            foreach (var item in items)
            {
                s.AppendFormat("<info MingXiId=\"{0}\" FaHuoShuLiang=\"{1}\" />", item.MingXiId, item.FaHuoShuLiang);
            }
            s.AppendFormat("</root>");
            return s.ToString();
        }
        
        /// <summary>
        /// create shouhuo chanpin xml
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        string CreateShouHuoChanPinXml(IList<EyouSoft.Model.MDingDanChanPinInfo> items)
        {
            if (items == null || items.Count == 0) return string.Empty;

            StringBuilder s = new StringBuilder();
            s.AppendFormat("<root>");
            foreach (var item in items)
            {
                s.AppendFormat("<info MingXiId=\"{0}\" DaoHuoShuLiang=\"{1}\">", item.MingXiId, item.DaoHuoShuLiang);
                s.AppendFormat("<CgsDaoHuoShuoMing><![CDATA[{0}]]></CgsDaoHuoShuoMing>", item.CgsDaoHuoShuoMing);
                s.AppendFormat("</info>");
            }
            s.AppendFormat("</root>");
            return s.ToString();
        }

        /// <summary>
        /// create shouhuo chanpin xml
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        string CreateBaoJiaChanPinXml(IList<EyouSoft.Model.MDingDanChanPinInfo> items)
        {
            if (items == null || items.Count == 0) return string.Empty;

            StringBuilder s = new StringBuilder();
            s.AppendFormat("<root>");
            foreach (var item in items)
            {
                s.AppendFormat("<info MingXiId=\"{0}\"", item.MingXiId);
                s.AppendFormat(" ShuLiang=\"{0}\" ", item.ShuLiang);
                s.AppendFormat(" ChanPinJiaGe=\"{0}\" ", item.ChanPinJiaGe);
                s.AppendFormat(" JinE=\"{0}\" ", item.JinE);
                s.AppendFormat(" >");
                s.AppendFormat("<GysBaoJiaShuoMing><![CDATA[{0}]]></GysBaoJiaShuoMing>", item.GysBaoJiaShuoMing);
                s.AppendFormat("</info>");
            }
            s.AppendFormat("</root>");
            return s.ToString();
        }
        #endregion

        #region public members
        /// <summary>
        /// 获取订单信息业务实体
        /// </summary>
        /// <param name="dingDanId">订单编号</param>
        /// <returns></returns>
        public EyouSoft.Model.MDingDanInfo GetInfo(string dingDanId)
        {
            EyouSoft.Model.MDingDanInfo info = null;
            var cmd = _db.GetSqlStringCommand("SELECT * FROM view_DingDan WHERE DingDanId=@DingDanId");
            _db.AddInParameter(cmd, "DingDanId", DbType.AnsiStringFixedLength, dingDanId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.MDingDanInfo();

                    info.CaiGouDanId = rdr["CaiGouDanId"].ToString();
                    info.CgsQueRenRenId = rdr["CgsQueRenRenId"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("CgsQueRenTime"))) info.CgsQueRenTime = rdr.GetDateTime(rdr.GetOrdinal("CgsQueRenTime"));
                    info.CgsShouHuoRenId = rdr["CgsShouHuoRenId"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("CgsShouHuoTime"))) info.CgsShouHuoTime = rdr.GetDateTime(rdr.GetOrdinal("CgsShouHuoTime"));
                    info.ChanPins = null;
                    if (!rdr.IsDBNull(rdr.GetOrdinal("DaoHuoTime"))) info.DaoHuoTime = rdr.GetDateTime(rdr.GetOrdinal("DaoHuoTime"));
                    info.DingDanId = dingDanId;
                    info.GysBaoJiaRenId = rdr["GysBaoJiaRenId"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("GysBaoJiaTime"))) info.GysBaoJiaTime = rdr.GetDateTime(rdr.GetOrdinal("GysBaoJiaTime"));
                    info.GysDaoHuoQueRenRenId = rdr["GysDaoHuoQueRenRenId"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("GysDaoHuoQueRenTime"))) info.GysDaoHuoQueRenTime = rdr.GetDateTime(rdr.GetOrdinal("GysDaoHuoQueRenTime"));
                    info.GysFaHuoRenId = rdr["GysFaHuoRenId"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("GysFaHuoTime"))) info.GysFaHuoTime = rdr.GetDateTime(rdr.GetOrdinal("GysFaHuoTime"));
                    info.GysId = rdr["GysId"].ToString();
                    info.GysName = rdr["GysName"].ToString();
                    info.JinE = rdr.GetDecimal(rdr.GetOrdinal("JinE"));
                    info.SongHuoRenDianHua = rdr["SongHuoRenDianHua"].ToString();
                    info.SongHuoRenName = rdr["SongHuoRenName"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("SongHuoTime"))) info.SongHuoTime = rdr.GetDateTime(rdr.GetOrdinal("SongHuoTime"));
                    info.Status = (EyouSoft.Model.DingDanStatus)rdr.GetInt32(rdr.GetOrdinal("Status"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("FaBuTime"))) info.FaBuTime = rdr.GetDateTime(rdr.GetOrdinal("FaBuTime"));
                    info.XiaDanTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.GysFaHuoShuoMing = rdr["GysFaHuoShuoMing"].ToString();
                    info.CgsName = rdr["CgsName"].ToString();
                    info.CaiGouDanHao = rdr["CaiGouDanHao"].ToString();
                    info.CaiGouDanName = rdr["CaiGouDanName"].ToString();
                    info.MoBanId = rdr["MoBanId"].ToString();
                    info.CaiGouBuMen = rdr["CaiGouBuMen"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("YuJiDaoHuoTime"))) info.YuJiDaoHuoTime = rdr.GetDateTime(rdr.GetOrdinal("YuJiDaoHuoTime"));
                    info.GysDaoHuoQueRenStatus = (QueRenStatus)rdr.GetInt32(rdr.GetOrdinal("GysDaoHuoQueRenStatus"));
                    info.CgsShouHuoRen = rdr["CgsShouHuoRen"].ToString();

                    info.CgsFuKuanStatus = (FuKuanStatus)rdr.GetInt32(rdr.GetOrdinal("CgsFuKuanStatus"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("CgsFuKuanTime"))) info.CgsFuKuanTime = rdr.GetDateTime(rdr.GetOrdinal("CgsFuKuanTime"));
                    info.CgsFuKuanCaoZuoRenId = rdr["CgsFuKuanCaoZuoRenId"].ToString();
                    info.CgsFuKuanCaoZuoRenName = rdr["CgsFuKuanCaoZuoRenName"].ToString();

                    info.CgsYiFuKuanJinE = rdr.GetDecimal(rdr.GetOrdinal("CgsYiFuKuanJinE"));
                    info.CgsId = rdr["CgsId"].ToString();

                    info.GysSongHuoRenId = rdr["GysSongHuoRenId"].ToString();
                }
            }

            if (info != null)
            {
                info.ChanPins = GetDingDanChanPins(dingDanId);
            }

            return info;
        }

        /// <summary>
        /// 获取订单信息集合
        /// </summary>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <param name="heJi">合计信息[0:decimal:采购金额][1:decimal:已付款金额]</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.MDingDanInfo> GetDingDans(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.MDingDanChaXunInfo chaXun,out object[] heJi)
        {
            heJi = new object[] { 0M,0M };
            IList<EyouSoft.Model.MDingDanInfo> items = new List<EyouSoft.Model.MDingDanInfo>();
            string fields = "*";
            StringBuilder sql = new StringBuilder();
            string tableName = "view_DingDan";
            string orderByString = " IssueTime DESC ";
            string heJiString = "SUM(JinE) AS JinEHeJi,SUM(CgsYiFuKuanJinE) AS CgsYiFuKuanJinEHeJi";

            #region sql
            sql.Append(" IsDelete='0' ");
            sql.AppendFormat(" AND Status<>{0} ", (int)EyouSoft.Model.DingDanStatus.计划采购);

            if (chaXun != null)
            {
                if (!string.IsNullOrEmpty(chaXun.CaiGouDanHao))
                {
                    sql.AppendFormat(" AND CaiGouDanHao LIKE '%{0}%' ", chaXun.CaiGouDanHao);
                }
                if (chaXun.CaiGouTime1.HasValue)
                {
                    sql.AppendFormat(" AND IssueTime>'{0}' ", chaXun.CaiGouTime1.Value.AddMinutes(-1));
                }
                if (chaXun.CaiGouTime2.HasValue)
                {
                    sql.AppendFormat(" AND IssueTime<'{0}' ", chaXun.CaiGouTime2.Value.AddDays(1).AddMinutes(-1));
                }
                if (!string.IsNullOrEmpty(chaXun.CgsId))
                {
                    sql.AppendFormat(" AND CgsId='{0}' ", chaXun.CgsId);
                }
                if (chaXun.DingDanStatus.HasValue)
                {
                    sql.AppendFormat(" AND Status={0} ", (int)chaXun.DingDanStatus);
                }
                if (chaXun.DingDanStatusIn!=null&&chaXun.DingDanStatusIn.Count()>0)
                {
                    sql.AppendFormat(" AND Status IN ({0}) ", Utils.GetSqlIdStrByArray(chaXun.DingDanStatusIn));                    
                }
                if (!string.IsNullOrEmpty(chaXun.GysId))
                {
                    sql.AppendFormat(" AND GysId='{0}' ", chaXun.GysId);
                }
                if (!string.IsNullOrEmpty(chaXun.GysName))
                {
                    sql.AppendFormat(" AND GysName LIKE '%{0}%' ", chaXun.GysName);
                }
                if (chaXun.CgsFuKuanStatus.HasValue)
                {
                    sql.AppendFormat(" AND CgsFuKuanStatus={0} ", (int)chaXun.CgsFuKuanStatus);
                }
                if (chaXun.CgsFuKuanTime1.HasValue)
                {
                    sql.AppendFormat(" AND CgsFuKuanTime>'{0}' ", chaXun.CgsFuKuanTime1.Value.AddMinutes(-1));
                }
                if (chaXun.CgsFuKuanTime2.HasValue)
                {
                    sql.AppendFormat(" AND CgsFuKuanTime<'{0}' ", chaXun.CgsFuKuanTime1.Value.AddDays(1).AddMinutes(-1));
                }
                if (chaXun.QueRenStatus.HasValue)
                {
                    sql.AppendFormat(" AND GysDaoHuoQueRenStatus={0} ", (int)chaXun.QueRenStatus);
                }
                if (!string.IsNullOrEmpty(chaXun.CaiGouDanId))
                {
                    sql.AppendFormat(" AND CaiGouDanId='{0}' ", chaXun.CaiGouDanId);
                }
                if (!string.IsNullOrEmpty(chaXun.CgsName))
                {
                    sql.AppendFormat(" AND CgsName LIKE '%{0}%' ", chaXun.CgsName);
                }
             }

            #endregion

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields, sql.ToString(), orderByString, heJiString))
            {
                while (rdr.Read())
                {
                    var info = new EyouSoft.Model.MDingDanInfo();

                    info.CaiGouDanId = rdr["CaiGouDanId"].ToString();
                    info.CgsQueRenRenId = rdr["CgsQueRenRenId"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("CgsQueRenTime"))) info.CgsQueRenTime = rdr.GetDateTime(rdr.GetOrdinal("CgsQueRenTime"));
                    info.CgsShouHuoRenId = rdr["CgsShouHuoRenId"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("CgsShouHuoTime"))) info.CgsShouHuoTime = rdr.GetDateTime(rdr.GetOrdinal("CgsShouHuoTime"));
                    info.ChanPins = null;
                    if (!rdr.IsDBNull(rdr.GetOrdinal("DaoHuoTime"))) info.DaoHuoTime = rdr.GetDateTime(rdr.GetOrdinal("DaoHuoTime"));
                    info.DingDanId = rdr["DingDanId"].ToString();
                    info.GysBaoJiaRenId = rdr["GysBaoJiaRenId"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("GysBaoJiaTime"))) info.GysBaoJiaTime = rdr.GetDateTime(rdr.GetOrdinal("GysBaoJiaTime"));
                    info.GysDaoHuoQueRenRenId = rdr["GysDaoHuoQueRenRenId"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("GysDaoHuoQueRenTime"))) info.GysDaoHuoQueRenTime = rdr.GetDateTime(rdr.GetOrdinal("GysDaoHuoQueRenTime"));
                    info.GysFaHuoRenId = rdr["GysFaHuoRenId"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("GysFaHuoTime"))) info.GysFaHuoTime = rdr.GetDateTime(rdr.GetOrdinal("GysFaHuoTime"));
                    info.GysId = rdr["GysId"].ToString();
                    info.GysName = rdr["GysName"].ToString();
                    info.JinE = rdr.GetDecimal(rdr.GetOrdinal("JinE"));
                    info.SongHuoRenDianHua = rdr["SongHuoRenDianHua"].ToString();
                    info.SongHuoRenName = rdr["SongHuoRenName"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("SongHuoTime"))) info.SongHuoTime = rdr.GetDateTime(rdr.GetOrdinal("SongHuoTime"));
                    info.Status = (EyouSoft.Model.DingDanStatus)rdr.GetInt32(rdr.GetOrdinal("Status"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("FaBuTime"))) info.FaBuTime = rdr.GetDateTime(rdr.GetOrdinal("FaBuTime"));
                    info.XiaDanTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.GysFaHuoShuoMing = rdr["GysFaHuoShuoMing"].ToString();
                    info.CgsName = rdr["CgsName"].ToString();
                    info.CaiGouDanHao = rdr["CaiGOuDanHao"].ToString();
                    info.CaiGouDanName = rdr["CaiGouDanName"].ToString();
                    info.CaiGouBuMen = rdr["CaiGouBuMen"].ToString();
                    info.FaBuRenName = rdr["FaBuRenName"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.YaoQiuDaoHuoTime = Utils.GetDateTimeNullable(rdr["YaoQiuDaoHuoTime"].ToString());
                    info.CgsShouHuoRen = rdr["CgsShouHuoRen"].ToString();
                    info.GysDaoHuoQueRenStatus = (EyouSoft.Model.QueRenStatus)rdr.GetInt32(rdr.GetOrdinal("GysDaoHuoQueRenStatus"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("YuJiDaoHuoTime"))) info.YuJiDaoHuoTime = rdr.GetDateTime(rdr.GetOrdinal("YuJiDaoHuoTime"));
                    info.CaoZuoRenName = rdr["CaoZuoRenName"].ToString();
                    info.CaiGouChanPinXiangShu = rdr.GetInt32(rdr.GetOrdinal("CaiGouChanPinXiangShu"));

                    info.CgsFuKuanStatus = (FuKuanStatus)rdr.GetInt32(rdr.GetOrdinal("CgsFuKuanStatus"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("CgsFuKuanTime"))) info.CgsFuKuanTime = rdr.GetDateTime(rdr.GetOrdinal("CgsFuKuanTime"));
                    info.CgsFuKuanCaoZuoRenId = rdr["CgsFuKuanCaoZuoRenId"].ToString();
                    info.CgsFuKuanCaoZuoRenName = rdr["CgsFuKuanCaoZuoRenName"].ToString();
                    info.CgsYiFuKuanJinE = rdr.GetDecimal(rdr.GetOrdinal("CgsYiFuKuanJinE"));
                    info.CgsId = rdr["CgsId"].ToString();
                    info.CaoZuoRenId = rdr["CaoZuoRenId"].ToString();
                    info.GysLxQQ = rdr["GysLxQQ"].ToString();
                    info.CgsLxQQ = rdr["CgsLxQQ"].ToString();

                    items.Add(info);
                }

                rdr.NextResult();

                if (rdr.Read())
                {
                    if (!rdr.IsDBNull(rdr.GetOrdinal("JinEHeJi"))) heJi[0] = rdr.GetDecimal(rdr.GetOrdinal("JinEHeJi"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("CgsYiFuKuanJinEHeJi"))) heJi[1] = rdr.GetDecimal(rdr.GetOrdinal("CgsYiFuKuanJinEHeJi"));
                }
            }

            return items;
        }

        /// <summary>
        /// 设置订单状态，返回1成功，其它失败
        /// </summary>
        /// <param name="dingDanId">订单编号</param>
        /// <param name="status">订单状态</param>
        /// <param name="caoZuoRenId">操作人编号</param>
        /// <param name="caoZuoTime">操作时间</param>
        /// <returns></returns>
        public int SheZhiStatus(string dingDanId, EyouSoft.Model.DingDanStatus status, string caoZuoRenId, DateTime caoZuoTime)
        {
            var cmd = _db.GetStoredProcCommand("proc_DingDan_SheZhiStatus");
            _db.AddInParameter(cmd, "@DingDanId", DbType.AnsiStringFixedLength, dingDanId);
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

        /// <summary>
        /// 设置订单报价信息业务实体，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int SheZhiBaoJiaInfo(EyouSoft.Model.MDingDanBaoJiaInfo info)
        {
            var cmd = _db.GetStoredProcCommand("proc_DingDan_SheZhiBaoJia");
            _db.AddInParameter(cmd, "@DingDanId", DbType.AnsiStringFixedLength, info.DingDanId);
            _db.AddInParameter(cmd, "@ChanPinXml", DbType.String, CreateBaoJiaChanPinXml(info.ChanPins));
            _db.AddOutParameter(cmd, "@RetCode", DbType.Int32, 4);
            _db.AddInParameter(cmd, "@CaoZuoRenId", DbType.AnsiStringFixedLength, info.CaoZuoRenId);
            _db.AddInParameter(cmd, "@IssueTime", DbType.DateTime, info.IssueTime);

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
        /// 设置订单发货信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int SheZhiFaShuoInfo(EyouSoft.Model.MDingDanFaHuoInfo info)
        {
            var cmd = _db.GetStoredProcCommand("proc_DingDan_SheZhiFaHuo");
            _db.AddInParameter(cmd, "@DingDanId", DbType.AnsiStringFixedLength, info.DingDanId);
            _db.AddInParameter(cmd, "@SongHuoRenName", DbType.String, info.SongHuoRenName);
            _db.AddInParameter(cmd, "@SongHuoRenDianHua", DbType.String, info.SongHuoRenDianHua);
            _db.AddInParameter(cmd, "@SongHuoTime", DbType.DateTime, info.SongHuoTime);
            _db.AddInParameter(cmd, "@ChanPinXml", DbType.String, CreateFaHuoChanPinXml(info.ChanPins));
            _db.AddInParameter(cmd, "@GysFaHuoShuoMing", DbType.String, info.GysFaHuoShuoMing);
            if (info.YuJiDaoHuoTime.HasValue)
            {
                _db.AddInParameter(cmd, "@YuJiDaoHuoTime", DbType.DateTime, info.YuJiDaoHuoTime);
            }
            else
            {
                _db.AddInParameter(cmd, "@YuJiDaoHuoTime", DbType.DateTime, DBNull.Value);
            }
            _db.AddInParameter(cmd, "@GysSongHuoRenId", DbType.AnsiStringFixedLength, info.GysSongHuoRenId);
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
        /// 设置订单收货信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int SheZhiShouHuoInfo(EyouSoft.Model.MDingDanShouHuoInfo info)
        {
            var cmd = _db.GetStoredProcCommand("proc_DingDan_SheZhiShouHuo");
            _db.AddInParameter(cmd, "@DingDanId", DbType.AnsiStringFixedLength, info.DingDanId);
            _db.AddInParameter(cmd, "@ShouHuoTime", DbType.DateTime, info.DaoHuoTime);
            _db.AddInParameter(cmd, "@ShouHuoRen", DbType.String, info.CgsShouHuoRen);
            _db.AddInParameter(cmd, "@ChanPinXml", DbType.String, CreateShouHuoChanPinXml(info.ChanPins));
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
        /// 设置供应商到货确认状态，返回1成功，其它失败
        /// </summary>
        /// <param name="dingDanId">订单编号</param>
        /// <param name="status">到货确认状态</param>
        /// <param name="caoZuoRenId">操作人编号</param>
        /// <param name="caoZuoTime">操作时间</param>
        /// <returns></returns>
        public int SheZhiGysDaoHuoQueRenStatus(string dingDanId, EyouSoft.Model.QueRenStatus status, string caoZuoRenId, DateTime caoZuoTime)
        {
            var cmd = _db.GetSqlStringCommand(" UPDATE tbl_DingDan SET GysDaoHuoQueRenStatus=@GysDaoHuoQueRenStatus,GysDaoHuoQueRenRenId=@GysDaoHuoQueRenRenId,GysDaoHuoQueRenTime=@GysDaoHuoQueRenTime WHERE DingDanId=@DingDanId ");
            _db.AddInParameter(cmd, "@GysDaoHuoQueRenStatus", DbType.Int32, status);
            _db.AddInParameter(cmd, "@GysDaoHuoQueRenRenId", DbType.AnsiStringFixedLength, caoZuoRenId);
            _db.AddInParameter(cmd, "@GysDaoHuoQueRenTime", DbType.DateTime, caoZuoTime);
            _db.AddInParameter(cmd, "@DingDanId", DbType.AnsiStringFixedLength, dingDanId);

            DbHelper.ExecuteSql(cmd, _db);

            return 1;
        }
        #endregion
    }
}
