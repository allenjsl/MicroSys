//常用地址 汪奇志 2015-06-01
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Toolkit;

namespace EyouSoft.WEB.cgs
{
    /// <summary>
    /// 常用地址
    /// </summary>
    public partial class DiZhi : CgsYeMian
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
                case "shezhimoren": SheZhiMoRen(); break;
                default: break;
            }

            InitRpt();
        }

        #region private members
        /// <summary>
        /// get chaxun
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.MDiZhiChaXunInfo GetChaXunInfo()
        {
            var info = new EyouSoft.Model.MDiZhiChaXunInfo();

            info.GongSiId = YongHuInfo.GongSiId;
            info.Name = Utils.GetQueryStringValue("txtName");

            return info;
        }

        /// <summary>
        /// init repeater
        /// </summary>
        void InitRpt()
        {
            pageIndex = Utils.GetPadingIndex();
            var chaXun = GetChaXunInfo();

            var items = new EyouSoft.BLL.BDiZhi().GetDiZhis(pageSize, pageIndex, ref recordCount, chaXun);

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
            string txtDiZhiId = Utils.GetFormValue("txtDiZhiId");

            int bllRetCode = new EyouSoft.BLL.BDiZhi().DiZhi_D(YongHuInfo.GongSiId, txtDiZhiId);

            if (bllRetCode == 1) Utils.RCWE_AJAX("1", "操作成功");
            else Utils.RCWE_AJAX("0", "操作失败");
        }

        /// <summary>
        /// 设置默认
        /// </summary>
        void SheZhiMoRen()
        {
            string txtDiZhiId = Utils.GetFormValue("txtDiZhiId");

            int bllRetCode = new EyouSoft.BLL.BDiZhi().SheZhiMoRen(YongHuInfo.GongSiId, txtDiZhiId);
            if (bllRetCode == 1) Utils.RCWE_AJAX("1", "操作成功");
            else Utils.RCWE_AJAX("0", "操作失败");
        }

        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            if (!YongHuInfo.IsPrivs(EyouSoft.Model.CGS_Privs1.系统设置))
            {
                Response.Redirect("/cgs/default1.aspx");
            }
        }
        #endregion

        #region protected members
        /// <summary>
        /// get caozuo
        /// </summary>
        /// <param name="isMoRen"></param>
        /// <returns></returns>
        protected string GetCaoZuo(object isMoRen)
        {
            string s = string.Empty;
            var _isMoRen = (bool)isMoRen;


            s = "<a href=\"javascript:void(0)\" class=\"blue_btn\" data-class=\"bianji\">修改</a> <a href=\"javascript:void(0)\" class=\"blue_btn\" data-class=\"shanchu\">删除</a>";

            if (!_isMoRen)
            {
                s += "<a href=\"javascript:void(0)\" class=\"blue_btn\" data-class=\"shezhimoren\">设为默认</a>";
            }

            return s;
        }
        #endregion
    }
}
