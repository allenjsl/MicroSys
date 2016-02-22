<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SongHuoRen.aspx.cs" Inherits="EyouSoft.WEB.gys.SongHuoRen" MasterPageFile="~/mp/Gys.Master" %>


<asp:Content ContentPlaceHolderID="body" ID="body" runat="server">
    <div class="right_bar">
    <div class="right_box">
        <!-- InstanceBeginEditable name="EditRegion4" -->
        <div class="basicT">
            送货人信息</div>
        <div class="searchbox fixed">
            <p>
                姓名：
                <input type="text" class="formsize120 input-txt" id="txtName" name="txtName" />
                <input type="submit" class="search-btn" id="btnChaXun" />
            </p>
        </div>
        <div class="tablehead">
            <ul class="fixed">
                <li><a href="javascript:void(0)" id="a_tianjia">新增</a></li>
                <!--<li><a href="javascript:void(0)" id="a_fabu">批量发布</a></li>-->
            </ul>
        </div>
        <!--列表表格-->
        <div class="tablelist-box mt10" style="">
            <table width="100%" id="liststyle">
                <tr>
                    <th class="thinputbg">
                        序号
                    </th>
                    <th align="left" style="width: 150px;">
                        姓名
                    </th>
                    <th style="text-align: left; width: 150px;">
                        联系电话&nbsp;
                    </th>
                    <th style="text-align: left; width: 150px;">
                        联系手机&nbsp;
                    </th>
                    <th style="text-align: left; width: 150px;">
                        是否默认&nbsp;
                    </th>
                    <th align="center" style="width: 150px;">
                        操作
                    </th>
                </tr>
                <asp:Repeater runat="server" ID="rpt">
                    <ItemTemplate>
                        <tr data-itemid="<%#Eval("DiZhiId") %>" class="table_tr_item">
                            <td align="center">
                                <%# Container.ItemIndex + 1+( this.pageIndex - 1) * this.pageSize%>
                            </td>
                            <td align="left">
                                <%#Eval("Name")%>
                            </td>
                            <td style="text-align: left;">
                                <%#Eval("DianHua")%>&nbsp;
                            </td>
                            <td style="text-align: left;">
                                <%#Eval("ShouJi")%>&nbsp;
                            </td>
                            <td style="text-align: left;">
                                <%#(bool)Eval("IsMoRen")?"是":"否"%>&nbsp;
                            </td>
                            <td align="left">
                                <%#GetCaoZuo(Eval("IsMoRen"))%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:PlaceHolder runat="server" ID="phEmpty">
                    <tr>
                        <td class="even" colspan="10" style="height: 30px; text-align: center;">
                            暂无送货人信息。
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
            reload: function(obj) {
                window.location.href = window.location.href;
            },
            tianJia: function() {
                var _data = {}
                Boxy.iframeDialog({ title: "送货人-新增", iframeUrl: "songhuorenedit.aspx", width: "700px", height: "400px", data: _data, afterHide: function() { iPage.reload(); } });
                return false;
            },
            bianJi: function(obj) {
                var _$tr = $(obj).closest("tr");
                var _data = { editid: _$tr.attr("data-itemid") };
                Boxy.iframeDialog({ title: "送货人-编辑", iframeUrl: "songhuorenedit.aspx", width: "700px", height: "400px", data: _data, afterHide: function() { iPage.reload(); } });
                return false;
            },
            shanChu: function(obj) {
                if (!confirm("删除操作不可恢复，你确定要删除吗？")) return false;
                var _$tr = $(obj).closest("tr");
                var _data = { txtDiZhiId: _$tr.attr("data-itemid") };
                var _self = this;
                $.ajax({ type: "post", cache: false, url: "songhuoren.aspx?dotype=shanchu", dataType: "json", data: _data,
                    success: function(ret) {
                        alert(ret.msg);
                        _self.reload();
                    },
                    error: function() {
                        _self.reload();
                    }
                });
            },
            sheZhiMoRen: function(obj) {
                var _$tr = $(obj).closest("tr");
                var _data = { txtDiZhiId: _$tr.attr("data-itemid") };
                var _self = this;
                $.ajax({ type: "post", cache: false, url: "songhuoren.aspx?dotype=shezhimoren", dataType: "json", data: _data,
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
            //mp.sheZhiWeiZhi("系统设置", "常用地址");

            var fenYePeiZhi = { pageSize: '<%=pageSize %>', pageIndex: '<%=pageIndex %>', recordCount: '<%=recordCount %>', showPrev: true, showNext: true, showDisplayText: true, showTiaoZhuan: true, cssClassName: 'page' }
            fenYe.replace("div_fenye", fenYePeiZhi);

            utilsUri.initChaXun();

            $("#a_tianjia").click(function() { iPage.tianJia(); });
            $('a[data-class="bianji"]').click(function() { iPage.bianJi(this); });
            $('a[data-class="shanchu"]').click(function() { iPage.shanChu(this); });
            $('a[data-class="shezhimoren"]').click(function() { iPage.sheZhiMoRen(this); });

            $("tr.table_tr_item:nth-child(odd)").addClass("odd");
        });
    </script>
</asp:Content>
