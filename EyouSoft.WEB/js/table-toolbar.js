var tableToolbar = {
	init:function(options){
		this.options = jQuery.extend(tableToolbar.DEFAULTS, options || {});

		this.container = $(this.options.tableContainerSelector);

		
		if(this.options.allSelectedCheckBoxSelector==""){
			if(this.options.isHaveAllSelectedFun){
				this.allSelectedBox = this.container.find("input[type='checkbox']").eq(0);
			}else{
				this.allSelectedBox = $([]);
			}
		}else{
			this.allSelectedBox = $(this.options.allSelectedCheckBoxSelector);
		}
		
		if(this.options.checkBoxSelector==""){
			if(this.options.isHaveAllSelectedFun){
				this.checkboxList = this.container.find("input[type='checkbox']:gt(0)");
			}else{
				this.checkboxList = this.container.find("input[type='checkbox']");
			}
		}else{
			this.checkboxList = $(this.options.checkBoxSelector);
		}

		this._initSelectedFun();
		this._initButtonFun();
		this._initTableStyle();
	},
	/* public */
	getSelectedColCount:function(){
		var count = 0;
		this.checkboxList.each(function(){
			if(this.checked){
				count++;	
			}
		});

		return count;
	},
	getSelectedCols:function(){
		var cols = [];
		this.checkboxList.each(function(){
			if(this.checked){
				cols.push($(this).closest("tr"));
			}
		});

		return cols;
	},
	/* private */
    _initSelectedFun:function(){
		var self = this;
		if(this.allSelectedBox.length>0){
			this.allSelectedBox.click(function(){
				var check = this.checked;
				if(check){
					self.checkboxList.attr("checked","checked");
					self.container.find("tr").addClass("selected");
				}else{
					self.checkboxList.removeAttr("checked");
					self.container.find("tr").removeClass("selected");
				}
			});
		}

		this.checkboxList.click(function(){
			var check = this.checked;
			if(check){
				self.allSelectedBox.attr("checked","checked");
				$(this).closest("tr").addClass('selected');
			}else{
				$(this).closest("tr").removeClass('selected');
			}
		});
	},
	_initButtonFun:function(){
		var self = this;
		//this.add = $(this.add_selector);
		
		this.update = $(this.options.update_selector);
		if(this.update.length>0){
			this.update.click(function(){
				var count = self.getSelectedColCount();
				var isSucess = false;
				var msg;
				if(count==0){
					isSucess = false;
					msg = self.options.update_msg1.replace(new RegExp(self.options.objectNamePlaceHolder,"gm"),self.options.objectName);
				}else if(count>1){
					isSucess = false;
					msg = self.options.update_msg2.replace(new RegExp(self.options.objectNamePlaceHolder,"gm"),self.options.objectName);
				}else{
					isSucess = true;
				}

				if(isSucess){
					if(self.options.updateCallBack!=null){
						self.options.updateCallBack(self.getSelectedCols());
						return false;
					}
				}else{
					//tip msg.
					self._showMsg(msg);
					return false;
				}
			});
		}

		this.deleteB = $(this.options.delete_selector);
		if(this.deleteB.length>0){
			this.deleteB.click(function(){
				var count = self.getSelectedColCount();
				var isSucess = false;
				var msg;
				if(count==0){
					msg = self.options.delete_msg.replace(new RegExp(self.options.objectNamePlaceHolder,"gm"),self.options.objectName);
				}else{
					isSucess = true;
				}

				if(isSucess){
					//confirm
					var confirmMsg = self._formatMsg(self.options.delete_confirm_msg);
					var _html = '<div style="padding: 10px 0 20px 10px;cursor: default;"><h1 style="margin:10px 5px;"><img src="../images/y-tanhao.gif" style=" vertical-align:middle; margin-right:5px;" />  {{msg}}</h1><input type="button" onclick="$.unblockUI();return false;"  value="取消" " style="width:64px; height:24px; border:0 none; background:url(../images/cx.gif);" /> <input type="button" id="BLOCKUI_YES" value="确 定" style="width:64px; height:24px; border:0 none; margin-left:20px; background:url(../images/cx.gif);" /> </div>';
					$.blockUI({	
						message: _html.replace(/{{msg}}/,confirmMsg),
						css: {backgroundColor:"#E9F4F9",borderColor:"#00446b",borderWidth:'1px',cursor:"pointer",color:"#ed0000",width:'375px'}
					});
					$("#BLOCKUI_YES").click(function(){
						$.unblockUI();
						if(self.options.deleteCallBack!=null){
							self.options.deleteCallBack(self.getSelectedCols());
							return false;
						}
					});
					return false;
					
				}else{
					//tip msg.
					self._showMsg(msg);
					return false;
				}
			});
		}

		this.cancel = $(this.options.cancel_selector);
		if(this.cancel.length>0){
			this.cancel.click(function(){
				var count = self.getSelectedColCount();
				var isSucess = false;
				var msg;
				if(count==0){
					msg = self.options.cancel_msg.replace(new RegExp(self.options.objectNamePlaceHolder,"gm"),self.options.objectName);
				}else{
					isSucess = true;
				}	

				if(isSucess){
					if(self.options.cancelCallBack!=null){
						self.options.cancelCallBack(self.getSelectedCols());
						return false;
					}
				}else{
					//tip msg.
					self._showMsg(msg);
					return false;
				}
			});
		}

		this.copy = $(this.options.copy_selector);
		if(this.copy.length>0){
			this.copy.click(function(){
				var count = self.getSelectedColCount();
				var isSucess = false;
				var msg;
				if(count==0){
					isSucess = false;
					msg = self.options.copy_msg1.replace(new RegExp(self.options.objectNamePlaceHolder,"gm"),self.options.objectName);
				}else if(count>1){
					isSucess = false;
					msg = self.options.copy_msg2.replace(new RegExp(self.options.objectNamePlaceHolder,"gm"),self.options.objectName);
				}else{
					isSucess = true;
				}

				if(isSucess){
					if(self.options.copyCallBack!=null){
						self.options.copyCallBack(self.getSelectedCols());
						return false;
					}
				}else{
					//tip msg.
					self._showMsg(msg);
					return false;
				}	
			});
		}	

		//other buttons.
		for(var i=0;i<this.options.otherButtons.length;i++){
			var button = this.options.otherButtons[i];
			var jButton = $(button.button_selector);
			if(jButton.length>0){
				var fun = (function(){
					var index = i;
					return function(){
						var button = self.options.otherButtons[index];
						var count = self.getSelectedColCount();
						var isSucess = false;
						var msg = button.msg;
						var msg2 = button.msg2;
						if(self.sucessRule[button.sucessRulr]==1){
							if(count==1){
								isSucess = true;
							}
						}else if(self.sucessRule[button.sucessRulr]==2){
							if(count>0){
								isSucess = true;
							}
						}

						if(isSucess){
							if(button.buttonCallBack!=null){
								button.buttonCallBack(self.getSelectedCols());
								return false;
							}
						}else{
							//tip msg.
							if (count > 1 && msg2) {
                                self._showMsg(msg2);
                            } else
                                self._showMsg(msg);
                            return false;
						}	
					};
				})();
				jButton.click(fun);
			}
		}
	},
	_showMsg:function(msg){
		var self = this;
		$.blockUI({	
			message: self._dialogHtml.replace(/{{msg}}/,msg),
			showOverlay:false,
			centerX:true,
			centerY:false,
			css: { top: '170px',backgroundColor:"#FEF7CB",borderColor:"#D59228",borderWidth:'1px',cursor:"pointer",color:"#ed0000"},
			timeout: 2000
		});
		
	},
	_formatMsg:function(msg){
		return msg.replace(new RegExp(this.options.objectNamePlaceHolder,"gm"),this.options.objectName);
	},
	_dialogHtml:'<h3 style="padding:20px 0">{{msg}}！</h3>',
	_initTableStyle:function(){
		//隔行,滑动,点击 变色.+ 单选框选中的行 变色:
		this.container.find("tr:even").addClass('odd');
		this.container.find("tr").hover(
			function() {$(this).addClass('highlight');},
			function() {$(this).removeClass('highlight');}
		);
		// 如果单选框默认情况下是选择的，变色.
		//$('#liststyle input[type="radio"]:checked').parents('tr').addClass('selected');
	
	},
	sucessRule:[0,1,2],/* 0index作为占位存在不起作用，1index代表只能选中一个,2index代表可以选择多个  */
	DEFAULTS:{
		tableContainerSelector:"#liststyle",
		allSelectedCheckBoxSelector:"",
		checkBoxSelector:"",
		isHaveAllSelectedFun:true,
		objectName:"列",
		objectNamePlaceHolder:"{{colName}}",
		add_selector:".toolbar_add",
	    update_selector:".toolbar_update",
	    delete_selector:".toolbar_delete",
		cancel_selector:".toolbar_cancel",
		copy_selector:".toolbar_copy",


		/* example */
		/*
			otherButton:[{
				button_selector:'',
				sucessRulr:1
				msg:'fssdsdfd',
				buttonCallBack:function(){}
			}]
			
		*/
		otherButtons:[],

		update_msg1:"未选中任何{{colName}}",
		update_msg2:"只能选择一条{{colName}} 进行修改",
		delete_msg:"未选中任何{{colName}}",
		delete_confirm_msg:"确定删除选中的{{colName}}？删除后不可恢复！",
		cancel_msg:"未选中任何{{colName}}",
		copy_msg1:"未选中任何{{colName}}",
		copy_msg2:"只能选择一条 {{colName}} 进行复制",

		updateCallBack:null,
		deleteCallBack:null,
		cancelCallBack:null,
		copyCallBack:null
	}
};
$(function(){
	$(window).resize(function(){$(".tablelist-box").height($("#liststyle").height())});
});