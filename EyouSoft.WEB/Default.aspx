<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EyouSoft.WEB._Default" %>

<%@ Register Assembly="EyouSoft.Toolkit" Namespace="EyouSoft.Toolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script type="text/javascript" src="/js/jquery-1.11.2.js"></script>
    <script type="text/javascript" src="/js/jquery.boxy.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:FenYe ID="fenYe" runat="server" />
        
        <a href="javascript:void(0)" onclick="f1()">F1</a>
    </div>
    </form>
    
    
    <script type="text/javascript">
        function f1() {
            var _title = "T";
            var _data = { txt1: "1", txt2: "2" };

            function reload() {
                window.location.href = window.location.href;
            }

            top.Boxy.iframeDialog({ title: _title, iframeUrl: "login.aspx.aspx", width: "770px", height: "500px", data: _data, afterHide: function() { reload(); } });
            return false;
        }
    </script>
</body>
</html>
