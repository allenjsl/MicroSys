/**
* @fileOverview:enow.core.js
* @author:汪奇志 2015-05-06
* @requires:jquery-1.11.2.js
* @version:0.0.0.1
*/

(function() {

    if (!window.eNow) { window.eNow = {}; }

    //转换成int,转换失败返回0
    window.eNow.getInt = function(o) {
        if (isNaN(o)) return 0;

        if (parseInt(o)) {
            return parseInt(o);
        }

        return 0;
    };

    //转换float值，转换失败返回0,保留二位小数（四舍五入）
    window.eNow.getFloat = function(o) {
        if (isNaN(o)) return 0;

        if (parseFloat(o)) {
            var _v = parseFloat(o) * 100;
            _v = _v / 100;

            return Math.round(parseFloat(_v.toFixed(2)) * 100) / 100;
        }

        return 0;
    };

    //算术运算，a:操作数1 b:操作数2 c:运算符
    window.eNow.yunSuan = function(a, b, c) {
        var _v = 0;
        switch (c) {
            case "+":
                _v = (this.getFloat(a) * 100 + this.getFloat(b) * 100) / 100;
                break;
            case "-":
                _v = (this.getFloat(a) * 100 - this.getFloat(b) * 100) / 100;
                break;
            case "*":
                _v = (this.getFloat(a) * this.getFloat(b) * 100) / 100;
                break;
            case "/":
                b = this.getFloat(b);
                if (b == 0) { return 0; }
                _v = (this.getFloat(a) / this.getFloat(b) * 100) / 100;
                break;
            default: _v = 0;
        }

        return this.getFloat(_v);
    };

    window.eNow.regExps = {};
    //数字
    eNow.regExps["isDecimal"] = /^[-\+]?\d+(\.\d+)?$/;
    //邮箱
    eNow.regExps["isEmail"] = /([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)/;
    //电话
    eNow.regExps["isPhone"] = /^((\(\d{2,3}\))|(\d{3}\-))?(\(0\d{2,3}\)|0\d{2,3}-?)?[1-9]\d{6,7}(\-\d{1,4})?$/;
    //传真
    eNow.regExps["isFax"] = /^((\(\d{2,3}\))|(\d{3}\-))?(\(0\d{2,3}\)|0\d{2,3}-?)?[1-9]\d{6,7}(\-\d{1,4})?$/;
    //手机
    eNow.regExps["isMobile"] = /^(13|15|18|14)\d{9}$/;
    //电话+手机
    eNow.regExps["isTel"] = /^((\(\d{2,3}\))|(\d{3}\-))?(\(0\d{2,3}\)|0\d{2,3}-?)?[1-9]\d{6,7}(\-\d{1,4})?$|^(13|15|18|14)\d{9}$/;
    //身份证
    eNow.regExps["isSFZ"] = /(^\d{15}$)|(^\d{17}[0-9Xx]$)/;
})();
