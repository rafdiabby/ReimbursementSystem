function Login() {
    var x = document.getElementById("loading");
    x.style.display = "block";

    var obj = new Object();

    obj.Email = $("#email").val();
    obj.Password = $("#password").val();

    console.log(obj);
    $.ajax({
        url: "/Login/Cek",
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
            x.style.display = "none";
        } else {
            swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Password Salah !!!'
            })
            x.style.display = "none";
        }
    }).fail((error) => {
        console.log(error);
    })
}