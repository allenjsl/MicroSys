using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Toolkit;

namespace EyouSoft.WEB.mp
{
    /// <summary>
    /// 采购商master page
    /// </summary>
    public partial class Cgs : System.Web.UI.MasterPage
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

                GongSiXinXi = yongHuInfo.GS_Name + " - " + yongHuInfo.Name;
            }

            var ipInfo = Utils.GetIpInfo();
            IpXinXi = ipInfo.region + ipInfo.city;

            if (string.IsNullOrEmpty(YeMianBiaoTi))
            {
                YeMianBiaoTi = "采购商平台-联速交易";
            }
        }
        #endregion
    }
}
