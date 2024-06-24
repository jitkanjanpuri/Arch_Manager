
(function ($) {
  // USE STRICT
  "use strict";  

//carousel
$('.owl-carousel').owlCarousel({
    loop:true,
    margin:10,
    nav:false,
    dots:true,
    autoplay:true,
    autoplayTimeout:2500,
    autoplayHoverPause:true,
    autoHeight: false,
    items:1,
});
$('#MainHomeSlider').carousel({
  interval: 10000
});

//datatable
$(document).on('click', 'table tbody tr', function(e){

  if ($(e.target).parents('.table-action-btn').length || $(e.target).hasClass('table-action-btn')) {
    return false; 
  }

  if($(this).find('input[type="checkbox"]').prop("checked")){
    $(this).removeClass('table-bg-onhover');
    //$(this).find('input[type="checkbox"]').prop("checked", false);
  }else{
    if($(this).hasClass('table-bg-onhover')){
      $(this).removeClass('table-bg-onhover');
    }else{
      $(this).addClass('table-bg-onhover');
    }
    //$(this).find('td > input[type="checkbox"]').prop("checked", true);
  }
});

//all in one dropdown
  $(document).on('click', '.all-in-dropdown-btn', function () {
     
  var DataEntryID = $(this).attr('entry-id');
  var x = $(this).offset();
    var rt = ($(window).width() - (x.left + $(this).outerWidth()));
  
    var topH = (Math.round(x.top * 10) / 10) - 390;
  if($('.all-in-dropdown-content').css('display') == 'block' )
  {
      $('.all-in-dropdown-content').hide();
  }else{
    $('.all-in-dropdown-content').fadeIn();
    $('.all-in-dropdown-content').css({"top": + topH + 50 + "px", "right":+ rt +"px"});
    $('.all-in-dropdown-content input#DataEntryID').val(DataEntryID);
  }
});

$(document).on('click', '.all-in-dropdown-btn-close',function(){
  $('.all-in-dropdown-content').fadeOut();
});

$(document).keyup(function(e) {
    if (e.keyCode == 27) { // Esc
      $('.all-in-dropdown-content').fadeOut();
    }
});

$('.multi-step-form-btn').click(function () {
  $('.multi-step-form-btn').removeClass('active');
  $('.multi-progress-bar li').nextAll().removeClass('active');
  $('.multi-progress-bar li').nextAll().slice(0, $(this).attr('step-id')-1).addClass('active');
});

    $(document).on('click', '.open-confirmation-from, .close-confirmation-popup', function () {
      
  $('.confirmation-popup').fadeToggle();
  $('.confirmation-popup .confirmed-action-cp').attr('form-id',$(this).attr('id'));
});

$(document).on('click', '.confirmed-action-cp', function(){
  $('.confirmation-popup').fadeToggle();
  $('#' + $(this).attr('form-id')).parents('form').submit();
});

  $(document).ready(function () {
   //   $('.js-example-basic-multiple').select2();
      //var s2 = $("#advisor_article_tagsx").select2({
      //    placeholder: "Choose event type",
      //    tags: true
      //});

      //var vals = ["Bihar", "Goa"];

      //vals.forEach(function (e) {
      //    if (!s2.find('option:contains(' + e + ')').length)
      //        s2.append($('<option>').text(e));
      //});

      // s2.val(vals).trigger("change"); 
        function formatState(state) {
            //if (!state.id) {
            //    alert(state.text);
            //    return state.text;
            //}
            var baseUrl = "img/flags";
            //var $state = $(
            //    '<span><img src="' + baseUrl + '/' + state.element.value.toLowerCase() + '.png" class="img-flag" /> ' + state.text + '</span>'
            //);

            return $state;
        };

        //$(".js-example-country").select2({
        //    templateResult: formatState
        //});
    });

    //$(document).ready(function () {
    //    $('.dataTable').DataTable();
    //});

})(jQuery);

