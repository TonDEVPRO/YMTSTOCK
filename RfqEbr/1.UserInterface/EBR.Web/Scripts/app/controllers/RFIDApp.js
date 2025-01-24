myApp.controller('RFIDPro', ['$rootScope', '$scope', '$http', '$window' , function ($rootScope, $scope, $http, $window) {

    /*    var today = moment(new Date());*/

    var options = {
        timepicker: true,
        format: "Y-m-d",
        //angularFormat: "yyyy-MM-dd HH:mm:ss",
        dayOfWeekStart: 1,
        css: {
            backgroundColor: "lightgoldenrodyellow"
        }
    };


    $scope.optionDate = angular.extend(angular.copy(options), {
        onShow: function (ct) {
            this.setOptions({
            });
        }
    });

    $scope.optionDateFrom = angular.extend(angular.copy(options),
        {
            onShow: function (ct) {
                this.setOptions({
                    maxDate: $scope.searchReport.DateTo ? $scope.searchReport.DateTo : false
                });
            }
        });
    $scope.optionDateTo = angular.extend(angular.copy(options), {
        onShow: function (ct) {
            this.setOptions({
                minDate: $scope.searchReport.DateFrom ? $scope.searchReport.DateFrom : false
            });
        }
    });


    $scope.ClearData = function () { location.reload(); };

   
}]);


