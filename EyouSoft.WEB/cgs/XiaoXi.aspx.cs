//采购商-消息中心 汪奇志 2015-05-08
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
    /// 采购商-消息中心
    /// </summary>
    public partial class XiaoXi : CgsYeMian
    {
        #region attributes
        protected int pageSize = 20;
        protected int pageIndex = 1;
        protected int recordCount = 0;

        protected string XiaoXiStatus = "";
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            InitPrivs();
            if (Utils.GetQueryStringValue("dotype") == "shezhiyidu") SheZhiYiDu();
            InitRpt();
        }

        #region private members
        /// <summary>
        /// get chaxun
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.MXiaoXiChaXunInfo GetChaXunInfo()
        {
            var info = new EyouSoft.Model.MXiaoXiChaXunInfo();

            info.JieShouGongSiId = YongHuInfo.GongSiId;
            info.LeiXings = new List<EyouSoft.Model.XiaoXiLeiXing>();
            info.LeiXings.Add(EyouSoft.Model.XiaoXiLeiXing.采购商待确认报价);
            info.LeiXings.Add(EyouSoft.Model.XiaoXiLeiXing.采购商待确认收货);
            info.Status = (EyouSoft.Model.XiaoXiStatus?)Utils.GetEnumValueNullable(typeof(EyouSoft.Model.XiaoXiStatus), Utils.GetQueryStringValue("txtStatus"));

            if (Utils.GetQueryStringValue("txtIsChaXun") != "1")
            {
                info.Status = EyouSoft.Model.XiaoXiStatus.未读;
                XiaoXiStatus = ((int)EyouSoft.Model.XiaoXiStatus.未读).ToString();
            }


            return info;
        }

        /// <summary>
        /// init repeater
        /// </summary>
        void InitRpt()
        {
            pageIndex = Utils.GetPadingIndex();
            var chaXun = GetChaXunInfo();
            var items = new EyouSoft.BLL.BXiaoXi().GetXiaoXis(pageSize, pageIndex, ref recordCount, chaXun);

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
        /// 设置已读
        /// </summary>
        void SheZhiYiDu()
        {
            var txtXiaoXiId = Utils.GetFormValue("txtXiaoXiId");

            int bllRetCode = new EyouSoft.BLL.BXiaoXi().SheZhiYiDu(txtXiaoXiId, YongHuInfo.YongHuId);

            if (bllRetCode == 1) Utils.RCWE_AJAX("1", "操作成功");
            else Utils.RCWE_AJAX("0", "操作失败");
        }

        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            if (!YongHuInfo.IsPrivs(EyouSoft.Model.CGS_Privs1.消息中心))
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
        /// <param name="leiXing"></param>
        /// <returns></returns>
        protected string GetCaoZuo(object status, object leiXing)
        {
            var _status = (EyouSoft.Model.XiaoXiStatus)status;
            var _leiXing = (EyouSoft.Model.XiaoXiLeiXing)leiXing;
            
            var s = string.Empty;
            var _shiZhiYiDu = "<a href=\"javascript:void(0)\" class=\"blue_btn\" data-class=\"shezhiyidu\">设为已读</a>&nbsp;&nbsp;";
            var _chaKan = "<a href=\"javascript:void(0)\" class=\"blue_btn\" data-class=\"chakan\">查看</a>&nbsp;&nbsp;";

            if (_status == EyouSoft.Model.XiaoXiStatus.未读)
            {
                s = _chaKan + _shiZhiYiDu;
            }

            if (_status == EyouSoft.Model.XiaoXiStatus.已读)
            {
                s = _chaKan;
            }

            return s;

            /*var _s = "<a href=\"javascript:void(0)\" class=\"blue_btn\" data-class=\"chakan\">查看</a>&nbsp;&nbsp;";
            return _s;*/
        }
        #endregion
    }
}
