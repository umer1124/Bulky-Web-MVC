var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').dataTable({
        "ajax": { url: '/admin/user/getall' },
        "columns": [
            { data: 'name', "width": "20%" },
            { data: 'email', "width": "15%" },
            { data: 'phoneNumber', "width": "10%" },
            { data: 'company.name', "width": "10%" },
            { data: 'role', "width": "5%" },
            {
                data: { id: 'id', lockoutEnd: 'lockoutEnd' },
                "render": function (data) {
                    var today = new Date().getTime();
                    var lockout = new Date(data.lockoutEnd).getTime();

                    if (lockout > today) {
                        return `
                            <div class="text-center">
                                <a onclick="lockUnlock('${data.id}')" class="btn btn-danger text-white"> 
                                    <i class="bi bi-lock-fill"></i> Lock
                                </a>
                                <a href="/Admin/User/RoleManagement?userId=${data.id}" class="btn btn-danger text-white"> 
                                    <i class="bi bi-pencil-square"></i> Permission
                                </a>
                            </div>
                        `
                    }
                    else {
                        return `
                            <div class="text-center">
                                <a onclick="lockUnlock('${data.id}')" class="btn btn-success text-white"> 
                                    <i class="bi bi-unlock-fill"></i> Unlock
                                </a>
                                <a href="/Admin/User/RoleManagement?userId=${data.id}" class="btn btn-danger text-white"> 
                                    <i class="bi bi-pencil-square"></i> Permission
                                </a>
                            </div>
                        `
                    }
                    
                },
                "width": "25%"
            }
        ]
    });
}

function lockUnlock(id) {
    $.ajax({
        type: "POST",
        url: '/Admin/User/LockUnlock',
        data: JSON.stringify(id),
        contentType: 'application/json',
        success: function (data) {
            dataTable.api().ajax.reload();
            toastr.success(data.message);
        }
    });
}