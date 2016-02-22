//用户注册 汪奇志 2015-06-12
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Toolkit;

namespace EyouSoft.WEB
{
    /// <summary>
    /// 用户注册
    /// </summary>
    public partial class ZhuCe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (Utils.GetQueryStringValue("dotype") == "zhuce") YongHuZhuCe();
        }

        #region private members

        /// <summary>
        /// get form info
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.MZhuCeYongHuInfo GetFormInfo()
        {
            var info = new EyouSoft.Model.MZhuCeYongHuInfo();
            info.FaRenName = Utils.GetFormValue("txt_farenname");
            info.FuZeRenDianHua = Utils.GetFormValue("txt_fuzerendianhua");
            info.FuZeRenName = Utils.GetFormValue("txt_fuzerenname");
            info.GongSiId = string.Empty;
            info.GongSiName = Utils.GetFormValue("txt_gongsiname");
            info.IssueTime = DateTime.Now;
            info.LaiYuan = EyouSoft.Model.LaiYuan.用户注册;
            info.LeiXing = EyouSoft.Model.GongSiLeiXing.采购商;
            info.PasswordMD5 = Utils.GetFormValue("txt_mima");
            info.ShenHeStatus = EyouSoft.Model.ShenHeStatus.未审核;
            info.YongHuId = string.Empty;
            info.YongHuMing = Utils.GetFormValue("txt_yonghuming");
            info.DiZhi = Utils.GetFormValue("txt_dizhi");

            if (Utils.GetQueryStringValue("leixing") == "gys") info.LeiXing = EyouSoft.Model.GongSiLeiXing.供应商;

            if (string.IsNullOrEmpty(info.GongSiName)) Utils.RCWE_AJAX("0", "请输入单位名称");
            if (string.IsNullOrEmpty(info.YongHuMing)) Utils.RCWE_AJAX("0", "请输入用户名");
            if (string.IsNullOrEmpty(info.PasswordMD5)) Utils.RCWE_AJAX("0", "请输入密码");

            return info;
        }

        /// <summary>
        /// yonghu zhuce
        /// </summary>
        void YongHuZhuCe()
        {
            var info = GetFormInfo();
            int bllRetCode = new EyouSoft.BLL.BYongHu().YongHu_ZhuCe(info);

            if (bllRetCode == 1) { Utils.RCWE_AJAX("1", "注册成功，请及时登录系统完善公司信息以便审核。"); }
            else if (bllRetCode == -99) { Utils.RCWE_AJAX("0", "注册失败：已经存在的单位名称。"); }
            else if (bllRetCode == -98) { Utils.RCWE_AJAX("0", "注册失败：已经存在的用户名。"); }
            else { Utils.RCWE_AJAX("0", "注册失败，请重试。"); }
        }
        #endregion
    }
}
