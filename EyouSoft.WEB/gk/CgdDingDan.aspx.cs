//采购单-订单管理 汪奇志 2015-05-07
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EyouSoft.Toolkit;

namespace EyouSoft.WEB.gk
{
    using EyouSoft.Model;

    /// <summary>
    /// 采购单-订单管理
    /// </summary>
    public partial class CgdDingDan : GkYeMian
    {
        #region attributes
        protected int pageSize = 20;
        protected int pageIndex = 1;
        protected int recordCount = 0;

        protected int T = Utils.GetInt(Utils.GetQueryStringValue("T"), 0);
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            InitPrivs();
            InitRpt();
        }

        #region private members
        /// <summary>
        /// get chaxun
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.MDingDanChaXunInfo GetChaXunInfo()
        {
            var info = new EyouSoft.Model.MDingDanChaXunInfo();
            int[] s = null;
            switch (T)
            {
                case 0:
                    s = new int[5];
                    s[0] = (int)DingDanStatus.采购申请;
                    s[1] = (int)DingDanStatus.供应商完成报价;
                    s[2] = (int)DingDanStatus.采购商确认报价;
                    s[3] = (int)DingDanStatus.供应商发货完成;
                    s[4] = (int)DingDanStatus.采购商确认收货;
                    info.QueRenStatus = QueRenStatus.未确认;
                    break;
                default:
                    s = new int[1];
                    s[0] = (int)DingDanStatus.采购商确认收货;
                    info.QueRenStatus = QueRenStatus.已确认;
                    break;
            }

            info.CaiGouDanHao = Utils.GetQueryStringValue("txtCgdHao");
            info.CaiGouTime1 = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtCaiGouTime1"));
            info.CaiGouTime2 = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtCaiGouTime2"));
            info.CgsName = Utils.GetQueryStringValue("txtCgsName");

            info.DingDanStatus = (EyouSoft.Model.DingDanStatus?)Utils.GetEnumValueNullable(typeof(EyouSoft.Model.DingDanStatus), Utils.GetQueryStringValue("txtStatus"));

            return info;
        }

        /// <summary>
        /// init repeater
        /// </summary>
        void InitRpt()
        {
            pageIndex = Utils.GetPadingIndex();
            var chaXun = GetChaXunInfo();

            var items = new EyouSoft.BLL.BDingDan().GetDingDans(pageSize, pageIndex, ref recordCount, chaXun);

            if (items != null && items.Count > 0)
            {
                rpt.DataSource = items;
                rpt.DataBind();

                phEmpty.Visible = false;
            }
            else
            {
                phEmpty.Visible = true;
            }
        }

        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            if (!YongHuInfo.IsPrivs(EyouSoft.Model.GK_Privs1.采购订单监控))
            {
                Response.Redirect("/gk/default1.aspx");
            }
        }
        #endregion

        #region protected members
        /// <summary>
        /// get caozuo
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        protected string GetCaoZuo(object status)
        {
            var _status = (EyouSoft.Model.DingDanStatus)status;

            string s = string.Empty;

            switch (_status)
            {
                //case EyouSoft.Model.DingDanStatus.取消采购:
                //case EyouSoft.Model.DingDanStatus.采购商确认收货:
                default:
                    s = "<a href=\"javascript:void(0)\" class=\"blue_btn\" data-class=\"chakan\">查看</a>";
                    break;
                //default:
                //    s = "<a href=\"javascript:void(0)\" class=\"blue_btn\" data-class=\"guanli\">管理</a>";
                //    break;
            }

            return s;
        }

        /// <summary>
        /// get jine
        /// </summary>
        /// <param name="jinE"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        protected string GetJinE(object jinE, object status)
        {
            var _jinE = (decimal)jinE;

            return _jinE.ToString("F2");
        }
        #endregion
    }
}
