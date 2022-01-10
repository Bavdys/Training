const form = document.forms["roleForm"];

document.forms["roleForm"].addEventListener("submit", e => {
    e.preventDefault();

    const selectRole = form.elements["role"];
    const roleId = selectRole.options[selectRole.selectedIndex].value;
    const userId = selectRole.options[selectRole.selectedIndex].getAttribute("data-userId");
    const data = { "IdRole": roleId, "IdUser": userId }
    var url = new URL(`${window.location.origin}/api/admin/users/update`);

    CreateDataOnServer(url, data, reset);
});

function reset() {
    GetDataFromServer(`${window.location.origin}/api${window.location.pathname}`).then(result => {
        var selectRole = form.elements["role"];
        var options = selectRole.options;
        var table = document.querySelector(".table")
        var tBody = table.querySelector("tbody")
       
        while (tBody.rows[0]) {
            tBody.deleteRow(0);
        }

        while (options[0]) {
            options[0] = null;
        }

        for (var i = 0; i < result.allRoles.length; i++) {
            var option = new Option(result.allRoles[i].name, result.allRoles[i].id);
            options[options.length] = option;
            option.setAttribute("data-userId", result.userId);
        }

        for (var i = 0; i < result.userRoles.length; i++) {
            var tr = document.createElement("tr");

            var tdName = document.createElement("td");
            tdName.textContent = result.userRoles[i].name;

            var tdAction = document.createElement("td");

            var buttonDelete = document.createElement("button");
            buttonDelete.className = "btn btn-outline-danger";
            buttonDelete.setAttribute("data-roleId", result.userRoles[i].id);
            buttonDelete.addEventListener("click", e => {
                e.preventDefault();

                var roleId = e.currentTarget.getAttribute("data-roleId");
                var url = new URL(`${window.location.origin}/api${window.location.pathname}`);
                url.searchParams.set("idRole", roleId)

                DeleteDataOnServer(url, reset);
            });
            tdAction.appendChild(buttonDelete);

            var spanDelete = document.createElement("span");
            buttonDelete.appendChild(spanDelete);

            var iconDelete = document.createElement("i");
            iconDelete.className = "fas fa-trash-alt";
            spanDelete.appendChild(iconDelete);

            tr.appendChild(tdName);
            tr.appendChild(tdAction);

            tBody.appendChild(tr);
        }
    })
}

reset();