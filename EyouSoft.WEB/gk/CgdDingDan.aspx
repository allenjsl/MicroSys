<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CgdDingDan.aspx.cs" Inherits="EyouSoft.WEB.gk.CgdDingDan" Title="采购订单管理-采购管理" MasterPageFile="~/mp/Gk.Master" %>
<%@ Import Namespace="EyouSoft.Toolkit" %>
<%@ Import Namespace="EyouSoft.WEB.gk" %>


<asp:Content ContentPlaceHolderID="body" ID="PageBody1" runat="server">
<div class="right_bar">
    <div class="right_box">
        <!-- InstanceBeginEditable name="EditRegion4" -->
        <div class="basicT">
            采购订单管理</div>
        <div class="searchbox fixed">           
            <p>
                采购单号：
                <input type="text" class="formsize120 input-txt" id="txtCgdHao" name="txtCgdHao" />
                采购时间：
                <input type="text" class="formsize80 input-txt" onfocus="WdatePicker();" id="txtCaiGouTime1" name="txtCaiGouTime1" />
                -
                <input type="text" class="formsize80 input-txt" onfocus="WdatePicker();" id="txtCaiGouTime2" name="txtCaiGouTime2" />
                采购部门：<input type="text" class="formsize120 input-txt" id="txtCgBuMenName" name="txtCgBuMenName" />
                采购商：<input type="text" class="formsize120 input-txt" id="txtCgsName" name="txtCgsName" /><input type="submit" class="search-btn" id="btnChaXun" />
            </p>
        </div>
        <div class="bb1"></div>
        <!--列表表格-->
        <div class="tablelist-box mt10" style="">
            <table width="100%" id="liststyle">
                <tr>
                    <th class="thinputbg">
                        序号
                    </th>
                    <th align="center">
                        采购单号
                    </th>                   
                  <% switch (T)%>
                  <%   {%>
                  <%       case 0:%>
                                <th style="text-align: right; width: 70px;">
                                    采购项数&nbsp;
                                </th>
                                <th style="text-align: right; width: 80px;">
                                    采购金额&nbsp;
                                </th>
                                <th align="center">
                                    采购商
                                </th>
                                <th align="center">
                                    采购部门
                                </th>
                                <th align="center" style="width:80px;">
                                    采购人
                                </th>
                                <th align="center" style="width:110px;">
                                    采购时间
                                </th>
                  <%       break;%>
                  <%       case 1:%>
                                <th align="center">
                                    采购商
                                </th>
                                <th align="center" style="width:80px;">
                                    采购人
                                </th>
                                <th align="center">
                                    送货人
                                </th>
                                <th align="center">
                                    送货时间
                                </th>
                                <th align="center" style="width:80px;">
                                    收货人
                                </th>
                                <th align="center" style="width:110px;">
                                    收货时间
                                </th>     
                                <th align="center" style="width:110px;">
                                    到货状态
                                </th>     
                  <%       break;%>
                  <%   } %>                                  
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
                  <% switch (T)%>
                  <%   {%>
                  <%       case 0:%>
                            <td style="text-align: right;">
                                <%#Eval("CaiGouChanPinXiangShu")%>&nbsp;
                            </td>
                            <td style="text-align: right;">
                                <%#GetJinE(Eval("JinE"),Eval("Status"))%>&nbsp;
                            </td>
                            <td align="left">
                                <%#Eval("CgsName")%>
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
                  <%       break;%>
                  <%       case 1:%>
                            <td align="left">
                                <%#Eval("CgsName")%>
                            </td>
                            <td align="left">
                                <%#Eval("CaoZuoRenName")%>
                            </td>
                            <td align="left">
                                <%#Eval("SongHuoRenName")%>
                            </td>
                            <td align="left">
                                <%#Eval("CgsShouHuoTime","{0:yyyy-MM-dd}")%>
                            </td>
                            <td align="left">
                                <%#Eval("CgsShouHuoRen")%>
                            </td>
                            <td align="left">
                                <%#Eval("CgsShouHuoTime", "{0:yyyy-MM-dd}")%>
                            </td>
                            <td align="left">
                                已到货
                            </td>
                  <%       break;%>
                  <%   } %>                                  
                            <td align="center">
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
</div>
    <script type="text/javascript" src="/js/fenye.js"></script>
    
    <script type="text/javascript">
        var iPage = {
            reload: function(obj) {
                window.location.href = window.location.href;
            },
            chaKan: function(obj) {
                var _$tr = $(obj).closest("tr");
                var _data = { editid: _$tr.attr("data-itemid") };
                Boxy.iframeDialog({ title: "采购订单-查看", iframeUrl: "cgddingdanedit.aspx", width: "870px", height: "540px", data: _data, afterHide: function() { iPage.reload(); } });
                return false;
            }
        };
        
        $(document).ready(function() {
//            mp.sheZhiWeiZhi("采购管理", "采购订单管理");

            var fenYePeiZhi = { pageSize: '<%=pageSize %>', pageIndex: '<%=pageIndex %>', recordCount: '<%=recordCount %>', showPrev: true, showNext: true, showDisplayText: true, showTiaoZhuan: true, cssClassName: 'page' }
            fenYe.replace("div_fenye", fenYePeiZhi);

            utilsUri.initChaXun();

            $('a[data-class="chakan"]').click(function() { iPage.chaKan(this); });

            $("tr.table_tr_item:nth-child(odd)").addClass("odd");
        });
    </script>
</asp:Content>
