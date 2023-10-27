toastr.options = {
    "closeButton": true,
    "debug": false,
    "newestOnTop": false,
    "progressBar": false,
    "positionClass": "toast-top-right",
    "preventDuplicates": false,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "5000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
};

var messageOptions = {
    Type: "",
    Title: "",
    Description: "",
    ThenTrue: "",
    ThenFalse: ""
};

var messageEvents =
{
    show: function (messageOptions) {
        swal.fire({
            "title": "" + messageOptions.Title + "",
            "text": "" + messageOptions.Description + "",
            "icon": "" + messageOptions.Type+"",
            "showCloseButton": true,
            "showCancelButton": true,
            "focusConfirm": false,
            "confirmButtonText":
                '<i class="fa fa-thumbs-up"></i> Onayla !',
            "confirmButtonAriaLabel": 'Onayla',
            "cancelButtonText":
                '<i class="fa fa-thumbs-down"></i> İptal',
            "cancelButtonAriaLabel": 'İptal Et'
        }).then((result) => {
            if (result.value && messageOptions.ThenTrue) {
                messageOptions.ThenTrue();
            } else if (messageOptions.ThenFalse){
                messageOptions.ThenFalse();
            }
        });
    }
}

var notificationEvents = {
    backNotification: function () {
        var result = "";
        result = $('#success').val();
        if (result != 'undefined' && result != "" && result != null) {
            notificationEvents.showInfo(result);
        }
        result = $('#error').val();
        if (result != 'undefined' && result != "" && result != null) {
            notificationEvents.showError(result);
        }
    },
    showInfo: function (message) {
        toastr.info(message);
    },
    showError: function (message) {
        toastr.error(message);
    }
}


