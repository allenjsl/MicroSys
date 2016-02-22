//采购模板相关业务实体 汪奇志 2015-04-22
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model
{
    #region 采购模板信息业务实体
    /// <summary>
    /// 采购模板信息业务实体
    /// </summary>
    public class MCaiGouMoBanInfo
    {
        /// <summary>
        /// 模板编号
        /// </summary>
        public string MoBanId { get; set; }
        /// <summary>
        /// 采购商编号
        /// </summary>
        public string CgsId { get; set; }
        /// <summary>
        /// 模板名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 操作人编号
        /// </summary>
        public string CaoZuoRenId { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 产品信息集合
        /// </summary>
        public IList<MCaiGouMoBanChanPinInfo> ChanPins { get; set; }
        /// <summary>
        /// 操作人姓名（OUTPUT）
        /// </summary>
        public string CaoZuoRenName { get; set; }
        /// <summary>
        /// 是否默认模板
        /// </summary>
        public bool IsMoRen { get; set; }
        /// <summary>
        /// 采购商名称（OUTPUT）
        /// </summary>
        public string CgsName { get; set; }
    }
    #endregion

    #region 采购模板产品信息业务实体
    /// <summary>
    /// 采购模板产品信息业务实体
    /// </summary>
    public class MCaiGouMoBanChanPinInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        public string ChanPinId { get; set; }
        /// <summary>
        /// 供应商编号
        /// </summary>
        public string GysId { get; set; }
        /// <summary>
        /// 采购数量
        /// </summary>
        public decimal ShuLiang { get; set; }
        /// <summary>
        /// 产品名称（OUTPUT）
        /// </summary>
        public string ChanPinName { get; set; }
        /// <summary>
        /// 供应商名称（OUTPUT）
        /// </summary>
        public string GysName { get; set; }
        /// <summary>
        /// 计量单位（OUTPUT）
        /// </summary>
        public string JiLiangDanWei { get; set; }
        /// <summary>
        /// 产品规格（OUTPUT）
        /// </summary>
        public string GuiGe { get; set; }
        /// <summary>
        /// 产品价格（上次报价）（OUTPUT）
        /// </summary>
        public decimal ChanPinJiaGe { get; set; }
        /// <summary>
        /// 产品品牌（OUTPUT）
        /// </summary>
        public string ChanPinPinPai { get; set; }
    }
    #endregion

    #region 采购模板信息查询业务实体
    /// <summary>
    /// 采购模板信息查询业务实体
    /// </summary>
    public class MCaiGouMoBanChaXunInfo
    {
        /// <summary>
        /// 采购商编号
        /// </summary>
        public string CgsId { get; set; }
        /// <summary>
        /// 模板编号
        /// </summary>
        public string Name { get; set; }
    }
    #endregion
}
