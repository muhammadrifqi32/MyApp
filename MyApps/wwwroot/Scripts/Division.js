var table = null;
var Departments = [];

$(document).ready(function () {
    //debugger;
    table = $('#Division').DataTable({
        "processing": true,
        "ajax": {
            url: "/Divisions/LoadDivision",
            type: "GET",
            dataType: "json",
            dataSrc: "",
        },
        "columnDefs":
            [{
                "targets": [1],
                "orderable": false
            }],
        "columns": [
            { "data": "name" },
            { "data": "deptName" },
            {
                "render": function (data, type, row) {
                    return '<button class="btn btn-warning " data-placement="left" data-toggle="tooltip" data-animation="false" title="Edit" onclick="return GetById(' + row.id + ')"> <i class="mdi mdi-pencil"></i></button >' + '&nbsp;' +
                        '<button class="btn btn-danger" data-placement="right" data-toggle="tooltip" data-animation="false" title="Delete" onclick="return Delete(' + row.id + ')"> <i class="mdi mdi-eraser"></i></button >'
                }
            }]
    });
});

function ClearScreen() {
    $('#Id').val('');
    $('#Name').val('');
    $('#DepartmentOption').val('');
    $('#Update').hide();
    $('#Save').show();
}

function LoadDepartment(element) {
    //debugger;
    if (Departments.length === 0) {
        $.ajax({
            type: "Get",
            url: "/Departments/LoadDepartment",
            success: function (data) {
                Departments = data;
                renderDepartment(element);
            }
        });
    }
    else {
        renderDepartment(element);
    }
}

function renderDepartment(element) {
    //debugger;
    var $option = $(element);
    $option.empty();
    $option.append($('<option/>').val('0').text('Select Department').hide());
    $.each(Departments, function (i, val) {
        $option.append($('<option/>').val(val.id).text(val.name));
    });
}
LoadDepartment($('#DepartmentOption'));

//function GetById(id) {
//    debugger;
//    $.ajax({
//        url: "/Divisions/GetById/" + id,
//        type: "GET",
//        contentType: "application/json;charset=utf-8",
//        dataType: "json",
//        async: false,
//        success: function (result) {
//            debugger;
//            const obj = JSON.parse(result);
//            $('#Id').val(obj.Id);
//            $('#Name').val(obj.Name);
//            $('#DepartmentOption').val(obj.DeptId);
//            $('#myModal').modal('show');
//            $('#Update').show();
//            $('#Save').hide();
//        },
//        error: function (errormessage) {
//            alert(errormessage.responseText);
//        }
//    });
//}

function GetById(id) {
    //debugger;
    $.ajax({
        url: "/Divisions/GetById/",
        data: { id: id }
    }).then((result) => {
        debugger;
        if (result) {
            $('#Id').val(result.id);
            $('#Name').val(result.name);
            $('#DepartmentOption').val(result.deptId);
            $('#myModal').modal('show');
            $('#Update').show();
            $('#Save').hide();
        }
    })
}

function Save() {
    debugger;
    var Division = new Object();
    Division.name = $('#Name').val();
    Division.deptId = $('#DepartmentOption').val();
    $.ajax({
        type: 'POST',
        url: '/Divisions/InsertOrUpdate/',
        data: Division
    }).then((result) => {
        debugger;
        if (result.statusCode === 200) {
            Swal.fire({
                position: 'center',
                type: 'success',
                title: 'Division Added Succesfully'
            }).then((result) => {
                if (result.value) {
                    table.ajax.reload();
                }
            });
        }
        else {
            Swal.fire('Error', 'Failed to Add Employee', 'error');
            ShowModal();
        }
    });
}

function Update() {
    debugger;
    var Division = new Object();
    Division.id = $('#Id').val();
    Division.name = $('#Name').val();
    Division.deptId = $('#DepartmentOption').val();
    $.ajax({
        type: "POST",
        url: '/Divisions/InsertOrUpdate/',
        data: Division
    }).then((result) => {
        debugger;
        if (result.statusCode == 200) {
            Swal.fire({
                position: 'center',
                type: 'success',
                title: 'Division Updated Successfully'
            });
            table.ajax.reload();
        } else {
            Swal.fire('Error', 'Failed to Update', 'error');
            ClearScreen();
        }
    })
}

function Delete(id) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.value) {
            debugger;
            $.ajax({
                url: "/Divisions/Delete/",
                data: { id: id }
            }).then((result) => {
                debugger;
                if (result.statusCode == 200) {
                    Swal.fire({
                        position: 'center',
                        type: 'success',
                        title: 'Delete Successfully'
                    });
                    table.ajax.reload();
                } else {
                    Swal.fire('Error', 'Failed to Delete', 'error');
                    ClearScreen();
                }
            })
        };
    });
}