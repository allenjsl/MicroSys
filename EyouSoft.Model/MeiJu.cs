using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model
{
    #region 公司类型
    /// <summary>
    /// 公司类型
    /// </summary>
    public enum GongSiLeiXing
    {
        /// <summary>
        /// 供应商
        /// </summary>
        供应商=0,
        /// <summary>
        /// 采购商
        /// </summary>
        采购商=1
    }
    #endregion

    #region 用户类型
    /// <summary>
    /// 用户类型
    /// </summary>
    public enum YongHuLeiXing
    {
        /// <summary>
        /// 供应商
        /// </summary>
        供应商 = 0,
        /// <summary>
        /// 采购商
        /// </summary>
        采购商 = 1,
        /// <summary>
        /// 平台
        /// </summary>
        平台 = 255
    }
    #endregion

    #region 用户状态
    /// <summary>
    /// 用户状态
    /// </summary>
    public enum YongHuStatus
    {
        /// <summary>
        /// 启用
        /// </summary>
        启用=0,
        /// <summary>
        /// 禁用
        /// </summary>
        禁用=1
    }
    #endregion

    #region 性别
    /// <summary>
    /// 性别
    /// </summary>
    public enum XingBie
    {
        /// <summary>
        /// 男
        /// </summary>
        男=0,
        /// <summary>
        /// 女
        /// </summary>
        女=1
    }
    #endregion

    #region 用户角色状态
    /// <summary>
    /// 用户角色状态
    /// </summary>
    public enum YongHuJueSeStatus
    {
        /// <summary>
        /// 可用
        /// </summary>
        可用 = 0,
        /// <summary>
        /// 禁用
        /// </summary>
        禁用 = 1
    }
    #endregion

    #region 用户登录类型
    /// <summary>
    /// 用户登录类型
    /// </summary>
    public enum LoginLeiXing
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        用户登录 = 0,
        /// <summary>
        /// 自动登录
        /// </summary>
        自动登录 = 1
    }
    #endregion

    #region 采购单状态
    /// <summary>
    /// 采购单状态
    /// </summary>
    public enum CaiGouDanStatus
    {
        /// <summary>
        /// 计划采购
        /// </summary>
        计划采购 = 0,
        /// <summary>
        /// 已下单
        /// </summary>
        已下单 = 1
    }
    #endregion

    #region 订单状态
    /// <summary>
    /// 订单状态
    /// </summary>
    public enum DingDanStatus
    {
        /// <summary>
        /// 计划采购
        /// </summary>
        计划采购 = 0,
        /// <summary>
        /// 采购申请
        /// </summary>
        采购申请 = 1,
        /// <summary>
        /// 供应商完成报价
        /// </summary>
        供应商完成报价 = 2,
        /// <summary>
        /// 采购商确认报价
        /// </summary>
        采购商确认报价 = 3,
        /// <summary>
        /// 供应商发货完成
        /// </summary>
        供应商发货完成 = 4,
        /// <summary>
        /// 采购商确认收货
        /// </summary>
        采购商确认收货 = 5,
        /// <summary>
        /// 取消采购
        /// </summary>
        取消采购 = 6
    }
    #endregion

    #region 确认状态
    /// <summary>
    /// 确认状态
    /// </summary>
    public enum QueRenStatus
    {
        /// <summary>
        /// 未确认
        /// </summary>
        未确认=0,
        /// <summary>
        /// 已确认
        /// </summary>
        已确认=1
    }
    #endregion

    #region 是否
    /// <summary>
    /// 是否
    /// </summary>
    public enum ShiFou
    {
        否=0,
        是=1
    }
    #endregion

    #region 消息类型
    /// <summary>
    /// 消息类型
    /// </summary>
    public enum XiaoXiLeiXing
    {
        /// <summary>
        /// 未知类型
        /// </summary>
        None = 0,
        /// <summary>
        /// 供应商待报价
        /// </summary>
        供应商待报价=1,
        /// <summary>
        /// 采购商待确认报价
        /// </summary>
        采购商待确认报价=2,
        /// <summary>
        /// 供应商待发货
        /// </summary>
        供应商待发货=3,
        /// <summary>
        /// 采购商待确认收货
        /// </summary>
        采购商待确认收货=4,
        /// <summary>
        /// 公司注册待审核
        /// </summary>
        公司注册待审核=5        
    }
    #endregion

    #region 消息状态
    /// <summary>
    /// 消息状态
    /// </summary>
    public enum XiaoXiStatus
    {
        /// <summary>
        /// 未读
        /// </summary>
        未读=0,
        /// <summary>
        /// 已读
        /// </summary>
        已读=1
    }
    #endregion

    #region 操作日志类型
    /// <summary>
    /// 操作日志类型
    /// </summary>
    public enum CaoZuoLogLeiXing
    {
        /// <summary>
        /// none
        /// </summary>
        None=0
    }
    #endregion

    #region 付款状态
    /// <summary>
    /// 付款状态
    /// </summary>
    public enum FuKuanStatus
    {
        /// <summary>
        /// 未付款
        /// </summary>
        未付款=0,
        /// <summary>
        /// 已付款
        /// </summary>
        已付款=1
    }
    #endregion

    #region 公司、用户来源
    /// <summary>
    /// 公司、用户来源
    /// </summary>
    public enum LaiYuan
    {
        /// <summary>
        /// 平台添加
        /// </summary>
        平台添加 = 0,
        /// <summary>
        /// 用户注册
        /// </summary>
        用户注册 = 1
    }
    #endregion

    #region 审核状态
    /// <summary>
    /// 审核状态
    /// </summary>
    public enum ShenHeStatus
    {
        /// <summary>
        /// 未审核
        /// </summary>
        未审核 = 0,
        /// <summary>
        /// 已审核
        /// </summary>
        已审核 = 1
    }
    #endregion
}
