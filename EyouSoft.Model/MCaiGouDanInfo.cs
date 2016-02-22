//采购单相关信息业务实体 汪奇志 2015-04-22
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model
{
    #region 采购单信息业务实体
    /// <summary>
    /// 采购单信息业务实体
    /// </summary>
    public class MCaiGouDanInfo
    {
        /// <summary>
        /// 采购单编号
        /// </summary>
        public string CaiGouDanId { get; set; }
        /// <summary>
        /// 采购商编号
        /// </summary>
        public string CgsId { get; set; }
        /// <summary>
        /// 采购单号
        /// </summary>
        public string CaiGouDanHao { get; set; }
        /// <summary>
        /// 采购单名称
        /// </summary>
        public string CaiGouDanName { get; set; }
        /// <summary>
        /// 采购模板编号
        /// </summary>
        public string MoBanId { get; set; }
        /// <summary>
        /// 采购单状态
        /// </summary>
        public CaiGouDanStatus Status { get; set; }
        /// <summary>
        /// 操作人编号
        /// </summary>
        public string CaoZuoRenId { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 收货地址
        /// </summary>
        public string ShouHuoDiZhi { get; set; }
        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string ShouHuoRenName { get; set; }
        /// <summary>
        /// 收货人电话
        /// </summary>
        public string ShouHuoRenDianHua { get; set; }
        /// <summary>
        /// 采购部门
        /// </summary>
        public string CaiGouBuMen { get; set; }
        /// <summary>
        /// 发布人编号
        /// </summary>
        public string FaBuRenId { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime FaBuTime { get; set; }
        /// <summary>
        /// 操作人姓名（OUTPUT）
        /// </summary>
        public string CaoZuoRenName { get; set; }
        /// <summary>
        /// 发布人姓名（OUTPUT）
        /// </summary>
        public string FaBuRenName { get; set; }
        /// <summary>
        /// 采购产品集合
        /// </summary>
        public IList<MCaiGouDanChanPinInfo> ChanPins { get; set; }
        /// <summary>
        /// 采购单说明
        /// </summary>
        public string CaiGouDanShuoMing { get; set; }
        /// <summary>
        /// 要求到货时间
        /// </summary>
        public DateTime? YaoQiuDaoHuoTime { get; set; }

        /// <summary>
        /// 收货地址编号
        /// </summary>
        public string ShouHuoDiZhiId { get; set; }
    }
    #endregion

    #region 采购单产品信息业务实体
    /// <summary>
    /// 采购单产品信息业务实体
    /// </summary>
    public class MCaiGouDanChanPinInfo
    {
        /// <summary>
        /// 采购产品明细编号
        /// </summary>
        public string MingXiId { get; set; }
        /// <summary>
        /// 采购订单编号
        /// </summary>
        public string DingDanId { get; set; }

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
        /// 产品规格（OUTPUT）
        /// </summary>
        public string ChanPinGuiGe { get; set; }
        /// <summary>
        /// 计量单位（OUTPUT）
        /// </summary>
        public string JiLiangDanWei { get; set; }
        /// <summary>
        /// 产品价格（OUTPUT）
        /// </summary>
        public decimal ChanPinJiaGe{ get; set; }
        /// <summary>
        /// 产品品牌（OUTPUT）
        /// </summary>
        public string ChanPinPinPai{ get; set; }
    }
    #endregion

    #region 采购单信息查询业务实体
    /// <summary>
    /// 采购单信息查询业务实体
    /// </summary>
    public class MCaiGouDanChaXunInfo
    {
        /// <summary>
        /// 采购单名称
        /// </summary>
        public string CaiGouDanName { get; set; }
        /// <summary>
        /// 采购单号
        /// </summary>
        public string CaiGouDanHao { get; set; }
        /// <summary>
        /// 发布时间-起
        /// </summary>
        public DateTime? FaBuTime1 { get; set; }
        /// <summary>
        /// 发布时间-止
        /// </summary>
        public DateTime? FaBuTime2 { get; set; }
        /// <summary>
        /// 采购商编号
        /// </summary>
        public string CgsId { get; set; }
    }
    #endregion
}
