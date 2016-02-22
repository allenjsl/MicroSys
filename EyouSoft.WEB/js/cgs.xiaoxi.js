//采购商消息处理 汪奇志 2015-05-08
//@requires:jquery-1.11.2.js
//@options.selector 显示消息数量的选择器
//@options.delay 获取消息数量的间隔时间
//@options.cacheexpiration cache有效时间

var cgsXiaoXi = {
    options: { "selector": "#em_xiaoxi_shuliang", "delay": 60000, cacheexpiration: 60000 }
    , _setCache: function(data) {
        $(this.options.selector).data("xiaoxi-time", new Date().getTime());
        $(this.options.selector).data("xiaoxi-data", data);
    }
    , _getCache: function() {
        var _time = $(this.options.selector).data("xiaoxi-time");
        var _data = $(this.options.selector).data("xiaoxi-data");

        if (typeof _time == "undefined") return null;
        if (typeof _data == "undefined") return null;

        var _time1 = new Date().getTime();

        var _cha = _time1 - _time;

        if (_cha < this.options.cacheexpiration) return _data;

        return null;
    }
    , _getXiaoXi: function(callback) {
        var _async = false;
        if (typeof callback == "function") _async = true;
        var _data = [];
        var _self = this;
        _data = this._getCache();

        if (_data != null) {
            if (_async) callback(_data);

            return _data;
        }

        $.ajax({
            url: "/ashx/handler.ashx?dotype=getcgsxiaoxi", cache: false, async: _async, dataType: "json",
            success: function(response) {
                _data = { ShuLiang: 0 };
                if (response.result == "1") _data = response.obj;
                if (_async) {
                    _self._setCache(_data);
                    callback(_data);
                }
            }
        });

        _self._setCache(_data);
        return _data;
    }
    , _getShuLiang: function() {
        var _self = this;
        function _initShuLaing(data) {
            $(_self.options.selector).html(data.ShuLiang);
            setTimeout(function() { _self._getShuLiang(); }, _self.options.delay);
        }

        this._getXiaoXi(_initShuLaing);
    }
    , init: function() {
        this._getShuLiang();
    }
};

$(document).ready(function() { cgsXiaoXi.init(); });
