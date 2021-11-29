document.getElementById("submitReq").addEventListener("click", function (event) {
    event.preventDefault()
});

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
    }
})