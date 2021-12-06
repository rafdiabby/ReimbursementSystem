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
            console.log(result);
            $("#inputNIK").val(result[0].nik);
            $("#inputCategory").val(result[0].category);
            $("#inputDesc").val(result[0].description);
            $("#inputAmount").val(formatter.format(result[0].amount));
            $("#inputPhone").val(result[0].phone);
            $("#inputfullName").val(result[0].fullName);
            var receipt = "";
            $.each(result[0].receipt, function (key, val) {
                receipt += `<a href="/${val}" class="d-block" target="_blank""><img src="/${val}"  style="max-width:300px" class="img-thumbnail"></a>`;
                console.log(receipt);
                console.log(val);
            })
            $(`#receipts`).html(receipt)
            console.log("ga error");
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

$("#approve").click(function() {
    var obj = new Object();
    obj.id =parseInt($("#reimId").val())
    obj.statusId = 3
    obj.statusDetails = $("#statusDetails").val()
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
        })
        alert("berhasil")
        $('#approvalModal').modal('toggle');
        window.location.href = "/Dashboard"
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
var employee = document.getElementById("ReimburseApprovalHR")
employee.classList.add("active")