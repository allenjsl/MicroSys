<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EyouSoft.WEB.gys.Default" MasterPageFile="~/mp/Gys.Master" %>
<%@ Import Namespace="EyouSoft.Model" %>
<%@ Register Assembly="EyouSoft.Toolkit" Namespace="EyouSoft.Toolkit" TagPrefix="uc1" %>

<asp:Content ID="body" runat="server" ContentPlaceHolderID="body">
               <div class="right_bar">
                   <div class="right_box">
        				<!-- InstanceBeginEditable name="EditRegion4" -->
                       <div class="basicT">采购列表</div>
                      
                       <div class="searchbox fixed"><p>
                              采购单号：
                              <input type="text" class="formsize120 input-txt" id="txtCaiGouDanHao" name="txtCaiGouDanHao" value="<%=Utils.GetQueryStringValue("txtCaiGouDanHao") %>"/>
                              采购时间：
                              <input type="text" class="formsize80 input-txt" id="txtFaBuTime1" name="txtFaBuTime1" value="<%=Utils.GetQueryStringValue("txtFaBuTime1") %>" onfocus="WdatePicker()"/> - <input type="text" class="formsize80 input-txt" id="txtFaBuTime2" name="txtFaBuTime2" value="<%=Utils.GetQueryStringValue("txtFaBuTime2") %>" onfocus="WdatePicker()"/>
                              采购商：<input type="text" class="formsize120 input-txt" id="txtCgsName" name="txtCgsName" value="<%=Utils.GetQueryStringValue("txtCgsName") %>" />
                              <input name="" type="submit" class="search-btn">
                            </p>
                       </div>

                       <%--<div class="tablehead">
                            <ul class="fixed">
                               <li><a href="">新增</a></li>
                               <li><a href="#">批量修改</a></li>
                               <li><a href="#">批量发布</a></li>
                          </ul>                
                       </div>--%>

                       <!--列表表格-->
                       <div class="tablelist-box">
                           <table width="100%"  id="liststyle">
                                <tr>
                                  <th class="thinputbg">序号</th>
                                  <th align="center" >采购单号</th>
                                  <th align="center" >采购名称</th>
                                  <th align="center" >采购商</th>
                                  <th align="center" >采购部门</th>
                                  <th align="center" >采购人</th>
                                  <% switch (T)%>
                                  <%   {%>
                                  <%       case 0:%>
                                              <th align="center" >采购时间</th>
                                              <th align="center" >报价状态</th>
                                              <th align="center" >操作</th>
                                  <%       break;%>
                                  <%       case 1:%>
                                              <th align="center" >要求发货时间</th>
                                              <th align="center" >发货状态</th>
                                              <th align="center" >操作</th>
                                  <%       break;%>
                                  <%       case 2:%>
                                              <th align="center" >送货时间</th>
                                              <th align="center" >收货人</th>
                                              <th align="center" >收货时间</th>
                                              <th align="center" >到货状态</th>
                                              <th align="center" >操作</th>
                                  <%       break;%>
                                  <%       case 3:%>
                                              <th align="center" >联系方式</th>
                                              <th align="center" >采购项数</th>
                                              <th align="center" >报价费用</th>
                                              <th align="center" >发货时间</th>
                                              <th align="center" >报价状态</th>
                                              <th align="center" >操作</th>
                                  <%       break;%>
                                  <%       case 4:%>
                                              <th align="center" >采购项数</th>
                                              <th align="center" >收货人</th>
                                              <th align="center" >到货时间</th>
                                              <th align="center" >送货人</th>
                                              <th align="center" >联系方式</th>
                                              <th align="center" >配送状态</th>
                                              <th align="center" >操作</th>
                                  <%       break;%>
                                  <%   } %>
                                </tr>
                                <asp:Repeater runat="server" ID="rpt">
                                <ItemTemplate>
                                  <tr data-id='<%#Eval("DingDanId") %>' class="table_tr_item">
                                    <td align="center"><%#Container.ItemIndex+(intPageIndex-1)*intPageSize+1 %></td>
                                    <td align="center"><%#Eval("CaiGouDanHao")%></td>
                                    <td align="center"><%#Eval("CaiGouDanName")%></td>
                                    <td align="center"><%#Eval("CgsName")%><%#EyouSoft.Toolkit.Utils.GetLxQQ(Eval("CgsLxQQ")) %></td>
                                    <td align="center"><%#Eval("CaiGouBuMen")%></td>
                                    <td align="center"><%#Eval("CaoZuoRenName")%></td>
                                  <% switch (T)%>
                                  <%   {%>
                                  <%       case 0:%>
                                            <td align="center"><%#Eval("IssueTime","{0:yyyy-MM-dd HH:mm}")%></td>
                                            <td align="center"><%#(int)Eval("Status") == (int)DingDanStatus.采购申请 ? "未报价" : "已报价"%></td>
                                            <td align="center"><a href="#" class="blue_btn"><%#(int)Eval("Status") == (int)DingDanStatus.采购申请 ? "报价" : "查看"%></a></td>
                                  <%       break;%>
                                  <%       case 1:%>
                                            <td align="center"><%#Eval("YaoQiuDaoHuoTime", "{0:yyyy-MM-dd HH:mm}")%></td>
                                            <td align="center"><%#(int)Eval("Status") == (int)DingDanStatus.采购商确认报价 ? "未发货" : "已发货"%></td>
                                            <td align="center"><a href="#" class="blue_btn"><%#(int)Eval("Status") == (int)DingDanStatus.采购商确认报价 ? "发货" : "查看"%></a></td>
                                  <%       break;%>
                                  <%       case 2:%>
                                            <td align="center"><%#Eval("SongHuoTime", "{0:yyyy-MM-dd HH:mm}")%></td>
                                            <td align="center"><%#Eval("CgsShouHuoRen")%></td>
                                            <td align="center"><%#Eval("CgsShouHuoTime", "{0:yyyy-MM-dd HH:mm}")%></td>
                                            <td align="center"><%#(int)Eval("Status") == (int)DingDanStatus.采购商确认收货 && (int)Eval("GysDaoHuoQueRenStatus") == (int)QueRenStatus.未确认 ? "未收货" : "已收货"%></td>
                                            <td align="center"><a href="#" class="blue_btn"><%#(int)Eval("Status") == (int)DingDanStatus.采购商确认收货 && (int)Eval("GysDaoHuoQueRenStatus") == (int)QueRenStatus.未确认 ? "确认" : "查看"%></a></td>
                                  <%       break;%>
                                  <%       case 3:%>
                                            <td align="center"><%#this.GetLianXiFangShi(Eval("CaoZuoRenId"))%></td>
                                            <td align="center"><%#Eval("CaiGouChanPinXiangShu")%></td>
                                            <td align="center"><%#Eval("JinE", "{0:F2}")%></td>
                                            <td align="center"><%#Eval("GysFaHuoTime","{0:yyyy-MM-dd}")%></td>
                                            <td align="center"><%#(int)Eval("Status") == (int)DingDanStatus.采购商确认报价? "采购确认" : "发货完成"%></td>
                                            <td align="center"><a href="#" class="blue_btn"><%#(int)Eval("Status") == (int)DingDanStatus.采购商确认报价? "发货确认" : "查看"%></a></td>
                                  <%       break;%>
                                  <%       case 4:%>
                                            <td align="center"><%#Eval("CaiGouChanPinXiangShu")%></td>
                                            <td align="center"><%#Eval("CgsShouHuoRen")%></td>
                                            <td align="center"><%#Eval("DaoHuoTime", "{0:yyyy-MM-dd}")%></td>
                                            <td align="center"><%#Eval("SongHuoRenName")%></td>
                                            <td align="center"><%#Eval("SongHuoRenDianHua")%></td>
                                            <td align="center"><%#(int)Eval("Status") == (int)DingDanStatus.采购商确认收货 && (int)Eval("GysDaoHuoQueRenStatus") == (int)QueRenStatus.未确认 ? "配送中" : "已送达"%></td>
                                            <td align="center"><a href="#" class="blue_btn"><%#(int)Eval("Status") == (int)DingDanStatus.采购商确认收货 && (int)Eval("GysDaoHuoQueRenStatus") == (int)QueRenStatus.未确认 ? "确认收货" : "查看"%></a></td>
                                  <%       break;%>
                                  <%   } %>
                                  </tr>
                                </ItemTemplate>
                                </asp:Repeater>
                                  
                           </table>
                       </div>
                       <!--列表表格--------end-->
                       
                       <!---------分页---->
        <div class="page_box" id="div_fenye">
        </div>
                       <!---------分页----------end--->
                            
                            
                            
						<!-- InstanceEndEditable -->
                   </div>
               </div>	
    <script type="text/javascript" src="/js/fenye.js"></script>
<script type="text/javascript">
var ipage = {
	CaoZuo: function(o) {
		mpage.ShowBoxy({
				url: "/CommonPage/CaiGouDingDan.aspx?T=<%=T %>&dingdanid="+$(o).closest("tr").attr("data-id"),
				title: "采购单",
				width: "950px",
				height: "600px"
			});
	}
};
$(function() {
    var fenYePeiZhi = { pageSize: '<%=intPageSize %>', pageIndex: '<%=intPageIndex %>', recordCount: '<%=intRecordCount %>', showPrev: true, showNext: true, showDisplayText: true, showTiaoZhuan: true, cssClassName: 'page' };
    fenYe.replace("div_fenye", fenYePeiZhi);
    $(".blue_btn").unbind("click").bind("click", function() {
        ipage.CaoZuo(this);
    });

    $("tr.table_tr_item:nth-child(odd)").addClass("odd");
});
</script>
</asp:Content>
