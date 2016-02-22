//用户角色相关BLL 汪奇志 2015-04-21
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL
{
    /// <summary>
    /// 用户角色相关BLL
    /// </summary>
    public class BYongHuJueSe
    {
        private readonly EyouSoft.DAL.DYongHuJueSe dal = new EyouSoft.DAL.DYongHuJueSe();

        #region constructure
        /// <summary>
        /// default constructor
        /// </summary>
         public BYongHuJueSe() { }
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
             if (string.IsNullOrEmpty(gongSiId) || string.IsNullOrEmpty(name)) return true;
             return dal.IsExists(gongSiId, jueSeId, name);
         }        

         /// <summary>
         /// 用户角色添加，返回1成功，其它失败
         /// </summary>
         /// <param name="info">实体</param>
         /// <returns></returns>
         public int JueSe_C(EyouSoft.Model.MYongHuJueSeInfo info)
         {
             if (info == null
                 || string.IsNullOrEmpty(info.GongSiId)
                 || string.IsNullOrEmpty(info.CaoZuoRenId)
                 || string.IsNullOrEmpty(info.Name)) return 0;
             info.JueSeId = Guid.NewGuid().ToString();
             info.IssueTime = DateTime.Now;
             int dalRetCode = dal.JueSe_C(info);
             if (dalRetCode == 1)
             {
                 var logInfo = new EyouSoft.Model.MCaoZuoLogInfo();
                 logInfo.BiaoTi = "添加用户角色";
                 logInfo.NeiRong = "添加用户角色，用户编号：" + info.JueSeId + "。";
                 logInfo.GuanLianId = info.JueSeId;
                 BCaoZuoLog.Log_C(logInfo);
             }
             return dalRetCode;
         }

         /// <summary>
         /// 用户角色修改，返回1成功，其它失败
         /// </summary>
         /// <param name="info">实体</param>
         /// <returns></returns>
         public int JueSe_U(EyouSoft.Model.MYongHuJueSeInfo info)
         {
             if (info == null 
                 || string.IsNullOrEmpty(info.GongSiId) 
                 || string.IsNullOrEmpty(info.CaoZuoRenId) 
                 || string.IsNullOrEmpty(info.Name) 
                 || string.IsNullOrEmpty(info.JueSeId)) return 0;
             info.IssueTime = DateTime.Now;
             int dalRetCode = dal.JueSe_U(info);
             if (dalRetCode == 1)
             {
                 var logInfo = new EyouSoft.Model.MCaoZuoLogInfo();
                 logInfo.BiaoTi = "修改用户角色";
                 logInfo.NeiRong = "修改用户角色，角色编号：" + info.JueSeId + "。";
                 logInfo.GuanLianId = info.JueSeId;
                 BCaoZuoLog.Log_C(logInfo);
             }
             return dalRetCode;
         }

         /// <summary>
         /// 用户角色删除，返回1成功，其它失败
         /// </summary>
         /// <param name="gongSiId">公司编号</param>
         /// <param name="jueSeId">角色编号</param>
         /// <returns></returns>
         public int JueSe_D(string gongSiId, string jueSeId)
         {
             if (string.IsNullOrEmpty(gongSiId) || string.IsNullOrEmpty(jueSeId)) return 0;
             int dalRetCode = dal.JueSe_D(gongSiId, jueSeId);
             if (dalRetCode == 1)
             {
                 var logInfo = new EyouSoft.Model.MCaoZuoLogInfo();
                 logInfo.BiaoTi = "删除用户角色";
                 logInfo.NeiRong = "删除用户角色，角色编号：" + jueSeId + "。";
                 logInfo.GuanLianId = jueSeId;
                 BCaoZuoLog.Log_C(logInfo);
             }
             return dalRetCode;
         }

         /// <summary>
         /// 获取角色信息业务实体
         /// </summary>
         /// <param name="jueSeId">角色编号</param>
         /// <returns></returns>
         public EyouSoft.Model.MYongHuJueSeInfo GetInfo(string jueSeId)
         {
             if (string.IsNullOrEmpty(jueSeId)) return null;
             var info = dal.GetInfo(jueSeId);
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
             if (pageSize <= 0) pageSize = 1;
             if (pageIndex <= 0) pageIndex = 1;

             var items = dal.GetJueSes(pageSize, pageIndex, ref recordCount, chaXun);
             return items;
         }

         /// <summary>
         /// 获取角色信息集合
         /// </summary>
         /// <param name="chaXun">查询</param>
         /// <returns></returns>
         public IList<EyouSoft.Model.MYongHuJueSeInfo> GetJueSes(EyouSoft.Model.MYongHuJueSeChaXunInfo chaXun)
         {
             int recordCount = 0;
             return GetJueSes(1000, 1, ref recordCount, chaXun);
         }

         /// <summary>
         /// 设置角色状态
         /// </summary>
         /// <param name="jueSeId">角色编号</param>
         /// <param name="status">角色状态</param>
         /// <returns></returns>
         public int SheZhiStatus(string jueSeId, EyouSoft.Model.YongHuJueSeStatus status)
         {
             if (string.IsNullOrEmpty(jueSeId)) return 0;
             int dalRetCode = dal.SheZhiStatus(jueSeId, status);
             if (dalRetCode == 1)
             {
                 var logInfo = new EyouSoft.Model.MCaoZuoLogInfo();
                 logInfo.BiaoTi = "设置用户角色状态";
                 logInfo.NeiRong = "设置用户角色状态，角色编号：" + jueSeId + "，角色状态：" + status + "。";
                 logInfo.GuanLianId = jueSeId;
                 BCaoZuoLog.Log_C(logInfo);
             }
             return dalRetCode;
         }
        #endregion
    }
}
