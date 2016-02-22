<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ShangChuan.ascx.cs" Inherits="EyouSoft.WEB.wuc.ShangChuan" %>

<div id="<%=this.ClientID %>" style="width:90%;">
    <input type="file" name="<%=FileClientId %>" id="<%=FileClientId %>" />
    <div class="uploadify_shuoming"><%=ShuoMing %></div><div style="clear:both;"></div>
    <div class="uploadify_queue" style="clear: both" id="<%=QueueClientId %>">
    </div>
    <div class="<%=XianShiClassName %>" id="<%=XianShiClientId %>">
        <ul></ul>
        <div style="clear: both;"></div>
    </div>
</div>
<script type="text/javascript">

    $(document).ready(function() {
        var _options = {};
        _options["ClientId"] = "<%=ClientID %>";
        _options["FileClientId"] = "<%=FileClientId %>";
        _options["QueueClientId"] = "<%=QueueClientId %>";
        _options["XianShiClientId"] = "<%=XianShiClientId %>";
        _options["FileTypeDesc"] = "<%=FileTypeDesc %>";
        _options["FileTypeExts"] = "<%=FileTypeExts %>";
        _options["FilepathClientName"] = "<%=FilepathClientName %>";
        _options["YuanFilepathClientName"] = "<%=YuanFilepathClientName %>";
        _options["Multi"] = "<%=Multi %>";
        _options["YuanFiles"] = JSON.parse('<%=YuanFilesJson %>');
        wucSC.init(_options);
    });

</script>
