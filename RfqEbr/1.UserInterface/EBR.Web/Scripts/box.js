var box = {
    info: function (title, callback) {
        swal({
            title: '<h3 class="text-primary">' + title + '</h3>',
            type: 'info',
            animation: false,
            closeOnCancel: false,
            html: true
        }, function () {
            if (callback)
                callback();
        });
    },

    warning: function (title, callback) {
        swal({
            title: '<h3 class="text-warning">' + title + '</h3>',
            type: 'warning',
            animation: false,
            html: true
        }, function () {
            if (callback)
                callback();
        });
    },

    error: function (title, callback) {
        swal({
            title: '<h3 class="text-danger">' + title + '</h3>',
            type: 'error',
            animation: false,
            html: true,
            customClass: 'swal-error'
        }, function () {
            if (callback)
                callback();
        });
    },

    success: function (title, callback) {
        swal({
            title: '<h3 class="text-success text-center">' + title + '</h3>',
            type: 'success',
            animation: false,
            html: true
        }, function () {
            if (callback)
                callback();
        });
    },

    confirm: function (title, callback) {
        swal({
            title: title,
            type: 'warning',
            showCancelButton: true,
            confirmButtonText: 'OK',
            animation: false,
            html: true,
        }, function (isConfirm) {
            callback(isConfirm);
        });
    },

    show: function (msg, callback) {
        swal({
            title: '<h3 class="text-primary text-center">' + msg + '</h3>',
            animation: false,
            closeOnCancel: false,
            type: 'info',
            html: true
        }, function () {
            if (callback)
                callback();
        });
    },

    timer: function (title, callback) {
        swal({
            title: '<h3 class="text-success">' + title + '</h3>',
            //type: 'success',
            animation: false,
            html: true,
            timer: 800,
            showConfirmButton: false
        }, function () {
            if (callback)
                callback();
        });
    },

    close: function () {
        swal.close();
    }
};