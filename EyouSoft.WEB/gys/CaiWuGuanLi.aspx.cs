using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace EyouSoft.WEB.gys
{
    using EyouSoft.Model;
    using EyouSoft.Toolkit;

    public partial class ChanPinJiaGe : GysYeMian
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
            info.CgsName = Utils.GetQueryStringValue("txtCgsName");
            info.GysId = YongHuInfo.GongSiId;
            info.CgsFuKuanStatus = (EyouSoft.Model.FuKuanStatus?)Utils.GetEnumValueNullable(typeof(EyouSoft.Model.FuKuanStatus), Utils.GetQueryStringValue("txtFuKuanStatus"));
            info.DingDanStatus = EyouSoft.Model.DingDanStatus.采购商确认收货;
            info.QueRenStatus = QueRenStatus.已确认;

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
            string txtDingDanId = Utils.GetFormValue("txtDingDanId");

            int bllRetCode = new EyouSoft.BLL.BFin().SheZhiCgsFuKuanStatus(YongHuInfo.GongSiId, txtDingDanId, EyouSoft.Model.FuKuanStatus.已付款, YongHuInfo.YongHuId);

            if (bllRetCode == 1) Utils.RCWE_AJAX("1", "操作成功");
            else Utils.RCWE_AJAX("0", "操作失败");
        }

        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            if (!YongHuInfo.IsPrivs(EyouSoft.Model.GYS_Privs1.财务管理))
            {
                Response.Redirect("/gys/default1.aspx");
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

            //string _fuKuan = "<a href=\"javascript:void(0)\" class=\"blue_btn\" data-class=\"fukuan\">确认付款</a>&nbsp&nbsp;";
            string _chaKan = "<a href=\"javascript:void(0)\" class=\"blue_btn\" data-class=\"chakan\">查看</a>&nbsp&nbsp;";
            string s = _chaKan;

            //if (_status == EyouSoft.Model.FuKuanStatus.未付款)
            //{
            //    s += _fuKuan;
            //}

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
