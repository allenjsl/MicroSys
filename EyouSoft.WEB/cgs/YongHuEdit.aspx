<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YongHuEdit.aspx.cs" Inherits="EyouSoft.WEB.cgs.YongHuEdit" MasterPageFile="~/mp/Boxy.Master" %>

<%@ Register Src="~/wuc/ShangChuan.ascx" TagName="ShangChuan" TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="body" runat="server" ID="body1">

    <form id="form1">
    <div class="alert_t">
        账号信息</div>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="mt10">
        <tr>
            <td align="left" style="width:100px;">
                <span class="bitian">*</span>用户账号：
            </td>
            <td>
                <input name="txtUsername" type="text" class="input-txt" id="txtUsername" style="width:200px;" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="left">
                <span class="bitian">*</span>用户密码：
            </td>
            <td>
                <input name="txtMiMa" type="password" class="input-txt" id="txtMiMa" style="width: 200px;" />
                <asp:Literal runat="server" ID="ltrTiShi0" Visible="false"><span style="color:#666">注：不填写密码不修改原始密码</span></asp:Literal>
            </td>
        </tr>
        <tr>
            <td align="left">
                用户状态：
            </td>
            <td>
                <asp:Literal runat="server" ID="ltrStatus">启用</asp:Literal>
            </td>
        </tr>
        <tr>
            <td align="left">
                用户角色：
            </td>
            <td>
                <select id="txtJueSe" name="txtJueSe" class="input_select">
                    <option value="">-请选择-</option>
                    <asp:Literal runat="server" ID="ltrJueSe"></asp:Literal>
                </select>
            </td>
        </tr>
    </table>
    <div class="alert_t">
        基本信息</div>        
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="mt10">
        <tr>
            <td align="left" style="width: 100px;">
                <span class="bitian">*</span>用户姓名：
            </td>
            <td>
                <input name="txtName" type="text" class="input-txt" id="txtName" style="width: 200px;" runat="server" />
            </td>
            <td align="left" style="width: 100px;">
                用户性别：
            </td>
            <td>
                <input type="radio" id="txtXingBie0" name="txtXingBie" value="0"/><label for="txtXingBie0">&nbsp;男&nbsp;</label>
                <input type="radio" id="txtXingBie1" name="txtXingBie" value="1" /><label for="txtXingBie1">&nbsp;女&nbsp;</label>
            </td>
        </tr>        
        <tr>
            <td align="left">
                <span class="bitian">*</span>所属部门：
            </td>
            <td>
                <input name="txtBuMenName" type="text" class="input-txt" id="txtBuMenName" style="width: 200px;" runat="server" />
            </td>
            <td align="left">
                职务：
            </td>
            <td>
                <input name="txtZhiWu" type="text" class="input-txt" id="txtZhiWu" style="width: 200px;" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="left">
                <span class="bitian">*</span>手机：
            </td>
            <td>
                <input name="txtShouJi" type="text" class="input-txt" id="txtShouJi" style="width: 200px;" runat="server" />
            </td>
            <td align="left">
                电话：
            </td>
            <td>
                <input name="txtDianHua" type="text" class="input-txt" id="txtDianHua" style="width: 200px;" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="left">
                传真：
            </td>
            <td>
                <input name="txtFax" type="text" class="input-txt" id="txtFax" style="width: 200px;" runat="server" />
            </td>
            <td align="left">
                邮箱：
            </td>
            <td>
                <input name="txtYouXiang" type="text" class="input-txt" id="txtYouXiang" style="width: 200px;" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="left">
                出生年月：
            </td>
            <td>
                <input name="txtChuShengRiQi" type="text" class="input-txt" id="txtChuShengRiQi" style="width: 200px;" runat="server" onfocus="WdatePicker();" />
            </td>
            <td align="left">
                入职时间：
            </td>
            <td>
                <input name="txtRuZhiRiQi" type="text" class="input-txt" id="txtRuZhiRiQi" style="width: 200px;" runat="server" onfocus="WdatePicker();" />
            </td>
        </tr>
        <tr>
            <td align="left">
                居住地址：
            </td>
            <td colspan="3">
                <input name="txtDiZhi" type="text" class="input-txt" id="txtDiZhi" style="width: 400px;" runat="server" />
            </td>
        </tr>    
       
    </table>
    <div class="alert_t">
        用户照片</div>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="mt10">
        <tr>
            <td align="left" style="width:100px;">
                照片上传：
            </td>
            <td>
                <uc1:ShangChuan runat="server" ID="txtZhaoPian" ShuoMing="建议尺寸：300*300px。" XianShiClassName="uploadify_yonghuzhaopian_xianshi"></uc1:ShangChuan>
            </td>
        </tr>
     </table> 
    <div class="alertbox-bot">
        <div class="alertbox-btn">
            <a href="javascript:void(0)" class="blue_btn" id="a_baocun">保 存</a></div>
    </div>
    </form>
    
    <script type="text/javascript">
        var iPage = {
            reload: function() {
                window.location.href = window.location.href;
            },
            close: function() {
                top.Boxy.getIframeDialog('<%=EyouSoft.Toolkit.Utils.GetQueryStringValue("iframeId") %>').hide();
            },
            yangZhengForm: function() {
                if ($.trim($("#<%=txtUsername.ClientID %>").val()).length < 1) { alert("请输入用户账号"); return false; }
                if ($.trim($("#txtMiMa").val()).length < 1 && "<%=EditId %>".length == 0) { alert("请输入用户密码"); return false; }
                if ($.trim($("#<%=txtName.ClientID %>").val()).length < 1) { alert("请输入用户姓名"); return false; }
                if ($.trim($("#<%=txtBuMenName.ClientID %>").val()).length < 1) { alert("请输入用户部门"); return false; }
                //if ($.trim($("#<%=txtShouJi.ClientID %>").val()).length < 1 && $.trim($("#<%=txtDianHua.ClientID %>").val()).length < 1) { alert("请输入用户手机或电话"); return false; }
                if ($.trim($("#<%=txtShouJi.ClientID %>").val()).length < 1) { alert("请输入用户手机"); return false; }
                if ($.trim($("#<%=txtShouJi.ClientID %>").val()).length>0&&!eNow.regExps["isTel"].test($.trim($("#<%=txtShouJi.ClientID %>").val()))) { alert("请输入正确的手机号码"); return false; }
                if ($.trim($("#<%=txtDianHua.ClientID %>").val()).length > 0 && !eNow.regExps["isTel"].test($.trim($("#<%=txtDianHua.ClientID %>").val()))) { alert("请输入正确的电话号码"); return false; }
                if ($.trim($("#<%=txtFax.ClientID %>").val()).length > 0 && !eNow.regExps["isFax"].test($.trim($("#<%=txtFax.ClientID %>").val()))) { alert("请输入正确的传真号码"); return false; }
                if ($.trim($("#<%=txtYouXiang.ClientID %>").val()).length > 0 && !eNow.regExps["isEmail"].test($.trim($("#<%=txtYouXiang.ClientID %>").val()))) { alert("请输入正确的邮箱"); return false; }

                return true;
            },
            baoCun: function(obj) {
                var yanZhengRetCode = this.yangZhengForm();
                if (!yanZhengRetCode) return false;

                $(obj).unbind("click").css({ "color": "#999999" });
                var _self = this;
                $.ajax({ type: "POST", url: window.location.href + "&doType=baocun", data: $("#form1").serialize(), cache: false, dataType: "json", async: false,
                    success: function(response) {
                        alert(response.msg);
                        if (response.result == "1") {
                            _self.close();
                        } else {
                            $(obj).bind("click", function() { _self.baoCun(obj); }).css({ "color": "" });
                        }
                    },
                    error: function() {
                        $(obj).bind("click", function() { _self.baoCun(obj); }).css({ "color": "" });
                    }
                });
            }
        }

        $(document).ready(function() {
            $("#txtXingBie<%=XingBie %>").prop("checked", true);
            $("#a_baocun").click(function() { iPage.baoCun(this); });
            if ("<%=EditId %>".length > 0) { $("#<%=txtUsername.ClientID %>").prop("readonly", true).addClass("readonly"); }
            $("#txtJueSe").val("<%=JueSeId %>");
        });
    </script>
</asp:Content>