//用户信息编辑 汪奇志 2015-05-05
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Toolkit;

namespace EyouSoft.WEB.gys
{
    using EyouSoft.Model;
    using System.Text;

    /// <summary>
    /// 用户信息编辑
    /// </summary>
    public partial class YongHuEdit : GysYeMian
    {
        #region attributes
        protected string EditId = string.Empty;

        protected string XingBie = "0";

        protected string JueSeId = "";

        protected YongHuLeiXing T = (YongHuLeiXing)Utils.GetInt(Utils.GetQueryStringValue("T"), 0);

        protected string GongSiId = string.Empty;
        #endregion
       
        protected void Page_Load(object sender, EventArgs e)
        {
            EditId = Utils.GetQueryStringValue("editid");

            if (Utils.GetQueryStringValue("dotype") == "baocun") BaoCun();

            InitJueSe();
            InitInfo();
        }

        #region private members
        /// <summary>
        /// init info
        /// </summary>
        void InitInfo()
        {
            if (string.IsNullOrEmpty(EditId)) return;

            var info = new EyouSoft.BLL.BYongHu().GetInfo(EditId);
            if (info == null) Utils.RCWE_AJAX("0", "异常请求");

            ltrTiShi0.Visible = true;

            txtUsername.Value = info.Username;
            ltrStatus.Text = info.Status.ToString();
            txtName.Value = info.Name;
            XingBie = ((int)info.XingBie).ToString();
            txtBuMenName.Value = info.BuMenName;
            txtZhiWu.Value = info.ZhiWu;
            txtShouJi.Value = info.ShouJi;
            txtDianHua.Value = info.DianHua;
            txtFax.Value = info.Fax;
            txtYouXiang.Value = info.Email;
            txtChuShengRiQi.Value = info.ChuShengRiQi.ToString("yyyy-MM-dd");
            txtRuZhiRiQi.Value = info.RuZhiRiQi.ToString("yyyy-MM-dd");
            txtDiZhi.Value = info.DiZhi;
            GongSiId = info.GongSiId;
            JueSeId = info.JueSeId.Trim();

            #region 照片
            var zhaoPianFiles = new List<EyouSoft.WEB.ashx.uploadfile.MFileInfo>();

            if (!string.IsNullOrEmpty(info.ZhaoPianFilepath))
            {
                var zhaoPianFile = new EyouSoft.WEB.ashx.uploadfile.MFileInfo();
                zhaoPianFile.Filepath = info.ZhaoPianFilepath;
                zhaoPianFiles.Add(zhaoPianFile);

                txtZhaoPian.YuanFiles = zhaoPianFiles;
            }
            #endregion
        }

        /// <summary>
        /// get form info
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.MYongHuInfo GetFormInfo()
        {
            var info = new EyouSoft.Model.MYongHuInfo();

            info.BuMenName = Utils.GetFormValue(txtBuMenName.UniqueID);
            info.CaoZuoRenId = YongHuInfo.YongHuId;
            info.ChuShengRiQi = Utils.GetDateTime(Utils.GetFormValue(txtChuShengRiQi.UniqueID), DateTime.Now);
            info.DianHua = Utils.GetFormValue(txtDianHua.UniqueID);
            info.DiZhi = Utils.GetFormValue(txtDiZhi.UniqueID);
            info.Email = Utils.GetFormValue(txtYouXiang.UniqueID);
            info.Fax = Utils.GetFormValue(txtFax.UniqueID);
            info.GongSiId = YongHuInfo.GongSiId;
            info.IssueTime = DateTime.Now;
            info.JueSeId = string.Empty;
            info.LeiXing = YongHuInfo.LeiXing;
            info.Name = Utils.GetFormValue(txtName.UniqueID);
            info.PasswordMD5 = Utils.GetFormValue("txtMiMa");
            info.RuZhiRiQi = Utils.GetDateTime(Utils.GetFormValue(txtRuZhiRiQi.UniqueID), DateTime.Now);
            info.ShouJi = Utils.GetFormValue(txtShouJi.UniqueID);
            info.Status = EyouSoft.Model.YongHuStatus.启用;
            info.Username = Utils.GetFormValue(txtUsername.UniqueID);
            info.XingBie = Utils.GetEnumValue<EyouSoft.Model.XingBie>(Utils.GetFormValue("txtXingBie"), EyouSoft.Model.XingBie.男);
            info.YongHuId = EditId;
            info.ZhaoPianFilepath = string.Empty;
            info.ZhiWu = Utils.GetFormValue(txtZhiWu.UniqueID);
            info.JueSeId = Utils.GetFormValue("txtJueSe");
            info.LaiYuan = EyouSoft.Model.LaiYuan.平台添加;
            info.ShenHeStatus = EyouSoft.Model.ShenHeStatus.已审核;

            #region 照片
            var zhaoPianFiles = txtZhaoPian.Files;
            if (zhaoPianFiles != null && zhaoPianFiles.Count > 0)
            {
                info.ZhaoPianFilepath = zhaoPianFiles[0].Filepath;
            }
            else
            {
                zhaoPianFiles = txtZhaoPian.YuanFiles;
                if (zhaoPianFiles != null && zhaoPianFiles.Count > 0)
                {
                    info.ZhaoPianFilepath = zhaoPianFiles[0].Filepath;
                }
            }
            #endregion

            if (!string.IsNullOrEmpty(info.PasswordMD5))
            {
                info.PasswordMD5 = Utils.MD5Encrypt(info.PasswordMD5);
            }

            if (string.IsNullOrEmpty(info.YongHuId))
            {
                if (string.IsNullOrEmpty(info.PasswordMD5))
                {
                    Utils.RCWE_AJAX("0", "请输入用户密码");
                }
            }

            return info;
        }

        /// <summary>
        /// baocun
        /// </summary>
        void BaoCun()
        {
            var info = GetFormInfo();
            int bllRetCode = 0;
            if (string.IsNullOrEmpty(info.YongHuId))
            {
                bllRetCode = new EyouSoft.BLL.BYongHu().YongHu_C(info);
            }
            else
            {
                bllRetCode = new EyouSoft.BLL.BYongHu().YongHu_U(info);
            }

            if (bllRetCode == 1) Utils.RCWE_AJAX("1", "操作成功");
            if (bllRetCode == -99) Utils.RCWE_AJAX("0", "操作失败：存在相同的用户名");
            else Utils.RCWE_AJAX("0", "操作失败");
        }

        /// <summary>
        /// init juese
        /// </summary>
        void InitJueSe()
        {
            var chaXun = new EyouSoft.Model.MYongHuJueSeChaXunInfo();
            chaXun.GongSiId = YongHuInfo.GongSiId;

            StringBuilder s = new StringBuilder();
            var items = new EyouSoft.BLL.BYongHuJueSe().GetJueSes(chaXun);

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    s.AppendFormat("<option value=\"{0}\">{1}</option>", item.JueSeId, item.Name);
                }
            }

            ltrJueSe.Text = s.ToString();
        }
        #endregion
    }
}
