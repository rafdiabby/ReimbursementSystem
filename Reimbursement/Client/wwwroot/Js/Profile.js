$(document).ready(function () {
    var nik = document.getElementById("sessionNIK").value;
    console.log(nik);

    $.ajax({
        url: "/employees/get/" + nik,
        success: function (result) {

            var nik = result.nik;
            var fullName = result.firstName + ' ' + result.lastName;
            var email = result.email;
            var phone = '+62'+ result.phone;
            var gender = "";
            if (result.gender == 0) {
                gender = "Male";
            } else {
                gender = "Female";
            }
            var noRekening = result.bankAccount;
            console.log(fullName);
            document.getElementById("nik").innerHTML = nik;
            document.getElementById("fullname").innerHTML = fullName;
            document.getElementById("email").innerHTML = email;
            document.getElementById("phone").innerHTML = phone;
            document.getElementById("gender").innerHTML = gender;
            document.getElementById("noRekening").innerHTML = noRekening;
        }
    });

    $.ajax({
        url: "/Employees/GetRole/" + nik,
        success: function (hasil) {
            var dataRole = [];
            $.each(hasil, function (key, val) {
                dataRole.push(val.roleName);
            });
            var acRole = dataRole.toString();
            document.getElementById("acRole").innerHTML = acRole;
        }
    });
})