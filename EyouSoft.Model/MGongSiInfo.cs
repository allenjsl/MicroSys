using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model
{
    #region 采购商、供应商信息业务实体
    /// <summary>
    /// 采购商、供应商信息业务实体
    /// </summary>
    public class MGongSiInfo
    {
        /// <summary>
        /// 平台公司编号
        /// </summary>
        public const string PingTaiGongSiId = "00000000-0000-0000-0000-000000000000";

        /// <summary>
        /// 公司编号
        /// </summary>
        public string GongSiId { get; set; }
        /// <summary>
        /// 公司类型
        /// </summary>
        public GongSiLeiXing LeiXing { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 法人代表姓名
        /// </summary>
        public string FanRenName { get; set; }
        /// <summary>
        /// 省份编号
        /// </summary>
        public int ShengFenId { get; set; }
        /// <summary>
        /// 城市编号
        /// </summary>
        public int ChengShiId { get; set; }
        /// <summary>
        /// 公司地址
        /// </summary>
        public string DiZhi { get; set; }
        /// <summary>
        /// 营业执照附件
        /// </summary>
        public string YingYeZhiZhaoFilepath { get; set; }
        /// <summary>
        /// 组织机构代码证附件
        /// </summary>
        public string ZuZhiJiGouFilepath { get; set; }
        /// <summary>
        /// 负责人姓名
        /// </summary>
        public string FuZeRenName { get; set; }
        /// <summary>
        /// 负责人电话
        /// </summary>
        public string FuZeRenDianHua { get; set; }
        /// <summary>
        /// 负责人身份证号
        /// </summary>
        public string FuZeRenShenFenZhengHao { get; set; }
        /// <summary>
        /// 负责人照片附件
        /// </summary>
        public string FuZeRenZhaoPianFilepath { get; set; }
        /// <summary>
        /// 财务姓名
        /// </summary>
        public string CaiWuName { get; set; }
        /// <summary>
        /// 财务电话
        /// </summary>
        public string CaiWuDianHua { get; set; }
        /// <summary>
        /// 账务身份证号
        /// </summary>
        public string CaiWuShenFenZhengHao { get; set; }
        /// <summary>
        /// 财务照片附件
        /// </summary>
        public string CaiWuZhaoPianFilepath { get; set; }
        /// <summary>
        /// 操作人编号
        /// </summary>
        public string CaoZuoRenId { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// logo filepath
        /// </summary>
        public string LogoFilepath { get; set; }

        /// <summary>
        /// 操作人姓名（OUTPUT）
        /// </summary>
        public string CaoZuoRenName { get; set; }
        /// <summary>
        /// 联系QQ
        /// </summary>
        public string LxQQ { get; set; }
        /// <summary>
        /// 公司来源
        /// </summary>
        public LaiYuan LaiYuan { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public ShenHeStatus ShenHeStatus { get; set; }
    }
    #endregion

    #region 采购商、供应商信息查询业务实体
    /// <summary>
    /// 采购商、供应商信息查询业务实体
    /// </summary>
    public class MGongSiChaXunInfo
    {
        /// <summary>
        /// 公司名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 公司类型
        /// </summary>
        public GongSiLeiXing? LeiXing { get; set; }

        /// <summary>
        /// 采购商编号（筛选采购商关联的供应商时用）
        /// </summary>
        public string CgsId { get; set; }

        /// <summary>
        /// 公司来源
        /// </summary>
        public LaiYuan? LaiYuan { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public ShenHeStatus? ShenHeStatus { get; set; }
    }
    #endregion

    #region 公司关系信息业务实体
    /// <summary>
    /// 公司关系信息业务实体
    /// </summary>
    public class MGongSiGuanXiInfo
    {
        /// <summary>
        /// 被关注公司编号
        /// </summary>
        public string GongSiId { get; set; }
        /// <summary>
        /// 被关注公司类型
        /// </summary>
        public GongSiLeiXing LeiXing { get; set; }
        /// <summary>
        /// 被关注公司名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 被关注公司法人代表姓名
        /// </summary>
        public string FanRenName { get; set; }
        /// <summary>
        /// 负责人姓名
        /// </summary>
        public string FuZeRenName { get; set; }
        /// <summary>
        /// 负责人电话
        /// </summary>
        public string FuZeRenDianHua { get; set; }
        /// <summary>
        /// 是否关注
        /// </summary>
        public bool IsGuanZhu { get; set; }
    }
    #endregion

    #region 公司关系信息业务实体
    /// <summary>
    /// 公司关系信息业务实体
    /// </summary>
    public class MGongSiGuanXiChaXunInfo
    {        
        /// <summary>
        /// 被关注公司名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 关注公司编号
        /// </summary>
        public string GongSiId { get; set; }
        /// <summary>
        /// 是否关注
        /// </summary>
        public bool? IsGuanZhu { get; set; }
        /// <summary>
        /// 被关注公司类型
        /// </summary>
        public EyouSoft.Model.GongSiLeiXing? LeiXing { get; set; }
    }
    #endregion
}
