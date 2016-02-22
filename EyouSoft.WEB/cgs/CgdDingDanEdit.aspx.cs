//采购订单-编辑 汪奇志 2015-05-07
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EyouSoft.Toolkit;

namespace EyouSoft.WEB.cgs
{
    /// <summary>
    /// 采购订单-编辑
    /// </summary>
    public partial class CgdDingDanEdit : CgsYeMian
    {
        #region 
        /// <summary>
        /// 订单编号
        /// </summary>
        protected string EditId = string.Empty;

        /// <summary>
        /// 是否显示实际到货数量
        /// </summary>
        protected bool IsXianShiShiJiDaoHuoShuLiang = false;
        /// <summary>
        /// 到货数量是否为只读
        /// </summary>
        protected string IsReadonlyDaoHuoShuLiang = "0";

        EyouSoft.Model.DingDanStatus DingDanStatus = EyouSoft.Model.DingDanStatus.计划采购;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            EditId = Utils.GetQueryStringValue("editid");

            switch (Utils.GetQueryStringValue("dotype"))
            {
                case "quxiao": QuXiao(); break;
                case "querenbaojia": QueRenBaoJia(); break;
                case "querenshouhuo": QueRenShouHuo(); break;
            }

            InitInfo();
        }

        #region private members
        /// <summary>
        /// init info
        /// </summary>
        void InitInfo()
        {
            if (string.IsNullOrEmpty(EditId)) Utils.RCWE_AJAX("0", "异常请求");

            var info = new EyouSoft.BLL.BDingDan().GetInfo(EditId);
            if (info == null) Utils.RCWE_AJAX("0", "异常请求");

            var cgdInfo = new EyouSoft.BLL.BCaiGouDan().GetInfo(info.CaiGouDanId);
            if (cgdInfo == null) Utils.RCWE_AJAX("0", "异常请求");

            ltrCaiGouDanHao.Text = info.CaiGouDanHao;
            ltrCaiGouDanName.Text = info.CaiGouDanName;
            ltrCaiGouRenName.Text = cgdInfo.CaoZuoRenName;
            ltrCaiGouBuMenName.Text = info.CaiGouBuMen;
            ltrGysName.Text = info.GysName;
            ltrStatus.Text = info.Status.ToString();

            DingDanStatus = info.Status;

            if (info.ChanPins != null && info.ChanPins.Count > 0)
            {
                rpt.DataSource = info.ChanPins;
                rpt.DataBind();
            }

            ltrHeJiJinE.Text = info.JinE.ToString("F2");

            ltrYaoQiuDaoHuoTime.Text=string.Format("{0:yyyy-MM-dd}", cgdInfo.YaoQiuDaoHuoTime);
            ltrShouHuoRenName.Text = cgdInfo.ShouHuoRenName;
            ltrShouHuoRenDianHua.Text = cgdInfo.ShouHuoRenDianHua;
            ltrShouHuoDiZhi.Text = cgdInfo.ShouHuoDiZhi;

            if (info.Status == EyouSoft.Model.DingDanStatus.供应商发货完成 
                || info.Status == EyouSoft.Model.DingDanStatus.采购商确认收货)
            {
                IsXianShiShiJiDaoHuoShuLiang = true;
                phFaHuo.Visible = true;                
                phDaoHuo.Visible = true;

                ltrFaHuoTime.Text = string.Format("{0:yyyy-MM-dd}", info.SongHuoTime);
                ltrYuJiDaoHuoTime.Text = string.Format("{0:yyyy-MM-dd}", info.YuJiDaoHuoTime);
                ltrSongHuoRenName.Text = info.SongHuoRenName;
                ltrSongHuoRenDianHua.Text = info.SongHuoRenDianHua;
                ltrGysFaHuoShuoMing.Text = info.GysFaHuoShuoMing;

                txtShiJiDaoHuoTime.Value = DateTime.Now.ToString("yyyy-MM-dd");
                txtDaoHuoQueRenRenName.Value = YongHuInfo.Name;
            }

            if (info.Status == EyouSoft.Model.DingDanStatus.采购商确认收货)
            {
                phDaoHuo.Visible = true;
                txtShiJiDaoHuoTime.Value = string.Format("{0:yyyy-MM-dd}", info.DaoHuoTime);
                txtDaoHuoQueRenRenName.Value = info.CgsShouHuoRen;

                IsReadonlyDaoHuoShuLiang = "1";
            }

            var _caoZuo = string.Empty;
            var _quXiao = "<a href=\"javascript:void(0)\" class=\"blue_btn\" id=\"a_quxiao\">取消采购</a>&nbsp;&nbsp;";
            var _queRenBaoJiao = "<a href=\"javascript:void(0)\" class=\"blue_btn\" id=\"a_querenbaojia\">确认报价</a>&nbsp;&nbsp;";
            var _queRenShouHuo = "<a href=\"javascript:void(0)\" class=\"blue_btn\" id=\"a_querenshouhuo\">确认收货</a>&nbsp;&nbsp;";

            switch (info.Status)
            {
                case EyouSoft.Model.DingDanStatus.采购申请:
                    _caoZuo = _quXiao;
                    break;
                case EyouSoft.Model.DingDanStatus.供应商完成报价:
                    _caoZuo = _queRenBaoJiao + _quXiao;
                    break;
                case EyouSoft.Model.DingDanStatus.采购商确认报价:
                    _caoZuo = _quXiao;
                    break;
                case EyouSoft.Model.DingDanStatus.供应商发货完成:
                    _caoZuo = _queRenShouHuo;
                    break;
                case EyouSoft.Model.DingDanStatus.采购商确认收货:
                    _caoZuo = "已确认收货，交易完成";
                    break;
                case EyouSoft.Model.DingDanStatus.取消采购:
                    _caoZuo = "该采购订单已取消";
                    break;
            }

            if (info.CgsFuKuanStatus == EyouSoft.Model.FuKuanStatus.已付款)
            {
                phFuKuan.Visible = true;

                ltrFuKuanTime.Text = string.Format("{0:yyyy-MM-dd HH:mm}", info.CgsFuKuanTime);
                ltrFuKuanCaoZuoRenName.Text = info.CgsFuKuanCaoZuoRenName;
            }

            ltrCaoZuo.Text = _caoZuo;
        }

        /// <summary>
        /// 取消
        /// </summary>
        void QuXiao()
        {
            int bllRetCode = new EyouSoft.BLL.BDingDan().SheZhiStatus(EditId, EyouSoft.Model.DingDanStatus.取消采购, YongHuInfo.YongHuId);

            if (bllRetCode == 1) Utils.RCWE_AJAX("1", "操作成功");
            else Utils.RCWE_AJAX("0", "操作失败");
        }

        /// <summary>
        /// 确认报价
        /// </summary>
        void QueRenBaoJia()
        {
            int bllRetCode = new EyouSoft.BLL.BDingDan().SheZhiStatus(EditId, EyouSoft.Model.DingDanStatus.采购商确认报价, YongHuInfo.YongHuId);

            if (bllRetCode == 1) Utils.RCWE_AJAX("1", "操作成功");
            else Utils.RCWE_AJAX("0", "操作失败");
        }

        /// <summary>
        /// 确认收货
        /// </summary>
        void QueRenShouHuo()
        {
            var info = new EyouSoft.Model.MDingDanShouHuoInfo();
            info.ChanPins = new List<EyouSoft.Model.MDingDanChanPinInfo>();
            info.DaoHuoTime = Utils.GetDateTime(Utils.GetFormValue(txtShiJiDaoHuoTime.UniqueID),DateTime.Now);
            info.DingDanId = EditId;
            info.CgsShouHuoRen = Utils.GetFormValue(txtDaoHuoQueRenRenName.UniqueID);

            var txt_chanpin_mignxiid = Utils.GetFormValues("txt_chanpin_mignxiid");
            var txt_chanpin_daohuoshuliang = Utils.GetFormValues("txt_chanpin_daohuoshuliang");

            if (txt_chanpin_mignxiid == null || txt_chanpin_mignxiid.Length == 0||txt_chanpin_daohuoshuliang==null) Utils.RCWE_AJAX("0", "表单异常");
            if (txt_chanpin_mignxiid.Length != txt_chanpin_daohuoshuliang.Length) Utils.RCWE_AJAX("0", "表单异常");

            for (var i = 0; i < txt_chanpin_mignxiid.Length; i++)
            {
                var item = new EyouSoft.Model.MDingDanChanPinInfo();
                item.MingXiId = txt_chanpin_mignxiid[i];
                item.DaoHuoShuLiang = Utils.GetDecimal(txt_chanpin_daohuoshuliang[i]);
                info.ChanPins.Add(item);
            }

            int bllRetCode1 = 0;
            int bllRetCode2 = 0;

            bllRetCode1 = new EyouSoft.BLL.BDingDan().SheZhiShouHuoInfo(info);

            if (bllRetCode1 == 1)
            {
                bllRetCode2 = new EyouSoft.BLL.BDingDan().SheZhiStatus(EditId, EyouSoft.Model.DingDanStatus.采购商确认收货, YongHuInfo.YongHuId);
            }

            if (bllRetCode2 == 1) Utils.RCWE_AJAX("1", "操作成功");
            else Utils.RCWE_AJAX("0", "操作失败");
        }
        #endregion

        #region protected members
        /// <summary>
        /// 获取实际到货数量
        /// </summary>
        /// <param name="caiGouShuLiang"></param>
        /// <param name="faHuoShuLiang"></param>
        /// <param name="shiJiDaoHuoShuLiang"></param>
        /// <returns></returns>
        protected string GetShiJiDaoHuoShuLiang(object caiGouShuLiang, object faHuoShuLiang, object shiJiDaoHuoShuLiang)
        {
            decimal shuLiang = (decimal)caiGouShuLiang;

            if (DingDanStatus == EyouSoft.Model.DingDanStatus.采购商确认收货) shuLiang = (decimal)shiJiDaoHuoShuLiang;

            return shuLiang.ToString("F2");
        }
        #endregion
    }
}
