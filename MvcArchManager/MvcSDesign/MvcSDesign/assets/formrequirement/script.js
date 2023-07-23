
//jQuery time
var current_fs, next_fs, previous_fs; //fieldsets
var left, opacity, scale; //fieldset properties which we will animate
var animating; //flag to prevent quick multi-click glitches

$(".next").click(function(){
	//alert("abc");
	//formvalidation();
	if(formvalidation() == 1){
		return;
	}
	if(animating) return false;
	animating = true;
	
	current_fs = $(this).parent();
	next_fs = $(this).parent().next();
	$('fieldset').removeClass('active');
	next_fs.addClass('active');
	//activate next step on progressbar using the index of next_fs
	$("#progressbar li").eq($("fieldset").index(next_fs)).addClass("active");
	
	//show the next fieldset
	next_fs.show(); 
	//hide the current fieldset with style
	current_fs.animate({opacity: 0}, {
		step: function(now, mx) {
			//as the opacity of current_fs reduces to 0 - stored in "now"
			//1. scale current_fs down to 80%
			scale = 1 - (1 - now) * 0.2;
			//2. bring next_fs from the right(50%)
			left = (now * 50)+"%";
			//3. increase opacity of next_fs to 1 as it moves in
			opacity = 1 - now;
			current_fs.css({
        'transform': 'scale('+scale+')',
        'position': 'absolute'
      });
			next_fs.css({'left': left, 'opacity': opacity});
		}, 
		duration: 800, 
		complete: function(){
			current_fs.hide();
			animating = false;
		}, 
		//this comes from the custom easing plugin
		easing: 'easeInOutBack'
	});
});

$(".previous").click(function(){
	if(animating) return false;
	animating = true;
	
	current_fs = $(this).parent();
	previous_fs = $(this).parent().prev();
	$('fieldset').removeClass('active');
	previous_fs.addClass('active');
	//de-activate current step on progressbar
	$("#progressbar li").eq($("fieldset").index(current_fs)).removeClass("active");
	
	//show the previous fieldset
	previous_fs.show(); 
	//hide the current fieldset with style
	current_fs.animate({opacity: 0}, {
		step: function(now, mx) {
			//as the opacity of current_fs reduces to 0 - stored in "now"
			//1. scale previous_fs from 80% to 100%
			scale = 0.8 + (1 - now) * 0.2;
			//2. take current_fs to the right(50%) - from 0%
			left = ((1-now) * 50)+"%";
			//3. increase opacity of previous_fs to 1 as it moves in
			opacity = 1 - now;
			current_fs.css({'left': left});
			previous_fs.css({'transform': 'scale('+scale+')', 'opacity': opacity});
		}, 
		duration: 800, 
		complete: function(){
			current_fs.hide();
			animating = false;
		}, 
		//this comes from the custom easing plugin
		easing: 'easeInOutBack'
	});
});

function formvalidation() {

	var error = 0;
	
	if( $('fieldset.active #txtProjectID').length ){
		var element = $('fieldset.active').find('#txtProjectID');
		if(element.val() == '' || !$.isNumeric(element.val()) ){
			element.addClass('error-field');
			element.next('.error-msg').fadeIn();
			error = 1;
		}
	}
	if( $('fieldset.active #txtWorkingStatus_I').length ){
		var element = $('fieldset.active').find('#txtWorkingStatus_I');
		if(element.val() == ''){
			element.addClass('error-field');
			element.next('.error-msg').fadeIn();
			error = 1;
		}
	}
	if( $('fieldset.active #txtSlabHeight_I').length ){
		var element = $('fieldset.active').find('#txtSlabHeight_I');
		if(element.val() == '' ){
			element.addClass('error-field');
			element.next('.error-msg').fadeIn();
			error = 1;
		}
	}
	if( $('fieldset.active #txtPlinthHeight_I').length ){
		var element = $('fieldset.active').find('#txtPlinthHeight_I');
		if(element.val() == '' ){
			element.addClass('error-field');
			element.next('.error-msg').fadeIn();
			error = 1;
		}
	}
	if( $('fieldset.active #txtPorchHeight_I').length ){
		var element = $('fieldset.active').find('#txtPorchHeight_I');
		if(element.val() == '' ){
			element.addClass('error-field');
			element.next('.error-msg').fadeIn();
			error = 1;
		}
	}
	if( $('fieldset.active #ddlElevationPattern').length ){
		var element = $('fieldset.active').find('#ddlElevationPattern');
		if(element.val() == ''){
			element.addClass('error-field');
			element.next('.error-msg').fadeIn();
			error = 1;
		}
	}
	if( $('fieldset.active #txtDoorLintel_I').length ){
		var element = $('fieldset.active').find('#txtDoorLintel_I');
		if(element.val() == '' ){
			element.addClass('error-field');
			element.next('.error-msg').fadeIn();
			error = 1;
		}
	}
	if( $('fieldset.active #txtWindowSill_I').length ){
		var element = $('fieldset.active').find('#txtWindowSill_I');
		if(element.val() == '' ){
			element.addClass('error-field');
			element.next('.error-msg').fadeIn();
			error = 1;
		}
	}
	if( $('fieldset.active #txtWindowLintel_I').length ){
		var element = $('fieldset.active').find('#txtWindowLintel_I');
		if(element.val() == '' ){
			element.addClass('error-field');
			element.next('.error-msg').fadeIn();
			error = 1;
		}
	}
	if( $('fieldset.active #txtPlotFaceSide_I').length ){
		var element = $('fieldset.active').find('#txtPlotFaceSide_I');
		if(element.val() == ''){
			element.addClass('error-field');
			element.next('.error-msg').fadeIn();
			error = 1;
		}
	}

	return error;
}

$(document).on('click', 'input, select, textarea', function() {
	$(this).removeClass('error-field');
	$(this).next('.error-msg').fadeOut();
});

$("#form1").submit(function(e) {
    //prevent Default functionality
    formvalidation();
	if(formvalidation() == 1){
		return false;
	}else{
		return true;
	}

});