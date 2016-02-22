//财务相关DAL 汪奇志 2015-04-23
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
    /// 财务相关DAL
    /// </summary>
    public class DFin:DALBase
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
        public DFin()
        {
            _db = SystemStore;
        }
        #endregion

        #region private members
        #endregion

        #region public members
        /// <summary>
        /// 设置采购商付款状态，返回1成功，其它失败
        /// </summary>
        /// <param name="cgsId">采购商编号</param>
        /// <param name="dingDanId">订单编号</param>
        /// <param name="status">付款状态</param>
        /// <param name="caoZuoRenId">操作人编号</param>
        /// <param name="caoZuoTime">操作时间</param>
        /// <returns></returns>
        public int SheZhiCgsFuKuanStatus(string cgsId, string dingDanId, EyouSoft.Model.FuKuanStatus status, string caoZuoRenId, DateTime caoZuoTime)
        {
            var cmd = _db.GetSqlStringCommand("UPDATE tbl_DingDan SET CgsFuKuanStatus=@CgsFuKuanStatus,CgsFuKuanCaoZuoRenId=@CgsFuKuanCaoZuoRenId,CgsFuKuanTime=@CgsFuKuanTime,CgsYiFuKuanJinE=JinE WHERE DingDanId=@DingDanId AND EXISTS(SELECT 1 FROM tbl_CaiGouDan AS A1 WHERE A1.CaiGouDanId=tbl_DingDan.CaiGouDanId AND A1.CgsId=@CgsId)");
            _db.AddInParameter(cmd, "@CgsFuKuanStatus", DbType.Int32, status);
            _db.AddInParameter(cmd, "@CgsFuKuanCaoZuoRenId", DbType.String, caoZuoRenId);
            _db.AddInParameter(cmd, "@CgsFuKuanTime", DbType.DateTime, caoZuoTime);
            _db.AddInParameter(cmd, "@DingDanId", DbType.AnsiStringFixedLength, dingDanId);
            _db.AddInParameter(cmd, "@CgsId", DbType.AnsiStringFixedLength, cgsId);

            DbHelper.ExecuteSql(cmd, _db);

            return 1;
        }
        #endregion
    }
}
