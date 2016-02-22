<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CaiGouDingDan.aspx.cs" Inherits="EyouSoft.WEB.CommonPage.CaiGouDingDan" MasterPageFile="/mp/Boxy.Master" %>

<asp:Content ID="body" runat="server" ContentPlaceHolderID="body">            

    <form id="form1">

  <div class="alert_t"><%=LeiXing%></div>
  
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="mt10">
      <tr>
        <td align="left" style="width:100px;">采购单号：</td>
        <td style="width:400px;"><label id="CaiGouDanHao" runat="server"></label></td>
        <td align="left" style="width:100px;">采&nbsp;&nbsp;购&nbsp;&nbsp;商：</td>
        <td><%--<select id="CgsName" class="formsize220 input-txt" runat="server" disabled>
        </select>--%><asp:Literal runat="server" ID="ltrCgsName"></asp:Literal></td>
    </tr>
      <tr>
        <td align="left">采购名称：</td>
        <td><%--<select id="CaiGouDanName" class="formsize220 input-txt" runat="server" disabled>
        </select>--%><asp:Literal runat="server" ID="ltrCgdName"></asp:Literal></td>
        <td align="left">采购部门：</td>
        <td><%--<select id="CaiGouBuMen" class="formsize220 input-txt" runat="server" disabled>
        </select>--%><asp:Literal runat="server" ID="ltrCaiGouBuMen"></asp:Literal></td>
    </tr>
        <!--
      <tr>
        <td align="left">采购类型：</td>
        <td><select id="CaiGouLeiXing" class="formsize220 input-txt" runat="server" disabled>
        </select></td>
    </tr>-->
        <tr>
            <td align="left">
                采购状态：
            </td>
            <td colspan="3">
                <%--<select id="CaiGouStatus" class="formsize220 input-txt" runat="server" disabled>
        </select>--%><asp:Literal runat="server" ID="ltrCgStatus"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td align="left" >
                采购人：
            </td>
            <td style="width: 400px;">
                <%--<input type="text" id="CaiGouRen" class="formsize220 input-txt" runat="server" disabled/>--%><asp:Literal runat="server" ID="ltrCgRenName"></asp:Literal>
            </td>
            <td align="left">
                采购时间：
            </td>
            <td>
                <%--<input type="text" id="CaiGouTime" class="formsize220 input-txt" runat="server" disabled/>--%><asp:Literal runat="server" ID="ltrCgTime"></asp:Literal>
            </td>
        </tr>
  </table>
 
  <table width="100%" align="center" cellpadding="0" cellspacing="0" class="mt10">
      <tr>
        <th align="left">产品名称</th>
        <%--<th align="left">供应商</th>--%>
        <th align="left" style="width: 120px;">产品规格</th>
        <th align="left" style="width:80px;">采购数量</th>
        <th align="right" style="width: 80px;">上次报价&nbsp;&nbsp;</th>
        <th align="right" style="width: 80px;">单价&nbsp;&nbsp;</th>
        <%if (IsXianShiShiJiDaoHuoShuLiang)
          { %>
        <th align="right" style="width: 120px;">实际到货数量&nbsp;&nbsp; </th>
        <%} %>
        <th align="right" style="width: 80px;">小计&nbsp;&nbsp;</th>
      </tr>
      <asp:Repeater runat="server" ID="rpt">
        <ItemTemplate>
      <tr data-chanpinjiage1="<%#Eval("ChanPinJiaGe1","{0:F2}") %>" data-chanpinitem="1">
        <td align="left">
            <input type="hidden" name="MingXiId" value="<%#Eval("MingXiId") %>"/>
            <%--<input name="ChanPinName" type="text" class="input-txt" value="<%#Eval("ChanPinName") %>" disabled>--%>
            <%#Eval("ChanPinName") %>
        </td>
        <%--<td align="left"><input name="GysName" type="text" class="input-txt" value="<%=GysName %>" disabled></td>--%>
        <td align="left"><%--<input name="ChanPinGuiGe" type="text" class="input-txt" value="<%#Eval("ChanPinGuiGe") %>" disabled>--%><%#Eval("ChanPinGuiGe") %></td>
        <td align="left"><%--<input name="ShuLiang" type="text" class="input-txt formsize50" value="<%#Eval("ShuLiang","{0:F2}") %>" <%=T == 0 && DingDanStatus == EyouSoft.Model.DingDanStatus.采购申请?"readonly":"readonly" %>><label><%#Eval("JiLiangDanWei")%></label>--%>
            <%#Eval("ShuLiang","{0:F2}") %><input name="ShuLiang" type="hidden" value="<%#Eval("ShuLiang","{0:F2}") %>"><label><%#Eval("JiLiangDanWei")%></label>
        </td>
        <td align="right"><%#Eval("ChanPinJiaGe1","{0:F2}") %>&nbsp;&nbsp;</td>
        <td align="right"><input name="ChanPinJiaGe" style="text-align:right; padding-right:10px;" type="text" class="input-txt formsize50" value="<%#Eval("ChanPinJiaGe","{0:F2}") %>" <%=T == 0 && DingDanStatus == EyouSoft.Model.DingDanStatus.采购申请?"":"readonly" %>></td>
        <%if (IsXianShiShiJiDaoHuoShuLiang)
          { %>
        <td align="right"><%#Eval("DaoHuoShuLiang","{0:F2}")%>&nbsp;&nbsp;</td>
        <%} %>
        <td align="right"><span class="xiaoji"><%#Eval("JinE","{0:F2}") %></span>&nbsp;&nbsp;<%--<input name="JinE" type="text" class="input-txt formsize50" value="<%#Eval("JinE","{0:F2}") %>" readonly>--%></td>
      </tr>
        </ItemTemplate>
        <FooterTemplate>
      <tr>
        <td align="right" colspan="<%=IsXianShiShiJiDaoHuoShuLiang?6:5 %>">合计：</td>
        <td align="right">
            <span id="span_heji"><%=HeJi.ToString("F2") %></span>&nbsp;&nbsp;<%--<input name="Heji" type="text" class="input-txt formsize50" id="Heji" value="<%=HeJi.ToString("F2") %>" readonly><label>元</label>--%></td>
      </tr>
        </FooterTemplate>
      </asp:Repeater>
  </table>
  
  <div class="alert_t">收货人信息</div>
  
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="mt10">
      <tr>
        <td align="left" style="width:100px;">收&nbsp;&nbsp;货&nbsp;&nbsp;人：</td>
        <td style="width: 400px;"><%--<input type="text" id="ShouHuoRen" class="formsize220 input-txt" runat="server" disabled/>--%><asp:Literal runat="server" ID="ltrSHRName"></asp:Literal></td>
        <td align="left" style="width:100px;">联系电话：</td>
        <td><%--<input type="text" id="LianXiTel" class="formsize220 input-txt" runat="server" disabled/>--%><asp:Literal runat="server" ID="ltrSHRDianHua"></asp:Literal></td>
    </tr>
      <tr>
        <td align="left">收货地址：</td>
        <td colspan="3"><%--<input type="text" id="ShouHuoDiZhi" class="w400 input-txt" runat="server" disabled/>--%><asp:Literal runat="server" ID="ltrSHDiZhi"></asp:Literal></td>
    </tr>
  </table>

  <% if (T == 1||T==2) %>
  <% { %>
  <div class="alert_t">发货信息</div>
  
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="mt10">
        <tr>
            <td style="width: 40px;">
                选择
            </td>
            <td style="width: 90px;">
                是否默认
            </td>
            <td style="width: 90px;">
                姓名
            </td>
            <td style="width: 100px;">
                电话
            </td>
            <td style="width: 100px;">
                手机
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
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <asp:PlaceHolder runat="server" ID="phDiZhiEmpty" Visible="false">
            <tr>
                <td colspan="5">
                    暂无发货人信息
                </td>
            </tr>
        </asp:PlaceHolder>
    </table>
  
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="mt10">
      <tr>
        <td align="left" style="width:100px;">
            <span class="bitian">*</span>送货人：</td>
        <td style="width:400px;"><input type="text" id="SongHuoRen" class="formsize220 input-txt" runat="server"/></td>
        <td align="left" style="width:100px;">
            <span class="bitian">*</span>联系电话：</td>
        <td><input type="text" id="SongHuoTel" class="formsize220 input-txt" runat="server"/></td>
    </tr>
      <tr>
        <td align="left">
            <span class="bitian">*</span>发货时间：</td>
        <td><input type="text" id="FaHuoTime" class="formsize220 input-txt" runat="server" onfocus="WdatePicker()"/></td>
        <td align="left">预计到货时间：</td>
        <td><input type="text" id="DaoHuoTime" class="formsize220 input-txt" runat="server" onfocus="WdatePicker()"/></td>
    </tr>
    <tr>
        <td align="left">发货说明：</td>
        <td colspan="3">
        <textarea id="txtFaHuoShuoMing" rows="3" cols="80" runat="server"></textarea>
        </td>
    </tr>
  </table>
  <% } %>

  <div class="alertbox-bot">
      <div class="alertbox-btn">
      <% if (T == 0 && DingDanStatus == EyouSoft.Model.DingDanStatus.采购申请) %>
      <% { %>
            <a href="javascript:void(0)" class="blue_btn" id="QueRen">确认报价</a>
            <a href="#" class="blue_btn" id="QuXiao">取消采购</a>
      <% } %>
      <% if (T == 1 && DingDanStatus == EyouSoft.Model.DingDanStatus.采购商确认报价) %>
      <% { %>
            <a href="javascript:void(0)" class="blue_btn" id="QueRen">确认发货</a>
      <% } %>
      <% if (T == 2 && DingDanStatus == EyouSoft.Model.DingDanStatus.采购商确认收货 && QueRenStatus == EyouSoft.Model.QueRenStatus.未确认) %>
      <% { %>
            <a href="javascript:void(0)" class="blue_btn" id="QueRen">确认到货</a>
      <% } %>      
        
      </div>
  </div>
  
  </form>
  
<script type="text/javascript">
    var iPage = {
        DingDanId: '<%=Request.QueryString["DingDanId"] %>',
        iframeId: '<%=Request.QueryString["iframeId"] %>',
        Close: function() {
            top.Boxy.getIframeDialog(iPage.iframeId).hide();
            top.window.location.reload();
        },
        Do: function(type) {
            if ($.trim($("#<%=SongHuoTel.ClientID %>").val()).length > 0 && !eNow.regExps["isTel"].test($.trim($("#<%=SongHuoTel.ClientID %>").val()))) { alert("请输入正确的送货人联系电话"); return false; }

            var _isHeLiJiaGe = true;
            $('tr[data-chanpinitem="1"]').each(function() {
                var _$tr = $(this);
                var _txtJiaGe = $.trim(_$tr.find("input[name='ChanPinJiaGe']").val());
                if (_txtJiaGe.length == 0) _isHeLiJiaGe = false;
                if (!eNow.regExps["isDecimal"].test(_txtJiaGe)) _isHeLiJiaGe = false;
                if (eNow.getFloat(_txtJiaGe) <= 0) _isHeLiJiaGe = false;
            });

            if (!_isHeLiJiaGe) { alert("请输入正确的单价信息"); return false; }

            $.ajax({
                type: "post",
                url: "CaiGouDingDan.aspx?T=<%=T %>&Status=<%=(int)DingDanStatus %>&QueRen=<%=(int)QueRenStatus %>&do=" + type + "&DingDanId=" + iPage.DingDanId,
                dataType: "json",
                cache: false,
                data: $("#form1").serialize(),
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
        heJi: function() {
            var _heJi = 0;
            $('tr[data-chanpinitem="1"]').each(function() {
                var _$tr = $(this);
                var _sl = eNow.getFloat(_$tr.find("input[name='ShuLiang']").val());
                var _dj = eNow.getFloat(_$tr.find("input[name='ChanPinJiaGe']").val());
                var _xj = eNow.yunSuan(_sl, _dj, "*");
                var _dj1 = eNow.getFloat(_$tr.attr("data-chanpinjiage1"));
                _$tr.find("span.xiaoji").html(_xj.toFixed(2));

                _heJi = eNow.yunSuan(_heJi, _xj, "+");

                if (_dj > _dj1) {
                    _$tr.css({ "background": "#ff0000" });
                }
                if (_dj < _dj1) {
                    _$tr.css({ "background": "green" });
                }
                if (_dj == _dj1) {
                    _$tr.css({ "background": "" });
                }
            });
            $("#span_heji").html(_heJi.toFixed(2));
        },
        changeDiZhi: function(obj) {
            var _$tr = $(obj).closest("tr");
            $("#<%=SongHuoRen.ClientID %>").val($.trim(_$tr.find("td").eq(2).html()));
            $("#<%=SongHuoTel.ClientID %>").val($.trim(_$tr.find("td").eq(3).html()));
            if ($.trim(_$tr.find("td").eq(3).html()).length == 0) $("#<%=SongHuoTel.ClientID %>").val($.trim(_$tr.find("td").eq(4).html()));
        },
        initMoRenDiZhi: function() {
            $("tr[data-class='tr_shouhuodizhi_item']").eq(0).find("input[name='radioDiZhi']").click();
        }
    };
    $(function() {
        $("#QueRen").unbind().bind("click", function() {
            iPage.Do("QueRen");
        });
        $("#QuXiao").unbind().bind("click", function() {
            iPage.Do("QuXiao");
        });
        $("input[name='ChanPinJiaGe']").change(function() { iPage.heJi(); })

        iPage.heJi();

        $("input[name='radioDiZhi']").click(function() { iPage.changeDiZhi(this); });
        if ("<%=DingDanId %>".length > 0 && $.trim("<%=SongHuoRenId %>").length > 0) { $("#radioDiZhi_<%=SongHuoRenId %>").prop("checked", true); }
        if ("<%=DingDanId %>".length == 0) { iPage.initMoRenDiZhi(); }
    });
</script>
</asp:Content>
