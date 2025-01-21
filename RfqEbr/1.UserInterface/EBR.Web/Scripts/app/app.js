"use strict";

// Define the HttpConfig module and configure HTTP interceptors
angular.module('HttpConfig', [])
    .config(['$httpProvider', function ($httpProvider) {
        $httpProvider.defaults.headers.common["X-Requested-With"] = 'XMLHttpRequest';

        $httpProvider.interceptors.push(['$q', '$injector', function ($q, $injector) {
            const spinnerFunction = () => {
                // Placeholder for spinner function
                console.log("Spinner function triggered");
            };

            let $http;
            let callback;

            return {
                request: function (config) {
                    console.log("Request intercepted");
                    spinnerFunction();
                    callback = config.callback;
                    return config;
                },
                response: function (response) {
                    $http = $http || $injector.get('$http');
                    if ($http.pendingRequests.length < 1) {
       /*                 $.unblockUI(); // Uncomment if you use blockUI for blocking UI during requests*/
                    }
                    return response;
                },
                responseError: function (response) {
                    $http = $http || $injector.get('$http');
                    if ($http.pendingRequests.length < 1) {
           /*              $.unblockUI(); // Uncomment if you use blockUI for blocking UI during errors*/
                        if (response.status === 403) {
                            if (response.data === "Session timeout, Please login again.") {
                                box.error(response.data, () => {
                                    document.location = window.autherize;
                                });
                            } else {
                                box.error(response.data);
                            }
                        } else {
                           // setTimeout(() => { box.error(response.data, callback); }, 0);
                        }
                    }
                    return $q.reject(response);
                }
            };
        }]);
    }]);

// Define the directive for a fixed header in tables
angular.module('anguFixedHeaderTable', [])
    .directive('fixedHeader', ['$timeout', function ($timeout) {
        return {
            restrict: 'A',
            scope: {
                tableHeight: '@'
            },
            link: function ($scope, $elem) {
                const elem = $elem[0];

                const isVisible = (el) => {
                    const style = window.getComputedStyle(el);
                    return style.display !== 'none' && el.offsetWidth !== 0;
                };

                const isTableReady = () => isVisible(elem.querySelector("tbody")) && elem.querySelector('tbody tr:first-child') != null;

                const unbindWatch = $scope.$watch(isTableReady, (newValue) => {
                    if (newValue) {
                        angular.element(elem.querySelectorAll('thead, tbody, tfoot')).css('display', '');

                        $timeout(() => {
                            angular.forEach(elem.querySelectorAll('tr:first-child th'), (thElem, i) => {
                                const tdElems = elem.querySelector(`tbody tr:first-child td:nth-child(${i + 1})`);
                                const tfElems = elem.querySelector(`tfoot tr:first-child td:nth-child(${i + 1})`);
                                const columnWidth = tdElems ? tdElems.offsetWidth : thElem.offsetWidth;

                                if (tdElems) tdElems.style.width = `${columnWidth}px`;
                                if (thElem) thElem.style.width = `${columnWidth}px`;
                                if (tfElems) tfElems.style.width = `${columnWidth}px`;
                            });

                            angular.element(elem.querySelectorAll('thead, tfoot')).css('display', 'block');
                            angular.element(elem.querySelectorAll('tbody')).css({
                                'display': 'block',
                                'height': $scope.tableHeight || 'inherit',
                                'overflow': 'auto'
                            });

                            const tbody = elem.querySelector('tbody');
                            const scrollBarWidth = tbody.offsetWidth - tbody.clientWidth;
                            if (scrollBarWidth > 0) {
                                const lastColumn = elem.querySelector('tbody tr:first-child td:last-child');
                                if (lastColumn) {
                                    lastColumn.style.width = `${lastColumn.offsetWidth - scrollBarWidth - 2}px`;
                                }
                            }
                        });

                        unbindWatch();
                    }
                });
            }
        };
    }]);

// Define the main application module
const myApp = angular.module('MainApp', ['HttpConfig', 'anguFixedHeaderTable']);