using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model
{
    #region 用户角色信息业务实体
    /// <summary>
    /// 用户角色信息业务实体
    /// </summary>
    public class MYongHuJueSeInfo
    {
        string _GongSiId = string.Empty;
        /// <summary>
        /// 角色编号
        /// </summary>
        public string JueSeId { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 采购商、供应商编号
        /// </summary>
        public string GongSiId { get { return _GongSiId; } set { _GongSiId = value; } }
        /// <summary>
        /// 角色描述
        /// </summary>
        public string MiaoShu { get; set; }
        /// <summary>
        /// 角色状态
        /// </summary>
        public YongHuJueSeStatus Status { get; set; }
        /// <summary>
        /// 权限
        /// </summary>
        public IList<int> Privs { get; set; }
        /// <summary>
        /// 操作人编号
        /// </summary>
        public string CaoZuoRenId { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
    }
    #endregion

    #region 用户角色信息查询业务实体
    /// <summary>
    /// 用户角色信息查询业务实体
    /// </summary>
    public class MYongHuJueSeChaXunInfo
    {
        /// <summary>
        /// 采购商、供应商编号
        /// </summary>
        public string GongSiId { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }
    }
    #endregion
}
