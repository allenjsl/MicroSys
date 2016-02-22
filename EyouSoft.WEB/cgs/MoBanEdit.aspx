<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MoBanEdit.aspx.cs" Inherits="EyouSoft.WEB.cgs.MoBanEdit" MasterPageFile="~/mp/Boxy.Master" %>

<asp:Content ContentPlaceHolderID="body" runat="server" ID="body1">
    <form id="form1">
    <div class="alert_t">采购模板信息</div>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="mt10">
        <tr>
            <td align="left" style="width: 100px;">
                模板名称：
            </td>
            <td>
                <input type="text" id="txtMoBanName" name="txtMoBanName" class="input-txt w400" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 100px;">
                发布人：
            </td>
            <td>
                <asp:Literal runat="server" ID="ltrCaoZuoRenName"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 100px;">
                发布时间：
            </td>
            <td>
                <asp:Literal runat="server" ID="ltrCaoZuoTime"></asp:Literal>
            </td>
        </tr>
    </table>
    <div class="alert_t2 mt10"></div>
    
    <table width="100%" align="center" cellpadding="0" cellspacing="0" class="mt10" id="table_chanpin">
        <tr>
            <th align="left">
                供应商
            </th>
            <th align="left" style="width: 245px;">
                产品名称
            </th>
            <th align="left" style="width: 120px;">
                产品规格
            </th>            
            <th align="left" style="width: 100px;">
                采购数量
            </th>
            <th align="left" style="width: 80px;">
                计量单位
            </th>
            <th align="left" style="width: 40px">
                操作
            </th>
        </tr>
        <asp:Repeater runat="server" ID="rpt">
            <ItemTemplate>
                <tr class="chanpin_item table_tr_item">
                    <td>
                        <input type="hidden" name="txt_moban_gysid" value="<%#Eval("GysId") %>" />
                        <input type="hidden" name="txt_moban_chanpinid" value="<%#Eval("ChanPinId") %>" />
                        <input type="text" readonly="readonly" name="txt_moban_gysname" class="input-txt" style="width: 150px;"  value="<%#Eval("GysName") %>"/>
                        <a href="javascript:void(0)" class="xuanyong_gys">选用</a>
                    </td>
                    <td>
                        <input type="text" readonly="readonly" name="txt_moban_chanpinname" class="input-txt" style="width: 150px;" value="<%#Eval("ChanPinName") %>" />
                        <a href="javascript:void(0)" class="xuanyong_chanpin">选用</a>
                    </td>
                    <td class="guige">
                        <%#Eval("GuiGe")%>
                    </td>                    
                    <td style="text-align: left;">
                        <input name="txt_moban_shuliang" type="text" class="input-txt" style="width: 50px;" value="<%#Eval("ShuLiang","{0:F2}") %>" />
                    </td>
                    <td class="danwei">
                        <%#Eval("JiLiangDanWei")%>
                    </td>
                    <td style="text-align: left;">                       
                        <a href="javascript:void(0)" class="chanpin_shanchu">删除</a>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr>
            <td colspan="6" style="text-align:left;">
                <a href="javascript:void(0)" id="chanpin_tianjia">添加</a>
            </td>
        </tr>
    </table>
    <div class="alert_t2 mt10">
    </div>
    <div style="font-size: 12px; color: #666; height: 24px; line-height: 12px;" class="mt10">
        操作说明：未选择产品的采购项将不会保存。</div>
    
    <div class="alertbox-bot">
        <div class="alertbox-btn">
            <asp:PlaceHolder runat="server" ID="phBaoCun"><a href="javascript:void(0)" class="blue_btn" id="a_baocun">保 存</a> </asp:PlaceHolder>
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
            chanPinTianJia: function(obj) {
                var _$tr = $("#table_chanpin tr.chanpin_item").eq(0).clone(true);

                _$tr.find("input").val("");
                _$tr.find("td.guige").html("");
                _$tr.find("td.danwei").html("");

                _$tr.insertBefore($(obj).closest("tr"));

                $(window).scrollTop($(document).height())
            },
            chanPinShanChu: function(obj) {
                if ($("#table_chanpin tr.chanpin_item").length == 1) { alert("不能删除：至少要保留一行。"); return false; }
                $(obj).closest("tr").remove();
            },
            xuanYongGYS: function(obj) {
                var _tr = $(obj).closest("tr");
                var _index = $("#table_chanpin tr.chanpin_item").index(_tr);
                var _data = {};
                _data["refereriframeid"] = '<%=EyouSoft.Toolkit.Utils.GetQueryStringValue("iframeId") %>';
                _data["fncallback"] = "GongSiXuanYong_callback";
                _data["fnget"] = "XuanYong_get";
                _data["identityid"] = _index;

                top.Boxy.iframeDialog({ title: "选用供应商", iframeUrl: "/commonpage/gysxuanyong.aspx", width: "870px", height: "540px", data: _data, afterHide: function() { } });
            },
            xuanYongChanPin: function(obj) {
                var _tr = $(obj).closest("tr");
                var _index = $("#table_chanpin tr.chanpin_item").index(_tr);
                var _data = {};
                _data["refereriframeid"] = '<%=EyouSoft.Toolkit.Utils.GetQueryStringValue("iframeId") %>';
                _data["fncallback"] = "ChanPinXuanYong_callback";
                _data["fnget"] = "XuanYong_get";
                _data["identityid"] = _index;
                _data["gysid"] = _tr.find("input[name='txt_moban_gysid']").val();

                top.Boxy.iframeDialog({ title: "选用产品", iframeUrl: "/commonpage/chanpinxuanyong.aspx", width: "870px", height: "540px", data: _data, afterHide: function() { } });
            },
            yanZhengForm: function() {
                if ($.trim($("#<%=txtMoBanName.ClientID %>").val()).length < 1) { alert("请填写模板名称"); return false; }
                if ($("#table_chanpin tr.chanpin_item").length == 0) { alert("至少要选择一个有效产品"); return false; }

                var _isYouXiao = false;
                $("#table_chanpin tr.chanpin_item").each(function() {
                    var _$tr = $(this);
                    var _gysId = $.trim(_$tr.find("input[name='txt_moban_gysid']").val());
                    var _chanPinId = $.trim(_$tr.find("input[name='txt_moban_chanpinid']").val());

                    if (_gysId.length > 0 && _chanPinId.length > 0) { _isYouXiao = true; }
                });

                if (!_isYouXiao) { alert("至少要选择一个有效产品"); return false; }

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
            }
        };

        function GongSiXuanYong_callback(data) {
            var _$tr = $("#table_chanpin tr.chanpin_item").eq(data.identityid);
            _$tr.find("input[name='txt_moban_gysid']").val(data.gongsiid);
            _$tr.find("input[name='txt_moban_gysname']").val(data.gongsiname);
            _$tr.find("input[name='txt_moban_chanpinid']").val("");
            _$tr.find("input[name='txt_moban_chanpinname']").val("");            
            _$tr.find("td.guige").html("");
            _$tr.find("td.danwei").html("");
        }
        
        function ChanPinXuanYong_callback(data) {
            var _$tr = $("#table_chanpin tr.chanpin_item").eq(data.identityid);
            _$tr.find("input[name='txt_moban_gysid']").val(data.gongsiid);
            _$tr.find("input[name='txt_moban_gysname']").val(data.gongsiname);
            _$tr.find("input[name='txt_moban_chanpinid']").val(data.chanpinid);
            _$tr.find("input[name='txt_moban_chanpinname']").val(data.chanpinname);            
            _$tr.find("td.guige").html(data.chanpinguige);
            _$tr.find("td.danwei").html(data.danwei);
        }
        
        function XuanYong_get(identityid) {
            var _$tr = $("#table_chanpin tr.chanpin_item").eq(identityid);
            var _data = {};

            _data["gongsiid"] = _$tr.find("input[name='txt_moban_gysid']").val();
            _data["chanpinid"] = _$tr.find("input[name='txt_moban_chanpinid']").val();

            return _data;
        }

        $(document).ready(function() {
            $("#chanpin_tianjia").click(function() { iPage.chanPinTianJia(this); });
            $(".chanpin_shanchu").click(function() { iPage.chanPinShanChu(this); });
            $(".xuanyong_gys").click(function() { iPage.xuanYongGYS(this); });
            $(".xuanyong_chanpin").click(function() { iPage.xuanYongChanPin(this); });

            $("#a_baocun").click(function() { iPage.baoCun(this); });
        });
    </script>
</asp:Content>
