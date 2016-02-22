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

namespace EyouSoft.WEB.gk
{
    using EyouSoft.Model;
    using EyouSoft.Toolkit;

    public partial class Default : GkYeMian
    {
        #region attributes
        protected int intPageSize = 20, intRecordCount, intPageIndex = Utils.GetPadingIndex();
        protected GongSiLeiXing T = (GongSiLeiXing)Utils.GetInt(Utils.GetQueryStringValue("T"), 0);
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            InitPrivs();
            if(Utils.GetQueryStringValue("do")=="delete")this.Delete();
            this.InitData();
        }

        void InitData()
        {
            var l = new BLL.BGongSi().GetGongSis(intPageSize, intPageIndex, ref intRecordCount, this.GetChaXun());
            if (l!=null&&l.Count>0)
            {
                rpt.DataSource = l;
                rpt.DataBind();
            }
        }
        MGongSiChaXunInfo GetChaXun()
        {
            return new MGongSiChaXunInfo()
                {
                    Name = Utils.GetQueryStringValue("GongSiName"),
                    LeiXing = T,
                    ShenHeStatus=EyouSoft.Model.ShenHeStatus.已审核
                };
        }
        void Delete()
        {
            switch (new BLL.BGongSi().GongSi_D(Utils.GetQueryStringValue("gongsiid")))
            {
                default:
                    Utils.RCWE_AJAX("0", "删除失败"); break;
                case 1:
                    Utils.RCWE_AJAX("1", "删除成功"); break;
            }
        }

        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            if (T == GongSiLeiXing.采购商)
            {
                if (!YongHuInfo.IsPrivs(EyouSoft.Model.GK_Privs1.采购商管理))
                {
                    Response.Redirect("/gk/default1.aspx");
                }
            }
            else 
            {
                if (!YongHuInfo.IsPrivs(EyouSoft.Model.GK_Privs1.供应商管理))
                {
                    Response.Redirect("/gk/default1.aspx");
                }
            }
        }
    }
}
