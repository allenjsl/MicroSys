//采购商财务对账 汪奇志 2015-05-07
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EyouSoft.Toolkit;

namespace EyouSoft.WEB.cgs
{
    /// <summary>
    /// 采购商财务对账
    /// </summary>
    public partial class CaiWuDuiZhang : CgsYeMian
    {
        #region attributes
        protected int pageSize = 20;
        protected int pageIndex = 1;
        protected int recordCount = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            InitPrivs();
            if (Utils.GetQueryStringValue("dotype") == "fukuan") FuKuan();
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

            info.CaiGouDanHao = Utils.GetQueryStringValue("txtCgdHao");
            info.CaiGouTime1 = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtCaiGouTime1"));
            info.CaiGouTime2 = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtCaiGouTime2"));
            info.CgsId = YongHuInfo.GongSiId;
            info.CgsFuKuanStatus = (EyouSoft.Model.FuKuanStatus?)Utils.GetEnumValueNullable(typeof(EyouSoft.Model.FuKuanStatus), Utils.GetQueryStringValue("txtFuKuanStatus"));
            info.DingDanStatus = EyouSoft.Model.DingDanStatus.采购商确认收货;
            info.GysName = Utils.GetQueryStringValue("txtGysName");

            return info;
        }

        /// <summary>
        /// init repeater
        /// </summary>
        void InitRpt()
        {
            pageIndex = Utils.GetPadingIndex();
            var chaXun = GetChaXunInfo();
            object[] heJi;
            var items = new EyouSoft.BLL.BDingDan().GetDingDans(pageSize, pageIndex, ref recordCount, chaXun, out heJi);

            if (items != null && items.Count > 0)
            {
                rpt.DataSource = items;
                rpt.DataBind();

                ltrJinEHeJi.Text = ((decimal)heJi[0]).ToString("F2");
                phHeJi.Visible = true;

                phEmpty.Visible = false;
            }
            else
            {
                phEmpty.Visible = true;
                phHeJi.Visible = false;
            }
        }

        /// <summary>
        /// 付款
        /// </summary>
        void FuKuan()
        {
            var txtDingDanIds = Utils.GetFormValues("txtDingDanId[]");
            if (txtDingDanIds == null || txtDingDanIds.Length == 0) { Utils.RCWE_AJAX("0", "操作失败：请选择需要支付的款项"); }
           
            int jiShu1 = 0;
            int jiShu2 = 0;

            foreach (var txtDingDanId in txtDingDanIds)
            {
                int bllRetCode = 0;
                bllRetCode = new EyouSoft.BLL.BFin().SheZhiCgsFuKuanStatus(YongHuInfo.GongSiId, txtDingDanId, EyouSoft.Model.FuKuanStatus.已付款, YongHuInfo.YongHuId);
                if (bllRetCode == 1) jiShu1++;
                else jiShu2++;
            }

            Utils.RCWE_AJAX("1", "操作成功");
        }

        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            if (!YongHuInfo.IsPrivs(EyouSoft.Model.CGS_Privs1.财务管理))
            {
                Response.Redirect("/cgs/default1.aspx");
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
            var _status = (EyouSoft.Model.FuKuanStatus)status;

            string _fuKuan = "<a href=\"javascript:void(0)\" class=\"blue_btn\" data-class=\"fukuan\">确认付款</a>&nbsp&nbsp;";
            string _chaKan = "<a href=\"javascript:void(0)\" class=\"blue_btn\" data-class=\"chakan\">查看</a>&nbsp&nbsp;";
            string s = _chaKan;

            if (_status == EyouSoft.Model.FuKuanStatus.未付款)
            {
                s += _fuKuan;
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
