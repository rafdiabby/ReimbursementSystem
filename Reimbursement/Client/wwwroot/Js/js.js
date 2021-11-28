$(document).ready(function () {
    var nik = document.getElementById("sessionNIK").value;
    var reimburse = document.getElementById("Reimburse");
    var employees = document.getElementById("Employees");
    var reimburseApproval = document.getElementById("ReimburseApproval");
    
    $.ajax({
        url: "/Employees/GetRole/" + nik,
        success: function (hasil) {
            var dataRole = [];
            $.each(hasil, function (key, val) {
                dataRole.push(val.roleName);
            });

            if (dataRole.find(element => element == "Finance")) {
                console.log("ini Finance");
                document.getElementById("jabatan").innerHTML = "Finance";
                employees.style.display = "none";
            } else if (dataRole.find(element => element == "HR")) {
                console.log("ini HR");
                document.getElementById("jabatan").innerHTML = "HR";
                reimburseApproval.style.display = "none";
            } else {
                console.log("ini Employee");
                document.getElementById("jabatan").innerHTML = "Employee";
                employees.style.display = "none";
                reimburseApproval.style.display = "none";
            }
        }
    });
    $.ajax({
        url: "/Employees/Get/" + nik,
        success: function (hasil) {
            var fullName = "";            
            fullName = hasil.firstName + ' ' + hasil.lastName;
            document.getElementById("fullName").innerHTML = fullName;
        }
    });
});