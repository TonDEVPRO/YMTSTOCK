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

    $scope.reloadPage = function () {
        $window.location.reload();
    };
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
                        window.location.href = window.baseUrl + "Home/YMTHome?EmpNo=" + $scope.listEmpNo.Id + '&CodeId=' + '0'
                    }
                });
        }
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



    // ** Start **  MenuList StockNDS //
    $scope.getStockStyle = function () {
        $http.get(window.baseUrl + 'Home/getStockStyleNDS')
            .then(function (response) {
                $scope.ListStockStyle = response.data;
            })
    };
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
    }
    $scope.editStocks = function (id) {
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
    $scope.YMTNdsSystemData = function (EmpNo , CodeId) {
        $http.post(window.baseUrl + 'Home/GetEmployee',
            {
                EmployeeNo: EmpNo
            }).then(function (res) {
                $scope.emplist = res.data;
            });
    }
    $scope.YMTHolidayStockData = function (EmpNo) {
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
                $scope.GetColorData();
            });
        };
    };
    $scope.GetColorData = function () {
        $http.get('/Home/GetAutoColors')
            .then(function (response) {
                $scope.ListColorsAuto = response.data;
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

                        Swal.fire({
                            icon: "success",
                            title: "You Save Master Stock Complete",
                            text: "TotalStock : " + $scope.ListSaveDataStock.Total + ""
                        }).then(function () {
                            $window.location.href = "/Home/YMTNdsStock?EmpNo=" + EmpNo + '&CodeId=' + '0';
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
    $scope.NDSHolidayStocks = function (EmpNo , CodeId) {
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
        $http.post(window.baseUrl + 'Home/GetHolidayDetailAlls', { OrderNo: ddlOrderNo, Style: ddlStyle, Color: Colors, Size: ddlSize }).then(function (response) {
            $scope.ListHolidayData = response.data;
        });
    };
    $scope.SaveHolidayStock = function (ddlOrderNo, ddlStyle, Colors, ddlSize, alldata, UserIds) {
        $http.post(window.baseUrl + 'Home/SaveHolidayStocks', { SaveHoliday: alldata, UserId: UserIds }).then(function (response) {
            $scope.ListdataA = response.data;
            Swal.fire({
                icon: "success",
                title: "You save Holiday Stock Complete",
                text: "TotalStock : " + $scope.ListdataA.Total + ""
            }).then(function () {
                $window.location.href = "/Home/NDSHolidayStock?EmpNo=" + FullUserId + '&CodeId=' + '0';
            });
        })
    };
    $scope.GetMasterSeason = function (MasterSea) {
        $http.post(window.baseUrl + 'Home/GetMasterBandName', {
            CodeName: MasterSea
        }).then(function (res) {
            $scope.ListCodeName = res.data
        });
        $http.post(window.baseUrl + 'Home/GetMasterSeasons', {
            CodeName: MasterSea
        }).then(function (res) {
            $scope.ListSeason = res.data
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
                    formData.append('brandCode', $scope.MasterCustomer);
                    formData.append('brandName', $scope.ListCodeName.CustomerName);
                    formData.append('season', $scope.SeasonName);
                    formData.append('styleName', $scope.StyleName);
                    formData.append('OrderNo', $scope.OrderNo);
                    formData.append('TypeName', $scope.TypeName);
                    formData.append('Id', $scope.emplist.Id);
                    $http.post(window.baseUrl + 'Home/UploadFiles', formData, {
                        headers: { 'Content-Type': undefined },
                        transformRequest: angular.identity
                    }).then(function (res) {
                        $scope.Listdataupload = res.data
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
    $scope.GetQuoTation = function (EmpNos, CodeId) {
        console.log(EmpNos);
        window.location.href = window.baseUrl + "Home/NdsSystemQuotation?EmpNo=" + EmpNos + '&CodeId=' + CodeId
    };
    $scope.Getdataindex = function (EmpNo , CodeId) {
        $http.post(window.baseUrl + 'Home/GetEmployee',
            {
                EmployeeNo: EmpNo
            }).then(function (res) {
                $scope.emplist = res.data;
                console.log($scope.emplist);
            });
        $http.post(window.baseUrl + 'Home/GetdataQuo')
            .then(function (response) {
                $scope.ListQuo = response.data; 
                $scope.data = [];
                $http.get(window.baseUrl + 'Home/GetdataquoNew')
                    .then(function (response) {
                        $scope.data = response.data.data;
                        setTimeout(function () {
                            $('#dataTable').DataTable();
                        }, 0);
                    });
            })
            .catch(function (error) {
                console.error("Error:", error);
            });
    }
    $scope.editQuo = function (EmpNos, QuoNo ) {
        $window.location.href = 'NdsSystemEditQuotation?EmpNo=' + EmpNos + '&CodeId=' + QuoNo;
    }
    $scope.GetdataQuoForEdits = function (EmpNo, CodeId) {
        console.log(EmpNo);
        console.log(CodeId);

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
            });
        $scope.GetPageLoad(EmpNo);
        $http.post(window.baseUrl + 'Home/GetdataQuoForEdit', {
            QuotationNumber: CodeId,
            EmpNos: EmpNo
        }).then(function (response) {
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
                CreateBy: EmpNo,// ***
                CreateDate: new Date().toISOString().slice(0, 10),// รูปแบบ dd/MM/yyyy HH:mm:ss
                StyleName: response.data.StyleName,// ไม่มี MAP
                QuoType: response.data.QuoType,
                QuoLastname: response.data.QuoLastname,

                Remark: response.data.QuoRemark,
                QuoTaxID: response.data.QuoTaxID,
                QuoShippingPrice: response.data.QuoShippingPrice,
                QuoStatus: response.data.QuoStatus, // เพิ่ม QuoStatus
                ShipDate: response.data.ShipDate  // เพิ่ม ShipDate


            };

            $scope.selectedShipDate = $scope.QuoData.ShipDate.split('T')[0];
            $scope.isConfirmed = $scope.QuoData.QuoStatus.toString() === '1';
            $scope.selectedShipDate = $scope.isConfirmed ? $scope.QuoData.ShipDate : '';
            $scope.selectedShipDate = new Date($scope.QuoData.ShipDate).toLocaleDateString('en-GB');
            $scope.SelectedTypeSell = $scope.QuoData.QuoType;
            $scope.SelectedProvinces = response.data.QuoProvince;
            $scope.GetListDistricts(response.data.QuoProvince);
            $scope.SelectedDistricts = response.data.QuoDistricts;
            $scope.GetListSub(response.data.QuoDistricts, response.data.QuoProvince)
            $scope.SelectedSub = response.data.QuoSubDistricts;
            $scope.SZipcode = response.data.QuoZipCode;

            $http.post(window.baseUrl + 'Home/GetForEditProduct', {
                QuotationNumber: $scope.QuoData.QuoNumber
            }).then(function (response) {
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
    $scope.GetDataPageLoad = function (EmpNo , CodeId) {
        $scope.NewEntry = {
            TotalPrice: 0,
            Quantity: 0,
            PricePerUnit : 0
        };
        $scope.GetSku(); // โหลด Style Name
        $scope.GetColors(); // โหลด Color
        $scope.GetSizes();     // โหลด Sizes
        $scope.GetProvince(); // โหลด จังหวัด
        $scope.GetOrderType(); // โหลด Type order
        $scope.GetLoadRemark(); // โหลด Master remark
    };
    $scope.GetPageLoad = function (EmpNo) {
        $scope.GetSku(); // โหลด Style Name
        $scope.GetColors(); // โหลด Color
        $scope.GetSizes();     // โหลด Sizes
        $scope.GetProvince(); // โหลด จังหวัด
        $scope.GetOrderType(); // โหลด Type order
        $scope.GetLoadRemark(); // โหลด Master remark
    };
    $scope.GetSku = function () {
        $http.get(window.baseUrl + 'Home/GetSku')
            .then(function (response) {
                $scope.ListDropSku = response.data;
            });
    };
    $scope.GetSkuCode = function (styleName) {
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
        $http.post(window.baseUrl + 'Home/GetDistricts',
            {
                Provinces: Provincess
            })
            .then(function (response) {
                $scope.ListDistricts = response.data;
            });
    }
    $scope.GetListSub = function (SelectedDistricts, SelectedProvinces) {
        $http.post(window.baseUrl + 'Home/GetListSubs',
            {
                Districts: SelectedDistricts,
                Provinces: SelectedProvinces
            })
            .then(function (response) {
                $scope.ListSub = response.data; // เก็บผลลัพธ์จาก Backend
            })
            .catch(function (error) {
                console.error("Error:", error);
            });
    }
    $scope.GetListZipcode = function (SelectedSub, SelectedDistricts) {
        $http.post(window.baseUrl + 'Home/GetListZipcode',
            {
                Districts: SelectedDistricts,
                SubDistricts: SelectedSub
            })
            .then(function (response) {
                $scope.SZipcode = response.data; // เก็บผลลัพธ์จาก Backend
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
        const entries = Array.isArray($scope.Entries) ? $scope.Entries : [];
        const entryTotal = entries.reduce(function (sum, entry) {
            return sum + (entry.Quantity * entry.PricePerUnit);
        }, 0);
        const shippingPrice = isNaN(parseFloat($scope.QuoData.QuoShippingPrice)) ? 0 : parseFloat($scope.QuoData.QuoShippingPrice);
        $scope.QuoData.TotalPrice = entryTotal + shippingPrice;
    };
    $scope.CalculateQty = function () {
        $scope.QuoData.TotalQty = $scope.Entries.reduce(function (sum, entry) {
            return sum + entry.Quantity;
        }, 0);
    };
    $scope.GenerateQuotationNumber = function () {
        $http.get(window.baseUrl + 'Home/GenerateQuotationNumber')
            .then(function (response) {
                $scope.NewQuoNumber = response.data;
                console.log("QuotationNumber:", response.data);
            });
    }
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
        window.open(window.baseUrl + 'Home/DownloadFile?filePath=' + encodeURIComponent(filePath), '_blank');
    };
    $scope.deleteFile = function (filePaths, index, DelId) {
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
                $window.location.href = 'NdsSystemQuotation?EmpNo=' + EmpNo + '&CodeId=' + '0';
            }
        });
    };
    $scope.CreateQuo = function (EmpNo, CodeId) {
        $window.location.href = 'NdsSystemCreateQuotation?EmpNo=' + EmpNo + '&CodeId=' + CodeId;
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
    $scope.viewQuotation = function (EmpNo ,quotationNumber ) {
        console.log(EmpNo);
        console.log(quotationNumber);

        $window.location.href = '/Home/NdsSystemViewPage?EmpNo=' + EmpNo + '&CodeId=' + quotationNumber;
    };
    $scope.backHome = function (EmpNo) {
        $window.location.href = 'NdsSystemQuotation?EmpNo=' + EmpNo + '&CodeId=' + '0';
    };
    $scope.GetOrderInfo = function (EmpNo) {
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
                setTimeout(function () {
                    $('#dataTables').DataTable();
                }, 0);
            });
    };
    $scope.GetOrderInformation = function (EmpNo, CodeId) {
        $window.location.href = 'NdsSystemOrderInformation?EmpNo=' + EmpNo + '&CodeId=' + CodeId;
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
        $http.get(window.baseUrl + `Home/GetOrderDetails?orderNumber=${orderNumber}`)
            .then(function (response) {
                $scope.modalData = response.data;
                $scope.GetProductOrder($scope.modalData.OrderNumber);
                // เก็บข้อมูลใน $scope.modalData
                $('#OrderDetails').modal('show'); // เปิด ModalPopup
            }, function (error) {
                console.error("Error fetching order details: ", error);
            });


    };
    $scope.GetProductOrder = function (orderNumber) {
        $http.get(window.baseUrl + `Home/GetProductOrders?orderNumber=${orderNumber}`)
            .then(function (response) {
                $scope.productOrders = response.data; // เก็บข้อมูลใน scope
                console.log($scope.productOrders);

            }, function (error) {
                console.error("Error fetching product orders: ", error);
            });
    };
    $scope.GetPageFile = function (EmpNo, orderNumber) {

        console.log(EmpNo);
        console.log(orderNumber);

        $http.post(window.baseUrl + 'Home/GetEmployee',
            {
                EmployeeNo: EmpNo
            }).then(function (res) {
                $scope.emplist = res.data;
            });

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

                console.log($scope.quoFile);

                if (Array.isArray($scope.quoFile) && $scope.quoFile.length > 0) {
                    var quotationNumber = $scope.quoFile[0].quotationNumber;
                    $scope.quotationNumber = quotationNumber;
                } else {
                    $scope.quotationNumber = "";
                }
            })
            .catch(function (error) {
                console.error("Error:", error);
            });
        $http.post(window.baseUrl + 'Home/GetDataOtherFileTable', {

            orderNumbers: orderNumber
        })
            .then(function (response) {
                $scope.files = response.data; // เก็บข้อมูลใน scope
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
        $window.location.href = 'NdsSystemViewAttachments?EmpNo=' + EmpNo + '&CodeId=' + orderNumber  ;
    };
    $scope.backHomeOrder = function (EmpNo) {
        $window.location.href = 'NdsSystemOrderInformation?EmpNo=' + EmpNo + '&CodeId=' + '0';
    };






    ///RFID Menu
    $scope.RFIDGETDATA = function (EmpNo , CodeId) {
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
    $scope.RFIDUrlInfor = function (EmpNo, CodeId) {
        $window.location.href = 'RFIDIndex?EmpNo=' + EmpNo + '&CodeId=' + CodeId;
    };
    $scope.NdssystemLink = function (EmpNo , CodeId) {
        $window.location.href = 'YMTNdsSystem?EmpNo=' + EmpNo + '&CodeId=' + CodeId;
    };
    $scope.NDSShopLink = function (EmpNo, CodeId) {
        $window.location.href = 'YMTNdsStock?EmpNo=' + EmpNo + '&CodeId=' + CodeId;
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


    $scope.POSSystemData = function (EmpNo , CodeId) {

        $window.location.href = 'POSSystem?EmpNo=' + EmpNo + '&CodeId=' + CodeId;
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


