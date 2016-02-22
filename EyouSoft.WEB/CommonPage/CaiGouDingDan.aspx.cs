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

namespace EyouSoft.WEB.CommonPage
{
    using System.Collections.Generic;

    using EyouSoft.Model;
    using EyouSoft.Toolkit;

    public partial class CaiGouDingDan : GysYeMian
    {
        #region attributes
        protected int T = Utils.GetInt(Utils.GetQueryStringValue("T"));

        protected string LeiXing = "采购单报价";

        protected DingDanStatus DingDanStatus = (DingDanStatus)Utils.GetInt(Utils.GetQueryStringValue("Status"));

        protected QueRenStatus QueRenStatus = (QueRenStatus)Utils.GetInt(Utils.GetQueryStringValue("QueRen"));

        protected string DingDanId = Utils.GetQueryStringValue("dingdanid");

        protected string GysName = string.Empty;

        protected decimal HeJi;

        protected string SongHuoRenId = string.Empty;
        /// <summary>
        /// 是否显示实际到货数量
        /// </summary>
        protected bool IsXianShiShiJiDaoHuoShuLiang = false;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (T == 3) T = 1;
            if (T == 4) T = 2;
            var dotype = Utils.GetQueryStringValue("do");
            if (dotype == "QueRen") this.QueRen();
            if (dotype == "QuXiao") this.QuXiao();
            if (!IsPostBack)
            {
                this.InitPage();
                this.InitData();

                InitDiZhiRpt();
            }
        }

        #region private members
        void InitPage()
        {
            switch (T)
            {
                case 1:
                    LeiXing = "采购单发货";
                    break;
                case 2:
                    LeiXing = "到货确认管理";
                    break;
            }
            //foreach (var e in Enum.GetValues(typeof(Model.DingDanStatus)))
            //{
            //    CaiGouStatus.Items.Add(new ListItem(e.ToString(),((int)e).ToString()));
            //}
        }

        void InitData()
        {
            var m = new BLL.BDingDan().GetInfo(DingDanId);

            if (m!=null)
            {
                DingDanStatus = m.Status;
                QueRenStatus = m.GysDaoHuoQueRenStatus;
                GysName = m.GysName;
                CaiGouDanHao.InnerText = m.CaiGouDanHao;
                //CaiGouDanName.Items.Add(m.CaiGouDanName);
                ltrCgdName.Text = m.CaiGouDanName;
                //CgsName.Items.Add(m.CgsName);
                ltrCgsName.Text = m.CgsName;
                //CaiGouBuMen.Items.Add(m.CaiGouBuMen);
                ltrCaiGouBuMen.Text = m.CaiGouBuMen;
                var mb = new BLL.BCaiGouMoBan().GetInfo(m.MoBanId);
                if (mb!=null)
                {
                    CaiGouLeiXing.Items.Add(mb.Name);
                }
                if (m.ChanPins!=null&&m.ChanPins.Count>0)
                {
                    HeJi = m.ChanPins.Sum(h => h.JinE);
                    rpt.DataSource = m.ChanPins;
                    rpt.DataBind();
                }
                //CaiGouStatus.Items.FindByValue(((int)m.Status).ToString()).Selected = true;
                ltrCgStatus.Text = m.Status.ToString();
                //CaiGouRen.Value = m.CaoZuoRenName;
                ltrCgRenName.Text = m.CaoZuoRenName;
                //CaiGouTime.Value = m.IssueTime.ToString("yyyy-MM-dd HH:mm");
                ltrCgTime.Text = m.IssueTime.ToString("yyyy-MM-dd HH:mm");
                var s = new BLL.BCaiGouDan().GetInfo(m.CaiGouDanId);
                if (s!=null)
                {
                    //ShouHuoRen.Value = s.ShouHuoRenName;
                    //ShouHuoDiZhi.Value = s.ShouHuoDiZhi;
                    //LianXiTel.Value = s.ShouHuoRenDianHua;
                    //CaiGouRen.Value = s.CaoZuoRenName;
                    //CaiGouTime.Value = s.IssueTime.ToString("yyyy-MM-dd HH:mm");

                    ltrSHRName.Text = s.ShouHuoRenName;
                    ltrSHDiZhi.Text = s.ShouHuoDiZhi;
                    ltrSHRDianHua.Text = s.ShouHuoRenDianHua;
                    ltrCgRenName.Text = s.CaoZuoRenName;
                    ltrCgTime.Text = s.IssueTime.ToString("yyyy-MM-dd HH:mm");
                }

                SongHuoRen.Value = m.SongHuoRenName;
                SongHuoTel.Value = m.SongHuoRenDianHua;
                FaHuoTime.Value = string.Format("{0:yyyy-MM-dd}", m.GysFaHuoTime);
                DaoHuoTime.Value = string.Format("{0:yyyy-MM-dd}", m.YuJiDaoHuoTime);

                SongHuoRenId = m.GysSongHuoRenId;
                txtFaHuoShuoMing.Value = m.GysFaHuoShuoMing;

                if (m.Status == DingDanStatus.采购商确认收货) IsXianShiShiJiDaoHuoShuLiang = true;
            }
        }

        void QueRen()
        {
            if (DingDanStatus == DingDanStatus.采购商确认收货&&QueRenStatus == QueRenStatus.未确认)
            {
                switch (new BLL.BDingDan().GysDaoHuoQueRen(DingDanId, YongHuInfo.YongHuId))
                {
                    case -1:
                        Utils.RCWE_AJAX("0", "该采购订单不存在"); break;
                    case -2:
                        Utils.RCWE_AJAX("0", "采购商收货未确认不能操作"); break;
                    case 1:
                        Utils.RCWE_AJAX("1", "确认成功"); break;                
                }
            }
            else
            {
                var status = DingDanStatus.供应商完成报价;
                var b = new BLL.BDingDan();

                if (T == 1 && DingDanStatus == DingDanStatus.采购商确认报价) status = DingDanStatus.供应商发货完成;

                if (status == DingDanStatus.供应商完成报价)
                {
                    b.SheZhiBaoJiaInfo(this.GetMDingDanBaoJiaInfo());
                }
                if (status == DingDanStatus.供应商发货完成)
                {
                    //var faHuoInfo = GetMDingDanFaHuoInfo();
                    b.SheZhiFaShuoInfo(this.GetMDingDanFaHuoInfo());
                }

                switch (b.SheZhiStatus(DingDanId, status, YongHuInfo.YongHuId))
                {
                    case -1:
                        Utils.RCWE_AJAX("0", "该采购订单不存在");
                        break;
                    case -2:
                        Utils.RCWE_AJAX("0", "该采购处于计划采购阶段不能操作");
                        break;
                    case -3:
                        Utils.RCWE_AJAX("0", "该采购订单已取消或发货完成或收货确认中不能操作");
                        break;
                    case -99:
                        Utils.RCWE_AJAX("0", "已确认");
                        break;
                    default:
                        Utils.RCWE_AJAX("1", "确认成功");
                        break;
                }
            }
        }

        void QuXiao()
        {
            switch (new BLL.BDingDan().SheZhiStatus(DingDanId, DingDanStatus.取消采购, YongHuInfo.YongHuId))
            {
                case -1:
                    Utils.RCWE_AJAX("0", "该采购订单不存在"); break;
                case -2:
                    Utils.RCWE_AJAX("0", "该采购处于计划采购阶段不能操作"); break;
                case -3:
                    Utils.RCWE_AJAX("0", "该采购订单已取消或发货完成或收货确认中不能操作"); break;
                case -99:
                    Utils.RCWE_AJAX("0", "已确认"); break;
                default:
                    Utils.RCWE_AJAX("1", "取消成功"); break;
            }
    
        }

        MDingDanBaoJiaInfo GetMDingDanBaoJiaInfo()
        {
            IList<MDingDanChanPinInfo> chanpins=new List<MDingDanChanPinInfo>();
            var mingxiids = Utils.GetFormValues("MingXiId");
            var shuliangs = Utils.GetFormValues("ShuLiang");
            var chanpinjiages = Utils.GetFormValues("ChanPinJiaGe");

            if (mingxiids!=null&&mingxiids.Count()>0)
            {
                for (int i = 0; i < mingxiids.Count(); i++)
                {
                    if (!string.IsNullOrEmpty(mingxiids[i]))
                    {
                        chanpins.Add(new MDingDanChanPinInfo()
                            {
                                MingXiId = mingxiids[i],
                                ShuLiang = Utils.GetDecimal(shuliangs[i]),
                                ChanPinJiaGe = Utils.GetDecimal(chanpinjiages[i]),
                                JinE = Utils.GetDecimal(shuliangs[i]) * Utils.GetDecimal(chanpinjiages[i]),
                                FaHuoShuLiang = Utils.GetDecimal(shuliangs[i]),
                            });
                    }
                }
            }
            return new MDingDanBaoJiaInfo()
                {
                    DingDanId = DingDanId,
                    ChanPins = chanpins,
                    CaoZuoRenId=YongHuInfo.YongHuId
                };
        }

        MDingDanFaHuoInfo GetMDingDanFaHuoInfo()
        {
            var info= new MDingDanFaHuoInfo()
                {
                    DingDanId=DingDanId,
                    SongHuoRenName = Utils.GetFormValue(SongHuoRen.UniqueID),
                    SongHuoRenDianHua = Utils.GetFormValue(SongHuoTel.UniqueID),
                    SongHuoTime = Utils.GetDateTime(Utils.GetFormValue(FaHuoTime.UniqueID),DateTime.Now),
                    YuJiDaoHuoTime = Utils.GetDateTimeNullable(Utils.GetFormValue(DaoHuoTime.UniqueID)),
                    ChanPins = this.GetMDingDanBaoJiaInfo().ChanPins,
                    GysSongHuoRenId = Utils.GetFormValue("radioDiZhi"),
                    GysFaHuoShuoMing=Utils.GetFormValue(txtFaHuoShuoMing.UniqueID)
                };

            if (string.IsNullOrEmpty(info.SongHuoRenName)) Utils.RCWE_AJAX("0", "请填写送货人姓名");
            if (string.IsNullOrEmpty(info.SongHuoRenDianHua)) Utils.RCWE_AJAX("0", "请填写送货人联系电话");
            if (!Utils.GetDateTimeNullable(Utils.GetFormValue(FaHuoTime.UniqueID)).HasValue) Utils.RCWE_AJAX("0", "请填写发货时间");

            return info;
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
