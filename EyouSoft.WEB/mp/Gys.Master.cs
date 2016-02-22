using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EyouSoft.WEB.mp
{
    using EyouSoft.Model;
    using EyouSoft.Security.Membership;
    using EyouSoft.Toolkit;

    using MYongHuInfo = EyouSoft.Model.SSO.MYongHuInfo;

    /// <summary>
    /// 供应商master page
    /// </summary>
    public partial class Gys : System.Web.UI.MasterPage
    {
        #region attributes
        /// <summary>
        /// 页面标题
        /// </summary>
        protected string YeMianBiaoTi = string.Empty;

        /// <summary>
        /// logo
        /// </summary>
        protected string LogoFilepath = string.Empty;
        /// <summary>
        /// 用户照片
        /// </summary>
        protected string YongHuZhaoPianFilepath = string.Empty;
        /// <summary>
        /// 用户姓名
        /// </summary>
        protected string YongHuName = string.Empty;
        /// <summary>
        /// IP信息
        /// </summary>
        protected string IpXinXi = string.Empty;

        protected EyouSoft.Model.SSO.MYongHuInfo YongHuInfo = null;
        /// <summary>
        /// 公司信息
        /// </summary>
        protected string GongSiXinXi = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            InitInfo();
        }

        protected int GetXiaoXiShu()
        {
            IList<XiaoXiLeiXing> leixings=new List<XiaoXiLeiXing>();
            leixings.Add(XiaoXiLeiXing.供应商待报价);
            leixings.Add(XiaoXiLeiXing.供应商待发货);

            EyouSoft.Model.SSO.MYongHuInfo yongHuInfo = null;
           
            string _gongSiId = string.Empty;

            if (EyouSoft.Security.Membership.YongHuProvider.IsLogin(out yongHuInfo))
            {
                _gongSiId = yongHuInfo.GongSiId;               
            }

            return new BLL.BXiaoXi().GetXiaoXiShu(new MXiaoXiChaXunInfo()
                {
                    Status=XiaoXiStatus.未读,
                    LeiXings = leixings,
                    JieShouGongSiId = _gongSiId
                });
        }

        #region private members
        /// <summary>
        /// init info
        /// </summary>
        void InitInfo()
        {
            YeMianBiaoTi = Page.Title;
            EyouSoft.Model.SSO.MYongHuInfo yongHuInfo = null;
            EyouSoft.Security.Membership.YongHuProvider.IsLogin(out yongHuInfo);

            if (yongHuInfo != null)
            {
                LogoFilepath = yongHuInfo.GS_LogoFilepath;
                YongHuName = yongHuInfo.Name;
                YongHuZhaoPianFilepath = yongHuInfo.ZhaoPianFilepath;

                if (string.IsNullOrEmpty(LogoFilepath)) LogoFilepath = "/images/logo.png";
                if (string.IsNullOrEmpty(YongHuZhaoPianFilepath)) YongHuZhaoPianFilepath = "/images/zhaopian.jpg";

                YongHuInfo = yongHuInfo;

                GongSiXinXi = yongHuInfo.GS_Name + " -" + yongHuInfo.Name;
            }

            var ipInfo = Utils.GetIpInfo();
            IpXinXi = ipInfo.region + ipInfo.city;

            if (string.IsNullOrEmpty(YeMianBiaoTi))
            {
                YeMianBiaoTi = "供应商平台-联速交易";
            }
        }
        #endregion
    }
}
