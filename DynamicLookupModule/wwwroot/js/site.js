$(function () { $('table thead tr th, table tbody tr td').addClass('text-center'); });


function Toast(type, message) {
    Command: toastr[type](message)

    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "200",
        "hideDuration": "500",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }
}

function InfoToast(message) { Toast('info', message); }
function WarningToast(message) { Toast('warning', message); }
function ErrorToast(message) { Toast('error', message); }
function WarningToast(message) { Toast('warning', message); }



function SwalInfo(message) {
    Swal.fire(message);
}

function SwalConfirm() {
    return Swal.fire({
        title: 'Are you sure?',
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    });
}

