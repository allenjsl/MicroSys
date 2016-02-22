//采购单-订单管理 汪奇志 2015-05-07
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
    /// 采购单-订单管理
    /// </summary>
    public partial class CgdDingDan : CgsYeMian
    {
        #region attributes
        protected int pageSize = 20;
        protected int pageIndex = 1;
        protected int recordCount = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            InitPrivs();
            InitRpt();
        }

        #region private members
        /// <summary>
        /// get chaxun
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.MDingDanChaXunInfo GetChaXunInfo()
        {
            var info = new EyouSoft.Model.MDingDanChaXunInfo();

            info.CaiGouDanHao = Utils.GetQueryStringValue("txtCgdHao");
            info.CaiGouTime1 = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtCaiGouTime1"));
            info.CaiGouTime2 = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtCaiGouTime2"));
            info.CgsId = YongHuInfo.GongSiId;
            info.DingDanStatus = (EyouSoft.Model.DingDanStatus?)Utils.GetEnumValueNullable(typeof(EyouSoft.Model.DingDanStatus), Utils.GetQueryStringValue("txtStatus"));

            return info;
        }

        /// <summary>
        /// init repeater
        /// </summary>
        void InitRpt()
        {
            pageIndex = Utils.GetPadingIndex();
            var chaXun = GetChaXunInfo();

            var items = new EyouSoft.BLL.BDingDan().GetDingDans(pageSize, pageIndex, ref recordCount, chaXun);

            if (items != null && items.Count > 0)
            {
                rpt.DataSource = items;
                rpt.DataBind();

                phEmpty.Visible = false;
            }
            else
            {
                phEmpty.Visible = true;
            }
        }
        #endregion

        #region protected members
        /// <summary>
        /// get caozuo
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        protected string GetCaoZuo(object status)
        {
            var _status = (EyouSoft.Model.DingDanStatus)status;

            string s = string.Empty;

            switch (_status)
            {
                case EyouSoft.Model.DingDanStatus.取消采购:
                case EyouSoft.Model.DingDanStatus.采购商确认收货:
                    s = "<a href=\"javascript:void(0)\" class=\"blue_btn\" data-class=\"chakan\">查看</a>";
                    break;
                default:
                    s = "<a href=\"javascript:void(0)\" class=\"blue_btn\" data-class=\"guanli\">管理</a>";
                    break;
            }

            return s;
        }

        /// <summary>
        /// get jine
        /// </summary>
        /// <param name="jinE"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        protected string GetJinE(object jinE, object status)
        {
            var _jinE = (decimal)jinE;

            return _jinE.ToString("F2");
        }

        /// <summary>
        /// get gys lxqq
        /// </summary>
        /// <param name="lxQQ"></param>
        /// <returns></returns>
        protected string GetGysLxQQ(object lxQQ)
        {
            if (lxQQ == null) return string.Empty;

            string s = string.Empty;

            return s.ToString();
        }

        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            if (!YongHuInfo.IsPrivs(EyouSoft.Model.CGS_Privs1.采购管理))
            {
                Response.Redirect("/cgs/default1.aspx");
            }
        }
        #endregion
    }
}
