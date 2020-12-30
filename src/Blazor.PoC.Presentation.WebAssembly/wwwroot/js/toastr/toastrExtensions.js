window.toastrFunctions = {
    info: function (message, title, options) {
        toastr.options = options;
        toastr.info(message, title);
    },
    error: function (message, title, options) {
        toastr.options = options;
        toastr.error(message, title);
    },
    warning: function (message, title, options) {
        toastr.options = options;
        toastr.warning(message, title);
    }
}