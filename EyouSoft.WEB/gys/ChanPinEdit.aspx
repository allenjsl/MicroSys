<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChanPinEdit.aspx.cs" Inherits="EyouSoft.WEB.gys.ChanPinEdit" MasterPageFile="/mp/Boxy.Master" %>

<%@ Register Src="~/wuc/ShangChuan02.ascx" TagName="ShangChuan02" TagPrefix="uc1" %>

<asp:Content ID="body" runat="server" ContentPlaceHolderID="body">
    <div class="alert_t">
        <%=string.IsNullOrEmpty(ChanPinId)?"新增产品":"编辑产品"%></div>
    <form>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="mt10">
        <tr>
            <td align="left">
                产品编码：
            </td>
            <td>
                <input id="BianMa" name="BianMa" type="text" class="w400 input-txt" runat="server" readonly="readonly" value="自动生成" style="background:#efefef;" />
            </td>
        </tr>
        <tr>
            <td align="left">
                <span class="bitian">*</span>产品名称：
            </td>
            <td>
                <input id="Name" name="Name" type="text" class="w400 input-txt" runat="server" maxlength="255" />
            </td>
        </tr>
        <tr>
            <td align="left">
                <span class="bitian">*</span>产品品牌：
            </td>
            <td>
                <input id="PinPai" name="PinPai" type="text" class="w400 input-txt" runat="server" maxlength="255" />
            </td>
        </tr>
        <tr>
            <td align="left">
                <span class="bitian">*</span>产品规格：
            </td>
            <td>
                <input id="GuiGe" name="GuiGe" type="text" class="w400 input-txt" runat="server" maxlength="255" />
            </td>
        </tr>
        <tr>
            <td align="left">
                市&nbsp;&nbsp;场&nbsp;&nbsp;价：
            </td>
            <td>
                <input id="JiaGe1" name="JiaGe1" type="text" class="w400 input-txt" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="left">
                <span class="bitian">*</span>计量单位：
            </td>
            <td>
                <input id="DanWei" name="DanWei" type="text" class="w400 input-txt" runat="server" maxlength="255" />
            </td>
        </tr>
        <tr>
            <td align="left">
                产品介绍：
            </td>
            <td>
                <textarea id="JieShao" name="JieShao" cols="60" rows="100" style="height: 100px" class="input-txt" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="left">
                产品图片：
            </td>
            <td>
                <uc1:ShangChuan02 runat="server" ID="ChanPinTuPian"  Multi="1"/>
            </td>
        </tr>
        <tr>
            <td align="left">
                发&nbsp;&nbsp;布&nbsp;&nbsp;人：
            </td>
            <td>
                <input id="FaBuRen" name="FaBuRen" type="text" class="w400 input-txt" runat="server" disabled />
            </td>            
        </tr>
        <tr>
            <td align="left">
                发布时间：
            </td>
            <td>
                <input id="FaBuTime" name="FaBuTime" type="text" class="w400 input-txt" runat="server" disabled />
            </td>
        </tr>
    </table>
    </form>
    <div class="alertbox-bot">
        <div class="alertbox-btn">
            <a href="#" class="blue_btn" id="Save">保&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;存</a> <a href="#" class="blue_btn" onclick="javascript:iPage.Close();">关&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;闭</a>
        </div>
    </div>

    <script type="text/javascript">
        var iPage = {
            ChanPinId: '<%=ChanPinId %>',
            iframeId: '<%=Request.QueryString["iframeId"] %>',
            Close: function() {
                top.Boxy.getIframeDialog(iPage.iframeId).hide();
                top.window.location.reload();
            },
            yanZhengForm: function() {
                if ($.trim($("#<%=Name.ClientID %>").val()).length < 1) { alert("请输入产品名称"); return false; }
                if ($.trim($("#<%=PinPai.ClientID %>").val()).length < 1) { alert("请输入产品品牌"); return false; }
                if ($.trim($("#<%=GuiGe.ClientID %>").val()).length < 1) { alert("请输入产品规格"); return false; }
                if ($.trim($("#<%=JiaGe1.ClientID %>").val()).length < 1) { alert("请输入市场价格"); return false; }
                if ($.trim($("#<%=DanWei.ClientID %>").val()).length < 1) { alert("请输入计量单位"); return false; }
                //if (eNow.getFloat($("#<%=JiaGe1.ClientID %>").val()) <= 0) { alert("请输入正确的市场价格"); return false; }
                if ($.trim($("#<%=JiaGe1.ClientID %>").val()).length > 0 && !eNow.regExps["isDecimal"].test($.trim($("#<%=JiaGe1.ClientID %>").val()))) { alert("请输入正确的市场价格"); return false; }

                return true;
            },
            Do: function(type) {
                if (!this.yanZhengForm()) return false;
                $.ajax({
                    type: "post",
                    url: "ChanPinEdit.aspx?do=" + type + "&ChanPinId=" + iPage.ChanPinId,
                    data: $("form").serialize(),
                    dataType: "json",
                    cache: false,
                    success: function(data) {
                        alert(data.msg);
                        if (data.result == "1") {
                            iPage.Close();
                        }
                    },
                    error: function() {
                        alert("服务器忙");
                    }
                });
            }
        };
        $(function() {
            $("#Save").unbind().bind("click", function() {
                iPage.Do("Save");
            });
        });
    </script>

</asp:Content>
