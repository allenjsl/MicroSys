<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CaiWuGuanLi.aspx.cs" Inherits="EyouSoft.WEB.gys.ChanPinJiaGe" MasterPageFile="/mp/Gys.Master" %>
<%@ Import Namespace="EyouSoft.Toolkit" %>
<%@ Import Namespace="EyouSoft.WEB.gk" %>

<asp:Content runat="server" ID="body" ContentPlaceHolderID="body">
<div class="right_bar">
    <div class="right_box">
            <!-- InstanceBeginEditable name="EditRegion4" -->
            <div class="basicT">
                财务对账表</div>
            <div class="searchbox fixed">
                <p>
                    采购单号：
                    <input type="text" class="formsize120 input-txt" id="txtCgdHao" name="txtCgdHao" />
                    采购时间：
                    <input type="text" class="formsize80 input-txt" onfocus="WdatePicker();" id="txtCaiGouTime1" name="txtCaiGouTime1" />
                    -
                    <input type="text" class="formsize80 input-txt" onfocus="WdatePicker();" id="txtCaiGouTime2" name="txtCaiGouTime2" />
                    采购商：<input type="text" class="formsize120 input-txt" id="txtCgsName" name="txtCgsName" />
                    收款状态：<select id="txtFuKuanStatus" name="txtFuKuanStatus" class="input_select">
                        <option value="">请选择</option>
                        <option value="<%=(int)EyouSoft.Model.FuKuanStatus.未付款 %>">未收款</option>
                        <option value="<%=(int)EyouSoft.Model.FuKuanStatus.已付款 %>">已收款</option>
                    </select><input type="submit" class="search-btn" id="btnChaXun" />
                </p>
            </div>
            <div class="bb1">
            </div>
            <!--列表表格-->
            <div class="tablelist-box mt10" style="">
                <table width="100%" id="liststyle">
                    <tr>
                        <th class="thinputbg" style="width:40px;">
                            序号
                        </th>
                        <th align="center">
                            采购单号
                        </th>
                        <th align="center">
                            采购商
                        </th>
                        <th style="text-align: right; width: 70px;">
                            采购项数&nbsp;
                        </th>
                        <!--<th align="left">
                            采购部门
                        </th>-->
                        <th align="center" style="width: 80px;">
                            采购人
                        </th>
                        <th align="center" style="width: 110px;">
                            采购时间
                        </th>
                        <th align="center" style="width: 110px;">
                            到货时间
                        </th>
                        <th style="text-align: right; width: 80px;">
                            采购金额&nbsp;
                        </th>
                        <th align="center" style="width: 90px;">
                            收款状态
                        </th>
                        <th align="center" style="width: 80px;">
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
                                    <%#Eval("CgsName")%><%#EyouSoft.Toolkit.Utils.GetLxQQ(Eval("CgsLxQQ")) %>
                                </td>
                                <td style="text-align: right;">
                                    <%#Eval("CaiGouChanPinXiangShu")%>&nbsp;
                                </td>                           
                                <!--<td align="left">
                                    <%#Eval("CaiGouBuMen")%>
                                </td>-->
                                <td align="left">
                                    <%#Eval("CaoZuoRenName")%>
                                </td>
                                <td align="left">
                                    <%#Eval("IssueTime","{0:yyyy-MM-dd HH:mm}")%>
                                </td>
                                 <td align="left">
                                    <%#Eval("DaoHuoTime", "{0:yyyy-MM-dd HH:mm}")%>
                                </td>
                                <td style="text-align: right;">
                                    <%#GetJinE(Eval("JinE"),Eval("Status"))%>&nbsp;
                                </td>
                                <td align="center">
                                    <%#(int)Eval("CgsFuKuanStatus")==(int)EyouSoft.Model.FuKuanStatus.未付款?"未收款":"已收款"%>
                                </td>
                                <td align="center">
                                    <%#GetCaoZuo(Eval("CgsFuKuanStatus"))%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:PlaceHolder runat="server" ID="phHeJi">
                        <tr>
                            <td  colspan="7" style="height: 30px; text-align: right;">
                                合计：
                            </td>
                            <td style="text-align:right;">
                                <asp:Literal runat="server" ID="ltrJinEHeJi"></asp:Literal>&nbsp;
                            </td>
                            <td colspan="2"></td>
                        </tr>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder runat="server" ID="phEmpty">
                        <tr>
                            <td class="even" colspan="10" style="height: 30px; text-align: center;">
                                暂无对账信息。
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
            chaKan: function(obj) {
                var _$tr = $(obj).closest("tr");
                var _data = { dingdanid: _$tr.attr("data-itemid"),T:2 };
                Boxy.iframeDialog({ title: "采购订单-查看", iframeUrl: "/commonpage/CaiGouDingDan.aspx", width: "870px", height: "540px", data: _data, afterHide: function() { iPage.reload(); } });
                return false;
            }
        };
        
        $(document).ready(function() {
//            mp.sheZhiWeiZhi("财务管理", "财务对账");

            var fenYePeiZhi = { pageSize: '<%=pageSize %>', pageIndex: '<%=pageIndex %>', recordCount: '<%=recordCount %>', showPrev: true, showNext: true, showDisplayText: true, showTiaoZhuan: true, cssClassName: 'page' }
            fenYe.replace("div_fenye", fenYePeiZhi);

            utilsUri.initChaXun();

            $('a[data-class="chakan"]').click(function() { iPage.chaKan(this); });

            $("tr.table_tr_item:nth-child(odd)").addClass("odd");
        });
    </script>
</asp:Content>
