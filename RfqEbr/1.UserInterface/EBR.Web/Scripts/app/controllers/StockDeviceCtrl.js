myApp.controller('StockDeviceITController', ['$rootScope', '$scope', '$http', '$window', '$interval', 'ScannerService', '$timeout', 'FormatdateService', function ($rootScope, $scope, $http, $window, $interval, ScannerService, $timeout, FormatdateService) {
    $scope.viewUSE = true
    $scope.Data = [];
    $scope.stockList = [];
    $scope.filteredList = [];
    $scope.currentPage = 0;
    $scope.pageSize = 10;
    $scope.totalPages = 0;
    $scope.searchText = "";
    $scope.totalData = 0;
    $scope.supList = []
    $scope.trascList = []
    $scope.emplist = ""
    // ตัวแปรของหน้า Receive Item
    $scope.qtyIn = 0;
    $scope.ItemNameReceiveStock = "";
    // ตัวแปรสำหรับ Model Receive Item ที่จะส่งไปที่ Transaction
    $scope.ReceiveTransaction = {
        DateIn: "",
        TypeReceive: "",
        TrxType: "",
        Supplier: "",
        PONo: "",
        ReceiveNo: "",
        Code: "",
        QtyIn: 0
    }
    // ตัวแปรสำหรับ Model Out Item ที่จะส่งไปที่ Transaction
    $scope.OutTransaction = {
        DateOut: "",
        TypeOut: "",
        TrxType: "",
        Department: "",
        ComputerName: "",
        DeviceId: 0,
        Username: ""
    }

    // สำหรับ Search ใน ReceiveStockDeviceIT
    $scope.FilterTransData = {
        startDate: null,
        endDate: null,
        TrxType: "",
        TrxName:"",
        supplier: "",
        receiveNo: "",
        poNo: "",
        ItemName: ""
    };


    //Get Master Departments
    $scope.DepartmentList = []
    $scope.GetDepartments = function () {
        $http.get(window.baseUrl + 'MasterDepartment/GetDepartmentData').then(function (res) {
            $scope.DepartmentList = res.data;
        })
    }

    //Get Devices Data
    $scope.DeviceList = []
    $scope.filterDeviceList = []
    $scope.GetDevicesData = function () {
        $http.get(window.baseUrl + 'MasterDevice/GetDeviceData').then(function (res) {
            $scope.DeviceList = res.data;
            $scope.filterDeviceList = $scope.DeviceList
        })
    }
    $scope.filterDevice = function (department) {
        if (department) { // department ต้องมีค่าที่ไม่ใช่ค่าว่าง, undefined, หรือ null
            console.log(`department is : ${department}`)
            $scope.filterDeviceList = $scope.DeviceList.filter(item => item.Dept === department);
            console.log($scope.filterDeviceList);
        } else { // ถ้า department เป็น "" หรือไม่มีค่า ให้คืนค่าทั้งหมด
            $scope.filterDeviceList = $scope.DeviceList;
        }
    };
    //Get Master TransactionType
    $scope.receiveList = [];
    $scope.receiveAllList = [];
    $scope.outList = [];
    $scope.outAllList = [];
    $scope.GetTransactionTypeData = async function () {
        await $http.get(window.baseUrl + 'MasterTransactionType/GetTransactionTypeData').then(async function (res) {
            $scope.trascList = res.data;
            $scope.receiveList = $scope.trascList.filter(item => {
                return item.TrxType === "IN" && item.TrxName !== "Return";
            })
            $scope.receiveAllList = $scope.trascList.filter(item => {
                return item.TrxType === "IN"
            })
            $scope.outList = $scope.trascList.filter(item => {
                return item.TrxType === "OUT" && item.TrxName !== "Borrow";
            })
            $scope.outAllList = $scope.trascList.filter(item => {
                return item.TrxType === "OUT"
            })
        })
    }
    //Get Master TransactionData
    $scope.DataFilterList = [];
    $scope.DataOutList = [];
    $scope.DataTranscList = [];
    $scope.DataOntableTrans = {
        DocNo: "",
        Date: "",
        TrxType: "",
        Supplier: "",
        Code: "",
        ItemName: "",
        QtyIn:0
    }
    $scope.DataFilterTransList = []
    $scope.GetTransactionData = async function (Type) {
        await $scope.GetStockDeviceData();
        await $scope.GetTransactionTypeData();
        await $http.get(window.baseUrl + 'MasterTransaction/GetTransactionData').then(function (res) {
            $scope.DataTranscList = res.data;
            $scope.DataFilterList = $scope.DataTranscList.filter(item => {
                return item.TrxType === Type;
            })
            
        }).then(async function() {
            for (var i = 0; i < $scope.DataFilterList.length; i++) {
                let DataOntableTrans = {
                    DocNo: $scope.DataFilterList[i].DocNo,
                    Date: await formatDate($scope.DataFilterList[i].CreateDate),
                    TrxType: $scope.DataFilterList[i].TrxType,
                    TrxName: $scope.DataFilterList.filter(item => { return item.TrxType === $scope.DataFilterList[i].TrxType })[i].TrxName,
                    Supplier: $scope.DataFilterList[i].Supplier,
                    Code: $scope.DataFilterList[i].Code,
                    ItemName: $scope.allStockList.filter(item => { return item.Code === $scope.DataFilterList[i].Code })[0].ItemName,
                    UserName: $scope.DataFilterList[i].UserName,
                    QtyIn: $scope.DataFilterList[i].Qty,
                }
                $scope.DataFilterTransList.push(DataOntableTrans)
            }
            $scope.filterTransData()
        })
    }

    $scope.filterTransData = function () {
        const startDate = new Date($scope.FilterTransData.startDate);
        const endDate = new Date($scope.FilterTransData.endDate);
        var searchItemNameLower = $scope.FilterTransData.ItemName ? $scope.FilterTransData.ItemName.toLowerCase() : "";
        var searchTrxNameLower = $scope.FilterTransData.TrxName ? $scope.FilterTransData.TrxName.toLowerCase() : "";
        console.log(`start date: ${startDate}`)
        console.log(`End date: ${endDate}`)
        // กรองข้อมูลจาก DataReceiveTransList
        $scope.filteredData = $scope.DataFilterTransList.filter(item => {
            const itemDate =  FormatdateService.parseDateThai(item.Date) // แปลงวันที่ของแต่ละรายการให้เป็น Date object


            if (isNaN(itemDate)) return false;
            // ตรวจสอบว่า itemDate อยู่ระหว่าง startDate และ endDate หรือไม่
            const isDateInRange = (!startDate || itemDate >= startDate) && (!endDate || itemDate <= endDate);
            const isItemNameMatch = !searchItemNameLower || (item.ItemName && item.ItemName.toLowerCase().includes(searchItemNameLower));
            const isTrxNameMatch = !searchTrxNameLower || (item.TrxName && item.TrxName.toLowerCase().includes(searchTrxNameLower));

            return isDateInRange && isItemNameMatch && isTrxNameMatch;
        });
        // ในกรณีที่ไม่เลือกวันที่ใดๆ จะใช้ DataReceiveTransList ทั้งหมด
        if (!$scope.FilterTransData.startDate && !$scope.FilterTransData.endDate) {
            $scope.filteredData = [];
        }
        $scope.currentPage = 0;
        $scope.paginate($scope.filteredData.length, $scope.filteredData);
    };
    //Get Master Supplier
    $scope.GetSupplierData = function () {
        $http.get(window.baseUrl + 'MasterSupplier/GetSupplierData').then(function (res) {
            $scope.supList = res.data
        })
    }
    // Get Employee Data
    $scope.YMTHomeData = function (EmpNo) {
        $http.post(window.baseUrl + 'Home/GetEmployee',
            {
                EmployeeNo: EmpNo
            }).then(function (res) {
                $scope.emplist = res.data;
            });
    }
    // ดึงข้อมูลทั้งหมดใน Stock IT 
    $scope.GetStockDeviceData = function () {
        return new Promise(function (resolve, reject) { // return Promise เพื่อให้ function อื่นสามารถ await เพื่อ Load Data ก่อนได้
            $http.get(window.baseUrl + 'StockDeviceIT/GetStockData')
                .then(function (res) {
                    $scope.allStockList = res.data.items;  // เก็บข้อมูลทั้งหมดในตัวแปรหลัก
                    $scope.stockDeviceITFilter();  // กรองข้อมูลที่จะแสดง
                    resolve();  // ส่งสัญญาณว่าโหลดข้อมูลเสร็จแล้ว
                })
                .catch(function (error) {
                    reject(error);  // หากเกิดข้อผิดพลาด ให้ reject
                });
        });
    };
    // Search Stock Device
    $scope.stockDeviceITFilter = function () {
        if (!$scope.allStockList) return;

        var searchTextLower = $scope.searchText ? $scope.searchText.toLowerCase() : "";

        // กรองข้อมูลโดยใช้เงื่อนไขเดียว
        $scope.stockList = $scope.allStockList.filter(item => {
            // กรองตาม Checkbox (Status)
            var statusMatch = $scope.viewUSE ? item.Status === 0 : item.Status === 1;

            // กรองตาม searchText (ค้นหา Code หรือ ItemName)
            var searchMatch = !searchTextLower || (
                (item.ItemName && item.ItemName.toLowerCase().includes(searchTextLower)) ||
                (item.Code && item.Code.toLowerCase().includes(searchTextLower))
            );

            return statusMatch && searchMatch;
        });
        $scope.currentPage = 0;
        $scope.paginate($scope.stockList.length, $scope.stockList);
    };

    // Pagination
    $scope.paginate = function (listLenght,list) {
        $scope.totalPages = Math.ceil(listLenght / $scope.pageSize);
        $scope.Data = [];
        for (var i = 0; i < $scope.totalPages; i++) {
            $scope.Data[i] = list.slice(i * $scope.pageSize, (i + 1) * $scope.pageSize);
        }
    };

    $scope.goToPage = function (page) {
        if (page >= 0 && page < $scope.totalPages) {
            $scope.currentPage = page;
        }
    };

    $scope.visiblePages = function () {
        var pages = [];
        var start = Math.max($scope.currentPage - 2, 0);
        var end = Math.min(start + 5, $scope.totalPages);
        for (var i = start; i < end; i++) {
            pages.push(i);
        }
        return pages;
    };
    // End Pagination


    // dropdownlist
    $scope.isTypeReceiveOpen = false;
    $scope.isTypeOutOpen = false;
    $scope.isSupplierOpen = false;
    $scope.isDepartmentOpen = false;
    $scope.isDevicesOpen = false;
    $scope.isItemNameOpen = false;

    $scope.selectedDepartment = function (item) {
        $scope.OutTransaction.Department = item
        $scope.isDepartmentOpen = false
        $scope.filterDevice($scope.OutTransaction.Department)
    }
    $scope.selectDevice = function (item) {
        $scope.OutTransaction.ComputerName = item
        $scope.OutTransaction.Department = $scope.filterDeviceList.filter(item => {
            return item.DeviceName === $scope.OutTransaction.ComputerName
        })[0].Dept
        $scope.filterDevice($scope.OutTransaction.Department)
        console.log(`device select : ${$scope.OutTransaction.Department}`)
        $scope.isDevicesOpen = false;
    }
    $scope.selectSupplier = function (item) {
        $scope.ReceiveTransaction.Supplier = item
        $scope.isSupplierOpen = false;
    };
    $scope.selectTransactionType = function (item) {
        $scope.ReceiveTransaction.TypeReceive = item
        $scope.OutTransaction.TypeOut = item
        $scope.FilterTransData.TrxName = item
        if ($scope.FilterTransData.TrxName) {
            $scope.filterTransData()
        }
        $scope.isTypeReceiveOpen = false;
        $scope.isTypeOutOpen = false;
    }
    $scope.selectItemName = function (item) {
        $scope.FilterTransData.ItemName = item
        if ($scope.FilterTransData.ItemName) {
            $scope.filterTransData()
        }
        $scope.isItemNameOpen = false;
    }
    $scope.hideDropdown = function ($event) {
        // ถ้าคลิกไปที่ dropdown-menu จะไม่ปิด dropdown
        if ($event.relatedTarget && $event.relatedTarget.classList.contains("dropdown-item")) {
            return;
        }
        $scope.isTypeReceiveOpen = false;
        $scope.isTypeOutOpen = false;
        $scope.isSupplierOpen = false;
        $scope.isDepartmentOpen = false;
        $scope.isDevicesOpen = false;
        $scope.isItemNameOpen = false;
    };

    // End-dropdownlist

    //Table Click Event
    $scope.addDataToTable = []
    $scope.clickOnTable = function (code) {
        $scope.selectedCode = code;
        
        const selectedItem = $scope.stockList.find(item => item.Code === $scope.selectedCode);
        if (selectedItem && !$scope.addDataToTable.find(item => item.Code === $scope.selectedCode)) {
            $scope.addDataToTable.push(selectedItem);
        }
        $('#AddItemModal').modal('hide'); // Dismiss the modal
    };
    $scope.clickToRemoveData = function (code) {
        $scope.addDataToTable = $scope.addDataToTable.filter(
            item => {
                return item.Code !== code;
            }
        )
    }

    // Scanner
    $scope.barcodeValue = '';
    let barcodeScannedListener = null;

    // ฟังก์ชันขอสิทธิ์เข้าถึงกล้อง
    $scope.requestCameraAccess = function () {
        ScannerService.requestCameraAccess();
    };

    // ฟังก์ชันเริ่มการสแกน
    $scope.startScanner = function () {
        if (barcodeScannedListener) {
            barcodeScannedListener();
        }
        barcodeScannedListener = $rootScope.$on('barcodeScanned', function (event, barcode) {
            $timeout(() => {
                $scope.barcodeValue = barcode;
                const itemFilterBarcode = $scope.stockList.find(item => item.Code === $scope.barcodeValue);

                if (itemFilterBarcode && !$scope.addDataToTable.find(item => item.Code === $scope.barcodeValue)) {
                    $scope.addDataToTable.push(itemFilterBarcode);
                    $('#barcodeScaner').modal('hide'); // Dismiss the modal
                    $scope.stopScanner(); // Stop the scanner when data is found
                } else if (itemFilterBarcode) {
                    $('#barcodeScaner').modal('hide'); // Dismiss the modal
                    $scope.stopScanner();
                }else {
                    console.log(`data is : ${$scope.barcodeValue}`);
                }
            }, 0);
        });

        ScannerService.startScanner(function (barcode) {});
    };

    // ฟังก์ชันหยุดการสแกน
    $scope.stopScanner = function () {
        ScannerService.stopScanner();
        if (barcodeScannedListener) {
            barcodeScannedListener();
            barcodeScannedListener = null;
        }
    };

    // ฟังก์ชันสลับกล้อง (ยังไม่รองรับใน QuaggaJS)
    $scope.toggleCamera = function () {
        ScannerService.toggleCamera();
    };

    // ฟังก์ชันบันทึกบาร์โค้ด
    $scope.saveBarcode = function () {
        ScannerService.saveBarcode();
    };
    // Receive Action
    $scope.ArrReceiveTrans = []
    $scope.qtyInTemp = {}; // ตัวแปรเก็บค่า qty ชั่วคราวสำหรับแต่ละแถว
    $scope.receiveItemStock = function () {
        if ($scope.ReceiveTransaction.DateIn === "" || $scope.ReceiveTransaction.TypeReceive === "" || $scope.ReceiveTransaction.Supplier === "") {
            Swal.fire({
                title: 'ข้อมูลจำเป็นไม่ครบ!',
                text: 'กรุณากรอกข้อมูล วัน/เดือน/ปี ประเภทการรับ Supplier ให้ครบ',
                icon: 'warning',
                confirmButtonText: 'OK'
            });
            return;
        } else {
            if (typeof $scope.addDataToTable !== 'undefined' && $scope.addDataToTable.length > 0) {
                let invalidItems = [];
                let overMaxItems = [];
                $scope.ArrReceiveTrans = []
                for (i = 0; i < $scope.addDataToTable.length; i++) {
                    let stockItem = $scope.allStockList.find(item => item.Code === $scope.addDataToTable[i].Code); 
                    let transaction = {
                        DateIn: $scope.ReceiveTransaction.DateIn,
                        TrxName: $scope.ReceiveTransaction.TypeReceive,
                        TrxType: $scope.receiveList.filter(item => { return item.TrxName === $scope.ReceiveTransaction.TypeReceive })[0].TrxType,
                        Supplier: $scope.ReceiveTransaction.Supplier,
                        PoNo: $scope.ReceiveTransaction.PONo,
                        ReceiveNo: $scope.ReceiveTransaction.ReceiveNo,
                        Code: $scope.addDataToTable[i].Code,
                        Qty: parseInt($scope.qtyInTemp[$scope.addDataToTable[i].Code]) || 0,
                        CreateBy: $scope.emplist.YPTName,
                        EditBy: $scope.emplist.YPTName
                    }
                    // ใช้ find เพื่อตรวจสอบว่า Code นี้มีอยู่ใน ArrReceiveTrans แล้วหรือไม่
                    let existingTransaction = $scope.ArrReceiveTrans.find(item => item.Code === transaction.Code);
                    // ตรวจสอบค่า qty ว่าเป็นตัวเลข และมากกว่า 0 หรือไม่
                    if (!transaction.Qty || isNaN(!transaction.Qty) || transaction.Qty <= 0) {
                        invalidItems.push(transaction.Code);
                    }
                    
                    // ตรวจสอบว่าจำนวนมากกว่า Maximum หรือไม่
                    if (stockItem && transaction.Qty > stockItem.Maximum) {
                        overMaxItems.push(transaction);
                    }
                    // ถ้าไม่มีข้อมูลที่ Code ซ้ำกัน (หมายถึงไม่พบข้อมูลใน ArrReceiveTrans) จะเพิ่มข้อมูลใหม่
                    if (!existingTransaction) {
                        $scope.ArrReceiveTrans.push(transaction);
                    } else {
                        existingTransaction.Qty = transaction.Qty;
                    }
                }
                if (invalidItems.length > 0) {
                    Swal.fire({
                        title: 'ข้อผิดพลาด!',
                        text: `กรุณาใส่จำนวนข้อมูล(Qty) ในตาราง`,
                        icon: 'error',
                        confirmButtonText: 'OK'
                    });
                    return; // หยุดการทำงาน
                }
                if (overMaxItems.length > 0) {
                    let overMaxText = overMaxItems.map(item => `${item.Code} (${item.Qty} / Max: ${$scope.allStockList.find(stock => stock.Code === item.Code)?.Maximum})`).join('\n');

                    Swal.fire({
                        title: 'รายการเกินจำนวนสูงสุด!',
                        text: `รายการต่อไปนี้เกิน Maximum:\n${overMaxText}`,
                        icon: 'warning',
                        showCancelButton: true,
                        confirmButtonText: 'ใช่! เพิ่มเข้าไป',
                        cancelButtonText: 'ไม่'
                    }).then((result) => {
                        if (!result.isConfirmed) {
                            return;
                        }
                        Swal.fire({
                            title: 'ต้องการเพิ่มของใน Stock?',
                            text: "กรุณาตรวจสอบข้อมูลให้ถูกต้อง!",
                            icon: 'warning',
                            showCancelButton: true,
                            confirmButtonText: 'ใช่!',
                            cancelButtonText: 'ไม่'
                        }).then((confirmResult) => {
                            if (confirmResult.isConfirmed) {
                                Swal.fire('เสร็จสิ้น', 'รับของเข้าStock เรียบร้อย.', 'success');
                                $http.post(window.baseUrl + 'ReceiveStockDeviceIT/ReceiveItemData', $scope.ArrReceiveTrans).then(function (res) {
                                    $scope.ReceiveTransaction = {
                                        DateIn: "",
                                        TypeReceive: "",
                                        TrxType: "",
                                        Supplier: "",
                                        PONo: "",
                                        ReceiveNo: "",
                                        Code: "",
                                        QtyIn: 0
                                    }
                                    $scope.addDataToTable = []; // ล้างข้อมูลตารางหลังจากยิง API
                                    $scope.qtyInTemp = {}; // ล้างค่าที่กรอกไว้ใน input
                                    $scope.GetStockDeviceData()
                                });
                            }
                        });
                    });
                } else {
                    // ใช้ setTimeout เพื่อรอให้ loop เสร็จสิ้นก่อนทำการ log
                    setTimeout(function () {
                        console.log(`ReceiveStock : ${JSON.stringify($scope.ArrReceiveTrans)}`);
                        Swal.fire({
                            title: 'ต้องการเพิ่มของใน Stock?',
                            text: "กรุณาตรวจสอบข้อมูลให้ถูกต้อง!",
                            icon: 'warning',
                            showCancelButton: true,
                            confirmButtonText: 'ใช่!',
                            cancelButtonText: 'ไม่'
                        }).then((result) => {
                            if (result.isConfirmed) {
                                Swal.fire('เสร็จสิ้น', 'รับของเข้าStock เรียบร้อย.', 'success');
                                $http.post(window.baseUrl + 'ReceiveStockDeviceIT/ReceiveItemData', $scope.ArrReceiveTrans).then(function (res) {
                                    console.log(`post receive stock : ${JSON.stringify(res.data)}`)
                                    $scope.ReceiveTransaction = {
                                        DateIn: "",
                                        TypeReceive: "",
                                        TrxType: "",
                                        Supplier: "",
                                        PONo: "",
                                        ReceiveNo: "",
                                        Code: "",
                                        QtyIn: 0
                                    }
                                    $scope.addDataToTable = []; // ล้างข้อมูลตารางหลังจากยิง API
                                    $scope.qtyInTemp = {}; // ล้างค่าที่กรอกไว้ใน input
                                    $scope.GetStockDeviceData()
                                })
                            }
                        });

                    }, 0);
                }

            } else {
                Swal.fire({
                    title: 'Error!',
                    text: 'ไม่มีข้อมูลอุปกรณ์ กรุณาเพิ่มข้อมูลอุปกรณ์!',
                    icon: 'error',
                    confirmButtonText: 'Close'
                });
            }
        }
    }
    $scope.qtyInTemp = {}; // ตัวแปรเก็บค่า qty ชั่วคราวสำหรับแต่ละแถว
    $scope.outItemStock = function () {
        if ($scope.OutTransaction.DateOut === "" || $scope.OutTransaction.TypeOut === "" || $scope.OutTransaction.ComputerName === "" || $scope.OutTransaction.Username === "") {
            Swal.fire({
                title: 'ข้อมูลจำเป็นไม่ครบ!',
                text: 'กรุณากรอกข้อมูล วัน/เดือน/ปี ประเภทการเบิก แผนกงานที่เบิก ComputerName ชื่อผู้เบิกให้ครบ',
                icon: 'warning',
                confirmButtonText: 'OK'
            });
            return;
        } else {
            if (typeof $scope.addDataToTable !== 'undefined' && $scope.addDataToTable.length > 0) {
                let invalidItems = [];
                let overMinItems = [];
                $scope.ArrReceiveTrans = []
                for (let i = 0; i < $scope.addDataToTable.length; i++) {
                    let stockItem = $scope.allStockList.find(item => item.Code === $scope.addDataToTable[i].Code);
                    let qty = parseInt($scope.qtyInTemp[$scope.addDataToTable[i].Code]) || 0;
                    let transaction = {
                        DateIn: $scope.OutTransaction.DateOut,
                        TrxName: $scope.OutTransaction.TypeOut,
                        TrxType: $scope.outList.find(item => item.TrxName === $scope.ReceiveTransaction.TypeReceive)?.TrxType,
                        DeptOut: $scope.OutTransaction.Department,
                        DeviceName: $scope.OutTransaction.ComputerName,
                        UserName: $scope.OutTransaction.Username,
                        DeviceId: $scope.DeviceList.find(item => item.DeviceName === $scope.OutTransaction.ComputerName)?.DeviceId,
                        Code: $scope.addDataToTable[i].Code,
                        Qty: qty,
                        CreateBy: $scope.emplist.YPTName,
                        EditBy: $scope.emplist.YPTName
                    }
                    let existingTransaction = $scope.ArrReceiveTrans.find(item => item.Code === transaction.Code);

                    if (!transaction.Qty || isNaN(transaction.Qty) || transaction.Qty <= 0) {
                        invalidItems.push(transaction.Code);
                    }

                    if (stockItem && stockItem.QtyBalance < transaction.Qty) {
                        overMinItems.push(transaction);
                    }

                    if (!existingTransaction) {
                        $scope.ArrReceiveTrans.push(transaction);
                    } else {
                        existingTransaction.Qty = transaction.Qty;
                    }

                    console.log(`Log Out Action : ${JSON.stringify($scope.ArrReceiveTrans)}`)
                }
                if (invalidItems.length > 0) {
                    Swal.fire({
                        title: 'ข้อผิดพลาด!',
                        text: `กรุณาใส่จำนวนข้อมูล(Qty) ในตาราง`,
                        icon: 'error',
                        confirmButtonText: 'OK'
                    });
                    return;
                }
                if (overMinItems.length > 0) {
                    let overMinText = overMinItems.map(item => `${item.Code} (${item.Qty} / QtyBalance: ${$scope.allStockList.find(stock => stock.Code === item.Code)?.QtyBalance})`).join('\n');
                    Swal.fire({
                        title: 'รายการเบิกของเกิน Stock!',
                        text: `รายการต่อไปนี้เบิกของเกิน Stock: \n ${overMinText}`,
                        icon: 'warning',
                        confirmButtonText: 'OK'
                    })

                } else {
                    setTimeout(function () {
                        Swal.fire({
                            title: 'ต้องการเบิกของจาก Stock?',
                            text: "กรุณาตรวจสอบข้อมูลให้ถูกต้อง!",
                            icon: 'warning',
                            showCancelButton: true,
                            confirmButtonText: 'ใช่!',
                            cancelButtonText: 'ไม่'
                        }).then((result) => {
                            if (result.isConfirmed) {
                                Swal.fire('เสร็จสิ้น', 'เบิกของจากStock เรียบร้อย.', 'success');
                                $http.post(window.baseUrl + 'OutStockDeviceIT/OutItemData', $scope.ArrReceiveTrans).then(function (res) {
                                    console.log(`post outed stock : ${JSON.stringify(res.data)}`)
                                    $scope.OutTransaction = {
                                        DateOut: "",
                                        TypeOut: "",
                                        TrxType: "",
                                        Department: "",
                                        ComputerName: "",
                                        DeviceId: 0,
                                        Username: ""
                                    }
                                    $scope.addDataToTable = []; // ล้างข้อมูลตารางหลังจากยิง API
                                    $scope.qtyInTemp = {}; // ล้างค่าที่กรอกไว้ใน input
                                    $scope.GetStockDeviceData()
                                })
                            }
                        });
                    }, 0);
                }
            } else {
                Swal.fire({
                    title: 'Error!',
                    text: 'ไม่มีข้อมูลอุปกรณ์ กรุณาเพิ่มข้อมูลอุปกรณ์!',
                    icon: 'error',
                    confirmButtonText: 'Close'
                });
            }
        }
    }


    // ฟังก์ชันที่จะแปลงวันที่เป็นรูปแบบที่ต้องการ
    async function formatDate(dateString) {
       return await FormatdateService.formatDate(dateString); // คืนค่าผลลัพธ์ที่ได้จาก service
    }

}]);