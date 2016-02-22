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
    using System.Text;

    using EyouSoft.Model;
    using EyouSoft.Toolkit;

    public partial class Default : GysYeMian
    {
        protected int intPageSize = 20, intRecordCount, intPageIndex = Utils.GetPadingIndex(),T=Utils.GetInt(Utils.GetQueryStringValue("T"));
        protected void Page_Load(object sender, EventArgs e)
        {
            InitPrivs();
            this.InitData();
        }

        void InitData()
        {
            var l = new BLL.BDingDan().GetDingDans(intPageSize, intPageIndex, ref intRecordCount, this.GetChaXun());
            if (l != null && l.Count > 0)
            {
                rpt.DataSource = l;
                rpt.DataBind();
            }
        }

        MDingDanChaXunInfo GetChaXun()
        {
            int[] s = null;
            switch (T)
            {
                case 0:
                    s = new int[5];
                    s[0] = (int)DingDanStatus.采购申请;
                    s[1] = (int)DingDanStatus.供应商完成报价;
                    s[2] = (int)DingDanStatus.采购商确认报价;
                    s[3] = (int)DingDanStatus.供应商发货完成;
                    s[4] = (int)DingDanStatus.采购商确认收货;
                    break;
                case 1:case 3:
                    s = new int[3];
                    s[0] = (int)DingDanStatus.采购商确认报价;
                    s[1] = (int)DingDanStatus.供应商发货完成;
                    s[2] = (int)DingDanStatus.采购商确认收货;
                    break;
                case 2:case 4:
                    s = new int[1];
                    s[0] = (int)DingDanStatus.采购商确认收货;
                    break;
            }

            return new MDingDanChaXunInfo()
                {
                    CaiGouDanHao = Utils.GetQueryStringValue("txtCaiGouDanHao"),
                    CaiGouTime1 = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtFaBuTime1")),
                    CaiGouTime2 = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtFaBuTime2")),                    
                    CgsName=Utils.GetQueryStringValue("txtCgsName"),
                    DingDanStatusIn = s,
                    GysId = YongHuInfo.GongSiId
                };
        }

        protected string GetLianXiFangShi(object yonghuid)
        {
            var m = new BLL.BYongHu().GetInfo(yonghuid.ToString());

            if (m!=null)
            {
                return string.IsNullOrEmpty(m.DianHua) ? m.ShouJi : m.DianHua;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            if (T == 0 || T == 1 || T == 2)
            {
                if (!YongHuInfo.IsPrivs(EyouSoft.Model.GYS_Privs1.供货管理))
                {
                    Response.Redirect("/gys/default1.aspx");
                }
            }

            if (T ==3 || T == 4 )
            {
                if (!YongHuInfo.IsPrivs(EyouSoft.Model.GYS_Privs1.消息中心))
                {
                    Response.Redirect("/gys/default1.aspx");
                }
            }
        }
    }
}
