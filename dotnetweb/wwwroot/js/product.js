let dataTable;
$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        ajax: { url: '/Admin/Product/GetAll' },
        columns: [
            { data: 'author', 'width': "20%" },
            { data: 'title', 'width': "20%" },
            { data: 'isbn', 'width': "20%" },
            { data: 'listPrice', 'width': "5%" },
            { data: 'category.name', 'width': "15%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="d-flex flex-column flex-sm-row justify-content-center gap-2">
                            <a href = "/Admin/Product/Upsert/${data}" class="btn btn-success btn-sm" > <i class="bi bi-pencil-square"></i> Edit</a>
                            <a onclick="Delete('/Admin/Product/Delete/${data}')" class="btn btn-danger btn-sm"> <i class="bi bi-trash"></i> Delete </a>
                            </div>`
                },
                'width': "20%"
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    });
}