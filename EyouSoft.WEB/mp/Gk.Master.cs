using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EyouSoft.WEB.mp
{
    using EyouSoft.Model.SSO;
    using EyouSoft.Security.Membership;
    using EyouSoft.Toolkit;

    /// <summary>
    /// 管控master page
    /// </summary>
    public partial class Gk : System.Web.UI.MasterPage
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

        protected int XiaoXiShu = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            InitInfo();
            InitXiaoXiShu();
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
            }

            var ipInfo = Utils.GetIpInfo();
            IpXinXi = ipInfo.region + ipInfo.city;

            if (string.IsNullOrEmpty(YeMianBiaoTi))
            {
                YeMianBiaoTi = "管控平台-联速交易";
            }
        }

        /// <summary>
        /// init xiaoxi shu
        /// </summary>
        void InitXiaoXiShu()
        {
            IList<EyouSoft.Model.XiaoXiLeiXing> leixings = new List<EyouSoft.Model.XiaoXiLeiXing>();
            leixings.Add(EyouSoft.Model.XiaoXiLeiXing.公司注册待审核);
            var chaXun = new EyouSoft.Model.MXiaoXiChaXunInfo();
            chaXun.Status = EyouSoft.Model.XiaoXiStatus.未读;
            chaXun.LeiXings = leixings;
            chaXun.JieShouGongSiId = YongHuInfo.GongSiId;

            XiaoXiShu= new BLL.BXiaoXi().GetXiaoXiShu(chaXun);
        }
        #endregion
    }
}
