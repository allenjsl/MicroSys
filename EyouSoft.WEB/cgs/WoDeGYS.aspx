<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WoDeGYS.aspx.cs" Title="我的供应商-系统设置" Inherits="EyouSoft.WEB.cgs.WoDeGYS" MasterPageFile="~/mp/Cgs.Master" %>


<asp:Content ContentPlaceHolderID="PageBody" ID="PageBody1" runat="server">
    <div class="right_box">
        <!-- InstanceBeginEditable name="EditRegion4" -->
        <div class="basicT">
            我的供应商</div>
        <div class="searchbox fixed">
            <form action="" method="get">
            <p>
                供应商名称：
                <input type="text" class="formsize120 input-txt" id="txtName" name="txtName" />
                我的供应商：<select id="txtIsGuanXi" name="txtIsGuanXi" class="input_select"><option value="">-请选择-</option>
                    <option value="1">是</option>
                    <option value="0">否</option>
                </select>
                <input type="submit" class="search-btn" id="btnChaXun" />
            </p>
            </form>
        </div>
        <div class="bb1">
        </div>
        <!--列表表格-->
        <div class="tablelist-box mt10" style="">
            <table width="100%" id="liststyle">
                <tr>
                    <th class="thinputbg">
                        序号
                    </th>
                    <th align="left">
                        供应商
                    </th>
                    <th align="left" style="width:150px;">
                        法人代表
                    </th>
                    <th style="text-align: left;width:150px;" >
                        负责人&nbsp;
                    </th>
                    <th style="text-align: left;width:150px;">
                        负责人电话&nbsp;
                    </th>
                    <th align="center" style="width: 150px;">
                        操作
                    </th>
                </tr>
                <asp:Repeater runat="server" ID="rpt">
                    <ItemTemplate>
                        <tr data-itemid="<%#Eval("GongSiId") %>" data-isguanxi="<%#(bool)Eval("IsGuanZhu")?"1":"0" %>" class="table_tr_item" title="选中后将会设置为自己的供应商">
                            <td align="center">
                                <%# Container.ItemIndex + 1+( this.pageIndex - 1) * this.pageSize%>
                            </td>
                            <td align="left">
                                <%#Eval("Name")%>
                            </td>
                            <td align="left">
                                <%#Eval("FanRenName")%>
                            </td>
                            <td style="text-align: left;">
                                <%#Eval("FuZeRenName")%>&nbsp;
                            </td>
                            <td style="text-align: left;">
                                <%#Eval("FuZeRenDianHua")%>&nbsp;
                            </td>
                            <td align="center">
                                <input type="checkbox" name="chkGuanXi"  />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:PlaceHolder runat="server" ID="phEmpty">
                    <tr>
                        <td class="even" colspan="10" style="height: 30px; text-align: center;">
                            暂无供应商信息。
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
            sheZhi: function(obj) {
                var _$tr = $(obj).closest("tr");
                var _data = { txtGongSiId: _$tr.attr("data-itemid"), txtIsGuanXi: "0" }
                if ($(obj).prop("checked")) _data.txtIsGuanXi = "1";

                var _self = this;
                $.ajax({ type: "post", cache: false, url: "wodegys.aspx?dotype=shezhi", dataType: "json", data: _data,
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
            mp.sheZhiWeiZhi("系统设置", "我的供应商");

            var fenYePeiZhi = { pageSize: '<%=pageSize %>', pageIndex: '<%=pageIndex %>', recordCount: '<%=recordCount %>', showPrev: true, showNext: true, showDisplayText: true, showTiaoZhuan: true, cssClassName: 'page' }
            fenYe.replace("div_fenye", fenYePeiZhi);

            utilsUri.initChaXun();

            $("tr.table_tr_item").each(function() { if ($(this).attr("data-isguanxi") == "1") $(this).find("input[name='chkGuanXi']").prop("checked", "checked"); });

            $("input[name='chkGuanXi']").click(function() { iPage.sheZhi(this); });

            $("tr.table_tr_item:nth-child(odd)").addClass("odd");
        });
    </script>

</asp:Content>
