//送货人 汪奇志 2015-06-05
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Toolkit;

namespace EyouSoft.WEB.gys
{
    /// <summary>
    /// 送货人
    /// </summary>
    public partial class SongHuoRenEdit : GysYeMian
    {
        #region attributes
        protected string EditId = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            EditId = Utils.GetQueryStringValue("editid");

            if (Utils.GetQueryStringValue("dotype") == "baocun") BaoCun();

            InitInfo();
        }

        #region private members
        /// <summary>
        /// init info
        /// </summary>
        void InitInfo()
        {
            if (string.IsNullOrEmpty(EditId)) return;

            var info = new EyouSoft.BLL.BDiZhi().GetInfo(EditId);
            if (info == null) Utils.RCWE_AJAX("0", "异常请求");

            txtDianHua.Value = info.DianHua;
            txtName.Value = info.Name;
            txtShouJi.Value = info.ShouJi;
        }

        /// <summary>
        /// get form info
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.MDiZhiInfo GetFormInfo()
        {
            var info = new EyouSoft.Model.MDiZhiInfo();

            info.CaoZuoRenId = YongHuInfo.YongHuId;
            info.DianHua = Utils.GetFormValue(txtDianHua.UniqueID);
            info.DiZhi = string.Empty;
            info.DiZhiId = EditId;
            info.GongSiId = YongHuInfo.GongSiId;
            info.IsMoRen = false;
            info.IssueTime = DateTime.Now;
            info.Name = Utils.GetFormValue(txtName.UniqueID);
            info.ShouJi = Utils.GetFormValue(txtShouJi.UniqueID);
            info.YongHuId = YongHuInfo.YongHuId;

            return info;
        }

        /// <summary>
        /// baocun
        /// </summary>
        void BaoCun()
        {
            var info = GetFormInfo();
            int bllRetCode = 0;
            if (string.IsNullOrEmpty(info.DiZhiId))
            {
                bllRetCode = new EyouSoft.BLL.BDiZhi().DiZhi_C(info);
            }
            else
            {
                bllRetCode = new EyouSoft.BLL.BDiZhi().DiZhi_U(info);
            }

            if (bllRetCode == 1) Utils.RCWE_AJAX("1", "操作成功");
            else Utils.RCWE_AJAX("0", "操作失败");
        }
        #endregion
    }
}
