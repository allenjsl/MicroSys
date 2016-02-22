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
using EyouSoft.Toolkit;

namespace EyouSoft.WEB.gys
{
    using System.Collections.Generic;

    using EyouSoft.Model;
    using EyouSoft.WEB.ashx;

    public partial class ChanPinEdit : GysYeMian
    {
        protected string ChanPinId = Utils.GetQueryStringValue("chanpinid");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.GetQueryStringValue("do")=="Save")this.Save();
            this.InitData();
        }
        void InitData()
        {
            var m = new BLL.BChanPin().GetInfo(ChanPinId);

            if (m!=null)
            {
                BianMa.Value = m.BianMa;
                Name.Value = m.Name;
                PinPai.Value = m.PinPai;
                GuiGe.Value = m.GuiGe;
                JiaGe1.Value = m.JiaGe1.ToString("F2");
                if (m.JiaGe1 == 0) JiaGe1.Value = string.Empty;
                DanWei.Value = m.JiLiangDanWei;
                JieShao.Value = m.JieShao;
                if (m.FuJians!=null&&m.FuJians.Count>0)
                {
                    IList<uploadfile.MFileInfo> f = m.FuJians.Select(mFuJianInfo => new uploadfile.MFileInfo() { Filepath = mFuJianInfo.Filepath }).ToList();
                    ChanPinTuPian.YuanFiles = f;
                }
                FaBuRen.Value = m.CaoZuoRenName;
                FaBuTime.Value = m.IssueTime.ToString();
            }
            else
            {
                FaBuRen.Value = YongHuInfo.Name;
                FaBuTime.Value = DateTime.Now.ToString();
            }
        }
        MChanPinInfo GetForm()
        {
            return new MChanPinInfo()
                {
                    ChanPinId = ChanPinId,
                    GysId = YongHuInfo.GongSiId,
                    BianMa = Utils.GetFormValue(BianMa.UniqueID),
                    Name = Utils.GetFormValue(Name.UniqueID),
                    PinPai = Utils.GetFormValue(PinPai.UniqueID),
                    GuiGe = Utils.GetFormValue(GuiGe.UniqueID),
                    JiLiangDanWei = Utils.GetFormValue(DanWei.UniqueID),
                    JiaGe1 = Utils.GetDecimal(Utils.GetFormValue(JiaGe1.UniqueID)),
                    JiaGe2 = Utils.GetDecimal(Utils.GetFormValue(JiaGe1.UniqueID)),
                    JieShao = Utils.GetFormValue(JieShao.UniqueID),
                    CaoZuoRenId = YongHuInfo.YongHuId,
                    IssueTime = DateTime.Now,
                    FuJians = this.GetFuJians()
                };
        }
        IList<MFuJianInfo> GetFuJians()
        {
            IList<MFuJianInfo> fujians = new List<MFuJianInfo>();

            var chanpintupians = ChanPinTuPian.Files != null && ChanPinTuPian.Files.Count > 0 ? ChanPinTuPian.Files : ChanPinTuPian.YuanFiles;
            if (chanpintupians != null && chanpintupians.Count > 0)
            {
                foreach (var chanpintupian in chanpintupians)
                {
                    fujians.Add(new MFuJianInfo()
                        {
                            Filepath = chanpintupian.Filepath
                        });
                }
            }
            return fujians;
        }
        void Save()
        {
            var b = new BLL.BChanPin();
            var r = string.IsNullOrEmpty(ChanPinId) ? b.ChanPin_C(this.GetForm()) : b.ChanPin_U(this.GetForm());

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
