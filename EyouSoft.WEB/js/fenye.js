/*
JavaScript分页 汪奇志 2015-04-30
fenYe.config.showPrev 是否显示上一页
fenYe.config.showNext 是否显示下一页
fenYe.config.showDisplayText 是否显示共N条记录，N/N页
fenYe.config.showTiaoZhuan 是否显示跳转
*/

if (!window.fenYe) {
    window.fenYe = (function() {
        var fenYe = {
            version: '0.0.2',
            author: 'wangqizhi',
            description: 'JavaScript分页',
            config: {
                position: 'center',
                cssClassName: 'page',
                pageSize: 10,
                pageIndex: 1,
                recordCount: 0,
                pageCount: 0,
                gotoPageFunctionName: 'fenYe.gotoPage',
                showPrev: true,
                showNext: true,
                showDisplayText: true,
                showTiaoZhuan:true
            }
        };
        return fenYe;
    })();
}

fenYe.getObj = function(elementId) {
    return document.getElementById(elementId);
};

fenYe.replace = function(elementId, config) {
    this.createConfig(config);
    this.getObj(elementId).innerHTML = this.createHTML();
};

fenYe.createConfig = function(config) {
    //if (config == null || config == 'undefined') return;

    for (var key in config) {
        if (key == 'pageCount') { continue; }
        this.config[key] = config[key];
    }

    if (isNaN(this.config.recordCount)) this.config.recordCount = 0;
    if (isNaN(this.config.pageSize)) this.config.pageSize = 1;
    if (isNaN(this.config.pageIndex)) this.config.pageIndex = 1;

    this.config.recordCount = parseInt(this.config.recordCount);
    this.config.pageSize = parseInt(this.config.pageSize);
    this.config.pageIndex = parseInt(this.config.pageIndex);

    this.config.pageCount = Math.ceil(this.config.recordCount / this.config.pageSize);
    if (this.config.pageIndex < 1) this.config.pageIndex = 1;
    if (this.config.pageIndex > this.config.pageCount) this.config.pageIndex = this.config.pageCount;
};

fenYe.createDisplayText = function() {
    var startRecordIndex = (this.config.pageIndex - 1) * this.config.pageSize;
    var finishRecordIndex = startRecordIndex + this.config.pageSize;
    if (this.config.recordCount > 0) startRecordIndex++;
    if (startRecordIndex < 0) startRecordIndex = 0;
    if (finishRecordIndex > this.config.recordCount) finishRecordIndex = this.config.recordCount;

    var s = new Array();

    s.push('<span>共<em>' + this.config.recordCount + '</em>条记录，' + this.config.pageIndex + '/' + this.config.pageCount + '页</span>')

    return s.join('');
};

fenYe.createEllipsis = function() {
    return '<span>...</span>';
};

fenYe.createPrev = function() {
    if (this.config.pageIndex > 1) {
        var prevPageIndex = this.config.pageIndex - 1;
        return '<a href="javascript:void(0)" onclick="' + this.config.gotoPageFunctionName + '(' + prevPageIndex + ')">上一页 </a>';
    } else {
        return '<span class="disabled">上一页 </span>';
    }
};

fenYe.createPage = function(pageIndex) {
    var s = '';

    if (pageIndex != this.config.pageIndex) {
        s = '<a href="javascript:void(0)" onclick="' + this.config.gotoPageFunctionName + '(' + pageIndex + ')">' + pageIndex + '</a>';
    } else {
        s = '<span class="current">' + pageIndex + '</span>';
    }

    return s;
};

fenYe.createNext = function() {
    if (this.config.pageIndex != this.config.pageCount) {
        var nextPageIndex = this.config.pageIndex + 1;
        return '<a href="javascript:void(0)" onclick="' + this.config.gotoPageFunctionName + '(' + nextPageIndex + ')">下一页 </a>';
    } else {
        return '<span class="disabled">下一页 </span>';
    }
}

fenYe.tiaoZhuan = function() {
    var obj = this.getObj("txt_fenye_tiaozhuan");
    var s = obj.value;
    if (s.length == 0) { alert("请输入页"); return false; }
    if (isNaN(s)) { alert("请输入页"); return false; }
    var _index = parseInt(s);

    if (_index < 1) { alert("请输入正确的页"); return false; }
    if (_index > this.config.pageCount) { alert("请输入正确的页"); return false; }

    if (this.config.gotoPageFunctionName.indexOf('.') == -1) {
        window[this.config.gotoPageFunctionName](_index);
        return;
    }

    var _arr = this.config.gotoPageFunctionName.split(".");
    if (_arr.length > 2) { alert("分页配置错误"); return; }
    window[_arr[0]][_arr[1]](_index);
}

fenYe.createTiaoZhuan = function() {
    return '<span>到&nbsp;&nbsp;<input type="text" class="page_input" id="txt_fenye_tiaozhuan">&nbsp;&nbsp;页&nbsp;&nbsp;</span><a class="pag_btn" href="javascript:void(0)" onclick="fenYe.tiaoZhuan()">确定</a>';
}

fenYe.createHTML = function(elementId) {
    var s = new Array();

    switch (this.config.position) {
        case 'center':
            s.push('<div class="' + this.config.cssClassName + '">');
            break;
        case 'left':
            s.push('<div class="' + this.config.cssClassName + '">');
            break;
        case 'right':
            s.push('<div class="' + this.config.cssClassName + '">');
            break;
    }

    var startRecordIndex = (this.config.pageIndex - 1) * this.config.pageSize;
    var finishRecordIndex = startRecordIndex + this.config.pageSize;
    if (this.config.recordCount > 0) startRecordIndex++;
    if (finishRecordIndex > this.config.recordCount) finishRecordIndex = this.config.recordCount;

    if (this.config.showDisplayText) {
        s.push(this.createDisplayText());
    }

    if (this.config.showPrev) {
        s.push(this.createPrev());
    }

    /*
    分页控件最多同时显示9个页面的页列表
    flag=0:分页显示所有的页列表
    flag=1:分页控件显示前面的7页列表+....+最后两页
    flag=2:分页控件显示前2页+...+当前页面前2页面至当前页后面2页页列表+....+最后2页    
    flag=3:分页控件显示前2页+...+最后7个页页列表
    */
    var flag = 0;

    if (this.config.pageCount <= 9) {
        flag = 0;
    } else if (this.config.pageIndex <= 5) {
        flag = 1;
    } else if (this.config.pageIndex + 4 < this.config.pageCount) {
        flag = 2;
    } else {
        flag = 3;
    }

    if (flag == 0) {
        for (var i = 1; i <= this.config.pageCount; i++) {
            s.push(this.createPage(i));
        }
    }

    if (flag == 1) {
        for (var i = 1; i <= 7; i++) {
            s.push(this.createPage(i));
        }
        s.push(this.createEllipsis());
        for (var i = this.config.pageCount - 1; i <= this.config.pageCount; i++) {
            s.push(this.createPage(i));
        }
    }

    if (flag == 2) {
        for (var i = 1; i <= 2; i++) {
            s.push(this.createPage(i));
        }

        s.push(this.createEllipsis());

        for (var i = this.config.pageIndex - 2; i <= this.config.pageIndex + 2; i++) {
            s.push(this.createPage(i));
        }

        s.push(this.createEllipsis());

        for (var i = this.config.pageCount - 1; i <= this.config.pageCount; i++) {
            s.push(this.createPage(i));
        }
    }

    if (flag == 3) {
        for (var i = 1; i <= 2; i++) {
            s.push(this.createPage(i));
        }

        s.push(this.createEllipsis());

        for (var i = this.config.pageCount - 6; i <= this.config.pageCount; i++) {
            s.push(this.createPage(i));
        }
    }

    if (this.config.showNext) {
        s.push(this.createNext());
    }

    if (this.config.showTiaoZhuan) {
        s.push(this.createTiaoZhuan());
    }

    s.push(this.createHiddens(elementId));
    s.push('</div>');

    return s.join('');
};

fenYe.createUrl = function(url, params) {
    if (url == null || url.length < 1) url = window.location.pathname;
    var isHaveParam = false;
    var isHaveQuestionMark = false;
    var questionMark = "?";
    var questionMarkIndex = url.indexOf(questionMark);
    var urlLength = url.length;

    if (questionMarkIndex == urlLength - 1) {
        isHaveQuestionMark = true;
    } else if (questionMarkIndex != -1) {
        isHaveParam = true;
    }

    if (isHaveParam == true) {
        for (var key in params) {
            url = url + "&" + key + "=" + encodeURIComponent(params[key]);
        }
    } else {
        if (isHaveQuestionMark == false) {
            url += questionMark;
        }
        for (var key in params) {
            url = url + key + "=" + encodeURIComponent(params[key]) + "&";
        }
        url = url.substr(0, url.length - 1);
    }

    return url;
};

fenYe.getUrlParms = function(removeParams) {
    removeParams = removeParams || [];
    var argsArr = {};
    var query = window.location.search;
    query = query.substring(1);
    var pairs = query.split("&");

    for (var i = 0; i < pairs.length; i++) {
        var sign = pairs[i].indexOf("=");
        if (sign == -1) {
            continue;
        }

        var aKey = pairs[i].substring(0, sign);
        var aValue = decodeURIComponent(pairs[i].substring(sign + 1));

        /*移除不需要要的键*/
        var isRemove = false;
        for (var j = 0; j < removeParams.length; j++) {
            if (aKey.toLowerCase() == removeParams[j].toLowerCase()) {
                isRemove = true;
                break;
            }
        }

        if (isRemove) {
            continue;
        }

        argsArr[aKey] = aValue;
    }

    return argsArr;
};

fenYe.gotoPage = function(pageIndex) {
    var url = window.location.pathname;
    var urlParms = this.getUrlParms(["page"]);
    urlParms["Page"] = pageIndex;

    window.location.href = this.createUrl(url, urlParms);
}

fenYe.createHiddens = function(elementId) {
    var s = new Array();
    var inputId = "fenYe_PageIndex_" + elementId;
    //当前页索引隐藏域
    s.push('<input type="hidden" name="' + inputId + '" id="' + inputId + '" value="' + this.config.pageIndex + '" />');
    return s.join('');
}
