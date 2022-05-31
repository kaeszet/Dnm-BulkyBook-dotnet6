var dataTable;

$(document).ready(function () {
    var url = window.location.search;
    switch (url) {
        case "?status=inprocess":
            loadDataTable("inprocess");
            break;
        case "?status=completed":
            loadDataTable("completed");
            break;
        case "?status=pending":
            loadDataTable("pending");
            break;
        case "?status=approved":
            loadDataTable("approved");
            break;
        default: loadDataTable("all");
    }


    //if (url.includes("inprocess")) {
    //    loadDataTable("inprocess");
    //}
    //loadDataTable();
    
});

function loadDataTable(status) {
    dataTable = $('#tblData').DataTable({
        ajax: {
            url: '/Admin/Order/GetAll?Status=' + status
        },
        columns: [
            { data: "id", width:"5%" },
            { data: "name", width: "25%" },
            { data: "phoneNumber", width: "15%" },
            { data: "applicationUser.email", width: "15%" },
            { data: "orderStatus", width: "15%" },
            { data: "orderTotal", width: "10%" },
            {
                data: "id",
                render: function (data) {
                    return `<div class="w-75 btn-group" role="group">
                            <a href="/Admin/Order/Details?orderId=${data}" class="btn btn-primary mx-2">
                                <i class="bi bi-pencil-square"></i> Details
                            </a>
                            </div>`
                },
                width: "5%"
            }
        ]
    });
}
