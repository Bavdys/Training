const exampleModal = document.querySelector('#groupsModal')
const exampleErrorModal = document.querySelector('#groupsErrorModal');
const form = document.forms["modalForm"];

$(exampleModal).on("show.bs.modal", function (event) {
    document.getElementById("errors").innerHTML = "";

    var button = event.relatedTarget
    var id = button.getAttribute('data-bs-id')

    if (id) {
        GetDataFromServer(`${window.location.origin}/api${window.location.pathname}/${id}`).then(result => {
            form.elements["Id"].value = result.id
            form.elements["Name"].value = result.name
            form.elements["VisualOrder"].value = result.visualOrder
            form.elements["Description"].value = result.description
        })
    }
})

$(exampleModal).on('hidden.bs.modal', function (event) {
    form.reset();
    form.elements["Id"].value = 0;
})

document.forms["modalForm"].addEventListener("submit", e => {
    e.preventDefault();
    document.getElementById("errors").innerHTML = "";
 
    const id = form.elements["Id"].value;
    const name = form.elements["Name"].value;
    const order = form.elements["VisualOrder"].value;
    const description = form.elements["Description"].value;

    var data = { "Name": name, "Description": description };

    if (order !== "") {
        data["VisualOrder"] = order;
    }
    
    if (id == 0) {
        CreateDataOnServer(`${window.location.origin}/api${window.location.pathname}`, data, reset, error);
    }
    else {
        data["Id"] = id;
        UpdateDataOnServer(`${window.location.origin}/api${window.location.pathname}`, data, reset, error);
    }
});

function reset() {
    $('#groupsModal').modal('hide');

    GetDataFromServer(`${window.location.origin}/api${window.location.pathname}`).then(data => {
        var table = document.querySelector(".table")
        var tBody = table.querySelector("tbody")

        while (tBody.rows[0]) {
            tBody.deleteRow(0);
        }

        for (var i = 0; i < data.length; i++) {
            var tr = document.createElement("tr");

            var thName = document.createElement("th");
            thName.setAttribute("scope", "row");
            thName.textContent = data[i].name;

            var tdDescription = document.createElement("td");
            tdDescription.textContent = data[i].description;

            var tdVisualOrder = document.createElement("td");
            tdVisualOrder.textContent = data[i].visualOrder;

            var tdAction = document.createElement("td");
            var div = document.createElement("div");
            div.classList.add("d-flex");
            tdAction.appendChild(div);

            var buttonUpdate = document.createElement("button");
            buttonUpdate.className = "btn btn-outline-warning mr-1";
            buttonUpdate.setAttribute("data-toggle", "modal");
            buttonUpdate.setAttribute("data-target", "#groupsModal"); 
            buttonUpdate.setAttribute("data-bs-id", data[i].id);
            div.appendChild(buttonUpdate);

            var spanUpdate = document.createElement("span");
            buttonUpdate.appendChild(spanUpdate);

            var iconUpdate = document.createElement("i");
            iconUpdate.className = "fas fa-pen";
            spanUpdate.appendChild(iconUpdate);

            var buttonDelete = document.createElement("button");
            buttonDelete.className = "btn btn-outline-danger";
            buttonDelete.setAttribute("data-bs-id",data[i].id);
            buttonDelete.addEventListener("click", e => {
                e.preventDefault();

                var id = e.currentTarget.getAttribute("data-bs-id");

                DeleteDataOnServer(`https://${window.location.host}/api${window.location.pathname}/${id}`, reset, showModalError);
            });
            div.appendChild(buttonDelete);

            var spanDelete = document.createElement("span");
            buttonDelete.appendChild(spanDelete);

            var iconDelete = document.createElement("i");
            iconDelete.className = "fas fa-trash-alt";
            spanDelete.appendChild(iconDelete);

            tr.appendChild(thName);
            tr.appendChild(tdDescription);
            tr.appendChild(tdVisualOrder);
            tr.appendChild(tdAction);

            tBody.appendChild(tr);
        }
    })
}

function showModalError() {
    $("#groupsErrorModal").modal('show');
}

function error(errorData) {
    if (errorData) {
        if (errorData.errors) {
            if (errorData.errors["Name"]) {
                addError(errorData.errors["Name"]);
            }
            if (errorData.errors["VisualOrder"]) {
                addError(errorData.errors["VisualOrder"]);
            }
        }
    }
}

function addError(errors) {
    errors.forEach(error => {
        const p = document.createElement("p");
        p.append(error);
        document.getElementById("errors").append(p);
    });
}

reset();


