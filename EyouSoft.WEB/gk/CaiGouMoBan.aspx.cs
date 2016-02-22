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

    public partial class CaiGouMoBan : GkYeMian
    {
        #region attributes
        protected int pageSize = 20;
        protected int pageIndex = 1;
        protected int recordCount = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            switch (Utils.GetQueryStringValue("dotype"))
            {
                case "shanchu": ShanChu(); break;
                default: break;
            }
            this.InitData();
        }
        void InitData()
        {
            var l = new BLL.BCaiGouMoBan().GetMoBans(pageSize, pageIndex, ref recordCount, this.GetChaXun());

            if (l!=null&&l.Count>0)
            {
                rpt.DataSource = l;
                rpt.DataBind();
                phEmpty.Visible = false;
            }
        }
        MCaiGouMoBanChaXunInfo GetChaXun()
        {
            return new MCaiGouMoBanChaXunInfo()
                {
                    Name = Utils.GetQueryStringValue("txtName")
                };
        }
        /// <summary>
        /// 删除
        /// </summary>
        void ShanChu()
        {
            string txtYongHuId = Utils.GetFormValue("txtYongHuId");

            int bllRetCode = new EyouSoft.BLL.BCaiGouMoBan().MoBan_D(YongHuInfo.GongSiId, txtYongHuId);

            if (bllRetCode == 1) Utils.RCWE_AJAX("1", "操作成功");
            else Utils.RCWE_AJAX("0", "操作失败");
        }
    }
}
