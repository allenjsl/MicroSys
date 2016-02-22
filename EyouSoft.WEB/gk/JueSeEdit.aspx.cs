//角色管理 汪奇志 2015-06-11
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Toolkit;

namespace EyouSoft.WEB.gk
{
    /// <summary>
    /// 角色管理
    /// </summary>
    public partial class JueSeEdit : GkYeMian
    {
        #region attributes
        protected string EditId = string.Empty;

        protected string JueSePrivs = "";
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            EditId = Utils.GetQueryStringValue("editid");

            if (Utils.GetQueryStringValue("dotype") == "baocun") BaoCun();

            InitInfo();
            InitRpt();
        }

        #region private members
        /// <summary>
        /// init info
        /// </summary>
        void InitInfo()
        {
            if (string.IsNullOrEmpty(EditId)) return;

            var info = new EyouSoft.BLL.BYongHuJueSe().GetInfo(EditId);
            if (info == null) Utils.RCWE_AJAX("0", "异常请求");

            txtName.Value = info.Name;

            if (info.Privs != null && info.Privs.Count > 0)
            {
                foreach (var item in info.Privs)
                {
                    JueSePrivs += item + ",";
                }
            }
        }

        /// <summary>
        /// inpt repeater
        /// </summary>
        void InitRpt()
        {
            var items = EyouSoft.Toolkit.MeiJuHelper.GetList(typeof(EyouSoft.Model.GK_Privs1));

            rpt.DataSource = items;
            rpt.DataBind();
        }

        /// <summary>
        /// get form info
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.MYongHuJueSeInfo GetFormInfo()
        {
            var info = new EyouSoft.Model.MYongHuJueSeInfo();

            info.CaoZuoRenId = YongHuInfo.YongHuId;
            info.GongSiId = YongHuInfo.GongSiId;
            info.IssueTime = DateTime.Now;
            info.JueSeId = EditId;
            info.MiaoShu = string.Empty;
            info.Name = Utils.GetFormValue(txtName.UniqueID);
            info.Privs = new List<int>();
            info.Status = EyouSoft.Model.YongHuJueSeStatus.可用;

            var chkPrivs1 = Utils.GetFormValues("chkPrivs1");

            if (chkPrivs1 != null && chkPrivs1.Length > 0)
            {
                for (var i = 0; i < chkPrivs1.Length; i++)
                {
                    info.Privs.Add(Utils.GetInt(chkPrivs1[i]));
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
            if (string.IsNullOrEmpty(info.JueSeId))
            {
                bllRetCode = new EyouSoft.BLL.BYongHuJueSe().JueSe_C(info);
            }
            else
            {
                bllRetCode = new EyouSoft.BLL.BYongHuJueSe().JueSe_U(info);
            }

            if (bllRetCode == 1) Utils.RCWE_AJAX("1", "操作成功");
            else Utils.RCWE_AJAX("0", "操作失败");
        }
        #endregion
    }
}
