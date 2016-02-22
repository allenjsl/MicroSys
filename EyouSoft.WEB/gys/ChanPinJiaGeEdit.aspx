<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChanPinJiaGeEdit.aspx.cs" Inherits="EyouSoft.WEB.gys.ChanPinJiaGeEdit"  MasterPageFile="/mp/Boxy.Master"%>

<asp:Content ID="body" runat="server" ContentPlaceHolderID="body">            


  <div class="alert_t">产品信息</div>
  <form>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="mt10">
      <tr>
        <td align="right">产品编码：</td>
        <td><input id="BianMa" name="BianMa" type="text" class="formsize220 input-txt" runat="server" disabled/></td>
    </tr>
      <tr>
        <td align="right">产品名称：</td>
        <td><input id="Name" name="Name" type="text" class="formsize220 input-txt" runat="server" disabled/></td>
        <td align="right">产品品牌：</td>
        <td><input id="PinPai" name="PinPai" type="text" class="formsize220 input-txt" runat="server" disabled/></td>
    </tr>
      <tr>
        <td align="right">产品规格：</td>
        <td><input id="GuiGe" name="GuiGe" type="text" class="formsize220 input-txt" runat="server" disabled/></td>
        <td align="right">计量单位：</td>
        <td><input id="DanWei" name="DanWei" type="text" class="formsize220 input-txt" runat="server" disabled/></td>
    </tr>
      <tr>
        <td align="right">原&nbsp;&nbsp;报&nbsp;&nbsp;价：</td>
        <td><input id="JiaGe1" name="JiaGe1" type="text" class="formsize220 input-txt" runat="server" disabled/></td>
        <%if(1==0) %>
        <%{ %>
        <td align="right">被采购数：</td>
        <td><input id="ShuLiang" name="ShuLiang" type="text" class="formsize220 input-txt" runat="server" disabled/></td>
        <%} %>
    </tr>
      <tr>
        <td align="right">现&nbsp;&nbsp;报&nbsp;&nbsp;价：</td>
        <td colspan="3"><input id="JiaGe2" name="JiaGe2" type="text" class="formsize220 input-txt" runat="server"/></td>
    </tr>
      <tr>
        <td align="right">报&nbsp;&nbsp;价&nbsp;&nbsp;人：</td>
        <td><input id="FaBuRen" name="FaBuRen" type="text" class="formsize220 input-txt" runat="server" disabled/></td>
        <td align="right">报价时间：</td>
        <td><input id="FaBuTime" name="FaBuTime" type="text" class="formsize220 input-txt" runat="server" disabled/></td>
    </tr>
  </table>  
</form>
  <div class="alertbox-bot">
      <div class="alertbox-btn">
        <a href="#" class="blue_btn" id="Save">保&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;存</a>
        <a href="#" class="blue_btn" onclick="javascript:iPage.Close();">关&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;闭</a>
      </div>
  </div>
  
  <div class="alert_t">历史报价记录</div>
  
  <table width="100%" align="center" cellpadding="0" cellspacing="0" class="mt10">
      <tr>
        <th align="center">报价时间</th>
        <th align="center">报价人</th>
        <th align="center">原报价</th>
        <th align="center">新报价</th>
      </tr>
      <asp:Repeater runat="server" ID="rpt">
        <ItemTemplate>
      <tr>
        <td align="center"><%#Eval("IssueTime","{0:yyyy-MM-dd}")%></td>
        <td align="center"><%#Eval("CaoZuoRenName")%></td>
        <td align="center"><%#Eval("JiaGe1","{0:F2}")%>元</td>
        <td align="center"><%#Eval("JiaGe2","{0:F2}")%>元</td>
      </tr>
        </ItemTemplate>
      </asp:Repeater>
  </table>
  
<script type="text/javascript">
    var iPage = {
    	ChanPinId:'<%=ChanPinId %>',
    	iframeId:'<%=Request.QueryString["iframeId"] %>',
    	Close:function () {
    		top.Boxy.getIframeDialog(iPage.iframeId).hide();
    		top.window.location.reload();
    	},
    	Do:function (type) {
    		$.ajax({
    				type: "post",
    				url: "ChanPinJiaGeEdit.aspx?do="+type+"&ChanPinId="+iPage.ChanPinId,
    				data:$("form").serialize(),
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
