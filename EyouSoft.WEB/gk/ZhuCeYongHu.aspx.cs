//注册用户管理 汪奇志 2015-06-12
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Toolkit;

namespace EyouSoft.WEB.gk
{
    /// <summary>
    /// 注册用户管理
    /// </summary>
    public partial class ZhuCeYongHu : GkYeMian
    {
        #region attributes
        protected int pageSize = 20;
        protected int pageIndex = 1;
        protected int recordCount = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            InitPrivs();
            switch (Utils.GetQueryStringValue("dotype"))
            {
                case "shenhe": ShenHe(); break;
                default: break;
            }

            InitRpt();
        }

        #region private members
        /// <summary>
        /// get chaxun
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.MZhuCeYongHuChaXunInfo GetChaXunInfo()
        {
            var info = new EyouSoft.Model.MZhuCeYongHuChaXunInfo();

            info.LaiYuan = EyouSoft.Model.LaiYuan.用户注册;
            info.LeiXing = (EyouSoft.Model.GongSiLeiXing?)Utils.GetEnumValueNullable(typeof(EyouSoft.Model.GongSiLeiXing), Utils.GetQueryStringValue("txtGongSiLeiXing"));
            info.GongSiName = Utils.GetQueryStringValue("txtGongSiName");
            info.ShenHeStatus = (EyouSoft.Model.ShenHeStatus?)Utils.GetEnumValueNullable(typeof(EyouSoft.Model.ShenHeStatus), Utils.GetQueryStringValue("txtShenHeStatus"));
            info.YongHuMing = Utils.GetQueryStringValue("txtYongHuMing");
            info.YongHuName = Utils.GetQueryStringValue("txtYongHuName");           

            return info;
        }

        /// <summary>
        /// init repeater
        /// </summary>
        void InitRpt()
        {
            pageIndex = Utils.GetPadingIndex();
            var chaXun = GetChaXunInfo();

            var items = new EyouSoft.BLL.BYongHu().GetZhuCeYongHus(pageSize, pageIndex, ref recordCount, chaXun);

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
        /// 审核
        /// </summary>
        void ShenHe()
        {
            string txtYongHuId = Utils.GetFormValue("txtYongHuId");
            string txtGongIsId = Utils.GetFormValue("txtGongSiId");

            int bllRetCode = new EyouSoft.BLL.BYongHu().YongHu_ShenHe(txtGongIsId, txtYongHuId,YongHuInfo.YongHuId);

            if (bllRetCode == 1) Utils.RCWE_AJAX("1", "操作成功");
            else Utils.RCWE_AJAX("0", "操作失败");
        }

        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            if (!YongHuInfo.IsPrivs(EyouSoft.Model.GK_Privs1.系统设置))
            {
                Response.Redirect("/gk/default1.aspx");
            }
        }
        #endregion

        #region protected members
        /// <summary>
        /// get caozuo
        /// </summary>
        /// <param name="shenHeStatus"></param>
        /// <returns></returns>
        protected string GetCaoZuo(object shenHeStatus)
        {
            var _shenHeStatus = (EyouSoft.Model.ShenHeStatus)shenHeStatus;

            if (_shenHeStatus == EyouSoft.Model.ShenHeStatus.已审核) return string.Empty;

            return "<a href=\"javascript:void(0)\" class=\"blue_btn\" data-class=\"shenhe\">审核</a>";
        }
        #endregion

    }
}
