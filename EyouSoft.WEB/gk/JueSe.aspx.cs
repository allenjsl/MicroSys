//角色管理 汪奇志 2015-06-11
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
    /// 角色管理
    /// </summary>
    public partial class JueSe : GkYeMian
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
                default: break;
            }

            InitRpt();
        }

        #region private members
        /// <summary>
        /// get chaxun
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.MYongHuJueSeChaXunInfo GetChaXunInfo()
        {
            var info = new EyouSoft.Model.MYongHuJueSeChaXunInfo();

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

            var items = new EyouSoft.BLL.BYongHuJueSe().GetJueSes(pageSize, pageIndex, ref recordCount, chaXun);

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
            string txtJueSeId = Utils.GetFormValue("txtJueSeId");

            int bllRetCode = new EyouSoft.BLL.BYongHuJueSe().JueSe_D(YongHuInfo.GongSiId, txtJueSeId);

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
    }
}
