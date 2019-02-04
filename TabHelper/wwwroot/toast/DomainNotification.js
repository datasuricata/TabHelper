function formOnFail(error) {
    if (error && error.status == 500)
        toastr.error(error.responseText);
}