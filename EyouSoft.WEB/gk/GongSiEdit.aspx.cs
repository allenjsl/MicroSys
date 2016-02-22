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
    using EyouSoft.WEB.ashx;
    using System.Collections.Generic;

    public partial class GongSiEdit : GkYeMian
    {
        #region attributes
        protected string GongSiId = Utils.GetQueryStringValue("gongsiid");
        protected GongSiLeiXing T = (GongSiLeiXing)Utils.GetInt(Utils.GetQueryStringValue("T"), 0);
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.GetQueryStringValue("do") == "Save") this.Save();

            if (Utils.GetQueryStringValue("dotype") == "shenhe") ShenHe();

            this.InitData();
        }

        #region private members
        void InitData()
        {
            var m = new BLL.BGongSi().GetInfo(GongSiId);

            if (m != null)
            {
                T = m.LeiXing;
                Name.Value = m.Name;
                FanRenName.Value = m.FanRenName;
                DiZhi.Value = m.DiZhi;
                IList<uploadfile.MFileInfo> f = new List<uploadfile.MFileInfo>();
                if (!string.IsNullOrEmpty(m.YingYeZhiZhaoFilepath))
                {
                    f.Add(new uploadfile.MFileInfo() { Filepath = m.YingYeZhiZhaoFilepath });
                    YingYeZhiZhaoFilepath.YuanFiles = f;
                }
                f.Clear();
                if (!string.IsNullOrEmpty(m.ZuZhiJiGouFilepath))
                {
                    f.Add(new uploadfile.MFileInfo() { Filepath = m.ZuZhiJiGouFilepath });
                    ZuZhiJiGouFilepath.YuanFiles = f;
                }
                FuZeRenName.Value = m.FuZeRenName;
                FuZeRenDianHua.Value = m.FuZeRenDianHua;
                FuZeRenShenFenZhengHao.Value = m.FuZeRenShenFenZhengHao;
                f.Clear();
                if (!string.IsNullOrEmpty(m.FuZeRenZhaoPianFilepath))
                {
                    f.Add(new uploadfile.MFileInfo() { Filepath = m.FuZeRenZhaoPianFilepath });
                    FuZeRenZhaoPianFilepath.YuanFiles = f;
                }
                CaiWuName.Value = m.CaiWuName;
                CaiWuDianHua.Value = m.CaiWuDianHua;
                CaiWuShenFenZhengHao.Value = m.CaiWuShenFenZhengHao;
                f.Clear();
                if (!string.IsNullOrEmpty(m.CaiWuZhaoPianFilepath))
                {
                    f.Add(new uploadfile.MFileInfo() { Filepath = m.CaiWuZhaoPianFilepath });
                    CaiWuZhaoPianFilepath.YuanFiles = f;
                }
                CaoZuoRen.Value = m.CaoZuoRenName;
                IssueTime.Value = m.IssueTime.ToString("yyyy-MM-dd HH:mm");
                txtLxQQ.Value = m.LxQQ;

                phShenHe.Visible = m.ShenHeStatus == ShenHeStatus.未审核;
            }
            else
            {
                CaoZuoRen.Value = YongHuInfo.Name;
                IssueTime.Value = DateTime.Now.ToString();
            }
        }

        MGongSiInfo GetForm()
        {
            return new MGongSiInfo()
            {
                GongSiId = GongSiId,
                LeiXing = T,
                Name = Utils.GetFormValue(Name.UniqueID),
                FanRenName = Utils.GetFormValue(FanRenName.UniqueID),
                DiZhi = Utils.GetFormValue(DiZhi.UniqueID),
                YingYeZhiZhaoFilepath = this.GetFuJians(YingYeZhiZhaoFilepath),
                ZuZhiJiGouFilepath = this.GetFuJians(ZuZhiJiGouFilepath),
                FuZeRenName = Utils.GetFormValue(FuZeRenName.UniqueID),
                FuZeRenDianHua = Utils.GetFormValue(FuZeRenDianHua.UniqueID),
                FuZeRenShenFenZhengHao = Utils.GetFormValue(FuZeRenShenFenZhengHao.UniqueID),
                FuZeRenZhaoPianFilepath = this.GetFuJians(FuZeRenZhaoPianFilepath),
                CaiWuName = Utils.GetFormValue(CaiWuName.UniqueID),
                CaiWuDianHua = Utils.GetFormValue(CaiWuDianHua.UniqueID),
                CaiWuShenFenZhengHao = Utils.GetFormValue(CaiWuShenFenZhengHao.UniqueID),
                CaiWuZhaoPianFilepath = this.GetFuJians(CaiWuZhaoPianFilepath),
                CaoZuoRenId = YongHuInfo.YongHuId,
                IssueTime = DateTime.Now,
                LxQQ = Utils.GetFormValue(txtLxQQ.UniqueID),
                LaiYuan = LaiYuan.平台添加,
                ShenHeStatus = ShenHeStatus.已审核
            };
        }

        string GetFuJians(WEB.wuc.ShangChuan02 p)
        {
            IList<MFuJianInfo> fujians = new List<MFuJianInfo>();

            var chanpintupians = p.Files != null && p.Files.Count > 0 ? p.Files : p.YuanFiles;
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
            return fujians.Count > 0 ? fujians[0].Filepath : string.Empty;
        }
        void Save()
        {
            var b = new BLL.BGongSi();
            var r = string.IsNullOrEmpty(GongSiId) ? b.GongSi_C(this.GetForm()) : b.GongSi_U(this.GetForm());

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

        /// <summary>
        /// shen he
        /// </summary>
        void ShenHe()
        {
            string txtYongHuId = Utils.GetFormValue("txtYongHuId");
            string txtGongIsId = Utils.GetFormValue("txtGongSiId");

            int bllRetCode = new EyouSoft.BLL.BYongHu().YongHu_ShenHe(txtGongIsId, txtYongHuId, YongHuInfo.YongHuId);

            if (bllRetCode == 1) Utils.RCWE_AJAX("1", "操作成功");
            else Utils.RCWE_AJAX("0", "操作失败");
        }
        #endregion
    }
}
