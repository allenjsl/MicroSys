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

    public partial class ChanPinJiaGeEdit : GysYeMian
    {
        protected string ChanPinId = Utils.GetQueryStringValue("chanpinid");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.GetQueryStringValue("do") == "Save") this.Save();
            this.InitData();
        }
        void InitData()
        {
            var b = new BLL.BChanPin();
            var m = b.GetInfo(ChanPinId);
            var l = b.GetChanPinJiaGes(ChanPinId);

            if (m != null)
            {
                BianMa.Value = m.BianMa;
                Name.Value = m.Name;
                PinPai.Value = m.PinPai;
                GuiGe.Value = m.GuiGe;
                JiaGe1.Value = m.JiaGe2.ToString("F2");
                DanWei.Value = m.JiLiangDanWei;
                FaBuRen.Value = m.CaoZuoRenName;
                FaBuTime.Value = m.IssueTime.ToString();
            }
            else
            {
                FaBuRen.Value = YongHuInfo.Name;
                FaBuTime.Value = DateTime.Now.ToString();
            }

            if (l!=null&&l.Count>0)
            {
                rpt.DataSource = l;
                rpt.DataBind();
            }
        }
        MChanPinJiaGeInfo GetForm()
        {
            return new MChanPinJiaGeInfo()
            {
                ChanPinId = ChanPinId,
                JiaGe1 = Utils.GetDecimal(Utils.GetFormValue(JiaGe1.UniqueID)),
                JiaGe2 = Utils.GetDecimal(Utils.GetFormValue(JiaGe2.UniqueID)),
                CaoZuoRenId = YongHuInfo.YongHuId,
                IssueTime = DateTime.Now,
                ShuoMing = "价格调整"
            };
        }
        void Save()
        {
            var b = new BLL.BChanPin();
            var r = b.ChanPinJiaGe_C(this.GetForm());

            switch (r)
            {
                default:
                    Utils.RCWE_AJAX("0", "保存失败");
                    break;
                case 1:
                    Utils.RCWE_AJAX("1", "保存成功");
                    break;
                case -99:
                    Utils.RCWE_AJAX("0", "该产品不存在");
                    break;
            }
        }
    }
}
