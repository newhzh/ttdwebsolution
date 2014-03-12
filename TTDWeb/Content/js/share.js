/* 
 * catherine
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
 function killErrors() {
        return true;
    }
    window.onerror = killErrors;
    
document.onclick = function(e) {	
      var popup = document.getElementById('pop');
      var ele = e ? e.target : window.event.srcElement;		
      if((ele.id !== 'pop')&&(ele.id !== 'footer_calc')) {			
	    popup.style.display= 'none';
	  }	
  }	
function show(){ 
var popup = document.getElementById('pop'); 
popup.style.display = ""; 
} 
function hide(){ 
var popup = document.getElementById('pop'); 
popup.style.display = "none"; 
} 

function changeCity(){
    var city_box = $('#city_box');
    city_box.css("display") == 'none' ? city_box.show() :  city_box.hide();
    $('body').on("click",function(e){       //点击空白区域关闭
        var target  = $(e.target);
        if(target.closest("#city_box").length == 0 && target.closest("#changeCityBtn").length == 0){
            $("#city_box").hide();
        }
    });
}
