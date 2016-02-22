//采购订单信息相关业务实体 汪奇志 2015-04-23
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model
{
    #region 采购订单信息业务实体
    /// <summary>
    /// 采购订单信息业务实体
    /// </summary>
    public class MDingDanInfo
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string DingDanId { get; set; }
        /// <summary>
        /// 采购单编号
        /// </summary>
        public string CaiGouDanId { get; set; }
        /// <summary>
        /// 供应商编号
        /// </summary>
        public string GysId { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string GysName { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public DingDanStatus Status { get; set; }
        /// <summary>
        /// 报价金额
        /// </summary>
        public decimal JinE { get; set; }
        /// <summary>
        /// 送货人姓名
        /// </summary>
        public string SongHuoRenName { get; set; }
        /// <summary>
        /// 送货人电话
        /// </summary>
        public string SongHuoRenDianHua { get; set; }
        /// <summary>
        /// 送货时间（实际送货时间）
        /// </summary>
        public DateTime? SongHuoTime { get; set; }
        /// <summary>
        /// 到货时间（实际到货时间）
        /// </summary>
        public DateTime? DaoHuoTime { get; set; }
        /// <summary>
        /// 供应商报价人编号
        /// </summary>
        public string GysBaoJiaRenId { get; set; }
        /// <summary>
        /// 供应商报价操作时间
        /// </summary>
        public DateTime? GysBaoJiaTime { get; set; }
        /// <summary>
        /// 采购商确认人编号
        /// </summary>
        public string CgsQueRenRenId { get; set; }
        /// <summary>
        /// 采购商确认操作时间
        /// </summary>
        public DateTime? CgsQueRenTime { get; set; }
        /// <summary>
        /// 供应商发货人编号
        /// </summary>
        public string GysFaHuoRenId { get; set; }
        /// <summary>
        /// 供应商发货操作时间
        /// </summary>
        public DateTime? GysFaHuoTime { get; set; }
        /// <summary>
        /// 采购商收货人编号
        /// </summary>
        public string CgsShouHuoRenId { get; set; }
        /// <summary>
        /// 采购商收货操作时间
        /// </summary>
        public DateTime? CgsShouHuoTime { get; set; }
        /// <summary>
        /// 供应商到货确认人编号
        /// </summary>
        public string GysDaoHuoQueRenRenId { get; set; }
        /// <summary>
        /// 供应商到货确认时间
        /// </summary>
        public DateTime? GysDaoHuoQueRenTime { get; set; }
        /// <summary>
        /// 产品信息集合
        /// </summary>
        public IList<MDingDanChanPinInfo> ChanPins { get; set; }
        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime XiaDanTime { get; set; }
        /// <summary>
        /// 采购计划发布时间
        /// </summary>
        public DateTime? FaBuTime { get; set; }
        /// <summary>
        /// 供应商发货说明
        /// </summary>
        public string GysFaHuoShuoMing { get; set; }
        /// <summary>
        /// 采购商名称
        /// </summary>
        public string CgsName { get; set; }
        /// <summary>
        /// 采购单号
        /// </summary>
        public string CaiGouDanHao { get; set; }
        /// <summary>
        /// 采购单名称
        /// </summary>
        public string CaiGouDanName { get; set; }
        /// <summary>
        /// 采购部门
        /// </summary>
        public string CaiGouBuMen { get; set; }
        /// <summary>
        /// 采购发布人
        /// </summary>
        public string FaBuRenName { get; set; }
        /// <summary>
        /// 采购发布时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 模版编号
        /// </summary>
        public string MoBanId { get; set; }
        /// <summary>
        /// 要求到货时间
        /// </summary>
        public DateTime? YaoQiuDaoHuoTime { get; set; }
        /// <summary>
        /// 采购商收货人姓名
        /// </summary>
        public string CgsShouHuoRen { get; set; }
        /// <summary>
        /// 供应商到货确认状态
        /// </summary>
        public QueRenStatus GysDaoHuoQueRenStatus { get; set; }
        /// <summary>
        /// 预计到货时间
        /// </summary>
        public DateTime? YuJiDaoHuoTime { get; set; }
        /// <summary>
        /// 操作人编号
        /// </summary>
        public string CaoZuoRenId { get; set; }

        /// <summary>
        /// 操作人姓名
        /// </summary>
        public string CaoZuoRenName { get; set; }
        /// <summary>
        /// 采购产品项数
        /// </summary>
        public int CaiGouChanPinXiangShu { get; set; }

        /// <summary>
        /// 采购商付款状态
        /// </summary>
        public FuKuanStatus CgsFuKuanStatus { get; set; }
        /// <summary>
        /// 采购商付款时间
        /// </summary>
        public DateTime? CgsFuKuanTime { get; set; }
        /// <summary>
        /// 采购商付款操作人编号
        /// </summary>
        public string CgsFuKuanCaoZuoRenId { get; set; }
        /// <summary>
        /// 采购商付款操作人姓名（OUTPUT）
        /// </summary>
        public string CgsFuKuanCaoZuoRenName { get; set; }
        /// <summary>
        /// 采购商已付款金额（OUTPUT）
        /// </summary>
        public decimal CgsYiFuKuanJinE { get; set; }
        /// <summary>
        /// 采购商编号
        /// </summary>
        public string CgsId { get; set; }
        /// <summary>
        /// 供应商送货人（地址）编号
        /// </summary>
        public string GysSongHuoRenId { get; set; }
        /// <summary>
        /// 供应商联系QQ
        /// </summary>
        public string GysLxQQ { get; set; }
        /// <summary>
        /// 采购商联系QQ
        /// </summary>
        public string CgsLxQQ { get; set; }
    }
    #endregion

    #region 采购订单产品信息业务实体
    /// <summary>
    /// 采购订单产品信息业务实体
    /// </summary>
    public class MDingDanChanPinInfo
    {
        /// <summary>
        /// 明细编号
        /// </summary>
        public string MingXiId { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        public string ChanPinId { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ChanPinName { get; set; }
        /// <summary>
        /// 产品规格
        /// </summary>
        public string ChanPinGuiGe { get; set; }
        /// <summary>
        /// 计量单位
        /// </summary>
        public string JiLiangDanWei { get; set; }
        /// <summary>
        /// 采购数量
        /// </summary>
        public decimal ShuLiang { get; set; }
        /// <summary>
        /// 产品单价
        /// </summary>
        public decimal ChanPinJiaGe { get; set; }
        /// <summary>
        /// 报价金额
        /// </summary>
        public decimal JinE { get; set; }
        /// <summary>
        /// 实际发货数量
        /// </summary>
        public decimal FaHuoShuLiang { get; set; }
        /// <summary>
        /// 实际到货数量
        /// </summary>
        public decimal DaoHuoShuLiang { get; set; }
        /// <summary>
        /// 采购商到货说明
        /// </summary>
        public string CgsDaoHuoShuoMing { get; set; }
        /// <summary>
        /// 供应商报价说明
        /// </summary>
        public string GysBaoJiaShuoMing { get; set; }
        /// <summary>
        /// 产品单价（上次）（OUTPUT）
        /// </summary>
        public decimal ChanPinJiaGe1 { get; set; }
    }
    #endregion

    #region 采购订单信息查询业务实体
    /// <summary>
    /// 采购订单信息查询业务实体
    /// </summary>
    public class MDingDanChaXunInfo
    {
        /// <summary>
        /// 采购商编号
        /// </summary>
        public string CgsId { get; set; }
        /// <summary>
        /// 采购单号
        /// </summary>
        public string CaiGouDanHao { get; set; }
        /// <summary>
        /// 采购时间-起
        /// </summary>
        public DateTime? CaiGouTime1 { get; set; }
        /// <summary>
        /// 采购时间-止
        /// </summary>
        public DateTime? CaiGouTime2 { get; set; }
        /// <summary>
        /// 供应商编号
        /// </summary>
        public string GysId { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public DingDanStatus? DingDanStatus { get; set; }
        /// <summary>
        /// 订单状态【1，2】
        /// </summary>
        public int[] DingDanStatusIn { get; set; }

        /// <summary>
        /// 采购商付款状态
        /// </summary>
        public FuKuanStatus? CgsFuKuanStatus { get; set; }
        /// <summary>
        /// 采购商付款时间-起
        /// </summary>
        public DateTime? CgsFuKuanTime1 { get; set; }
        /// <summary>
        /// 采购商付款时间-止
        /// </summary>
        public DateTime? CgsFuKuanTime2 { get; set; }
        /// <summary>
        /// 供应商确认到货状态
        /// </summary>
        public QueRenStatus? QueRenStatus { get; set; }
        /// <summary>
        /// 采购单编号
        /// </summary>
        public string CaiGouDanId { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string GysName { get; set; }
        /// <summary>
        /// 采购商名称
        /// </summary>
        public string CgsName { get; set; }
    }
    #endregion

    #region 订单发货信息业务实体
    /// <summary>
    /// 订单发货信息业务实体
    /// </summary>
    public class MDingDanFaHuoInfo
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string DingDanId { get; set; }
        /// <summary>
        /// 送货人姓名
        /// </summary>
        public string SongHuoRenName { get; set; }
        /// <summary>
        /// 送货人电话
        /// </summary>
        public string SongHuoRenDianHua { get; set; }
        /// <summary>
        /// 送货时间
        /// </summary>
        public DateTime SongHuoTime { get; set; }
        /// <summary>
        /// 发货产品信息
        /// </summary>
        public IList<MDingDanChanPinInfo> ChanPins { get; set; }
        /// <summary>
        /// 供应商发货说明
        /// </summary>
        public string GysFaHuoShuoMing { get; set; }
        /// <summary>
        /// 预计到货时间
        /// </summary>
        public DateTime? YuJiDaoHuoTime { get; set; }
        /// <summary>
        /// 供应商送货人（地址）编号
        /// </summary>
        public string GysSongHuoRenId { get; set; }
    }
    #endregion

    #region 订单收获信息业务实体
    /// <summary>
    /// 订单收获信息业务实体
    /// </summary>
    public class MDingDanShouHuoInfo
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string DingDanId { get; set; }
        /// <summary>
        /// 实际到货时间
        /// </summary>
        public DateTime DaoHuoTime { get; set; }
        /// <summary>
        /// 采购商收货人姓名
        /// </summary>
        public string CgsShouHuoRen { get; set; }
        /// <summary>
        /// 到货产品信息
        /// </summary>
        public IList<MDingDanChanPinInfo> ChanPins { get; set; }
    }
    #endregion

    #region 订单报价信息业务实体
    /// <summary>
    /// 订单报价信息业务实体
    /// </summary>
    public class MDingDanBaoJiaInfo
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string DingDanId { get; set; }
        /// <summary>
        /// 报价产品信息
        /// </summary>
        public IList<MDingDanChanPinInfo> ChanPins { get; set; }
        /// <summary>
        /// 操作人编号
        /// </summary>
        public string CaoZuoRenId { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get { return DateTime.Now; } }
    }
    #endregion
}
