<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CaiGouMoBanEdit.aspx.cs" Inherits="EyouSoft.WEB.gk.CaiGouMoBanEdit" MasterPageFile="/mp/Boxy.Master" %>

<%@ Import Namespace="EyouSoft.Toolkit" %>
<%@ Import Namespace="EyouSoft.WEB.gk" %>
<%@ Import Namespace="EyouSoft.Model" %>
<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="body" runat="server" ContentPlaceHolderID="body">
    <form>
    <div class="alert_t">
        <%=string.IsNullOrEmpty(MoBanId)?"新增模版":"编辑模版"%></div>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="mt10">
        <tr>
            <td align="left" style="width: 100px;">
                模版名称：
            </td>
            <td>
                <input id="Name" name="Name" type="text" class="w400 input-txt" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="left">
                采&nbsp;&nbsp;购&nbsp;&nbsp;商：
            </td>
            <td>
                <input type="hidden" id="txt_gongsi_id" name="txt_gongsi_id" runat="server" />
                <input type="text" id="txt_gongsi_name" name="txt_gongsi_name" class="input-txt w400" runat="server" />
            </td>
        </tr>
    </table>
    <div class="alert_t2 mt10">
    </div>
    <table width="100%" align="center" cellpadding="0" cellspacing="0" class="mt10" id="table_chanpin">
        <tr>
            <th align="left">
                供应商
            </th>
            <th align="left">
                产品名称
            </th>
            <th align="left">
                产品规格
            </th>
            <th align="left">
                默认数量
            </th>
            <th align="left">
                操作
            </th>
        </tr>
        <asp:Repeater runat="server" ID="rpt">
            <ItemTemplate>
                <tr class="lxr_item">
                    <td align="left">
                        <input type="hidden" name="MingXiId" value="<%#Eval("Id") %>" />
                        <input type="hidden" name="GysId" value="<%#Eval("GysId") %>" />
                        <input type="hidden" name="ChanPinId" value="<%#Eval("ChanPinId") %>" />
                        <input name="GysName" type="text" class="input-txt" value="<%#Eval("GysName") %>" disabled /><a href="javascript:;" onclick="iPage.GysXuanYong(this)">选用</a>
                    </td>
                    <td align="left">
                        <input name="ChanPinName" type="text" class="input-txt formsize220" value="<%#Eval("ChanPinName") %>" disabled /><a href="javascript:;" onclick="iPage.ChanPinXuanYong(this)">选用</a>
                    </td>
                    <td align="left">
                        <input name="ChanPinGuiGe" type="text" class="input-txt" value="<%#Eval("GuiGe") %>" disabled>
                    </td>
                    <td align="left">
                        <input name="ShuLiang" type="text" class="input-txt formsize80" value="<%#Eval("ShuLiang","{0:F2}") %>" /><input name="DanWei" type="text" class="input-txt formsize50" value="<%#Eval("JiLiangDanWei") %>" disabled />
                    </td>
                    <td align="left">
                        <a href="javascript:void(0)" onclick="iPage.Delete(this);">删除</a>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <asp:PlaceHolder runat="server" ID="ph">
            <tr class="lxr_item">
                <td align="left">
                    <input type="hidden" name="MingXiId" />
                    <input type="hidden" name="GysId" />
                    <input type="hidden" name="ChanPinId" />
                    <input name="GysName" type="text" class="input-txt" disabled /><a href="javascript:;" onclick="iPage.GysXuanYong(this)">选用</a>
                </td>
                <td align="left">
                    <input name="ChanPinName" type="text" class="input-txt formsize220" disabled /><a href="javascript:;" onclick="iPage.ChanPinXuanYong(this)">选用</a>
                </td>
                <td align="left">
                    <input name="ChanPinGuiGe" type="text" class="input-txt" disabled>
                </td>
                <td align="left">
                    <input name="ShuLiang" type="text" class="input-txt formsize80" /><input name="DanWei" type="text" class="input-txt formsize50" disabled />
                </td>
                <td align="left">
                    <a href="javascript:void(0)" onclick="iPage.Delete(this);">删除</a>
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="5" style="text-align: left;">
                <a href="javascript:void(0)" id="chanpin_tianjia">添加</a>
            </td>
        </tr>
    </table>
    <div class="alert_t2 mt10">
    </div>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="mt10">
        <tr>
            <td align="left" style="width:100px;">
                发&nbsp;&nbsp;布&nbsp;&nbsp;人：
            </td>
            <td align="left">
                <input type="text" id="CaoZuoRen" class="formsize220 input-txt" runat="server" disabled />
            </td>
            <td align="left" style="width:100px;">
                发布时间：
            </td>
            <td align="left">
                <input type="text" id="IssueTime" class="formsize220 input-txt" runat="server" disabled />
            </td>
        </tr>
        <tr>
            <td colspan="7" style="color: red;">
                请选择要采购的产品，没有选择产品的采购模版项将不予保存。
            </td>
        </tr>
    </table>
    <div class="alertbox-bot">
        <div class="alertbox-btn">
            <a href="#" class="blue_btn" id="save">保&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;存</a> <a href="#" class="blue_btn" onclick="javascript:iPage.Close();">关&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;闭</a>
        </div>
    </div>
    </form>
    
    <link rel="stylesheet" href="/js/jquery-ui.1.11.1/themes/redmond/jquery-ui.css">
    <script type="text/javascript" src="/js/jquery-ui.1.11.1/jquery-ui.min.js"></script>
    <script type="text/javascript" src="/js/gongsi.autocomplete.js"></script>

    <script type="text/javascript">
        var iPage = {
            MoBanId: '<%=MoBanId %>',
            iframeId: '<%=Request.QueryString["iframeId"] %>',
            Close: function() {
                top.Boxy.getIframeDialog(iPage.iframeId).hide();
                top.window.location.reload();
            },
            Open: function(url, title, w, h, d) {
                top.Boxy.iframeDialog({
                    iframeUrl: url,
                    title: title,
                    modal: true,
                    width: w,
                    height: h,
                    data: d
                });
            },
            Add: function(o) {
                var _tr = $(o).closest("tr");
                var _new = _tr.clone();

                _new.find("input[name=MingXiId]").val("");
                _new.insertAfter(_tr);
            },
            Delete: function(o) {
                var _tr = $(o).closest("tr");
                var _l = $("#table_chanpin tr.lxr_item").length;

                if (_l == 1) {
                    alert("不能删除：至少要保留一行。"); return false;
                } else {
                    _tr.remove();
                }
            },
            GysXuanYong: function(o) {
                var _tr = $(o).closest("tr");
                var _index = $("tr.lxr_item").index(_tr);
                var _d = {};
                _d["refereriframeid"] = iPage.iframeId;
                _d["fncallback"] = "GongSiXuanYong_callback";
                _d["fnget"] = "XuanYong_get";
                _d["identityid"] = _index;

                iPage.Open("/commonpage/gysxuanyong.aspx", "供应商选用", "870px", "540px", _d);
            },
            ChanPinXuanYong: function(o) {
                var _tr = $(o).closest("tr");
                var _index = $("tr.lxr_item").index(_tr);
                var _d = {};
                _d["refereriframeid"] = iPage.iframeId;
                _d["fncallback"] = "ChanPinXuanYong_callback";
                _d["fnget"] = "XuanYong_get";
                _d["identityid"] = _index;
                _d["gysid"] = _tr.find("input[name=GysId]").val();

                iPage.Open("/commonpage/chanpinxuanyong.aspx", "产品选用", "870px", "540px", _d);
            },
            Do: function(type) {
                if ($.trim($("#<%=Name.ClientID %>").val()).length == 0) { alert("请填写模板名称"); return false; }
                if ($.trim($("#<%=txt_gongsi_id.ClientID %>").val()).length == 0) { alert("请选择采购商"); return false; }
                if ($.trim($("#<%=txt_gongsi_name.ClientID %>").val()).length == 0) { alert("请选择采购商"); return false; }

                if ($("tr.lxr_item").length == 0) { alert("至少要选择一个有效产品"); return false; }

                var _isYouXiao = false;
                $("tr.lxr_item").each(function() {
                    var _$tr = $(this);
                    var _gysId = $.trim(_$tr.find("input[name='GysId']").val());
                    var _chanPinId = $.trim(_$tr.find("input[name='ChanPinId']").val());

                    if (_gysId.length > 0 && _chanPinId.length > 0) { _isYouXiao = true; }
                });

                if (!_isYouXiao) { alert("至少要选择一个有效产品"); return false; }

                $.ajax({
                    type: "post",
                    url: "CaiGouMoBanEdit.aspx?do=" + type + "&mobanid=" + iPage.MoBanId,
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
            chanPinTianJia: function(obj) {
                var _$tr = $("#table_chanpin tr.lxr_item").eq(0).clone(true);

                _$tr.find("input").val("");

                _$tr.insertBefore($(obj).closest("tr"));

                $(window).scrollTop($(document).height())
            }
        };

        $(function() {
            $("#save").unbind().bind("click", function() { iPage.Do("Save"); });

            if ($("#<%=txt_gongsi_id.ClientID %>").length > 0 && "<%=MoBanId %>".length > 0) { $("#<%=txt_gongsi_name.ClientID %>").prop("readonly", true).addClass("readonly"); }
            gongSiAutocomplete.init({ txt_id_id: "<%=txt_gongsi_id.ClientID %>", txt_name_id: "<%=txt_gongsi_name.ClientID %>", callback: function(data) { }, gslx: "1", pplx: 1 });

            $("#chanpin_tianjia").click(function() { iPage.chanPinTianJia(this); });
        });
        
        
        function GongSiXuanYong_callback(data) {
            var _$tr = $("tr.lxr_item").eq(data.identityid);
            _$tr.find("input[name=GysId]").val(data.gongsiid);
            _$tr.find("input[name=GysName]").val(data.gongsiname);
            _$tr.find("input[name=ChanPinId]").val("");
            _$tr.find("input[name=ChanPinName]").val("");
            _$tr.find("input[name=ChanPinGuiGe]").val("");
            _$tr.find("input[name=DanWei]").val("");
        }
        function ChanPinXuanYong_callback(data) {
            var _$tr = $("tr.lxr_item").eq(data.identityid);
            _$tr.find("input[name=GysId]").val(data.gongsiid);
            _$tr.find("input[name=GysName]").val(data.gongsiname);
            _$tr.find("input[name=ChanPinId]").val(data.chanpinid);
            _$tr.find("input[name=ChanPinName]").val(data.chanpinname);
            _$tr.find("input[name=ChanPinGuiGe]").val(data.chanpinguige);
            _$tr.find("input[name=DanWei]").val(data.danwei);
        }
        function XuanYong_get(identityid) {
            var _$tr = $("tr.lxr_item").eq(identityid);
            var _data = {};

            _data["gongsiid"] = _$tr.find("input[name=GysId]").val();
            _data["chanpinid"] = _$tr.find("input[name=ChanPinId]").val();

            return _data;
        }
    </script>

</asp:Content>
