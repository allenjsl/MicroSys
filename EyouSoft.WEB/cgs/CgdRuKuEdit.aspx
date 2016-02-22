<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CgdRuKuEdit.aspx.cs" Inherits="EyouSoft.WEB.cgs.CgdRuKuEdit" MasterPageFile="~/mp/Boxy.Master" %>

<asp:Content ContentPlaceHolderID="body" runat="server" ID="body1">
    <form id="form1">
    <div class="alert_t">
        请选择采购单模板</div>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="mt10">
        <tr>
            <td align="left" style="width: 100px;">
                模板名称：
            </td>
            <td>
                <select name="txtMoBan" id="txtMoBan" class="input_select">
                    <option value="">-请选择采购模板-</option>
                    <asp:Literal runat="server" ID="ltrMoBanOption"></asp:Literal>
                </select>&nbsp;&nbsp;
                <input type="text" id="txtCaiGouDanName" name="txtCaiGouDanName" class="input-txt" style="width: 200px;" runat="server" />
            </td>
        </tr>        
    </table>
    <div class="alert_t2 mt10"></div>   
    
    <table width="100%" align="center" cellpadding="0" cellspacing="0" class="mt10" id="table_chanpin">
        <tr>
            <th align="left" style="width:245px;">
                产品名称
            </th>
            <th align="left">
                供应商
            </th>
            <th align="left" style="width:90px;">
                产品品牌
            </th> 
            <th align="left" style="width:90px;">
                产品规格
            </th>           
            <th align="left" style="width: 70px;">
                计量单位
            </th>
            <th align="right" style="width: 75px;">
                上次报价&nbsp;&nbsp;
            </th>
            <th align="center" style="width: 70px;">
                采购数量
            </th>            
            <th align="center" style="width:60px">
                <input type="checkbox" name="chk_quanxuan" id="chk_quanxuan" />
            </th>
        </tr>
        
        <asp:Repeater runat="server" ID="rpt">
        <ItemTemplate>
        <tr class="chanpin_item table_tr_item">
            <td>
                <input type="hidden" name="txt_chanpin_mignxiid" value="<%#Eval("MingXiId") %>" />
                <input type="hidden" name="txt_chanpin_id" value="<%#Eval("ChanPinId") %>" />
                <input type="hidden" name="txt_chanpin_gysid" value="<%#Eval("GysId") %>" />
                <input type="hidden" name="txt_chanpin_dingdanid" value="<%#Eval("DingDanId") %>" />
                <%#Eval("ChanPinName")%>
            </td>
            <td>                
                <%#Eval("GysName")%>
            </td>
            <td>                
                <%#Eval("ChanPinPinPai")%>
            </td>
            <td>
                <%#Eval("ChanPinGuiGe")%>
            </td>
            <td>
                <%#Eval("JiLiangDanWei")%>
            </td>
            <td style="text-align: right;">
                <%#Eval("ChanPinJiaGe","{0:F2}")%>&nbsp;&nbsp;
            </td>
            <td style="text-align:center;">
                <input name="txt_chanpin_shuliang" type="text" class="input-txt" style="width: 50px;" value="<%#Eval("ShuLiang","{0:F2}") %>" />
            </td>
            <td style="text-align:center;">
                <%--<a href="javascript:void(0)" onclick="iPage.chanPinShanChu(this);">删除</a>--%>
                <input type="hidden" name="txt_chanpin_xuanzhong" value="1" />
                <input type="checkbox" name="chk_chanpin_xuanzhong" checked="checked" />
            </td>
        </tr>
        </ItemTemplate>
        </asp:Repeater>
        
        <tr id="tr_chanpin_tishi">
            <td colspan="7" style="color:#666;">
                请选择采购模板，选择采购模板事将加载指定模板的采购信息。
            </td>
        </tr>
    </table>
    
    <div class="alert_t mt10">
        收货信息</div>
        
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="mt10">
        <tr>
            <td style="width:40px;">
                选择
            </td>
            <td style="width: 90px;">
                是否默认
            </td>
            <td style="width:90px;">
                姓名
            </td>
            <td style="width:100px;">
                电话
            </td>
            <td style="width:100px;">
                手机
            </td>
            <td>
                地址
            </td>            
        </tr>        
        <asp:Repeater runat="server" ID="rptDiZhi">
        <ItemTemplate>
        <tr data-class="tr_shouhuodizhi_item">
            <td>
                <input type="radio" name="radioDiZhi" id="radioDiZhi_<%#Eval("DiZhiId") %>" value="<%#Eval("DiZhiId") %>" />
            </td>
            <td>
                <%#(bool)Eval("IsMoRen") ?"是":"否"%>
            </td>
            <td>
                <%#Eval("Name") %>
            </td>
            <td>
                <%#Eval("DianHua") %>
            </td>
            <td>
                <%#Eval("ShouJi") %>
            </td>
            <td>
                <%#Eval("DiZhi") %>
            </td>            
        </tr>        
        </ItemTemplate>
        </asp:Repeater>       
        <asp:PlaceHolder runat="server" ID="phDiZhiEmpty" Visible="false">
        <tr><td colspan="5">暂无常用地址信息</td></tr>
        </asp:PlaceHolder> 
    </table>
        
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="mt10">
        <tr>
            <td align="left" style="width: 100px;">
                要求到货时间：
            </td>
            <td>
                <input type="text" id="txtYaoQiuDaoHuoTime" name="txtYaoQiuDaoHuoTime" class="input-txt" style="width: 200px;" runat="server" onfocus="WdatePicker();" />
            </td>
            <td align="left" style="width: 100px;">
                <span class="bitian">*</span>收货人姓名：
            </td>
            <td>
                <input type="text" id="txtShouHuoRenName" name="txtShouHuoRenName" class="input-txt" style="width: 200px;" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 100px;">
                <span class="bitian">*</span>收货人电话：
            </td>
            <td>
                <input type="text" id="txtShouHuoRenDianHua" name="txtShouHuoRenDianHua" class="input-txt" style="width: 200px;" runat="server" />
            </td>
            <td align="left" style="width: 100px;">
                <span class="bitian">*</span>收货地址：
            </td>
            <td>
                <input type="text" id="txtShouHuoDiZhi" name="txtShouHuoDiZhi" class="input-txt" style="width: 200px;" runat="server" />
            </td>
        </tr>
    </table>
    
    <div class="alert_t mt10">其它信息</div>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="mt10">
        <tr>
            <td align="left" style="width: 100px;">
                状态：
            </td>
            <td>
                <asp:Literal runat="server" ID="ltrCgdStatus">计划发布</asp:Literal>
            </td>
            <td align="left" style="width: 100px;">
                发布人：
            </td>
            <td>
                <asp:Literal runat="server" ID="ltrFaBuRenName"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td align="left">
                发布时间：
            </td>
            <td>
                <asp:Literal runat="server" ID="ltrFaBuTime"></asp:Literal>
            </td>
            <td align="left">
                采购部门：
            </td>
            <td>
                <asp:Literal runat="server" ID="ltrCanGouBuMenName"></asp:Literal>
            </td>            
        </tr>
    </table>
    
    <div class="alert_t2 mt10"></div>
    <div style="font-size: 12px; color: #666; height: 24px; line-height: 12px;" class="mt10">
        操作说明：所有采购产品采购数量合计不能为零，采购数量为零的产品信息不会进入采购单。未在供应商处采购过的产品上次报价显示为零。</div>
    
    <div class="alertbox-bot">
        <div class="alertbox-btn">
            <asp:PlaceHolder runat="server" ID="phBaoCun">
            <a href="javascript:void(0)" class="blue_btn" id="a_baocun">保 存</a>
            </asp:PlaceHolder>
            
            <asp:Literal runat="server" ID="ltrCaoZuoTiShi"></asp:Literal>            
        </div>
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
            initMoBan: function(moBanId) {
                if (moBanId.length == 0) return;

                function _callback(data) {
                    var _items = data.ChanPins;
                    if (_items == null || _items.length == 0) {
                        alert("你选择的模板信息暂不含采购产品信息，请选择其它采购模板");
                        return;
                    }

                    var s = [];
                    for (var i = 0; i < _items.length; i++) {
                        s.push('<tr class="chanpin_item table_tr_item">');
                        s.push('<td>');
                        s.push('<input type="hidden" name="txt_chanpin_mignxiid" value="" />');
                        s.push('<input type="hidden" name="txt_chanpin_dingdanid" value="" />')
                        s.push('<input type="hidden" name="txt_chanpin_id" value="' + _items[i].ChanPinId + '"/>');
                        s.push('<input type="hidden" name="txt_chanpin_gysid" value="' + _items[i].GysId + '"/>');
                        s.push(_items[i].ChanPinName)
                        s.push('</td>');
                        s.push('<td>' + _items[i].GysName + '</td>');
                        s.push('<td>' + _items[i].ChanPinPinPai + '</td>');
                        s.push('<td>' + _items[i].GuiGe + '</td>');
                        s.push('<td>' + _items[i].JiLiangDanWei + '</td>');
                        s.push('<td style="text-align:right;">' + _items[i].ChanPinJiaGe.toFixed(2) + '&nbsp;&nbsp;&nbsp;</td>');
                        s.push('<td style="text-align:center;"><input name="txt_chanpin_shuliang" type="text" class="input-txt" value="' + _items[i].ShuLiang.toFixed(2) + '" style="width:50px;"/></td>');
                        //s.push('<td style="text-align:right;"><a href="javascript:void(0)" onclick="iPage.chanPinShanChu(this);">删除</a></td>');
                        s.push('<td style="text-align:center"><input type="hidden" name="txt_chanpin_xuanzhong" value="0" />');
                        s.push('<input type="checkbox" name="chk_chanpin_xuanzhong" /></td>');
                        s.push('</tr>');
                    }
                    $("#table_chanpin").find("tr.chanpin_item").remove();
                    $("#tr_chanpin_tishi").hide();
                    $("#table_chanpin").append(s.join(''));
                }

                var _self = this;
                $.ajax({ type: "get", url: window.location.href + "&doType=getmobaninfo&mobanid=" + moBanId, cache: false, dataType: "json", async: false,
                    success: function(response) {
                        if (response.result == "1") {
                            _callback(response.obj);
                        }
                    }
                });
            },
            changeMoBan: function() {
                var _moBanId = $("#txtMoBan").val();

                if (_moBanId.length == 0) {
                    $("#<%=txtCaiGouDanName.ClientID %>").val("");
                    $("#table_chanpin").find("tr.chanpin_item").remove();
                    $("#tr_chanpin_tishi").show();
                    return;
                }

                $("#<%=txtCaiGouDanName.ClientID %>").val($("#txtMoBan").find("option:selected").text());

                this.initMoBan(_moBanId);
            },
            chanPinShanChu: function(obj) {
                if ("<%=CgdStatus %>" == "<%=(int)EyouSoft.Model.CaiGouDanStatus.已下单 %>") { alert("采购单已发布，不能删除，若需要调整采购单内容，请取消相应采购单重新采购。"); return false; }

                if ($("#table_chanpin tr.chanpin_item").length == 1) { alert("不能删除：至少要采购一件产品。"); return false; }

                if (!confirm("你确定要删除该采购产品吗？")) return false;
                $(obj).closest("tr").remove();
            },
            yanZhengForm: function() {
                if ($.trim($("#<%=txtCaiGouDanName.ClientID %>").val()).length < 1) { alert("请填写采购单名称"); return false; }
                if ($("#table_chanpin tr.chanpin_item").length == 0) { alert("至少需要采购一件产品"); return false; }

                var _xuanZhongJiShu = 0;
                $("input[name='chk_chanpin_xuanzhong']").each(function() {
                    if ($(this).prop("checked")) { $(this).closest("td").find("input[name='txt_chanpin_xuanzhong']").val("1"); _xuanZhongJiShu++; }
                    else $(this).closest("td").find("input[name='txt_chanpin_xuanzhong']").val("0");
                });

                if (_xuanZhongJiShu == 0) { alert("请选择需要采购的项"); return false; }

                var _shuLiangHeJi = 0;
                $("#table_chanpin tr.chanpin_item").each(function() {
                    if ($(this).find('input[name="chk_chanpin_xuanzhong"]').prop("checked"))
                        _shuLiangHeJi = eNow.yunSuan(_shuLiangHeJi, $(this).find('input[name="txt_chanpin_shuliang"]').val(), "+");
                });

                if (_shuLiangHeJi <= 0) { alert("所有采购商品的采购数量合计不能为零。"); return false; }

                if ($.trim($("#<%=txtShouHuoRenName.ClientID %>").val()).length < 1) { alert("请填写收获人姓名"); return false; }
                if ($.trim($("#<%=txtShouHuoRenDianHua.ClientID %>").val()).length < 1) { alert("请填写收获人电话"); return false; }
                if (!eNow.regExps["isTel"].test($.trim($("#<%=txtShouHuoRenDianHua.ClientID %>").val()))) { alert("请输入正确的收获人电话"); return false; }
                if ($.trim($("#<%=txtShouHuoDiZhi.ClientID %>").val()).length < 1) { alert("请填写收获地址"); return false; }

                return true;
            },
            baoCun: function(obj) {
                var yanZhengFormRetCode = this.yanZhengForm();
                if (!yanZhengFormRetCode) { return false; }
                var _self = this;
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
            changeDiZhi: function(obj) {
                var _$tr = $(obj).closest("tr");
                $("#<%=txtShouHuoRenName.ClientID %>").val($.trim(_$tr.find("td").eq(2).html()));
                $("#<%=txtShouHuoRenDianHua.ClientID %>").val($.trim(_$tr.find("td").eq(3).html()));
                if ($.trim(_$tr.find("td").eq(3).html()).length == 0) $("#<%=txtShouHuoRenDianHua.ClientID %>").val($.trim(_$tr.find("td").eq(4).html()));
                $("#<%=txtShouHuoDiZhi.ClientID %>").val($.trim(_$tr.find("td").eq(5).html()));
            },
            initMoRenDiZhi: function() {
                $("tr[data-class='tr_shouhuodizhi_item']").eq(0).find("input[name='radioDiZhi']").click();
            }
        };

        $(document).ready(function() {
            //$("#txtMoBan").val("<%=MoBanId %>");
            $("#txtMoBan").change(function() { iPage.changeMoBan(); });
            if ("<%=EditId %>".length == 0) { $("#txtMoBan").val("<%=MoRenMoBanId %>").change(); }
            if ("<%=EditId %>".length > 0) { $("#tr_chanpin_tishi").hide(); }

            $("#a_baocun").click(function() { iPage.baoCun(this); });

            $("input[name='radioDiZhi']").click(function() { iPage.changeDiZhi(this); });
            if ("<%=EditId %>".length > 0 && $.trim("<%=ShouHuoDiZhiId %>").length > 0) { $("#radioDiZhi_<%=ShouHuoDiZhiId %>").prop("checked", true); }
            if ("<%=EditId %>".length == 0) { iPage.initMoRenDiZhi(); }
            if ("<%=EditId %>".length > 0) { $("#chk_quanxuan").prop("checked", true); }

            $("#chk_quanxuan").click(function() { $("input[name='chk_chanpin_xuanzhong']").prop("checked", $(this).prop("checked")); });
        });
    </script>
</asp:Content>
