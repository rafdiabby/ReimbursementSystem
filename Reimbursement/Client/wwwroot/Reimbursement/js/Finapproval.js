console.log("halo")
var formatter = new Intl.NumberFormat('en-US', {
    style: 'currency',
    currency: 'IDR',
});

//prefill request form
$(document).ready(function () {
    var id = $("#reimId").val();
    $.ajax({
        url: "https://localhost:44393/API/Reimbursements/GetOnly/" + id,
        success: function (result) {
            $("#inputNIK").val(result[0].nik);
            $("#inputCategory").val(result[0].category);
            $("#inputDesc").val(result[0].description);
            $("#inputAmount").val(formatter.format(result[0].amount));
            $("#inputfullName").val(result[0].fullName);
            $("#inputPhone").val(result[0].phone);
            var receipt = "";
            $.each(result[0].receipt, function (key, val) {
                receipt += `<a href="/${val}" class="d-block" target="_blank""><img src="/${val}"  style="max-width:300px" class="img-thumbnail"></a>`;
            })
            $(`#receipts`).html(receipt)
            var nik = $("#inputNIK").val();
            console.log(nik)
            Get(nik);
            console.log("ga error");

            var nik = $("#inputNIK").val();
            console.log("input NIK nya adalah");
            console.log(nik)
            $.ajax({
                url: "https://localhost:44393/API/Reimbursements/Check/" + nik,
                success: function (result) {
                    console.log(result);
                    if (result == 0) {

                        $("#check").val("This employee doesn't have a refund yet");
                        var check = document.getElementById("check")
                        check.classList.add("text-success")
                        var stats = document.getElementById("statusDetails");
                        stats.removeAttribute("readonly")
                    }
                    else if (result == 1) {
                        $("#check").val("This employee already has a refund returned");
                        var check = document.getElementById("check")
                        check.classList.add("text-danger")
                        var stats = document.getElementById("statusDetails");
                        stats.setAttribute("readonly", true)
                    }
                }
            })

            $.ajax({
                url: "/Category/Getall/",
                success: function (result) {
                    var cat = $('#categoryId').val();
                    $.each(result, function (key, val) {
                        if (result.categoryName == cat) {
                            $('#maxValue').val(formatter.format(val.maxValue))
                        }
                        else {

                        }
                    })
                }
            })
        },
        error: function (result) {
            var content = `<h1>Error : Reimbursement ID Not Found</h1>`;
            console.log("error");
            var form = document.getElementById("registerForm");
            while (form.hasChildNodes()) {
                form.removeChild(form.firstChild);
            }
            $("#registerForm").html(content);

        }
    })

    
})

function Get(nik) {

    $.ajax({
        url: "/employees/get/" + nik,
        success: function (result) {
            $('#bankAcc').val(result.bankAccount);
            console.log("ini bank acc")
        }
    })
}

$("#approve").click(function () {
    var obj = new Object();
    obj.id = parseInt($("#reimId").val())
    obj.statusId = 4
    obj.statusDetails = "Refunded : IDR." + $("#statusDetails").val()
    console.log(obj)
    $.ajax({
        //headers: {
        //    'Accept': 'application/json',
        //    'Content-Type': 'application/json'
        //},
        'url': "/Reimburse/UpdateStatus",
        'type': "PATCH",
        'data': { reimbursement: obj },
        'dataType': 'json'
    }).done((result) => {
        var mailContent = new Object();
        mailContent.ToEmail = "abby.rafdi2@gmail.com"
        mailContent.Subject = "Reimbursement Status Update"
        mailContent.Body = "Information : your reimbursement request has been approved and waiting for transfer process, please check on website for additional details"
        $.ajax({
            //headers: {
            //    'Accept': 'application/json',
            //    'Content-Type': 'application/json'
            //},
            'url': "/Mail/SendMail",
            'type': "POST",
            'data': { mail: mailContent },
            'dataType': 'json'
        });
        var obja = new Object();
        obja.reimId = parseInt($("#reimId").val())
        obja.statusId = 4
        console.log(obja);

            alert("berhasil");
            $('#approvalModal').modal('toggle');
            window.location.href = "/Finance";

    }).fail((error) => {
        alert(
            'Terjadi kesalahan'
        );
    })

})

$("#reject").click(function () {


    var obj = new Object();
    obj.id = parseInt($("#reimId").val())
    obj.statusId = 2
    obj.statusDetails = $("#statusDetails2").val()
    console.log(obj)
    $.ajax({
        //headers: {
        //    'Accept': 'application/json',
        //    'Content-Type': 'application/json'
        //},
        'url': "/Reimburse/UpdateStatus",
        'type': "PATCH",
        'data': { reimbursement: obj },
        'dataType': 'json'
    }).done((result) => {
        var mailContent = new Object();
        mailContent.ToEmail = "abby.rafdi2@gmail.com"
        mailContent.Subject = "Reimbursement Status Update"
        mailContent.Body = "Sorry, your reimbursement has been rejected, please check on website for additional details"
        $.ajax({
            //headers: {
            //    'Accept': 'application/json',
            //    'Content-Type': 'application/json'
            //},
            'url': "/Mail/SendMail",
            'type': "POST",
            'data': { mail: mailContent },
            'dataType': 'json'
        }).done((result) => {
            alert("berhasil")
            $('#rejectModal').modal('toggle');
            window.location.href = "/Dashboard"

        }).fail((error) => {
            alert(
                'Terjadi kesalahan'
            );

        }).fail((error) => {
            alert(
                'Terjadi kesalahan'
            );
        })


    })
})

//biar menu ke highlight
var dashboard = document.getElementById("Dashboard")
dashboard.classList.remove("active")
var employee = document.getElementById("ReimburseApprovalFinance")
employee.classList.add("active")


