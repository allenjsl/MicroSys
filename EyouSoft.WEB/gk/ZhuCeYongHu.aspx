<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ZhuCeYongHu.aspx.cs" Inherits="EyouSoft.WEB.gk.ZhuCeYongHu" MasterPageFile="~/mp/GK.Master" Title="注册用户管理-系统设置" %>

<asp:Content ContentPlaceHolderID="body" ID="body" runat="server">
    <div class="right_bar">
        <div class="right_box">
            <!-- InstanceBeginEditable name="EditRegion4" -->
            <div class="basicT">
                注册用户管理</div>
            <div class="searchbox fixed">
                <p>
                    公司名称：
                    <input type="text" class="formsize120 input-txt" id="txtGongSiName" name="txtGongSiName" />                    
                    用户名：
                    <input type="text" class="formsize120 input-txt" id="txtYongHuMing" name="txtYongHuMing" />
                    审核状态：
                    <select name="txtShenHeStatus" id="txtShenHeStatus" class="input_select">
                        <option value="">-请选择-</option>
                        <%=EyouSoft.Toolkit.MeiJuHelper.GetSelectOption(typeof(EyouSoft.Model.ShenHeStatus)) %>
                    </select>
                    <input type="submit" class="search-btn" id="btnChaXun" />
                </p>
            </div>
            <%--<div class="tablehead">
                <ul class="fixed">
                    <li><a href="javascript:void(0)" id="a_shenhe">批量审核</a></li>
                </ul>
            </div>--%>
            <!--列表表格-->
            <div class="tablelist-box mt10" style="">
                <table width="100%" id="liststyle">
                    <tr>
                        <th class="thinputbg">
                            序号
                        </th>
                        <th align="left">
                            公司类型
                        </th>
                        <th align="left">
                            公司名称
                        </th>
                        <th align="left">
                            法人姓名
                        </th>
                        <th align="left">
                            负责人姓名
                        </th>
                        <th align="left">
                            负责人电话
                        </th>
                        <th align="left">
                            注册用户名
                        </th>
                        <th align="left">
                            注册时间
                        </th>
                        <th align="left">
                            审核状态
                        </th>                        
                        <th align="center" style="width: 80px;">
                            操作
                        </th>
                    </tr>
                    <asp:Repeater runat="server" ID="rpt">
                        <ItemTemplate>
                            <tr data-yonghuid="<%#Eval("YongHuId") %>" data-gongsiid="<%#Eval("GongSiId") %>" class="table_tr_item">
                                <td align="center">
                                    <%# Container.ItemIndex + 1+( this.pageIndex - 1) * this.pageSize%>
                                </td>
                                <td align="left">
                                    <%#Eval("LeiXing")%>
                                </td>
                                <td align="left">
                                    <%#Eval("GongSiName")%>
                                </td>
                                <td align="left">
                                    <%#Eval("FaRenName")%>
                                </td>
                                <td align="left">
                                    <%#Eval("FuZeRenName")%>
                                </td>
                                <td align="left">
                                    <%#Eval("FuZeRenDianHua")%>
                                </td>
                                <td align="left">
                                    <%#Eval("YongHuMing")%>
                                </td>
                                <td align="left">
                                    <%#Eval("IssueTime","{0:yyyy-MM-dd}")%>
                                </td>
                                <td align="left">
                                    <%#Eval("ShenHeStatus")%>
                                </td>
                                <td align="left">
                                    <a href="javascript:void(0)" class="blue_btn" data-class="chakan">查看</a> <%#GetCaoZuo(Eval("ShenHeStatus")) %>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:PlaceHolder runat="server" ID="phEmpty">
                        <tr>
                            <td class="even" colspan="10" style="height: 30px; text-align: center;">
                                暂无注册用户信息。
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
            shenHe: function(obj) {
                if (!confirm("审核操作不可逆，你确定要审核吗？")) return false;
                var _$tr = $(obj).closest("tr");
                var _data = { txtYongHuId: _$tr.attr("data-yonghuid"), txtGongSiId: _$tr.attr("data-gongsiid") };
                var _self = this;

                $.ajax({ type: "post", cache: false, url: "zhuceyonghu.aspx?dotype=shenhe", dataType: "json", data: _data,
                    success: function(ret) {
                        alert(ret.msg);
                        _self.reload();
                    }
                });
            },
            chaKan: function(obj) {
                var _$tr = $(obj).closest("tr");
                var _data = { gongsiid: _$tr.attr("data-gongsiid") };

                Boxy.iframeDialog({ title: "公司信息-查看", iframeUrl: "/gk/gongsiedit.aspx", width: "750px", height: "500px", data: _data, afterHide: function() { iPage.reload(); } });
                return false;
            }
        };

        $(document).ready(function() {
            //mp.sheZhiWeiZhi("系统设置", "角色管理");

            var fenYePeiZhi = { pageSize: '<%=pageSize %>', pageIndex: '<%=pageIndex %>', recordCount: '<%=recordCount %>', showPrev: true, showNext: true, showDisplayText: true, showTiaoZhuan: true, cssClassName: 'page' }
            fenYe.replace("div_fenye", fenYePeiZhi);

            utilsUri.initChaXun();

            $('a[data-class="shenhe"]').click(function() { iPage.shenHe(this); });
            $('a[data-class="chakan"]').click(function() { iPage.chaKan(this); });

            $("tr.table_tr_item:nth-child(odd)").addClass("odd");
        });
    </script>

</asp:Content>