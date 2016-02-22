//采购单入库 汪奇志 2015-05-04
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
    /// 采购单入库
    /// </summary>
    public partial class CgdRuKu : CgsYeMian
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
                case "shanchu": ShanChu(); break;
                case "fabu": FaBu(); break;
                default: break;
            }

            InitRpt();
        }

        #region private members
        /// <summary>
        /// get chaxun
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.MCaiGouDanChaXunInfo GetChaXunInfo()
        {
            var info = new EyouSoft.Model.MCaiGouDanChaXunInfo();

            info.CaiGouDanName = Utils.GetQueryStringValue("txtCgdName");
            info.FaBuTime1 = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtFaBuTime1"));
            info.FaBuTime2 = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtFaBuTime2"));
            info.CgsId = YongHuInfo.GongSiId;

            return info;
        }

        /// <summary>
        /// init repeater
        /// </summary>
        void InitRpt()
        {
            pageIndex = Utils.GetPadingIndex();
            var chaXun = GetChaXunInfo();

            var items = new EyouSoft.BLL.BCaiGouDan().GetCaiGouDans(pageSize, pageIndex, ref recordCount, chaXun);

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
        /// 删除
        /// </summary>
        void ShanChu()
        {
            string txtCgdId = Utils.GetFormValue("txtCgdId");

            int bllRetCode = new EyouSoft.BLL.BCaiGouDan().CaiGouDan_D(YongHuInfo.GongSiId, txtCgdId);

            if (bllRetCode == 1) Utils.RCWE_AJAX("1", "操作成功");
            else Utils.RCWE_AJAX("0", "操作失败");
        }

        /// <summary>
        /// 发布
        /// </summary>
        void FaBu()
        {
            string txtCgdId = Utils.GetFormValue("txtCgdId");

            int bllRetCode = new EyouSoft.BLL.BCaiGouDan().FaBu(YongHuInfo.GongSiId, txtCgdId, YongHuInfo.YongHuId);

            if (bllRetCode == 1) Utils.RCWE_AJAX("1", "操作成功");
            else Utils.RCWE_AJAX("0", "操作失败");
        }

        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            if (!YongHuInfo.IsPrivs(EyouSoft.Model.CGS_Privs1.采购管理))
            {
                Response.Redirect("/cgs/default1.aspx");
            }
        }
        #endregion

        #region protected members
        /// <summary>
        /// get caozuo
        /// </summary>
        /// <param name="status">采购单状态</param>
        /// <returns></returns>
        protected string GetCaoZuo(object status)
        {
            string s = string.Empty;
            var _status = (EyouSoft.Model.CaiGouDanStatus)status;

            if (_status == EyouSoft.Model.CaiGouDanStatus.计划采购)
            {
                s = "<a href=\"javascript:void(0)\" class=\"blue_btn\" data-class=\"bianji\">编辑</a> <a href=\"javascript:void(0)\" class=\"blue_btn\" data-class=\"fabu\">下单</a> <a href=\"javascript:void(0)\" class=\"blue_btn\" data-class=\"shanchu\">删除</a>";
            }

            if (_status == EyouSoft.Model.CaiGouDanStatus.已下单)
            {
                s = "<a href=\"javascript:void(0)\" class=\"blue_btn\" data-class=\"chakan\">查看</a>";
            }

            return s;
        }
        #endregion
    }
}
