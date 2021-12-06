console.log("HALO")

//number formatter
var formatter = new Intl.NumberFormat('en-US', {
    style: 'currency',
    currency: 'IDR',

    // These options are needed to round to whole numbers if that's what you want.
    //minimumFractionDigits: 0, // (this suffices for whole numbers, but will print 2500.10 as $2,500.1)
    //maximumFractionDigits: 0, // (causes 2500.99 to be printed as $2,501)
});


//using datatable
$(document).ready(function () {
    console.log("ini dashboard")
    var onlyNoob = document.getElementById("jabatan").value;
    var nik = document.getElementById("sessionNIK").value;

    var url = "https://localhost:44393/API/Reimbursements/GetAll/"

    var data = $('table.table').DataTable({
        'responsive': true,
        'ajax': {
            'url': url,
            'dataSrc': ''
        },
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'excelHtml5',
                name: 'excel',
                title: 'Reimbursement',
                sheetName: 'Reimbursement',
                text: '',
                className: 'fa fa-download btn-default hidden',
                filename: 'Reimbursement',
                autoFilter: true,
                exportOptions: {
                    columns: [0, 1, 2, 3,4,5,6]
                }
            }
        ],
        drawCallback: function () {
            $('.hidden')[0].style.visibility = 'hidden'
            $('.dt-buttons')[0].style.visibility = 'hidden'
        },
        "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
        'columns': [
            {
                "data": "reimId"
            },
            {
                "data": "nik"
            },
            {
                "data": "fullName",
            },
            {
                "data": "category"
            },
            {
                "data": "description",
            },
            {
                "data": "",
                "render": function (data, type, row, meta) {
                    return formatter.format(row['amount'])
                }
            },
            {
                "data": "",
                "render": function (data, type, row, meta) {
                    if (row['status'] == "Rejected") {
                        return `<span class="badge badge-danger">${row['status']}</span>`
                    } else if (row['status'] == "Approved") {
                        return `<span class="badge badge-primary">${row['status']}</span>`
                    } else if (row['status'] == "Submitted") {
                        return `<span class="badge badge-secondary">${row['status']}</span>`
                    } else if (row['status'] == "Refunded") {
                        return `<span class="badge badge-success">${row['status']}</span>`
                    }
                }
            },
            {
                "data": "statusDetails"
            },
            {
                "data": "",
                "render": function (data, type, row, meta) {
                    return `<a href="../Approval/${row['reimId']}" data-toggle="tooltip" data-placement="top" title="Request details"><button type="button" class="btn btn-info" >Details</button></a>`;
                },
                "orderable": false
            }
        ]
    });
    $('body').tooltip({
        selector: "[data-tooltip=tooltip]",
        container: "body"
    });
    data.on('draw', function () {
        $('[data-toggle="tooltip"]').tooltip();
    })

    $('#reimDataRejected').DataTable().search('Rejected').draw();
    $('#reimDataApprove').DataTable().search('Approved').draw();
    $('#reimDataSubmitted').DataTable().search('Submitted').draw();
    $('#reimDataRefunded').DataTable().search('Refunded').draw();
    
})
$("#exportExcel").on('click', function (e) {
    ExportExcel();
});
//excel export button
function ExportExcel() {
    var table = $('#reimData').DataTable();
    table.buttons('.buttons-excel').trigger();
}

//biar menu ke highlight
var dashboard = document.getElementById("Dashboard")
dashboard.classList.remove("active")
var employee = document.getElementById("ReimburseApprovalHR")
employee.classList.add("active")