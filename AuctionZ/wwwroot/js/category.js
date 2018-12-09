
function GetCategories() {
    $.ajax({
        url: '/api/categories',
        type: 'GET',
        contentType: "application/json",
        success: function (category) {
            if (hideOrShow(category.length))
                return;
            var rows = "";
            $.each(category, function (index, category) {
                rows += row(category);
            })
            $("table tbody").append(rows);
        }
    });
}
function GetCategory(id) {
    $.ajax({
        url: '/api/categories/' + id,
        type: 'GET',
        contentType: "application/json",
        success: function (category) {
            $("#openModal").click();
            form = $("#categoryForm")[0];
            form.elements["id"].value = category.categoryId;
            form.elements["name"].value = category.name;
        }
    });
}

function CreateCategory(name) {
    $.ajax({
        url: "api/categories",
        contentType: "application/json",
        method: "POST",
        data: JSON.stringify(name),
        success: function(category) {
            reset();
            $("table tbody").append(row(category));
            hideOrShow(+category.length);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            if (xhr.status == 400) {
                console.log(xhr.responseText);
                alert(xhr.responseText);
            }
        }
    });
}

function EditCategory(id, name) {
    $.ajax({
        url: "api/categories/" + id,
        contentType: "application/json",
        method: "PUT",
        data: JSON.stringify({
            CategoryId: id,
            Name: name
        }),
        success: function (category) {
            reset();
            $("tr[data-rowid='" + category.categoryId + "']").replaceWith(row(category));
        }
    })
}


function reset() {
    var form = document.forms["categoryForm"];
    form.reset();
    form.elements["id"].value = 0;
    $("#closeButton").click();
}

function hideOrShow(len) {
    var table = $("#categoriesTable");
    if (len === 0 || len === -1) {
        table.hide();
        return true;
    }
    table.show();
    return false;
}


function DeleteCategory(id) {
    $.ajax({
        url: "api/categories/" + id,
        contentType: "application/json",
        method: "DELETE",
        success: function(category) {
            console.log(category.categoryId);
            $("tr[data-rowid='" + category.categoryId + "']").remove();
            hideOrShow(+$("td").length - 1);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            if (xhr.status == 400)
                    alert(xhr.responseText);
        }
    });
}

var row = function(category) {
    return "<tr data-rowid='" + category.categoryId +
        "'><td>" +
        category.name +
        "</td>" +
        "<td><a id='editButton' class='btn btn-success' data-id='" +
        category.categoryId +
        "'>Edit</a> | " +
        "<a id='deleteButton' class='btn btn-danger' data-id='" +
        category.categoryId +
        "'>Delete</a></td></tr>";
};

//$("#reset").click(function (e) {
//
//    e.preventDefault();
//    reset();
//})


$("#categoryForm").submit(function (e) {
    e.preventDefault();
    var id = this.elements["id"].value;
    var name = this.elements["name"].value;
    if (id == 0)
        CreateCategory(name);
    else
        EditCategory(id, name);
});


$("body").on("click", "#editButton", function () {
    var id = $(this).data("id");
    GetCategory(id);
})


$("body").on("click", "#deleteButton", function () {
    var id = $(this).data("id");
    DeleteCategory(id);
})


GetCategories();
