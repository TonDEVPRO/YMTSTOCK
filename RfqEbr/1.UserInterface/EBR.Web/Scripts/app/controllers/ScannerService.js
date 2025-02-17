myApp.service('ScannerService', function ($timeout, $http, $rootScope) {
    let scannerActive = false;
    let barcodeValue = "";
    let lastScannedTime = 0;
    const debounceDelay = 300; // หน่วงเวลา 300 มิลลิวินาที

    // ฟังก์ชันขอสิทธิ์เข้าถึงกล้อง
    async function requestCameraAccess() {
        try {
            // ขอสิทธิ์การเข้าถึงกล้อง
            const stream = await navigator.mediaDevices.getUserMedia({ video: true });
            stream.getTracks().forEach(track => track.stop());

            const permissionStatus = await navigator.permissions.query({ name: 'camera' });

            if (permissionStatus.state === 'granted') {
                await initCamera();
                startScanner();
                document.getElementById('camera-permission-text').style.display = 'none';
            } else if (permissionStatus.state === 'denied') {
                document.getElementById('camera-permission-success').style.display = 'none';
                alert("คุณปฏิเสธการให้สิทธิ์เข้าถึงกล้อง");
            } else {
                document.getElementById('camera-permission-success').style.display = 'none';
                alert("ไม่ได้รับอนุญาตให้เข้าถึงกล้อง");
            }

            permissionStatus.onchange = function () {
                console.log("สถานะสิทธิ์การเข้าถึงกล้องเปลี่ยนแปลง:", permissionStatus.state);
            };

        } catch (error) {
            document.getElementById('camera-permission-success').style.display = 'none';
            console.error("ไม่สามารถเข้าถึงสิทธิ์การใช้กล้อง:", error);
            alert("ไม่สามารถตรวจสอบสถานะสิทธิ์กล้องได้ กรุณาตรวจสอบการเข้าถึงสิทธิ์ใช้งานกล้องใน Browser");
        }
    }

    // ฟังก์ชันสำหรับเริ่มกล้อง
    async function initCamera() {
        try {
            const stream = await navigator.mediaDevices.getUserMedia({ video: true });

            $timeout(function () {
                let videoElement = document.getElementById('scanner-container');
                if (videoElement) {
                    videoElement.srcObject = stream;
                }
            });
        } catch (error) {
            console.error("ไม่สามารถเข้าถึงกล้อง:", error);
            alert("ไม่สามารถเปิดกล้องได้");
        }
    }

    async function startScanner(callback) {
        try {
            // รีเซ็ตตัวแปรสถานะ
            barcodeValue = "";
            lastScannedTime = 0;

            // การตั้งค่า Quagga เพื่อเริ่มการสแกน
            await initQuagga();

            // ตั้งค่าฟังก์ชันเมื่อเจอการสแกน
            Quagga.onDetected(function (result) {
                const currentTime = Date.now();

                // เช็คว่าเวลาผ่านไปมากกว่า debounceDelay หรือไม่
                if (currentTime - lastScannedTime > debounceDelay) {
                    lastScannedTime = currentTime;  // อัพเดทเวลาการสแกนล่าสุด
                    const barcode = result.codeResult.code;

                    // ตรวจสอบว่า barcode เปลี่ยนแปลงหรือไม่
                    if (barcode !== barcodeValue) {
                        barcodeValue = barcode;
                        // ใช้ $timeout เพื่อให้ AngularJS ทำการ apply การเปลี่ยนแปลง
                        // เรียก callback พร้อมส่งค่าบาร์โค้ด
                        if (callback) {
                            callback(barcodeValue);  // เรียก callback
                        }
                        $timeout(function () {
                            // ใช้ $evalAsync แทนการใช้ $apply
                            $rootScope.$evalAsync(() => {
                                $rootScope.$broadcast('barcodeScanned', barcodeValue);
                            }, 0);

                        });
                    }
                }
            });

            // เริ่มการสแกน
            Quagga.start();
            scannerActive = true;
        } catch (error) {
            console.error("เกิดข้อผิดพลาดในการเริ่มต้นการสแกน:", error);
            alert("เกิดข้อผิดพลาดในการเริ่มต้นการสแกน กรุณาตรวจสอบการเข้าถึงสิทธิ์ใช้งานกล้องใน Browser");
        }
    }

    // ฟังก์ชันเริ่มต้น Quagga
    async function initQuagga() {
        return new Promise((resolve, reject) => {
            Quagga.init({
                inputStream: {
                    name: "Live",
                    type: "LiveStream",
                    target: document.querySelector('#scanner-container'),
                    constraints: {
                        facingMode: 'environment', // ใช้กล้องหลัง
                    }
                },
                decoder: {
                    readers: [
                        "code_128_reader",
                        "ean_reader",
                        "ean_8_reader",
                        "code_39_reader",
                        "upc_reader", // UPC-A
                        "i2of5_reader" // Interleaved 2 of 5
                    ] // ประเภทบาร์โค้ดที่ต้องการสแกน
                }
            }, function (err) {
                if (err) {
                    console.error("เกิดข้อผิดพลาดในการเริ่ม Quagga:", err);
                    reject(err);
                } else {
                    resolve();
                }
            });
        });
    }

    // ฟังก์ชันหยุดการสแกน
    function stopScanner() {
        Quagga.stop();
        Quagga.offDetected();
        scannerActive = false;
    }

    // ฟังก์ชันสลับกล้อง
    async function toggleCamera() {
        alert("QuaggaJS ไม่รองรับการสลับกล้องในขณะนี้");
    }

    // ฟังก์ชันบันทึกบาร์โค้ด
    async function saveBarcode() {
        try {
            const response = await $http.post('/Barcode/SaveBarcode', { barcode: barcodeValue });
            alert("บันทึกสำเร็จ!");
        } catch (error) {
            alert("เกิดข้อผิดพลาด: " + error.data);
        }
    }

    // Return เป็น Function แทน Object
    return {
        requestCameraAccess,
        startScanner,
        stopScanner,
        toggleCamera,
        saveBarcode
    };
});