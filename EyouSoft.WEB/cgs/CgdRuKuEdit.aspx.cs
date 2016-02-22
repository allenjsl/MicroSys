//采购单新增、修改 汪奇志 2015-05-06
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Toolkit;
using System.Text;

namespace EyouSoft.WEB.cgs
{
    /// <summary>
    /// 采购单新增、修改
    /// </summary>
    public partial class CgdRuKuEdit : CgsYeMian
    {
        #region attributes
        /// <summary>
        /// 默认模板编号
        /// </summary>
        protected string MoRenMoBanId = string.Empty;
        protected string EditId = string.Empty;

        protected string MoBanId = string.Empty;
        /// <summary>
        /// 采购单状态
        /// </summary>
        protected string CgdStatus = "0";

        protected string ShouHuoDiZhiId = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            EditId = Utils.GetQueryStringValue("editid");

            switch (Utils.GetQueryStringValue("dotype"))
            {
                case "getmobaninfo": GetMoBanInfo(); break;
                case "baocun": BaoCun(); break;
                default: break;
            }

            InitMoBans();
            InitInfo();
            InitDiZhiRpt();
        }

        #region private members
        /// <summary>
        /// 初始化模板信息
        /// </summary>
        void InitMoBans()
        {
            var chaXun = new EyouSoft.Model.MCaiGouMoBanChaXunInfo();
            chaXun.CgsId = YongHuInfo.GongSiId;

            var items = new EyouSoft.BLL.BCaiGouMoBan().GetMoBans(chaXun);

            StringBuilder s = new StringBuilder();
            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    s.AppendFormat("<option value=\"{0}\">{1}</option>",item.MoBanId,item.Name);

                    if (item.IsMoRen) MoRenMoBanId = item.MoBanId;
                }
            }

            ltrMoBanOption.Text = s.ToString();
        }

        /// <summary>
        /// get moban info
        /// </summary>
        void GetMoBanInfo()
        {
            string moBanId = Utils.GetQueryStringValue("mobanid");
            if (string.IsNullOrEmpty(moBanId)) Utils.RCWE_AJAX("0","",null);
            var info = new EyouSoft.BLL.BCaiGouMoBan().GetInfo(moBanId);
            if (info == null) Utils.RCWE_AJAX("0", "", null);
            Utils.RCWE_AJAX("1", "", info);
        }

        /// <summary>
        /// init info
        /// </summary>
        void InitInfo()
        {
            if (string.IsNullOrEmpty(EditId))
            {
                //txtFaBuRenName.Value = YongHuInfo.Name;
                //txtFaBuTime.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

                ltrFaBuRenName.Text = YongHuInfo.Name;
                ltrFaBuTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                ltrCanGouBuMenName.Text = YongHuInfo.BuMenName;

                return;
            }

            var info = new EyouSoft.BLL.BCaiGouDan().GetInfo(EditId);
            if (info == null) Utils.RCWE_AJAX("0", "异常请求");

            txtCaiGouDanName.Value = info.CaiGouDanName;

            if (info.YaoQiuDaoHuoTime.HasValue)
            {
                txtYaoQiuDaoHuoTime.Value = info.YaoQiuDaoHuoTime.Value.ToString("yyyy-MM-dd");
            }
            ltrCgdStatus.Text = info.Status.ToString();
            ltrFaBuRenName.Text = info.CaoZuoRenName;
            ltrFaBuTime.Text = info.IssueTime.ToString("yyyy-MM-dd HH:mm");
            ltrCanGouBuMenName.Text = info.CaiGouBuMen;
            CgdStatus = ((int)info.Status).ToString();

            txtShouHuoRenName.Value = info.ShouHuoRenName;
            txtShouHuoRenDianHua.Value = info.ShouHuoRenDianHua;
            txtShouHuoDiZhi.Value = info.ShouHuoDiZhi;

            if (info.ChanPins != null && info.ChanPins.Count > 0)
            {
                rpt.DataSource = info.ChanPins;
                rpt.DataBind();
            }

            if (info.Status == EyouSoft.Model.CaiGouDanStatus.已下单)
            {
                phBaoCun.Visible = false;

                ltrCaoZuoTiShi.Text = "采购单已发布，若需要调整采购单内容，请取消相应采购单重新采购。";
            }

            ShouHuoDiZhiId = info.ShouHuoDiZhiId;
        }

        /// <summary>
        /// get form info
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.MCaiGouDanInfo GetFormInfo()
        {
            var info = new EyouSoft.Model.MCaiGouDanInfo();
            info.CaiGouBuMen = YongHuInfo.BuMenName;
            info.CaiGouDanHao = string.Empty;
            info.CaiGouDanId = EditId;
            info.CaiGouDanName = Utils.GetFormValue(txtCaiGouDanName.UniqueID);
            info.CaiGouDanShuoMing = string.Empty;
            info.CaoZuoRenId = YongHuInfo.YongHuId;
            info.CaoZuoRenName = string.Empty;
            info.CgsId = YongHuInfo.GongSiId;
            info.ChanPins = new List<EyouSoft.Model.MCaiGouDanChanPinInfo>();
            info.FaBuRenId = string.Empty;
            info.FaBuRenName = string.Empty;
            info.FaBuTime = DateTime.Now;
            info.IssueTime = DateTime.Now;
            info.MoBanId = Utils.GetFormValue("txtMoBan");
            info.ShouHuoDiZhi = Utils.GetFormValue(txtShouHuoDiZhi.UniqueID);
            info.ShouHuoRenDianHua = Utils.GetFormValue(txtShouHuoRenDianHua.UniqueID);
            info.ShouHuoRenName = Utils.GetFormValue(txtShouHuoRenName.UniqueID);
            info.Status = EyouSoft.Model.CaiGouDanStatus.计划采购;
            info.YaoQiuDaoHuoTime = Utils.GetDateTimeNullable(Utils.GetFormValue(txtYaoQiuDaoHuoTime.UniqueID));

            var txt_chanpin_id = Utils.GetFormValues("txt_chanpin_id");
            var txt_chanpin_gysid = Utils.GetFormValues("txt_chanpin_gysid");
            var txt_chanpin_shuliang = Utils.GetFormValues("txt_chanpin_shuliang");
            var txt_chanpin_mignxiid = Utils.GetFormValues("txt_chanpin_mignxiid");
            var txt_chanpin_dingdanid = Utils.GetFormValues("txt_chanpin_dingdanid");
            var txt_chanpin_xuanzhong = Utils.GetFormEditorValues("txt_chanpin_xuanzhong");

            if (txt_chanpin_id == null || txt_chanpin_id.Length == 0) Utils.RCWE_AJAX("0", "操作失败：至少需要采购一件产品");
            if (txt_chanpin_id.Length != txt_chanpin_gysid.Length
                || txt_chanpin_id.Length != txt_chanpin_shuliang.Length
                || txt_chanpin_id.Length != txt_chanpin_mignxiid.Length
                || txt_chanpin_id.Length != txt_chanpin_dingdanid.Length
                || txt_chanpin_id.Length != txt_chanpin_xuanzhong.Length) Utils.RCWE_AJAX("0", "操作失败：表单异常");

            for (int i = 0; i < txt_chanpin_id.Length; i++)
            {
                var item = new EyouSoft.Model.MCaiGouDanChanPinInfo();
                item.ChanPinId = txt_chanpin_id[i];
                item.GysId = txt_chanpin_gysid[i];
                item.MingXiId = txt_chanpin_mignxiid[i];
                item.ShuLiang = Utils.GetDecimal(txt_chanpin_shuliang[i]);
                item.DingDanId = txt_chanpin_dingdanid[i];

                if (string.IsNullOrEmpty(item.ChanPinId) || string.IsNullOrEmpty(item.GysId) || item.ShuLiang <= 0 || txt_chanpin_xuanzhong[i] != "1") continue;

                info.ChanPins.Add(item);
            }

            if (info.ChanPins == null || info.ChanPins.Count == 0) Utils.RCWE_AJAX("0", "操作失败：至少需要采购一件产品");

            info.ShouHuoDiZhiId = Utils.GetFormValue("radioDiZhi");

            return info;
        }

        /// <summary>
        /// baocun
        /// </summary>
        void BaoCun()
        {
            var info = GetFormInfo();

            int bllRetCode = 0;
            if (string.IsNullOrEmpty(info.CaiGouDanId))
            {
                bllRetCode = new EyouSoft.BLL.BCaiGouDan().CaiGouDan_C(info);
            }
            else
            {
                bllRetCode = new EyouSoft.BLL.BCaiGouDan().CaiGouDan_U(info);
            }

            if (bllRetCode == 1) Utils.RCWE_AJAX("1", "操作成功");
            else Utils.RCWE_AJAX("0", "操作失败");
        }

        /// <summary>
        /// init dizhi repeater
        /// </summary>
        void InitDiZhiRpt()
        {
            int recordCount = 0;
            var chaXun = new EyouSoft.Model.MDiZhiChaXunInfo();

            chaXun.GongSiId = YongHuInfo.GongSiId;
            chaXun.Name = string.Empty;

            var items = new EyouSoft.BLL.BDiZhi().GetDiZhis(100, 1, ref recordCount, chaXun);

            if (items != null && items.Count > 0)
            {
                rptDiZhi.DataSource = items;
                rptDiZhi.DataBind();
            }
            else
            {
                phDiZhiEmpty.Visible = true;
            }
        }
        #endregion
    }
}
