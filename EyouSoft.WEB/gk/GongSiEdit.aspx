<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GongSiEdit.aspx.cs" Inherits="EyouSoft.WEB.gk.GongSiEdit" MasterPageFile="/mp/Boxy.Master" %>

<%@ Register Src="~/wuc/ShangChuan02.ascx" TagName="ShangChuan02" TagPrefix="uc1" %>

<asp:Content ID="body" runat="server" ContentPlaceHolderID="body">
    <form>
    <div class="alert_t">
        公司信息</div>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="mt10">
        <tr>
            <td align="left" width="20%">
                <span class="bitian">*</span>公司名称：
            </td>
            <td>
                <input id="Name" name="Name" type="text" class="w400 input-txt" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="left">
                <span class="bitian">*</span>法人代表：
            </td>
            <td>
                <input id="FanRenName" name="FanRenName" type="text" class="w400 input-txt" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="left">
                <span class="bitian">*</span>公司地址：
            </td>
            <td>
                <input id="DiZhi" name="DiZhi" type="text" class="w400 input-txt" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="left">
                营业执照：
            </td>
            <td>
                <uc1:ShangChuan02 runat="server" ID="YingYeZhiZhaoFilepath" />
            </td>
        </tr>
        <tr>
            <td align="left">
                组织机构代码：
            </td>
            <td>
                <uc1:ShangChuan02 runat="server" ID="ZuZhiJiGouFilepath" />
            </td>
        </tr>
        <tr>
            <td align="left">
                联系QQ：
            </td>
            <td>
                <input id="txtLxQQ" name="txtLxQQ" type="text" class="w400 input-txt" runat="server" />
            </td>
        </tr>
    </table>
    <div class="alert_t">
        联系人信息</div>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="mt10">
        <tr>
            <td align="left" width="20%">
                <span class="bitian">*</span>负 责 人：
            </td>
            <td>
                <input id="FuZeRenName" name="FuZeRenName" type="text" class="w400 input-txt" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="left">
                <span class="bitian">*</span>联系电话：
            </td>
            <td>
                <input id="FuZeRenDianHua" name="FuZeRenDianHua" type="text" class="w400 input-txt" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="left">
                身份证号：
            </td>
            <td>
                <input id="FuZeRenShenFenZhengHao" name="FuZeRenShenFenZhengHao" type="text" class="w400 input-txt" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="left">
                照片：
            </td>
            <td>
                <uc1:ShangChuan02 runat="server" ID="FuZeRenZhaoPianFilepath" />
            </td>
        </tr>
        <tr style="border-top: 1px solid #2cb4ff;">
            <td align="left">
                财 务：
            </td>
            <td>
                <input id="CaiWuName" name="CaiWuName" type="text" class="w400 input-txt" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="left">
                联系电话：
            </td>
            <td>
                <input id="CaiWuDianHua" name="CaiWuDianHua" type="text" class="w400 input-txt" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="left">
                身份证号：
            </td>
            <td>
                <input id="CaiWuShenFenZhengHao" name="CaiWuShenFenZhengHao" type="text" class="w400 input-txt" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="left">
                照片：
            </td>
            <td>
                <uc1:ShangChuan02 runat="server" ID="CaiWuZhaoPianFilepath" />
            </td>
        </tr>
        <tr style="border-top: 1px solid #2cb4ff;">
            <td align="left">
                发 布 人：
            </td>
            <td>
                <input id="CaoZuoRen" name="CaoZuoRen" type="text" class="w400 input-txt" runat="server" disabled />
            </td>            
        </tr>
        <tr>
            <td align="left">
                发布时间：
            </td>
            <td>
                <input id="IssueTime" name="IssueTime" type="text" class="w400 input-txt" runat="server" disabled />
            </td>
        </tr>
    </table>
    </form>
    <div class="alertbox-bot">
        <div class="alertbox-btn">
            <a href="javascript:void(0)" class="blue_btn" id="Save">保&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;存</a>
            <asp:PlaceHolder runat="server" ID="phShenHe" Visible="false">
            <a href="javascript:void(0)" class="blue_btn" id="a_shenhe">审&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;核</a>
            </asp:PlaceHolder>
        </div>
    </div>

    <script type="text/javascript">
        var iPage = {
            GongSiId: '<%=GongSiId %>',
            iframeId: '<%=Request.QueryString["iframeId"] %>',
            Close: function() {
                top.Boxy.getIframeDialog(iPage.iframeId).hide();
                top.window.location.reload();
            },
            reload: function() {
                window.location.href = window.location.href;
            },
            yanZhengForm: function() {
                if ($.trim($("#<%=Name.ClientID %>").val()).length < 1) { alert("请输入公司名称"); return false; }
                if ($.trim($("#<%=FanRenName.ClientID %>").val()).length < 1) { alert("请输入法人代表"); return false; }
                if ($.trim($("#<%=DiZhi.ClientID %>").val()).length < 1) { alert("请输入公司地址"); return false; }
                if ($.trim($("#<%=FuZeRenName.ClientID %>").val()).length < 1) { alert("请输入负责人"); return false; }
                if ($.trim($("#<%=FuZeRenDianHua.ClientID %>").val()).length < 1) { alert("请输入负责人电话"); return false; }

                if ($.trim($("#<%=FuZeRenDianHua.ClientID %>").val()).length > 0 && !eNow.regExps["isTel"].test($.trim($("#<%=FuZeRenDianHua.ClientID %>").val()))) { alert("请输入正确的负责人电话"); return false; }
                if ($.trim($("#<%=FuZeRenShenFenZhengHao.ClientID %>").val()).length > 0 && !eNow.regExps["isSFZ"].test($.trim($("#<%=FuZeRenShenFenZhengHao.ClientID %>").val()))) { alert("请输入正确的负责人身份证号"); return false; }

                if ($.trim($("#<%=CaiWuDianHua.ClientID %>").val()).length > 0 && !eNow.regExps["isTel"].test($.trim($("#<%=CaiWuDianHua.ClientID %>").val()))) { alert("请输入正确的财务电话"); return false; }
                if ($.trim($("#<%=CaiWuShenFenZhengHao.ClientID %>").val()).length > 0 && !eNow.regExps["isSFZ"].test($.trim($("#<%=CaiWuShenFenZhengHao.ClientID %>").val()))) { alert("请输入正确的财务身份证号"); return false; }

                return true;
            },
            Do: function(type) {
                if (!this.yanZhengForm()) return false;
                $.ajax({
                    type: "post",
                    url: "GongSiEdit.aspx?T=<%=(int)T %>&do=" + type + "&GongSiId=" + iPage.GongSiId,
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
            },
            shenHe: function(obj) {
                if (!confirm("审核操作不可逆，你确定要审核该公司信息吗？")) return false;

                $(obj).unbind("click").css({ "color": "#999999" });

                var _self = this;
                $.ajax({ type: "POST", url: "gongsiedit.aspx?doType=shenhe", data: { txtGongSiId: '<%=GongSiId %>' }, cache: false, dataType: "json", async: false,
                    success: function(response) {
                        alert(response.msg);
                        if (response.result == "1") {
                            _self.reload();
                        } else {
                            $(obj).bind("click", function() { _self.shenHe(obj); }).css({ "color": "" });
                        }
                    }
                });
            }
        };
        $(function() {
            $("#Save").unbind().bind("click", function() { iPage.Do("Save"); });

            $("#a_shenhe").click(function() { iPage.shenHe(this); });
        });
    </script>

</asp:Content>
