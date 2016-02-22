<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DiZhiEdit.aspx.cs" Inherits="EyouSoft.WEB.cgs.DiZhiEdit" MasterPageFile="~/mp/Boxy.Master" %>

<asp:Content ContentPlaceHolderID="body" runat="server" ID="body1">
    <form id="form1">
    <div class="alert_t">
        地址信息</div>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="mt10">
        <tr>
            <td align="left" style="width: 100px;">
                <span class="bitian">*</span>联系姓名：
            </td>
            <td>
                <input name="txtName" type="text" class="input-txt" id="txtName" style="width: 400px;" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="left">
                <span class="bitian">*</span>联系地址：
            </td>
            <td>
                <input name="txtDiZhi" type="text" class="input-txt" id="txtDiZhi" style="width: 400px;" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="left">
                <span class="bitian">*</span>联系电话：
            </td>
            <td>
                <input name="txtDianHua" type="text" class="input-txt" id="txtDianHua" style="width: 400px;" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="left">
                <span class="bitian">*</span>联系手机：
            </td>
            <td>
               <input name="txtShouJi" type="text" class="input-txt" id="txtShouJi" style="width: 400px;" runat="server" />
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
                if ($.trim($("#<%=txtName.ClientID %>").val()).length < 1) { alert("请输入联系姓名"); return false; }
                if ($.trim($("#<%=txtDiZhi.ClientID %>").val()).length < 1) { alert("请输入地址"); return false; }
                if ($.trim($("#<%=txtDianHua.ClientID %>").val()).length < 1
                    && $.trim($("#<%=txtShouJi.ClientID %>").val()).length < 1) { alert("请输入联系电话或手机"); return false; }

                if ($.trim($("#<%=txtDianHua.ClientID %>").val()).length > 0 && !eNow.regExps["isTel"].test($.trim($("#<%=txtDianHua.ClientID %>").val()))) { alert("请输入正确的电话号码"); return false; }
                if ($.trim($("#<%=txtShouJi.ClientID %>").val()).length > 0 && !eNow.regExps["isTel"].test($.trim($("#<%=txtShouJi.ClientID %>").val()))) { alert("请输入正确的手机号码"); return false; }
                
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
            $("#a_baocun").click(function() { iPage.baoCun(this); });
        });
    </script>

</asp:Content>
