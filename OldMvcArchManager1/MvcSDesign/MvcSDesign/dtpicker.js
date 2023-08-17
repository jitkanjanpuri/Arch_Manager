var myApp = angular.module('myApp', []);

myApp.controller('myController', ['$scope', function ($scope) {


    $scope.books = [
        { 'id': '1', 'name': 'jQuery' },
        { 'id': '2', 'name': 'CSS3' },
        { 'id': '3', 'name': 'Angular 2' }
    ]

    $scope.booksArray = Object.keys($scope.books)
        .map(function (value, index) {
            return { values: $scope.books[value] }
        }
    );
}]);

myApp.directive("datepicker", function () {

    function link(scope, element, attrs) {
        // CALL THE "datepicker()" METHOD USING THE "element" OBJECT.
        // dateFormat: "dd/MM/yyyy"
       //alert(element);
        element.datepicker({
             dateFormat: "dd-MM-yyyy"
        });
    }

    return {
        require: 'ngModel',
        link: link
    };
});