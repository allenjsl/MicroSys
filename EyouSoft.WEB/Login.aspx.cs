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
using EyouSoft.Toolkit;
using EyouSoft.Security.Membership;

namespace EyouSoft.WEB
{
    using EyouSoft.Model;

    public partial class Login : System.Web.UI.Page
    {
        protected EyouSoft.Model.SSO.MYongHuInfo UserInfo;

        protected static string GYS_DEFAULT = "/gys/Default.aspx?T=0";

        protected static string GK_DEFAULT = "/gk/Default.aspx?T="+(int)GongSiLeiXing.采购商;

        protected static string CGS_DEFAULT = "/cgs/cgdruku.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            /*if (!IsPostBack)
            {
                if (YongHuProvider.IsLogin(out UserInfo))
                {
                    switch (UserInfo.LeiXing)
                    {
                        case YongHuLeiXing.供应商:
                            Response.Redirect(GYS_DEFAULT);
                            break;
                        case YongHuLeiXing.平台:
                            Response.Redirect(GK_DEFAULT);
                            break;
                        case YongHuLeiXing.采购商:
                            Response.Redirect(CGS_DEFAULT);
                            break;
                        default:
                            break;
                    }
                }
 
            }*/

            if (Utils.GetQueryStringValue("type")=="login")
            {
                this.DoLogin();
            }
        }

         void DoLogin()
         {
             var usernm = Utils.GetFormValue("txt_u");
             var userpw = Utils.GetFormValue("txt_p");
             var redirect = string.Empty;

             var r = YongHuProvider.Login(usernm, Utils.MD5Encrypt(userpw), out UserInfo, 30);

             if (UserInfo != null)
             {
                 switch (UserInfo.LeiXing)
                 {
                     case YongHuLeiXing.供应商:
                         redirect = GYS_DEFAULT;
                         break;
                     case YongHuLeiXing.平台:
                         redirect = GK_DEFAULT;
                         break;
                     case YongHuLeiXing.采购商:
                         redirect = CGS_DEFAULT;
                         break;
                     default:
                         break;
                 }
             }

             switch (r)
             {
                 case -1:
                     Utils.RCWE_AJAX("0", "用户名不能为空");
                     break;
                 case -2:
                     Utils.RCWE_AJAX("0", "密码不能为空");
                     break;
                 case -4:
                     Utils.RCWE_AJAX("0", "用户名或密码不正确");
                     break;
                 case -5:
                     Utils.RCWE_AJAX("0", "该用户不可用");
                     break;
                 case -6:
                     Utils.RCWE_AJAX("0", "您的账号需要等待审核后才能使用");
                     break;
                 default:
                     Utils.RCWE_AJAX("1", "登录成功，正在为您跳转...", redirect);
                     break;
             }
         }
    }
}
