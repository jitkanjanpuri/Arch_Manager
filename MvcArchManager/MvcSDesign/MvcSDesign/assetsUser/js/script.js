//$(document).ready( function () {
//    $('#dataTable').DataTable();
//} );

//
/* Examples progress circle*/
//(function($) {
//   $('.average-per-day-circle').circleProgress({
//    value: 0.81,
//    fill: {gradient: ['#5ce497', '#469EFB']},
//    size:260,
//  }).on('circle-animation-progress', function(event, progress) {
//    $(this).find('strong').html(Math.round(81 * progress) );
//  });
  
//})(jQuery);

//
/* Examples progress bar*/
//if(jQuery("#chart-combined").length > 0) {
//  var ctx = document.getElementById('chart-combined');
//  if (ctx) {
//    var mq = window.matchMedia( "(max-width: 570px)" );
//    if (mq.matches) {
//      ctx.height = 600;
//    }
//    else {
//      ctx.height = 160;
//    }

//    var chart = new Chart(ctx, {

//      type: 'bar',
//      data: {
//          datasets: [{
//              label: 'Website Blog',
//              data: [440, 505, 414, 671, 227, 413, 201, 352, 752, 320, 257, 160],
//              backgroundColor: '#DC828F',
//              order: 2,
//          }, {
//              label: 'Social Media',
//              borderColor: '#F7CE76',
//              backgroundColor: 'transparent',
//              data: [123, 142, 535, 27, 543, 122, 17, 131, 522, 22, 512, 116],
//              type: 'line',
//              // this dataset is drawn on top
//              order: 1
//          }],
//          labels: ["01 Jan 2020 ", "02 Jan 2020  ", "03 Jan 2020 ", "04 Jan 2020  ", "05 Jan 2020 ", "06 Jan 2020  ", "07 Jan 2020 ", "08 Jan 2020  ", "09 Jan 2020 ", "10 Jan 2020  ", "11 Jan 2020 ", "12 Jan 2020  "],
//      },

//      options: {
//        responsive: true,
//      }
//    });
//  }
//}
//if(jQuery("#stacked-bars-chart").length > 0) {
//  var ctx2 = document.getElementById('stacked-bars-chart').getContext('2d');
//  if (ctx2) {
//    var mq = window.matchMedia( "(max-width: 570px)" );
//    if (mq.matches) {
//      ctx2.height = 400;
//    }
//    else {
//      ctx2.height = 160;
//    }
//    var barChartData = new Chart(ctx2,{
//        type: 'horizontalBar',
//        data:{
//        labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July'],
//        datasets: [{
//          label: 'Dataset 1',
//          backgroundColor: "#DC828F",
//          data: [ 123, 142, 535, 27, 543, 838, 299]
//        }, {
//          label: 'Dataset 2',
//          backgroundColor: "#F7CE76",
//          data: [122, 17, 131, 522, 22, 512, 116]
//        }, {
//          label: 'Dataset 3',
//          backgroundColor: "#8C7386",
//          data: [440, 201, 352, 752, 320, 257, 160],
//        }]
//      },
//      options: {
//        responsive: true,
//        scales: {
//          xAxes: [{
//            stacked: true,
//          }],
//          yAxes: [{
//            stacked: true
//          }]
//        }
//      }
//    });
//  }
//}
    