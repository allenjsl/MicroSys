<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CgdRuKu.aspx.cs" Inherits="EyouSoft.WEB.cgs.CgdRuKu" Title="采购单入库-采购管理" MasterPageFile="~/mp/Cgs.Master" %>

<asp:Content ContentPlaceHolderID="PageHead" ID="PageHead1" runat="server">

</asp:Content>

<asp:Content ContentPlaceHolderID="PageBody" ID="PageBody1" runat="server">
    <div class="right_box">
        <!-- InstanceBeginEditable name="EditRegion4" -->
        <div class="basicT">
            采购列表</div>
        <div class="searchbox fixed">
            <form action="cgdruku.aspx" method="get"> 
            <p>
                采购名称：
                <input type="text" class="formsize120 input-txt" id="txtCgdName" name="txtCgdName" />
                发布时间：
                <input type="text" class="formsize80 input-txt" onfocus="WdatePicker();" id="txtFaBuTime1" name="txtFaBuTime1"/>
                -
                <input type="text" class="formsize80 input-txt" onfocus="WdatePicker();" id="txtFaBuTime2" name="txtFaBuTime2"/>
                采购状态：<select id="txtCgdStatus" name="txtCgdStatus" class="input_select">
                    <option value="">请选择</option>
                    <%=EyouSoft.Toolkit.MeiJuHelper.GetSelectOption(typeof(EyouSoft.Model.CaiGouDanStatus))%>
                </select><input type="submit" class="search-btn" id="btnChaXun" />
            </p>
            </form>
        </div>
        <div class="tablehead">
            <ul class="fixed">
                <li><a href="javascript:void(0)" id="a_tianjia">新增</a></li>
                <!--<li><a href="javascript:void(0)" id="a_fabu">批量发布</a></li>-->
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
                    <th align="left">
                        采购名称
                    </th>
                    <!--<th align="center">
                        供应商
                    </th>-->
                    <th align="left" style="width:80px;">
                        采购状态
                    </th>
                    <th align="left" style="width:100px;">
                        发布人
                    </th>
                    <th align="left" style="width:120px;">
                        发布时间
                    </th>
                    <th align="left" style="width:110px;">
                        要求到货时间
                    </th>
                    <th align="center" style="width:188px;">
                        操作
                    </th>
                </tr>
                <asp:Repeater runat="server" ID="rpt">
                <ItemTemplate>
                <tr data-itemid="<%#Eval("CaiGouDanId") %>" class="table_tr_item">
                    <td align="center">
                        <%--<input type="checkbox" name="chk_quanxuan_item" />--%>
                        <%# Container.ItemIndex + 1+( this.pageIndex - 1) * this.pageSize%>
                    </td>
                    <td align="left">
                        <%#Eval("CaiGouDanName")%>
                    </td>
                    <!--<td align="center">
                        
                    </td>-->
                    <td align="left">
                        <%#Eval("Status")%>
                    </td>
                    <td align="left">
                        <%#Eval("CaoZuoRenName")%>
                    </td>
                    <td align="left">
                       <%#Eval("IssueTime","{0:yyyy-MM-dd HH:mm}")%>
                    </td>
                    <td align="left">
                        <%#Eval("YaoQiuDaoHuoTime", "{0:yyyy-MM-dd}")%>
                    </td>
                    <td align="left">
                        <%#GetCaoZuo(Eval("Status")) %>
                    </td>
                </tr>
                </ItemTemplate>
                </asp:Repeater>
                                
                <asp:PlaceHolder runat="server" ID="phEmpty">
                    <tr>
                        <td class="even" colspan="8" style="height: 30px; text-align: center;">
                            暂无采购单信息。
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
            reload: function() {
                window.location.href = window.location.href;
            },
            tianJia: function() {
                var _data = {}
                Boxy.iframeDialog({ title: "采购单入库-新增", iframeUrl: "cgdrukuedit.aspx", width: "870px", height: "540px", data: _data, afterHide: function() { iPage.reload(); } });
                return false;
            },
            bianJi: function(obj) {
                var _$tr = $(obj).closest("tr");
                var _data = { editid: _$tr.attr("data-itemid") };
                Boxy.iframeDialog({ title: "采购单入库-编辑", iframeUrl: "cgdrukuedit.aspx", width: "870px", height: "540px", data: _data, afterHide: function() { iPage.reload(); } });
                return false;
            },
            chaKan: function(obj) {
                var _$tr = $(obj).closest("tr");
                var _data = { editid: _$tr.attr("data-itemid") };
                Boxy.iframeDialog({ title: "采购单入库-查看", iframeUrl: "cgdrukuedit.aspx", width: "870px", height: "540px", data: _data, afterHide: function() { iPage.reload(); } });
                return false;
            },
            shanChu: function(obj) {
                if (!confirm("删除操作不可恢复，你确定要删除吗？")) return false;
                var _$tr = $(obj).closest("tr");
                var _data = { txtCgdId: _$tr.attr("data-itemid") };
                var _self = this;
                $.ajax({ type: "post", cache: false, url: "cgdruku.aspx?dotype=shanchu", dataType: "json", data: _data,
                    success: function(ret) {
                        alert(ret.msg);
                        _self.reload();
                    },
                    error: function() {
                        _self.reload();
                    }
                });
            },
            faBu: function(obj) {
                if (!confirm("下单操作不可撤销，你确定要下单吗？")) return false;
                var _$tr = $(obj).closest("tr");
                var _data = { txtCgdId: _$tr.attr("data-itemid") };
                var _self = this;
                $.ajax({ type: "post", cache: false, url: "cgdruku.aspx?dotype=fabu", dataType: "json", data: _data,
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
            mp.sheZhiWeiZhi("采购管理", "采购单入库");

            var fenYePeiZhi = { pageSize: '<%=pageSize %>', pageIndex: '<%=pageIndex %>', recordCount: '<%=recordCount %>', showPrev: true, showNext: true, showDisplayText: true, showTiaoZhuan: true, cssClassName: 'page' }
            fenYe.replace("div_fenye", fenYePeiZhi);

            utilsUri.initChaXun();

            $("#chk_quanxuan").click(function() { $("input[name='chk_quanxuan_item']").prop("checked", $(this).prop("checked")); });
            $("#a_tianjia").click(function() { iPage.tianJia(); });
            $('a[data-class="bianji"]').click(function() { iPage.bianJi(this); });
            $('a[data-class="fabu"]').click(function() { iPage.faBu(this); });
            $('a[data-class="chakan"]').click(function() { iPage.chaKan(this); });
            $('a[data-class="shanchu"]').click(function() { iPage.shanChu(this); });

            $("tr.table_tr_item:nth-child(odd)").addClass("odd");
        });
    </script>
</asp:Content>
