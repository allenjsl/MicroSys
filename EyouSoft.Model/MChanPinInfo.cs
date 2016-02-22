//供应商产品信息相关业务实体 汪奇志 2015-04-22
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model
{
    #region 附件信息业务实体
    /// <summary>
    /// 附件信息业务实体
    /// </summary>
    public class MFuJianInfo
    {
        /// <summary>
        /// 附件编号
        /// </summary>
        public int FuJianId { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string Filepath { get; set; }
        /// <summary>
        /// 附件类型
        /// </summary>
        public int LeiXing { get; set; }
    }
    #endregion

    #region 供应商产品信息业务实体
    /// <summary>
    /// 供应商产品信息业务实体
    /// </summary>
    public class MChanPinInfo
    {
        /// <summary>
        /// 产品编号
        /// </summary>
        public string ChanPinId { get; set; }
        /// <summary>
        /// 供应商编号
        /// </summary>
        public string GysId { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string GysName { get; set; }
        /// <summary>
        /// 产品编码
        /// </summary>
        public string BianMa { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 产品品牌
        /// </summary>
        public string PinPai { get; set; }
        /// <summary>
        /// 产品规格
        /// </summary>
        public string GuiGe { get; set; }
        /// <summary>
        /// 计量单位
        /// </summary>
        public string JiLiangDanWei { get; set; }
        /// <summary>
        /// 市场价格
        /// </summary>
        public decimal JiaGe1 { get; set; }
        /// <summary>
        /// 最新价格
        /// </summary>
        public decimal JiaGe2 { get; set; }
        /// <summary>
        /// 产品介绍
        /// </summary>
        public string JieShao { get; set; }
        /// <summary>
        /// 操作人编号
        /// </summary>
        public string CaoZuoRenId { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 附件集合
        /// </summary>
        public IList<MFuJianInfo> FuJians { get; set; }

        /// <summary>
        /// 操作人姓名（OUTPUT）
        /// </summary>
        public string CaoZuoRenName { get; set; }
    }
    #endregion

    #region 供应商产品信息查询业务实体
    /// <summary>
    /// 供应商产品信息查询业务实体
    /// </summary>
    public class MChanPinChaXunInfo
    {
        /// <summary>
        /// 供应商编号
        /// </summary>
        public string GysId { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string GysName { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 发布时间-起
        /// </summary>
        public DateTime? FaBuTime1 { get; set; }
        /// <summary>
        /// 发布时间-止
        /// </summary>
        public DateTime? FaBuTime2 { get; set; }
        /// <summary>
        /// 产品编码
        /// </summary>
        public string BianMa { get; set; }

        /// <summary>
        /// 采购商编号（筛选采购商关联的供应商产品时用）
        /// </summary>
        public string CgsId { get; set; }
        /// <summary>
        /// 产品品牌
        /// </summary>
        public string PinPai { get; set; }
        /// <summary>
        /// 产品规格
        /// </summary>
        public string GuiGe { get; set; }
    }
    #endregion

    #region 产品价格信息业务实体
    /// <summary>
    /// 产品价格信息业务实体
    /// </summary>
    public class MChanPinJiaGeInfo
    {
        /// <summary>
        /// 价格编号
        /// </summary>
        public string JiaGeId { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        public string ChanPinId { get; set; }
        /// <summary>
        /// 上次价格(OUTPUT)
        /// </summary>
        public decimal JiaGe1 { get; set; }
        /// <summary>
        /// 最新价格
        /// </summary>
        public decimal JiaGe2 { get; set; }
        /// <summary>
        /// 价格说明
        /// </summary>
        public string ShuoMing { get; set; }
        /// <summary>
        /// 操作人编号
        /// </summary>
        public string CaoZuoRenId { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 操作人姓名（OUTPUT）
        /// </summary>
        public string CaoZuoRenName { get; set; }
    }
    #endregion
}
