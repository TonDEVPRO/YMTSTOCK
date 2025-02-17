myApp.service('FormatdateService', function ($timeout, $http, $rootScope) {

    async function formatDate(dateString) {
        const months = [
            'มกราคม', 'กุมภาพันธ์', 'มีนาคม', 'เมษายน', 'พฤษภาคม', 'มิถุนายน',
            'กรกฎาคม', 'สิงหาคม', 'กันยายน', 'ตุลาคม', 'พฤศจิกายน', 'ธันวาคม'
        ];

        const date = new Date(dateString); // แปลงจาก String เป็น Date object
        const day = String(date.getDate()).padStart(2, '0'); // วัน
        const month = months[date.getMonth()]; // เดือน
        const year = date.getFullYear(); // ปี
        return `${day} ${month} ${year}`; // รูปแบบวันที่ในภาษาไทย
    }

    // สำหรับแปลง format ให้เป็น Object เพื่อไปทำการเทียบค่าได้ โดยอิง format จาก function formatDate
    function parseDateThai(dateString) {
        const months = {
            'มกราคม': 0,
            'กุมภาพันธ์': 1,
            'มีนาคม': 2,
            'เมษายน': 3,
            'พฤษภาคม': 4,
            'มิถุนายน': 5,
            'กรกฎาคม': 6,
            'สิงหาคม': 7,
            'กันยายน': 8,
            'ตุลาคม': 9,
            'พฤศจิกายน': 10,
            'ธันวาคม': 11
        };

        // แยกวันที่ออกเป็นวันที่, เดือน, ปี
        const [day, month, year] = dateString.split(' ');

        // แปลงเดือนที่เป็นชื่อภาษาไทยไปเป็นตัวเลข
        const monthNumber = months[month];

        // สร้าง Date object จากปี, เดือน, และวัน
        const parsedDate = new Date(year, monthNumber, day);

        return parsedDate;
    }
    // Return เป็น Function แทน Object
    return {
        formatDate,
        parseDateThai
    };
});