$(document).ready(function () {
    var nik = document.getElementById("sessionNIK").value;
    var reimburse = document.getElementById("Reimburse");
    var employees = document.getElementById("Employees");
    var reimburseApprovalHR = document.getElementById("ReimburseApprovalHR");
    var reimburseApprovalFinance = document.getElementById("ReimburseApprovalFinance");
    
    
    
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

                reimburse.style.display = "block";
                reimburseApprovalFinance.style.display = "block";
                $("#onlyEmployee").val("No");
            } else if (dataRole.find(element => element == "HR")) {
                console.log("ini HR");
                document.getElementById("jabatan").innerHTML = "HR";

                reimburse.style.display = "block";
                employees.style.display = "block";
                reimburseApprovalHR.style.display = "block";
                $("#onlyEmployee").val("No");
            } else {
                console.log("ini Employee");
                document.getElementById("jabatan").innerHTML = "Employee";

                reimburse.style.display = "block";
                /*$("#onlyEmployee").val("Yes");*/
                document.getElementById("onlyEmployee").value = "Yes";
            }
        }
    });
    console.log("ini abis ambil role")
    $.ajax({
        url: "/Employees/Get/" + nik,
        success: function (hasil) {
            var fullName = "";            
            fullName = hasil.firstName + ' ' + hasil.lastName;
            document.getElementById("fullName").innerHTML = fullName;
        }
    });
});