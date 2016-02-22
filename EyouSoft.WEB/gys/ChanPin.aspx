<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChanPin.aspx.cs" Inherits="EyouSoft.WEB.gys.ChanPin" MasterPageFile="~/mp/Gys.Master" %>
<%@ Import Namespace="EyouSoft.Toolkit" %>
<%@ Register Assembly="EyouSoft.Toolkit" Namespace="EyouSoft.Toolkit" TagPrefix="uc1" %>

<asp:Content ID="body" runat="server" ContentPlaceHolderID="body">
               <div class="right_bar">
                   <div class="right_box">
        				<!-- InstanceBeginEditable name="EditRegion4" -->                        
                       <div class="basicT">采购列表</div>
                      
                       <div class="searchbox fixed"><p>
                              产品名称：
                              <input type="text" class="formsize120 input-txt" name="txtChanPinName" value="<%=Utils.GetQueryStringValue("txtChanPinName") %>"/>
                              产品品牌：
                              <input type="text" class="formsize120 input-txt" name="txtPinPai" value="<%=Utils.GetQueryStringValue("txtPinPai") %>" />
                              产品规格：
                              <input type="text" class="formsize120 input-txt" name="txtGuiGe" value="<%=Utils.GetQueryStringValue("txtGuiGe") %>" />
                              
                              <%if(T==0) %>
                              <%{ %>
                              发布时间：
                              <input type="text" class="formsize80 input-txt" name="txtFaBuTime1" value="<%=Utils.GetQueryStringValue("txtFaBuTime1") %>" onfocus="WdatePicker()"/> - <input type="text" class="formsize80 input-txt" name="txtFaBuTime2" value="<%=Utils.GetQueryStringValue("txtFaBuTime2") %>" onfocus="WdatePicker()"/>
                              <%} %>
                              <input name="" type="submit" class="search-btn">
                            </p>
                       </div>

                              <%if(T==0) %>
                              <%{ %>
                       <div class="tablehead">
                            <ul class="fixed">
                               <li><a href="#" do="add">新增</a></li>
                          </ul>                
                       </div>
                              <%} %>

                       <!--列表表格-->
                       <div class="tablelist-box">
                           <table width="100%"  id="liststyle">
                                <tr>
                                  <th class="thinputbg">序号</th>
                                  <th align="center" >产品编号</th>
                                  <th align="center" >产品名称</th>
                                  <th align="center" >产品品牌</th>
                                  <th align="center" >产品规格</th>
                              <%if(T==0) %>
                              <%{ %>
                                  <th align="center" >市场价</th>
                                  <th align="center" >发布时间</th>
                                  <th align="center" >操作</th>
                              <%} %>

                              <%if(T==1) %>
                              <%{ %>
                                  <th align="center" >原报价</th>
                                  <th align="center" >现报价</th>
                                  <th align="center" >最新报价时间</th>
                                  <th align="center" >操作</th>
                              <%} %>
                              
                                </tr>
                                <asp:Repeater runat="server" ID="rpt">
                                <ItemTemplate>
                                  <tr data-id='<%#Eval("ChanPinId") %>' class="table_tr_item">
                                    <td align="center"><%#Container.ItemIndex+(intPageIndex-1)*intPageSize+1 %></td>
                                    <td align="center"><%#Eval("BianMa")%></td>
                                    <td align="center"><%#Eval("Name")%></td>
                                    <td align="center"><%#Eval("PinPai")%></td>
                                    <td align="center"><%#Eval("GuiGe")%></td>
                              <%if(T==0) %>
                              <%{ %>
                                    <td align="center"><%#Eval("JiaGe1","{0:F2}")%></td>
                                    <td align="center"><%#Eval("IssueTime", "{0:yyyy-MM-dd HH:mm}")%></td>
                                    <td align="center"><a href="#" class="blue_btn" do="edit">编辑</a><a href="#" class="blue_btn" do="delete">删除</a></td>
                              <%} %>
                              <%if(T==1) %>
                              <%{ %>
                                    <td align="center"><%#Eval("JiaGe1","{0:F2}")%></td>
                                    <td align="center"><%#Eval("JiaGe2", "{0:F2}")%></td>
                                    <td align="center"><%#Eval("IssueTime", "{0:yyyy-MM-dd HH:mm}")%></td>
                                    <td align="center"><a href="#" class="blue_btn" do="tiao">调整</td>
                              <%} %>
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
        Url: "ChanPinEdit.aspx",
        Add: function() {
            mpage.ShowBoxy({
                url: ipage.Url,
                title: "新增产品",
                width: "850px",
                height: "500px"
            });
        },
        Edit: function(o) {
            mpage.ShowBoxy({
                url: ipage.Url + "?chanpinid=" + $(o).closest("tr").attr("data-id"),
                title: "编辑产品",
                width: "850px",
                height: "500px"
            });
        },
        Delete: function(o) {
            if (!confirm("删除后不可恢复，你确定要删除吗？")) return false;
            var _$tr = $(o).closest("tr");
            var _data = { txtChanPinId: _$tr.attr("data-id") };
            $.ajax({ type: "post", url: "ChanPin.aspx?do=delete", dataType: "json", cache: false, data: _data,
                success: function(data) {
                    alert(data.msg);
                    window.location.reload();
                },
                error: function() {
                    alert("服务器忙");
                    window.location.reload();
                }
            });
        },
        Tiao: function(o) {
            mpage.ShowBoxy({
                url: "ChanPinJiaGeEdit.aspx?chanpinid=" + $(o).closest("tr").attr("data-id"),
                title: "产品价格调整",
                width: "750px",
                height: "900px"
            });
        }
    };
$(function() {
    var fenYePeiZhi = { pageSize: '<%=intPageSize %>', pageIndex: '<%=intPageIndex %>', recordCount: '<%=intRecordCount %>', showPrev: true, showNext: true, showDisplayText: true, showTiaoZhuan: true, cssClassName: 'page' };
    fenYe.replace("div_fenye", fenYePeiZhi);
    $("a[do=add]").unbind("click").bind("click", function() {
        ipage.Add();
    });
    $("a[do=edit]").unbind("click").bind("click", function() {
        ipage.Edit(this);
    });
    $("a[do=delete]").unbind("click").bind("click", function() {
        ipage.Delete(this);
    });
    $("a[do=tiao]").unbind("click").bind("click", function() {
        ipage.Tiao(this);
    });

    $("tr.table_tr_item:nth-child(odd)").addClass("odd");
});
</script>
</asp:Content>