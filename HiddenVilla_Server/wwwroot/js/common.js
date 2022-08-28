window.ShowToastr = (type, message) => {
    if (type === "success") {
        toastr.success(message, "Operation Successful", {timeout: 10000})
    }

    if (type === "error") {
        toastr.error(message, "Operation Failed", {timeout: 10000})
    }
}

window.showSwal = (type, message) => {
    if (type === "success") {
        Swal.fire(
            'Success',
            message,
            'success'
        )
    }

    if (type === "error") {
        Swal.fire(
            'Error',
            message,
            'error'
        )
    }
}

function ShowDeleteConfirmationModal() {
    $('#deleteConfirmationModal').modal('show');
}

function HideDeleteConfirmationModal() {
    $('#deleteConfirmationModal').modal('hide');
}
