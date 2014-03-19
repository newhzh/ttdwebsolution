;(function(jQuery) {
    jQuery.fn.dkSearch = function(options){
		var defaults = {
			searchbox : 'searchitems',		
			itemBox : 'selectDD',
			inputClass : 'search_input',
			dwClass : 'sidw',		
			btnClass : 'search_btn',
                        btnClass_1: 'search_btn_bg1',
                        btnClass_2: 'search_btn_bg2',
			ulClass : 'search_ul',
			lioverClass : 'liover',
			lioverinClass : 'lioverin',
			TypesClass : 'typesdl',
			FirstClass : 'firstdl',
			MoneyClass : 'moneydl',
			TimeClass : 'timedl',
			tbClass : 'typesearchBox',
			snClass : 'searchNotice',
			noticeMsg : 'inputnotice',
			submitClass : 'search_submit',
			items : new Array('index'),
			diyitems : new Array(),
                        target: '_self',
			errorMsg : {
				'numDot' : '不足一万可用小数表示',
				'onlynum' : '请输入整数',
				'onlynumDot' : '请输入数字',
				'notempty' : '不允许为空',
				'moneyReg' : function(val){
					val = parseFloat(val);
					if(val < 0.1 || val > 3000){
						return '请输入 0.1 至 3000 的数';
					}
					return '';
				},
				'timeReg' : function(val){
					val = parseInt(val);
					if(val < 1 || val > 360){
						return '请输入 1 至 360 的整数';
					}
					return '';
				}
			},

		}
		var o = jQuery.extend(true,defaults,options);
		var box = jQuery(this);
		jQuery(document).ready(function() {
			$searchbox = '.'+o['searchbox'];
			$itembox = '.'+o['itemBox'];
			$ulClass = 'ul.'+o['ulClass'];
			$btnClass = '.'+o['btnClass'];
			$inputClass = '.'+o['inputClass'];
			$dwClass = '.'+o['dwClass'];
			$ntitem = '.'+o['noticeMsg'];
			$snClass = '.'+o['snClass'];
			$submitClass = '.'+o['submitClass'];
			$tbClass = '.'+o['tbClass'];
			
			init();
		});
		var readData = function(){
			if(o['diyitems'].length){
				var $delkey = new Array();
				var $delitems = jQuery.grep(o['items'], function(n,i){
									isdel = jQuery.inArray(n,o['diyitems']);
									if(isdel < 0){
										$delkey.push(i);
										return isdel;
									}
								});
				for(var key in $delkey){
					o['data']['index']['Types']['txt'].splice($delkey[key]-key,1);
					o['data']['index']['Types']['val'].splice($delkey[key]-key,1);
				}
				for(var key in $delitems){
					box.find('.type_'+$delitems[key]).remove();
				}
				box.find('.type_'+o['diyitems'][0]).show();
				box.find('.type_'+o['diyitems'][0]).focus();
				o['items'] = o['diyitems'];
			}
			$items = o['items'];
			for (var key in $items){
				$itemname = $items[key];
				if($itemname == 'index'){ setTypesData();}
				if($itemname == 'gouche'){ setData('First',$itemname);}
				setData('Money',$itemname);
				setData('Term',$itemname);
			}
		};
		var setTypesData = function(){
			$dataClass = '.'+o['TypesClass'];
			$data = o['data']['index']['Types']['txt'];
			$dataval = o['data']['index']['Types']['val'];
			$datadl = '';
				$iobj = $dataClass+' '+$itembox;
				$dataDef = o['data']['index']['Types']['def'];
				var a = new Array($dataDef['txt'],$dataDef['val'],'');
				setVal(box.find($iobj+' '+$inputClass),box.find($iobj+' '+$dwClass),a);
				box.find($iobj+' '+$inputClass).attr('readonly','readonly');
			for(k in $data){
				$datadl = $datadl + '<li data-txt="'+$data[k]+'" data-val="'+$dataval[k]+'" data-dw="">'+$data[k]+'</li>';
			}
			box.find($iobj).append('<ul class="'+o['ulClass']+'" style="display:none;"></ul>');
			box.find($iobj+' '+$ulClass).append($datadl);
			box.find($iobj+' '+$ulClass+' li').live('click',function(e){
				var that = jQuery(this);
				var itemval = that.attr('data-val');
				box.find($tbClass).hide();
				box.find('.type_'+itemval).show();
                                box.find('.type_'+itemval+' '+$inputClass).eq(0).focus();
			})
		};
		var setData = function(datatype,itemname){
			if(typeof(o['data'][itemname][datatype])!= 'undefined'){
				$dataClass = '.'+o[datatype+'Class'];
				$data = o['data'][itemname][datatype]['val'];
				$datadw = o['data'][itemname][datatype]['dw'];
				$dataDef = o['data'][itemname][datatype]['def'];
				$iobj = '.type_'+$itemname+' '+$dataClass+' '+$itembox;
				$input = box.find($iobj+' '+$inputClass);
				$isdef = typeof($input.attr('data-val')) == 'undefined' || $input.attr('data-val') == '' || typeof($input.attr('data-dw')) == 'undefined' || $input.attr('data-dw') == '';
				if(datatype == 'Money' || datatype == 'Term'){
					if($isdef){
						var a = new Array($dataDef['val'],$dataDef['val'],$dataDef['dw']);
					}else{
						var a = new Array($input.attr('data-val'),$input.attr('data-val'),$input.attr('data-dw'));
					}
					if(datatype == 'Money'){ 
						$input.attr('data-type','numDot');
						box.find('.type_'+$itemname+' '+$dataClass+' '+$snClass).html(o['errorMsg']['numDot']);
					}
					else{ $input.attr('data-type','num');}
				}else if(datatype == 'First'){
					if($isdef){
						var a = new Array(o['data'][itemname][datatype].txt($dataDef['val']),$dataDef['val'],$dataDef['dw']);
					}else{
						var a = new Array(o['data'][itemname][datatype].txt($input.attr('data-val')),$input.attr('data-val'),$input.attr('data-dw'));
					}
					$input.attr('readonly','readonly');
				}
				setVal($input,box.find($iobj+' '+$dwClass),a);
				$datadl = '';
				for(k in $data){
                                        if(datatype == 'First'){
                                            var nowtxt = getItemTxt(itemname,datatype,k);
                                        }else{
                                            var nowtxt = $data[k];
                                        }
					$datadl = $datadl + '<li data-txt="'+nowtxt+'" data-val="'+$data[k]+'" data-dw="'+getItemDw(itemname,datatype,k)+'">'+getItemTxt(itemname,datatype,k)+'</li>';
				}
				box.find($iobj).append('<ul class="'+o['ulClass']+'" style="display:none;"></ul>');
				box.find($iobj+' '+$ulClass).append($datadl);
			}
		};
		var init = function(e){
			box.find($itembox).live({
				click : function(e){
					e.stopPropagation();
  					e = e.target || e.srcElement;
					if(e == this || $(e).hasClass('sidw')){
						$menu = getMenu($(this),1);
						if(isHide($menu)){
							showMenu($menu);
						}
					}
				}
			});
			box.find($itembox+' '+$inputClass).live({
				click : function(e){
					e.stopPropagation();
					$menu = getMenu($(this),3);
					if(isHide($menu)){
						showMenu($menu);
					}
				},
				keypress : function(e){
					$menu = getMenu($(this),3);
					if(!isHide($menu)){
						hideMenu($menu);
					}
				},
				keyup : function(e){
					setNT(jQuery(this));
				},
				focus : function(){
					 setCharetAtEnd(this);
					 setNT(jQuery(this));
				},
				paste : function(){
					return false;
				},
				dragover : function(){
					return false;
				}
			});
			box.find($itembox+' '+$ulClass+' li').live({
				mouseover : function(){
					$menu = getMenu($(this),4);
					$(this).siblings('li').removeClass(o['lioverClass']);
					$(this).addClass(o['lioverClass']);
				},
				click : function(e){
					e.stopPropagation();
					$menu = getMenu($(this),4);
					var that = jQuery(this);
					var $input = $menu.siblings($inputClass);
					var $dw = $menu.siblings($dwClass);
					var a = new Array(that.attr('data-txt'),that.attr('data-val'),that.attr('data-dw'));
					setVal($input,$dw,a);
					if(!isHide($menu)){
						hideMenu($menu);
					}
					if($input.attr('data-type') == 'num' || $input.attr('data-type') == 'numDot'){
						$input.focus();
					}
				}
			});
			box.find($itembox+' '+$btnClass).live(
				'click' , function(e){
					e.stopPropagation();
					$menu = getMenu($(this),2);
					if(isHide($menu)){
						showMenu($menu);
					}else{
						hideMenu($menu);
					}
				}
			);
			box.find($tbClass).live({
				submit : function(e){
					e.preventDefault();
					box.find($ulClass).hide();
                                        //$form = jQuery(this).parents($tbClass);
                                        $form = jQuery(this);
					$items = $form.find($inputClass);
					var error = 0;
					$items.each(function(){
						if(setNT(jQuery(this)) == false){
							error++;
						}
					})
					if(error > 0){
						return false;
					}else{
						$type = $form.attr('data-type');
						$moneyItem = $form.find("input[name=money]");
						$timeItem = $form.find("input[name=term]");
						$moneyNum = $moneyItem.attr('data-val');
						$moneyDw = $moneyItem.attr('data-dw');
						$timeNum = $timeItem.attr('data-val');
						$timeDw = $timeItem.attr('data-dw');
                                                var bs = 1;
                                                if($timeDw == '年'){
                                                    bs = 12;
                                                }
                                                var $seaUrl = '/'+$type+'-'+$moneyNum+'x'+parseInt($timeNum)*bs;
						if($type == 's2'){
                                                    $firstNum = $form.find("input[name=first]").attr('data-val');
                                                    if($firstNum == undefined){
                                                        $firstNum = 3;
                                                    }
                                                    $seaUrl = $seaUrl+'x'+$firstNum;
                                                }
                                                $seaUrl = $seaUrl+'-0x0x9999/';
                                                if(o['target'] == '_self'){
                                                    window.location.href = $seaUrl;
                                                }else{
                                                    window.open($seaUrl,o['target']);
                                                }
					}
				}
			})
			jQuery('body').live('click', function(){
                            hideMenu();
			});
			var setNT = function(that){
				var value = that.val();
				$dt = that.attr('data-type');
				$nt = that.parents($searchbox).find($ntitem);
				$sn = that.parents($searchbox).find($snClass);
				var msg = '';
				if(value == ''){
					msg = o['errorMsg']['notempty'];
				}else{
					if($dt == 'numDot'){
                                                that.val(that.val().replace(/[^\d.]/g, ""));
                                                that.val(that.val().replace(/^\./g, ""));
                                                that.val(that.val().replace(/\.{2,}/g, "."));
                                                that.val(that.val().replace(".", "$#$").replace(/\./g, "").replace("$#$", "."));
						if(isDecimal(that.val()) == false){
							msg = o['errorMsg']['onlynumDot'];
						}else{
							msg = o['errorMsg'].moneyReg(that.val());
						}
					}else if($dt == 'num'){
                                                that.val(that.val().replace(/[^\d]/g, ""));
						if(isInteger(that.val()) == false){
							msg = o['errorMsg']['onlynum'];
						}else{
							msg = o['errorMsg'].timeReg(that.val());
						}
					}
				}
				$nt.html(msg);
				if(msg == '') {
					$nt.hide();
					$sn.show();
					if(that.attr("readonly") != 'readonly'){
                                            that.attr('data-val',that.val());
                                        }
					return true;
				}else{
					$sn.hide();
					$nt.show();
					return false;
				}
			};
			var getMenu = function(obj,t){
				if(t == 1){
					return obj.children($ulClass);
				}else if(t == 2){
					return obj.nextAll($ulClass);
				}else if(t == 3){
					return obj.nextAll($ulClass);
				}else if(t == 4){
					return obj.parent($ulClass);
				}
			}
			var getDw = function(){
				return obj.children('.sidw');
			}
			var setCharetAtEnd = function(field) { 
			   if (field.createTextRange) {
				   var r = field.createTextRange(); 
				   r.moveStart('character', field.value.length); 
				   r.collapse(); 
				   r.select(); 
			   } 
			}
			var isInteger = function(str){
				var regu = /^[0-9]{1,}$/;
				return regu.test(str);
			};
			var isDecimal = function(str){
				if(isInteger(str)){
					return true;
				}
			    var re = /^([0-9.]+)+(\d+)$/;
			    if (re.test(str)) {
			        if (RegExp.$1 == 0 && RegExp.$2 == 0) {
			            return false;
			        }
			        return true;
			    }
			    else {
			        return false;
			    }
			}; 
		}
		var setVal = function(i,d,a){
			i.val(a[0]);
			i.attr('data-val',a[1]);
			i.attr('data-dw',a[2]);
			d.html(a[2]);
		}
		var showMenu = function(obj){
                        hideMenu();
                        var btnObj = obj.parent($itembox).find($btnClass);
                        btnObj.removeClass(o['btnClass_2']);
                        btnObj.addClass(o['btnClass_1']);
			obj.show();
		}
		var hideMenu = function(obj){
			box.find($ulClass).hide();
                        box.find($btnClass).removeClass(o['btnClass_1']);
                        box.find($btnClass).addClass(o['btnClass_2']);
		}
		var isHide = function(obj){
			return obj.css('display') == 'none' ? true : false;
		}
		var getItemTxt = function(t,i,num){
			var obj = o['data'][t][i];
			var val = obj['val'][num];
			return typeof(obj['dw']) == 'undefined'? obj.txt(val) : val + (typeof(obj['dw']) == 'string'?obj['dw']:obj['dw'][num]);
		};
		var getItemVal = function(t,i,num){
			return o['data'][t][i]['val'][num];
		};
		var getItemDw = function(t,i,num){
			var obj = o['data'][t][i];
			return typeof(obj['dw']) == 'undefined'? '' : (typeof(obj['dw']) == 'string'?obj['dw']:obj['dw'][num]);
		}
	}
})(jQuery);