using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using EyouSoft.Toolkit;

namespace EyouSoft.WEB.ashx
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Handler : IHttpHandler
    {
        HttpContext context = null;

        public void ProcessRequest(HttpContext _context)
        {
            context = _context;
            string dotype = Utils.GetQueryStringValue("dotype");

            switch (dotype)
            {
                case "getcgsxiaoxi": GetCgsXiaoXi(); break;
                case "getautocompletegongsi": GetAutocompleteGongSi(); break;
                default: break;
            }
        }

        #region private members
        /// <summary>
        /// 获取采购商消息数量
        /// </summary>
        void GetCgsXiaoXi()
        {
            var info = new MAjaxXiaoXiInfo();
            var chaXun = new EyouSoft.Model.MXiaoXiChaXunInfo();
            EyouSoft.Model.SSO.MYongHuInfo loginYongHuInfo;
            bool isLogin = EyouSoft.Security.Membership.YongHuProvider.IsLogin(out loginYongHuInfo);

            if (isLogin && loginYongHuInfo.LeiXing== EyouSoft.Model.YongHuLeiXing.采购商)
            {
                chaXun.JieShouGongSiId = loginYongHuInfo.GongSiId;
                chaXun.Status = EyouSoft.Model.XiaoXiStatus.未读;
                chaXun.LeiXings = new List<EyouSoft.Model.XiaoXiLeiXing>();
                chaXun.LeiXings.Add(EyouSoft.Model.XiaoXiLeiXing.采购商待确认报价);
                chaXun.LeiXings.Add(EyouSoft.Model.XiaoXiLeiXing.采购商待确认收货);

                var xiaoXiShuLiang = new EyouSoft.BLL.BXiaoXi().GetXiaoXiShu(chaXun);

                info.ShuLiang = xiaoXiShuLiang;
            }

            Utils.RCWE_AJAX("1", "", info);
        }

        /// <summary>
        /// getautocompletegongsi
        /// </summary>
        void GetAutocompleteGongSi()
        {
            var chaXun = new EyouSoft.Model.MGongSiChaXunInfo();
            chaXun.ShenHeStatus = EyouSoft.Model.ShenHeStatus.已审核;
            chaXun.Name = Utils.GetQueryStringValue("q");
            chaXun.LeiXing = (EyouSoft.Model.GongSiLeiXing?)Utils.GetEnumValueNullable(typeof(EyouSoft.Model.GongSiLeiXing), Utils.GetQueryStringValue("gslx"));

            var recordCount = 0;
            var items = new EyouSoft.BLL.BGongSi().GetGongSis(10, 1, ref recordCount, chaXun);

            var items1 = new List<MAjaxAutocompleteGongSiInfo>();

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    var item1 = new MAjaxAutocompleteGongSiInfo();
                    item1.GongSiId = item.GongSiId;
                    item1.GongSiName = item.Name;

                    items1.Add(item1);
                }
            }

            if (items1 == null || items1.Count == 0)
            {
                items1 = new List<MAjaxAutocompleteGongSiInfo>();
                items1.Add(new MAjaxAutocompleteGongSiInfo() { GongSiName = "未匹配到公司信息" });
            }

            Utils.RCWE(Newtonsoft.Json.JsonConvert.SerializeObject(items1));
        }
        #endregion

        #region public members
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        #endregion
    }

    #region ajax xiaoxi info
    /// <summary>
    /// ajax xiaoxi info
    /// </summary>
    public class MAjaxXiaoXiInfo
    {
        /// <summary>
        /// 消息数量
        /// </summary>
        public int ShuLiang { get; set; }
    }
    #endregion

    #region AJAX自动完成公司信息业务实体
    /// <summary>
    /// AJAX自动完成公司信息业务实体
    /// </summary>
    class MAjaxAutocompleteGongSiInfo
    {
        /// <summary>
        /// 公司编号
        /// </summary>
        public string GongSiId { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string GongSiName { get; set; }
    }
    #endregion
}
