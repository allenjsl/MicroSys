<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JueSeEdit.aspx.cs" Inherits="EyouSoft.WEB.gk.JueSeEdit" MasterPageFile="~/mp/Boxy.Master" %>

<asp:Content ContentPlaceHolderID="body" runat="server" ID="body1">
    <form id="form1">
    <div class="alert_t">
        角色信息</div>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="mt10">
        <tr>
            <td align="left" style="width: 100px;">
                角色名称：
            </td>
            <td>
                <input name="txtName" type="text" class="input-txt" id="txtName" style="width: 400px;" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="left">
                角色权限：
            </td>
            <td>
                <asp:Repeater runat="server" ID="rpt">
                    <ItemTemplate>
                        <p style="line-height: 30px;">
                            <input type="checkbox" value="<%#Eval("Value") %>" name="chkPrivs1" id="chk_<%#Eval("Value") %>">&nbsp;&nbsp;<label for="chk_<%#Eval("Value") %>"><%#Eval("Text") %></label></p>
                    </ItemTemplate>
                </asp:Repeater>
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
                if ($.trim($("#<%=txtName.ClientID %>").val()).length < 1) { alert("请输入角色名称"); return false; }

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
            },
            initChk: function() {
                var _s = "<%=JueSePrivs %>";
                if (_s.length == 0) return;
                var _arr = _s.split(",");
                if (_arr.length == 0) return;
                for (var i = 0; i < _arr.length; i++) {
                    $("#chk_" + _arr[i]).prop("checked", true);
                }
            }
        }

        $(document).ready(function() {
            $("#a_baocun").click(function() { iPage.baoCun(this); });
            iPage.initChk();
        });
    </script>

</asp:Content>
