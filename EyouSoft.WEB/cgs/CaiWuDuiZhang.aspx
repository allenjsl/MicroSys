<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CaiWuDuiZhang.aspx.cs" Inherits="EyouSoft.WEB.cgs.CaiWuDuiZhang" Title="财务对账-财务管理" MasterPageFile="~/mp/Cgs.Master" %>


<asp:Content ContentPlaceHolderID="PageBody" ID="PageBody1" runat="server">
    <div class="right_box">
        <!-- InstanceBeginEditable name="EditRegion4" -->
        <div class="basicT">
            财务对账表</div>
        <div class="searchbox fixed">
            <form action="caiwuduizhang.aspx" method="get">
            <p>
                采购单号：
                <input type="text" class="formsize120 input-txt" id="txtCgdHao" name="txtCgdHao" />
                采购时间：
                <input type="text" class="formsize80 input-txt" onfocus="WdatePicker();" id="txtCaiGouTime1" name="txtCaiGouTime1" />
                -
                <input type="text" class="formsize80 input-txt" onfocus="WdatePicker();" id="txtCaiGouTime2" name="txtCaiGouTime2" />
                采购部门：<input type="text" class="formsize120 input-txt" id="txtCgBuMenName" name="txtCgBuMenName" /><br />
                供应商：<input type="text" class="formsize120 input-txt" id="txtGysName" name="txtGysName" />
                付款状态：<select id="txtFuKuanStatus" name="txtFuKuanStatus" class="input_select">
                    <option value="">请选择</option>
                    <%=EyouSoft.Toolkit.MeiJuHelper.GetSelectOption(typeof(EyouSoft.Model.FuKuanStatus))%>
                </select><input type="submit" class="search-btn" id="btnChaXun" />
            </p>
            </form>
        </div>
        <div class="tablehead">
            <ul class="fixed">
                <li><a href="javascript:void(0)" id="a_piliangfukuan">批量付款</a></li>
            </ul>
        </div>
        <!--列表表格-->
        <div class="tablelist-box" style="">
            <table width="100%" id="liststyle">
                <tr>
                    <th class="thinputbg" style="width:30px;">
                        <input type="checkbox" id="chk_quanxuan" />
                    </th>
                    <th class="thinputbg" style="width:40px;">
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
                    <!--<th align="left">
                        采购部门
                    </th>-->
                    <th align="left" style="width: 80px;">
                        采购人
                    </th>
                    <th align="left" style="width: 110px;">
                        采购时间
                    </th>
                    <th align="left" style="width: 110px;">
                        到货时间
                    </th>
                    <th style="text-align: right; width: 80px;">
                        采购金额&nbsp;
                    </th>
                    <th align="left" style="width: 90px;">
                        付款状态
                    </th>
                    <th align="center" style="width: 80px;">
                        操作
                    </th>
                </tr>
                <asp:Repeater runat="server" ID="rpt">
                    <ItemTemplate>
                        <tr data-itemid="<%#Eval("DingDanId") %>" class="table_tr_item" data-cgsfukuanstatus="<%#(int)Eval("CgsFuKuanStatus")%>">
                            <td style="text-align: center;">
                                <input type="checkbox" name="chk_quanxuan_item" />
                            </td>
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
                                <%#Eval("DaoHuoTime", "{0:yyyy-MM-dd}")%>
                            </td>
                            <td style="text-align: right;">
                                <%#GetJinE(Eval("JinE"),Eval("Status"))%>&nbsp;
                            </td>
                            <td align="left">
                                <%#Eval("CgsFuKuanStatus")%>
                            </td>
                            <td align="left">
                                <%#GetCaoZuo(Eval("CgsFuKuanStatus"))%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:PlaceHolder runat="server" ID="phHeJi">
                    <tr>
                        <td  colspan="8" style="height: 30px; text-align: right;">
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
                        <td class="even" colspan="11" style="height: 30px; text-align: center;">
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
            },
            fuKuan: function(obj) {
                if (!confirm("该操作不可撤销，你确定要付款吗？")) return false;

                $(obj).unbind("click").css({ "color": "#999999" });
                var _self = this;
                var _$tr = $(obj).closest("tr");
                var _data = { txtDingDanId: [] };
                _data.txtDingDanId.push(_$tr.attr("data-itemid"));

                $.ajax({ type: "post", url: "caiwuduizhang.aspx?doType=fukuan", data: _data, cache: false, dataType: "json", async: false,
                    success: function(response) {
                        alert(response.msg);
                        _self.reload();
                    }
                });
            },
            piLiangFuKuan: function() {
                var _data = { txtDingDanId: [] };
                $("input[name='chk_quanxuan_item']:checked").each(function() {
                var _$tr = $(this).closest("tr");
                if (_$tr.attr("data-cgsfukuanstatus") == "<%=(int)EyouSoft.Model.FuKuanStatus.已付款 %>") { $(this).prop("checked", false); return; }
                    _data.txtDingDanId.push(_$tr.attr("data-itemid"));
                });
                if (_data.txtDingDanId.length == 0) { alert("请选择需要支付的款项"); return; }
                if (!confirm("该操作不可撤销，你确定要付款吗？")) return false;
                var _self = this;
                $.ajax({ type: "post", url: "caiwuduizhang.aspx?doType=fukuan", data: _data, cache: false, dataType: "json", async: false,
                    success: function(response) {
                        alert(response.msg);
                        _self.reload();
                    }
                });
            }
        };

        $(document).ready(function() {
            mp.sheZhiWeiZhi("财务管理", "财务对账");

            var fenYePeiZhi = { pageSize: '<%=pageSize %>', pageIndex: '<%=pageIndex %>', recordCount: '<%=recordCount %>', showPrev: true, showNext: true, showDisplayText: true, showTiaoZhuan: true, cssClassName: 'page' }
            fenYe.replace("div_fenye", fenYePeiZhi);

            utilsUri.initChaXun();

            $('a[data-class="chakan"]').click(function() { iPage.chaKan(this); });
            $('a[data-class="fukuan"]').click(function() { iPage.fuKuan(this); });

            $("#chk_quanxuan").click(function() { $("input[name='chk_quanxuan_item']").prop("checked", $(this).prop("checked")); });
            $("#a_piliangfukuan").click(function() { iPage.piLiangFuKuan(); });

            $("tr.table_tr_item:nth-child(odd)").addClass("odd");
        });
    </script>

</asp:Content>
