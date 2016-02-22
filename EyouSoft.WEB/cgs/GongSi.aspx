<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GongSi.aspx.cs" Inherits="EyouSoft.WEB.cgs.GongSi" Title="公司信息-系统设置" MasterPageFile="~/mp/Cgs.Master" %>

<%@ Register Src="~/wuc/ShangChuan02.ascx" TagName="ShangChuan02" TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="PageBody" ID="PageBody1" runat="server">
    <div class="right_box">
        <form id="form1">
        <div class="alertbox-outbox">
            <div class="alert_t">
                公司信息&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#ff0000;"><asp:Literal runat="server" ID="ltrTiShiXiaoXi"></asp:Literal></span></div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="mt10">
                <tr>
                    <td align="left" style="width: 120px;">
                        <span class="bitian">*</span>公司名称：
                    </td>
                    <td style="width: 40%">
                        <input type="text" class="input-txt w400" id="txtGongSiName" name="txtGongSiName" runat="server" />
                    </td>
                    <td align="left" style="width: 120px;">
                        <span class="bitian">*</span>法人代表：
                    </td>
                    <td>
                        <input type="text" class="input-txt w400" id="txtFaRenName" name="txtFaRenName" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <span class="bitian">*</span>公司地址：
                    </td>
                    <td>
                        <input type="text" class="input-txt w400" id="txtDiZhi" name="txtDiZhi" runat="server" />
                    </td>
                     <td align="left" >
                        联系QQ：
                    </td>
                    <td>
                        <input type="text" class="input-txt w400" id="txtLxQQ" name="txtLxQQ" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <span class="bitian">*</span>营业执照：
                    </td>
                    <td>
                        <uc1:ShangChuan02 runat="server" ID="txtYingYeZhiZhao" />                        
                    </td>
                    <td align="left">
                        <span class="bitian">*</span>组织机构代码：
                    </td>
                    <td>
                        <uc1:ShangChuan02 runat="server" ID="txtZuZhiJiGou" />
                    </td>
                </tr>
            </table>
            
            <div class="alert_t">
                负责人信息</div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="mt10">
                <tr>
                    <td align="left" style="width: 120px;">
                        <span class="bitian">*</span>负责人：
                    </td>
                    <td style="width: 40%">
                        <input type="text" class="input-txt w400" id="txtFuZeRenName" name="txtFuZeRenName" runat="server" />
                    </td>
                    <td align="left" style="width: 120px;">
                        <span class="bitian">*</span>联系电话：
                    </td>
                    <td>
                        <input type="text" class="input-txt w400" id="txtFuZeRenDianHua" name="txtFuZeRenDianHua" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        身份证号：
                    </td>
                    <td>
                        <input type="text" class="input-txt w400" id="txtFuZeRenShenFenZhengHao" name="txtFuZeRenShenFenZhengHao" runat="server" />
                    </td>
                    <td align="left">
                        照片：
                    </td>
                    <td>
                        <uc1:ShangChuan02 runat="server" ID="txtFuZeRenZhaoPian" />
                    </td>
                </tr>
            </table>
            
            <div class="alert_t">
                财务信息</div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="mt10">
                <tr>
                    <td align="left" style="width: 120px;">
                        财务：
                    </td>
                    <td style="width: 40%">
                        <input type="text" class="input-txt w400" id="txtCaiWuName" name="txtCaiWuName" runat="server" />
                    </td>
                    <td align="left" style="width: 120px;">
                        联系电话：
                    </td>
                    <td>
                        <input type="text" class="input-txt w400" id="txtCaiWuDianHua" name="txtCaiWuDianHua" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        身份证号：
                    </td>
                    <td>
                        <input type="text" class="input-txt w400" id="txtCaiWuShenFenZhengHao" name="txtCaiWuShenFenZhengHao" runat="server" />
                    </td>
                    <td align="left">
                        照片：
                    </td>
                    <td>
                        <uc1:ShangChuan02 runat="server" ID="txtCaiWuZhaoPian" />
                    </td>
                </tr>
            </table>
            
            <div style="text-align: center;" class="alertbox-bot mt20">
                <a class="blue_btn" href="javascript:void(0)" id="a_baocun">保存</a></div>
        </div>
        </form>
    </div>

    <script src="/uploadify3_2_1/jquery.uploadify.js" type="text/javascript"></script>
    <link href="/uploadify3_2_1/uploadify.css" rel="stylesheet" type="text/css" />
  
    <script type="text/javascript">
        var iPage = {
            reload: function() {
                window.location.href = window.location.href;
            },
            yanZhengForm: function() {
                if ($.trim($("#<%=txtGongSiName.ClientID %>").val()).length < 1) { alert("请输入公司名称"); return false; }
                if ($.trim($("#<%=txtFaRenName.ClientID %>").val()).length < 1) { alert("请输入法人代表"); return false; }
                if ($.trim($("#<%=txtDiZhi.ClientID %>").val()).length < 1) { alert("请输入公司地址"); return false; }

                if ($("input[name='<%=txtYingYeZhiZhao.FilepathClientName %>']").length == 0 && $("input[name='<%=txtYingYeZhiZhao.YuanFilepathClientName %>']").length == 0) { alert("请上传营业执照"); return false; }
                if ($("input[name='<%=txtZuZhiJiGou.FilepathClientName %>']").length == 0 && $("input[name='<%=txtZuZhiJiGou.YuanFilepathClientName %>']").length == 0) { alert("请上传组织机构代码"); return false; }

                if ($.trim($("#<%=txtFuZeRenName.ClientID %>").val()).length < 1) { alert("请输入负责人"); return false; }
                if ($.trim($("#<%=txtFuZeRenDianHua.ClientID %>").val()).length < 1) { alert("请输入负责人电话"); return false; }

                if ($.trim($("#<%=txtFuZeRenDianHua.ClientID %>").val()).length > 0 && !eNow.regExps["isTel"].test($.trim($("#<%=txtFuZeRenDianHua.ClientID %>").val()))) { alert("请输入正确的负责人电话"); return false; }
                if ($.trim($("#<%=txtFuZeRenShenFenZhengHao.ClientID %>").val()).length > 0 && !eNow.regExps["isSFZ"].test($.trim($("#<%=txtFuZeRenShenFenZhengHao.ClientID %>").val()))) { alert("请输入正确的负责人身份证号"); return false; }

                if ($.trim($("#<%=txtCaiWuDianHua.ClientID %>").val()).length > 0 && !eNow.regExps["isTel"].test($.trim($("#<%=txtCaiWuDianHua.ClientID %>").val()))) { alert("请输入正确的财务电话"); return false; }
                if ($.trim($("#<%=txtCaiWuShenFenZhengHao.ClientID %>").val()).length > 0 && !eNow.regExps["isSFZ"].test($.trim($("#<%=txtCaiWuShenFenZhengHao.ClientID %>").val()))) { alert("请输入正确的财务身份证号"); return false; }

                return true;
            },
            baoCun: function(obj) {
                if (!this.yanZhengForm()) return false;
                $(obj).unbind("click").css({ "color": "#999999" });

                var _self = this;
                $.ajax({ type: "POST", url: "gongsi.aspx?doType=baocun", data: $("#form1").serialize(), cache: false, dataType: "json", async: false,
                    success: function(response) {
                        alert(response.msg);
                        if (response.result == "1") {
                            _self.reload();
                        } else {
                            $(obj).bind("click", function() { _self.baoCun(obj); }).css({ "color": "" });
                        }
                    }
                });
            }
        };

        $(document).ready(function() {
            $("#a_baocun").click(function() { iPage.baoCun(this); });
        });
    </script>
</asp:Content>
