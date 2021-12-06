
//document.getElementById("submitReq").addEventListener("click", function (event) {
//    event.preventDefault()
//});

$("#submitReq").click(function () {
    var mailContent = new Object();
    var NIK = $("#inputNIK").val()
    mailContent.ToEmail = "abby.rafdi2@gmail.com"
    mailContent.Subject = `New Reimbursement Request`
    mailContent.Body = "Your employee with NIK :" + NIK +"has new reimbursement request waiting for approval, check on website for additional details"
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
        document.forms[0].submit();
    })
})

$("#request").validate({
    rules: {
        "nik": {
            required: true
        },
        "categoryId": {
            required: true
        },
        "description": {
            required: true
        },
        "Amount": {
            required: true
        },
        "receiptFile": {
            required: true
        },
        errorPlacement: function (error, element) { },
        highlight: function (element) {
            $(element).closest('.form-control').addClass('is-invalid');
        },
        unhighlight: function (element) {
            $(element).closest('.form-control').removeClass('is-invalid');
            $(element).closest('.form-control').addClass('is-valid ');
        }
    }
});
$('#submitReq').click(function (e) {
    e.preventDefault();
    if ($("#EmployeeForm").valid() == true) {
        console.log("valid")
    }
    else {console.log("ga valid")}
});

//biar menu ke highlight
var dashboard = document.getElementById("Dashboard")
dashboard.classList.remove("active")
var reimburse = document.getElementById("Reimburse")
reimburse.classList.add("active")
console.log("yhaa")

$("#haaa").click(function (e) {

    //nanti ini ditaro di sebelum ajax submit atau sebelum ngasih command submit form
    Swal.fire({
        title: 'Please wait..',
        allowOutsideClick: false,
        didOpen: () => {
            Swal.showLoading(false)
        }
    })

    //ini ditaro setelah command submit form atau ajax success
    Swal.close()
    Swal.fire({
        icon: 'success',
        title: 'Success',
        text: 'Your request has been submitted',
        didOpen: () => { Swal.hideLoading()}
    })
    
})