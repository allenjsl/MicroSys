//系统设置-我的供应商 汪奇志 2015-06-01
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
    /// 系统设置-我的供应商
    /// </summary>
    public partial class WoDeGYS : CgsYeMian
    {
        #region attributes
        protected int pageSize = 20;
        protected int pageIndex = 1;
        protected int recordCount = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            InitPrivs();
            if (Utils.GetQueryStringValue("dotype") == "shezhi") SheZhi(); 

            InitRpt();
        }

        #region private members
        /// <summary>
        /// get chaxun
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.MGongSiGuanXiChaXunInfo GetChaXunInfo()
        {
            var info = new EyouSoft.Model.MGongSiGuanXiChaXunInfo();

            info.GongSiId = YongHuInfo.GongSiId;
            info.LeiXing = EyouSoft.Model.GongSiLeiXing.供应商;
            info.Name = Utils.GetQueryStringValue("txtName");
            if (Utils.GetQueryStringValue("txtIsGuanXi") == "1") info.IsGuanZhu = true;
            if (Utils.GetQueryStringValue("txtIsGuanXi") == "0") info.IsGuanZhu = false;

            return info;
        }

        /// <summary>
        /// init repeater
        /// </summary>
        void InitRpt()
        {
            pageIndex = Utils.GetPadingIndex();
            var chaXun = GetChaXunInfo();

            var items = new EyouSoft.BLL.BGongSi().GetGongSiGuanXis(pageSize, pageIndex, ref recordCount, chaXun);

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
        /// shezhi
        /// </summary>
        void SheZhi()
        {
            var txtGongSiId = Utils.GetFormValue("txtGongSiId");
            var txtIsGuanXi = Utils.GetFormValue("txtIsGuanXi");
            int bllRetCode = 0;
            if (txtIsGuanXi == "1")
            {
                bllRetCode = new EyouSoft.BLL.BGongSi().GuanXi_C(YongHuInfo.GongSiId, txtGongSiId, YongHuInfo.YongHuId);
            }
            else
            {
                bllRetCode=new EyouSoft.BLL.BGongSi().GuanXi_D(YongHuInfo.GongSiId, txtGongSiId);
            }
            
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
    }
}
