<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XiaoXi.aspx.cs" Inherits="EyouSoft.WEB.gys.XiaoXi" Title="消息中心" MasterPageFile="~/mp/Gys.Master" %>

<asp:Content ContentPlaceHolderID="body" ID="PageBody1" runat="server">
<div class="right_bar">
    <div class="right_box">
        <!-- InstanceBeginEditable name="EditRegion4" -->
        <div class="basicT">
            消息中心</div>
        <div class="searchbox fixed">
            <input type="hidden" id="txtIsChaXun" name="txtIsChaXun" value="1" />
            <p>
                消息时间：
                <input type="text" class="formsize80 input-txt" onfocus="WdatePicker();" id="txtXiaoXiTime1" name="txtXiaoXiTime1" />
                -
                <input type="text" class="formsize80 input-txt" onfocus="WdatePicker();" id="txtXiaoXiTime2" name="txtXiaoXiTime2" />
                <!--消息类型：<select id="txtLeiXing" name="txtLeiXing" class="input_select">
                    <option value="">-请选择-</option>
                    <option value="2">报价待确认</option>
                    <option value="4">收货待确认</option>
                </select>-->                
                消息状态：<select id="txtStatus" name="txtStatus" class="input_select">
                    <option value="">-请选择-</option>
                    <%=EyouSoft.Toolkit.MeiJuHelper.GetSelectOption(typeof(EyouSoft.Model.XiaoXiStatus))%>
                </select><input type="submit" class="search-btn" id="btnChaXun" />
            </p>
        </div>
        <div class="bb1">
        </div>
        <!--列表表格-->
        <div class="tablelist-box mt10" style="">
            <table width="100%" id="liststyle">
                <tr>
                    <th class="thinputbg" style="width: 40px;">
                        序号
                    </th>
                    <th align="left" style="width:120px;">
                        状态
                    </th>
                    <th align="left" style="width:150px;">
                        消息标题
                    </th>
                    <th style="text-align: left">
                        消息内容
                    </th>
                    <th align="left" style="width:120px;">
                        消息时间
                    </th>
                    <th align="left" style="width: 120px;">
                        操作
                    </th>
                </tr>
                <asp:Repeater runat="server" ID="rpt">
                    <ItemTemplate>
                        <tr data-itemid="<%#Eval("XiaoXiId") %>" data-guanlianid="<%#Eval("GuanLianId") %>" data-leixing="<%#(int)Eval("LeiXing") %>" data-status="<%#(int)Eval("Status") %>" class="table_tr_item">
                            <td align="center">
                                <%# Container.ItemIndex + 1+( this.pageIndex - 1) * this.pageSize%>
                            </td>
                            <td align="left">
                                <%#Eval("Status")%>
                            </td>
                            <td align="left">
                                <%#Eval("BiaoTi")%>
                            </td>
                            <td style="">
                                <%#Eval("NeiRong")%>&nbsp;
                            </td>
                            <td align="left">
                                <%#Eval("FaChuTime", "{0:yyyy-MM-dd HH:mm}")%>
                            </td>
                            <td align="left">
                                <%#GetCaoZuo(Eval("Status"),Eval("LeiXing"))%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:PlaceHolder runat="server" ID="phEmpty">
                    <tr>
                        <td class="even" colspan="10" style="height: 30px; text-align: center;">
                            暂无消息。
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
            	var _leixing = _$tr.attr("data-leixing");
            	var _t = 0;
            	switch (_leixing) {
            	case "3":
            		_t = 2;
            		break;
            	}
            	var _data = { dingdanid: _$tr.attr("data-guanlianid"), T: _t };

            	var _status = _$tr.attr("data-status");
            	if (_status == "<%=(int)EyouSoft.Model.XiaoXiStatus.未读 %>") this.sheZhiYiDu1(obj);
                
                Boxy.iframeDialog({ title: "采购订单-查看", iframeUrl: "/commonpage/caigoudingdan.aspx", width: "870px", height: "540px", data: _data, afterHide: function() { iPage.reload(); } });
                return false;
            },
            sheZhiYiDu: function(obj) {
                var _$tr = $(obj).closest("tr");
                var _data = { txtXiaoXiId: _$tr.attr("data-itemid") };
                var _self = this;

                $.ajax({ type: "post", url: "xiaoxi.aspx?doType=shezhiyidu", data: _data, cache: false, dataType: "json", async: false,
                    success: function(response) {
                        alert(response.msg);
                        _self.reload();
                    }
                });
            },
            sheZhiYiDu1: function(obj) {
                var _$tr = $(obj).closest("tr");
                var _data = { txtXiaoXiId: _$tr.attr("data-itemid") };
                var _self = this;

                $.ajax({ type: "post", url: "xiaoxi.aspx?doType=shezhiyidu", data: _data, cache: false, dataType: "json", async: true, success: function(response) { } });
            }
        };

        $(document).ready(function() {
//            mp.sheZhiWeiZhi("消息中心", "消息中心");

            var fenYePeiZhi = { pageSize: '<%=pageSize %>', pageIndex: '<%=pageIndex %>', recordCount: '<%=recordCount %>', showPrev: true, showNext: true, showDisplayText: true, showTiaoZhuan: true, cssClassName: 'page' }
            fenYe.replace("div_fenye", fenYePeiZhi);

            $("#txtStatus").val("<%=XiaoXiStatus %>");

            utilsUri.initChaXun();

            $('a[data-class="chakan"]').click(function() { iPage.chaKan(this); });
            $('a[data-class="shezhiyidu"]').click(function() { iPage.sheZhiYiDu(this); });
            $("tr.table_tr_item:nth-child(odd)").addClass("odd");
        });
    </script>

</asp:content>
