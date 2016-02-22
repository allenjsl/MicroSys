<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GysXuanYong.aspx.cs" Inherits="EyouSoft.WEB.CommonPage.GysXuanYong" MasterPageFile="/mp/Boxy.Master" %>
<%@ Import Namespace="EyouSoft.Toolkit" %>

<asp:Content runat="server" ID="head" ContentPlaceHolderID="head"></asp:Content>

<asp:Content runat="server" ID="body" ContentPlaceHolderID="body">
  <div class="alert_t">供应商选用</div>
  <form method="GET" action="GysXuanYong.aspx">
        <div class="searchbox fixed">
            <p>
            <input type="hidden" name="identityid" id="identityid" />
            <input type="hidden" name="fncallback" id="fncallback" />
            <input type="hidden" name="fnget" id="fnget" />
            <input type="hidden" id="iframeId" name="iframeId"/>
            <input type="hidden" id="refereriframeid" name="refereriframeid"/>
            供&nbsp;&nbsp;应&nbsp;&nbsp;商：<input id="Name" name="Name" type="text" class="formsize220 input-txt"/>
            <input type="submit" class="search-btn" id="Submit1" />
            </p>
        </div>
  </form>
    <div class="alert_t2 mt10"></div>   
 
  <table width="100%" align="center" cellpadding="0" cellspacing="0" class="mt10">
      <tr>
        <th class="thinputbg">
            序号
        </th> 
        <th align="center">供应商</th>
        <th align="center">法人代表</th>
        <th align="center">负责人</th>
        <th align="center">联系电话</th>
      </tr>
      <asp:Repeater runat="server" ID="rpt">
        <ItemTemplate>
          <tr>
            <td align="center">
                <input type="radio" name="chk" value="<%#Eval("GongSiId") %>" data-name="<%#Eval("Name") %>" />
                <%# Container.ItemIndex + 1+( this.pageIndex - 1) * this.pageSize%>
            </td>
            <td align="center"><%#Eval("Name") %></td>
            <td align="center"><%#Eval("FanRenName") %></td>
            <td align="center"><%#Eval("FuZeRenName")%></td>
            <td align="center"><%#Eval("FuZeRenDianHua")%></td>
          </tr>
        </ItemTemplate>
      </asp:Repeater>
      <asp:PlaceHolder runat="server" ID="ph">
        <tr>
            <td class="even" colspan="8" style="height: 30px; text-align: center;">
                暂无供应商信息。
            </td>
        </tr>
      </asp:PlaceHolder>
  </table>
  
    <div class="page_box" id="div_fenye">
    </div>

  <div class="alertbox-bot">
      <div class="alertbox-btn">
        <a href="#" class="blue_btn" id="xuanyong">选&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;用</a>
        <a href="#" class="blue_btn" onclick="javascript:iPage.Close();">关&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;闭</a>
      </div>
  </div>
    <script type="text/javascript" src="/js/fenye.js"></script>
  <script>
    var iPage = {
    	Close: function() {
    		top.Boxy.getIframeDialog($("#iframeId").val()).hide();
    	},
    	getWindow: function() {
    		var _window = null;
    		var _refereriframeid = $("#refereriframeid").val();
    		if (_refereriframeid.length > 0) {
    			_window = top.Boxy.getIframeWindow(_refereriframeid);
    		} else {
    			_window = top.window;
    		}

    		return _window;
    	},
    	XuanYong: function() {
    		var _chked = $("input[name=chk]:checked");
    		var _options = { };
    		var _window = iPage.getWindow();

    		_options["identityid"] = $("#identityid").val();
    		_options["gongsiid"] = _chked.val();
    		_options["gongsiname"] = _chked.attr("data-name");
    		
    		_window[$("#fncallback").val()](_options);

    		iPage.Close();
    	}
    };
    $(function() {
    	var fenYePeiZhi = { pageSize: '<%=pageSize %>', pageIndex: '<%=pageIndex %>', recordCount: '<%=recordCount %>', showPrev: true, showNext: true, showDisplayText: true, showTiaoZhuan: true, cssClassName: 'page' }
    	fenYe.replace("div_fenye", fenYePeiZhi);

    	utilsUri.initChaXun();

    	var _fnget = $("#fnget").val();
    	var _identityid = $("#identityid").val();
    	var _window = iPage.getWindow();

    	var _data = _window[_fnget](_identityid);

    	if (_data.gongsiid.length > 0) {
    		$("input[value=" + _data.gongsiid + "]").attr("checked", "checked");
    	}

    	$("#xuanyong").unbind("click").bind("click", function() { iPage.XuanYong(); });
    });
  </script>
</asp:Content>
