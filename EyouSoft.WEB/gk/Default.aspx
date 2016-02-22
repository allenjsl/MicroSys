<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EyouSoft.WEB.gk.Default" MasterPageFile="/mp/Gk.Master" %>
<%@ Import Namespace="EyouSoft.Toolkit" %>

<asp:Content runat="server" ID="head" ContentPlaceHolderID="head">

</asp:Content>

<asp:Content runat="server" ID="body" ContentPlaceHolderID="body">
   <div class="right_bar">
       <div class="right_box">
			<!-- InstanceBeginEditable name="EditRegion4" -->            
        <div class="basicT">
            <%=T %></div>
           <div class="searchbox fixed"><p>
                  公司名称：
                  <input type="text" class="formsize120 input-txt" id="GongSiName" name="GongSiName" value="<%=Utils.GetQueryStringValue("GongSiName") %>"/>
                  <input name="" type="submit" class="search-btn">
                </p>
           </div>

           <div class="tablehead">
                <ul class="fixed">
                   <li><a href="#" do="add">新增</a></li>
              </ul>                
           </div>

           <!--列表表格-->
           <div class="tablelist-box">
               <table width="100%"  id="liststyle">
                    <tr>
                      <th class="thinputbg">序号</th>
                      <th align="center" >公司名称</th>
                      <th align="center" >法人代表</th>
                      <th align="center" >负责人</th>
                      <th align="center" >联系电话</th>
                      <th align="center" >财务联系人</th>
                      <th align="center" >财务联系电话</th>
                      <th align="center" >操作</th>
                    </tr>
                    <asp:Repeater runat="server" ID="rpt">
                    <ItemTemplate>
                      <tr data-id='<%#Eval("GongSiId") %>' class="table_tr_item">
                        <td align="center"><%#Container.ItemIndex+(intPageIndex-1)*intPageSize+1 %></td>
                        <td align="center"><%#Eval("Name")%></td>
                        <td align="center"><%#Eval("FanRenName")%></td>
                        <td align="center"><%#Eval("FuZeRenName")%></td>
                        <td align="center"><%#Eval("FuZeRenDianHua")%></td>
                        <td align="center"><%#Eval("CaiWuName")%></td>
                        <td align="center"><%#Eval("CaiWuDianHua")%></td>
                        <td align="center"><a href="#" class="blue_btn" do="edit">编辑</a><a href="#" class="blue_btn" do="delete">删除</a></td>
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
        Url: "GongSiEdit.aspx",
        Add: function() {
            mpage.ShowBoxy({
                url: ipage.Url + "?T=<%=(int)T %>",
                title: "新增-<%=T %>",
                width: "750px",
                height: "500px"
            });
        },
        Edit: function(o) {
            mpage.ShowBoxy({
                url: ipage.Url + "?T=<%=(int)T %>&gongsiid=" + $(o).closest("tr").attr("data-id"),
                title: "编辑-<%=T %>",
                width: "750px",
                height: "500px"
            });
        },
        Delete: function(o) {
            if (!confirm("删除操作不可恢复，你确定要删除吗？")) return false;
            $.ajax({
                type: "get",
                url: "Default.aspx?do=delete&gongsiid=" + $(o).closest("tr").attr("data-id"),
                dataType: "json",
                cache: false,
                success: function(data) {
                    alert(data.msg);
                    window.location.reload();
                },
                error: function() {
                    alert("服务器忙");
                    window.location.reload();
                }
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

    $("tr.table_tr_item:nth-child(odd)").addClass("odd");
});
</script>
</asp:Content>