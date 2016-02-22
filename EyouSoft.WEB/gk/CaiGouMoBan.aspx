<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CaiGouMoBan.aspx.cs" Inherits="EyouSoft.WEB.gk.CaiGouMoBan" MasterPageFile="/mp/Gk.Master" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head"></asp:Content>

<asp:Content ID="body" runat="server" ContentPlaceHolderID="body">
<div class="right_bar">
    <div class="right_box">
        <!-- InstanceBeginEditable name="EditRegion4" -->
        <div class="basicT">
            采购模版</div>
        <div class="searchbox fixed">
            <p>
                采购单名称：
                <input type="text" class="formsize120 input-txt" id="txtName" name="txtName" />
                <input type="submit" class="search-btn" id="btnChaXun" />
            </p>
        </div>
        <div class="tablehead">
            <ul class="fixed">
                <li><a href="javascript:void(0)" id="a_tianjia">新增</a></li>
                <%--<li><a href="javascript:void(0)">批量禁用</a></li><li><a href="javascript:void(0)">批量删除</a></li>--%>
            </ul>
        </div>
        <!--列表表格-->
        <div class="tablelist-box" style="">
            <table width="100%" id="liststyle">
                <tr>
                    <th class="thinputbg">
                        <%--<input type="checkbox" id="chk_quanxuan" />
                        全选--%>序号
                    </th>                    
                    <th align="center">
                        采购单名称
                    </th>
                    <th align="center">
                        采购商
                    </th>
                    <th align="center">
                        发布人
                    </th>
                    <th align="center">
                        发布时间
                    </th>
                    <th align="center" style="width:180px;">
                        操作
                    </th>
                </tr>
                <asp:Repeater runat="server" ID="rpt">
                    <ItemTemplate>
                        <tr data-itemid="<%#Eval("MoBanId") %>" class="table_tr_item">
                            <td align="center">
                                <%--<input type="checkbox" name="chk_quanxuan_item" />--%>
                                <%# Container.ItemIndex + 1+( this.pageIndex - 1) * this.pageSize%>
                            </td>
                            <td align="center">
                                <%#Eval("Name")%>
                            </td>
                            <td align="center">
                                <%#Eval("CgsName")%>
                            </td>
                            <td align="center">
                                <%#Eval("CaoZuoRenName")%>
                            </td>
                            <td align="center">
                                <%#Eval("IssueTime","{0:yyyy-MM-dd hh:mm}")%>
                            </td>
                        <td align="center"><a href="#" class="blue_btn" do="edit">编辑</a><a href="#" class="blue_btn" do="delete">删除</a></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:PlaceHolder runat="server" ID="phEmpty">
                    <tr>
                        <td class="even" colspan="8" style="height: 30px; text-align: center;">
                            暂无采购模版信息。
                        </td>
                    </tr>
                </asp:PlaceHolder>
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
        var iPage = {
            reload: function() {
                window.location.href = window.location.href;
            },
            tianJia: function() {
                var _data = {}
                Boxy.iframeDialog({ title: "采购模版-新增", iframeUrl: "caigoumobanedit.aspx", width: "870px", height: "540px", data: _data, afterHide: function() { iPage.reload(); } });
                return false;
            },
            bianJi: function(obj) {
                var _$tr = $(obj).closest("tr");
                var _data = { mobanid: _$tr.attr("data-itemid") };
                Boxy.iframeDialog({ title: "采购模版-编辑", iframeUrl: "caigoumobanedit.aspx", width: "870px", height: "540px", data: _data, afterHide: function() { iPage.reload(); } });
                return false;
            },
            shanChu: function(obj) {
                if (!confirm("删除操作不可恢复，你确定要删除吗？")) return false;
                var _$tr = $(obj).closest("tr");
                var _data = { txtYongHuId: _$tr.attr("data-itemid") };
                var _self = this;
                $.ajax({ type: "post", cache: false, url: "caigoumoban.aspx?dotype=shanchu", dataType: "json", data: _data,
                    success: function(ret) {
                        alert(ret.msg);
                        _self.reload();
                    },
                    error: function() {
                        _self.reload();
                    }
                });
            }
        };

        $(document).ready(function() {
           // mp.sheZhiWeiZhi("系统设置", "用户管理");

            var fenYePeiZhi = { pageSize: '<%=pageSize %>', pageIndex: '<%=pageIndex %>', recordCount: '<%=recordCount %>', showPrev: true, showNext: true, showDisplayText: true, showTiaoZhuan: true, cssClassName: 'page' }
            fenYe.replace("div_fenye", fenYePeiZhi);

            utilsUri.initChaXun();

            $("#chk_quanxuan").click(function() { $("input[name='chk_quanxuan_item']").prop("checked", $(this).prop("checked")); });
            $("#a_tianjia").click(function() { iPage.tianJia(); });
            $('a[do="edit"]').click(function() { iPage.bianJi(this); });
            $('a[do="delete"]').click(function() { iPage.shanChu(this); });

            $("tr.table_tr_item:nth-child(odd)").addClass("odd");
        });
    </script>
</asp:Content>
