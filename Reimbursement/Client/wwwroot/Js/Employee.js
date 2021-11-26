$(document).ready(function () {
    var tabel = $('#EmployeeTable').DataTable({
        'ajax': {
            'url': "/Employees/GetAll",
            'dataType': 'json',
            'dataSrc': ''
        },
        "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
        'columnDefs': [{

            'targets': [6], /* column index */

            'orderable': false, /* true or false */

        }],
        'columns': [
            {
                "data": "nik"
            },
            {
                "data": "",
                "render": function (data, type, row, meta) {
                    return row['firstName'] + ' ' + row['lastName'];
                }
            },
            {
                "data": "email"
            },
            {
                "data": "",
                "render": function (data, type, row, meta) {
                    if (row['phone'].search(0) == 0) {
                        return row['phone'].replace('0', '+62');
                    } else {
                        return '+62' + row['phone'];
                    }
                }
            },
            {
                "data": "",
                "render": function (data, type, row, meta) {
                    if (row['gender'] == '0') {
                        return 'Male';
                    } else {
                        return 'Female';
                    }
                }
            },
            {
                "data": "bankAccount"
            },
            {
                "data": "",
                "render": function (data, type, row, meta) {
                    var btn = '<div class="form-button-action"> <button type="button" data-toggle="modal" data-target="#addRole" data-toggle="tooltip" title="" class="btn btn-link btn-success btn-lg" data-original-title="Add Role"> <i class="fa fa-plus"></i> </button> <button type="button" data-toggle="modal" data-target="#editData" data-toggle="tooltip" title="" class="btn btn-link btn-primary btn-lg" data-original-title="Edit Data"> <i class="fa fa-edit"></i> </button> <button type="button" onclick="Delete(' + row['nik'] + ');" data-toggle="tooltip" title="" class="btn btn-link btn-danger" data-original-title="Remove"> <i class="fa fa-times"></i> </button> </div>';
                    return btn;
                }
            }
        ],
        buttons: [
            {
                extend: 'excelHtml5',
                name: 'excel',
                title: 'Employee',
                sheetName: 'Employee',
                text: '',
                className: 'fa fa-download btn-default',
                filename: 'Data',
                autoFilter: true,
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5]
                }
            }
        ],
    });

    $("#exportExcel").on('click', function (e) {
        tabel.buttons('.buttons-excel').trigger();
    });

    tabel.on('draw', function () {
        $('[data-toggle="tooltip"]').tooltip();
    })

    $("#EmployeeForm").validate({
        rules: {
            "nik": {
                required: true
            },
            "firstName": {
                required: true
            },
            "lastName": {
                required: true
            },
            "email": {
                required: true
            },
            "phone": {
                required: true
            },
            "noRekening": {
                required: true
            },
            "gender": {
                required: true,
            }
        },
        errorPlacement: function (error, element) { },
        highlight: function (element) {
            $(element).closest('.form-control').addClass('is-invalid');
        },
        unhighlight: function (element) {
            $(element).closest('.form-control').removeClass('is-invalid');
            $(element).closest('.form-control').addClass('is-valid ');
        }
    });

    $('#btnSubmit').click(function (e) {
        e.preventDefault();
        if ($("#EmployeeForm").valid() == true) {
            Insert();
        }
    });

    $("#nik").keypress(function (data) {
        if (data.which != 8 && data.which != 0 && (data.which < 48 || data.which > 57)) {
            $('#nik').addClass('is-invalid');
            $("#pesanNik").html("Nik Harus Berupa Angka !!!").show();
            return false;
        }
        else {
            $('#nik').css('border-color', 'Green');
            $("#pesanNik").hide();
            return true;
        }
    });
});

function Insert() {

    var obj = new Object();

    obj.NIK = $("#nik").val();
    obj.firstName = $("#firstName").val();
    obj.lastName = $("#lastName").val();
    obj.email = $("#email").val();
    obj.phone = $("#phone").val();
    obj.gender = $("#gender").val();
    obj.bankAccount = $("#noRekening").val();
    obj.Password = "123";
    obj.roleId = "1";


    $.ajax({
        url: "/Employees/Register",
        type: 'POST',
        data: { register: obj },
        dataType: "json"
    }).done((result) => {
        if (result.pesan == "1") {
            swal("Good job!", "You clicked the button!", {
                icon: "success",
                buttons: {
                    confirm: {
                        className: 'btn btn-success'
                    }
                },
            });
            $('#EmployeeTable').DataTable().ajax.reload();
            $("#tambahData").modal("hide");
        } else if (result.pesan == "2") {
            swal("ERROR", "NIK Sudah Terdaftar !!!", {
                icon: "error",
                buttons: {
                    confirm: {
                        className: 'btn btn-danger'
                    }
                },
            });
        } else if (result.pesan == "3") {
            swal("ERROR", "Email Sudah Terdaftar !!!", {
                icon: "error",
                buttons: {
                    confirm: {
                        className: 'btn btn-danger'
                    }
                },
            });
        } else {
            swal("ERROR", "Phone Sudah Terdaftar !!!", {
                icon: "error",
                buttons: {
                    confirm: {
                        className: 'btn btn-danger'
                    }
                },
            });
        }
    }).fail((error) => {
        console.log(error);
    })
}

function Delete(nik) {
    swal({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        type: 'warning',
        buttons: {
            cancel: {
                visible: true,
                text: 'No, cancel!',
                className: 'btn btn-danger'
            },
            confirm: {
                text: 'Yes, delete it!',
                className: 'btn btn-success'
            }
        }
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                url: "/Employees/DeleteEmployees/" + nik,
                type: "Delete"
            }).then((result) => {
                console.log(result);
                if (result == 200) {
                    swal("data has been deleted !!!", {
                        icon: "success",
                        buttons: {
                            confirm: {
                                className: 'btn btn-success'
                            }
                        }
                    });
                    $('#EmployeeTable').DataTable().ajax.reload();
                } else {
                    swal("ERROR", "gagal dihapus !!!", {
                        icon: "error",
                        buttons: {
                            confirm: {
                                className: 'btn btn-danger'
                            }
                        },
                    });
                }
            })
            
        } else {
            swal("Your imaginary file is safe!", {
                buttons: {
                    confirm: {
                        className: 'btn btn-success'
                    }
                }
            });
        }
    });
}