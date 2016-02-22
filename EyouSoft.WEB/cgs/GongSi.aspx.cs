//公司信息管理 汪奇志 2015-06-24
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Toolkit;

namespace EyouSoft.WEB.cgs
{
    /// <summary>
    /// 公司信息管理
    /// </summary>
    public partial class GongSi : CgsYeMian
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.GetQueryStringValue("dotype") == "baocun") BaoCun();

            InitInfo();
        }

        #region private members
        /// <summary>
        /// init info
        /// </summary>
        void InitInfo()
        {
            var info = new EyouSoft.BLL.BGongSi().GetInfo(YongHuInfo.GongSiId);

            if (info == null) Utils.RCWE_AJAX("0", "表单异常，请刷新后重试");

            txtCaiWuDianHua.Value = info.CaiWuDianHua;
            txtCaiWuName.Value = info.CaiWuName;
            txtCaiWuShenFenZhengHao.Value = info.CaiWuShenFenZhengHao;
            txtDiZhi.Value = info.DiZhi;
            txtFaRenName.Value = info.FanRenName;
            txtFuZeRenDianHua.Value = info.FuZeRenDianHua;
            txtFuZeRenName.Value = info.FuZeRenName;
            txtFuZeRenShenFenZhengHao.Value = info.FuZeRenShenFenZhengHao;
            txtLxQQ.Value = info.LxQQ;
            txtGongSiName.Value = info.Name;

            if (!string.IsNullOrEmpty(info.CaiWuZhaoPianFilepath))
            {
                var items=new List<EyouSoft.Model.MFuJianInfo>();
                items.Add(new EyouSoft.Model.MFuJianInfo(){ Filepath=info.CaiWuZhaoPianFilepath});
                txtCaiWuZhaoPian.YuanFiles1 = items;
            }

            if (!string.IsNullOrEmpty(info.FuZeRenZhaoPianFilepath))
            {
                var items = new List<EyouSoft.Model.MFuJianInfo>();
                items.Add(new EyouSoft.Model.MFuJianInfo() { Filepath = info.FuZeRenZhaoPianFilepath });
                txtFuZeRenZhaoPian.YuanFiles1 = items;
            }

            if (!string.IsNullOrEmpty(info.YingYeZhiZhaoFilepath))
            {
                var items = new List<EyouSoft.Model.MFuJianInfo>();
                items.Add(new EyouSoft.Model.MFuJianInfo() { Filepath = info.YingYeZhiZhaoFilepath });
                txtYingYeZhiZhao.YuanFiles1 = items;
            }

            if (!string.IsNullOrEmpty(info.ZuZhiJiGouFilepath))
            {
                var items = new List<EyouSoft.Model.MFuJianInfo>();
                items.Add(new EyouSoft.Model.MFuJianInfo() { Filepath = info.ZuZhiJiGouFilepath });
                txtZuZhiJiGou.YuanFiles1 = items;
            }

            if (info.ShenHeStatus == EyouSoft.Model.ShenHeStatus.未审核)
            {
                if (string.IsNullOrEmpty(info.YingYeZhiZhaoFilepath) || string.IsNullOrEmpty(info.ZuZhiJiGouFilepath))
                {
                    ltrTiShiXiaoXi.Text = "您的公司信息尚未审核，请尽快上传营业执照和组织机构代码以便审核。";
                }
                else
                {
                    ltrTiShiXiaoXi.Text = "您的公司信息正在审核中....";
                }
            }
        }

        /// <summary>
        /// get form info
        /// </summary>
        EyouSoft.Model.MGongSiInfo GetFormInfo()
        {
            var info = new EyouSoft.BLL.BGongSi().GetInfo(YongHuInfo.GongSiId);

            if (info == null) { Utils.RCWE_AJAX("0", "表单异常，请刷新后重试"); }

            info.CaiWuDianHua = Utils.GetFormValue(txtCaiWuDianHua.UniqueID);
            info.CaiWuName = Utils.GetFormValue(txtCaiWuName.UniqueID);
            info.CaiWuShenFenZhengHao = Utils.GetFormValue(txtCaiWuShenFenZhengHao.UniqueID);
            info.CaiWuZhaoPianFilepath = null;            
            info.CaoZuoRenId = YongHuInfo.YongHuId;
            info.ChengShiId = 0;
            info.DiZhi = Utils.GetFormValue(txtDiZhi.UniqueID);
            info.FanRenName = Utils.GetFormValue(txtFaRenName.UniqueID);
            info.FuZeRenDianHua = Utils.GetFormValue(txtFuZeRenDianHua.UniqueID);
            info.FuZeRenName = Utils.GetFormValue(txtFuZeRenName.UniqueID);
            info.FuZeRenShenFenZhengHao = Utils.GetFormValue(txtFuZeRenShenFenZhengHao.UniqueID);
            info.FuZeRenZhaoPianFilepath = null;
            info.GongSiId = YongHuInfo.GongSiId;
            info.IssueTime = DateTime.Now;
            info.LxQQ = Utils.GetFormValue(txtLxQQ.UniqueID);
            info.Name = Utils.GetFormValue(txtGongSiName.UniqueID);
            info.ShengFenId = 0;
            info.YingYeZhiZhaoFilepath = null;
            info.ZuZhiJiGouFilepath = null;

            var caiWuZhaoPianItems = txtCaiWuZhaoPian.Files2;
            if (caiWuZhaoPianItems != null && caiWuZhaoPianItems.Count > 0) info.CaiWuZhaoPianFilepath = caiWuZhaoPianItems[0].Filepath;

            var fuZeRenZhaoPianItems = txtFuZeRenZhaoPian.Files2;
            if (fuZeRenZhaoPianItems != null && fuZeRenZhaoPianItems.Count > 0) info.FuZeRenZhaoPianFilepath = fuZeRenZhaoPianItems[0].Filepath;

            var yingYeZhiZhaoItems = txtYingYeZhiZhao.Files2;
            if (yingYeZhiZhaoItems != null && yingYeZhiZhaoItems.Count > 0) info.YingYeZhiZhaoFilepath = yingYeZhiZhaoItems[0].Filepath;

            var zuZhiJiGouItems = txtZuZhiJiGou.Files2;
            if (zuZhiJiGouItems != null && zuZhiJiGouItems.Count > 0) info.ZuZhiJiGouFilepath = zuZhiJiGouItems[0].Filepath;

            if (string.IsNullOrEmpty(info.YingYeZhiZhaoFilepath)) Utils.RCWE_AJAX("0", "请上传营业执照");
            if (string.IsNullOrEmpty(info.ZuZhiJiGouFilepath)) Utils.RCWE_AJAX("0", "请上传组织机构代码");

            return info;
        }

        /// <summary>
        /// baocun
        /// </summary>
        void BaoCun()
        {
            var info = GetFormInfo();

            int bllRetCode = new EyouSoft.BLL.BGongSi().GongSi_U(info);

            if (bllRetCode == 1) Utils.RCWE_AJAX("1", "操作成功");
            else Utils.RCWE_AJAX("0", "操作失败");
        }
        #endregion
    }
}
