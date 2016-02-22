using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Toolkit;

namespace EyouSoft.WEB.cgs
{
    public partial class MoBanEdit : CgsYeMian
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
            IList<EyouSoft.Model.MCaiGouMoBanChanPinInfo> items = new List<EyouSoft.Model.MCaiGouMoBanChanPinInfo>();

            if (string.IsNullOrEmpty(EditId))
            {                
                items.Add(new EyouSoft.Model.MCaiGouMoBanChanPinInfo());
                rpt.DataSource = items;
                rpt.DataBind();

                ltrCaoZuoRenName.Text = YongHuInfo.Name;
                ltrCaoZuoTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

                return;
            }

            var info = new EyouSoft.BLL.BCaiGouMoBan().GetInfo(EditId);

            if (info == null) Utils.RCWE("异常请求");

            txtMoBanName.Value = info.Name;
            ltrCaoZuoRenName.Text = info.CaoZuoRenName;
            ltrCaoZuoTime.Text = info.IssueTime.ToString("yyyy-MM-dd HH:mm");

            items = info.ChanPins;

            if (items == null || items.Count == 0)
            {
                items.Add(new EyouSoft.Model.MCaiGouMoBanChanPinInfo());
            }

            rpt.DataSource = items;
            rpt.DataBind();
        }

        /// <summary>
        /// get form info
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.MCaiGouMoBanInfo GetFormInfo()
        {
            var info = new EyouSoft.Model.MCaiGouMoBanInfo();
            info.CaoZuoRenId = YongHuInfo.YongHuId;
            info.CgsId = YongHuInfo.GongSiId;
            info.ChanPins=new List<EyouSoft.Model.MCaiGouMoBanChanPinInfo>();
            info.IsMoRen = false;
            info.IssueTime = DateTime.Now;
            info.MoBanId = EditId;
            info.Name = Utils.GetFormValue(txtMoBanName.UniqueID);

            var txt_moban_gysid = Utils.GetFormValues("txt_moban_gysid");
            var txt_moban_chanpinid = Utils.GetFormValues("txt_moban_chanpinid");
            var txt_moban_shuliang = Utils.GetFormValues("txt_moban_shuliang");

            if (txt_moban_gysid.Length != txt_moban_chanpinid.Length
                || txt_moban_gysid.Length != txt_moban_shuliang.Length) Utils.RCWE_AJAX("0", "表单异常");

            for (int i = 0; i < txt_moban_gysid.Length; i++)
            {
                var item = new EyouSoft.Model.MCaiGouMoBanChanPinInfo();

                item.GysId = txt_moban_gysid[i];
                item.ChanPinId = txt_moban_chanpinid[i];
                item.ShuLiang = Utils.GetDecimal(txt_moban_shuliang[i]);

                if (string.IsNullOrEmpty(item.GysId) 
                    || string.IsNullOrEmpty(item.ChanPinId)) continue;

                bool isExists = false;
                foreach (var item1 in info.ChanPins)
                {
                    if (item1.ChanPinId == item.ChanPinId) isExists = true;
                }

                if (isExists) continue;

                info.ChanPins.Add(item);
            }

            if (info.ChanPins == null
                || info.ChanPins.Count == 0) Utils.RCWE_AJAX("0", "至少要选择一个有效产品");

            return info;
        }

        /// <summary>
        /// baocun
        /// </summary>
        void BaoCun()
        {
            var info = GetFormInfo();

            int bllRetCode = 0;
            if (string.IsNullOrEmpty(info.MoBanId))
            {
                bllRetCode = new EyouSoft.BLL.BCaiGouMoBan().MoBan_C(info);
            }
            else
            {
                bllRetCode = new EyouSoft.BLL.BCaiGouMoBan().MoBan_U(info);
            }

            if (bllRetCode == 1) Utils.RCWE_AJAX("1", "操作成功");
            else Utils.RCWE_AJAX("0", "操作失败");
        }
        #endregion
    }
}
