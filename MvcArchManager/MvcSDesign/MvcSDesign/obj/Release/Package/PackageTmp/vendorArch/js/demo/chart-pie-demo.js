// Set new default font family and font color to mimic Bootstrap's default styling
Chart.defaults.global.defaultFontFamily = 'Nunito', '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
Chart.defaults.global.defaultFontColor = '#858796';

// Pie Chart Example
var ctx_pie_1 = document.getElementById("myPieChart");
if (ctx_pie_1) {
var myPieChart = new Chart(ctx_pie_1, {
  type: 'doughnut',
  data: {
    labels: ["3D Plans", "BirdEyeView", "Exterior", "Interior", "Other", "Planning", "Structure", "Walkthrough"],
    datasets: [{
      data: [14, 20, 49, 79, 40, 78, 30, 27],
      backgroundColor: ['#4e73df', '#1cc88a', '#36b9cc', '#5A6268', '#47A846', '#FEC22A', '#25A2B8', '#343A40'],
      hoverBackgroundColor: ['#2e59d9', '#17a673', '#2c9faf', '#47A246', '#FEC-2A', '#15A2B8', '#341A40'],
      hoverBorderColor: "rgba(234, 236, 244, 1)",
    }],
  },
  options: {
    maintainAspectRatio: false,
    tooltips: {
      backgroundColor: "rgb(255,255,255)",
      bodyFontColor: "#858796",
      borderColor: '#dddfeb',
      borderWidth: 1,
      xPadding: 15,
      yPadding: 15,
      displayColors: false,
      caretPadding: 10,
    },
    legend: {
      display: false
    },
    cutoutPercentage: 80,
  },
});
}



// Pie Chart Maximum Quotations
var ctx_pie_2 = document.getElementById("PieChartMaximumQuotations");
if (ctx_pie_2) {
var myPieChartMax = new Chart(ctx_pie_2, {
  type: 'doughnut',
  data: {
    labels: ["3D Plans", "BirdEyeView", "Exterior", "Interior", "Other", "Planning", "Structure", "Walkthrough"],
    datasets: [{
      data: [19, 20, 29, 39, 100, 68, 3, 27],
      backgroundColor: ['#4e73df', '#1cc88a', '#36b9cc', '#5A6268', '#47A846', '#FEC22A', '#25A2B8', '#343A40'],
      hoverBackgroundColor: ['#2e59d9', '#17a673', '#2c9faf', '#47A246', '#FEC-2A', '#15A2B8', '#341A40'],
      hoverBorderColor: "rgba(234, 236, 244, 1)",
    }],
  },
  options: {
    maintainAspectRatio: false,
    tooltips: {
      backgroundColor: "rgb(255,255,255)",
      bodyFontColor: "#858796",
      borderColor: '#dddfeb',
      borderWidth: 1,
      xPadding: 15,
      yPadding: 15,
      displayColors: false,
      caretPadding: 10,
    },
    legend: {
      display: false
    },
    cutoutPercentage: 80,
  },
});
}

// Pie Chart Maximum Quotations
var ctx_pie_3 = document.getElementById("PieChartTotalQuotations");
if (ctx_pie_3) {
var myPieChartTotal = new Chart(ctx_pie_3, {
  type: 'doughnut',
  data: {
    labels: ["3D Plans", "BirdEyeView", "Exterior", "Interior", "Other", "Planning", "Structure", "Walkthrough"],
    datasets: [{
      data: [9, 30, 24, 29, 200, 38, 83, 27],
      backgroundColor: ['#4e73df', '#1cc88a', '#36b9cc', '#5A6268', '#47A846', '#FEC22A', '#25A2B8', '#343A40'],
      hoverBackgroundColor: ['#2e59d9', '#17a673', '#2c9faf', '#47A246', '#FEC-2A', '#15A2B8', '#341A40'],
      hoverBorderColor: "rgba(234, 236, 244, 1)",
    }],
  },
  options: {
    maintainAspectRatio: false,
    tooltips: {
      backgroundColor: "rgb(255,255,255)",
      bodyFontColor: "#858796",
      borderColor: '#dddfeb',
      borderWidth: 1,
      xPadding: 15,
      yPadding: 15,
      displayColors: false,
      caretPadding: 10,
    },
    legend: {
      display: false
    },
    cutoutPercentage: 80,
  },
});
}

// Quotations Report
var ctx_pie_4 = document.getElementById("QuotationsReport")
if (ctx_pie_4) {
var QuotationsReport = new Chart(ctx_pie_4, {
  type: 'doughnut',
  data: {
    labels: ["3D Plans", "BirdEyeView", "Exterior", "Interior", "Other", "Planning", "Structure", "Walkthrough"],
    datasets: [{
      data: [9, 30, 24, 29, 200, 38, 83, 27],
      backgroundColor: ['#4e73df', '#1cc88a', '#36b9cc', '#5A6268', '#47A846', '#FEC22A', '#25A2B8', '#343A40'],
      hoverBackgroundColor: ['#2e59d9', '#17a673', '#2c9faf', '#47A246', '#FEC-2A', '#15A2B8', '#341A40'],
      hoverBorderColor: "rgba(234, 236, 244, 1)",
    }],
  },
  options: {
    maintainAspectRatio: false,
    responsive: true,
    cutoutPercentage: 55,
    animation: {
      animateScale: true,
      animateRotate: true
    },
    legend: {
      display: true
    },
    tooltips: {
      titleFontFamily: "Nunito",
      xPadding: 15,
      yPadding: 10,
      caretPadding: 0,
      bodyFontSize: 16
    }
  }
});
}

// Confirmed Projects
var ctx_pie_5 = document.getElementById("ConfirmedProjects")
if (ctx_pie_5) {
var ConfirmedProjects = new Chart(ctx_pie_5, {
  type: 'doughnut',
  data: {
    labels: ["3D Plans", "BirdEyeView", "Exterior", "Interior", "Other", "Planning", "Structure", "Walkthrough"],
    datasets: [{
      data: [9, 30, 24, 29, 200, 38, 83, 27],
      backgroundColor: ['#4e73df', '#1cc88a', '#36b9cc', '#5A6268', '#47A846', '#FEC22A', '#25A2B8', '#343A40'],
      hoverBackgroundColor: ['#2e59d9', '#17a673', '#2c9faf', '#47A246', '#FEC-2A', '#15A2B8', '#341A40'],
      hoverBorderColor: "rgba(234, 236, 244, 1)",
    }],
  },
  options: {
    maintainAspectRatio: false,
    responsive: true,
    cutoutPercentage: 55,
    animation: {
      animateScale: true,
      animateRotate: true
    },
    legend: {
      display: true
    },
    tooltips: {
      titleFontFamily: "Nunito",
      xPadding: 15,
      yPadding: 10,
      caretPadding: 0,
      bodyFontSize: 16
    }
  }
});
}

