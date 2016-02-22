using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Toolkit;

namespace EyouSoft.WEB
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("/logout.aspx");
            fenYe.intPageSize = 10;
            fenYe.CurrencyPage = 2;
            fenYe.intRecordCount = 209;
        }
    }
}
