<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JueSe.aspx.cs" Inherits="EyouSoft.WEB.cgs.JueSe" MasterPageFile="~/mp/Cgs.Master" Title="角色管理-系统设置" %>

<asp:Content ContentPlaceHolderID="PageBody" ID="PageBody1" runat="server">
    <div class="right_box">
        <!-- InstanceBeginEditable name="EditRegion4" -->
        <div class="basicT">
            角色管理</div>
        <div class="searchbox fixed">
            <form action="" method="get">
            <p>
                角色名称：
                <input type="text" class="formsize120 input-txt" id="txtName" name="txtName" />
                <input type="submit" class="search-btn" id="btnChaXun" />
            </p>
            </form>
        </div>
        <div class="tablehead">
            <ul class="fixed">
                <li><a href="javascript:void(0)" id="a_tianjia">新增</a></li>
            </ul>
        </div>
        <!--列表表格-->
        <div class="tablelist-box mt10" style="">
            <table width="100%" id="liststyle">
                <tr>
                    <th class="thinputbg">
                        序号
                    </th>
                    <th align="left">
                        角色名称
                    </th>
                    <th align="center" style="width: 150px;">
                        操作
                    </th>
                </tr>
                <asp:Repeater runat="server" ID="rpt">
                    <ItemTemplate>
                        <tr data-itemid="<%#Eval("JueSeId") %>" class="table_tr_item">
                            <td align="center">
                                <%# Container.ItemIndex + 1+( this.pageIndex - 1) * this.pageSize%>
                            </td>
                            <td align="left">
                                <%#Eval("Name")%>
                            </td>
                            <td align="left">
                                <a href="javascript:void(0)" class="blue_btn" data-class="bianji">修改</a> <a href="javascript:void(0)" class="blue_btn" data-class="shanchu">删除</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:PlaceHolder runat="server" ID="phEmpty">
                    <tr>
                        <td class="even" colspan="10" style="height: 30px; text-align: center;">
                            暂无角色。
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

    <script type="text/javascript" src="/js/fenye.js"></script>

    <script type="text/javascript">
        var iPage = {
            reload: function(obj) {
                window.location.href = window.location.href;
            },
            tianJia: function() {
                var _data = {}
                Boxy.iframeDialog({ title: "角色-新增", iframeUrl: "jueseedit.aspx", width: "700px", height: "400px", data: _data, afterHide: function() { iPage.reload(); } });
                return false;
            },
            bianJi: function(obj) {
                var _$tr = $(obj).closest("tr");
                var _data = { editid: _$tr.attr("data-itemid") };
                Boxy.iframeDialog({ title: "角色-编辑", iframeUrl: "jueseedit.aspx", width: "700px", height: "400px", data: _data, afterHide: function() { iPage.reload(); } });
                return false;
            },
            shanChu: function(obj) {
                if (!confirm("删除操作不可恢复，你确定要删除吗？")) return false;
                var _$tr = $(obj).closest("tr");
                var _data = { txtJueSeId: _$tr.attr("data-itemid") };
                var _self = this;

                $.ajax({ type: "post", cache: false, url: "juese.aspx?dotype=shanchu", dataType: "json", data: _data,
                    success: function(ret) {
                        alert(ret.msg);
                        _self.reload();
                    }
                });
            }
        };

        $(document).ready(function() {
            mp.sheZhiWeiZhi("系统设置", "角色管理");

            var fenYePeiZhi = { pageSize: '<%=pageSize %>', pageIndex: '<%=pageIndex %>', recordCount: '<%=recordCount %>', showPrev: true, showNext: true, showDisplayText: true, showTiaoZhuan: true, cssClassName: 'page' }
            fenYe.replace("div_fenye", fenYePeiZhi);

            utilsUri.initChaXun();

            $("#a_tianjia").click(function() { iPage.tianJia(); });
            $('a[data-class="bianji"]').click(function() { iPage.bianJi(this); });
            $('a[data-class="shanchu"]').click(function() { iPage.shanChu(this); });

            $("tr.table_tr_item:nth-child(odd)").addClass("odd");
        });
    </script>

</asp:Content>
