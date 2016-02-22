/**
* @fileOverview:公司自动完成
* @author:汪奇志 2015-06-16
* @requires:jquery-1.11.2.js
* @version:0.0.0.1
*/

var gongSiAutocomplete = {
    jiShu: 0,
    //options:{txt_id_id:"公司编号input id",txt_name_id:"公司名称input id",gslx:"公司类型[供应商=0][采购商=1]",pplx:"匹配类型[允许匹配不到信息的录入=0][不允许匹配不到信息的录入=1]",callback:function(data){}}
    init: function(options) {
        if (typeof options.txt_id_id == "undefined" || options.txt_id_id.length == 0) return;
        if (typeof options.txt_name_id == "undefined" || options.txt_name_id.length == 0) return;
        var _callback_fn = function(data) { };
        if (typeof options.callback == "function") _callback_fn = options.callback;

        var _options = {
            source: function(request, response) {
                $.ajax({
                    url: "/ashx/handler.ashx?dotype=getautocompletegongsi",
                    dataType: "json",
                    data: {
                        q: request.term, gslx: options.gslx
                    },
                    success: function(data) {
                        response(data);
                    }
                });
            },
            focus: function(event, ui) {
                //$("#" + options.txt_id_id).val(ui.item.GongSiId);
                //$(this).val(ui.item.GongSiName);
                return false;
            },
            select: function(event, ui) {
                $("#" + options.txt_id_id).val(ui.item.GongSiId);
                $(this).val(ui.item.GongSiName);
                _callback_fn({ GongSiId: ui.item.GongSiId, GongSiName: ui.item.GongSiName });
                return false;
            },
            minLength: 1,
            close: function(event) {
                if (options.pplx == 1) {
                    if ($("#" + options.txt_id_id).val().length == 0) {
                        $(this).val("");
                        _callback_fn({ GongSiId: "", GongSiName: "" });
                    }
                } else {
                    if ($(this).val() == "未匹配到公司信息") $(this).val("");
                }
            },
            change: function(event, item) {
                if (options.pplx == 1) {
                    if (typeof item == "undefined" || item == null || typeof item.item == "undefined" || item.item == null) {
                        $(this).val("");
                        $("#" + options.txt_id_id).val("");
                        _callback_fn({ GongSiId: "", GongSiName: "" });
                    }
                }
            }
        };

        $("#" + options.txt_name_id).autocomplete(_options).autocomplete("instance")._renderItem = function(ul, item) {
            return $("<li>").append("<a>" + item.GongSiName + "</a>").appendTo(ul);
        };

        this.jiShu++;
    }
};


