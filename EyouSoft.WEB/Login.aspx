<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="EyouSoft.WEB.Login" %>

<!doctype html>
<html>
<head>
    <title>联速交易管理平台</title>
    <link href="css/basic.css?v=0.0.0.2" rel="stylesheet" type="text/css"/>
    <link href="css/login.css?v=0.0.0.2" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/js/jquery-1.11.2.js"></script>
</head>
<body>
    <div class="login_head">
        <div class="login_headbox">
            <img src="images/login_log.png"></div>
    </div>
    <div class="login_banner">
        <div class="login_warp">
            <div class="login_bar">
                <div class="login_box">
                    <form id="form1">
                    <ul>
                        <li>
                            <input type="text" class="inputbk" value="用户名" id="txt_u" name="txt_u"></li>
                        <li>
                            <input type="password" class="inputbk" value="00" id="txt_p" name="txt_p"></li>
                        <li class="mt20">
                            <input type="button" class="login_btn" value="登 录" id="btn_login"></li>
                        <li class="mt20"><a href="/zhuce.aspx?leixing=gys" class="g">供应商注册</a> <a href="/zhuce.aspx?leixing=cgs" class="c">采购商注册</a> </li>
                    </ul>
                    </form>
                </div>
            </div>
        </div>
    </div>
    <div class="login_foot mt10">
        <div class="login_footbox">
            <h1>
                <img src="images/login_about.png"></h1>
            <div class="login_about">
                <img src="images/login_bot.png">
                <p>
                    广西联速交易平台，是专门为酒店行业的采购方与供应方提供 相关交易服务的电子商务平台。联速交易平台主要负责订单、 采购方库存、采购配送流程等优化处理。解决供求双方财务对 账及结算管理、供求双方销售及库存产品等方面的自动化统计 与分析。相对于传统的下单、对帐、结果的方式，联速交易平 台具有快速、可靠的交易，高效的结算，智能化的统计分析， 无与伦比的安全性及万无一失的可靠性的特点。</p>
            </div>
            <div class="foot_txt mt10">
                前沿科技 版权所有</div>
        </div>
    </div>    
    
    <script type="text/javascript">
        var ipage = {
            Login: function() {
                if ($.trim($("#txt_u").val()).length == 0 || $.trim($("#txt_u").val()) == "用户名") {
                    alert("用户名不能为空");
                    $("#txt_u").focus();
                    return false;
                }

                if ($.trim($("#txt_p").val()).length == 0 || $.trim($("#txt_p").val()) == "00") {
                    alert("密码不能为空");
                    $("#txt_p").focus();
                    return false;
                }

                $.ajax({ type: "POST", url: "Login.aspx?type=login", data: $("#form1").serialize(), dataType: "json", cache: false,
                    success: function(data) {
                        if (data.result == "0") {
                            alert(data.msg);
                        } else {
                            window.location.href = data.obj;
                        }
                    }
                });
            }
        };

        $(document).ready(function() {
            $("#btn_login").unbind("click").bind("click", function() { ipage.Login(); });
            $("#txt_u,#txt_p").keypress(function(e) { if (e.keyCode == 13) { ipage.Login(); } });

            $("#txt_u").focus(function() { if ($.trim(this.value) == "用户名") { this.value = ""; $(this).css({ "color": "#333333" }); } });
            $("#txt_u").blur(function() { if ($.trim(this.value) == "") { this.value = "用户名"; $(this).css({ "color": "#c1c1c1" }); } });

            $("#txt_p").focus(function() { if ($.trim(this.value) == "00") { this.value = ""; $(this).css({ "color": "#333333" }); } });
            $("#txt_p").blur(function() { if ($.trim(this.value) == "") { this.value = "00"; $(this).css({ "color": "#c1c1c1" }); } });
        });
    </script>
</body>
</html>
