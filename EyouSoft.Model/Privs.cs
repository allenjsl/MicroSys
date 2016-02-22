using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model
{
    #region 采购商一级栏目
    /// <summary>
    /// 采购商一级栏目
    /// </summary>
    public enum CGS_Privs1
    {
        采购管理=100,
        财务管理=101,        
        系统设置=102,
        消息中心=103
    }
    #endregion    

    #region 供应商一级栏目
    /// <summary>
    /// 供应商一级栏目
    /// </summary>
    public enum GYS_Privs1
    {
        供货管理 = 200,
        产品管理=201,
        财务管理=202,
        消息中心=203,
        系统设置=204
    }
    #endregion

    #region 管控一级栏目
    /// <summary>
    /// 管控一级栏目
    /// </summary>
    public enum GK_Privs1
    {
        采购商管理 = 300,
        供应商管理=301,
        采购订单监控=302,
        系统设置=303,
        消息中心=304
    }
    #endregion
}
