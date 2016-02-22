<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ZhuCe.aspx.cs" Inherits="EyouSoft.WEB.ZhuCe" %>

<!doctype html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>用户注册-联速交易管理平台</title>
    <link href="css/basic.css" rel="stylesheet" type="text/css" />
    <link href="css/login.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/js/jquery-1.11.2.js"></script>
    <script type="text/javascript" src="/js/enow.core.js"></script>
    <script type="text/javascript" src="/js/md5-min.js"></script>

</head>
<body>
    <div class="login_head">
        <div class="login_headbox">
            <a href="/login.aspx"><img src="images/login_log.png"></a></div>
    </div>
    <div class="reg_form">
        <ul>
            <li><span class="name">单位名称：</span><input type="text" class="input_bk formsize350" id="txt_gongsiname" name="txt_gongsiname" />
                <span class="font_red">*</span></li>
            <li><span class="name">法人姓名：</span><input type="text" class="input_bk formsize350" id="txt_farenname" name="txt_farenname" /><span class="font_red">* </span></li>
            <li><span class="name">负责人姓名：</span><input type="text" class="input_bk formsize350" id="txt_fuzerenname" name="txt_fuzerenname" /><span class="font_red">* </span></li>
            <li><span class="name">负责人电话：</span><input type="text" class="input_bk formsize350" id="txt_fuzerendianhua" name="txt_fuzerendianhua" /><span class="font_red">* </span></li>
            <li><span class="name">单位地址：</span><input type="text" class="input_bk formsize350" id="txt_dizhi" name="txt_dizhi" /><span class="font_red">* </span></li>
            <li><span class="name">用户名：</span><input type="text" class="input_bk formsize350" id="txt_yonghuming" name="txt_yonghuming" />
                <span class="font_red">* </span></li>
            <li><span class="name">密码：</span><input type="password" class="input_bk formsize350" id="txt_mima1" name="txt_mima1" />
                <span class="font_red">* </span></li>
            <li><span class="name">确认密码：</span><input type="password" class="input_bk formsize350" id="txt_mima2" name="txt_mima2" />
                <span class="font_red">* </span></li>
        </ul>
        <div class="reg_btn">
            <input type="button" value="注册" id="btn_zhuce" /></div>
    </div>

    <script type="text/javascript">
        function reload() {
            window.location.href = window.location.href;
        }

        function redirectLogin() {
            window.location.href = "/login.aspx";
        }

        function yanZhengForm() {
            if ($.trim($("#txt_gongsiname").val()).length == 0) { alert("请输入单位名称"); return false; }
            if ($.trim($("#txt_farenname").val()).length == 0) { alert("请输入法人姓名"); return false; }
            if ($.trim($("#txt_fuzerenname").val()).length == 0) { alert("请输入负责人姓名"); return false; }
            if ($.trim($("#txt_fuzerendianhua").val()).length == 0) { alert("请输入负责人电话"); return false; }
            if (!eNow.regExps["isTel"].test($.trim($("#txt_fuzerendianhua").val()))) { alert("请输入正确的负责人电话"); return false; }
            if ($.trim($("#txt_dizhi").val()).length == 0) { alert("请输入单位地址"); return false; }
            if ($.trim($("#txt_yonghuming").val()).length == 0) { alert("请输入用户名"); return false; }
            if ($.trim($("#txt_mima1").val()).length == 0) { alert("请输入密码"); return false; }
            if ($.trim($("#txt_mima2").val()).length == 0) { alert("请输入确认密码"); return false; }
            if ($.trim($("#txt_mima1").val()) != $.trim($("#txt_mima2").val())) { alert("两次输入的密码不一致"); return false; }
            return true;
        }

        function zhuCe(obj) {
            if (!yanZhengForm()) return false;
            var _data = {};
            _data["txt_gongsiname"] = $.trim($("#txt_gongsiname").val());
            _data["txt_farenname"] = $.trim($("#txt_farenname").val());
            _data["txt_fuzerenname"] = $.trim($("#txt_fuzerenname").val());
            _data["txt_fuzerendianhua"] = $.trim($("#txt_fuzerendianhua").val());
            _data["txt_dizhi"] = $.trim($("#txt_dizhi").val());
            _data["txt_yonghuming"] = $.trim($("#txt_yonghuming").val());
            _data["txt_mima"] = hex_md5($.trim($("#txt_mima1").val()));

            $(obj).unbind("click").css({ "color": "#999999" });
            var _url = window.location.href;
            if (_url.indexOf('?') == -1) _url += "?";
            _url += "&doType=zhuce";
            $.ajax({ type: "post", url: _url, data: _data, cache: false, dataType: "json", async: false,
                success: function(response) {
                    alert(response.msg);
                    if (response.result == 1) redirectLogin();
                    else $(obj).click(function() { zhuCe(this); }).css({ "color": "" });
                }
            });
        }

        $(document).ready(function() {
            $("#btn_zhuce").click(function() { zhuCe(this); });
        });    
    </script>

</body>
</html>
