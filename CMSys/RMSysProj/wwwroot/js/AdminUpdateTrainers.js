const form = document.forms["trainerForm"];

document.forms["trainerForm"].addEventListener("submit", e => {
    e.preventDefault();

    const selectTrainer = form.elements["trainer"];
    const trainerId = selectTrainer.options[selectTrainer.selectedIndex].value;
    const courseId = selectTrainer.options[selectTrainer.selectedIndex].getAttribute("data-courseId");
    const data = { "IdCourse": courseId, "IdTrainer": trainerId }
    var url = new URL(`${window.location.origin}/api/admin/courses/trainers`);

    CreateDataOnServer(url, data, reset);
});

function reset() {
    GetDataFromServer(`${window.location.origin}/api${window.location.pathname}`).then(result => {
        var selectTrainer = form.elements["trainer"];
        var options = selectTrainer.options;
        var table = document.querySelector(".table")
        var tBody = table.querySelector("tbody")

        while (tBody.rows[0]) {
            tBody.deleteRow(0);
        }

        while (options[0]) {
            options[0] = null;
        }

        for (var i = 0; i < result.allTrainers.length; i++) {
            var option = new Option(result.allTrainers[i].fullName, result.allTrainers[i].id);
            options[options.length] = option;
            option.setAttribute("data-courseId", result.courseId);
        }

        for (var i = 0; i < result.courseTrainers.length; i++) {
            var tr = document.createElement("tr");

            var tdName = document.createElement("td");
            tdName.textContent = result.courseTrainers[i].fullName;

            var tdVisualOrder = document.createElement("td");
            tdVisualOrder.textContent = result.courseTrainers[i].visualOrder;

            var tdAction = document.createElement("td");
           
            var buttonDelete = document.createElement("button");
            buttonDelete.className = "btn btn-outline-danger";
            buttonDelete.setAttribute("data-trainerId", result.courseTrainers[i].id);
            buttonDelete.addEventListener("click", e => {
                e.preventDefault();

                var trainerId = e.currentTarget.getAttribute("data-trainerId");
                var url = new URL(`${window.location.origin}/api${window.location.pathname}`);
                url.searchParams.set("idTrainer", trainerId)

                DeleteDataOnServer(url, reset);
            });
            tdAction.appendChild(buttonDelete);

            var spanDelete = document.createElement("span");
            buttonDelete.appendChild(spanDelete);

            var iconDelete = document.createElement("i");
            iconDelete.className = "fas fa-trash-alt";
            spanDelete.appendChild(iconDelete);

            tr.appendChild(tdName);
            tr.appendChild(tdVisualOrder);
            tr.appendChild(tdAction);

            tBody.appendChild(tr);
        }
    })
}

reset();