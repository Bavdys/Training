async function GetDataFromServer(url) {
    const response = await fetch(url, {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        return await response.json();
    }
}
async function CreateDataOnServer(url, data, succefulCallback, errorCallback) {
    const response = await fetch(url, {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify(data)
    });

    if (response.ok === true) {
        if (succefulCallback) {
            succefulCallback();
        }
    }
    else {
        if (errorCallback) {
            const errorData = await response.json();
            errorCallback(errorData);
        }
    }
}

async function UpdateDataOnServer(url, data, succefulCallback, errorCallback) {

    const response = await fetch(url, {
        method: "PUT",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify(data)
    });

    if (response.ok === true) {
        if (succefulCallback) {
            succefulCallback();
        }
    }
    else {
        if (errorCallback) {
            const errorData = await response.json();
            errorCallback(errorData);
        }
    }
}

async function DeleteDataOnServer(url, succefulCallback, errorCallback) {
    const response = await fetch(url, {
        method: "DELETE",
        headers: { "Accept": "application/json" }
    });

    if (response.ok === true) {
        if (succefulCallback) {
            succefulCallback();
        }
    }
    else {
        if (errorCallback) {
            const errorData = await response.json();
            errorCallback(errorData);
        }
    }
}

function SetCurrentNavigation(url) {
    var urlItems = document.querySelectorAll(".main-navigation")

    for (var i = 0; i < urlItems.length; i++) {
        var urlItem = urlItems[i].querySelector(".url")
        if (url.includes(urlItem.href)) {
            urlItem.classList.add("current")
        }
    }
}

document.querySelector("#btnCollapse").addEventListener("click", e => {
    var collapseDiv = document.querySelector("#collapse");
    var collapseIcon = document.querySelector("#icnCollapse")

    collapseDiv.classList.toggle("d-none")
    collapseIcon.classList.toggle("fa-angle-left")
    collapseIcon.classList.toggle("fa-angle-right")
 
})

document.addEventListener("DOMContentLoaded", e => {
    SetCurrentNavigation(document.URL)
});
