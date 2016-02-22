using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Toolkit.DAL;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using EyouSoft.Toolkit;

namespace EyouSoft.Security.Membership
{
    /// <summary>
    /// 用户登录DAL
    /// </summary>
    internal class DYongHu : DALBase, IYongHu
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
        /// <summary>
        /// read yonghu info
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        EyouSoft.Model.SSO.MYongHuInfo ReadYongHuInfo(DbCommand cmd)
        {
            EyouSoft.Model.SSO.MYongHuInfo info = null;

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.SSO.MYongHuInfo();
                    info.BuMenName = rdr["BuMenName"].ToString();
                    info.DianHua = rdr["DianHua"].ToString();
                    info.Email = rdr["Email"].ToString();
                    info.Fax = rdr["Fax"].ToString();
                    info.GongSiId = rdr["GongSiId"].ToString();
                    info.JueSeId = rdr["JueSeId"].ToString();
                    info.LeiXing = (EyouSoft.Model.YongHuLeiXing)rdr.GetInt32(rdr.GetOrdinal("LeiXing"));
                    info.Name = rdr["Name"].ToString();
                    info.ShouJi = rdr["ShouJi"].ToString();
                    info.Status = (EyouSoft.Model.YongHuStatus)rdr.GetInt32(rdr.GetOrdinal("Status"));
                    info.Username = rdr["Username"].ToString();
                    info.XingBie = (EyouSoft.Model.XingBie)rdr.GetInt32(rdr.GetOrdinal("XingBie"));
                    info.YongHuId = rdr["YongHuId"].ToString();
                    info.ZhaoPianFilepath = rdr["ZhaoPianFilepath"].ToString();
                    info.ShenHeStatus = (EyouSoft.Model.ShenHeStatus)rdr.GetInt32(rdr.GetOrdinal("ShenHeStatus"));
                }
            }

            if (info != null)
            {
                info.Privs = GetJueSePrivs(info.JueSeId);

                if (info.LeiXing != EyouSoft.Model.YongHuLeiXing.平台)
                {
                    var gongSiInfo = GetGongSiInfo(info.GongSiId);

                    info.GS_Name = gongSiInfo[0];
                    info.GS_LogoFilepath = gongSiInfo[1];
                }
            }

            if (info != null && (info.Privs == null || info.Privs.Count == 0))
            {
                info.Privs = new List<int>();
                if (info.LeiXing == EyouSoft.Model.YongHuLeiXing.采购商)
                {
                    info.Privs.Add(102);
                }

                if (info.LeiXing == EyouSoft.Model.YongHuLeiXing.供应商)
                {
                    info.Privs.Add(204);
                }

                if (info.LeiXing == EyouSoft.Model.YongHuLeiXing.平台)
                {
                    info.Privs.Add(303);
                }
            }

            return info;
        }

        /// <summary>
        /// get juese privs
        /// </summary>
        /// <param name="jueSeId"></param>
        /// <returns></returns>
        IList<int> GetJueSePrivs(string jueSeId)
        {
            IList<int> items=new List<int>();
            if (string.IsNullOrEmpty(jueSeId)) return items;

            var cmd = _db.GetSqlStringCommand("SELECT Privs FROM tbl_YongHuJueSe WHERE JueSeId=@JueSeId");
            _db.AddInParameter(cmd, "JueSeId", System.Data.DbType.AnsiStringFixedLength, jueSeId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    items = EyouSoft.Toolkit.Utils.Split2(rdr[0].ToString(), ",");
                }
            }

            return items;
        }

        /// <summary>
        /// 获取公司信息
        /// </summary>
        /// <param name="gongSiId">公司编号</param>
        /// <returns></returns>
        string[] GetGongSiInfo(string gongSiId)
        {
            string[] r = new string[] { "", "" };
            var cmd = _db.GetSqlStringCommand("SELECT * FROM tbl_GongSi WHERE GongSiId=@GongSiId");
            _db.AddInParameter(cmd, "GongSiId", System.Data.DbType.AnsiStringFixedLength, gongSiId);

            using (var rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    r[0] = rdr["Name"].ToString();
                    r[1] = rdr["LogoFilepath"].ToString();
                }
            }

            return r;
        }
        #endregion

        #region IYongHu 成员
        /// <summary>
        /// 用户登录，根据用户名、用户密码获取用户信息
        /// </summary>
        /// <param name="username">登录账号</param>
        /// <param name="pwd">登录密码</param>
        /// <returns></returns>
        public EyouSoft.Model.SSO.MYongHuInfo Login(string username, string pwd_md5)
        {
            var cmd = _db.GetSqlStringCommand("SELECT * FROM tbl_YongHu WHERE Username=@Username AND PasswordMD5=@PasswordMD5 AND IsDelete='0'");
            _db.AddInParameter(cmd, "Username", System.Data.DbType.String, username);
            _db.AddInParameter(cmd, "PasswordMD5", System.Data.DbType.String, pwd_md5);

            var info = ReadYongHuInfo(cmd);
            return info;
        }

        /// <summary>
        /// 用户登录，根据公司编号、用户名、用户编号获取用户信息
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="username">登录账号</param>
        /// <param name="yongHuId">用户编号</param>
        /// <param name="keHuId">客户编号</param>
        /// <returns></returns>
        public EyouSoft.Model.SSO.MYongHuInfo Login(string gongSiId, string username, string yongHuId)
        {
            var cmd = _db.GetSqlStringCommand("SELECT * FROM tbl_YongHu WHERE GongSiId=@GongSiId AND Username=@Username AND YongHuId=@YongHuId AND IsDelete='0'");
            _db.AddInParameter(cmd, "Username", System.Data.DbType.String, username);
            _db.AddInParameter(cmd, "YongHuId", System.Data.DbType.AnsiStringFixedLength, yongHuId);
            _db.AddInParameter(cmd, "GongSiId", System.Data.DbType.AnsiStringFixedLength, gongSiId);

            var info = ReadYongHuInfo(cmd);
            return info;
        }

        /// <summary>
        /// 写登录日志
        /// </summary>
        /// <param name="info">登录用户信息</param>
        /// <param name="leiXing">登录类型</param>
        public void LoginLogwr(EyouSoft.Model.SSO.MYongHuInfo info, EyouSoft.Model.LoginLeiXing leiXing)
        {
            var cmd = _db.GetSqlStringCommand("INSERT INTO [tbl_YongHuLoginLog]([LogId],[YongHuId],[Time],[GongSiId],[IP],[JSON],[LeiXing]) VALUES(@LogId,@YongHuId,@Time,@GongSiId,@IP,@JSON,@LeiXing)");
            _db.AddInParameter(cmd, "LogId", System.Data.DbType.AnsiStringFixedLength, Guid.NewGuid().ToString());
            _db.AddInParameter(cmd, "YongHuId", System.Data.DbType.AnsiStringFixedLength, info.YongHuId);
            _db.AddInParameter(cmd, "Time", System.Data.DbType.DateTime, DateTime.Now);
            _db.AddInParameter(cmd, "GongSiId", System.Data.DbType.AnsiStringFixedLength, info.GongSiId);
            _db.AddInParameter(cmd, "IP", System.Data.DbType.String, Utils.GetRemoteIP());
            _db.AddInParameter(cmd, "JSON", System.Data.DbType.String, new EyouSoft.Toolkit.BrowserInfo().ToJsonString());
            _db.AddInParameter(cmd, "LeiXing", System.Data.DbType.Int32, leiXing);

            DbHelper.ExecuteSql(cmd, _db);
        }
        #endregion
    }
}
