function Login() {
    var obj = new Object();

    obj.Email = $("#email").val();
    obj.Password = $("#password").val();

    console.log(obj);
    $.ajax({
        url: "/Employees/Cek",
        type: 'POST',
        data: { login: obj },
        dataType: "json"
    }).done((result) => {
        if (result.pesan == "1") {
            $("#loginForm").submit();
        } else if (result.pesan == "2") {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Email belum terdaftar !!!'
            })
        } else {
            swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Password Salah !!!'
            })
        }
    }).fail((error) => {
        console.log(error);
    })
}