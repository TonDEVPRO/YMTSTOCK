myApp.controller('mainController', ['$rootScope', '$scope', '$http', '$window', '$interval', function ($rootScope, $scope, $http, $window, $interval) {

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

    $scope.searchReport = [{ DateFrom: "", DateTo: "" }];
    $scope.ClearData = function () { location.reload(); };
    $scope.InvoiceData = [{ PartNoItem: '', Customers: '', Packages: '' }];
    $scope.ClearData = function () { location.reload(); };
    $scope.SaveLogin = function (EmpNoData) {
        if (EmpNoData == undefined) {
            Swal.fire({
                icon: 'error',
                title: 'EmployeeNo && Password',
                text: 'Please Insert EmployeeNo && Password'
            });

        } else if (EmpNoData.EmpNo == '' || EmpNoData.EmpNo == undefined) {
            Swal.fire({
                icon: 'error',
                title: 'EmployeeNo',
                text: 'Please Insert EmployeeNo'
            });
        } else if (EmpNoData.EmpPass == '' || EmpNoData.EmpPass == undefined) {
            Swal.fire({
                icon: 'error',
                title: 'Password',
                text: 'Please Insert Password'
            });
        } else {
            $http.post(window.baseUrl + 'Home/checkUser',
                {
                    Emp_EmpNo: EmpNoData.EmpNo,
                    Emp_Pass: EmpNoData.EmpPass
                }).then(function (res) {

                    $scope.listEmpNo = res.data;
                    console.log($scope.listEmpNo);

                    if ($scope.listEmpNo == '' || $scope.listEmpNo == undefined) {
                        Swal.fire({
                            icon: 'error',
                            title: 'No Authorized...',
                            text: 'No Authorized In System'
                        });
                    } else {


                        window.location.href = window.baseUrl + "Home/YMTHome?EmpNo=" + $scope.listEmpNo.Id
                    }
                });
        }
    };
    $scope.YMTHrManPower = function (EmpNo) {
        $http.get(window.baseUrl + 'Home/APIPRO').then(function (response) {
            var data = response.data;
            $scope.listdata = data;

            // เตรียมข้อมูลสำหรับกราฟ
            var labels = data.map(function (item) { return item.ProductName; });
            var salesData = data.map(function (item) { return item.SalesAmount; });
            var totalSales = salesData.reduce(function (acc, value) { return acc + value; }, 0);

            // สร้าง Pie chart ด้วย Chart.js
            var ctx = document.getElementById('salesChart').getContext('2d');
            var chart = new Chart(ctx, {
                type: 'pie',
                data: {

                    datasets: [{
                        label: 'Sales Amount',
                        data: salesData,
                        backgroundColor: [

                            'rgba(54, 162, 235, 0.2)',
                            'rgba(255, 206, 86, 0.2)',
                            'rgba(75, 192, 192, 0.2)',
                            'rgba(153, 102, 255, 0.2)',
                            'rgba(255, 159, 64, 0.2)',
                            'rgba(255, 99, 132, 0.2)'
                        ],
                        borderColor: [

                            'rgba(54, 162, 235, 1)',
                            'rgba(255, 206, 86, 1)',
                            'rgba(75, 192, 192, 1)',
                            'rgba(153, 102, 255, 1)',
                            'rgba(255, 159, 64, 1)',
                            'rgba(255, 99, 132, 1)'
                        ],
                        borderWidth: 2
                    }],
                    labels: labels
                },
                options: {
                    responsive: true,
                    plugins: {
                        //legend: {
                        //    position: 'top',
                        //    labels: {
                        //        font: {
                        //            size: 20
                        //        }
                        //    }
                        //},
                        tooltip: {
                            callbacks: {
                                label: function (tooltipItem) {
                                    var label = tooltipItem.label || '';
                                    var value = tooltipItem.raw;
                                    var percentage = ((value / totalSales) * 100).toFixed(2);
                                    return label + '  : ' + value + ' (' + percentage + '%)';
                                }
                            }
                        },
                        datalabels: {
                            formatter: function (value, context) {
                                var percentage = ((value / totalSales) * 100).toFixed(2);
                                return percentage + '%';
                            },
                            color: '#000',
                        }
                    }
                },
                plugins: [ChartDataLabels]
            });
        });

    };
    $scope.YMTHomeData = function (EmpNo) {



        $http.post(window.baseUrl + 'Home/GetEmployee',
            {
                EmployeeNo: EmpNo
            }).then(function (res) {
                $scope.emplist = res.data;
                console.log($scope.emplist);
            });
    }
    $scope.YMTPNDSData = function (EmpNo) {
        $http.post(window.baseUrl + 'Home/GetEmployee',
            {
                EmployeeNo: EmpNo
            }).then(function (res) {
                $scope.emplist = res.data;
                $scope.users = [
                    { id: 1, name: 'John Doe', email: 'john@example.com', role: 'Admin' },
                    { id: 2, name: 'Jane Smith', email: 'jane@example.com', role: 'User' },
                    { id: 3, name: 'Michael Johnson', email: 'michael@example.com', role: 'Manager' },
                    { id: 4, name: 'Alice Brown', email: 'alice@example.com', role: 'Admin' },
                    { id: 5, name: 'Bob Davis', email: 'bob@example.com', role: 'User' }
                ];

                $scope.$on('ngRepeatFinished', function () {
                    $('#example').DataTable();
                });

            }
            )
    };
    $scope.YMTQCOnlineData = function (EmpNo) {
        $http.post(window.baseUrl + 'Home/GetEmployee',
            {
                EmployeeNo: EmpNo
            }).then(function (res) {
                $scope.emplist = res.data;
            });
    }
    $scope.YMTPNoteData = function (EmpNo) {
        $scope.StatusRole = 0;
        $http.post(window.baseUrl + 'Home/GetEmployee',
            {
                EmployeeNo: EmpNo
            }).then(function (res) {
                $scope.emplist = res.data;
            });
        if ($scope.StatusRole == 0) {
            $scope.GetMasterCustomerOK();
        }
        $http.post(window.baseUrl + 'Home/GetMasterCustomerName').then(function (res) {
            $scope.MasterCust = res.data;
        });
    }
    $scope.YMTNdsStockData = function (EmpNo) {
        $http.post(window.baseUrl + 'Home/GetEmployee',
            {
                EmployeeNo: EmpNo
            }).then(function (res) {
                $scope.emplist = res.data;

                $scope.data = [];
                $http.get(window.baseUrl + 'Home/GetData')
                    .then(function (response) {
                        $scope.data = response.data.data;

                        setTimeout(function () {
                            $('#dataTable').DataTable();
                        }, 0);
                    });
            });

        $scope.getStockStyle();
    }


    $scope.getStockStyle = function () {
        $http.get(window.baseUrl + 'Home/getStockStyleNDS')
            .then(function (response) {
                $scope.ListStockStyle = response.data;
            })
    };



    // ** Start **  MenuList StockNDS //

    $scope.getstocknds = function () {
        $http.get(window.baseUrl + 'Home/GetDataStockNds')
            .then(function (response) {
                $scope.employees = response.data.data;
                console.log(response.data);
            })
            .catch(function (error) {
                console.error(error);
            });
    };


    $scope.GetDescStyle = function (ddlStyle) {
        $scope.ddlcolor = '';
        $scope.ddlSize = '';
        $http.post(window.baseUrl + 'Home/GetDescStyle', { Style: ddlStyle }).then(function (response) {
            $scope.ListDesc = response.data;

            $http.post('GetColors', {
                Style: $scope.ListDesc.Style
            }).then(function (response) {
                $scope.ListColor = response.data;
            })
        })
    };

    $scope.GetDescColors = function (ddlStyle, ddlcolor) {
        $scope.ddlSize = '';
        $scope.ListStockTotal = '';

        $http.post(window.baseUrl + 'Home/GetSizeNDS', { Style: ddlStyle, color: ddlcolor })
            .then(function (response) {
                $scope.ListSizeNDS = response.data;
                console.log($scope.ListSizeNDS);
            })
    };

    $scope.GetDetailAll = function (ddlStyle, ddlcolor, ddlSize) {
        $http.post(window.baseUrl + 'Home/GetAllStock', { Style: ddlStyle, color: ddlcolor, Size: ddlSize })
            .then(function (response) {
                $scope.ListStockTotal = response.data;
                console.log($scope.ListStockTotal);
            })
    };

    // ** END **  MenuList StockNDS //

    $scope.reloadPage = function () {
        $window.location.reload();
    };

    //Sum Total Add stock
    $scope.SumTotal = function (Totals, AddQty) {
        $scope.TotalStock = Totals + AddQty;
    };

    $scope.ReduceTotal = function (ListStocks, ReduceQty) {
        if (ListStocks.Total < ReduceQty) {
            Swal.fire({
                icon: "error",
                title: "Error",
                text: "You entered a number greater than total qty."
            });
            $scope.ReduceQty = '';
            $scope.UpTotalStock = ListStocks.Total;
        } else {

            $scope.UpTotalStock = ListStocks.Total - ReduceQty;
        }
    };



    $scope.editQuotation = function (id) {
        $scope.UpTotalStock = '';

        $http.post(window.baseUrl + 'Home/EditStock', { id: id })
            .then(function (response) {
                $scope.ListStock = response.data;
            })
    };



    // SaveStockNDS //
    $scope.SaveStock = function (ListStockTotal, TotalStock, AddNewQty, FullUserId) {
        $scope.ListSaveStock =
        {
            Total: TotalStock,
            Id: ListStockTotal.Id,
            Color: ListStockTotal.Color,
            Cost: ListStockTotal.Cost,
            Description: ListStockTotal.Description,
            Price: ListStockTotal.Price,
            Size: ListStockTotal.Size,
            Style: ListStockTotal.Style,
            Status: ListStockTotal.Status,
            Remark: ListStockTotal.Remark
        };
        console.log($scope.ListSaveStock);

        $scope.ListSaveStockLog =
        {
            StockId: ListStockTotal.Id,
            Total: ListStockTotal.Total,
            Remark: ListStockTotal.Remark,
            AddQty: AddNewQty,
            TotalQty: TotalStock,
            CreateBy: FullUserId
        };


        $http.post(window.baseUrl + 'Home/SaveStocks', {
            ndsShopStocks: $scope.ListSaveStock
        }).then(function (response) {
            $scope.ListStockTotals = response.data;

            $http.post(window.baseUrl + 'Home/SaveStockLogs', {
                ndsShopStocksLogs: $scope.ListSaveStockLog
            }).then(function (response) {
                $scope.ListStocklogs = response.data;
            });

            Swal.fire({
                icon: "success",
                title: "You Add Stock Complete",
                text: "TotalStock : " + TotalStock + ""
            }).then(function () {
                $window.location.href = "/Home/YMTNdsStock?EmpNo=" + FullUserId;
            });
        });
    };

    $scope.UpdateStockss = function (ListStock, ReduceQty, UpTotalStock, FullUserId) {

        $scope.ListUpdateStock =
        {
            Total: UpTotalStock, Id: ListStock.Id,
            Color: ListStock.Color, Cost: ListStock.Cost,
            Description: ListStock.Description, Price: ListStock.Price,
            Size: ListStock.Size, Style: ListStock.Style, Status: ListStock.Status
        };

        $scope.ListUpdateStockLog =
        {
            StockId: ListStock.Id,
            Total: ListStock.Total,
            AddQty: ReduceQty,
            TotalQty: UpTotalStock,
            CreateBy: FullUserId
        };


        $http.post(window.baseUrl + 'Home/UpdateStocks', {
            ndsShopStocks: $scope.ListUpdateStock
        }).then(function (response) {
            $scope.UpdateStockTotals = response.data;

            console.log($scope.UpdateStockTotals);
            $http.post(window.baseUrl + 'Home/UpdateStockLogs', {
                ndsShopStocksLogs: $scope.ListUpdateStockLog
            }).then(function (response) {
                $scope.ListStocklogs = response.data;
            });

            Swal.fire({
                icon: "success",
                title: "You Update Stock Complete",
                text: "TotalStock : " + UpTotalStock + ""
            }).then(function () {
                $window.location.href = "/Home/YMTNdsStock?EmpNo=" + FullUserId;
            });
        });
    };







    $scope.YMTNdsSystemData = function (EmpNo) {
        console.log(EmpNo);


        $http.post(window.baseUrl + 'Home/GetEmployee',
            {
                EmployeeNo: EmpNo
            }).then(function (res) {
                $scope.emplist = res.data;
            });
    }



    $scope.YMTHolidayStockData = function (EmpNo) {
        console.log(EmpNo);


        $http.post(window.baseUrl + 'Home/GetEmployee',
            {
                EmployeeNo: EmpNo
            }).then(function (res) {
                $scope.emplist = res.data;
            });
    }



    $scope.GetData = function () {
        $http.get('/Home/getStockStyleNDS')
            .then(function (response) {
                $scope.ListStockStyle = response.data;

                console.log($scope.ListStockStyle);
            });

        $scope.complete = function (string) {
            var output = [];
            angular.forEach($scope.ListStockStyle, function (StyleNo) {
                if (StyleNo.toLowerCase().indexOf(string.toLowerCase()) >= 0) {
                    output.push(StyleNo);
                }
            });
            $scope.filterCountry = output;
        };

        $scope.fillTextbox = function (string) {
            $scope.StyleNo = string;
            $scope.filterCountry = null;

            $http.post('/Home/GetDescStyle', { Style: $scope.StyleNo }).then(function (response) {
                $scope.ListStyle = response.data;

                console.log($scope.ListStyle);


                $scope.GetColorData();
            });
        };
    };

    $scope.GetColorData = function () {
        $http.get('/Home/GetAutoColors')
            .then(function (response) {
                $scope.ListColorsAuto = response.data;

                console.log($scope.ListColorsAuto);
            });

        $scope.completeColor = function (string) {
            var output = [];
            angular.forEach($scope.ListColorsAuto, function (Color) {
                if (Color.toLowerCase().indexOf(string.toLowerCase()) >= 0) {
                    output.push(Color);
                }
            });
            $scope.filterDataColor = output;
        };

        $scope.fillColor = function (string) {
            $scope.Color = string;
            $scope.filterDataColor = null;

            console.log($scope.Color);

            $scope.GetSizeData();
        };
    };


    $scope.GetSizeData = function () {
        $http.get('/Home/GetAutoSizes')
            .then(function (response) {
                $scope.ListSizeAuto = response.data;
            });

        $scope.completeSize = function (string) {
            var output = [];
            angular.forEach($scope.ListSizeAuto, function (Size) {
                if (Size.toLowerCase().indexOf(string.toLowerCase()) >= 0) {
                    output.push(Size);
                }
            });
            $scope.filterDataSize = output;
        };

        $scope.fillSize = function (string) {
            $scope.Size = string;
            $scope.filterDataSize = null;

            $scope.AddMaster = {
                Cost: '',
                Price: '',
                Total: ''
            };
        };
    };





    $scope.SaveMasterStock = function (StyleNo, Descriptions, Colors, Sizes, AddMaster, EmpNo) {

        $scope.ListMasterStock = {
            Style: StyleNo,
            Color: Colors,
            Size: Sizes,
            Description: Descriptions,
            Cost: AddMaster.Cost,
            Price: AddMaster.Price,
            Total: AddMaster.Total,
            Remark: AddMaster.Remark,
            CreateBy: EmpNo
        }



        if (AddMaster.Cost > AddMaster.Price) {
            Swal.fire({
                icon: "error",
                title: "less than the cost price",
                text: "The selling price cannot be less than the cost price."
            });

            $scope.AddMaster.Price = '';
        }
        else {
            $http.post(window.baseUrl + 'Home/CheckMasterStock', {
                MasterStocks: $scope.ListMasterStock
            }).then(function (res) {
                $scope.ListMasterStock = res.data

                if ($scope.ListMasterStock == 'HaveStock') {
                    Swal.fire({
                        icon: "error",
                        title: "Already have stock",
                        text: "Please add stock in menu AddStock"
                    });
                } else {
                    $http.post(window.baseUrl + 'Home/SaveMasterStock', {
                        MasterSaveStocks: $scope.ListMasterStock
                    }).then(function (res) {
                        $scope.ListSaveDataStock = res.data
                        console.log($scope.ListSaveDataStock);


                        Swal.fire({
                            icon: "success",
                            title: "You Save Master Stock Complete",
                            text: "TotalStock : " + $scope.ListSaveDataStock.Total + ""
                        }).then(function () {
                            $window.location.href = "/Home/YMTNdsStock?EmpNo=" + FullUserId;
                        });


                        $scope.Color = '';
                        $scope.Size = '';
                        $scope.AddMaster = {
                            Cost: '',
                            Price: '',
                            Total: ''
                        };

                    });
                }
            });
        }
    }


    $scope.NDSHolidayStocks = function (EmpNo) {
        $http.post(window.baseUrl + 'Home/GetEmployee',
            {
                EmployeeNo: EmpNo
            }).then(function (res) {
                $scope.emplist = res.data;

                $scope.ListHolidaydata = [];
                $http.get(window.baseUrl + 'Home/GetholidayData')
                    .then(function (response) {
                        $scope.ListHolidaydata = response.data.data;

                        setTimeout(function () {
                            $('#dataTable').DataTable();
                        }, 0);
                    });
                $scope.GetStockHoliday();
            });
    }





    $scope.GetStockHoliday = function () {
        $http.get(window.baseUrl + 'Home/GetStockHolidays')
            .then(function (response) {
                $scope.ListHolidayOrder = response.data;
            })
    };





    $scope.GetHolidayOrder = function (ddlOrderNo) {
        $http.post(window.baseUrl + 'Home/GetHolidayStyle', { OrderNos: ddlOrderNo }).then(function (response) {
            $scope.ListHolidayStyle = response.data;
            console.log($scope.ListHolidayStyle);
        })
    };



    $scope.GetHolidayDescStyle = function (ddlOrderNo, ddlStyle) {

        $http.post(window.baseUrl + 'Home/GetHolidayDescStyle', { OrderNo: ddlOrderNo, Style: ddlStyle }).then(function (response) {
            $scope.ListHolidayDesc = response.data;
            $http.post(window.baseUrl + 'Home/GetHolidayColor', { OrderNo: ddlOrderNo, Style: ddlStyle }).then(function (response) {
                $scope.ListHolidayColor = response.data;
            })
        })
    };


    $scope.GetHolidaySize = function (ddlOrderNo, ddlStyle, Colors) {

        console.log(ddlOrderNo);
        console.log(ddlStyle);
        console.log(Colors);

        $http.post(window.baseUrl + 'Home/GetHolidaySizes', { OrderNo: ddlOrderNo, Style: ddlStyle, Color: Colors }).then(function (response) {
            $scope.ListHolidaySize = response.data;
            console.log($scope.ListHolidaySize);

            $scope.ddlSize = '';

            $scope.ListHolidayData = {
                Cost: '',
                Price: '',
                Total: '',
                Remark: ''
            };
        })
    };




    $scope.GetHolidayDetailAll = function (ddlOrderNo, ddlStyle, Colors, ddlSize) {

        console.log(ddlOrderNo);
        console.log(ddlStyle);
        console.log(Colors);

        $http.post(window.baseUrl + 'Home/GetHolidayDetailAlls', { OrderNo: ddlOrderNo, Style: ddlStyle, Color: Colors, Size: ddlSize }).then(function (response) {



            $scope.ListHolidayData = response.data;
            console.log($scope.ListHolidayData);



        })
    };



    $scope.SaveHolidayStock = function (ddlOrderNo, ddlStyle, Colors, ddlSize, alldata, UserIds) {

        console.log(ddlOrderNo);
        console.log(ddlStyle);
        console.log(Colors);
        console.log(ddlSize);
        console.log(alldata);
        console.log(UserIds);



        $http.post(window.baseUrl + 'Home/SaveHolidayStocks', { SaveHoliday: alldata, UserId: UserIds }).then(function (response) {



            $scope.ListdataA = response.data;
            console.log($scope.ListdataA);

            Swal.fire({
                icon: "success",
                title: "You save Holiday Stock Complete",
                text: "TotalStock : " + $scope.ListdataA.Total + ""
            }).then(function () {
                $window.location.href = "/Home/NDSHolidayStock?EmpNo=" + FullUserId;
            });

        })


    };





    $scope.GetMasterSeason = function (MasterSea) {
        $http.post(window.baseUrl + 'Home/GetMasterBandName', {
            CodeName: MasterSea
        }).then(function (res) {
            $scope.ListCodeName = res.data
            console.log($scope.ListCodeName);
        });
        $http.post(window.baseUrl + 'Home/GetMasterSeasons', {
            CodeName: MasterSea
        }).then(function (res) {
            $scope.ListSeason = res.data
            console.log($scope.ListSeason);
        });
    }
    $scope.GetMasterStyleName = function (MasterCustomer, SeasonNames) {

        $http.post(window.baseUrl + 'Home/GetListAllStyle', {
            CodeName: MasterCustomer,
            SeasonName: SeasonNames
        }).then(function (res) {
            $scope.Listdata = res.data
        });



        $http.post(window.baseUrl + 'Home/GetMasterStyle', {
            CodeName: MasterCustomer,
            SeasonName: SeasonNames
        }).then(function (res) {
            $scope.ListStylename = res.data
        });
    }
    $scope.GetMasterOrderNo = function (MasterCustomer, SeasonNames, StyleNames) {

        $http.post(window.baseUrl + 'Home/GetListAllStyle', {
            CodeName: MasterCustomer,
            ListStyle: StyleNames,
            SeasonName: SeasonNames
        }).then(function (res) {
            $scope.Listdata = res.data
        });

        $http.post(window.baseUrl + 'Home/GetMasterOrder', {
            CodeName: MasterCustomer,
            SeasonName: SeasonNames,
            StyleName: StyleNames
        }).then(function (res) {
            $scope.ListOrderName = res.data
            console.log($scope.ListOrderName);
        });

    }
    $scope.uploadFiles = function () {

        var files = document.getElementById('fileInput').files;
        var formData = new FormData();

        console.log(files);

        if (files.length == 0) {
            Swal.fire({
                icon: 'error',
                title: '!!! No files uploaded.!!!',
                text: 'Please uploadfiles',
            });
        } else {


            $scope.ListDataAlready =
            {
                brandCode: $scope.MasterCustomer,
                brandName: $scope.ListCodeName.CustomerName,
                season: $scope.SeasonName,
                styleName: $scope.StyleName,
                OrderNo: $scope.OrderNo,
                TypeName: $scope.TypeName,
                NewFileName: files[0].name
            };

            console.log($scope.ListDataAlready);

            $http.post(window.baseUrl + 'Home/checkfilealready', {
                DataAlready: $scope.ListDataAlready
            }).then(function (res) {
                $scope.listfile = res.data
                console.log($scope.listfile);

                if ($scope.listfile == '') {
                    // Append files
                    for (var i = 0; i < files.length; i++) {
                        formData.append('files', files[i]);
                    }

                    // Append form data
                    formData.append('brandCode', $scope.MasterCustomer);
                    formData.append('brandName', $scope.ListCodeName.CustomerName);
                    formData.append('season', $scope.SeasonName);
                    formData.append('styleName', $scope.StyleName);
                    formData.append('OrderNo', $scope.OrderNo);
                    formData.append('TypeName', $scope.TypeName);
                    formData.append('Id', $scope.emplist.Id);

                    console.log($scope.emplist);




                    $http.post(window.baseUrl + 'Home/UploadFiles', formData, {
                        headers: { 'Content-Type': undefined },
                        transformRequest: angular.identity
                    }).then(function (res) {
                        $scope.Listdataupload = res.data
                        console.log($scope.Listdataupload);



                        if ($scope.Listdataupload == 'No files uploaded.') {
                            Swal.fire({
                                icon: 'error',
                                title: '!!! No files uploaded.!!!',
                                text: 'Please uploadfiles',
                            });
                        } else {

                            Swal.fire({
                                icon: 'success',
                                title: 'Uploadfile Success',
                                text: 'Uploadfile Success'
                            });
                            $scope.Listdata = $scope.Listdataupload.GetFinishdata;
                        }

                    });

                } else {

                    Swal.fire({
                        icon: 'error',
                        title: '!!! Error Already Data and FileUpload.!!!',
                        text: 'Please check Data and file upload',
                    });


                }
            });
        }

    };

    $scope.checkEnter = function (event, EmpNoData) {
        console.log(event);


        $scope.inputValue = '';

        if (event.keyCode === 13) {
            if (EmpNoData == undefined) {

                Swal.fire({
                    icon: 'error',
                    title: 'EmployeeNo && Password',
                    text: 'Please Insert EmployeeNo && Password'
                });

            } else if (EmpNoData.EmpNo == '' || EmpNoData.EmpNo == undefined) {
                Swal.fire({
                    icon: 'error',
                    title: 'EmployeeNo',
                    text: 'Please Insert EmployeeNo'
                });
            } else if (EmpNoData.EmpPass == '' || EmpNoData.EmpPass == undefined) {
                Swal.fire({
                    icon: 'error',
                    title: 'Password',
                    text: 'Please Insert Password'
                });
            } else {
                $http.post(window.baseUrl + 'Home/checkUser',
                    {
                        Emp_EmpNo: EmpNoData.EmpNo,
                        Emp_Pass: EmpNoData.EmpPass
                    }).then(function (res) {

                        $scope.listEmpNo = res.data;
                        console.log($scope.listEmpNo);

                        if ($scope.listEmpNo == '' || $scope.listEmpNo == undefined) {
                            Swal.fire({
                                icon: 'error',
                                title: 'No Authorized...',
                                text: 'No Authorized In System'
                            });
                        } else {
                            window.location.href = window.baseUrl + "Home/YMTHome?EmpNo=" + $scope.listEmpNo.Id
                        }
                    });
            }
        }
    };

    //CheckMenu
    $scope.ChecKStaus = function (Status) {
        $scope.StatusRole = Status;
        console.log(Status);

        if ($scope.StatusRole == 2) {
            $scope.GetMasterCustomerOK();
        }
    };
    $scope.GetMasterCustomerOK = function () {
        $http.post(window.baseUrl + 'Home/GetMasterCustomers').then(function (res) {
            $scope.GetMasterCustomesData = res.data
            console.log($scope.GetMasterCustomesData);
        });
    };
    $scope.GetNameCustomer = function (UploadLoadCustomer) {
        $http.post(window.baseUrl + 'Home/GetMasterNameCustomers', {
            getBandname: UploadLoadCustomer
        }).then(function (res) {
            $scope.ListNameCustomer = res.data
        });

        $http.post(window.baseUrl + 'Home/GetUpdateSeason', {
            CodeName: UploadLoadCustomer
        }).then(function (res) {
            $scope.GetListSeason = res.data
            console.log($scope.GetListSeason);
        });
    };
    $scope.GetUpateStyles = function (UploadLoadCustomer, SeasonNames) {
        $http.post(window.baseUrl + 'Home/GetUpateMasterStyle', {
            CodeName: UploadLoadCustomer,
            SeasonName: SeasonNames
        }).then(function (res) {
            $scope.GetListStylename = res.data
            console.log($scope.GetListStylename);
        });
    }
    $scope.GetUpdateOrderNo = function (UploadLoadCustomer, SeasonUpdateName, StyleNameUpdate) {
        $http.post(window.baseUrl + 'Home/GetUpdateMasterOrder', {
            UpdateCodeName: UploadLoadCustomer,
            UpdateSeasonName: SeasonUpdateName,
            UpdateStyleName: StyleNameUpdate
        }).then(function (res) {
            $scope.GetListOrderName = res.data
            console.log($scope.GetListOrderName);
        });

    }
    $scope.GetUpdateTypeName = function (UploadLoadCustomer, SeasonUpdateName, StyleNameUpdate, OrderNoUpdate) {

        $http.post(window.baseUrl + 'Home/GetUpdateMasterTypeName', {
            UpdateCodeName: UploadLoadCustomer,
            UpdateSeasonName: SeasonUpdateName,
            UpdateStyleName: StyleNameUpdate,
            UpdateOrderNo: OrderNoUpdate
        }).then(function (res) {
            $scope.GetListTypename = res.data
            console.log($scope.GetListTypename);
        });

    }
    $scope.GetDataUpdatefile = function (UploadLoadCustomer, SeasonUpdateName, StyleNameUpdate, OrderNoUpdate, TypenameUpdate) {

        $http.post(window.baseUrl + 'Home/Getdataupdate', {
            UpdateCodeName: UploadLoadCustomer,
            UpdateSeasonName: SeasonUpdateName,
            UpdateStyleName: StyleNameUpdate,
            UpdateOrderNo: OrderNoUpdate,
            UpdateTypename: TypenameUpdate
        }).then(function (res) {
            $scope.Listdataupdate = res.data
            console.log($scope.Listdataupdate);
        });
    }
    $scope.uploadUpdateFiles = function () {

        var files = document.getElementById('fileInputs').files;
        var formData = new FormData();




        if (files.length == 0) {
            Swal.fire({
                icon: 'error',
                title: '!!! No files uploaded.!!!',
                text: 'Please uploadfiles',
            });
        } else {


            $scope.ListDataAlready = {
                brandCode: $scope.UploadLoadCustomer, brandName: $scope.ListNameCustomer.BrandName, season: $scope.SeasonUpdateName, styleName: $scope.StyleNameUpdate,
                OrderNo: $scope.OrderNoUpdate, TypeName: $scope.TypeNameUpdate, NewFileName: files[0].name
            };


            $http.post(window.baseUrl + 'Home/checkfilealready', {
                DataAlready: $scope.ListDataAlready
            }).then(function (res) {
                $scope.listfile = res.data
                console.log($scope.listfile);

                if ($scope.listfile == '') {
                    for (var i = 0; i < files.length; i++) {
                        formData.append('files', files[i]);
                    }
                    formData.append('brandCode', $scope.UploadLoadCustomer);
                    formData.append('brandName', $scope.ListNameCustomer.BrandName);
                    formData.append('season', $scope.SeasonUpdateName);
                    formData.append('styleName', $scope.StyleNameUpdate);
                    formData.append('OrderNo', $scope.OrderNoUpdate);
                    formData.append('TypeName', $scope.TypeNameUpdate);
                    formData.append('Id', $scope.emplist.Id);

                    $http.post(window.baseUrl + 'Home/UploadUpdatesFiles', formData, {
                        headers: { 'Content-Type': undefined },
                        transformRequest: angular.identity
                    }).then(function (res) {
                        $scope.Listdataupdate = res.data
                        console.log($scope.Listdataupdate);

                        if ($scope.Listdataupdates == 'No files uploaded.') {
                            Swal.fire({
                                icon: 'error',
                                title: '!!! No files uploaded.!!!',
                                text: 'Please uploadfiles',
                            });
                        } else {
                            Swal.fire({
                                icon: 'success',
                                title: 'Uploadfile Success',
                                text: 'Uploadfile Success'
                            });
                        }
                    });
                }
                else {
                    Swal.fire({
                        icon: 'error',
                        title: '!!! Error Data !!!',
                        text: 'Please delete old data before revision',
                    });
                }
            });
        }
    };
    $scope.deldataupdate = function (deldata) {
        console.log(deldata);

        $http.post(window.baseUrl + 'Home/DeldataupdateToLog', {
            deldatas: deldata
        }).then(function (res) {
            $scope.Listdataupdate = res.data;

            console.log($scope.Listdataupdate);
        });
    }


    //MenuSearch 
    $scope.GetListCustomerSearch = function (CustomerSearch) {


        $http.post(window.baseUrl + 'Home/GetListCustomerSearchs', {
            CustomerSearchs: CustomerSearch
        }).then(function (res) {
            $scope.ListSearchData = res.data;

            $scope.ListBrandName = $scope.ListSearchData[0].BrandName;

            console.log($scope.ListSearchData);
            console.log($scope.ListBrandName);
        });

        $http.post(window.baseUrl + 'Home/GetSearchSeason', {
            CustomerSearchs: CustomerSearch
        }).then(function (res) {
            $scope.GetSearchListSeason = res.data
            console.log($scope.GetSearchListSeason);
        });





    }
    $scope.GetSearchStylesName = function (CustomerSearch, SeasonSearch) {




        $http.post(window.baseUrl + 'Home/ListSearchStylesName', {
            CustomerSearchs: CustomerSearch,
            SeasonSearchs: SeasonSearch
        }).then(function (res) {
            $scope.ListSearchData = res.data;

            console.log($scope.ListSearchData);
        });

        $http.post(window.baseUrl + 'Home/MenuSearchMasterStyle', {
            CustomerSearchs: CustomerSearch,
            SeasonSearchs: SeasonSearch
        }).then(function (res) {
            $scope.GetSearchListStylename = res.data;

            console.log($scope.GetSearchListStylename);
        });

    }
    $scope.GetSearchOrderNo = function (CustomerSearch, SeasonSearch, StyleNameSearch) {
        $http.post(window.baseUrl + 'Home/ListSearchOrderNo', {
            CustomerSearchs: CustomerSearch,
            SeasonSearchs: SeasonSearch,
            StyleNameSearchs: StyleNameSearch
        }).then(function (res) {
            $scope.ListSearchData = res.data;

            console.log($scope.ListSearchData);
        });

        $http.post(window.baseUrl + 'Home/MenuSearchOrderNo', {
            CustomerSearchs: CustomerSearch,
            SeasonSearchs: SeasonSearch,
            StyleNameSearchs: StyleNameSearch
        }).then(function (res) {
            $scope.GetSearchListOrderName = res.data;

            console.log($scope.GetSearchListOrderName);
        });

    }
    $scope.GetSearchTypeName = function (CustomerSearch, SeasonSearch, StyleNameSearch, OrderNoSearch) {
        $http.post(window.baseUrl + 'Home/ListSearchTypename', {
            CustomerSearchs: CustomerSearch,
            SeasonSearchs: SeasonSearch,
            StyleNameSearchs: StyleNameSearch,
            OrderNoSearchs: OrderNoSearch
        }).then(function (res) {
            $scope.ListSearchData = res.data;

            console.log($scope.ListSearchData);
        });


        $http.post(window.baseUrl + 'Home/MenuSearchTypeName', {
            CustomerSearchs: CustomerSearch,
            SeasonSearchs: SeasonSearch,
            StyleNameSearchs: StyleNameSearch,
            OrderNoSearchs: OrderNoSearch
        }).then(function (res) {
            $scope.GetSearchListTypename = res.data;

            console.log($scope.GetSearchListTypename);
        });
    }
    $scope.GetSearchdatafile = function (CustomerSearch, SeasonSearch, StyleNameSearch, OrderNoSearch, TypeNameSearch) {
        $http.post(window.baseUrl + 'Home/ListSearchdataupdate', {
            CustomerSearchs: CustomerSearch,
            SeasonSearchs: SeasonSearch,
            StyleNameSearchs: StyleNameSearch,
            OrderNoSearchs: OrderNoSearch,
            TypeNameSearchs: TypeNameSearch
        }).then(function (res) {
            $scope.ListSearchData = res.data;

            console.log($scope.ListSearchData);
        });
    }
    $scope.ShippingManualFAEmp = function (EmpNo) {
        $http.post(window.baseUrl + 'Home/GetEmployee',
            {
                EmployeeNo: EmpNo
            }).then(function (res) {
                $scope.emplist = res.data;

                console.log($scope.emplist);


                $scope.NewPro = [];
                $http.post(window.baseUrl + 'Home/GetCustomerNewProduct').then(function (res) {
                    $scope.NewPro = res.data;
                });

                $scope.complete = function (string) {
                    var output = [];
                    angular.forEach($scope.NewPro, function (CustomerData) {
                        if (CustomerData.toLowerCase().indexOf(string.toLowerCase()) >= 0) {
                            output.push(CustomerData);
                        }
                    });
                    $scope.filterCountry = output;
                };
                $scope.fillTextbox = function (string) {
                    $scope.CustomerData = string;
                    $scope.filterCountry = null;

                    $http.post(window.baseUrl + 'Home/GetCustomerData',
                        {
                            CustomerDatas: $scope.CustomerData

                        }).then(function (res) {
                            $scope.AllSaveDatas = res.data
                            console.log($scope.AllSaveDatas);

                            $scope.ShowShip = 99;
                            $scope.ShowMeaAndRemark = 55;

                            $http.post(window.baseUrl + 'Home/GetManualall',
                                {
                                    InvoiceNo: $scope.AllSaveDatas[0].PLNo
                                }).then(function (res) {

                                    $scope.MasterRemarks = res.data.ShippingSaveRemarkData

                                    $scope.AllMeasure = res.data.ShippingSaveMeasurementData

                                    $scope.SaveShipBill = res.data.ShippingManualBillData


                                    console.log(res.data);

                                });
                        });
                };
            });
    };


    $scope.GetQuoTation = function (EmpNos) {
        console.log(EmpNos);
        window.location.href = window.baseUrl + "Home/NdsSystemQuotation?EmpNo=" + EmpNos
    };

    $scope.Getdataindex = function (EmpNo) {

        console.log(EmpNo);


        $http.post(window.baseUrl + 'Home/GetEmployee',
            {
                EmployeeNo: EmpNo
            }).then(function (res) {
                $scope.emplist = res.data;
                console.log($scope.emplist);
            });



        $http.post(window.baseUrl + 'Home/GetdataQuo')
            .then(function (response) {
                $scope.ListQuo = response.data; // เก็บผลลัพธ์จาก Backend

                console.log($scope.ListQuo);


                $scope.data = [];
                $http.get(window.baseUrl + 'Home/GetdataquoNew')
                    .then(function (response) {
                        $scope.data = response.data.data;

                        setTimeout(function () {
                            $('#dataTable').DataTable();
                        }, 0);
                    });

                /*              $scope.initializeDataTable(); // เรียกใช้ DataTable หลังจากข้อมูลพร้อม*/
            })
            .catch(function (error) {
                console.error("Error:", error);
            });
    }

    // ฟังก์ชันสร้าง DataTable
    $scope.initializeDataTable = function () {
        $('#quotationTable').DataTable({
            destroy: true, // ลบ DataTable เดิมก่อน
            data: $scope.ListQuo, // ใช้ข้อมูลจาก $scope.ListQuo
            datalist: $scope.emplist,
            columns: [
                { data: 'QuotationNumber', className: 'text-center' },
                { data: 'QuoType', className: 'text-center' },
                { data: 'CustomerName', className: 'text-center' },
                { data: 'QuoLastname', className: 'text-center' },
                {
                    data: 'CreateDate',
                    className: 'text-center',
                    render: function (data) {
                        return new Date(data).toLocaleDateString('th-TH');
                    }
                },
                {
                    data: 'QuoStatus',
                    className: 'text-center',
                    render: function (data) {
                        return data === 1 ? 'Confirmed' : '';
                    }
                },
                {
                    data: null,
                    className: 'text-center',
                    render: function (data, type, row, datalist) {
                        if (row.QuoStatus === 0) {
                            return `<button class="btn btn-primary edit-button" data-quotation-number="${data.QuotationNumber}">Edit</button>`;
                        } else {
                            return `<button class="btn btn-secondary edit-button" data-quotation-number="${data.QuotationNumber}" disabled>Edit</button>`;
                        }
                    }
                },
                {
                    data: null,
                    className: 'text-center',
                    render: function (data) {
                        return `<button class="btn btn-success pdf-button" data-quotation-number="${data.QuotationNumber}">PDF</button>`;
                    }
                },
                {
                    data: null,
                    className: 'text-center',
                    render: function (data) {
                        return `<button class="btn btn-info view-button" data-quotation-number="${data.QuotationNumber}">View</button>`;
                    }
                }
            ],
            order: [[0, 'desc']] // เรียงลำดับ Quotation Number
        });

        // จัดการ Event Buttons
        $('#quotationTable').on('click', '.edit-button', function () {
            //const id = $(this).data('quotationNumber');
            //const IdCreate = $(this).datalist('Id');
            //$scope.editQuo(id, IdCreate);
            const id = $(this).data('quotationNumber');
            const IdCreate = $(this).data('createBy');
            $scope.editQuo(id, IdCreate);
        });

        $('#quotationTable').on('click', '.pdf-button', function () {
            const id = $(this).data('QuotationNumber');
            $scope.PrintPDF(id);
        });

        $('#quotationTable').on('click', '.view-button', function () {
            const id = $(this).data('QuotationNumber');
            $scope.viewQuotation(id);
        });
    };














    // ฟังก์ชัน Edit เพื่อเปลี่ยนเส้นทางไปยังหน้าที่ต้องการแก้ไข
    $scope.editQuo = function (QuoNo, EmpNos) {
        // ไปยังหน้าแก้ไขข้อมูล โดยใช้ quotationNumber เป็นพารามิเตอร์
        console.log(QuoNo);
        console.log(EmpNos);


        //$http.post(window.baseUrl + 'Home/NdsSystemEditQuotation', {
        //    QuotationNumber: QuoNo,
        //    EmpNo: EmpNos
        //})
        //    .then(function (response) {
        //        $scope.data = response.data;
        //        console.log($scope.data);
        //    });

        $window.location.href = 'NdsSystemEditQuotation?EmpNo=' + EmpNos + '&QuotationNumber=' + QuoNo;
    }


    $scope.GetdataQuoForEdits = function (EmpNo, QuoNum) {
        console.log(QuoNum);
        console.log(EmpNo);


        $scope.NewEntry = {
            TotalPrice: 0,
            Quantity: 0,
            PricePerUnit: 0
        };


        $http.post(window.baseUrl + 'Home/GetEmployee',
            {
                EmployeeNo: EmpNo
            }).then(function (res) {
                $scope.emplist = res.data;
                console.log($scope.emplist);
            });


        $scope.GetPageLoad(EmpNo);

        $http.post(window.baseUrl + 'Home/GetdataQuoForEdit', {
            QuotationNumber: QuoNum,
            EmpNos: EmpNo
        }).then(function (response) {


            console.log(response.data);


            //const shipDate = response.data.shipDate
            //    ? formatDateToDDMMYYYY(new Date(response.data.shipDate))
            //    : '';
            /* $scope.SelectedProvinces = response.data.provinces;*/

            $scope.QuoData = {
                QuoNumber: response.data.QuotationNumber,
                CustomerName: response.data.CustomerName,
                /* CustFirstname: response.data.custFirstname,*/
                /* CustLastname: response.data.custLastname,*/
                CompanyName: response.data.QuoCompanyName,
                OrderDate: new Date().toISOString().slice(0, 10), // รูปแบบ yyyy-MM-dd
                OrderStatus: '', //กำหนดให้เป็น Processing

                TotalQty: response.data.TotalQty,
                TotalPrice: response.data.TotalPrice,
                CustomerEmail: response.data.CustomerEmail,
                CustomerAddress: response.data.CustomerAddress,
                CustomerTaxID: response.data.CustomerTaxID,
                CustomerAddressTax: response.data.CustomerAddressTax,
                CustomerPhone: response.data.CustomerPhone,
                /* Remark: response.data.remark,*/
                CreateBy: EmpNo,// ***
                CreateDate: new Date().toISOString().slice(0, 10),// รูปแบบ dd/MM/yyyy HH:mm:ss
                StyleName: response.data.StyleName,// ไม่มี MAP
                QuoType: response.data.QuoType,
                QuoLastname: response.data.QuoLastname,
                // QuoCompanyName: response.data.quoCompanyName,
                //QuoProvince: response.data.quoProvince,
                //QuoDistricts: response.data.quoDistricts,
                //QuoSubDistricts: response.data.quoSubDistricts,
                //QuoZipCode: response.data.quoZipCode,

                Remark: response.data.QuoRemark,
                QuoTaxID: response.data.QuoTaxID,
                QuoShippingPrice: response.data.QuoShippingPrice,
                QuoStatus: response.data.QuoStatus, // เพิ่ม QuoStatus
                ShipDate: response.data.ShipDate  // เพิ่ม ShipDate


            };


            console.log($scope.QuoData);


            $scope.selectedShipDate = $scope.QuoData.ShipDate.split('T')[0];

            $scope.isConfirmed = $scope.QuoData.QuoStatus.toString() === '1';
            $scope.selectedShipDate = $scope.isConfirmed ? $scope.QuoData.ShipDate : '';

            $scope.selectedShipDate = new Date($scope.QuoData.ShipDate).toLocaleDateString('en-GB');

            console.log($scope.selectedShipDate);

            $scope.SelectedTypeSell = $scope.QuoData.QuoType;
            console.log($scope.QuoData.QuoType);

            $scope.SelectedProvinces = response.data.QuoProvince;


            console.log(response.data.QuoProvince);
            $scope.GetListDistricts(response.data.QuoProvince);

            $scope.SelectedDistricts = response.data.QuoDistricts;
            $scope.GetListSub(response.data.QuoDistricts, response.data.QuoProvince)
            $scope.SelectedSub = response.data.QuoSubDistricts;
            $scope.SZipcode = response.data.QuoZipCode;



            $http.post(window.baseUrl + 'Home/GetForEditProduct', {
                QuotationNumber: $scope.QuoData.QuoNumber
            }).then(function (response) {
                console.log(response.data);

                // ตรวจสอบว่า response.data เป็น Array หรือไม่
                if (Array.isArray(response.data)) {
                    $scope.Entries = response.data.map(entry => ({
                        SelectedStyleName: entry.ProductName,
                        SelectedSku: entry.SKUCodeFull,
                        SelectedSize: entry.Size,
                        SelectedColor: entry.Color,
                        Quantity: entry.Qty,
                        PricePerUnit: entry.Price,
                        TotalPrice: entry.Qty * entry.Price
                    }));
                } else {
                    $scope.Entries = [];
                }

                $scope.CalculateTotalSum();
                $scope.CalculateQty();
            });
            $scope.GetLoadFiles(); //Load file

        })
    }

    $scope.GetDataPageLoad = function (EmpNo) {

 
        $scope.NewEntry = {
            TotalPrice: 0,
            Quantity: 0,
            PricePerUnit : 0
        };
        console.log($scope.NewEntry.TotalPrice);
     /*   $scope.NewEntry.TotalPrice = 0;*/

        console.log(EmpNo);
        $scope.GetSku(); // โหลด Style Name
        $scope.GetColors(); // โหลด Color
        $scope.GetSizes();     // โหลด Sizes
        $scope.GetProvince(); // โหลด จังหวัด
        $scope.GetOrderType(); // โหลด Type order
        $scope.GetLoadRemark(); // โหลด Master remark


    };


    $scope.GetPageLoad = function (EmpNo) {

        console.log(EmpNo);
        $scope.GetSku(); // โหลด Style Name
        $scope.GetColors(); // โหลด Color
        $scope.GetSizes();     // โหลด Sizes
        $scope.GetProvince(); // โหลด จังหวัด
        $scope.GetOrderType(); // โหลด Type order
        $scope.GetLoadRemark(); // โหลด Master remark


    };


    /*EXEC gnerateQuotationNumber*/
    $scope.GetSku = function () {
        $http.get(window.baseUrl + 'Home/GetSku')
            .then(function (response) {
                $scope.ListDropSku = response.data;
                console.log("Sku Number:", response.data);
            });
    };

    $scope.GetSkuCode = function (styleName) {
        console.log("Selected StyleName:", styleName);
        $http.post(window.baseUrl + 'Home/GetSkuCode', {
            StyleCode: styleName
        })
            .then(function (response) {
                $scope.skuCode = response.data;
            })
            .catch(function (error) {
                console.error("Error fetching SKU Codes:", error);
            });
    };

    /* ListCompany*/

    /* $scope.GetPageLoad()*/ //เรียกใช้ฟังก์ชัน - > มีการเรียกจากหน้า ng-init="GetPageLoad()"
    $scope.GetColors = function () {
        $http.get(window.baseUrl + 'Home/GetColorss')
            .then(function (response) {
                $scope.ListColors = response.data;
                console.log("Colors:", response.data);
            });
    };
    $scope.GetSizes = function () {
        $http.get(window.baseUrl + 'Home/GetSizes')
            .then(function (response) {
                $scope.ListSizes = response.data;
                console.log("Colors:", response.data);
            });
    };

    $scope.GetOrderType = function () {


        $http.get(window.baseUrl + 'Home/GetOrderType')
            .then(function (response) {
                $scope.ListTypeSell = response.data;
                console.log("OrderTypes:", response.data);
            });

    }

    $scope.GetProvince = function () {
        $http.get(window.baseUrl + 'Home/GetProvinces')
            .then(function (response) {
                $scope.ListProvinces = response.data;
            });
    }

    $scope.GetListDistricts = function (Provincess) {

        // ส่งข้อมูลไปยัง Backend
        $http.post(window.baseUrl + 'Home/GetDistricts',
            {
                Provinces: Provincess
            })
            .then(function (response) {
                $scope.ListDistricts = response.data;
            });
    }

    $scope.GetListSub = function (SelectedDistricts, SelectedProvinces) {
        //Get SubDist Where จังหวัด, อำเภอ
        console.log(SelectedDistricts, SelectedProvinces);
        // ส่งข้อมูลไปยัง Backend
        $http.post(window.baseUrl + 'Home/GetListSubs',
            {
                Districts: SelectedDistricts,
                Provinces: SelectedProvinces
            })
            .then(function (response) {
                console.log("Response จาก Backend:", response);
                $scope.ListSub = response.data; // เก็บผลลัพธ์จาก Backend
                console.log("ListDistricts:", $scope.ListSub);
            })
            .catch(function (error) {
                console.error("Error:", error);
            });
    }

    $scope.GetListZipcode = function (SelectedSub, SelectedDistricts) {

        console.log(SelectedSub, SelectedDistricts);
        // ส่งข้อมูลไปยัง Backend
        $http.post(window.baseUrl + 'Home/GetListZipcode',
            {

                Districts: SelectedDistricts,
                SubDistricts: SelectedSub
            })
            .then(function (response) {
                $scope.SZipcode = response.data; // เก็บผลลัพธ์จาก Backend

                console.log($scope.SZipcode);
            })
            .catch(function (error) {
                console.error("Error:", error);
            });
    }

    $scope.GetLoadRemark = function () {
        $http.get(window.baseUrl + 'Home/GetLoadRemark')
            .then(function (response) {
                $scope.Remarks = response.data; // เก็บข้อมูล Remark ใน Scope
            }, function (error) {
                console.error("Error loading remarks:", error);
            });
    };


    $scope.AddEntry = function () {
        if (!$scope.skuCode || !$scope.QuoData.StyleName || !$scope.NewEntry.SelectedSize || !$scope.NewEntry.SelectedColor || !$scope.NewEntry.Quantity || !$scope.NewEntry.PricePerUnit) {


            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: "Please fill in all information completely!"

            });
            return;
        }
        $scope.NewEntry.SelectedStyleName = $scope.QuoData.StyleName;
        $scope.NewEntry.SelectedSku = $scope.skuCode;

        if (!$scope.Entries) {
            $scope.Entries = []; // ตรวจสอบว่ากำหนดเป็น array หรือยัง
        }
        $scope.Entries.push(angular.copy($scope.NewEntry));
        //คำนวนใหม่
        $scope.CalculateTotalSum()
        $scope.CalculateQty(); // คำนวณยอดรวมของ Quantity
        $scope.NewEntry = {
            SelectedStyleName: '',
            SelectedSku: '',
            SelectedSize: '',
            SelectedColor: '',
            Quantity: 0,
            PricePerUnit: 0,
            TotalPrice: 0
        };
    };

    $scope.RemoveEntry = function (index) {

        if (index >= 0 && index < $scope.Entries.length) {
            $scope.Entries.splice(index, 1);
            console.log("After removal:", $scope.Entries);
            //คำนวนใหม่
            $scope.CalculateTotalSum()
            $scope.CalculateQty(); // คำนวณยอดรวมของ Quantity
        } else {
            alert('');
        }
    };

    $scope.TotalSum = 0;

    // ฟังก์ชันคำนวณยอดรวมทั้งหมด
    $scope.CalculateTotalSum = function () {

        //console.log("CalculateTotalSum Called");

        // เช็คว่าเป็น Array - ตั้งต้นเป็น Array ว่าง (ต้องเช็คเพราะว่าบางทีมันมองเป็น NULL)
        const entries = Array.isArray($scope.Entries) ? $scope.Entries : [];

        //console.log("Entries:", entries);

        // Entries
        const entryTotal = entries.reduce(function (sum, entry) {
            return sum + (entry.Quantity * entry.PricePerUnit);
        }, 0);

        //console.log("Entry Total:", entryTotal);

        // เช็ค แปลง QuoShippingPrice เป็น Number
        const shippingPrice = isNaN(parseFloat($scope.QuoData.QuoShippingPrice)) ? 0 : parseFloat($scope.QuoData.QuoShippingPrice);
        //console.log("Parsed Shipping Price:", shippingPrice);

        // Sum and update TotalPrice
        $scope.QuoData.TotalPrice = entryTotal + shippingPrice;
        /*console.log("Total Price Updated:", $scope.QuoData.TotalPrice);*/

    };

    $scope.CalculateQty = function () {
        $scope.QuoData.TotalQty = $scope.Entries.reduce(function (sum, entry) {
            return sum + entry.Quantity;
        }, 0);
    };



    //$scope.QuoData.TotalPrice = $scope.TotalSum;
    //$scope.NewQuoNumber = [];



    //GenQuotationNumber
    $scope.GenerateQuotationNumber = function () {
        $http.get(window.baseUrl + 'Home/GenerateQuotationNumber')
            .then(function (response) {
                $scope.NewQuoNumber = response.data;
                console.log("QuotationNumber:", response.data);
            });

    }



    // File

    $scope.UploadFile = function () {
        var fileInput = document.getElementById('fileInput').files[0];

        if (!fileInput) {
            Swal.fire({
                icon: "warning",
                title: "No file selected",
                text: "Please select a file before uploading."
            });
            return;
        }

        // ตรวจสอบและตั้งค่า fileDescription เป็นค่าว่างถ้าไม่มีการกรอก
        if (!$scope.fileDescription || $scope.fileDescription.trim() === "") {
            $scope.fileDescription = "";
        }

        var formData = new FormData();
        formData.append("files", fileInput);
        formData.append("fileDescription", $scope.fileDescription);
        formData.append("quotationNumber", $scope.QuoData.QuoNumber); // รับ QuotationNumber จาก Model

        $http.post(window.baseUrl + 'Home/UploadFiledata', formData, {
            headers: { 'Content-Type': undefined }
        }).then(function (response) {



            console.log(response);
            // ใช้ $scope.$apply เพื่อกระตุ้น AngularJS ให้จับการเปลี่ยนแปลง
            $scope.$applyAsync(function () {
                $scope.files.push(response.data); // เพิ่มไฟล์ใหม่ลงในตาราง
            });

            Swal.fire({
                icon: "success",
                title: "File uploaded",
                text: "The file has been uploaded successfully."
            });


            $scope.fileDescription = ""; // ล้างคำอธิบาย
            document.getElementById('fileInput').value = null; // ล้างไฟล์ที่เลือก
        }).catch(function (error) {
            console.error("Error uploading file:", error);
            Swal.fire({
                icon: "error",
                title: "Upload Failed",
                text: "An error occurred while uploading the file."
            });
        });
    };


    $scope.downloadFile = function (filePath) {

        console.log(filePath);
        window.open(window.baseUrl + 'Home/DownloadFile?filePath=' + encodeURIComponent(filePath), '_blank');
    };

    $scope.deleteFile = function (filePaths, index, DelId) {
        console.log(index);

        Swal.fire({
            title: "Are you sure?",
            text: "This file will be permanently deleted!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Yes, delete it!",
            cancelButtonText: "Cancel"
        }).then((result) => {
            if (result.isConfirmed) {
                $http.post(window.baseUrl + 'Home/DeleteFile', {
                    filePath: filePaths,
                    quotationNumber: $scope.QuoData.QuoNumber,
                    DelIds: DelId
                }).then(function (response) {
                    Swal.fire({
                        icon: "success",
                        title: "Deleted",
                        text: response.data.Message
                    });
                    $scope.files.splice(index, 1); // ลบไฟล์ออกจากตาราง
                }).catch(function (error) {
                    console.error("Error deleting file:", error);
                    Swal.fire({
                        icon: "error",
                        title: "Delete Failed",
                        text: "An error occurred while deleting the file."
                    });
                });
            }
        });
    };

    $scope.GetLoadFiles = function () {
        // ตรวจสอบว่ามี Quotation Number หรือไม่
        console.log($scope.QuoData.QuoNumber + " Quo")
        if (!$scope.QuoData.QuoNumber) {
            console.warn("Quotation number is required.");
            return;
        }

        // เรียก API ดึงข้อมูลไฟล์
        $http.get(window.baseUrl + 'Home/GetQuotationFiles', {
            params: { quotationNumber: $scope.QuoData.QuoNumber }

        }).then(function (response) {
            $scope.files = response.data;
        }, function (error) {
            console.error("Error loading files:", error);
        });
    };


    $scope.updateShipmentDate = function () {
        if ($scope.isConfirmed) {
            // กรณี Confirm เป็นจริง ให้ตั้งค่า ShipDate
            $scope.selectedShipDate = $scope.QuoData.ShipDate || '';
        } else {
            // กรณีไม่ Confirm ให้ลบค่า ShipDate
            $scope.clearShipmentDate();
        }
    };

    $scope.clearShipmentDate = function () {
        $scope.selectedShipDate = ''; // เคลียร์ค่าของ ShipDate
    };

    $scope.$watch('isConfirmed', function (newValue, oldValue) {
        if (newValue !== oldValue) {
            $scope.updateShipmentDate();
        }
    });

    $scope.viewQuotation = function (quotationNumber) {
        // ดึงข้อมูล Quotation โดยใช้ API

    };

    $scope.deleteQuotation = function (quotationNumber) {
        Swal.fire({
            title: 'Are you sure?',
            text: 'Do you want to delete this Quotation?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Yes, delete it!',
            cancelButtonText: 'No, cancel!'
        }).then(function (result) {
            if (result.isConfirmed) {
                $http.post('/Home/DeleteQuo', { QuotationNumber: quotationNumber }).then(function () {
                    Swal.fire('Deleted!', 'Your Quotation has been deleted.', 'success');
                    /*   $scope.loadQuotations();*/ // โหลดข้อมูลใหม่
                }, function (error) {
                    Swal.fire('Error', 'Failed to delete quotation.', 'error');
                });
            }
        });
    };

    // Function Update

    $scope.UpdateQuotation = function (EmpNo, selectedShipDate) {
        console.log(selectedShipDate);


        if (!$scope.QuoData.QuoNumber || !$scope.Entries.length) {
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: "Please fill in all required fields!"
            });
            return;
        }


        var quoStatus = $scope.isConfirmed ? 1 : 0; // 1 for confirmed, 0 otherwise
        /*  var shipDate = $scope.isConfirmed ? $scope.selectedShipDate : new Date().toISOString().slice(0, 10);*/ // Use selected date or default to today
        var shipDate = $scope.isConfirmed
            ? new Date($scope.selectedShipDate.split('/').reverse().join('-')) // แปลงจาก dd/mm/yyyy เป็น Date Object
            : new Date(); // กรณีไม่ได้เลือกวันใช้วันปัจจุบัน


        console.log(shipDate);

        /*     const*/

        $scope.updateData = {
            QuotationNumber: $scope.QuoData.QuoNumber,
            QuoType: $scope.SelectedTypeSell,
            CustomerName: $scope.QuoData.CustomerName,
            QuoLastname: $scope.QuoData.QuoLastname,
            QuoCompanyName: $scope.QuoData.CompanyName,
            OrderDate: $scope.QuoData.OrderDate,
            ShipDate: shipDate,
            TotalQty: $scope.QuoData.TotalQty,
            TotalPrice: $scope.QuoData.TotalPrice,
            Remark: $scope.QuoData.Remark,
            CustomerAddress: $scope.QuoData.CustomerAddress,
            CustomerPhone: $scope.QuoData.CustomerPhone,
            QuoTaxID: $scope.QuoData.QuoTaxID,
            CustomerEmail: $scope.QuoData.CustomerEmail,
            QuoProvince: $scope.SelectedProvinces,
            QuoDistricts: $scope.SelectedDistricts,
            QuoSubDistricts: $scope.SelectedSub,
            QuoZipCode: $scope.SZipcode,
            QuoShippingPrice: $scope.QuoData.QuoShippingPrice,
            QuoStatus: quoStatus,
            Entries: $scope.Entries.map(entry => ({
                ProductName: entry.SelectedStyleName,
                SKUCodeFull: entry.SelectedSku,
                Sku: entry.SelectedSku, // แก้ไขให้ส่งค่าที่ถูกต้อง
                Qty: entry.Quantity,
                Size: entry.SelectedSize,
                Color: entry.SelectedColor,
                Price: entry.PricePerUnit
            }))
        };
        console.log($scope.updateData);


        //// Send update request

        $http.post(window.baseUrl + 'Home/UpdateQuotations',
            {
                updateModel: $scope.updateData,
                EmpNos: EmpNo
            })
            .then(function (response) {
                $scope.data = response.data; // เก็บข้อมูล Remark ใน Scope

                Swal.fire({
                    icon: "success",
                    title: "Update Complete",
                    text: "Quotation updated successfully."
                }).then(function () {
                    $window.location.href = 'NdsSystemQuotation?EmpNo=' + EmpNo;
                });

                console.log($scope.data);
            }, function (error) {
                console.error("Error loading remarks:", error);
            });
        //$http.post(window.baseUrl + 'Home/UpdateQuotations', $scope.updateData)
        //    .then(function (response) {
        //        Swal.fire({
        //            icon: "success",
        //            title: "Update Complete",
        //            text: "Quotation updated successfully."
        //        }).then(function () {
        //            /*                    $window.location.href = "Home/NdsSystemEditQuotation";*/

        //            $window.location.href = 'NdsSystemEditQuotation?QuotationNumber=' + QuoNo + '&EmpNo=' + EmpNos;
        //        });
        //    })
        //    .catch(function (error) {
        //        console.error("Update Error:", error);
        //        Swal.fire({
        //            icon: "error",
        //            title: "Error",
        //            text: "Failed to update quotation."
        //        });
        //    });
    };


    $scope.Cancel = function (EmpNo) {
        Swal.fire({
            title: 'Are you sure?',
            text: "Changes will not be saved!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, cancel!',
            cancelButtonText: 'No, stay here'
        }).then((result) => {
            if (result.isConfirmed) {
                $window.location.href = 'NdsSystemQuotation?EmpNo=' + EmpNo;
            }
        });
    };


    $scope.CreateQuo = function (EmpNo) {
        // Redirect to CreateQuo View
        $window.location.href = 'NdsSystemCreateQuotation?EmpNo=' + EmpNo;
    };



    $scope.PrintPDF = function (NumberQuo) {
        // รับค่า QuotationNumber จากข้อมูลที่กรอกโดยผู้ใช้
        console.log(NumberQuo);
        var quotationNumber = NumberQuo;
        /* var quotationNumber = $scope.QuoData.QuoNumber;*/

        // ตรวจสอบว่ามี QuotationNumber
        if (!quotationNumber) {
            alert("กรุณากรอกหมายเลขใบเสนอราคา");
            return;
        }

        // เรียกใช้ API เพื่อสร้าง PDF
        var url = '/Home/PrintPDF?quotationNumber=' + encodeURIComponent(quotationNumber);

        // เปิดแท็บใหม่เพื่อดาวน์โหลดหรือแสดงไฟล์ PDF
        window.open(url, '_blank');
    };



    $scope.viewQuotation = function (quotationNumber, EmpNos) {

        $window.location.href = '/Home/NdsSystemViewPage?EmpNo=' + EmpNos + '&quotationNumber=' + quotationNumber;

    };

    $scope.backHome = function (EmpNo) {
        $window.location.href = 'NdsSystemQuotation?EmpNo=' + EmpNo;
    };


    // Order Information

    $scope.GetOrderInfo = function (EmpNo) {
        //$http.get(window.baseUrl + "Home/GetOrderInfos")
        //    .then(function (response) {
        //        $scope.dataOrders = response.data.data; // Bind data to scope
        //        console.log($scope.dataOrders);
        //        initializeDataTable(); // เรียก DataTable หลังโหลดข้อมูล
        //    }, function (error) {
        //        console.error("Error fetching order information: ", error);
        //    });

        $http.post(window.baseUrl + 'Home/GetEmployee',
            {
                EmployeeNo: EmpNo
            }).then(function (res) {
                $scope.emplist = res.data;

            });



        $scope.dataOrders = [];
        $http.get(window.baseUrl + 'Home/GetOrderInfos')
            .then(function (response) {
                $scope.dataOrders = response.data.data;


                console.log($scope.dataOrders);
                setTimeout(function () {
                    $('#dataTables').DataTable();
                }, 0);
            });
    };

    $scope.GetOrderInformation = function (EmpNo) {
        console.log(EmpNo);

        $window.location.href = 'NdsSystemOrderInformation?EmpNo=' + EmpNo;
    }

    $scope.formatDate = function (netDate) {
        // ดึงเฉพาะตัวเลขจาก /Date(...)/ และแปลงเป็น timestamp
        var timestamp = parseInt(netDate.replace(/\/Date\((\d+)\)\//, '$1'));
        var date = new Date(timestamp);

        // คืนค่ารูปแบบวันที่ในแบบที่ต้องการ
        /*    return date.toLocaleDateString(); // เช่น "22/1/2025" (ขึ้นอยู่กับโลเคล)*/

        // แปลงรูปแบบวันที่เป็น dd/MM/yyyy
        var day = date.getDate().toString().padStart(2, '0'); // เติม 0 ข้างหน้าให้ครบ 2 หลัก
        var month = (date.getMonth() + 1).toString().padStart(2, '0'); // เดือน +1 และเติม 0
        var year = date.getFullYear();

        return `${day}/${month}/${year}`; // คืนค่าวันที่ในรูปแบบ dd/MM/yyyy
    };


    $scope.openModal = function (orderNumber) {
        // เรียกข้อมูลจากเซิร์ฟเวอร์เพื่อแสดงใน ModalPopup
        $http.get(window.baseUrl + `Home/GetOrderDetails?orderNumber=${orderNumber}`)
            .then(function (response) {
                $scope.modalData = response.data;

                console.log($scope.modalData);

                $scope.GetProductOrder($scope.modalData.OrderNumber);
                // เก็บข้อมูลใน $scope.modalData
                $('#OrderDetails').modal('show'); // เปิด ModalPopup
            }, function (error) {
                console.error("Error fetching order details: ", error);
            });


    };

    $scope.GetProductOrder = function (orderNumber) {

        console.log(orderNumber);


        $http.get(window.baseUrl + `Home/GetProductOrders?orderNumber=${orderNumber}`)


            .then(function (response) {
                $scope.productOrders = response.data; // เก็บข้อมูลใน scope
                console.log($scope.productOrders);

            }, function (error) {
                console.error("Error fetching product orders: ", error);
            });
    };

    $scope.GetPageFile = function (EmpNo, orderNumber) {
        // ดึงค่าจาก URL Query String
        const urlParams = new URLSearchParams(window.location.search);
        /*      const orderNumber = urlParams.get('orderNumber'); // ดึงค่าของ orderNumber*/
        $scope.orderNumber = orderNumber;
        if (!orderNumber) {
            console.error("Order number is missing in the URL.");
            return;
        }

        $scope.GetDataQuoFileTable(orderNumber);
    };


    $scope.GetDataQuoFileTable = function (orderNumber) {


        $http.post(window.baseUrl + 'Home/GetDataQuoFileTables',
            {
                orderNumbers: orderNumber
            })
            .then(function (response) {
                $scope.quoFile = response.data;
                if (Array.isArray($scope.quoFile) && $scope.quoFile.length > 0) {
                    var quotationNumber = $scope.quoFile[0].quotationNumber;
                    $scope.quotationNumber = quotationNumber;
                } else {
                    $scope.quotationNumber = "";
                }
                console.log($scope.quoFile);
            })
            .catch(function (error) {
                console.error("Error:", error);
            });

        $http.post(window.baseUrl + 'Home/GetDataOtherFileTable', {

            orderNumbers: orderNumber
        })
            .then(function (response) {
                $scope.files = response.data; // เก็บข้อมูลใน scope
                console.log($scope.files);


            }, function (error) {
                console.error("Error OtherFile : ", error);
            });

    };


    $scope.downloadQuotation = function (quotationNumber) {
        if (!quotationNumber) {
            Swal.fire({
                icon: "error",
                title: "Error",
                text: "Quotation Number is missing!"
            });
            return;
        }
        const url = '/PDF/PrintPDF?quotationNumber=' + encodeURIComponent(quotationNumber);
        window.open(url, '_blank');
    };

    // Upload File AboutOrder -ดูตารางที่ save file ของ Order
    $scope.UploadFileAboutOrder = function () {

        var fileInput = document.getElementById("fileInput").files[0];
        if (!fileInput) {
            Swal.fire({
                icon: "warning",
                title: "No file selected",
                text: "Please select a file before uploading."
            });
            return;
        }

        if (!$scope.fileDescription || $scope.fileDescription.trim() === "") {
            $scope.fileDescription = "";
        }




        var formData = new FormData();
        formData.append("file", fileInput);
        formData.append("fileDescription", $scope.fileDescription || "");
        formData.append("orderNumber", $scope.orderNumber);

        $http.post(window.baseUrl + "Home/UploadFileAboutOrders", formData, {
            headers: { "Content-Type": undefined }
        }).then(function (response) {


            $http.post(window.baseUrl + 'Home/GetDataOtherFileTable',
                {
                    orderNumbers: response.data.data.OrderNumber
                })
                .then(function (response) {
                    $scope.files = response.data;
                    console.log($scope.files);
                });


            Swal.fire({
                icon: "success",
                title: "File uploaded",
                text: "The file has been uploaded successfully."
            });


            $scope.fileDescription = ""; // ล้างคำอธิบาย
            document.getElementById('fileInput').value = null; // ล้างไฟล์ที่เลือก


        }).catch(function () {
            Swal.fire({
                icon: "error",
                title: "Upload Failed",
                text: "An error occurred while uploading the file."
            });
        });
    };



    $scope.deleteFileAboutOrder = function (filePath, index, fileId) {
        Swal.fire({
            title: "Are you sure?",
            text: "This file will be permanently deleted!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Yes, delete it!",
            cancelButtonText: "Cancel"
        }).then((result) => {
            if (result.isConfirmed) {
                $http.post(window.baseUrl + 'Home/DeleteFileAboutOrders', {
                    filePath: filePath,
                    orderNumber: $scope.orderNumber,
                    FilesId: fileId
                }).then(function (response) {
                    Swal.fire({
                        icon: "success",
                        title: "Deleted",
                        text: response.data.Message
                    });
                    $scope.files.splice(index, 1); // ลบไฟล์ออกจากตาราง
                }).catch(function (error) {
                    console.error("Error deleting file:", error);
                    Swal.fire({
                        icon: "error",
                        title: "Delete Failed",
                        text: "An error occurred while deleting the file."
                    });
                });
            }
        });
    };


    $scope.GetFilesData = function (orderNumber, EmpNo) {
        $window.location.href = 'NdsSystemViewAttachments?orderNumber=' + orderNumber + '&EmpNo=' + EmpNo;
    };


    $scope.backHomeOrder = function (EmpNo) {
        $window.location.href = 'NdsSystemOrderInformation?EmpNo=' + EmpNo;
    };






    ///RFID Menu


    $scope.RFIDGETDATA = function (EmpNo) {
        console.log(EmpNo);


        $http.post(window.baseUrl + 'Home/GetEmployee',
            {
                EmployeeNo: EmpNo
            }).then(function (res) {
                $scope.emplist = res.data;
                console.log($scope.emplist);

                $scope.getTags();

                intervalPromise = $interval(function () {
                    $scope.getTags(); // ดึงข้อมูล RFID
                }, 1000);
            });

    };

    $scope.RFIDUrlInfor = function (EmpNo) {
        $window.location.href = 'RFIDIndex?EmpNo=' + EmpNo;
    };

    $scope.NdssystemLink = function (EmpNo) {
        $window.location.href = 'YMTNdsSystem?EmpNo=' + EmpNo;
    };

    $scope.tags = [];

    let intervalPromise;

    $scope.StartCheck = 1;
    $scope.EPCID = ''; // Default EPCID value
    $scope.ActiveId = 1; // Default Active status
    $scope.products = []; // Array to hold product data

    $scope.getTags = function () {
        $http.post(window.baseUrl + 'Home/GetTags').then(function (response) {
            $scope.products = response.data;



            $scope.groupedProducts = (function () {
                let groupedCache = null; // แคชข้อมูลที่ประมวลผลแล้ว
                let lastProductList = []; // เก็บสถานะของ $scope.products ล่าสุด

                return function () {
                    // ตรวจสอบว่าข้อมูลเปลี่ยนหรือไม่
                    if (angular.equals(lastProductList, $scope.products)) {
                        return groupedCache; // ถ้าข้อมูลไม่เปลี่ยน คืนค่าแคช
                    }

                    // อัปเดตสถานะใหม่
                    lastProductList = angular.copy($scope.products);

                    // คำนวณใหม่
                    let grouped = {};
                    $scope.products.forEach(product => {
                        let groupKey = product.ProductName + " - " + product.Size;

                        if (!grouped[groupKey]) {
                            grouped[groupKey] = {
                                ProductName: product.ProductName,
                                Size: product.Size,
                                TotalPrice: 0,
                                TotalQuantity: 0
                            };
                        }

                        grouped[groupKey].Prices = product.UnitPrice;
                        grouped[groupKey].TotalPrice += product.UnitPrice;
                        grouped[groupKey].TotalQuantity += 1; // แก้ไขจำนวนที่เพิ่ม
                    });

                    groupedCache = Object.values(grouped); // แคชข้อมูลใหม่
                    return groupedCache;
                };
            })();

            // คำนวณ Total Amount และ Total Quantity แบบลดความซ้ำซ้อน
            $scope.calculateTotals = function () {
                if (!$scope.products || !$scope.products.length) {
                    $scope.TotalsData = 0;
                    $scope.totalQtys = 0;
                    return;
                }

                // ใช้ reduce เพื่อรวมข้อมูล
                let totals = $scope.products.reduce((acc, product) => {
                    acc.totalAmount += product.UnitPrice;
                    acc.totalQty += 1; // นับจำนวนสินค้า
                    return acc;
                }, { totalAmount: 0, totalQty: 0 });

                $scope.TotalsData = totals.totalAmount;
                $scope.totalQtys = totals.totalQty;
            };

            // เรียกใช้คำนวณครั้งแรก
            $scope.calculateTotals();


        });

    };

    $scope.ClickStart = function () {
        $http.post(window.baseUrl + 'Home/StartReading').then(function (response) {
            $scope.getTags();
        });
        $scope.StartCheck = 2;
    };


    $scope.startReading = function () {
        $http.post(window.baseUrl + 'Home/StartReading').then(function (response) {
            $scope.getTags();
        });
    };

    $scope.ReStartReading = function () {
        $http.post(window.baseUrl + 'Home/ReStartReading').then(function (response) {
            $scope.getTags();
        });
    };


    $scope.stopReading = function () {
        $http.post(window.baseUrl + 'Home/StopReading').then(function () {
            $scope.StartCheck = 1;
        });
    };







    ///RFID Menu



    $scope.POSSystemData = function (EmpNo) {

        $window.location.href = 'POSSystem?EmpNo=' + EmpNo;
    }






    $scope.SaveQuotation = function (QuoData, SelectedProvinces, SelectedDistricts,
        SelectedSub, SZipcode, skuCode, SelectedTypeSell, Entries, EmpNo) {
        // Validate ค่าว่าง


        console.log(SelectedTypeSell);
        console.log(SelectedDistricts);
        console.log(QuoData);

        console.log(Entries);

        if (SelectedTypeSell == undefined) {
            Swal.fire({
                icon: "error",
                title: "Validation Error",
                text: "กรุณาระบุข้อมูล Quotation Type"
            });
        }
        else if (QuoData == undefined)
        {
                Swal.fire({
                    icon: "error",
                    title: "Validation Error",
                    text: "กรุณาระบุข้อมูล Customer Name หรือ Company Name"
                });
        }
        else if (QuoData.CustomerEmail == undefined) {
            Swal.fire({
                icon: "error",
                title: "Validation Error",
                text: "กรุณาระบุข้อมูล CustomerEmail"
            });
        }
        else if (QuoData.CustomerPhone == undefined) {
            Swal.fire({
                icon: "error",
                title: "Validation Error",
                text: "กรุณาระบุข้อมูล CustomerPhone"
            });
        }

        else if (SelectedDistricts == undefined) {
            Swal.fire({
                icon: "error",
                title: "Validation Error",
                text: "กรุณาระบุที่อยู่"
            });
        }
        else if (Entries == undefined)
        {
             Swal.fire({
                icon: "error",
                title: "Validation Error",
                text: "โปรดระบุสินค้า อย่างน้อย 1 รายการ"
             });
        }
        else {
            $scope.ListdataQuo = {
                QuotationNumber: QuoData.QuoNumber,
                CustomerName: QuoData.CustomerName,
                OrderDate: QuoData.OrderDate,
                OrderStatus: '',
                ShipDate: QuoData.ShipDate,
                TotalQty: QuoData.TotalQty,
                TotalPrice: QuoData.TotalPrice,
                CustomerEmail: QuoData.CustomerEmail,
                CustomerAddress: QuoData.CustomerAddress,
                CustomerPhone: QuoData.CustomerPhone,
                Remark: QuoData.Remark,
                CreateBy: EmpNo,
                CreateDate: QuoData.CreateDate,
                QuoProvince: SelectedProvinces,
                QuoStatus: 0,
                QuoDistricts: SelectedDistricts,
                QuoSubDistricts: SelectedSub,
                QuoZipCode: SZipcode,
                QuoCompanyName: QuoData.CompanyName,
                QuoRemark: QuoData.Remark,
                QuoLastname: QuoData.QuoLastname,
                QuoTaxID: QuoData.QuoTaxID,
                QuoType: SelectedTypeSell,
                QuoShippingPrice: QuoData.QuoShippingPrice
            };
            console.log($scope.ListdataQuo);


            $http.post(window.baseUrl + 'Home/SaveQuotations', {
                ListdataQuos :  $scope.ListdataQuo
            }).then(function (response) {
                console.log(response.data);


                var generatedQuotationNumber = response.data.QuotationNumber;

                var updatedEntries = angular.copy(Entries);
                updatedEntries.forEach(function (entry) {
                    entry.QuotationNumber = generatedQuotationNumber;
                });

                console.log(generatedQuotationNumber);
                console.log(updatedEntries);

                $http.post(window.baseUrl + 'Home/SaveToProductTable', {
                    Entries: updatedEntries,
                   EmpNos : EmpNo
                }).then(function (response) {

                    var QuoNumber = response.data[0].QuotationNumber;

     
                        Swal.fire({
                            icon: "success",
                            title: "Save Complete",
                            text: "New Quotation : " + QuoNumber + " Created."
                        }).then(function () {
                            // Redirect ไปหน้า index
                            $window.location.href = 'NdsSystemQuotation?EmpNo=' + EmpNo;
                        });
                    })
                    .catch(function (error) {
                        console.error("Error while saving entries:", error);
                        Swal.fire({
                            icon: "error",
                            title: "Error",
                            text: "Failed to save entries."
                        });
                    });
                // Add Update Quo Status
            }).catch(function (error) {
                console.error("Error:", error);
                alert("ไม่สามารถบันทึกข้อมูลได้");
            });
        }


      
    };










}]);


