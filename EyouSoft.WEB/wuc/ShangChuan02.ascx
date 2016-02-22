<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ShangChuan02.ascx.cs" Inherits="EyouSoft.WEB.wuc.ShangChuan02" %>
<div id="<%=this.ClientID %>" style="width: 90%;">
    <input type="file" name="<%=FileClientId %>" id="<%=FileClientId %>" />
    <div class="<%=XianShiClassName %>" id="<%=XianShiClientId %>">
       
    </div>
    <div class="uploadify_queue" style="clear: both" id="<%=QueueClientId %>">
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
        wucSC.init02(_options);
    });

</script>

