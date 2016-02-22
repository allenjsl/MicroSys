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

namespace EyouSoft.WEB.CommonPage
{
    using EyouSoft.Model;
    using EyouSoft.Toolkit;

    public partial class ChanPinXuanYong : System.Web.UI.Page
    {
        #region attributes
        protected int pageSize = 10;
        protected int pageIndex = 1;
        protected int recordCount = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            this.InitData();
        }
        void InitData()
        {
            var l = new BLL.BChanPin().GetChanPins(pageSize, pageIndex, ref recordCount, this.GetChaXun());

            if (l != null && l.Count > 0)
            {
                rpt.DataSource = l;
                rpt.DataBind();
                ph.Visible = false;
            }
        }

        MChanPinChaXunInfo GetChaXun()
        {
            var info= new MChanPinChaXunInfo()
            {
                Name = Utils.GetQueryStringValue("chanpin"),
                GysId = Utils.GetQueryStringValue("gysid"),
                GysName = Utils.GetQueryStringValue("gys")
            };

            EyouSoft.Model.SSO.MYongHuInfo yongHuInfo = null;

            if (EyouSoft.Security.Membership.YongHuProvider.IsLogin(out yongHuInfo))
            {
                if (yongHuInfo.LeiXing == YongHuLeiXing.采购商)
                    info.CgsId = yongHuInfo.GongSiId;
            }

            return info;
        }
    }
}
