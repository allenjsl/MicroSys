<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CgdDingDanEdit.aspx.cs" Inherits="EyouSoft.WEB.gk.CgdDingDanEdit" MasterPageFile="~/mp/Boxy.Master" %>

<asp:Content ContentPlaceHolderID="body" runat="server" ID="body1">
    <form id="form1">
    <div class="alert_t">
        采购单信息</div>
        
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="mt10">
        <tr>
            <td align="left" style="width: 100px;">
                采购单号：
            </td>
            <td style="width: 319px;">
                <asp:Literal runat="server" ID="ltrCaiGouDanHao"></asp:Literal>
            </td>
            <td align="left" style="width: 100px;">
                采购单名称：
            </td>
            <td>
                <asp:Literal runat="server" ID="ltrCaiGouDanName"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 100px;">
                采购人：
            </td>
            <td>
                <asp:Literal runat="server" ID="ltrCaiGouRenName"></asp:Literal>
            </td>
            <td align="left" style="width: 100px;">
                采购部门：
            </td>
            <td>
                <asp:Literal runat="server" ID="ltrCaiGouBuMenName"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 100px;">
                供应商：
            </td>
            <td>
                <asp:Literal runat="server" ID="ltrGysName"></asp:Literal>
            </td>
            <td align="left" style="width: 100px;">
                采购状态：
            </td>
            <td>
                <asp:Literal runat="server" ID="ltrStatus"></asp:Literal>
            </td>
        </tr>        
    </table>
    <div class="alert_t2 mt10"></div>
    
    <table width="100%" align="center" cellpadding="0" cellspacing="0" class="mt10" id="table_chanpin">
        <tr>
            <th align="left" style="width: 245px;">
                产品名称
            </th>
            <th align="left" style="width: 120px;">
                产品规格
            </th>
            <th align="left" style="width: 80px;">
                计量单位
            </th>
            <th align="right" style="width: 80px;">
                单价&nbsp;&nbsp;&nbsp;
            </th>
            <th align="right" style="width: 120px;">
                采购数量&nbsp;&nbsp;&nbsp;
            </th>
            <th align="right" style="width: 80px;">
                小计&nbsp;&nbsp;&nbsp;
            </th>
            <%if (IsXianShiShiJiDaoHuoShuLiang)
              { %>
            <th align="left" style="width: 120px;">
                实际到货数量
            </th>
            <%} %>
        </tr>
        <asp:Repeater runat="server" ID="rpt">
        <ItemTemplate>
        <tr class="chanpin_item table_tr_item">
            <td>
                <input type="hidden" name="txt_chanpin_mignxiid" value="<%#Eval("MingXiId") %>" />
                <%#Eval("ChanPinName")%>
            </td>
            <td>
                <%#Eval("ChanPinGuiGe")%>
            </td>
            <td>
                <%#Eval("JiLiangDanWei")%>
            </td>
            <td style="text-align: right;">
                <%#Eval("ChanPinJiaGe","{0:F2}")%>&nbsp;&nbsp;&nbsp;
            </td>
            <td style="text-align: right;">
                <%#Eval("ShuLiang","{0:F2}") %>&nbsp;&nbsp;&nbsp;
            </td>
            <td style="text-align: right;">
                <%#Eval("JinE","{0:F2}") %>&nbsp;&nbsp;&nbsp;
            </td>
            <%if (IsXianShiShiJiDaoHuoShuLiang)
              { %>
            <td style="text-align: left;">
                <input name="txt_chanpin_daohuoshuliang" type="text" class="input-txt" style="width: 50px;" value="<%#GetShiJiDaoHuoShuLiang(Eval("ShuLiang"),Eval("FaHuoShuLiang"),Eval("DaoHuoShuLiang")) %>" />
            </td>
            <%} %>
        </tr>
        </ItemTemplate>
        </asp:Repeater>    
        <tr>
            <td colspan="5" style="text-align:right;">合计：</td>
            <td style="text-align:right;"><asp:Literal runat="server" ID="ltrHeJiJinE"></asp:Literal>&nbsp;&nbsp;&nbsp;</td>
            <%if (IsXianShiShiJiDaoHuoShuLiang)
              { %>
            <td></td>
            <%} %>
        </tr> 
    </table>
    
    <asp:PlaceHolder runat="server" ID="phShouHuo">
    <div class="alert_t mt10">
        收货信息</div>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="mt10">
        <tr>
            <td align="left" style="width: 100px;">
                要求到货时间：
            </td>
            <td style="width:319px;">
                <asp:Literal runat="server" ID="ltrYaoQiuDaoHuoTime"></asp:Literal>                
            </td>
            <td align="left" style="width: 100px;">
                收货人姓名：
            </td>
            <td>
                <asp:Literal runat="server" ID="ltrShouHuoRenName"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 100px;">
                收货人电话：
            </td>
            <td>
                <asp:Literal runat="server" ID="ltrShouHuoRenDianHua"></asp:Literal>
            </td>
            <td align="left" style="width: 100px;">
                收货地址：
            </td>
            <td>
                <asp:Literal runat="server" ID="ltrShouHuoDiZhi"></asp:Literal>
            </td>
        </tr>
    </table>
    </asp:PlaceHolder>
    
    <asp:PlaceHolder runat="server" ID="phFaHuo" Visible="false">
    <div class="alert_t mt10">
        发货信息</div>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="mt10">
        <tr>
            <td align="left" style="width: 100px;">
                实际发货时间：
            </td>
            <td style="width: 319px;">
                <asp:Literal runat="server" ID="ltrFaHuoTime"></asp:Literal>
            </td>
            <td align="left" style="width: 100px;">
                预计到货时间：
            </td>
            <td> 
                <asp:Literal runat="server" ID="ltrYuJiDaoHuoTime"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 100px;">
                送货人姓名：
            </td>
            <td>
                <asp:Literal runat="server" ID="ltrSongHuoRenName"></asp:Literal>
            </td>
            <td align="left" style="width: 100px;">
                送货人电话：
            </td>
            <td>
                <asp:Literal runat="server" ID="ltrSongHuoRenDianHua"></asp:Literal>
            </td>
        </tr>
        
    </table>
    </asp:PlaceHolder>
    
    <asp:PlaceHolder runat="server" ID="phDaoHuo" Visible="false">
    <div class="alert_t mt10">
        到货信息</div>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="mt10">
        <tr>
            <td align="left" style="width: 100px;">
                实际到货时间：
            </td>
            <td style="width: 319px;">
                <input type="text" id="txtShiJiDaoHuoTime" name="txtYaoQiuDaoHuoTime" class="input-txt" style="width: 200px;" runat="server" onfocus="WdatePicker();" />
            </td>
            <td align="left" style="width: 100px;">
                到货确认人：
            </td>
            <td>
                <input type="text" id="txtDaoHuoQueRenRenName" name="txtDaoHuoQueRenRenName" class="input-txt" style="width: 200px;" runat="server" />
            </td>
        </tr>
    </table>
    </asp:PlaceHolder>
    
    <asp:PlaceHolder runat="server" ID="phFuKuan" Visible="false">
    <div class="alert_t mt10">
        付款信息</div>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="mt10">
        <tr>
            <td align="left" style="width: 100px;">
                付款时间：
            </td>
            <td style="width: 319px;">
                <asp:Literal runat="server" ID="ltrFuKuanTime"></asp:Literal>
            </td>
            <td align="left" style="width: 100px;">
                付款人姓名：
            </td>
            <td>
                <asp:Literal runat="server" ID="ltrFuKuanCaoZuoRenName"></asp:Literal>
            </td>
        </tr>
    </table>
    </asp:PlaceHolder>
    
    <div class="alertbox-bot">
        <div class="alertbox-btn">            
            <asp:Literal runat="server" ID="ltrCaoZuo"></asp:Literal>
        </div>
    </div>
    </form>
    
    <script type="text/javascript">
        var iPage = {
            reload: function() {
                window.location.href = window.location.href;
            },
            quXiao: function(obj) {
                if (!confirm("该操作不可撤销，你确定要取消吗？")) return false;

                $(obj).unbind("click").css({ "color": "#999999" });
                var _self = this;
                $.ajax({ type: "get", url: window.location.href + "&doType=quxiao", cache: false, dataType: "json", async: false,
                    success: function(response) {
                        alert(response.msg);
                        _self.reload();
                    }
                });
            },
            queRenBaoJia: function(obj) {
                if (!confirm("该操作不可撤销，你确定要确认报价吗？")) return false;

                $(obj).unbind("click").css({ "color": "#999999" });
                var _self = this;
                $.ajax({ type: "get", url: window.location.href + "&doType=querenbaojia", cache: false, dataType: "json", async: false,
                    success: function(response) {
                        alert(response.msg);
                        _self.reload();
                    }
                });
            },
            queRenShouHuo: function(obj) {
                if (!confirm("该操作不可撤销，你确定要确认收获吗？")) return false;

                $(obj).unbind("click").css({ "color": "#999999" });
                var _self = this;
                $.ajax({ type: "post", url: window.location.href + "&doType=querenshouhuo", data: $("#form1").serialize(), cache: false, dataType: "json", async: false,
                    success: function(response) {
                        alert(response.msg);
                        _self.reload();
                    }
                });
            }
        };

        $(document).ready(function() {
            $("#a_quxiao").click(function() { iPage.quXiao(this); });
            $("#a_querenbaojia").click(function() { iPage.queRenBaoJia(this); });
            $("#a_querenshouhuo").click(function() { iPage.queRenShouHuo(this); });
        });
    </script>
</asp:Content>
