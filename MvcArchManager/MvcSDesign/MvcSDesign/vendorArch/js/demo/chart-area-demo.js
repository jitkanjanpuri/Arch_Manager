// Set new default font family and font color to mimic Bootstrap's default styling
Chart.defaults.global.defaultFontFamily = 'Nunito', '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
Chart.defaults.global.defaultFontColor = '#858796';

function number_format(number, decimals, dec_point, thousands_sep) {
  // *     example: number_format(1234.56, 2, ',', ' ');
  // *     return: '1 234,56'
  number = (number + '').replace(',', '').replace(' ', '');
  var n = !isFinite(+number) ? 0 : +number,
    prec = !isFinite(+decimals) ? 0 : Math.abs(decimals),
    sep = (typeof thousands_sep === 'undefined') ? ',' : thousands_sep,
    dec = (typeof dec_point === 'undefined') ? '.' : dec_point,
    s = '',
    toFixedFix = function(n, prec) {
      var k = Math.pow(10, prec);
      return '' + Math.round(n * k) / k;
    };
  // Fix for IE parseFloat(0.55).toFixed(0) = 0;
  s = (prec ? toFixedFix(n, prec) : '' + Math.round(n)).split('.');
  if (s[0].length > 3) {
    s[0] = s[0].replace(/\B(?=(?:\d{3})+(?!\d))/g, sep);
  }
  if ((s[1] || '').length < prec) {
    s[1] = s[1] || '';
    s[1] += new Array(prec - s[1].length + 1).join('0');
  }
  return s.join(dec);
}

// Area Chart Example
var ctx_area_1 = document.getElementById("myAreaChart");
if(ctx_area_1){
var myLineChart = new Chart(ctx_area_1, {
  type: 'line',
  data: {
    labels: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
    datasets: [{
      label: "Earnings",
      lineTension: 0.3,
      backgroundColor: "rgba(78, 115, 223, 0.05)",
      borderColor: "rgba(78, 115, 223, 1)",
      pointRadius: 3,
      pointBackgroundColor: "rgba(78, 115, 223, 1)",
      pointBorderColor: "rgba(78, 115, 223, 1)",
      pointHoverRadius: 3,
      pointHoverBackgroundColor: "rgba(78, 115, 223, 1)",
      pointHoverBorderColor: "rgba(78, 115, 223, 1)",
      pointHitRadius: 10,
      pointBorderWidth: 2,
      data: [0, 10000, 5000, 15000, 10000, 20000, 15000, 25000, 20000, 30000, 25000, 40000],
    }],
  },
  options: {
    maintainAspectRatio: false,
    layout: {
      padding: {
        left: 10,
        right: 25,
        top: 25,
        bottom: 0
      }
    },
    scales: {
      xAxes: [{
        time: {
          unit: 'date'
        },
        gridLines: {
          display: false,
          drawBorder: false
        },
        ticks: {
          maxTicksLimit: 7
        }
      }],
      yAxes: [{
        ticks: {
          maxTicksLimit: 5,
          padding: 10,
          // Include a dollar sign in the ticks
          callback: function(value, index, values) {
            return '$' + number_format(value);
          }
        },
        gridLines: {
          color: "rgb(234, 236, 244)",
          zeroLineColor: "rgb(234, 236, 244)",
          drawBorder: false,
          borderDash: [2],
          zeroLineBorderDash: [2]
        }
      }],
    },
    legend: {
      display: false
    },
    tooltips: {
      backgroundColor: "rgb(255,255,255)",
      bodyFontColor: "#858796",
      titleMarginBottom: 10,
      titleFontColor: '#6e707e',
      titleFontSize: 14,
      borderColor: '#dddfeb',
      borderWidth: 1,
      xPadding: 15,
      yPadding: 15,
      displayColors: false,
      intersect: false,
      mode: 'index',
      caretPadding: 10,
      callbacks: {
        label: function(tooltipItem, chart) {
          var datasetLabel = chart.datasets[tooltipItem.datasetIndex].label || '';
          return datasetLabel + ': $' + number_format(tooltipItem.yLabel);
        }
      }
    }
  }
});
}
//WidgetChart 1
var ctx_area_2 = document.getElementById("widgetChart1");
if (ctx_area_2) {
  ctx_area_2.height = 130;
  var widgetChart1 = new Chart(ctx_area_2, {
    type: 'line',
    data: {
      labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July'],
      type: 'line',
      datasets: [{
        data: [78, 81, 80, 45, 34, 12, 40],
        label: 'Dataset',
        backgroundColor: 'rgba(255,255,255,.1)',
        borderColor: 'rgba(255,255,255,.55)',
      },]
    },
    options: {
      maintainAspectRatio: true,
      legend: {
        display: false
      },
      layout: {
        padding: {
          left: 0,
          right: 0,
          top: 0,
          bottom: 0
        }
      },
      responsive: true,
      scales: {
        xAxes: [{
          gridLines: {
            color: 'transparent',
            zeroLineColor: 'transparent'
          },
          ticks: {
            fontSize: 2,
            fontColor: 'transparent'
          }
        }],
        yAxes: [{
          display: false,
          ticks: {
            display: false,
          }
        }]
      },
      title: {
        display: false,
      },
      elements: {
        line: {
          borderWidth: 0
        },
        point: {
          radius: 0,
          hitRadius: 10,
          hoverRadius: 4
        }
      }
    }
  });
}


//WidgetChart 2
var ctx_area_3 = document.getElementById("widgetChart2");
if (ctx_area_3) {
  ctx_area_3.height = 130;
  var widgetChart2 = new Chart(ctx_area_3, {
    type: 'line',
    data: {
      labels: ['January', 'February', 'March', 'April', 'May', 'June'],
      type: 'line',
      datasets: [{
        data: [1, 18, 9, 17, 34, 22],
        label: 'Dataset',
        backgroundColor: 'transparent',
        borderColor: 'rgba(255,255,255,.55)',
      },]
    },
    options: {

      maintainAspectRatio: false,
      legend: {
        display: false
      },
      responsive: true,
      tooltips: {
        mode: 'index',
        titleFontSize: 12,
        titleFontColor: '#000',
        bodyFontColor: '#000',
        backgroundColor: '#fff',
        titleFontFamily: 'Nunito',
        bodyFontFamily: 'Nunito',
        cornerRadius: 3,
        intersect: false,
      },
      scales: {
        xAxes: [{
          gridLines: {
            color: 'transparent',
            zeroLineColor: 'transparent'
          },
          ticks: {
            fontSize: 2,
            fontColor: 'transparent'
          }
        }],
        yAxes: [{
          display: false,
          ticks: {
            display: false,
          }
        }]
      },
      title: {
        display: false,
      },
      elements: {
        line: {
          tension: 0.00001,
          borderWidth: 1
        },
        point: {
          radius: 4,
          hitRadius: 10,
          hoverRadius: 4
        }
      }
    }
  });
}


//WidgetChart 3
var ctx_area_4 = document.getElementById("widgetChart3");
if (ctx_area_4) {
  ctx_area_4.height = 130;
  var widgetChart3 = new Chart(ctx_area_4, {
    type: 'line',
    data: {
      labels: ['January', 'February', 'March', 'April', 'May', 'June'],
      type: 'line',
      datasets: [{
        data: [65, 59, 84, 84, 51, 55],
        label: 'Dataset',
        backgroundColor: 'transparent',
        borderColor: 'rgba(255,255,255,.55)',
      },]
    },
    options: {

      maintainAspectRatio: false,
      legend: {
        display: false
      },
      responsive: true,
      tooltips: {
        mode: 'index',
        titleFontSize: 12,
        titleFontColor: '#000',
        bodyFontColor: '#000',
        backgroundColor: '#fff',
        titleFontFamily: 'Nunito',
        bodyFontFamily: 'Nunito',
        cornerRadius: 3,
        intersect: false,
      },
      scales: {
        xAxes: [{
          gridLines: {
            color: 'transparent',
            zeroLineColor: 'transparent'
          },
          ticks: {
            fontSize: 2,
            fontColor: 'transparent'
          }
        }],
        yAxes: [{
          display: false,
          ticks: {
            display: false,
          }
        }]
      },
      title: {
        display: false,
      },
      elements: {
        line: {
          borderWidth: 1
        },
        point: {
          radius: 4,
          hitRadius: 10,
          hoverRadius: 4
        }
      }
    }
  });
}


//WidgetChart 4
var ctx_area_5 = document.getElementById("widgetChart4");
if (ctx_area_5) {
  ctx_area_5.height = 115;
  var widgetChart4 = new Chart(ctx_area_5, {
    type: 'bar',
    data: {
      labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
      datasets: [
        {
          label: "My First dataset",
          data: [78, 81, 80, 65, 58, 75, 60, 75, 65, 60, 60, 75],
          borderColor: "transparent",
          borderWidth: "0",
          backgroundColor: "rgba(255,255,255,.3)"
        }
      ]
    },
    options: {
      maintainAspectRatio: true,
      legend: {
        display: false
      },
      scales: {
        xAxes: [{
          display: false,
          categoryPercentage: 1,
          barPercentage: 0.65
        }],
        yAxes: [{
          display: false
        }]
      }
    }
  });
}



