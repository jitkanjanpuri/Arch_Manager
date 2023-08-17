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

//Technical Report
var ctx_bar_1 = document.getElementById("TechnicalReport");
if (ctx_bar_1) {
  var mq = window.matchMedia( "(max-width: 570px)" );
  if (mq.matches) {
    ctx_bar_1.height = 600;
  }
  else {
    ctx_bar_1.height = 100;
  }
  var myChart = new Chart(ctx_bar_1, {
    type: 'bar',
    defaultFontFamily: 'Nunito',
    data: {
      labels: ["Nazeem Khan", "Mohd Nadeem Shaikh", "Nalini Nalge", "Puneet Jain", "Rahul Sen", "Suman Pal", "Veer Kushwah", "Yusuf Khan"],
      datasets: [
        {
          label: "Elevation",
          data: [65, 59, 50, 81, 56, 55, 40, 20],
          borderColor: "rgba(62, 250, 205, 0.9)",
          borderWidth: "0",
          backgroundColor: "rgba(62, 250, 205, 0.7)",
          fontFamily: "Nunito"
        },
        {
          label: "Revised Elevation",
          data: [28, 48, 40, 19, 86, 27, 50, 10],
          borderColor: "rgba(130,58,129,0.9)",
          borderWidth: "0",
          backgroundColor: "rgba(130,58,129,0.7)",
          fontFamily: "Nunito"
        },
        {
          label: "3D Model",
          data: [65, 79, 80, 81, 56, 55, 40, 70],
          borderColor: "rgba(106, 233, 25, 0.9)",
          borderWidth: "0",
          backgroundColor: "rgb(106, 233, 25, 0.7)",
          fontFamily: "Nunito"
        },
        {
          label: "Draft View",
          data: [28, 48, 40, 19, 66, 27, 90, 70],
          borderColor: "rgba(173,216,230,0.9)",
          borderWidth: "0",
          backgroundColor: "rgba(173,216,230,0.7)",
          fontFamily: "Nunito"
        },
        {
          label: "Revised Draft",
          data: [65, 59, 60, 81, 56, 35, 40, 80],
          borderColor: "rgba(153, 166, 41, 0.9)",
          borderWidth: "0",
          backgroundColor: "rgb(153, 166, 41, 0.7)",
          fontFamily: "Nunito"
        },
        {
          label: "Final View",
          data: [28, 48, 40, 19, 36, 27, 90, 70],
          borderColor: "rgba(45,90,217,0.9)",
          borderWidth: "0",
          backgroundColor: "rgba(45,90,217,0.7)",
          fontFamily: "Nunito"
        }
      ]
    },
    options: {
      legend: {
        position: 'top',
        labels: {
          fontFamily: 'Nunito'
        }

      },
      scales: {
        xAxes: [{
          ticks: {
            fontFamily: "Nunito"

          }
        }],
        yAxes: [{
          ticks: {
            beginAtZero: true,
            fontFamily: "Nunito"
          }
        }]
      }
    }
  });
}

// Bar Chart Example
var ctx_bar_2 = document.getElementById("myBarChart");
if (ctx_bar_2) {
var myBarChart = new Chart(ctx_bar_2, {
  type: 'bar',
  data: {
    labels: ["January", "February", "March", "April", "May", "June"],
    datasets: [{
      label: "Revenue",
      backgroundColor: "#4e73df",
      hoverBackgroundColor: "#2e59d9",
      borderColor: "#4e73df",
      data: [4215, 5312, 6251, 7841, 9821, 14984],
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
          unit: 'month'
        },
        gridLines: {
          display: false,
          drawBorder: false
        },
        ticks: {
          maxTicksLimit: 6
        },
        maxBarThickness: 25,
      }],
      yAxes: [{
        ticks: {
          min: 0,
          max: 15000,
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
      titleMarginBottom: 10,
      titleFontColor: '#6e707e',
      titleFontSize: 14,
      backgroundColor: "rgb(255,255,255)",
      bodyFontColor: "#858796",
      borderColor: '#dddfeb',
      borderWidth: 1,
      xPadding: 15,
      yPadding: 15,
      displayColors: false,
      caretPadding: 10,
      callbacks: {
        label: function(tooltipItem, chart) {
          var datasetLabel = chart.datasets[tooltipItem.datasetIndex].label || '';
          return datasetLabel + ': $' + number_format(tooltipItem.yLabel);
        }
      }
    },
  }
});
}


//bar chart Comapre
var ctx_bar_3 = document.getElementById("barChart");
if (ctx_bar_3) {
var mq = window.matchMedia( "(max-width: 570px)" );
if (mq.matches) {
  ctx_bar_3.height = 200;
}
else {
  ctx_bar_3.height = 100;
}
  var myChart = new Chart(ctx_bar_3, {
    type: 'bar',
    defaultFontFamily: 'Nunito',
    data: {
      labels: ["January", "February", "March", "April", "May", "June", "July"],
      datasets: [
        {
          label: "TARGET",
          data: [65, 59, 80, 81, 56, 55, 40],
          borderColor: "rgba(0, 123, 255, 0.9)",
          borderWidth: "0",
          backgroundColor: "rgba(0, 123, 255, 0.5)",
          fontFamily: "Nunito"
        },
        {
          label: "ACHIEVED",
          data: [28, 48, 40, 19, 86, 27, 90],
          borderColor: "rgba(0,0,0,0.09)",
          borderWidth: "0",
          backgroundColor: "rgba(0,0,0,0.07)",
          fontFamily: "Nunito"
        }
      ]
    },
    options: {
      legend: {
        position: 'top',
        labels: {
          fontFamily: 'Nunito'
        }

      },
      scales: {
        xAxes: [{
          ticks: {
            fontFamily: "Nunito"

          }
        }],
        yAxes: [{
          ticks: {
            beginAtZero: true,
            fontFamily: "Nunito"
          }
        }]
      }
    }
  });
}

// single bar chart
var ctx_bar_4 = document.getElementById("singelBarChart");
if (ctx_bar_4) {
  ctx_bar_4.height = 200;
  var singelBarChart = new Chart(ctx_bar_4, {
    type: 'bar',
    data: {
      labels: ["Mon", "Tu", "Wed", "Th", "Fri", "Sat"],
      datasets: [
        {
          label: "THIS WEEK CALLING",
          data: [55, 75, 81, 100, 55, 40],
          borderColor: "rgba(0, 123, 255, 0.9)",
          borderWidth: "0",
          backgroundColor: "rgba(0, 123, 255, 0.5)"
        }
      ]
    },
    options: {
      legend: {
        position: 'top',
        labels: {
          fontFamily: 'Nunito'
        }

      },
      scales: {
        xAxes: [{
          ticks: {
            fontFamily: "Nunito"

          }
        }],
        yAxes: [{
          ticks: {
            beginAtZero: true,
            fontFamily: "Nunito"
          }
        }]
      }
    }
  });
}

//bar chart Comapre month 1
var ctx_bar_5 = document.getElementById("barChartmonthcomapre1");
if (ctx_bar_5) {
  var mq = window.matchMedia( "(max-width: 570px)" );
  if (mq.matches) {
    ctx_bar_5.height = 200;
  }
  else {
    ctx_bar_5.height = 100;
  }
  var barChartmonthcomapre1 = new Chart(ctx_bar_5, {
    type: 'bar',
    defaultFontFamily: 'Nunito',
    data: {
      labels: ["January", "February", "March", "April", "May", "June", "July"],
      datasets: [
        {
          label: "TARGET",
          data: [65, 59, 80, 81, 56, 55, 40],
          borderColor: "rgba(0, 123, 255, 0.9)",
          borderWidth: "0",
          backgroundColor: "rgba(0, 123, 255, 0.5)",
          fontFamily: "Nunito"
        },
        {
          label: "ACHIEVED",
          data: [28, 48, 40, 19, 86, 27, 90],
          borderColor: "rgba(0,0,0,0.09)",
          borderWidth: "0",
          backgroundColor: "rgba(0,0,0,0.07)",
          fontFamily: "Nunito"
        }
      ]
    },
    options: {
      legend: {
        position: 'top',
        labels: {
          fontFamily: 'Nunito'
        }

      },
      scales: {
        xAxes: [{
          ticks: {
            fontFamily: "Nunito"

          }
        }],
        yAxes: [{
          ticks: {
            beginAtZero: true,
            fontFamily: "Nunito"
          }
        }]
      }
    }
  });
}

//bar chart Comapre month 2
var ctx_bar_6 = document.getElementById("barChartmonthcomapre2");
if (ctx_bar_6) {
  var mq = window.matchMedia( "(max-width: 570px)" );
  if (mq.matches) {
    ctx_bar_6.height = 200;
  }
  else {
    ctx_bar_6.height = 100;
  }
  var barChartmonthcomapre2 = new Chart(ctx_bar_6, {
    type: 'bar',
    defaultFontFamily: 'Nunito',
    data: {
      labels: ["January", "February", "March", "April", "May", "June", "July"],
      datasets: [
        {
          label: "TARGET",
          data: [65, 59, 80, 81, 56, 55, 40],
          borderColor: "rgba(0, 123, 255, 0.9)",
          borderWidth: "0",
          backgroundColor: "rgba(0, 123, 255, 0.5)",
          fontFamily: "Nunito"
        },
        {
          label: "ACHIEVED",
          data: [28, 48, 40, 19, 86, 27, 90],
          borderColor: "rgba(0,0,0,0.09)",
          borderWidth: "0",
          backgroundColor: "rgba(0,0,0,0.07)",
          fontFamily: "Nunito"
        }
      ]
    },
    options: {
      legend: {
        position: 'top',
        labels: {
          fontFamily: 'Nunito'
        }

      },
      scales: {
        xAxes: [{
          ticks: {
            fontFamily: "Nunito"

          }
        }],
        yAxes: [{
          ticks: {
            beginAtZero: true,
            fontFamily: "Nunito"
          }
        }]
      }
    }
  });
}

// Maximum Calling Bar Chart
var ctx_bar_7 = document.getElementById("MaximumCallingBarChart");
if (ctx_bar_7) {
  var mq = window.matchMedia( "(max-width: 570px)" );
  if (mq.matches) {
    ctx_bar_7.height = 200;
  }
  else {
    ctx_bar_7.height = 130;
  }
  var MaximumCallingBarChart = new Chart(ctx_bar_7, {
    type: 'bar',
    data: {
      labels: ["Mon", "Tu", "Wed", "Th", "Fri", "Sat"],
      datasets: [
        {
          label: "THIS WEEK CALLING",
          data: [55, 75, 81, 100, 55, 40],
          borderColor: "rgba(0, 123, 255, 0.9)",
          borderWidth: "0",
          backgroundColor: "rgba(0, 123, 255, 0.5)"
        }
      ]
    },
    options: {
      legend: {
        position: 'top',
        labels: {
          fontFamily: 'Nunito'
        }

      },
      scales: {
        xAxes: [{
          ticks: {
            fontFamily: "Nunito"

          }
        }],
        yAxes: [{
          ticks: {
            beginAtZero: true,
            fontFamily: "Nunito"
          }
        }]
      }
    }
  });
}

// Today Calling Bar Chart
var ctx_bar_8 = document.getElementById("TodayCallingBarChart");
if (ctx_bar_8) {
  var mq = window.matchMedia( "(max-width: 570px)" );
  if (mq.matches) {
    ctx_bar_8.height = 200;
  }
  else {
    ctx_bar_8.height = 130;
  }
  var TodayCallingBarChart = new Chart(ctx_bar_8, {
    type: 'bar',
    data: {
      labels: ["Mon", "Tu", "Wed", "Th", "Fri", "Sat"],
      datasets: [
        {
          label: "THIS WEEK CALLING",
          data: [10, 75, 81, 100, 55, 40],
          borderColor: "rgba(0, 123, 255, 0.9)",
          borderWidth: "0",
          backgroundColor: "rgba(0, 123, 255, 0.5)"
        }
      ]
    },
    options: {
      legend: {
        position: 'top',
        labels: {
          fontFamily: 'Nunito'
        }

      },
      scales: {
        xAxes: [{
          ticks: {
            fontFamily: "Nunito"

          }
        }],
        yAxes: [{
          ticks: {
            beginAtZero: true,
            fontFamily: "Nunito"
          }
        }]
      }
    }
  });
}

//Revenue Employee report
var ctx_bar_9 = document.getElementById("RevenueEmp");
if (ctx_bar_9) {
var RevenueEmp = window.matchMedia( "(max-width: 570px)" );
if (RevenueEmp.matches) {
  ctx_bar_9.height = 200;
}
else {
  ctx_bar_9.height = 100;
}
  var myChart = new Chart(ctx_bar_9, {
    type: 'bar',
    defaultFontFamily: 'Nunito',
    data: {
      labels: ["Poonam", "Manisha", "Sweta", "Aarti", "Sarmista", "Priyanka", "Pooja", "Deepa", "Vandana"],
      datasets: [
        {
          label: "TARGET",
          data: [65, 59, 80, 81, 56, 55, 40, 120, 80, 150],
          borderColor: "rgba(0, 123, 255, 0.9)",
          borderWidth: "0",
          backgroundColor: "rgba(0, 123, 255, 0.5)",
          fontFamily: "Nunito"
        },
        {
          label: "ACHIEVED",
          data: [28, 48, 40, 19, 86, 27, 90, 90, 80, 120],
          borderColor: "rgb(25%, 65%, 45%)",
          borderWidth: "0",
          backgroundColor: "rgb(25%, 65%, 45%)",
          fontFamily: "Nunito"
        }
      ]
    },
    options: {
      legend: {
        position: 'top',
        labels: {
          fontFamily: 'Nunito'
        }

      },
      scales: {
        xAxes: [{
          ticks: {
            fontFamily: "Nunito"

          }
        }],
        yAxes: [{
          ticks: {
            beginAtZero: true,
            fontFamily: "Nunito"
          }
        }]
      }
    }
  });
}

// Today Calling Report
var ctx_bar_10 = document.getElementById("TodayCallingReprt");
var WindowHieght = window.matchMedia( "(max-width: 570px)" );
if (ctx_bar_10) {
if (WindowHieght.matches) {
  ctx_bar_10.height = 200;
}
else {
  ctx_bar_10.height = 100;
}
  var TodayCallingReprt = new Chart(ctx_bar_10, {
    type: 'bar',
    data: {
      labels: ["Poonam", "Manisha", "Sweta", "Aarti", "Sarmista", "Priyanka", "Pooja", "Deepa", "Vandana"],
      datasets: [
        {
          label: "Calling",
          data: [10, 75, 81, 100, 55, 40, 30, 40, 90],
          borderColor: "rgba(0, 123, 255, 0.9)",
          borderWidth: "0",
          backgroundColor: "rgba(0, 123, 255, 0.5)"
        }
      ]
    },
    options: {
      legend: {
        position: 'top',
        labels: {
          fontFamily: 'Nunito'
        }

      },
      scales: {
        xAxes: [{
          ticks: {
            fontFamily: "Nunito"

          }
        }],
        yAxes: [{
          ticks: {
            beginAtZero: true,
            fontFamily: "Nunito"
          }
        }]
      }
    }
  });
}

// State Calling Report
var ctx_bar_11 = document.getElementById("StateCallingReprt");
var WindowHieght = window.matchMedia( "(max-width: 570px)" );
if (ctx_bar_11) {
if (WindowHieght.matches) {
  ctx_bar_11.height = 200;
}
else {
  ctx_bar_11.height = 100;
}
  var StateCallingReprt = new Chart(ctx_bar_11, {
    type: 'bar',
    data: {
      labels: ["Maharashtra", "Telangana", "WB", "Chandigagh", "Hariyana", "MP", "Panjab", "UP", "Utrakhand", "Gujrat", "Assam", "Goa", "Karnatka"],
      datasets: [
        {
          label: "State Level CC",
          data: [10, 75, 81, 30, 24, 100, 55, 40, 30, 40, 90, 20, 33],
          borderColor: "rgba(0, 123, 255, 0.9)",
          borderWidth: "0",
          backgroundColor: "rgba(0, 123, 255, 0.5)"
        }
      ]
    },
    options: {
      legend: {
        position: 'top',
        labels: {
          fontFamily: 'Nunito'
        }

      },
      scales: {
        xAxes: [{
          ticks: {
            fontFamily: "Nunito"

          }
        }],
        yAxes: [{
          ticks: {
            beginAtZero: true,
            fontFamily: "Nunito"
          }
        }]
      }
    }
  });
}