﻿window.ShowToastr = (type, message) => {

    toastr.options.toastClass = "toastr";

    if (type === "success") {
        toastr.success(message, "", { timeOut: 5000 });
        //toastr.success(message, "Opration Successful", { timeOut: 20000 });
    }
    if (type === "error") {
        toastr.error(message, "", { timeOut: 5000 });
        //toastr.error(message, "Opration Failed", { timeOut: 20000 });
    }
}

window.ShowSwal = (type, message) => {
    if (type === "success") {
        Swal.fire({
            title: "Opration Successful!",
            text: message,
            icon: "success"
        });
    }
    if (type === "error") {
        Swal.fire({
            title: "Opration Failed",
            text: message,
            icon: "error"
        });
    }
}