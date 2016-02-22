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

    public partial class ChanPin : GysYeMian
    {
        protected int intPageSize = 20, intRecordCount, intPageIndex = Utils.GetPadingIndex(), T = Utils.GetInt(Utils.GetQueryStringValue("T"));
        protected void Page_Load(object sender, EventArgs e)
        {
            InitPrivs();
            if (Utils.GetQueryStringValue("do")=="delete")this.Delete();
            this.InitData();
        }
        void InitData()
        {
            var l = new BLL.BChanPin().GetChanPins(intPageSize, intPageIndex,ref intRecordCount, this.GetChaXun());
            if (l!=null&&l.Count>0)
            {
                rpt.DataSource = l;
                rpt.DataBind();
            }
        }
        MChanPinChaXunInfo GetChaXun()
        {
            return new MChanPinChaXunInfo()
                {
                    GysId = YongHuInfo.GongSiId,
                    Name = Utils.GetQueryStringValue("txtChanPinName"),
                    FaBuTime1 = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtFaBuTime1")),
                    FaBuTime2 = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtFaBuTime2")),
                    PinPai = Utils.GetQueryStringValue("txtPinPai"),
                    GuiGe = Utils.GetQueryStringValue("txtGuiGe")
                };
        }
        void Delete()
        {
            switch (new BLL.BChanPin().ChanPin_D(YongHuInfo.GongSiId, Utils.GetFormValue("txtChanPinId")))
            {
                case 0:
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
            if (!YongHuInfo.IsPrivs(EyouSoft.Model.GYS_Privs1.产品管理))
            {
                Response.Redirect("/gys/default1.aspx");
            }
        }
    }
}
