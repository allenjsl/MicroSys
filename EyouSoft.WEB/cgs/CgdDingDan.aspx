<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CgdDingDan.aspx.cs" Inherits="EyouSoft.WEB.cgs.CgdDingDan" Title="采购订单管理-采购管理" MasterPageFile="~/mp/Cgs.Master" %>


<asp:Content ContentPlaceHolderID="PageBody" ID="PageBody1" runat="server">
    <div class="right_box">
        <!-- InstanceBeginEditable name="EditRegion4" -->
        <div class="basicT">
            采购订单管理</div>
        <div class="searchbox fixed">
            <form action="cgddingdan.aspx" method="get">
            <p>
                采购单号：
                <input type="text" class="formsize120 input-txt" id="txtCgdHao" name="txtCgdHao" />
                采购时间：
                <input type="text" class="formsize80 input-txt" onfocus="WdatePicker();" id="txtCaiGouTime1" name="txtCaiGouTime1" />
                -
                <input type="text" class="formsize80 input-txt" onfocus="WdatePicker();" id="txtCaiGouTime2" name="txtCaiGouTime2" />
                采购部门：<input type="text" class="formsize120 input-txt" id="txtCgBuMenName" name="txtCgBuMenName" />
                采购状态：<select id="txtStatus" name="txtStatus" class="input_select">
                    <option value="">请选择</option>
                    <%=EyouSoft.Toolkit.MeiJuHelper.GetSelectOption(typeof(EyouSoft.Model.DingDanStatus),new string[]{"0"})%>
                </select><input type="submit" class="search-btn" id="btnChaXun" />
            </p>
            </form>
        </div>
        <div class="bb1"></div>
        <!--列表表格-->
        <div class="tablelist-box mt10" style="">
            <table width="100%" id="liststyle">
                <tr>
                    <th class="thinputbg">
                        序号
                    </th>
                    <th align="left">
                        采购单号
                    </th>                   
                    <th align="center">
                        供应商
                    </th>
                    <th style="text-align: right; width: 70px;">
                        采购项数&nbsp;
                    </th>
                    <th style="text-align: right; width: 80px;">
                        采购金额&nbsp;
                    </th>
                    <th align="left">
                        采购部门
                    </th>
                    <th align="left" style="width:80px;">
                        采购人
                    </th>
                    <th align="left" style="width:110px;">
                        采购时间
                    </th>
                    <th align="left" style="width: 90px;">
                        采购状态
                    </th>
                    <th align="center" style="width:80px;">
                        操作
                    </th>
                </tr>
                <asp:Repeater runat="server" ID="rpt">
                    <ItemTemplate>
                        <tr data-itemid="<%#Eval("DingDanId") %>" class="table_tr_item">
                            <td align="center">
                                <%# Container.ItemIndex + 1+( this.pageIndex - 1) * this.pageSize%>
                            </td>
                            <td align="left">
                                <%#Eval("CaiGouDanHao")%>
                            </td>                            
                            <td align="center">
                                <%#Eval("GysName")%><%#EyouSoft.Toolkit.Utils.GetLxQQ(Eval("GysLxQQ")) %>
                            </td>
                            <td style="text-align: right;">
                                <%#Eval("CaiGouChanPinXiangShu")%>&nbsp;
                            </td>
                            <td style="text-align: right;">
                                <%#GetJinE(Eval("JinE"),Eval("Status"))%>&nbsp;
                            </td>
                            <td align="left">
                                <%#Eval("CaiGouBuMen")%>
                            </td>
                            <td align="left">
                                <%#Eval("CaoZuoRenName")%>
                            </td>
                            <td align="left">
                                <%#Eval("IssueTime","{0:yyyy-MM-dd HH:mm}")%>
                            </td>
                            <td align="left">
                                <%#Eval("Status")%>
                            </td>
                            <td align="left">
                                <%#GetCaoZuo(Eval("Status")) %>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:PlaceHolder runat="server" ID="phEmpty">
                    <tr>
                        <td class="even" colspan="10" style="height: 30px; text-align: center;">
                            暂无采购订单信息。
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
            guanLi: function(obj) {
                var _$tr = $(obj).closest("tr");
                var _data = { editid: _$tr.attr("data-itemid") };
                Boxy.iframeDialog({ title: "采购订单-管理", iframeUrl: "cgddingdanedit.aspx", width: "870px", height: "540px", data: _data, afterHide: function() { iPage.reload(); } });
                return false;

            },
            chaKan: function(obj) {
                var _$tr = $(obj).closest("tr");
                var _data = { editid: _$tr.attr("data-itemid") };
                Boxy.iframeDialog({ title: "采购订单-查看", iframeUrl: "cgddingdanedit.aspx", width: "870px", height: "540px", data: _data, afterHide: function() { iPage.reload(); } });
                return false;
            }
        };
        
        $(document).ready(function() {
            mp.sheZhiWeiZhi("采购管理", "采购订单管理");

            var fenYePeiZhi = { pageSize: '<%=pageSize %>', pageIndex: '<%=pageIndex %>', recordCount: '<%=recordCount %>', showPrev: true, showNext: true, showDisplayText: true, showTiaoZhuan: true, cssClassName: 'page' }
            fenYe.replace("div_fenye", fenYePeiZhi);

            utilsUri.initChaXun();

            $('a[data-class="chakan"]').click(function() { iPage.chaKan(this); });
            $('a[data-class="guanli"]').click(function() { iPage.guanLi(this); });

            $("tr.table_tr_item:nth-child(odd)").addClass("odd");
        });
    </script>
</asp:Content>
