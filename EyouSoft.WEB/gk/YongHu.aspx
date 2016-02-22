<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YongHu.aspx.cs" Inherits="EyouSoft.WEB.gk.YongHu" Title="用户管理-系统设置" MasterPageFile="~/mp/Gk.Master" %>
<%@ Import Namespace="EyouSoft.Toolkit" %>

<asp:Content ContentPlaceHolderID="head" ID="head" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="body" ID="body" runat="server">
<div class="right_bar">
    <div class="right_box">
        <!-- InstanceBeginEditable name="EditRegion4" -->
        <div class="basicT">
            用户管理</div>
        <div class="searchbox fixed">
            <p>
                用户姓名：
                <input type="text" class="formsize120 input-txt" id="txtName" name="txtName" />
                用户账号：
                <input type="text" class="formsize80 input-txt" id="txtUsername" name="txtUsername" />
                <%if(T!=EyouSoft.Model.YongHuLeiXing.平台) %>
                <%{ %>
                <%=T%>：<input type="text" class="formsize120 input-txt" id="txtGongSiName" name="txtGongSiName"/>
                <%} %>
                用户状态：<select id="txtStatus" name="txtStatus" class="input_select">
                    <option value="">请选择</option>
                    <%=EyouSoft.Toolkit.MeiJuHelper.GetSelectOption(typeof(EyouSoft.Model.YongHuStatus))%>
                </select><input type="submit" class="search-btn" id="btnChaXun" />
            </p>
        </div>
        <div class="tablehead">
            <ul class="fixed">
                <li><a href="javascript:void(0)" id="a_tianjia">新增</a></li>
                <%--<li><a href="javascript:void(0)">批量禁用</a></li><li><a href="javascript:void(0)">批量删除</a></li>--%>
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
                    <th align="center">
                        账号
                    </th>
                    <th align="center">
                        姓名
                    </th>
                    <th align="center">
                        账号状态
                    </th>
                    <th align="center">
                        <% switch (T) %>
                        <%{ %>
                        <%  case  EyouSoft.Model.YongHuLeiXing.平台: %>
                                    部门名称
                        <%          break; %>
                        <%  default: %>
                                <%=T%>名称
                        <%          break; %>
                        <%} %>
                    </th>
                    <th align="center">
                        联系电话
                    </th>
                    <th align="center">
                        联系手机
                    </th>
                    <th align="center" style="width:180px;">
                        操作
                    </th>
                </tr>
                <asp:Repeater runat="server" ID="rpt">
                    <ItemTemplate>
                        <tr data-itemid="<%#Eval("YongHuId") %>" class="table_tr_item">
                            <td align="center">
                                <%--<input type="checkbox" name="chk_quanxuan_item" />--%>
                                <%# Container.ItemIndex + 1+( this.pageIndex - 1) * this.pageSize%>
                            </td>
                            <td align="center">
                                <%#Eval("Username")%>
                            </td>
                            <td align="center">
                                <%#Eval("Name")%>
                            </td>
                            <td align="center">
                                <%#Eval("Status")%>
                            </td>
                            <td align="center">
                        <% switch (T) %>
                        <%{ %>
                        <%  case  EyouSoft.Model.YongHuLeiXing.平台: %>
                                <%#Eval("BuMenName")%>
                        <%          break; %>
                        <%  default: %>
                                <%#Eval("GongSiName")%>
                        <%          break; %>
                        <%} %>
                            </td>
                            <td align="center">
                                <%#Eval("DianHua")%>
                            </td>
                            <td align="center">
                                <%#Eval("ShouJi")%>
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
                            暂无用户信息。
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
            reload: function() {
                window.location.href = window.location.href;
            },
            tianJia: function() {
                var _data = {T:<%=(int)T %>}
                Boxy.iframeDialog({ title: "用户-新增", iframeUrl: "yonghuedit.aspx", width: "870px", height: "540px", data: _data, afterHide: function() { iPage.reload(); } });
                return false;
            },
            bianJi: function(obj) {
                var _$tr = $(obj).closest("tr");
                var _data = { T:<%=(int)T %>,editid: _$tr.attr("data-itemid") };
                Boxy.iframeDialog({ title: "用户-编辑", iframeUrl: "yonghuedit.aspx", width: "870px", height: "540px", data: _data, afterHide: function() { iPage.reload(); } });
                return false;
            },
            shanChu: function(obj) {
                if (!confirm("删除操作不可恢复，你确定要删除吗？")) return false;
                var _$tr = $(obj).closest("tr");
                var _data = { txtYongHuId: _$tr.attr("data-itemid") };
                var _self = this;
                $.ajax({ type: "post", cache: false, url: "yonghu.aspx?dotype=shanchu", dataType: "json", data: _data,
                    success: function(ret) {
                        alert(ret.msg);
                        _self.reload();
                    },
                    error: function() {
                        _self.reload();
                    }
                });
            },
            qiYong: function(obj) {
                if (!confirm("启用账号后可该账号可正常使用系统，你确定要启用该用户账号吗？")) return false;
                var _$tr = $(obj).closest("tr");
                var _data = { txtYongHuId: _$tr.attr("data-itemid") };
                var _self = this;
                $.ajax({ type: "post", cache: false, url: "yonghu.aspx?dotype=qiyong", dataType: "json", data: _data,
                    success: function(ret) {
                        alert(ret.msg);
                        _self.reload();
                    },
                    error: function() {
                        _self.reload();
                    }
                });
            },
            jinYong: function(obj) {
                if (!confirm("禁用账号后该账号将不能登录系统，你确定要禁用该用户账号吗？")) return false;
                var _$tr = $(obj).closest("tr");
                var _data = { txtYongHuId: _$tr.attr("data-itemid") };
                var _self = this;
                $.ajax({ type: "post", cache: false, url: "yonghu.aspx?dotype=jinyong", dataType: "json", data: _data,
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
           // mp.sheZhiWeiZhi("系统设置", "用户管理");

            var fenYePeiZhi = { pageSize: '<%=pageSize %>', pageIndex: '<%=pageIndex %>', recordCount: '<%=recordCount %>', showPrev: true, showNext: true, showDisplayText: true, showTiaoZhuan: true, cssClassName: 'page' }
            fenYe.replace("div_fenye", fenYePeiZhi);

            utilsUri.initChaXun();

            $("#chk_quanxuan").click(function() { $("input[name='chk_quanxuan_item']").prop("checked", $(this).prop("checked")); });
            $("#a_tianjia").click(function() { iPage.tianJia(); });
            $('a[data-class="bianji"]').click(function() { iPage.bianJi(this); });
            $('a[data-class="qiyong"]').click(function() { iPage.qiYong(this); });
            $('a[data-class="jinyong"]').click(function() { iPage.jinYong(this); });
            $('a[data-class="shanchu"]').click(function() { iPage.shanChu(this); });
            
            $("tr.table_tr_item:nth-child(odd)").addClass("odd");
        });
    </script>
</asp:Content>
