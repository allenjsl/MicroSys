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
    using System.Collections.Generic;

    using EyouSoft.Model;
    using EyouSoft.Toolkit;

    public partial class CaiGouMoBanEdit : GkYeMian
    {
        protected string MoBanId = Utils.GetQueryStringValue("mobanid"),CgsId=string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.GetQueryStringValue("do") == "Save") this.Save();
            this.InitData();
        }

        void InitData()
        {
            var m = new BLL.BCaiGouMoBan().GetInfo(MoBanId);

            if (m!=null)
            {
                CgsId = m.CgsId;
                Name.Value = m.Name;
                if (m.ChanPins!=null&&m.ChanPins.Count>0)
                {
                    rpt.DataSource = m.ChanPins;
                    rpt.DataBind();
                    ph.Visible = false;
                }
                CaoZuoRen.Value = m.CaoZuoRenName;
                IssueTime.Value = m.IssueTime.ToString();

                txt_gongsi_id.Value = m.CgsId;
                txt_gongsi_name.Value = m.CgsName;
            }
            else
            {
                CaoZuoRen.Value = YongHuInfo.Name;
                IssueTime.Value = DateTime.Now.ToString();
            }
        }

        MCaiGouMoBanInfo GetForm()
        {
            return new MCaiGouMoBanInfo()
                {
                    MoBanId = MoBanId,
                    CgsId = Utils.GetFormValue(txt_gongsi_id.UniqueID),
                    Name = Utils.GetFormValue(Name.UniqueID),
                    CaoZuoRenId = YongHuInfo.YongHuId,
                    IssueTime = DateTime.Now,
                    ChanPins = this.GetChanPins()
                };
        }

        IList<MCaiGouMoBanChanPinInfo> GetChanPins()
        {
            IList<MCaiGouMoBanChanPinInfo> chanpins = new List<MCaiGouMoBanChanPinInfo>();
            var mingxiids = Utils.GetFormValues("MingXiId");
            var chanpinids = Utils.GetFormValues("ChanPinId");
            var shuliangs = Utils.GetFormValues("ShuLiang");
            var gysids = Utils.GetFormValues("GysId");

            if (mingxiids != null && mingxiids.Count() > 0)
            {
                for (int i = 0; i < mingxiids.Count(); i++)
                {
                    if (!string.IsNullOrEmpty(chanpinids[i]))
                    {
                        var isExists = false;
                        foreach (var item in chanpins)
                        {
                            if (item.ChanPinId == chanpinids[i]) isExists = true;
                        }

                        if (isExists) continue;                        

                        chanpins.Add(new MCaiGouMoBanChanPinInfo()
                        {
                            Id = mingxiids[i],
                            ChanPinId = chanpinids[i],
                            GysId = gysids[i],
                            ShuLiang = Utils.GetDecimal(shuliangs[i]),
                        });
                    }
                }
            }
            return chanpins;
        }
        void Save()
        {
            var b = new BLL.BCaiGouMoBan();
            var r = string.IsNullOrEmpty(MoBanId) ? b.MoBan_C(this.GetForm()) : b.MoBan_U(this.GetForm());

            switch (r)
            {
                default:
                    Utils.RCWE_AJAX("0", "保存失败");
                    break;
                case 1:
                    Utils.RCWE_AJAX("1", "保存成功");
                    break;
            }
        }
    }
}
