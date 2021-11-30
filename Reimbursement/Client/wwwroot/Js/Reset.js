$(document).ready(function () {
    var nik = document.getElementById("sessionNIK").value;
    console.log(nik);

    document.getElementById("id").value = nik;

    $('#btnSubmit').click(function (e) {
        e.preventDefault();
        if ($("#reset").valid() == true) {
            var pass = document.getElementById("password").value;
            var passConfir = document.getElementById("confirPassword").value;
            var cek = (pass == passConfir).toString();
            console.log(cek);
            if (cek == "false") {
                swal("ERROR", "Password dan Confirmasi Password tidak sama !!!", {
                    icon: "error",
                    buttons: {
                        confirm: {
                            className: 'btn btn-danger'
                        }
                    },
                });
            } else {
                Reset();
            }
        }
    });

    $("#reset").validate({
        rules: {
            "password": {
                required: true
            },
            "confirPassword": {
                required: true
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
});

function Reset() {

    var nik = $("#id").val();

    var obj = new Object();

    obj.nik = $("#id").val();
    obj.password = $("#password").val();


    $.ajax({
        url: "/Login/ResetPW/",
        'type': 'Put',
        'data': { id: nik, account: obj },
        'dataType': 'json'
    }).done((result) => {
        if (result.pesan == "1") {
            swal("Good job!", "Berhasil Ubah Password silahkan Login Kembali", {
                icon: "success",
                buttons: {
                    confirm: {
                        className: 'btn btn-success',
                    }
                },
            });
            window.location.replace("/Login/Logout/");
        } else {
            swal("ERROR", "Gagal Update Password !!!", {
                icon: "error",
                buttons: {
                    confirm: {
                        className: 'btn btn-danger'
                    }
                },
            });
        }
    }).fail((error) => {
        swal("ERROR", `${error.messege}`, {
            icon: "error",
            buttons: {
                confirm: {
                    className: 'btn btn-danger'
                }
            },
        });
    })
}